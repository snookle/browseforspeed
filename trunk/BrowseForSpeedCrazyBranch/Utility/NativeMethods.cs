// Copyright (C) 2006 Chris Schletter
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

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LFS.BrowseForSpeed
{
    public static partial class NativeMethods
    {
        #region Public Methods
        public static void DisableCloseButton(Form form)
        {
            try
            {
                EnableMenuItem(GetSystemMenu(form.Handle, false), SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
            }
            catch { }
        }

        public static void EnableCloseButton(Form form)
        {
            try
            {
                EnableMenuItem(GetSystemMenu(form.Handle, false), SC_CLOSE, MF_BYCOMMAND | MF_ENABLED);
            }
            catch { }
        }

        public static char GetAsciiCharacter(int keyCode, int flags)
        {
            uint lpChar = 0;
            byte[] characterData = new byte[256];

            GetKeyboardState(characterData);

            ToAscii(keyCode, flags, characterData, ref lpChar, 0);
            return (char)lpChar;
        }

        // Allows us to get current keyboard state. 
        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("user32.dll")]
        public static extern int GetKeyboardState(byte[] lpKeyState);

        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("user32.dll")]
        private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        [System.Security.SuppressUnmanagedCodeSecurity] // We won't use this maliciously
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool PeekMessage(out PeekMsg msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);

        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32")]
        public static extern bool QueryPerformanceFrequency(out long PerformanceFrequency);

        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32")]
        public static extern bool QueryPerformanceCounter(out long PerformanceCount);

        public static void ScrollRichTextBox(RichTextBox box)
        {
            SendMessage(box.Handle, WM_VSCROLL, (IntPtr)SB_BOTTOM, IntPtr.Zero);
        }

        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("winmm.dll")]
        public static extern int timeGetTime();

        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("user32.dll")]
        public static extern bool ToAscii(int VirtualKey, int ScanCode, byte[] lpKeyState, ref uint lpChar, int uFlags);
        #endregion

        #region Public Properties
        public static bool AppStillIdle
		{
			get
			{
				PeekMsg msg;
				return !PeekMessage(out msg, IntPtr.Zero, 0, 0, 0);
			}
        }
        #endregion

        #region Structures
        [StructLayout(LayoutKind.Sequential)]
        public struct PeekMsg
        {
            public IntPtr hWnd;
            public Message msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public System.Drawing.Point p;
        }
        #endregion

        #region Constants
        public const int WM_VSCROLL = 277; // Vertical scroll
        public const int SB_BOTTOM = 7; // Scroll to bottom 

        public const int SC_CLOSE = 0xF060;
        public const int MF_BYCOMMAND = 0x0;
        public const int MF_GRAYED = 0x1;
        public const int MF_ENABLED = 0x0;
        #endregion
    }
}
