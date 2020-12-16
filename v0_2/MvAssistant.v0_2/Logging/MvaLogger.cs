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
    public class MvaLogger : IMvaLoggable
    {
        public ConcurrentQueue<MvaLoggerEventArgs> msgQueue = new ConcurrentQueue<MvaLoggerEventArgs>();
        public MvaCancelTask Task;


        /// <summary>
        /// 預設寫Log是用非同步
        /// </summary>
        /// <param name="ea"></param>
        public void Write(MvaLoggerEventArgs ea)
        {
            this.WriteAsyn(ea);
        }
        public void Write(MvaLoggerEventArgs ea, MvaLoggerEnumLevel _level = MvaLoggerEnumLevel.Info)
        {
            ea.Level = _level;
            this.WriteAsyn(ea);
        }


        public void WriteSyn(MvaLoggerEventArgs ea)
        {
            this.OnLogWrite(ea);
            OnEveryLogWrite(this, ea);
        }
 

        public void WriteAsyn(MvaLoggerEventArgs ea)
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

                this.Task = MvaCancelTask.RunLoop(() =>
                {
                    MvaSpinWait.SpinUntil(() => this.msgQueue.Count > 0);

                    MvaLoggerEventArgs eaLog;
                    if (!this.msgQueue.TryDequeue(out eaLog)) return true;
                    this.WriteSyn(eaLog);
                    return true;
                });
            }
        }



        //public static void Write(string msg, params object[] args) { Logger.Write(string.Format(msg, args)); }會造成呼叫模擬兩可

        public void Verbose(string msg, params object[] args) { this.Write(string.Format(msg, args), MvaLoggerEnumLevel.Verbose); }
        public void Debug(string msg, params object[] args) { this.Write(string.Format(msg, args), MvaLoggerEnumLevel.Debug); }
        public void Info(string msg, params object[] args) { this.Write(string.Format(msg, args), MvaLoggerEnumLevel.Info); }
        public void Warn(string msg, params object[] args) { this.Write(string.Format(msg, args), MvaLoggerEnumLevel.Warn); }
        public void Error(string msg, params object[] args) { this.Write(string.Format(msg, args), MvaLoggerEnumLevel.Error); }
        public void Fatal(string msg, params object[] args) { this.Write(string.Format(msg, args), MvaLoggerEnumLevel.Fatal); }




        #region Event
        public event EventHandler<MvaLoggerEventArgs> EhLogWrite;
        void OnLogWrite(MvaLoggerEventArgs ea)
        {
            if (this.EhLogWrite == null)
                return;
            this.EhLogWrite(this, ea);
        }
        #endregion






        #region Static
        public static MvaLogger Singleton { get { return MvaLoggerMapper.Singleton.Get(); } }


        public static event EventHandler<MvaLoggerEventArgs> EhEveryLogWrite;
        static void OnEveryLogWrite(object sender, MvaLoggerEventArgs ea)
        {
            if (EhEveryLogWrite == null) return;
            EhEveryLogWrite(sender, ea);
        }

        #endregion


     
    }
}