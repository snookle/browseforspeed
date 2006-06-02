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
using System.Text;

namespace LFS.BrowseForSpeed.Network
{
    public class Query
    {
        public Query(string user, ulong requestedCars, ulong ignoredCars, bool hideEmpty, bool hideFull)
        {
            Query.SetFilters(requestedCars, ignoredCars, hideEmpty, hideFull);

            Encoding.ASCII.GetBytes(user.ToCharArray(), 0, user.Length, _username, 0);
        }

        #region Public Methods
        public static void SetFilters(ulong requestedCars, ulong ignoredCars)
        {
            _requestedCars = requestedCars;
            _ignoredCars = ignoredCars;
        }

        public static void SetFilters(ulong requestedCars, ulong ignoredCars, bool hideEmpty, bool hideFull)
        {
            Query.SetFilters(requestedCars, ignoredCars);

            if (hideEmpty)
                _dataClientVersionInfo[3] = 0x12; //16 (!empty) + 2 (default) 
            else
                _dataClientVersionInfo[3] = 0x02;
        }
        #endregion

        #region Public Properties
        public byte[] Data
        {
            get
            {
                byte[] data = new byte[77];

                int dataPosition = 0;
                Array.Copy(_dataHeader, data, _dataHeader.Length);
                dataPosition += _dataHeader.Length;

                Array.Copy(_dataClientVersionInfo, 0, data, dataPosition, _dataClientVersionInfo.Length);
                dataPosition += _dataClientVersionInfo.Length;

                data[13] = (byte)_requestedCars;
                data[14] = (byte)(_requestedCars >> 8);
                data[15] = (byte)(_requestedCars >> 16);
                data[16] = (byte)(_requestedCars >> 24);
                data[17] = (byte)_ignoredCars;
                data[18] = (byte)(_ignoredCars >> 8);
                data[19] = (byte)(_ignoredCars >> 16);
                data[20] = (byte)(_ignoredCars >> 24);
                dataPosition += 8;

                _un[0] = 1;
                Array.Copy(_un, 0, data, dataPosition, _un.Length);
                dataPosition += _un.Length;

                Array.Copy(_username, 0, data, dataPosition, _username.Length);
                dataPosition += _username.Length;

                Array.Copy(_dataUnknown, 0, data, dataPosition, _dataUnknown.Length);
                dataPosition += _dataUnknown.Length;

                Array.Copy(_dataFooter, 0, data, dataPosition, _dataFooter.Length);

                return data;
            }
        }
        #endregion

        #region Fields
        private static ulong _requestedCars;
        private static ulong _ignoredCars;
        private byte[] _un = new byte[20];
        private byte[] _username = new byte[24];
        #endregion

        #region Constants
        private static byte[] _dataClientVersionInfo = { 0x04, 0x1d, 0x00, 0x02, 0x02, 0x05, 0x55, 0x00 };
        private static byte[] _dataHeader = { 0x4c, 0x4c, 0x46, 0x53, 0x00 }; //LLFS\0
        private static byte[] _dataUnknown = { 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private static byte[] _dataFooter = { 0x2f, 0x4e }; // /N;
        #endregion
    }
}
