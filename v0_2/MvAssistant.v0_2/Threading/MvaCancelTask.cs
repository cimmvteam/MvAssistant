using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Threading
{
    public class MvaCancelTask : MvaTask
    {
        public CancellationTokenSource CancelTokenSource = new CancellationTokenSource();

        ~MvaCancelTask() { this.Dispose(false); }
        public CancellationToken CancelToken { get { return this.CancelTokenSource.Token; } }

        public static MvaCancelTask Run(Action<CancellationToken> act, string name = null)
        {
            var task = new MvaCancelTask();
            task.Name = name;
            var ct = task.CancelTokenSource.Token;
            task.Task = Task.Factory.StartNew(() =>
            {
                act(ct);
            }, ct);

            return task;
        }


        /// <summary>
        /// Run loop的過程不會try/catch.
        /// 邏輯上來講, 使用者應該要take care exception
        /// </summary>
        /// <param name="funcIsContinue"></param>
        /// <param name="delay_ms"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MvaCancelTask RunLoop(Func<bool> funcIsContinue, int delay_ms = 0, string name = null)
        {
            var task = new MvaCancelTask();
            task.Name = name;
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
        public static MvaCancelTask RunLoop(Func<bool> funcIsContinue, string name, int delay_ms = 0)
        {
            var task = new MvaCancelTask();
            task.Name = name;
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
            if (this.Task.Status < TaskStatus.RanToCompletion) this.Task.Wait(1000);
            base.DisposeSelf();
        }
        #endregion

    }
}
