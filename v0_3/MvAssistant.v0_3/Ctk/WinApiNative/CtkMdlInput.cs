using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvaCToolkitCs.v1_2.WinApiNative
{

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Pack = 1, Size = 28)]
    public struct CtkMdlInput
    {
        [System.Runtime.InteropServices.FieldOffset(0)]
        public CtkMdlInputType dwType;
        [System.Runtime.InteropServices.FieldOffset(4)]
        public CtkStructHookMouse mi;
        [System.Runtime.InteropServices.FieldOffset(4)]
        public CtkStructHookKeyboard ki;
        [System.Runtime.InteropServices.FieldOffset(4)]
        public CtkStructHookHardware hi;
    }
}
