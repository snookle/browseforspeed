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
using System.Text.RegularExpressions;

namespace LFS.BrowseForSpeed
{
    public sealed class Utility
    {
        #region Public Methods
        public static string DecodeString(byte[] buffer, int startPosition, int maxLength) 
        {
			int endPosition = 0;
            for (int count = 0; count < maxLength; count++)
            {
                if (buffer[count + startPosition] == 0x00)
                {
                    endPosition = count;
					break;
				}

                if (count + 1 == maxLength)
                    endPosition = count + 1;
			}
            return Encoding.ASCII.GetString(buffer, startPosition, endPosition);
        }

        /// <summary>
        /// Tests for a valid integer.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <returns></returns>
        public static bool IsInteger(string value)
        {
            //return !_notIntPattern.IsMatch(value) && _intPattern.IsMatch(value);
            return _integerPattern.IsMatch(value);
        }

        /// <summary>
        /// Tests for a valid number.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <returns></returns>
        public static bool IsNumber(string value)
        {
            return _numberPattern.IsMatch(value);
        }
        
        public static string RemoveColorCodes(string value)
        {
            value = Regex.Replace(value, "\\^\\d", "", RegexOptions.Compiled);
            value = Regex.Replace(value, "\\^\\^", "^", RegexOptions.Compiled);

            return value;
        }
        #endregion

        #region Fields
        private static Regex _integerPattern = new Regex(@"^-?\d\d*$", RegexOptions.Compiled);
        private static Regex _numberPattern = new Regex(@"^-?\d\d*.?\d*$", RegexOptions.Compiled);
        #endregion
    }
}
