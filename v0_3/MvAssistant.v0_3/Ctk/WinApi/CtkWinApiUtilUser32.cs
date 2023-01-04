using MvaCToolkitCs.v1_2.WinApiNative;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvaCToolkitCs.v1_2.WinApi
{

    public class CtkWinApiUtilUser32
    {




        public static void SendMouseInput(CtkEnumMouseFlag mouseFlag)
        {
            CtkMdlInput mouseInput = new CtkMdlInput();

            mouseInput.dwType = 0;
            mouseInput.mi = new CtkStructHookMouse();
            mouseInput.mi.dwExtraInfo = IntPtr.Zero;
            mouseInput.mi.dx = 0;
            mouseInput.mi.dy = 0;
            mouseInput.mi.time = 0;
            mouseInput.mi.mouseData = 0;
            mouseInput.mi.dwFlags = mouseFlag;

            CtkUser32Lib.SendInput(1, ref mouseInput, System.Runtime.InteropServices.Marshal.SizeOf(typeof(CtkMdlInput)));
        }

        public static void MouseLeftClick()
        {
            SendMouseInput(CtkEnumMouseFlag.LEFTDOWN);
            System.Threading.Thread.Sleep(20);
            SendMouseInput(CtkEnumMouseFlag.LEFTUP);
        }

        public static void MouseMiddleClick()
        {
            SendMouseInput(CtkEnumMouseFlag.MIDDLEDOWN);
            System.Threading.Thread.Sleep(20);
            SendMouseInput(CtkEnumMouseFlag.MIDDLEUP);
        }

        public static void MouseRightClick()
        {
            SendMouseInput(CtkEnumMouseFlag.RIGHTDOWN);
            System.Threading.Thread.Sleep(20);
            SendMouseInput(CtkEnumMouseFlag.RIGHTUP);
        }




        public static void KeyDown(short vk)
        {
            CtkMdlInput input = new CtkMdlInput();
            input.dwType = CtkMdlInputType.Keyboard;


            input.ki = new CtkStructHookKeyboard();
            input.ki.wVk = vk;
            input.ki.dwFlags = 0;
            input.ki.wScan = 0;
            input.ki.time = 0;
            input.ki.dwExtraInfo = IntPtr.Zero;

            CtkUser32Lib.SendInput(1, ref input, System.Runtime.InteropServices.Marshal.SizeOf(typeof(CtkMdlInput)));
        }

        public static void KeyUp(short vk)
        {
            CtkMdlInput input = new CtkMdlInput();
            input.dwType = CtkMdlInputType.Keyboard;


            input.ki = new CtkStructHookKeyboard();
            input.ki.wVk = vk;
            input.ki.dwFlags = CtkEnumKeyboardFlag.KEYUP;
            input.ki.wScan = 0;
            input.ki.time = 0;
            input.ki.dwExtraInfo = IntPtr.Zero;

            CtkUser32Lib.SendInput(1, ref input, System.Runtime.InteropServices.Marshal.SizeOf(typeof(CtkMdlInput)));
        }


        public static void KeyPress(short vk)
        {
            KeyDown(vk);
            System.Threading.Thread.Sleep(20);
            KeyUp(vk);
        }

    }
}
