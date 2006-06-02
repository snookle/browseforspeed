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

using ICSharpCode.SharpZipLib.GZip;

namespace LFS.BrowseForSpeed.Network
{
    public sealed class LFSWorldQuery
    {
        #region Public Methods
        public static void ClearPublicStatsCache()
        {
            _cacheBuffer = null;
        }

        public static int FetchPublicStatsInfo(ref ServerInformation serverInfo)
        {
            return FindHostOrPlayer(ref serverInfo, null);
        }

        public static int FetchPublicStatsInfo(string playerName, out ServerInformation serverInfo)
        {
            serverInfo = new ServerInformation();
            return FindHostOrPlayer(ref serverInfo, playerName);
        }
        #endregion

        #region Private Methods
        private static int FindHostOrPlayer(ref ServerInformation serverInfo, string playerName)
        {
            try
            {
                if (!RequestPublicStats()) 
                    return -2;

                byte[] localBuffer;

                Stream compressedData = null;
                try
                {
                    compressedData = new GZipInputStream(new MemoryStream(_cacheBuffer));
                    localBuffer = GetStreamAsBytes(compressedData);
                }
                finally
                {
                    if (compressedData != null)
                    {
                        try
                        {
                            compressedData.Close();
                            compressedData = null;
                        }
                        catch { }
                    }
                }

                int count = 0;
                int numRacers = 0;
                string[] racers = null;
                string hostName = string.Empty;
                bool found = false;

                while (count < localBuffer.Length)
                {
                    hostName = Utility.RemoveColorCodes(Utility.DecodeString(localBuffer, count, 32));
                    numRacers = (int)localBuffer[count + 52];

                    if (!string.IsNullOrEmpty(playerName))
                    {
                        racers = new string[numRacers];
                        for (int racerCount = 0; racerCount < numRacers; racerCount++)
                        {
                            racers[racerCount] = Utility.DecodeString(localBuffer, count + 53 + (24 * racerCount), 24);
                            if (racers[racerCount] == playerName)
                            {
                                found = true;
                                break;
                            }
                        }  
                    }
                    else if (hostName == serverInfo.Hostname)
                        found = true;

                    if (found)
                    { 
                        //either we've found a player, or the hostname matched
                        serverInfo = new ServerInformation();
                        serverInfo.Hostname = hostName;
                        serverInfo.Players = numRacers;
                        serverInfo.RacerNames = racers;
                        serverInfo.Passworded = ((ulong)(localBuffer[count + 47] * 16777216 + localBuffer[count + 46] * 65536 + localBuffer[count + 45] * 256 + localBuffer[count + 44]) & 8) != 0;
                        
                        return 1;
                    }

                    count += (53 + (24 * numRacers));
                }
            }
            catch (Exception e)
            {
                return -1;
            }

            return 0;
        }

        private static bool RequestPublicStats()
        {
            try
            {
                if ((_cacheBuffer == null) || (System.Environment.TickCount > (_lastUpdate + CacheTimeLimit)))
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.lfsworld.net/pubstat/get_stat2.php?action=hosts&c=1");
                    request.Timeout = 4000;
                    HttpWebResponse response = null;

                    try
                    {
                        response = (HttpWebResponse)request.GetResponse();

                        Stream data = null;
                        try
                        {
                            data = response.GetResponseStream();
                            _cacheBuffer = GetStreamAsBytes(data);
                        }
                        finally
                        {
                            if (data != null)
                            {
                                try
                                {
                                    data.Close();
                                    data = null;
                                }
                                catch { }
                            }
                        }
                    }
                    finally
                    {
                        if (response != null)
                        {
                            try
                            {
                                response.Close();
                                response = null;
                            }
                            catch { }
                        }
                    }

                    _lastUpdate = System.Environment.TickCount;
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private static byte[] GetStreamAsBytes(Stream stream)
        {
            byte[] buffer = new byte[32768];
            using (MemoryStream output = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                        return output.ToArray();

                    output.Write(buffer, 0, read);
                }
            }
        }
        #endregion

        #region Fields
        private static byte[] _cacheBuffer;
        private static int _lastUpdate;
        #endregion

        #region Constants
        private const int CacheTimeLimit = 30000; //30 second cache
        #endregion
    }
}
