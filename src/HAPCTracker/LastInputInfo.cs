using System;
using System.Runtime.InteropServices;

namespace HAPCTracker
{
    /// <summary>
    /// Access to the last time user of the current Windows session
    /// provided some type of input.
    /// See https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getlastinputinfo
    /// </summary>
    public static class LastInputInfo
    {
        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-lastinputinfo
        /// </summary>
        private struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getlastinputinfo
        /// </summary>
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        private static LASTINPUTINFO _last = new LASTINPUTINFO
        {
            cbSize = (uint)Marshal.SizeOf(typeof(LASTINPUTINFO))
        };

        /// <summary>
        /// Get the last time user input was received.
        /// This calls the User32 method GetLastInputInfo.
        /// </summary>
        public static TimeSpan LastInputTime()
        {
            if (!GetLastInputInfo(ref _last))
            {
                throw new Exception("Failed invoking GetLastInputInfo");
            }

            return TimeSpan.FromMilliseconds(Environment.TickCount - _last.dwTime);
        }
    }
}
