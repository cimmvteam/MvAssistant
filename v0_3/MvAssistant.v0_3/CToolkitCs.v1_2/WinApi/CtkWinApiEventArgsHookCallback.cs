using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.WinApi
{
    public class CtkWinApiEventArgsHookCallback : EventArgs
    {
        public int nCode;
        public IntPtr wParam;
        public IntPtr lParam;
    }
}
