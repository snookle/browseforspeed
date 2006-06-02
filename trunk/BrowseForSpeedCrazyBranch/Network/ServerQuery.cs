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
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LFS.BrowseForSpeed.Network
{
	public sealed class MasterServerQuery 
    {
        private MasterServerQuery(string address, int port) 
        {
            _serverAddress = address;
            _serverPort = port;

            // TODO: Put these in a friggin' config file, geesh. Losers.
            _trackCodes = new Dictionary<string, string>(52);
            _trackCodes.Add("BL1", "Blackwood");
            _trackCodes.Add("BL1R", "Blackwood Rev");
            _trackCodes.Add("BL2", "Blackwood Rallyx");
            _trackCodes.Add("BL2R", "Blackwood Rallyx Rev");
            _trackCodes.Add("BL3", "Blackwood Car Park");
            _trackCodes.Add("SO1", "South City Classic");
            _trackCodes.Add("SO1R", "South City Classic Rev");
            _trackCodes.Add("SO2", "South City Sprint 1");
            _trackCodes.Add("SO2R", "South City Sprint 1 Rev");
            _trackCodes.Add("SO3", "South City Sprint 2");
            _trackCodes.Add("SO3R", "South City Sprint 2 Rev");
            _trackCodes.Add("SO4", "South City Long");
            _trackCodes.Add("SO4R", "South City Long Rev");
            _trackCodes.Add("SO5", "South City Town");
            _trackCodes.Add("SO5R", "South City Town Rev");
            _trackCodes.Add("FE1", "Fern Bay Club");
            _trackCodes.Add("FE1R", "Fern Bay Club Rev");
            _trackCodes.Add("FE2", "Fern Bay Green");
            _trackCodes.Add("FE2R", "Fern Bay Green Rev");
            _trackCodes.Add("FE3", "Fern Bay Gold");
            _trackCodes.Add("FE3R", "Fern Bay Gold Rev");
            _trackCodes.Add("FE4", "Fern Bay Black");
            _trackCodes.Add("FE4R", "Fern Bay Black Rev");
            _trackCodes.Add("FE5", "Fern Bay Rally Cross");
            _trackCodes.Add("FE5R", "Fern Bay Rally Cross Rev");
            _trackCodes.Add("FE6", "Fern Bay Rallyx Green");
            _trackCodes.Add("FE6R", "Fern Bay Rallyx Green Rev");
            _trackCodes.Add("AU1", "Autocross");
            _trackCodes.Add("AU2", "Skid Pan");
            _trackCodes.Add("AU3", "Drag Strip");
            _trackCodes.Add("AU4", "Drag Strip 8 plyr");
            _trackCodes.Add("KY1", "Kyoto Oval Rev");
            _trackCodes.Add("KY1R", "Kyoto Oval Rev");
            _trackCodes.Add("KY2", "Kyoto National");
            _trackCodes.Add("KY2R", "Kyoto National Rev");
            _trackCodes.Add("KY3", "Kyoto GP Long");
            _trackCodes.Add("KY3R", "Kyoto GP Long Rev");
            _trackCodes.Add("WE1", "Westhill International");
            _trackCodes.Add("WE1R", "Westhill International Rev");
            _trackCodes.Add("WE2", "Westhill International Rev");
            _trackCodes.Add("AS1", "Aston Cadet");
            _trackCodes.Add("AS1R", "Aston Cadet Rev");
            _trackCodes.Add("AS2", "Aston Club");
            _trackCodes.Add("AS2R", "Aston Club Rev");
            _trackCodes.Add("AS3", "Aston National");
            _trackCodes.Add("AS3R", "Aston National Rev");
            _trackCodes.Add("AS4", "Aston Historic");
            _trackCodes.Add("AS4R", "Aston Historic Rev");
            _trackCodes.Add("AS5", "Aston Grand Prix");
            _trackCodes.Add("AS5R", "Aston Grand Prix Rev");
            _trackCodes.Add("AS6", "Aston Grand Touring");
            _trackCodes.Add("AS6R", "Aston Grand Touring Rev");
            _trackCodes.Add("AS7", "Aston North");
            _trackCodes.Add("AS7R", "Aston North Rev");
        }

        #region Public Methods
        public static void ChangeFilters(ulong requestedCars, ulong ignoredCars)
        {
            Query.SetFilters(requestedCars, ignoredCars);
        }

        // TODO: VERY SIMILAR TO Query class...fix this
        public string FindUser(string username, string userToFind)
        {
            byte[] query = {
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
            byte[] receiveBuffer = new byte[41];
            Encoding ascii = Encoding.ASCII;
            ascii.GetBytes(username.ToCharArray(), 0, username.Length, user, 0);
            ascii.GetBytes(userToFind.ToCharArray(), 0, userToFind.Length, finduser, 0);
            Array.Copy(user, 0, query, 41, user.Length); //41 = username offset
            Array.Copy(finduser, 0, query, 9, finduser.Length); //9 = user to find offset
            TcpClient client = new TcpClient(_serverAddress, _serverPort);
            Stream str = client.GetStream();
            str.Write(query, 0, query.Length);
            str.Read(receiveBuffer, 0, receiveBuffer.Length);
            client.Close();
            if (receiveBuffer[0] == 0x20)
            {
                return null;
            }
            else
            {
                Decoder d = Encoding.ASCII.GetDecoder();
                char[] hostchars = new char[32];
                d.GetChars(receiveBuffer, 1, 32, hostchars, 0);
                return new string(hostchars);
            }
        }

        public event EventHandler<ServerInformationEventArgs> HostQueried;

        public static void Initialize(string address, int port) 
        {
            _instance = new MasterServerQuery(address, port);
        }

        public void Perform(ulong requestedCars, ulong ignoredCars, IPEndPoint[] hosts, QueryType queryType)
        {
            QueryThreadManager manager = null;

            try
            {
                manager = new QueryThreadManager();
                manager.HostQueried += new EventHandler<ServerInformationEventArgs>(QueryThreadManagerHostQueried);

                ContinueProcessing = true;
                _totalServers = hosts.Length;

                foreach (IPEndPoint host in hosts)
                {
                    HostInfo h;
                    h.Host = host;
                    h.Passworded = false;
                    h.QueryType = queryType;

                    manager.Add(h);
                }
            }
            finally
            {
                try
                {
                    if (manager != null)
                    {
                        manager.Close();
                        manager = null;
                    }
                }
                catch { }
            }
        }

        public void Perform(ulong requestedCars, ulong ignoredCars, bool hideEmpty, bool hideFull, QueryType queryType) 
        {
            QueryThreadManager manager = null;
            TcpClient client = null;
            Stream clientStream = null;

            try
            {
                manager = new QueryThreadManager();
                manager.HostQueried += new EventHandler<ServerInformationEventArgs>(QueryThreadManagerHostQueried);

                ContinueProcessing = true;
                List<HostInfo> allHosts = new List<HostInfo>();

                Query query = new Query("browseforspeed", requestedCars, ignoredCars, hideEmpty, hideFull);

                // TODO: Need to get this from a configuration file, not hard coded. Fucking idiots.
                client = new TcpClient(_serverAddress, _serverPort);
                clientStream = client.GetStream();
                byte[] queryBuffer = query.Data;
                
                ulong hosts = 1;
                for (ulong count = 0; count < hosts; count++)
                {
                    // Send the query data...
                    clientStream.Write(queryBuffer, 0, queryBuffer.Length);

                    // Receive the response...
                    byte[] receiveBuffer = new byte[117];
                    clientStream.Read(receiveBuffer, 0, receiveBuffer.Length);
                    if (count == 0)
                        _totalServers = (int)(receiveBuffer[2] * 256 + receiveBuffer[1]);
                    
                    IPEndPoint[] addresses = new IPEndPoint[16]; //up to 16 addresses per packet
                    for (int addressCount = 0; addressCount < 16; addressCount++)
                    {
                        byte[] address = new byte[4];
                        
                        //21 = offset of first IP
                        Array.Copy(receiveBuffer, 21 + addressCount * 4, address, 0, 4);

                        // 85 = offset of first port
                        int port = (int)(receiveBuffer[86 + addressCount * 2] * 256 + receiveBuffer[85 + addressCount * 2]);
                        if (port == 0) 
                            break; //all done

                        addresses[addressCount] = new IPEndPoint(IPAddress.Parse(string.Concat(address[0].ToString(),
                                                                                               ".",
                                                                                               address[1].ToString(),
                                                                                               ".",
                                                                                               address[2].ToString(),
                                                                                               ".",
                                                                                               address[3].ToString())), port);
                        HostInfo foundHost;
                        foundHost.Host = addresses[addressCount];
                        foundHost.Passworded = (receiveBuffer[5 + addressCount] == 0x09);
                        foundHost.QueryType = queryType;

                        allHosts.Add(foundHost);
                        
                        manager.Add(foundHost);
                    }
                    hosts = (ulong)(receiveBuffer[2] * 256 + receiveBuffer[1]);
                }
            }
            finally
            {
                try
                {
                    if (client != null)
                    {
                        clientStream = null;
                        client.Close();
                        client = null;
                    }
                }
                catch { }

                try
                {
                    if (manager != null)
                    {
                        manager.Close();
                        manager = null;
                    }
                }
                catch { }
            }
		}

        public void Stop()
        {
            ContinueProcessing = false;
        }
        #endregion

        #region Public Properties
        public bool ContinueProcessing
        {
            get { return _continueProcessing; }
            set { _continueProcessing = value; }
        }

        public static MasterServerQuery Instance
        {
            get 
            {
                if (_instance == null)
                    throw new InvalidOperationException("MasterServerQuery instance not initialized.");

                return _instance; 
            }
        }

        public int QueryDelayMilliseconds
        {
            get { return _queryDelayMilliseconds; }
            set { _queryDelayMilliseconds = value; }
        }

        public Dictionary<string, string> TrackCodes
        {
            get { return _trackCodes; }
        }

        public bool UseQueryDelay
        {
            get { return _useQueryDelay; }
            set { _useQueryDelay = value; }
        }
        #endregion

        #region Private Event Handlers
        private void QueryThreadManagerHostQueried(object sender, ServerInformationEventArgs args)
        {
            if (HostQueried != null)
            {
                args.Server.TotalServers = _totalServers;
                HostQueried(this, args);
            }
        }
        #endregion

        #region Static Methods that really should be fixed
        public static Array getCarNames(ulong c) 
        {
            List<string> carNames = new List<string>();
			for (int i = 0; i < CAR_GROUP_BITS.Length; ++i) {
				if (((c & CAR_GROUP_BITS[i]) != 0) && c >= CAR_GROUP_BITS[i]) {
					carNames.Add(CAR_GROUP_NAMES[i]);					
					c -= CAR_GROUP_BITS[i];
				}
			}			
			for (int i = 0; i < MasterServerQuery.CAR_BITS.Length; ++i) {
				if ((c & CAR_BITS[i]) != 0) {
					carNames.Add(CAR_NAMES[i]);					
					c -= CAR_BITS[i];
				}
			}
			return carNames.ToArray();
        }
        #endregion

        #region Fields
        private static MasterServerQuery _instance;

        private bool _continueProcessing;
        public int _queryDelayMilliseconds = 150;
        private string _serverAddress;
        private int _serverPort;
        private Dictionary<string, string> _trackCodes;
        private int _totalServers; 
        private bool _useQueryDelay = true;
        #endregion

        #region Public Constants
        public static ulong[] CAR_BITS = { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384, 32768, 65536, 131072, 262144 };
        public static string[] CAR_NAMES = { "XFG", "XRG", "XRT", "RB4", "FXO", "LX4", "LX6", "MRT", "UF1", "RAC", "FZ5", "FOX", "XFR", "UFR", "FO8", "FXR", "XRR", "FZR", "BF1" };
        public static ulong[] CAR_GROUP_BITS = { 524287, 280704, 229376, 12561, 1600, 259, 28 };
        public static string[] CAR_GROUP_NAMES = { "ALL", "SS", "GTR", "FWD", "LRF", "STD", "TBO" };
        public static ulong[] CAR_GROUP_DISALLOW = { 0, 243583, 280704, 511726, 522368, 524028, 392896 };
        public static ulong[] CAR_GROUP_DONTCARE = { 524287, 280704, 14207, 12561, 319, 259, 291 };

        public const int MaximumQueryThreads = 16;
        #endregion
	}

    public enum QueryType
    {
        Main,
        Favorite,
        Person
    }
}
