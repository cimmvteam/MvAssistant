using MvaCToolkitCs.v1_2.Threading;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace MvaCToolkitCs.v1_2.Logging
{
    public class CtkLogger : IDisposable
    {

        protected ConcurrentQueue<CtkLoggerEventArgs> queue = new ConcurrentQueue<CtkLoggerEventArgs>();
        CtkTask task;



        public void CloseTask()
        {
            CtkUtil.DisposeTaskTry(this.task);
        }

        public virtual void LogWrite(Object obj, CtkLoggerEventArgs ea)
        {
            ea.LogObject = obj;
            this.WriteAsyn(ea);
        }
        public virtual void LogWrite(Object obj, CtkLoggerEventArgs ea, CtkLoggerEnumLevel _level)
        {
            ea.LogObject = obj;
            ea.Level = _level;
            this.WriteAsyn(ea);
        }

        /// <summary> 預設寫Log是用非同步 </summary>
        public virtual void Write(CtkLoggerEventArgs ea) { this.WriteAsyn(ea); }
        public virtual void Write(CtkLoggerEventArgs ea, CtkLoggerEnumLevel _level)
        {
            ea.Level = _level;
            this.WriteAsyn(ea);
        }




        protected virtual void WriteAsyn(CtkLoggerEventArgs ea)
        {
            this.queue.Enqueue(ea);
            if (!Monitor.TryEnter(this, 1000)) return;
            try
            {
                //若還沒結束執行, 先return
                if (this.task != null && !this.task.IsEnd()) return;
                this.CloseTask();

                this.task = CtkTask.RunLoop(() =>
                {
                    CtkLoggerEventArgs myea;
                    if (!this.queue.TryDequeue(out myea)) return true;//取不出來就下次再取
                    this.WriteSyn(myea);

                    //若Count等於零, 這個task會結束, IsEnd() = true
                    return this.queue.Count > 0;
                });
            }
            finally { Monitor.Exit(this); }
        }
        protected virtual void WriteSyn(CtkLoggerEventArgs ea)
        {
            this.OnLogWrite(ea);
            OnEveryLogWrite(this, ea);
        }





        #region Quick Usage

        public virtual void Debug(string msg) { this.Write(msg, CtkLoggerEnumLevel.Debug); }
        public virtual void DebugF(string msg, params object[] args) { this.Write(string.Format(msg, args), CtkLoggerEnumLevel.Debug); }
        public virtual void Error(string msg) { this.Write(msg, CtkLoggerEnumLevel.Error); }
        public virtual void ErrorF(string msg, params object[] args) { this.Write(string.Format(msg, args), CtkLoggerEnumLevel.Error); }
        public virtual void Fatal(string msg) { this.Write(msg, CtkLoggerEnumLevel.Fatal); }
        public virtual void FatalF(string msg, params object[] args) { this.Write(string.Format(msg, args), CtkLoggerEnumLevel.Fatal); }
        public virtual void Info(string msg) { this.Write(msg, CtkLoggerEnumLevel.Info); }
        public virtual void InfoF(string msg, params object[] args) { this.Write(string.Format(msg, args), CtkLoggerEnumLevel.Info); }
        public virtual void Verbose(string msg) { this.Write(msg, CtkLoggerEnumLevel.Verbose); }
        public virtual void VerboseF(string msg, params object[] args) { this.Write(string.Format(msg, args), CtkLoggerEnumLevel.Verbose); }
        public virtual void Warn(string msg) { this.Write(msg, CtkLoggerEnumLevel.Warn); }
        public virtual void WarnF(string msg, params object[] args) { this.Write(string.Format(msg, args), CtkLoggerEnumLevel.Warn); }

        #endregion


        #region Take Log Object
        public virtual void LogDebug(object obj, string msg) { this.LogWrite(obj, msg, CtkLoggerEnumLevel.Debug); }
        public virtual void LogDebugF(object obj, string msg, params object[] args) { this.LogWrite(obj, string.Format(msg, args), CtkLoggerEnumLevel.Debug); }
        public virtual void LogError(object obj, string msg) { this.LogWrite(obj, msg, CtkLoggerEnumLevel.Error); }
        public virtual void LogErrorF(object obj, string msg, params object[] args) { this.LogWrite(obj, string.Format(msg, args), CtkLoggerEnumLevel.Error); }
        public virtual void LogFatal(object obj, string msg) { this.LogWrite(obj, msg, CtkLoggerEnumLevel.Fatal); }
        public virtual void LogFatalF(object obj, string msg, params object[] args) { this.LogWrite(obj, string.Format(msg, args), CtkLoggerEnumLevel.Fatal); }
        public virtual void LogInfo(object obj, string msg) { this.LogWrite(obj, msg, CtkLoggerEnumLevel.Info); }
        public virtual void LogInfoF(object obj, string msg, params object[] args) { this.LogWrite(obj, string.Format(msg, args), CtkLoggerEnumLevel.Info); }
        public virtual void LogVerbose(object obj, string msg) { this.LogWrite(obj, msg, CtkLoggerEnumLevel.Verbose); }
        public virtual void LogVerboseF(object obj, string msg, params object[] args) { this.LogWrite(obj, string.Format(msg, args), CtkLoggerEnumLevel.Verbose); }
        public virtual void LogWarn(object obj, string msg) { this.LogWrite(obj, msg, CtkLoggerEnumLevel.Warn); }
        public virtual void LogWarnF(object obj, string msg, params object[] args) { this.LogWrite(obj, string.Format(msg, args), CtkLoggerEnumLevel.Warn); }

        #endregion


        #region Event

        /// <summary>
        /// Event Naming: Prepare, Eh, After
        /// </summary>
        public event EventHandler<CtkLoggerEventArgs> EhLogWrite;
        void OnLogWrite(CtkLoggerEventArgs ea)
        {
            if (this.EhLogWrite == null) return;
            this.EhLogWrite(this, ea);
        }
        #endregion



        #region IDisposable
        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void DisposeClose()
        {
            try { this.CloseTask(); }
            catch (Exception ex) { CtkLog.Write(ex); }
            //斷線不用清除Event, 但Dispsoe需要, 因為即使斷線此物件仍存活著
            CtkEventUtil.RemoveEventHandlersOfOwnerByFilter(this, (dlgt) => true);
        }

        // Protected implementation of Dispose pattern.
        protected void Dispose(bool disposing)
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
        #endregion




        #region Static

        public static event EventHandler<CtkLoggerEventArgs> EhEveryLogWrite;
        static void OnEveryLogWrite(object sender, CtkLoggerEventArgs ea)
        {
            if (EhEveryLogWrite == null) return;
            EhEveryLogWrite(sender, ea);
        }

        #endregion

    }


}
