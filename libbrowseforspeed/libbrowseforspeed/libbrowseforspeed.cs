// Copyright (C) 2006 Richard Nelson, Ben Kenny, Philip Nelson
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

// project created on 21/05/2006 at 8:20 A

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using System.Timers;
using ICSharpCode.SharpZipLib.GZip;
using System.Text.RegularExpressions;


namespace libbrowseforspeed {
	public class ServerInformation : EventArgs {
		public bool success;
		public bool connectFailed;
		public bool readFailed;
		public int totalServers;
		public IPEndPoint host;
		public int slots;
		public int players;
		public ulong rules;
		public ulong cars;
		public string hostname;
		public string track;		
		public int ping;
		public bool passworded;
		public string[] racerNames;
		public string password;
	}
			
	public struct hostInfo {
		public IPEndPoint host;
		public bool passworded;
		public object callbackObj;
	}	

	public delegate void ServerQueried(object o, ServerInformation info, object callbackObj);

	public class LFSQuery {

		public static ulong[] CAR_BITS = {1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384, 32768, 65536, 131072, 262144};
		public static string[] CAR_NAMES = {"XFG", "XRG", "XRT", "RB4", "FXO", "LX4", "LX6", "MRT", "UF1", "RAC", "FZ5", "FOX", "XFR", "UFR", "FO8", "FXR", "XRR", "FZR", "BF1"};
		public static ulong[] CAR_GROUP_BITS = {524287, 280704, 229376, 12561, 1600, 259, 28};
		public static string[] CAR_GROUP_NAMES = {"ALL", "SS", "GTR", "FWD", "LRF", "STD", "TBO"};
		public static ulong[] CAR_GROUP_DISALLOW = {0, 243583, 280704, 511726, 522368, 524028, 392896};
		public static ulong[] CAR_GROUP_DONTCARE = {524287, 280704, 14207, 12561, 319, 259, 291};
		
		public static int QTHREADS = 16;
		public static int THREAD_WAIT = 150;
		public static bool xpsp2_wait = true;
		
		public const ulong CAR_XFG = 1;
		public const ulong CAR_XRG = 2;
		public const ulong CAR_XRT = 4;
		public const ulong CAR_RB4 = 8;
		public const ulong CAR_FXO = 16;
		public const ulong CAR_LX4 = 32;
		public const ulong CAR_LX6 = 64;
		public const ulong CAR_MRT = 128;
		public const ulong CAR_UF1 = 256;
		public const ulong CAR_RAC = 512;
		public const ulong CAR_FZ5 = 1024;
		public const ulong CAR_FOX = 2048;
		public const ulong CAR_XFR = 4096;
		public const ulong CAR_UFR = 8192;
		public const ulong CAR_FO8 = 16384;
		public const ulong CAR_FXR = 32768;
		public const ulong CAR_XRR = 65536;
		public const ulong CAR_FZR = 131072;
		public const ulong CAR_BF1 = 262144;

		//car groups
		public const ulong CARS_ALL = 524287;
		public const ulong CARS_STD = 259;
		public const ulong CARS_TBO = 28;
		public const ulong CARS_LRF = 1600;
		public const ulong CARS_FWD = 12561;
		public const ulong CARS_GTR = 229376;
		public const ulong CARS_SS = 280704;

		private static bool keepQuerying;
		private static int totalServers;
		
		private static Stream pubstatStream;
		private static int pubstatLastUpdate;
		private const int PUBSTAT_CACHE_TIME = 1000 * 30; //30 second cache

		public static event ServerQueried queried;
	
		public class ServerQuery {
			private hostInfo host;
			private System.Threading.ManualResetEvent timeoutEvent;
			private System.Threading.ManualResetEvent readTimeoutEvent;

			public void connectCallback(IAsyncResult ar) {
				Socket sock = (Socket)ar.AsyncState;
				if (sock != null) {
					try {
					 	sock.EndConnect(ar);
					} catch (Exception e) { return; }
				} else {
					return;
				}
				timeoutEvent.Set();
			}
			
			public void readCallback(IAsyncResult ar) {				
				NetworkStream str = (NetworkStream)ar.AsyncState;				
				if (str != null) {
					try {
						str.EndRead(ar);
					} catch (Exception e) { return; }
				} else {
					return;
				}
				readTimeoutEvent.Set();
			}
			
