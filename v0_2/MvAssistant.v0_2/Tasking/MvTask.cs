using MvAssistant.v0_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Tasking
{
    public class MvTask : IDisposable
    {
        public string Name;
        public Task Task;


        ~MvTask() { this.Dispose(false); }

        public virtual int Id { get { return this.Task.Id; } }

        public static MvTask Run(Action act)
        {
            var task = new MvTask();
            task.Task = Task.Factory.StartNew(act);
            return task;
        }

        public bool IsEnd() { return this.Task == null ? true :  this.Task.IsCompleted || this.Task.IsCanceled || this.Task.IsFaulted; }


        public void Start()
        {
            if (this.Task == null) throw new InvalidOperationException("Task尚未設定");
            this.Task.Start();
        }
        public bool Wait(int milliseconds) { return this.Task.Wait(milliseconds); }


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
            if (this.Task != null)
            {
                this.Task.Dispose();
            }

            if (this.Task != null)
            {
                //預期Task是能在迴圈開始時, 自己確認是否終止
                //一般會以 disposed 和 IsCancellationRequested 作為持續的判斷
                try
                {
                    using (this.Task)
                        if (!this.Task.Wait(3 * 1000))
                        {
                            if (this.Task.Status < TaskStatus.Running)
                                MvLog.WarnNs(this, "MvTask is no start");
                            else if(this.Task.Status ==  TaskStatus.Running)
                                MvLog.WarnNs(this, "MvTask can not cancel");
                        }
                }
                catch (OperationCanceledException) { }
                this.Task = null;
            }



        }


        #endregion

    }
}
