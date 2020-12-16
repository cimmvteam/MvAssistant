using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Tasking
{
    public class MvaCancelTask : MvaTask
    {
        public CancellationTokenSource CancelTokenSource = new CancellationTokenSource();

        ~MvaCancelTask() { this.Dispose(false); }
        public CancellationToken CancelToken { get { return this.CancelTokenSource.Token; } }

        public static MvaCancelTask Run(Action<CancellationToken> act)
        {
            var task = new MvaCancelTask();
            var ct = task.CancelTokenSource.Token;
            task.Task = Task.Factory.StartNew(() =>
            {
                act(ct);
            }, ct);

            return task;
        }

        public static MvaCancelTask RunLoop(Func<bool> funcIsContinue, int delay_ms = 0)
        {
            var task = new MvaCancelTask();
            var ct = task.CancelTokenSource.Token;
            task.Task = Task.Factory.StartNew(() =>
            {
                while (!ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();
                    if (!funcIsContinue()) break;
                    if (delay_ms > 0) MvaSpinWait.SpinUntil(() => ct.IsCancellationRequested, delay_ms);
                }
            }, ct);

            return task;
        }

        public void Cancel() { this.CancelTokenSource.Cancel(); }
        #region IDisposable

        protected override void DisposeSelf()
        {
            this.CancelTokenSource.Cancel();
            this.Task.Wait(1000);
            base.DisposeSelf();
        }
        #endregion

    }
}
