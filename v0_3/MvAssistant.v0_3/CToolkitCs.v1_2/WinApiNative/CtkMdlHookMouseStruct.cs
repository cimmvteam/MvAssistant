using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.WinApiNative
{

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
    public struct CtkMdlHookMouseStruct
    {
        public Int32 dx;
        public Int32 dy;
        public Int32 mouseData;
        public CtkEnumMouseFlag dwFlags;
        public Int32 time;
        public IntPtr dwExtraInfo;
      
    }
}