			public void setHost(hostInfo host) {
				this.host = host;
			}

			byte[] send_query1 = {
				0x0c, 0x02, 0x05, 0x55, 0x00, 0x1d, 0x01, 0x2f,
				0x4e, 0x00, 0x00, 0x00, 0x00
			};
			byte[] send_query2 = {
				0x0c, 0x02, 0x05, 0x55, 0x00, 0x1d, 0x02, 0x2f,
				0x4e, 0x00, 0x00, 0x00, 0x00
			};
			/*byte[] send_query3 = {
				0x0c, 0x02, 0x05, 0x55, 0x00, 0x1d, 0x02, 0x2f,
				0x4e, 0x00, 0x00, 0x00, 0x00
			};*/

			public ServerQuery() { }

			public void queryServer() {				
				IPEndPoint endpoint = new IPEndPoint(host.host.Address, host.host.Port);
				Socket sock = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				timeoutEvent = new System.Threading.ManualResetEvent(false);				
				sock.BeginConnect(endpoint, new AsyncCallback(connectCallback), sock);
				if (timeoutEvent.WaitOne(1000, false)) {
					//connected aok!
					NetworkStream str = new NetworkStream(sock);					
					//str.ReadTimeout = 500; //.NET 2.0 :(
					str.Write(send_query1, 0, send_query1.Length);
					ServerInformation serverinfo = query1(ref str);
					if (serverinfo.success) {
						str.Write(send_query2, 0, send_query2.Length);
						query2(ref str, ref serverinfo);
					}
					//client.Close();
					try {
						str.Close();
						sock.Shutdown(SocketShutdown.Both);
						sock.Close();						
					} catch (Exception e) { }
					if (queried != null) {
						queried(this, serverinfo, host.callbackObj);
					}
				} else {
					//connect FAILED BOOHOO
					ServerInformation ret = new ServerInformation();
					ret.success = false;
					ret.connectFailed = true;
					ret.totalServers = LFSQuery.totalServers;
					if (sock != null) {
						try {
							sock.Shutdown(SocketShutdown.Both);
							sock.Close();
						} catch (Exception e) { }
					}
					queried(this, ret, host.callbackObj);
				}
			}
			
			private ServerInformation query1(ref NetworkStream str) {
				readTimeoutEvent = new System.Threading.ManualResetEvent(false);
				byte[] recbuf = new byte[37];				
				//int rcn = 0;
				//int pingStart = System.Environment.TickCount;
				DateTime time = System.DateTime.Now;
				long pingStart = time.Ticks;
				str.BeginRead(recbuf, 0, recbuf.Length, new AsyncCallback(readCallback), str);
				if (readTimeoutEvent.WaitOne(1000, false)) {
					//Console.Write("OKAY!\n");
				} else {
					ServerInformation ret = new ServerInformation();
					ret.success = false;
					ret.readFailed = true;
					return ret;
				}
				/*try {
					rcn = str.Read(recbuf, 0, recbuf.Length);
				} catch (Exception e) {
					serverInformation ret = new serverInformation();
					ret.success = false;
					ret.readFailed = true;
					//queried(this, ret);
					return ret;
				}*/
				time = System.DateTime.Now;
				long pingEnd = time.Ticks;
				//int pingEnd = System.Environment.TickCount;
				/*if (rcn != recbuf.Length) {					
					serverInformation ret = new serverInformation();
					ret.success = false;
					ret.readFailed = true;
					//queried(this, ret);
					return ret;
				}*/
				readTimeoutEvent.Close();
				ServerInformation serverinfo = new ServerInformation();
				serverinfo.success = true;
				serverinfo.ping = (int)((pingEnd - pingStart) / 10000);
				serverinfo.rules = (ulong)(recbuf[4] * 256 + recbuf[3]);
				serverinfo.players = (int)recbuf[1];
				serverinfo.slots = (int)recbuf[2];
				serverinfo.host = host.host;
				serverinfo.passworded = host.passworded;
				serverinfo.totalServers = LFSQuery.totalServers;				
				serverinfo.hostname = getLFSString(recbuf, 5, 32);				
				return serverinfo;
			}

