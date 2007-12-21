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
		public int adminslots;
		public int players;
		public ulong rules;
		public ulong cars;
		public string rawHostname;
		public string hostname;		
		public string track;
		public int ping;
		public bool passworded;
		public string[] racerNames;
		public string password;
		public byte version;
		public string adminPassword;
		public int insimPort;
	}

	public struct hostInfo {
		public ServerInformation info;
		public bool passworded;
		public object callbackObj;
		public byte version;
	}

	public delegate void ServerQueried(object o, ServerInformation info, object callbackObj);

	public class LFSQuery {

		public static ulong[] CAR_BITS = {1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384, 32768, 65536, 131072, 262144, 1 << 19};
		public static string[] CAR_NAMES = {"XFG", "XRG", "XRT", "RB4", "FXO", "LX4", "LX6", "MRT", "UF1", "RAC", "FZ5", "FOX", "XFR", "UFR", "FO8", "FXR", "XRR", "FZR", "BF1", "FBM"};
		public static ulong[] CAR_GROUP_BITS = {((1 << 20) - 1), 804992, 229376, 12561, 1600, 259, 28};
		public static string[] CAR_GROUP_NAMES = {"ALL", "SS", "GTR", "FWD", "LRF", "STD", "TBO"};
		public static ulong[] CAR_GROUP_DISALLOW = {0, 243583, 804992, 511726, 522368, 524028, 392896};
		public static ulong[] CAR_GROUP_DONTCARE = {524287, 804992, 14207, 12561, 319, 259, 291};
		public static byte VERSION_DEMO = (byte)0x00;
		public static byte VERSION_S1 = (byte)0x01;
		public static byte VERSION_S2 = (byte)0x02;

		private static string[] pubstatTracks = {"BL", "SO", "FE", "AU", "KY", "WE", "AS"};

		public static Hashtable msFilters;
		public static Hashtable trackCodes;


		public static int QTHREADS = 16;
		public static int THREAD_WAIT = 150;
		public static bool xpsp2_wait = true;

		private static bool keepQuerying;
		private static int totalServers;

		private static byte[] pubstatBuf;
		private static int pubstatLastUpdate;
		private const int PUBSTAT_CACHE_TIME = 1000 * 30; //30 second cache

		public static event ServerQueried queried;

		public LFSQuery() {
			fillStaticStuff();
		}

		private static void fillStaticStuff() {
			trackCodes = new Hashtable(53);
			trackCodes.Add("BL1", "Blackwood");
			trackCodes.Add("BL1R", "Blackwood Rev");
			trackCodes.Add("BL2", "Blackwood Rallyx");
			trackCodes.Add("BL2R", "Blackwood Rallyx Rev");
			trackCodes.Add("BL3", "Blackwood Car Park");
			trackCodes.Add("SO1", "South City Classic");
			trackCodes.Add("SO1R", "South City Classic Rev");
			trackCodes.Add("SO2", "South City Sprint 1");
			trackCodes.Add("SO2R", "South City Sprint 1 Rev");
			trackCodes.Add("SO3", "South City Sprint 2");
			trackCodes.Add("SO3R", "South City Sprint 2 Rev");
			trackCodes.Add("SO4", "South City Long");
			trackCodes.Add("SO4R", "South City Long Rev");
			trackCodes.Add("SO5", "South City Town");
			trackCodes.Add("SO5R", "South City Town Rev");
			trackCodes.Add("SO6", "South City Chicane Route");
			trackCodes.Add("SO6R", "South City Chicane Route Rev");
			trackCodes.Add("FE1", "Fern Bay Club");
			trackCodes.Add("FE1R", "Fern Bay Club Rev");
			trackCodes.Add("FE2", "Fern Bay Green");
			trackCodes.Add("FE2R", "Fern Bay Green Rev");
			trackCodes.Add("FE3", "Fern Bay Gold");
			trackCodes.Add("FE3R", "Fern Bay Gold Rev");
			trackCodes.Add("FE4", "Fern Bay Black");
			trackCodes.Add("FE4R", "Fern Bay Black Rev");
			trackCodes.Add("FE5", "Fern Bay Rally Cross");
			trackCodes.Add("FE5R", "Fern Bay Rally Cross Rev");
			trackCodes.Add("FE6", "Fern Bay Rallyx Green");
			trackCodes.Add("FE6R", "Fern Bay Rallyx Green Rev");
			trackCodes.Add("AU1", "Autocross");
			trackCodes.Add("AU2", "Skid Pad");
			trackCodes.Add("AU3", "Drag Strip");
			trackCodes.Add("AU4", "Drag Strip 8 plyr");
			trackCodes.Add("KY1", "Kyoto Oval");
			trackCodes.Add("KY1R", "Kyoto Oval Rev");
			trackCodes.Add("KY2", "Kyoto National");
			trackCodes.Add("KY2R", "Kyoto National Rev");
			trackCodes.Add("KY3", "Kyoto GP Long");
			trackCodes.Add("KY3R", "Kyoto GP Long Rev");
			trackCodes.Add("WE1", "Westhill International");
			trackCodes.Add("WE1R", "Westhill International Rev");
			trackCodes.Add("AS1", "Aston Cadet");
			trackCodes.Add("AS1R", "Aston Cadet Rev");
			trackCodes.Add("AS2", "Aston Club");
			trackCodes.Add("AS2R", "Aston Club Rev");
			trackCodes.Add("AS3", "Aston National");
			trackCodes.Add("AS3R", "Aston National Rev");
			trackCodes.Add("AS4", "Aston Historic");
			trackCodes.Add("AS4R", "Aston Historic Rev");
			trackCodes.Add("AS5", "Aston Grand Prix");
			trackCodes.Add("AS5R", "Aston Grand Prix Rev");
			trackCodes.Add("AS6", "Aston Grand Touring");
			trackCodes.Add("AS6R", "Aston Grand Touring Rev");
			trackCodes.Add("AS7", "Aston North");
			trackCodes.Add("AS7R", "Aston North Rev");
			msFilters = new Hashtable(4);
			msFilters.Add("Private", (byte)0x04);
			msFilters.Add("Public", (byte)0x08);
			msFilters.Add("Empty", (byte)0x10);
			msFilters.Add("Full", (byte)0x20);
			msFilters.Add("Can Reset", (byte)0x40);
			msFilters.Add("Cruise", (byte)0x80);
		}

		public class ServerQuery {
			private hostInfo host;
			private System.Threading.ManualResetEvent timeoutEvent;
			private System.Threading.ManualResetEvent readTimeoutEvent;

			public void connectCallback(IAsyncResult ar) {
				Socket sock = (Socket)ar.AsyncState;
				if (sock != null) {
					try {
					 	sock.EndConnect(ar);
					} catch (Exception) { return; }
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
					} catch (Exception) { return; }
				} else {
					return;
				}
				readTimeoutEvent.Set();
			}

			public void setHost(hostInfo host) {
				this.host = host;
			}

			byte[] send_query1 = {
				0x0c, 0x02, 0x05, 0x58, 0x1f, 0x1d, 0x01, 0x36,
				0x4e, 0x00, 0x00, 0x00, 0x00
			};
			byte[] send_query2 = {
				0x0c, 0x02, 0x05, 0x58, 0x1f, 0x1d, 0x02, 0x36,
				0x4e, 0x00, 0x00, 0x00, 0x00
			};
			/*byte[] send_query3 = {
				0x0c, 0x02, 0x05, 0x55, 0x00, 0x1d, 0x02, 0x2f,
				0x4e, 0x00, 0x00, 0x00, 0x00
			};*/

			public ServerQuery() { }

			public void queryServer() {
				IPEndPoint endpoint = new IPEndPoint(host.info.host.Address, host.info.host.Port);
				Socket sock = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				timeoutEvent = new System.Threading.ManualResetEvent(false);
				sock.BeginConnect(endpoint, new AsyncCallback(connectCallback), sock);
				if (this.host.version == VERSION_DEMO) {
					send_query1[1] = send_query2[1] = 0x00;
					send_query1[7] = send_query2[7] = 0x16;
					send_query1[8] = send_query2[8] = 0x00;
				} else if (this.host.version == VERSION_S1) {
					send_query1[1] = send_query2[1] = 0x01;
					send_query1[7] = send_query2[7] = 0x26;
					send_query1[8] = send_query2[8] = 0x27;
				} else {
					send_query1[1] = send_query2[1] = 0x02;
					send_query1[7] = send_query2[7] = 0x36;
					send_query1[8] = send_query2[8] = 0x4e;
				}

				if (timeoutEvent.WaitOne(1000, false)) {
					//connected aok!
					NetworkStream str = new NetworkStream(sock);
					//str.ReadTimeout = 500; //.NET 2.0 :(... mono probably supports this now?
					str.Write(send_query1, 0, send_query1.Length);
					ServerInformation serverinfo = query1(ref str);
					if (serverinfo.success) {
						str.Write(send_query2, 0, send_query2.Length);
						query2(ref str, ref serverinfo);
					}
					serverinfo.version = host.version;
					//client.Close();
					try {
						str.Close();
						sock.Shutdown(SocketShutdown.Both);
						sock.Close();
					} catch (Exception) { }
					if (queried != null) {
						queried(this, serverinfo, host.callbackObj);
					}
				} else {
					//connect FAILED BOOHOO
					ServerInformation ret = host.info;
					ret.success = false;
					ret.connectFailed = true;
					ret.totalServers = LFSQuery.totalServers;
					if (sock != null) {
						try {
							sock.Shutdown(SocketShutdown.Both);
							sock.Close();
						} catch (Exception) { }
					}
					queried(this, ret, host.callbackObj);
				}
			}

			private ServerInformation query1(ref NetworkStream str) {
				readTimeoutEvent = new System.Threading.ManualResetEvent(false);
				byte[] recbuf = new byte[37];
				DateTime time = System.DateTime.Now;
				long pingStart = time.Ticks;
				str.BeginRead(recbuf, 0, recbuf.Length, new AsyncCallback(readCallback), str);
				if (readTimeoutEvent.WaitOne(1000, false)) {
					//Console.Write("OKAY!\n");
				} else {
					ServerInformation ret = host.info;
					ret.success = false;
					ret.readFailed = true;
					return ret;
				}
				time = System.DateTime.Now;
				long pingEnd = time.Ticks;
				readTimeoutEvent.Close();
				ServerInformation serverinfo = host.info;
				serverinfo.success = true;
				serverinfo.ping = (int)((pingEnd - pingStart) / 10000);
				serverinfo.rules = (ulong)(recbuf[4] * 256 + recbuf[3]);
				serverinfo.players = (int)recbuf[1];
				serverinfo.slots = (int)recbuf[2];
				serverinfo.host = host.info.host;
				serverinfo.passworded = (serverinfo.rules & 8) != 0;
				serverinfo.totalServers = LFSQuery.totalServers;
				serverinfo.hostname = getConvertedLFSString(recbuf, 5, 32);
                serverinfo.rawHostname = getLFSString(recbuf, 5, 32);
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
				if (recbuf[0] == 0x00) {
					serverinfo.success = false;
					return;
				}
				serverinfo.success = true;
				serverinfo.cars = (ulong)(recbuf[12] * 16777216 + recbuf[11] * 65536 + recbuf[10] * 256 + recbuf[9]);
				serverinfo.track = (string)trackCodes[getLFSString(recbuf, 1, 4)];
				serverinfo.adminslots = (int)recbuf[7];
				serverinfo.slots -= serverinfo.adminslots;
			}
		}

		private class Query {
			static byte[] header = { 0x4c, 0x4c, 0x46, 0x53, 0x00}; //LLFS\0
			static byte[] client_version_info = {0x04, 0x1d, 0x00, 0xd6, 0x02, 0x05, 0x58, 0x1f};
			public static ulong cars_compulsory;
			public static ulong cars_illegal;
			public byte[] un = new byte[20];
			public byte[] username = new byte[24];
			public static byte[] unknown = {0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
			public static byte[] footer = { 0x36, 0x4e, 0x36, 0x4e }; // /N;

			public Query(ulong cars_compulsory, ulong cars_illegal, string user, byte filters, byte version) {
				filters ^= 0x02; //not sure what 0x01 and 0x02 are. 0x02 seems to be set always?
				client_version_info[3] = filters;
				client_version_info[4] = version;
				if (version == VERSION_DEMO) {
					footer[0] = footer[2] = 0x16; //I'm pretty sure that 0,1 are the lower version to ask for
					footer[1] = footer[3] = 0x00; // and 2,3 are the higher version
				} else if (version == VERSION_S1) {
					footer[0] = footer[2] = 0x26; // i.e if you put 26,27,36,4e then you're requesting
					footer[1] = footer[3] = 0x27; // S1 and S2 servers.
				} else {
					footer[0] = footer[2] = 0x36;
					footer[1] = footer[3] = 0x4e;
				}
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

		public void query(ulong cars_compulsory, ulong cars_illegal, string username, object callbackObj, byte filters, byte version) {
			LFSQuery.keepQuerying = true;
			ArrayList allHosts = new ArrayList();
			Query query = new Query(cars_compulsory, cars_illegal, username, filters, version);
			TcpClient client = new TcpClient("master.liveforspeed.net", 29339);
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
					h.info = new ServerInformation();
					h.info.host = ips[j];
					h.passworded = ((recbuf[5+j] & 8) == 0);
					h.callbackObj = callbackObj;
					h.version = version;
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

		public void query(ulong cars_compulsory, ulong cars_illegal, string username, ServerInformation[] hosts, object callbackObj) {
			LFSQuery.keepQuerying = true;
			QueryThreadManager m = new QueryThreadManager();
			totalServers = hosts.Length;
			foreach (ServerInformation host in hosts) {
				hostInfo h = new hostInfo();
				h.info = host;
				h.passworded = false;
				h.callbackObj = callbackObj;
				h.version = host.version;
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
			TcpClient client = new TcpClient("master.liveforspeed.net", 29339);
			//TcpClient client = new TcpClient("127.0.0.1", 29339);
			Stream str = client.GetStream();
			str.Write(query, 0, query.Length);
			str.Read(recbuf, 0, recbuf.Length);
			client.Close();
			if (recbuf[0] == 0x20) {
				return null;
			} else {
				return getLFSString(recbuf, 1, 32);
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
			pubstatBuf = null;
		}

		public static int getPubStatInfo(string playername, out ServerInformation serverInfo) {
			serverInfo = new ServerInformation();
			return findHostOrPlayer(ref serverInfo, playername);
		}

		public static int getPubStatInfo(ref ServerInformation serverInfo) {
			return findHostOrPlayer(ref serverInfo, null);
		}

		static Hashtable playerServers = new Hashtable();
		private static int findHostOrPlayer(ref ServerInformation serverInfo, string racer) {
			int ret = 0;
			fillStaticStuff();
			if (racer != null && playerServers.Count > 0 && System.Environment.TickCount <= (pubstatLastUpdate + PUBSTAT_CACHE_TIME)) {
				ServerInformation s = (ServerInformation)(playerServers[racer]);
				if (s != null) {
					s.version = serverInfo.version;
					serverInfo = new ServerInformation();
					serverInfo.version = s.version;
					serverInfo.hostname = s.hostname;
					serverInfo.rawHostname = s.rawHostname;
					serverInfo.players = s.players;
					serverInfo.racerNames = s.racerNames;
					serverInfo.password = s.password;
					serverInfo.cars = s.cars;
					serverInfo.rules = s.rules;
					serverInfo.track = s.track;
					//Console.Write("HUZZA!!!!!!\n");
					return 1;
				}
				return 0;
			}
			try {
				if (!getPubStatBuf()) return -1;
				//Console.Write("using pubstat query\n");
				Stream s = new GZipInputStream(new MemoryStream(pubstatBuf));
				byte[] buf = getStreamBytes(s);
				s.Close();
				int i = 0;
				string[] racers = null;
				bool found = false;
				while (i < buf.Length) {
                    string rawHostname = getLFSString(buf, i, 32);
					string hostname = getConvertedLFSString(buf, i, 32);
					int numRacers = (int)buf[i + 52];
					if ((racer == null && removeColourCodes(hostname) == removeColourCodes(serverInfo.hostname)) || racer != null) {
						racers = new string[numRacers];
						ServerInformation si = new ServerInformation();
						si.version = serverInfo.version;
						si.password = serverInfo.password;
						si.rawHostname = rawHostname;
						si.hostname = hostname;
						si.players = numRacers;
						si.passworded = ((ulong)(buf[i + 47] * 16777216 + buf[i + 46] * 65536 + buf[i + 45] * 256 + buf[i + 44]) & 8) != 0;
						si.cars = (ulong)(buf[i + 43] * 16777216 + buf[i + 42] * 65536 + buf[i + 41] * 256 + buf[i + 40]);
						si.rules = (ulong)(buf[i + 47] * 16777216 + buf[i + 46] * 65536 + buf[i + 45] * 256 + buf[i + 44]);
						string track = "";
						track += pubstatTracks[(int)(buf[i + 36])]; //BL
						track += (((int)(buf[i + 37])) + 1); //1
						if (((int)(buf[i + 38])) == 1) {
							track += "R";
						}
						si.track = (string)trackCodes[track];
						if (serverInfo.host == null) {
							si.ping = -1;
						}
						for (int j = 0; j < numRacers; ++j) {
							racers[j] = getLFSString(buf, i + 53 + (24 * j), 24);
							if (!found && (racer != null && racers[j].ToLower() == racer.ToLower())) {
								found = true;
								ret = 1;
							}
						}
						si.racerNames = racers;
						foreach (string r in racers) {
							playerServers[r] = si;
						}
						if (found || (racer == null)) {
							ret = 1;
							serverInfo = si;
							found = false; //stop it doing this again
						}
					}
					i += (53 + (24 * numRacers));
				}
			} catch (Exception) {
				return -1;
			}
			return ret;
		}
		
		private static bool getPubStatBuf() {
			return getPubStatBuf("MI3VNTM8hN1mv0t6i9O2j48dvC8F5uzM"); //wabz's idkey
		}

		private static bool getPubStatBuf(string idkey) {
			try {
				if (pubstatBuf == null || System.Environment.TickCount > (pubstatLastUpdate + PUBSTAT_CACHE_TIME)) {
					HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.lfsworld.net/pubstat/get_stat2.php?action=hosts&c=1&idk=" + idkey);
					request.Timeout = 4000;
					HttpWebResponse response = (HttpWebResponse)request.GetResponse();
					Stream s = response.GetResponseStream();
					pubstatBuf = getStreamBytes(s);
					s.Close();
					pubstatLastUpdate = System.Environment.TickCount;
				}
				return true;
			} catch (Exception) {
				return false;
			}
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
		
		public static string convertLFSString(string str) {
			byte[] b = Encoding.GetEncoding(1252).GetBytes(str);
			return getConvertedLFSString(b, 0, b.Length);
		}

		private static string getLFSString(byte[] buf, int startpos, int maxlen) {
			int i;
			int endpos = 0;
			for (i = 0; i < maxlen; ++i) {
				if (buf[i + startpos] == 0x00) {
					endpos = i;
					break;
				}
				if (i + 1 == maxlen) {
					endpos = i+1;
				}
			}
			return Encoding.GetEncoding(1252).GetString(buf, startpos, endpos);
		}

		private static string getConvertedLFSString(byte[] buf, int startpos, int maxlen) {
			int i;
			string ret = "";
			Encoding enc = Encoding.GetEncoding(1252); //latin-1, default
			int ignore = 0;
			for (i = 0; i < maxlen; ++i) {
				if (buf[i + startpos] == 0x00) {
					break;
				}
				if (buf.Length > i + startpos + 1) {
					byte[] c = { buf[i + startpos], buf[i + startpos + 1]};
					string tmp = Encoding.GetEncoding(1252).GetString(c);
					if (tmp == "^J") {
						enc = Encoding.GetEncoding(932); //japanese
						ignore += 2;
					} else if (tmp == "^G") {
						enc = Encoding.GetEncoding(1253); //greek
						ignore += 2;
					} else if (tmp == "^B") {
						enc = Encoding.GetEncoding(1257); //baltic
						ignore += 2;
					} else if (tmp == "^C") {
						enc = Encoding.GetEncoding(1251); //cryllic
						ignore += 2;
					} else if (tmp == "^E") {
						enc = Encoding.GetEncoding(1250); //eastern europe
						ignore += 2;
					} else if (tmp == "^T") {
						enc = Encoding.GetEncoding(1254); //turkish
						ignore += 2;
					} else if (tmp == "^L") {
						enc = Encoding.GetEncoding(1252); //latin-1
						ignore += 2;
					}
				}
				if (ignore == 0) {
					ret += enc.GetString(buf, i + startpos, 1);
				} else {
					ignore--;
				}
			}
			//FIXME: do this better
			ret = Regex.Replace(ret, @"(\A|[^^])\^v", "$1|");
			ret = Regex.Replace(ret, @"(\A|[^^])\^v", "$1|");
			return ret;
		}

		public static string removeColourCodes(string text) {
			text = Regex.Replace(text, "\\^\\d", "");
			text = Regex.Replace(text, "\\^\\^", "^");
			return text;
		}

		/*This works, but can't resolve ambiguities. e.g ﾉﾏedline ﾉﾏacing may have originally had two ^Js.
		public static string convertToLFSString(string str) {
			string ret = "";
			Encoding current = Encoding.GetEncoding(1252);
			Encoding lat = Encoding.GetEncoding(1252);
			Encoding jap = Encoding.GetEncoding(932);
			Encoding greek = Encoding.GetEncoding(1253);
			Encoding bal = Encoding.GetEncoding(1257);
			Encoding cryl = Encoding.GetEncoding(1251);
			Encoding euro = Encoding.GetEncoding(1250);
			Encoding turk = Encoding.GetEncoding(1254);
			char[] c = str.ToCharArray();
			for (int i = 0; i < c.Length; ++i) {
				if (current != jap && jap.GetBytes(c, i, 1)[0] != lat.GetBytes(c, i, 1)[0]) {
					current = jap;
					ret += "^J";
				} else if (current != greek && greek.GetBytes(c, i, 1)[0] != lat.GetBytes(c, i, 1)[0]) {
					current = greek;
					ret += "^G";
				} else if (current != bal && bal.GetBytes(c, i, 1)[0] != lat.GetBytes(c, i, 1)[0]) {
					current = bal;
					ret += "^B";
				} else if (current != cryl && cryl.GetBytes(c, i, 1)[0] != lat.GetBytes(c, i, 1)[0]) {
					current = cryl;
					ret += "^C";
				} else if (current != euro && euro.GetBytes(c, i, 1)[0] != lat.GetBytes(c, i, 1)[0]) {
					current = euro;
					ret += "^E";
				} else if (current != turk && turk.GetBytes(c, i, 1)[0] != lat.GetBytes(c, i, 1)[0]) {
					current = turk;
					ret += "^T";
				}
				ret += lat.GetString(current.GetBytes(c, i, 1));
			}
			return ret;
		}*/
	}

}
