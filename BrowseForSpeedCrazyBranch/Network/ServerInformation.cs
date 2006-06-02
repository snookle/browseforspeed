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

namespace LFS.BrowseForSpeed.Network
{
    public class ServerInformation
    {
        public ulong Cars;
        public bool ConnectFailed;
        public IPEndPoint Host;
        public string Hostname;
        public bool Passworded;
        public string Password;
        public int Ping;
        public int Players;
        public string[] RacerNames;
        public bool ReadFailed;
        public ulong Rules;
        public int Slots;
        public bool Success;
        public int TotalServers;
        public string Track;
    }
}
