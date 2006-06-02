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

namespace LFS.BrowseForSpeed.Network
{
    public class ServerInformationEventArgs : EventArgs
    {
        public ServerInformationEventArgs(ServerInformation server)
        {
            _server = server;
        }

        #region Public Properties
        public QueryType QueryType
        {
            get { return _queryType; }
        }

        public ServerInformation Server
        {
            get { return _server; }
        }
        #endregion

        #region Fields
        private QueryType _queryType = QueryType.Main;
        private ServerInformation _server;
        #endregion
    }
}