			private void query2(ref NetworkStream str, ref ServerInformation serverinfo) {
				readTimeoutEvent = new System.Threading.ManualResetEvent(false);
				byte[] recbuf = new byte[13];
				//int rcn = 0;
				str.BeginRead(recbuf, 0, recbuf.Length, new AsyncCallback(readCallback), str);
				if (readTimeoutEvent.WaitOne(1000, false)) {
				} else {
					serverinfo.success = false;
					serverinfo.readFailed = true;
					return;
				}
				readTimeoutEvent.Close();
				/*try {
					rcn = str.Read(recbuf, 0, recbuf.Length);
				} catch (Exception e) {
					serverinfo.success = false;
					serverinfo.readFailed = true;
					return;
				}
				if (rcn != recbuf.Length) {					
					serverinfo.success = false;
					serverinfo.readFailed = true;					
					return;
				}*/
				serverinfo.success = true;
				serverinfo.cars = (ulong)(recbuf[12] * 16777216 + recbuf[11] * 65536 + recbuf[10] * 256 + recbuf[9]);
				Decoder d = Encoding.ASCII.GetDecoder();
				char[] track = new char[32];
				d.GetChars(recbuf, 1, 4, track, 0);
				serverinfo.track = new string(track);
			}
		}

		private class Query {
			static byte[] header = { 0x4c, 0x4c, 0x46, 0x53, 0x00}; //LLFS\0
			static byte[] client_version_info = {0x04, 0x1d, 0x00, 0x02, 0x02, 0x05, 0x55, 0x00};
			public static ulong cars_compulsory;
			public static ulong cars_illegal;
			public byte[] un = new byte[20];
			public byte[] username = new byte[24];
			public static byte[] unknown = {0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
			public static byte[] footer = { 0x2f, 0x4e }; // /N;

			public Query(ulong cars_compulsory, ulong cars_illegal, string user) {
				Query.cars_compulsory = cars_compulsory;
				Query.cars_illegal = cars_illegal;
				Encoding ascii = Encoding.ASCII;
				ascii.GetBytes(user.ToCharArray(), 0, user.Length, this.username, 0);
			}			

			public void setFilters(ulong cars_compulsory, ulong cars_illegal) {
				Query.cars_compulsory = cars_compulsory;
				Query.cars_illegal = cars_illegal;
			}

			public byte[] getBytes() {
				byte[] ret = new byte[77];
				int pos = 0;
				Array.Copy(header, ret, header.Length);
				pos += header.Length;
				Array.Copy(client_version_info, 0, ret, pos, client_version_info.Length);
				pos += client_version_info.Length;
				ret[13] = (byte)cars_compulsory;
				ret[14] = (byte)(cars_compulsory >> 8);
				ret[15] = (byte)(cars_compulsory >> 16);
				ret[16] = (byte)(cars_compulsory >> 24);
				ret[17] = (byte)cars_illegal;
				ret[18] = (byte)(cars_illegal >> 8);
				ret[19] = (byte)(cars_illegal >> 16);
				ret[20] = (byte)(cars_illegal >> 24);
				pos += 8;
				Array.Copy(un, 0, ret, pos, un.Length);
				pos += un.Length;
				Array.Copy(username, 0, ret, pos, username.Length);
				pos += username.Length;
				Array.Copy(unknown, 0, ret, pos, unknown.Length);
				pos += unknown.Length;
				Array.Copy(footer, 0, ret, pos, footer.Length);
				return ret;
			}

		}

		public LFSQuery() {

		}

