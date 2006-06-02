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
using System.Net;
using System.Net.Sockets;

namespace LFS.BrowseForSpeed.Network
{
    public class MasterServerQueryReader
    {
        public MasterServerQueryReader(HostInfo host)
        {
            _host = host;
        }

        #region Public Methods
        public event EventHandler<ServerInformationEventArgs> HostQueried;

        public void Perform()
        {
            ServerInformation serverInfo = null;
            Socket socket = null;

            try
            {
                IPEndPoint endpoint = new IPEndPoint(_host.Host.Address, _host.Host.Port);
                socket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                _timeoutEvent = new System.Threading.ManualResetEvent(false);
                socket.BeginConnect(endpoint, new AsyncCallback(ConnectCallback), socket);
                if (_timeoutEvent.WaitOne(1000, false))
                {
                    NetworkStream stream = null;

                    try
                    {
                        //connected aok!
                        stream = new NetworkStream(socket);
                        stream.Write(_requestServer, 0, _requestServer.Length);
                        serverInfo = HandleServerResponse(ref stream);
                        if (serverInfo.Success)
                        {
                            stream.Write(_requestCarsTrack, 0, _requestCarsTrack.Length);
                            HandleCarsTrackResponse(ref stream, ref serverInfo);
                        }
                    }
                    finally
                    {
                        try
                        {
                            if (stream != null)
                            {
                                stream.Close();
                                stream = null;
                            }
                        }
                        catch { }
                    }
                }
                else
                {
                    //connect FAILED BOOHOO
                    serverInfo = new ServerInformation();
                    serverInfo.Success = false;
                    serverInfo.ConnectFailed = true;
                    if (socket != null)
                    {
                        try
                        {
                            socket.Shutdown(SocketShutdown.Both);
                            socket.Close();
                        }
                        catch (Exception e) { }
                    }
                }
            }
            finally
            {
                try
                {
                    if (socket != null)
                    {
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                        socket = null;
                    }
                }
                catch { }
            }

            if ((HostQueried != null) && (serverInfo != null))
                HostQueried(this, new ServerInformationEventArgs(serverInfo));
        }
        #endregion

        #region Private Methods
        private void ConnectCallback(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            if (socket != null)
            {
                try
                {
                    socket.EndConnect(ar);
                }
                catch { return; }

                _timeoutEvent.Set();
            }

            return;
        }

        private void HandleCarsTrackResponse(ref NetworkStream stream, ref ServerInformation serverInfo)
        {
            if (stream == null)
                throw new ArgumentNullException("stream", "Invalid NetworkStream object.");
            if (serverInfo == null)
                throw new ArgumentNullException("serverInfo", "Invalid ServerInformation object.");

            byte[] receiveBuffer = new byte[13];

            try
            {
                _readTimeoutEvent = new System.Threading.ManualResetEvent(false);

                stream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, new AsyncCallback(ReadCallback), stream);
                if (!_readTimeoutEvent.WaitOne(1000, false))
                {
                    serverInfo.Success = false;
                    serverInfo.ReadFailed = true;
                    return;
                }
            }
            finally
            {
                _readTimeoutEvent.Close();
            }

            serverInfo.Success = true;
            serverInfo.Cars = (ulong)(receiveBuffer[12] * 16777216 + receiveBuffer[11] * 65536 + receiveBuffer[10] * 256 + receiveBuffer[9]);

           string key = Utility.DecodeString(receiveBuffer, 1, 4);
            if (MasterServerQuery.Instance.TrackCodes.ContainsKey(key))
                serverInfo.Track = (string)MasterServerQuery.Instance.TrackCodes[key];
        }

        private ServerInformation HandleServerResponse(ref NetworkStream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream", "Invalid NetworkStream object.");

            ServerInformation serverInfo = new ServerInformation();
            byte[] receiveBuffer = new byte[37];
            long pingStart = System.DateTime.Now.Ticks;

            try
            {
                _readTimeoutEvent = new System.Threading.ManualResetEvent(false);

                stream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, new AsyncCallback(ReadCallback), stream);
                if (!_readTimeoutEvent.WaitOne(1000, false))
                {
                    serverInfo.Success = false;
                    serverInfo.ReadFailed = true;
                }
                else
                {
                    serverInfo.Success = true;
                    serverInfo.Ping = (int)((System.DateTime.Now.Ticks - pingStart) / 10000);
                    serverInfo.Rules = (ulong)(receiveBuffer[4] * 256 + receiveBuffer[3]);
                    serverInfo.Players = (int)receiveBuffer[1];
                    serverInfo.Slots = (int)receiveBuffer[2];
                    serverInfo.Host = _host.Host;
                    serverInfo.Passworded = _host.Passworded;
                    serverInfo.Hostname = Utility.DecodeString(receiveBuffer, 5, 32);
                }
            }
            finally
            {
                _readTimeoutEvent.Close();
            }

            return serverInfo;
        }

        private void ReadCallback(IAsyncResult ar)
        {
            NetworkStream stream = (NetworkStream)ar.AsyncState;
            if (stream != null)
            {
                try
                {
                    stream.EndRead(ar);
                }
                catch { return; }

                _readTimeoutEvent.Set();
            }

            return;
        }
        #endregion

        #region Fields
        private HostInfo _host;
        private System.Threading.ManualResetEvent _timeoutEvent;
        private System.Threading.ManualResetEvent _readTimeoutEvent;
        #endregion

        #region Constants
        byte[] _requestServer = { 0x0c, 0x02, 0x05, 0x55, 0x00, 0x1d, 0x01, 0x2f, 0x4e, 0x00, 0x00, 0x00, 0x00 };
        byte[] _requestCarsTrack = { 0x0c, 0x02, 0x05, 0x55, 0x00, 0x1d, 0x02, 0x2f, 0x4e, 0x00, 0x00, 0x00, 0x00b };
        #endregion
    }

    
}
