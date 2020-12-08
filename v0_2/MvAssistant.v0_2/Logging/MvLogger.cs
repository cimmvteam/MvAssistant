using MvAssistant.v0_2.Tasking;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace MvAssistant.v0_2.Logging
{
    public class MvLogger : IMvLoggable
    {
        public ConcurrentQueue<MvLoggerEventArgs> msgQueue = new ConcurrentQueue<MvLoggerEventArgs>();
        public MvCancelTask Task;


        /// <summary>
        /// 預設寫Log是用非同步
        /// </summary>
        /// <param name="ea"></param>
        public void Write(MvLoggerEventArgs ea)
        {
            this.WriteAsyn(ea);
        }
        public void Write(MvLoggerEventArgs ea, MvLoggerEnumLevel _level = MvLoggerEnumLevel.Info)
        {
            ea.Level = _level;
            this.WriteAsyn(ea);
        }


        public void WriteSyn(MvLoggerEventArgs ea)
        {
            this.OnLogWrite(ea);
            OnEveryLogWrite(this, ea);
        }
 

        public void WriteAsyn(MvLoggerEventArgs ea)
        {
            this.msgQueue.Enqueue(ea);

            lock (this)
            {
                if (this.Task != null)
                {
                    if (this.Task.IsEnd())
                    {
                        using (var obj = this.Task)
                            obj.Cancel();
                    }
                    else return;
                }

                this.Task = MvCancelTask.RunLoop(() =>
                {
                    MvSpinWait.SpinUntil(() => this.msgQueue.Count > 0);

                    MvLoggerEventArgs eaLog;
                    if (!this.msgQueue.TryDequeue(out eaLog)) return true;
                    this.WriteSyn(eaLog);
                    return true;
                });
            }
        }



        //public static void Write(string msg, params object[] args) { Logger.Write(string.Format(msg, args)); }會造成呼叫模擬兩可

        public void Verbose(string msg, params object[] args) { this.Write(string.Format(msg, args), MvLoggerEnumLevel.Verbose); }
        public void Debug(string msg, params object[] args) { this.Write(string.Format(msg, args), MvLoggerEnumLevel.Debug); }
        public void Info(string msg, params object[] args) { this.Write(string.Format(msg, args), MvLoggerEnumLevel.Info); }
        public void Warn(string msg, params object[] args) { this.Write(string.Format(msg, args), MvLoggerEnumLevel.Warn); }
        public void Error(string msg, params object[] args) { this.Write(string.Format(msg, args), MvLoggerEnumLevel.Error); }
        public void Fatal(string msg, params object[] args) { this.Write(string.Format(msg, args), MvLoggerEnumLevel.Fatal); }




        #region Event
        public event EventHandler<MvLoggerEventArgs> EhLogWrite;
        void OnLogWrite(MvLoggerEventArgs ea)
        {
            if (this.EhLogWrite == null)
                return;
            this.EhLogWrite(this, ea);
        }
        #endregion






        #region Static
        public static MvLogger Singleton { get { return MvLoggerMapper.Singleton.Get(); } }


        public static event EventHandler<MvLoggerEventArgs> EhEveryLogWrite;
        static void OnEveryLogWrite(object sender, MvLoggerEventArgs ea)
        {
            if (EhEveryLogWrite == null) return;
            EhEveryLogWrite(sender, ea);
        }

        #endregion


     
    }
}