using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CToolkitCs.v1_2.WinApiNative
{
    public class CtkWinInetLib
    {

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookieEx(string lpszUrlName, string lpszCookieName, string lpszCookieData, uint dwFlags, IntPtr dwReserved);
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetOption(int hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

    }
}
