using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CToolkitCs.v1_2.WinApiNative;
using System.Threading;


namespace CToolkitCs.v1_2.WinApi
{
    public class CtkWinApiHookKeyboard
    {
        IntPtr intPtrHook;
        CtkUser32Lib.HookProc hookProc;

        Dictionary<int, bool> keepKeys = new Dictionary<int, bool>();
        public bool IsKeepCtrl { get { return keepKeys[162]; } }
        public bool IsKeepAlt { get { return keepKeys[164]; } }


        ~CtkWinApiHookKeyboard()
        {
            this.Unhook();
        }

        public void Hook()
        {
            //Hook Keyboard
            Unhook();
            if (intPtrHook == IntPtr.Zero)
            {
                hookProc = new CtkUser32Lib.HookProc(HookProcCallback);
                intPtrHook = CtkUser32Lib.SetWindowsHookEx(CtkEnumHookType.WH_KEYBOARD_LL,
                    hookProc,
                    IntPtr.Zero,
                    0);

                if (intPtrHook == IntPtr.Zero)
                    throw new CtkException("WinApi Error-" + System.Runtime.InteropServices.Marshal.GetLastWin32Error());
            }
        }
        public void Unhook()
        {
            if (intPtrHook != IntPtr.Zero)
            {
                CtkUser32Lib.UnhookWindowsHookEx(intPtrHook);
                intPtrHook = IntPtr.Zero;
            }
        }

        protected int HookProcCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                try { this.OnHookCallback(new CtkWinApiEventArgsHookCallback() { nCode = nCode, wParam = wParam, lParam = lParam }); }
                catch (Exception ex) { ThreadPool.QueueUserWorkItem(delegate { this.OnHookCallbackException(new CtkWinApiEventArgsException() { exception = ex }); }); }

                try
                {

                    int vkCode = System.Runtime.InteropServices.Marshal.ReadInt32(lParam);
                    CtkEnumConst kbInput = (CtkEnumConst)wParam;

                    if (kbInput == CtkEnumConst.WM_KEYDOWN || kbInput == CtkEnumConst.WM_SYSKEYDOWN) { keepKeys[vkCode] = true; }
                    if (kbInput == CtkEnumConst.WM_KEYUP || kbInput == CtkEnumConst.WM_SYSKEYUP) { keepKeys[vkCode] = false; }


                }
                catch (Exception ex)
                {
                    //給背景執行緒處理, 再出Exception也與原執行緒無關, 可以正常工作
                    ThreadPool.QueueUserWorkItem(delegate { this.OnHookCallbackException(new CtkWinApiEventArgsException() { exception = ex }); });
                }
            }
            return CtkUser32Lib.CallNextHookEx(intPtrHook, nCode, wParam, lParam);
        }





        #region Event


        //---HookCallback----------------------------------------------------------------
        public event EventHandler<CtkWinApiEventArgsHookCallback> EhHookCallback;
        protected void OnHookCallback(CtkWinApiEventArgsHookCallback ehargs)
        {
            if (EhHookCallback == null) return;
            this.EhHookCallback(this, ehargs);
        }




        public event EventHandler<CtkWinApiEventArgsException> EhHookCallbackException;
        protected void OnHookCallbackException(CtkWinApiEventArgsException ex)
        {
            if (EhHookCallbackException == null) return;
            this.EhHookCallbackException(this, ex);
        }


        #endregion

    }
}
