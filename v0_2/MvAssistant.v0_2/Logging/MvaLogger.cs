using MvAssistant.v0_2.Threading;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace MvAssistant.v0_2.Logging
{

    public class MvaLogger : IDisposable
    {

        protected ConcurrentQueue<MvaLoggerEventArgs> queue = new ConcurrentQueue<MvaLoggerEventArgs>();
        MvaTask task;

        public virtual void Debug(string msg, params object[] args) { this.Write(string.Format(msg, args), MvaLoggerEnumLevel.Debug); }
        public virtual void Error(string msg, params object[] args) { this.Write(string.Format(msg, args), MvaLoggerEnumLevel.Error); }
        public virtual void Fatal(string msg, params object[] args) { this.Write(string.Format(msg, args), MvaLoggerEnumLevel.Fatal); }
        public virtual void Info(string msg, params object[] args) { this.Write(string.Format(msg, args), MvaLoggerEnumLevel.Info); }
        public virtual void Verbose(string msg, params object[] args) { this.Write(string.Format(msg, args), MvaLoggerEnumLevel.Verbose); }
        public virtual void Warn(string msg, params object[] args) { this.Write(string.Format(msg, args), MvaLoggerEnumLevel.Warn); }

        /// <summary>
        /// 預設寫Log是用非同步
        /// </summary>
        /// <param name="ea"></param>
        public virtual void Write(MvaLoggerEventArgs ea) { this.WriteAsyn(ea); }
        public virtual void Write(MvaLoggerEventArgs ea, MvaLoggerEnumLevel _level)
        {
            ea.Level = _level;
            this.WriteAsyn(ea);
        }
        protected virtual void WriteAsyn(MvaLoggerEventArgs ea)
        {
            this.queue.Enqueue(ea);
            if (!Monitor.TryEnter(this, 1000)) return;
            try
            {
                //若還沒結束執行, 先return
                if (this.task != null && !this.task.IsEnd()) return;
                this.CloseTask();

                this.task = MvaTask.RunLoop(() =>
                {
                    MvaLoggerEventArgs myea;
                    lock (this)
                    {
                        if (!this.queue.TryDequeue(out myea)) return true;//取不出來就下次再取
                    }
                    this.WriteSyn(myea);

                    //若Count等於零, 這個task會結束, IsEnd() = true
                    return this.queue.Count > 0;
                });
            }
            finally { Monitor.Exit(this); }
        }
        protected virtual void WriteSyn(MvaLoggerEventArgs ea)
        {
            this.OnLogWrite(ea);
            OnEveryLogWrite(this, ea);
        }


        public void CloseTask()
        {
            if (this.task != null)
            {
                //若之前有, 把它清乾淨
                using (var obj = this.task)
                {
                    if (!obj.IsEnd()) obj.Cancel();
                    obj.Dispose();
                }
            }
        }
        public void Close()
        {
            this.CloseTask();
        }



        #region Event

        /// <summary>
        /// Event Naming: Prepare, Eh, After
        /// </summary>
        public event EventHandler<MvaLoggerEventArgs> EhLogWrite;
        void OnLogWrite(MvaLoggerEventArgs ea)
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

            this.DisposeSelf();

            disposed = true;
        }


        void DisposeSelf()
        {
            try { this.Close(); }
            catch (Exception ex) { MvaLog.Write(ex); }
        }

        #endregion




        #region Static


        public static event EventHandler<MvaLoggerEventArgs> EhEveryLogWrite;
        static void OnEveryLogWrite(object sender, MvaLoggerEventArgs ea)
        {
            if (EhEveryLogWrite == null) return;
            EhEveryLogWrite(sender, ea);
        }

        #endregion

    }




}