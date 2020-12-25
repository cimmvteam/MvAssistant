using MvAssistant.v0_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Threading
{
    public class MvaTask : IDisposable
    {
        public string Name;
        public Task Task;
        public TaskStatus Status { get { return this.Task.Status; } }

        ~MvaTask() { this.Dispose(false); }

        public virtual int Id { get { return this.Task.Id; } }

        public static MvaTask Run(Action act)
        {
            var task = new MvaTask();
            task.Task = Task.Factory.StartNew(act);
            return task;
        }
        public static MvaTask Run(Action act, string taskName)
        {
            var task = new MvaTask();
            task.Name = taskName;
            task.Task = Task.Factory.StartNew(act);
            return task;
        }

        public bool IsEnd() { return this.Task == null ? true : this.Task.IsCompleted || this.Task.IsCanceled || this.Task.IsFaulted; }


        public void Start()
        {
            if (this.Task == null) throw new InvalidOperationException("Task尚未設定");
            this.Task.Start();
        }
        public bool Wait(int milliseconds) { return this.Task.Wait(milliseconds); }
        public void Wait() { this.Task.Wait(); }


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
            //預期Task是能在迴圈開始時, 自己確認是否終止
            //一般會以 disposed 和 IsCancellationRequested 作為持續的判斷
            try
            {
                if (this.Task != null)
                    MvaUtil.DisposeTask(this.Task);
            }
            catch (OperationCanceledException ex) { MvaLog.WarnNs(this, ex); }
            this.Task = null;
        }

        #endregion

    }
}
