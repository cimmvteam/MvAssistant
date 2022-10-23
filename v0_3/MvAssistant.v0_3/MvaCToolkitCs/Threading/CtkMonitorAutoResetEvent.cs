using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvaCToolkitCs.v1_2.Threading
{

    /*[d20220501] 
     * 若使用ManualResetEvent, 才需要搭配Monitor去阻擋同時通過兩個以上.
     * 若使用AutoResetEvent, 本身就只會放行一個, 不用搭配Monitor. 若以防萬一, 還是可用一下Monitor
     */
    /*[d20220502]
     * 若使用ManualResetEvent, WaitOne以後要自己Reset, 避免後面Thread跟著過去.
     * 若使用AutoResetEvent, WaitOne以後會自動Reset, 只放行一個
     */
    /*[d20220717] 可設定確認間隔的休息時間, 把執行緒讓出去. 
     * 利用Monitor的特性 鎖住特定範圍, 鎖定範圍要盡可能小.
     * 利用ResetEvent特性 阻止其它執行緒操作.
     * 若不想使用此類別
     *   若只使用Monitor無法長期阻塞執行緒, 並不建議Lock大範圍的Code.
     *   若只使用ManualResetEvent, 有機率同時放行兩個以上.
     *   若只使用AutoResetEvent
     *     .. 若配合持續等待, 在被長期阻塞期間, 該執行緒會無法進行任何作業
     *     .. 若配合持續等待, 且就是要刻意不進行其它操作, 會導致程序在釋放時無法終止.
     *        所以, 此類別的等待都兼具 總等待時間, 嘗試時間 與 休息時間, 以利判斷是否停止嘗試
     *     .. 若配合短期等待, 繼續其它作業, 是符合需求, 但: 
     *        無法被繼承 或 提供其它方便功能.
     *        要注意寫 try/finally 避免沒有被Set回來
     *        這邊提供 using/dispose 可以主動 解鎖/解阻塞
     *        
     * 此類別除了提供方便的功能以外, 也同時敍述各種可能狀況, 提供使用參考
     * 未必非得要用此類別完成工作, 而是藉此類別理解運作.
     * MS提供的Library通常較是基底且較為彈性, 使用上需要進行包裝才方便
     * 
     * 短期阻塞用 lock, 短期阻塞+嘗試時間用 Monitor.
     * lock/Monitor 鎖執行緒不建議用在長期 且 要盡可能減少涵蓋代碼: 一是影響效能, 二是容易有死鎖.
     * 長期阻塞(跨method) 用 ResetEvent.
     * 需要 方便的功能 再考慮用此類別
     */



    /// <summary> 為了反覆確認可否 進入/取得鎖 而寫的類別. </summary>
    public class CtkMonitorAutoResetEvent : IDisposable
    {
        /// <summary> 使用AutoResetEvent, 放了一個Thread後會自動再堵起來 </summary>
        protected AutoResetEvent _resetEvent = new AutoResetEvent(true);

        /// <summary> 不使用Monitor阻塞 </summary>
        public virtual CtkMonitorAutoResetEventBlocker OnceBlocker()
        {
            if (!this.TryReset()) return null;
            return new CtkMonitorAutoResetEventBlocker(this);
        }
        /// <summary> 使用Monitor阻塞 </summary>
        public virtual CtkMonitorAutoResetEventBlockerM OnceBlockerM()
        {
            if (!this.TryEnter()) return null;
            return new CtkMonitorAutoResetEventBlockerM(this);
        }


        /// <summary> 使用Monitor進行Reset </summary>
        public virtual bool TryEnter(int waitTimeLimitMs = 0, int eachTryGetLockTimeLimitMs = 10, int eachTryResetTimeLimitMs = 10, int eachRestTimeMs = 10)
        {
            var entryTime = DateTime.Now;

            //waitTimeUpLimitMs <= 0 代表無限等待
            while (waitTimeLimitMs <= 0 || (DateTime.Now - entryTime).TotalMilliseconds < waitTimeLimitMs)
            {
                //注意: 如果 eachTryGetLockTimeLimitMs <= 0, 執行緒會一直在嘗試取得 lock, 無法回到這行
                if (this.disposed) return false;

                try
                {//不做大範圍Lock, 只對關鍵的
                    var isGetLock = false;
                    if (eachTryGetLockTimeLimitMs > 0)
                        isGetLock = Monitor.TryEnter(this, eachTryGetLockTimeLimitMs);
                    else
                        isGetLock = Monitor.TryEnter(this);


                    if (isGetLock)
                    {
                        //若在這Wait不歸還Lock, 會導致其它Thread無法取得Lock, 無法進行Set就永遠等不到
                        if (eachTryResetTimeLimitMs <= 0)
                            throw new ArgumentException("if you can get locker then another cannot, reset time <= 0 will cause dead lock");

                        if (this._resetEvent.WaitOne(eachTryResetTimeLimitMs))
                        {
                            //AutoResetEvent會自動Reset, 此行不一定要執行, 只是以防萬一
                            this._resetEvent.Reset();//先堵住, 不讓後面的再次進行
                            return true;
                        }
                    }
                }
                finally { if (Monitor.IsEntered(this)) Monitor.Exit(this); }
                //解鎖後再等待
                Thread.Sleep(eachRestTimeMs);
            }
            return false;
        }

        /// <summary> 使用Monitor進行Set </summary>
        public virtual bool TryExit(int waitTimeLimitMs = 0, int eachTryGetLockTimeLimitMs = 10)
        {
            var entryTime = DateTime.Now;

            //waitTimeLimitMs <= 0 代表無限等待
            while (waitTimeLimitMs <= 0 || (DateTime.Now - entryTime).TotalMilliseconds < waitTimeLimitMs)
            {
                //注意: 如果 tryGetLockTimeMs <= 0, 執行緒會一直在嘗試取得 lock, 無法回到這行
                if (this.disposed) return false;

                try
                {
                    //不做大範圍Lock, 只對關鍵的
                    var isGetLock = false;
                    if (eachTryGetLockTimeLimitMs > 0)
                        isGetLock = Monitor.TryEnter(this, eachTryGetLockTimeLimitMs);
                    else
                        isGetLock = Monitor.TryEnter(this);

                    if (isGetLock)
                    {
                        this._resetEvent.Set();
                        return true;
                    }
                }
                finally { if (Monitor.IsEntered(this)) Monitor.Exit(this); }
            }
            return false;
        }

        /// <summary> 不使用Monitor進行Reset </summary>
        public virtual bool TryReset(int waitTimeLimitMs = 0, int eachTryTimeLimitMs = 10, int eachRestTimeMs = 10)
        {
            var entryTime = DateTime.Now;

            //waitTimeLimitMs <= 0 代表無限等待
            while (waitTimeLimitMs <= 0 || (DateTime.Now - entryTime).TotalMilliseconds < waitTimeLimitMs)
            {
                if (this.disposed) return false;

                if (eachTryTimeLimitMs <= 0)
                {
                    if (this._resetEvent.WaitOne())
                    {
                        //AutoResetEvent會自動Reset, 此行不一定要執行, 只是以防萬一
                        this._resetEvent.Reset();//先堵住, 不讓後面的再次進行
                        return true;
                    }
                }
                else
                {
                    if (this._resetEvent.WaitOne(eachTryTimeLimitMs))
                    {
                        //AutoResetEvent會自動Reset, 此行不一定要執行, 只是以防萬一
                        this._resetEvent.Reset();//先堵住, 不讓後面的再次進行
                        return true;
                    }
                }

                Thread.Sleep(eachRestTimeMs);
            }
            return false;
        }

        /// <summary> 不使用Monitor進行Set </summary>
        public virtual bool TrySet(int waitTimeLimitMs = 0, int eachTryTimeLimitMs = 10, int eachRestTimeMs = 10)
        {
            var entryTime = DateTime.Now;

            //waitTimeLimitMs <= 0 代表無限等待
            while (waitTimeLimitMs <= 0 || (DateTime.Now - entryTime).TotalMilliseconds < waitTimeLimitMs)
            {
                if (this.disposed) return false;

                if (eachTryTimeLimitMs <= 0)
                {
                    this._resetEvent.Set();
                    return true;
                }

                Thread.Sleep(eachRestTimeMs);
            }

            return false;
        }



        #region IDisposable

        // Flag: Has Dispose already been called?
        protected bool disposed = false;
        // Public implementation of Dispose pattern callable by consumers.
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            this.DisposeSelf();
            disposed = true;
        }

        protected virtual void DisposeSelf()
        {
            if (this._resetEvent != null)
            {
                this._resetEvent.Close();
                this._resetEvent.Dispose();
            }
            if (Monitor.IsEntered(this))
            {
                Monitor.Exit(this);
            }

        }

        #endregion




    }
}