		public void query(ulong cars_compulsory, ulong cars_illegal, string username, object callbackObj) {
			LFSQuery.keepQuerying = true;
			ArrayList allHosts = new ArrayList();
			Query query = new Query(cars_compulsory, cars_illegal, username);			
			TcpClient client = new TcpClient("82.44.126.169", 29339);
			Stream str = client.GetStream();
			ulong hosts = 1;
			byte[] b = query.getBytes();
			QueryThreadManager m = new QueryThreadManager();
			for (ulong i = 0; i < hosts; i++) {
				IPEndPoint[] ips = new IPEndPoint[16]; //up to 16 addresses per packet
				str.Write(b, 0, b.Length);
				byte[] recbuf = new byte[117];
				str.Read(recbuf, 0, recbuf.Length);
				if (i == 0) {
					LFSQuery.totalServers = (int)(recbuf[2] * 256 + recbuf[1]);
				}
				for (int j = 0; j < 16; j++) {					
					byte[] ip = new byte[4];
					int port;					
					Array.Copy(recbuf, 21 + j * 4, ip, 0, 4); //21 = offset of first IP
					port = (int)(recbuf[86 + j * 2] * 256 + recbuf[85 + j * 2]); // 85 = offset of first port
					if (port == 0) break; //all done
					ips[j] = new IPEndPoint(IPAddress.Parse(ip[0].ToString() + "."
														+ ip[1].ToString() + "."
														+ ip[2].ToString() + "."
														+ ip[3].ToString()), port);
					hostInfo h;
					h.host = ips[j];
					h.passworded = (recbuf[5+j] == 0x09);
					h.callbackObj = callbackObj;
					allHosts.Add(h);
					m.addHost(h);
				}
				hosts = (ulong)(recbuf[2] * 256 + recbuf[1]);
			}
			client.Close();
			m.allDone();
			m.getThread().Join();
		}
		
		public static void stopQuerying() {
			LFSQuery.keepQuerying = false;
		}
	
		public void query(ulong cars_compulsory, ulong cars_illegal, string username, IPEndPoint[] hosts, object callbackObj) {
			LFSQuery.keepQuerying = true;			
			QueryThreadManager m = new QueryThreadManager();
			totalServers = hosts.Length;
			foreach (IPEndPoint host in hosts) {
				hostInfo h;
				h.host = host;
				h.passworded = false;
				h.callbackObj = callbackObj;
				m.addHost(h);
			}
			m.allDone();
			m.getThread().Join();
		}

		public string findUser(string username, string userToFind) {
			byte[] query = { //VERY SIMILAR TO Query class...fix this
				0x4c, 0x4c, 0x46, 0x53, 0x00, 0x04, 0x1d, 0x04, 
				0x00, 0x54, 0x75, 0x6b, 0x6b, 0x6f, 0x00, 0x00, 
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
				0x00, 0x53, 0x6e, 0x6f, 0x6f, 0x6b, 0x6c, 0x65, 
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
				0x00, 0x19, 0x00, 0xe4, 0xa3, 0xff, 0xbe, 0x59,
				0xf2, 0x00, 0x00, 0x2f, 0x4e
			};
			byte[] user = new byte[24];
			byte[] finduser = new byte[24];
			byte[] recbuf = new byte[41];
			Encoding ascii = Encoding.ASCII;
			ascii.GetBytes(username.ToCharArray(), 0, username.Length, user, 0);
			ascii.GetBytes(userToFind.ToCharArray(), 0, userToFind.Length, finduser, 0);
			Array.Copy(user, 0, query, 41, user.Length); //41 = username offset
			Array.Copy(finduser, 0, query, 9, finduser.Length); //9 = user to find offset
			TcpClient client = new TcpClient("82.44.126.169", 29339);
			//TcpClient client = new TcpClient("127.0.0.1", 29339);
			Stream str = client.GetStream();
			str.Write(query, 0, query.Length);
			str.Read(recbuf, 0, recbuf.Length);
			client.Close();
			if (recbuf[0] == 0x20) {
				return null;
			} else {
				Decoder d = Encoding.ASCII.GetDecoder();
				char[] hostchars = new char[32];
				d.GetChars(recbuf, 1, 32, hostchars, 0);
				return new string(hostchars);
			}			
		}

		public static void changeFilters(ulong cars_compulsory, ulong cars_illegal) {
			Query.cars_illegal = cars_illegal;
			Query.cars_compulsory = cars_compulsory;
		}

