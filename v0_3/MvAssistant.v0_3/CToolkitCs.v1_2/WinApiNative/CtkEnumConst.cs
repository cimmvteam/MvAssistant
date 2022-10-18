using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.WinApiNative
{
    public enum CtkEnumConst
    {
        WM_ACTIVATE = 0x0006,
        WM_APPCOMMAND = 0x0319,
        WM_CHAR = 0x0102,
        WM_DEADCHAR = 0x0103,
        WM_HOTKEY = 0x0312,
        WM_KEYDOWN = 0x100,
        WM_KEYUP = 0x0101,
        WM_KILLFOCUS = 0x0008,
        WM_SETFOCUS = 0x0007,
        WM_SYSDEADCHAR = 0x0107,
        WM_SYSKEYDOWN = 0x0104,
        WM_SYSKEYUP = 0x0105,
        WM_UNICHAR = 0x0109,

        WH_MOUSE_LL = 14,
        WH_KEYBOARD_LL = 13,
        WH_MOUSE = 7,
        WH_KEYBOARD = 2,

        WM_MOUSEMOVE = 0x200,
        WM_LBUTTONDOWN = 0x201,
        WM_RBUTTONDOWN = 0x204,
        WM_MBUTTONDOWN = 0x207,
        WM_LBUTTONUP = 0x202,
        WM_RBUTTONUP = 0x205,
        WM_MBUTTONUP = 0x208,
        WM_LBUTTONDBLCLK = 0x203,
        WM_RBUTTONDBLCLK = 0x206,
        WM_MBUTTONDBLCLK = 0x209,
        WM_MOUSEWHEEL = 0x020A,


        MEF_LEFTDOWN = 0x00000002,
        MEF_LEFTUP = 0x00000004,
        MEF_MIDDLEDOWN = 0x00000020,
        MEF_MIDDLEUP = 0x00000040,
        MEF_RIGHTDOWN = 0x00000008,
        MEF_RIGHTUP = 0x00000010,

        KEF_EXTENDEDKEY = 0x1,
        KEF_KEYUP = 0x2,

        VK_SHIFT = 0x10,
        VK_CAPITAL = 0x14,
        VK_NUMLOCK = 0x90,

        WM_IME_SETCONTEXT = 0x0281,

        WM_IME_COMPOSITION = 0x010F,
        GCS_COMPSTR = 0x0008,
    }
}
