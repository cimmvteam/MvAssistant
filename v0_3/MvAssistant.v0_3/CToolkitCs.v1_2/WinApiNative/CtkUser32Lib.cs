using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CToolkitCs.v1_2.WinApiNative
{
    public class CtkUser32Lib
    {


        #region Hook
        public delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", EntryPoint = "SetWindowsHookEx", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(CtkEnumHookType idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        #endregion


        [DllImport("user32.dll", SetLastError = true)]
        public static extern Int32 SendInput(Int32 cInputs, ref CtkMdlInput pInputs, Int32 cbSize);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern System.IntPtr FindWindowByCaption(int ZeroOnly, string lpWindowName);



    }
}
