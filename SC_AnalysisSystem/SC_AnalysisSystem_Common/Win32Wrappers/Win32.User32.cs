using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace YJYTemplate.Common
{
    public static partial class Win32
    {
        [DllImport("User32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        public const int SW_SHOWNORMAL = 1;
        public const int SW_SHOW = 5;
        public const int SW_RESTORE = 9;

        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}
