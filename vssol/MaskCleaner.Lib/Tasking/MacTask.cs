using MvLib.TaskDispatch;
using MvLib.Tasking;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Tasking
{
    public class MacTask : MvCancelTask
    {

        public int Priority;

        public Thread Thread;

        public System.Timers.Timer Timer;

        private ProcessThread ProcessThread;

        public MacTask(string purpose, Task tk)
        {
            this.Name = purpose;
            this.Task = tk;
            this.AddToMgr();
        }

        /// <summary>
        /// 件立Thread物件並且填入threadMGR管理
        /// </summary>
        /// <param name="purpose"></param>
        /// <param name="act"></param>
        public MacTask(string purpose, Action act)
        {
            this.Name = purpose;
            this.Task = new Task(act);
            this.AddToMgr();
        }

        public MacTask(string porpose)
        {
            this.Name = porpose;
            this.AddToMgr();
        }

        public MacTask(System.Timers.Timer tr)
        {
            this.Timer = tr;
            this.AddToMgr();
        }

        public MacTask(Thread td)
        {
            this.Thread = td;
            this.AddToMgr();
        }

        public MacTask(string purpose, System.Diagnostics.ProcessThread singleThread)
        {
            // TODO: Complete member initialization
            this.Name = purpose;
            this.ProcessThread = singleThread;
            this.AddToMgr();
        }

        ~MacTask() { this.Dispose(false); }

        public override int Id
        {
            get
            {
                if (this.Task != null) return this.Task.Id;
                if (this.Thread != null) return this.Thread.ManagedThreadId;
                if (this.ProcessThread != null) return this.ProcessThread.Id;
                //timer id can't get
                return -1;
            }
        }
        public TaskStatus Status { get { return this.Task.Status; } }



        /// <summary>
        /// 無法RUN起來初始THREAD
        /// </summary>
        public void Run()
        {
            if (Task != null)
            {
                this.Task.Start();
            }
            else if (Thread != null)
            {
                Thread.Start();
            }
            else if (Timer != null)
            {
                Timer.Start();
            }
        }

        public bool Wait(int milliseconds) { return this.Task.Wait(milliseconds); }
        private void AddToMgr()
        {
            MacTaskMgr.Instance.Add(this);
        }




        #region IDisposable




        protected override void DisposeSelf()
        {
            if (this.CancelToken != null)
                this.CancelTokenSource.Cancel();

            if (this.Task != null)
            {
                if (this.Task.Status == TaskStatus.Running)
                {
                    try
                    {
                        using (this.Task)
                            if (!this.Task.Wait(3 * 1000))
                                Console.WriteLine("Cannot Cancel");
                    }
                    catch (OperationCanceledException) { }
                }
                this.Task = null;
            }

            if (this.CancelTokenSource != null)
                this.CancelTokenSource.Dispose();
            this.CancelTokenSource = null;

            base.DisposeSelf();
        }

        #endregion




        #region Static

        public static MacTask Run(MacEnumTaskPurpose purpose, string name, Action act)
        {
            MacTask mt = new MacTask(purpose.ToString() + "." + name, act);
            mt.Run();
            return mt;
        }

        public static MacTask Run(string purpose, Action act)
        {
            MacTask mt = new MacTask(purpose, act);
            mt.Run();
            return mt;
        }

        public static MacTask Run(MacEnumTaskPurpose name, Action act)
        {
            MacTask mt = new MacTask(name.ToString(), act);
            mt.Run();
            return mt;
        }


        public static MacTask RunLoopUntilCancel(MacEnumTaskPurpose purpose, Func<bool> funcIsContinue, int millisecond_delay = 0)
        {
            return RunLoopUntilCancel(purpose.ToString(), funcIsContinue, millisecond_delay);
        }
        public static MacTask RunLoopUntilCancel(string purpose, Func<bool> funcIsContinue, int delay_ms = 0)
        {
            var task = new MacTask(purpose);
            MvCancelTask.RunLoopUntilCancel(funcIsContinue, delay_ms);
            return task;
        }

        #endregion

    }
}
