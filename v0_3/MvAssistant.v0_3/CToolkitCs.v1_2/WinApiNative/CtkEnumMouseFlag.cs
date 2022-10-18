using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.WinApiNative
{
    [Flags()]
    public enum CtkEnumMouseFlag : int
    {
        MOVE = 0x1,
        LEFTDOWN = 0x2,
        LEFTUP = 0x4,
        RIGHTDOWN = 0x8,
        RIGHTUP = 0x10,
        MIDDLEDOWN = 0x20,
        MIDDLEUP = 0x40,
        XDOWN = 0x80,
        XUP = 0x100,
        VIRTUALDESK = 0x400,
        WHEEL = 0x800,
        ABSOLUTE = 0x8000
    }

}