		private class QueryThreadManager {
			private bool keepgoing = true;
			private Thread[] queries;			
			private Queue myQueue;
			private Thread myThread;
			public QueryThreadManager() {
				myQueue = new Queue();
				queries = new Thread[QTHREADS];
				myThread = new Thread(new ThreadStart(this.run));
				myThread.Start();
			}
			private void run() {
				while ((keepgoing || myQueue.Count > 0) && LFSQuery.keepQuerying) {
					if (myQueue.Count > 0) {
						for (int i = 0; i < queries.Length; ++i) {
							if (queries[i] == null || queries[i].ThreadState == ThreadState.Unstarted || queries[i].ThreadState == ThreadState.Stopped) {
								if (xpsp2_wait) {
									Thread.Sleep(THREAD_WAIT); // THANKS BILL, YOU FREAKIN IDIOT
								}
								ServerQuery s = new ServerQuery();
								s.setHost((hostInfo)myQueue.Dequeue());
								queries[i] = new Thread(new ThreadStart(s.queryServer));
								queries[i].Start();
								break;
							}
						}
					}
				}
				if (!LFSQuery.keepQuerying) {
					myQueue.Clear();
					allDone(); //might have been finished by LFSQuery.keepQuerying. need to join left over threads.
				}
			}
			public void addHost(hostInfo host) {
				myQueue.Enqueue(host);
			}
			public void allDone() {
				int j = 0;
				int i = 0;
				while (true) { //Join threads until there are none to join 
					if (queries[i] != null && queries[i].ThreadState != ThreadState.Stopped) {
						queries[i].Join();
					} else {
						if (++j == QTHREADS) break;
					}
					if (++i == QTHREADS) {
						i = 0;
						j = 0;
					}
				}				
				keepgoing = false;
			}

			public Thread getThread() { return myThread; }
		}
		
		public static Array getCarNames(ulong c) {
			ArrayList carNames = new ArrayList();
			for (int i = 0; i < CAR_GROUP_BITS.Length; ++i) {
				if (((c & CAR_GROUP_BITS[i]) != 0) && c >= CAR_GROUP_BITS[i]) {
					carNames.Add(CAR_GROUP_NAMES[i]);					
					c -= CAR_GROUP_BITS[i];
				}
			}			
			for (int i = 0; i < LFSQuery.CAR_BITS.Length; ++i) {
				if ((c & CAR_BITS[i]) != 0) {
					carNames.Add(CAR_NAMES[i]);					
					c -= CAR_BITS[i];
				}
			}
			return carNames.ToArray();
		}
		
		public static void clearPubstatCache() {
			pubstatStream = null;
		}

		public static int getPubStatInfo(ref ServerInformation serverInfo) {
			try {
				if (pubstatStream == null || System.Environment.TickCount > (pubstatLastUpdate + PUBSTAT_CACHE_TIME)) {
					HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.lfsworld.net/pubstat/get_stat2.php?action=hosts&c=1");
					request.Timeout = 4000;
					HttpWebResponse response = (HttpWebResponse)request.GetResponse();
					pubstatStream = response.GetResponseStream();
					pubstatLastUpdate = System.Environment.TickCount;
				}
				Stream s = new GZipInputStream(pubstatStream);
				byte[] buf = getStreamBytes(s);
				s.Close();
				//pubstream.Close();
				int i = 0;
				string[] racers = null;
				
				while (i < buf.Length) {
					string hostname = removeColourCodes(getLFSString(buf, i, 32));
					int numRacers = (int)buf[i + 52];
					if (hostname == serverInfo.hostname) {
						racers = new string[numRacers];
						for (int j = 0; j < numRacers; ++j) {
							racers[j] = getLFSString(buf, i + 53 + (24 * j), 24);
						}
						serverInfo.players = numRacers;
						serverInfo.racerNames = racers;
						serverInfo.passworded = ((ulong)(buf[i + 47] * 16777216 + buf[i + 46] * 65536 + buf[i + 45] * 256 + buf[i + 44]) & 8) != 0;
						return 1;
					}
					i += (53 + (24 * numRacers));
				}
			} catch (Exception e) {
				return -1;
			}
			return 0;
		}

		private static byte[] getStreamBytes(Stream stream) {
			byte[] buf = new byte[32768];
			using (MemoryStream ms = new MemoryStream()) {
				while (true) {
					int read = stream.Read(buf, 0, buf.Length);
					if (read <= 0) {
						return ms.ToArray();
					}
					ms.Write(buf, 0, read);
				}
			}
		}

		private static string getLFSString(byte[] buf, int startpos, int maxlen) {
			int i;
			int endpos = 0;
			for (i = 0; i < maxlen; ++i) {
				if (buf[i + startpos] == 0x00) {
					endpos = i;
					break;
				}
			}
			return Encoding.ASCII.GetString(buf, startpos, endpos);
		}
		
		public static string removeColourCodes(string text) {
			text = Regex.Replace(text, "\\^\\d", "");
			text = Regex.Replace(text, "\\^\\^", "^");
			return text;
		}
	}
}
