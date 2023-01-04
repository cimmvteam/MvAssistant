using MvaCToolkitCs.v1_2.ContextFlow;
using MvaCToolkitCs.v1_2.Threading;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvaCToolkitCs.v1_2.Worker
{
    public abstract class CtkWorkerProcBase01<T> : ICtkContextFlowRun
        where T : EventArgs, ICtkWorkerProcBase01Msg
    {
        /*[d20210211]
         * 建立一物件, 負責處理一 特定 領域/專長/作業 的事,
         * 此物件通常有自己的生命週, 有一些與主物件的溝通方式 並 提供一些存取操作的方法.
         * 有點類似 Controller, 但 Controller 較像是在控制 商業邏輯層, 如何接軌 DB, UI 與 Model.
         * 而 Worker 的工作比較雜, 一個特定範圍的作業都是找它處理
         */
        /*[d20221106] 一個 Thread/Task 處理事務 的 概念, 太常用了
         * 配合 Context Flow 與 Command Pattern 建置一個基本工作物件 */





        #region Process

        protected ConcurrentQueue<T> CtkProcMsgQueue = new ConcurrentQueue<T>();
        /// <summary> 預設休息1秒, 避免資源消耗 </summary>
        protected int CtkProcSleep = 1000;
        protected CtkTask CtkProcTask;

        public virtual void CtkProcMsgRequest(T msg) { lock (this) { this.CtkProcMsgQueue.Enqueue(msg); } }
        protected virtual int CtkProcMain()
        {

            for (var idx = 0; idx < 99; idx++)
            {
                lock (this)
                {
                    if (this.CtkProcMsgQueue.Count == 0) break;
                    var msg = default(T);
                    if (!this.CtkProcMsgQueue.TryDequeue(out msg)) throw new CtkException("Can not get message from queue");
                    var result = this.CtkProcMsg(msg);

                    //若沒拋出 Exception 僅有 Error Code, 那麼 Log後 繼續下一回
                    if (result != 0) CtkLog.WarnAnF(this, $"{this.GetType().Name} Error Code= {result}");
                }
            }

            return this.CtkProcWork();
        }
        protected abstract int CtkProcMsg(T msg);
        protected abstract int CtkProcWork();

        #endregion



        #region Event

        public event EventHandler<T> EhException;
        public event EventHandler<T> EhMsgGenerated;

        protected void OnException(Exception ex)
        {
            if (this.EhException == null) return;
            var ea = Activator.CreateInstance<T>();
            ea.Exception = ex;
            this.EhException(this, ea);
        }
        protected void OnException(T ea)
        {
            if (this.EhException == null) return;
            this.EhException(this, ea);
        }
        protected void OnMsgGenerated(T msg)
        {
            if (this.EhMsgGenerated == null) return;
            this.EhMsgGenerated(this, msg);
        }

        #endregion



        #region Context Flow

        public bool CtkCfIsRunning { get; set; }
        public virtual int CtkCfBoot() { return 0; }
        public virtual int CtkCfFree()
        {
            this.DisposeClose();
            return 0;
        }
        public virtual int CtkCfLoad() { return 0; }
        public virtual int CtkCfRunLoop()
        {
            if (this.CtkProcTask != null && this.CtkProcTask.Status < TaskStatus.RanToCompletion) return 0;
            if (this.CtkCfIsRunning) return 0;
            this.CtkCfIsRunning = true;

            while (this.CtkCfIsRunning && !this.disposed)
            {
                try
                {
                    this.CtkProcMain();
                }
                catch (Exception ex)
                {
                    this.OnException(ex);//Task不中斷, 回報Exception
                    CtkLog.WarnAn(this, ex);//同時Record
                }
                Thread.Sleep(this.CtkProcSleep);
            }
            this.CtkCfIsRunning = false;
            return 0;
        }
        public virtual int CtkCfRunLoopStart()
        {
            if (this.CtkProcTask != null && this.CtkProcTask.Status < TaskStatus.RanToCompletion) return 0;
            if (this.CtkCfIsRunning) return 0;
            this.CtkCfIsRunning = true;
            this.CtkProcTask = CtkTask.RunLoop(ct =>
            {
                try
                {
                    this.CtkProcMain();
                }
                catch (Exception ex)
                {
                    this.OnException(ex);//Task不中斷, 回報Exception
                    CtkLog.WarnAn(this, ex);//同時Record
                }
                return this.CtkCfIsRunning && !this.disposed;
            }, this.GetType().Name, this.CtkProcSleep);

            return 0;
        }
        public virtual int CtkCfRunOnce()
        {
            try
            {
                this.CtkProcMain();
            }
            catch (Exception ex)
            {
                this.OnException(ex);//回報Exception
                CtkLog.WarnAn(this, ex);//同時Record
                throw ex;//跑一次的, 有Exception就拋出
            }
            return 0;
        }
        public virtual int CtkCfUnload() { return 0; }

        #endregion



        #region IDisposable

        // Flag: Has Dispose already been called?
        protected bool disposed = false;

        ~CtkWorkerProcBase01() { this.Dispose(false); }

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
            this.DisposeClose();
            disposed = true;
        }
        public virtual void DisposeClose()
        {
            CtkEventUtil.RemoveEventHandlersOfOwnerByFilter(this, (dlgt) => true);
            CtkUtil.DisposeTaskTry(this.CtkProcTask);
        }

        #endregion








    }
}
