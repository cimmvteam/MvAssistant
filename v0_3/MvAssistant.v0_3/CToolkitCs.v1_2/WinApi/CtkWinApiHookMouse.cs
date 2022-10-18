using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CToolkitCs.v1_2.WinApiNative;
using System.Runtime.InteropServices;
using System.Threading;


namespace CToolkitCs.v1_2.WinApi
{
    public class CtkWinApiHookMouse
    {
        IntPtr intPtrHook;
        CtkUser32Lib.HookProc HookProcdure;

        /// <summary>
        /// 取得或設定是否獨佔所有滑鼠事件。
        /// </summary>
        public bool Monopolize = false;

        //記憶上次MouseDonw的引發位置，如果與MouseUp的位置不同則不引發Click事件。
        int m_LastBTDownX = 0;
        int m_LastBTDownY = 0;

        //記憶游標上一次的位置，避免MouseMove事件一直引發。
        int m_OldX = 0;
        int m_OldY = 0;

        ~CtkWinApiHookMouse()
        {
            this.Unhook();
        }

        public void Hook()
        {
            //Hook Keyboard
            Unhook();
            if (intPtrHook == IntPtr.Zero)
            {
                HookProcdure = new CtkUser32Lib.HookProc(HookProcCallback);
                intPtrHook = CtkUser32Lib.SetWindowsHookEx(CtkEnumHookType.WH_MOUSE_LL,
                    HookProcdure,
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


        int HookProcCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            EventArgsMouse ehargs = null;
            if (nCode >= 0)
            {
                try
                {
                    var hookType = (CtkEnumConst)wParam;
                    var mouseHookStruct = (CtkMdlHookMouseStruct)Marshal.PtrToStructure(lParam, typeof(CtkMdlHookMouseStruct));

                    short mouseDelta = 0;

                    if (hookType == CtkEnumConst.WM_MOUSEWHEEL)
                        mouseDelta = (short)((mouseHookStruct.mouseData >> 16) & 0xffff);

                    ehargs = new EventArgsMouse(hookType, mouseHookStruct.dx, mouseHookStruct.dy, mouseDelta);

                    if (hookType == CtkEnumConst.WM_MOUSEWHEEL)
                        this.OnMouseWheel(ehargs);

                    else if (hookType == CtkEnumConst.WM_LBUTTONUP || hookType == CtkEnumConst.WM_RBUTTONUP || hookType == CtkEnumConst.WM_MBUTTONUP)
                    {
                        this.OnMouseUp(ehargs);
                        if (mouseHookStruct.dx == m_LastBTDownX && mouseHookStruct.dy == m_LastBTDownY)
                            this.OnMouseClick(ehargs);
                    }
                    else if (hookType == CtkEnumConst.WM_LBUTTONDOWN || hookType == CtkEnumConst.WM_RBUTTONDOWN || hookType == CtkEnumConst.WM_MBUTTONDOWN)
                    {
                        m_LastBTDownX = mouseHookStruct.dx;
                        m_LastBTDownY = mouseHookStruct.dy;
                        this.OnMouseDown(ehargs);
                    }
                    else if (m_OldX != mouseHookStruct.dx || m_OldY != mouseHookStruct.dy)
                    {
                        m_OldX = mouseHookStruct.dx;
                        m_OldY = mouseHookStruct.dy;
                        this.OnMouseMove(ehargs);
                    }
                }
                catch (Exception ex)
                {
                    //給背景執行緒處理, 再出Exception也與原執行緒無關, 可以正常工作
                    ThreadPool.QueueUserWorkItem(delegate { this.OnHookCallbackException(new CtkWinApiEventArgsException() { exception = ex }); });
                }
            }

            if (Monopolize || (ehargs != null && ehargs.Handled))
                return -1;


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




        //---Mouse Event----------------------------------------------------------------


        /// <summary>
        /// 提供 GlobalMouseUp、GlobalMouseDown 和 GlobalMouseMove 事件的資料。
        /// </summary>
        public class EventArgsMouse : EventArgs
        {
            /// <summary>
            /// 取得按下哪個滑鼠鍵的資訊。
            /// </summary>
            public CtkWinApiEnumMouseLMR Button { get; private set; }
            /// <summary>
            /// 取得滑鼠滾輪滾動時帶有正負號的刻度數乘以 WHEEL_DELTA 常數。 一個刻度是一個滑鼠滾輪的刻痕。
            /// </summary>
            public int Delta { get; private set; }
            /// <summary>
            /// 取得滑鼠在產生滑鼠事件期間的 X 座標。
            /// </summary>
            public int X { get; private set; }
            /// <summary>
            /// 取得滑鼠在產生滑鼠事件期間的 Y 座標。
            /// </summary>
            public int Y { get; private set; }
            internal EventArgsMouse(CtkEnumConst wParam, int x, int y, int delta)
            {
                Button = CtkWinApiEnumMouseLMR.None;
                switch (wParam)
                {
                    case CtkEnumConst.WM_LBUTTONDOWN:
                    case CtkEnumConst.WM_LBUTTONUP:
                        Button = CtkWinApiEnumMouseLMR.Left;
                        break;
                    case CtkEnumConst.WM_RBUTTONDOWN:
                    case CtkEnumConst.WM_RBUTTONUP:
                        Button = CtkWinApiEnumMouseLMR.Right;
                        break;
                    case CtkEnumConst.WM_MBUTTONDOWN:
                    case CtkEnumConst.WM_MBUTTONUP:
                        Button = CtkWinApiEnumMouseLMR.Middle;
                        break;
                }
                this.X = x;
                this.Y = y;
                this.Delta = delta;
            }
            private bool m_Handled;
            /// <summary>
            /// 取得或設定值，指出是否處理事件。
            /// </summary>
            public bool Handled
            {
                get { return m_Handled; }
                set { m_Handled = value; }
            }
        }
        public event EventHandler<EventArgsMouse> EhMouseWheel;
        protected void OnMouseWheel(EventArgsMouse ehargs)
        {
            if (EhMouseWheel == null) return;
            this.EhMouseWheel(this, ehargs);
        }

        public event EventHandler<EventArgsMouse> EhMouseDown;
        protected void OnMouseDown(EventArgsMouse ehargs)
        {
            if (EhMouseDown == null) return;
            this.EhMouseDown(this, ehargs);
        }
        public event EventHandler<EventArgsMouse> EhMouseUp;
        protected void OnMouseUp(EventArgsMouse ehargs)
        {
            if (EhMouseUp == null) return;
            this.EhMouseUp(this, ehargs);
        }
        public event EventHandler<EventArgsMouse> EhMouseClick;
        protected void OnMouseClick(EventArgsMouse ehargs)
        {
            if (EhMouseClick == null) return;
            this.EhMouseClick(this, ehargs);
        }
        public event EventHandler<EventArgsMouse> EhMouseMove;
        protected void OnMouseMove(EventArgsMouse ehargs)
        {
            if (EhMouseMove == null) return;
            this.EhMouseMove(this, ehargs);
        }

        #endregion


    }
}
