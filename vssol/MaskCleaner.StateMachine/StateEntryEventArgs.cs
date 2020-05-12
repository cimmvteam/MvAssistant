using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaskCleaner.StateMachine
{
    public class StateEntryEventArgs : EventArgs
    {
        [Obsolete("改用StateJob")]
        public Func<bool> ConfirmResult = () => { return true; };

        [Obsolete("改用StateJob")]
        public Action DeclareSensor = new Action(() => { });

        [Obsolete("改用StateJob")]
        public List<TransitionEvaluation> EvalList = new List<TransitionEvaluation>(64);

        [Obsolete("改用StateJob")]
        public Action<Exception> ExceptionHandle = (ex) => { };

        /// <summary>
        /// return true 表示可以走下一步
        /// </summary>
        /// [Obsolete("改用StateJob")]
        public Func<bool> ExecuteMajorTaskWhenOnEntry = () => false;

        [Obsolete("改用StateJob")]
        public List<Func<bool>> GuardOfExecuteMajorTaskWhenOnEntry = new List<Func<bool>>(1);

        [Obsolete("改用StateJob")]
        public Action JudgeSpec = () => { };

        [Obsolete("改用StateJob")]
        public List<Action> JudgeSpecs = new List<Action>();

        public StateMachine Sender;
        public IStateParam TriggerStateParam;
        public StateEntryEventArgs()
        {
        }

        public StateEntryEventArgs(StateMachine sm)
        {
            this.Sender = sm;
            this.ExceptionHandle = (ex) => { sm.SmTriggerAlarmByException(ex); };
        }

        [Obsolete("改用StateJob")]
        public void RunEntry()
        {
           
            //注意Thread Safe, 目前一個State Machine只有一個執行緒, 暫時不會有衝突
            var taskCancelToken = new CancellationTokenSource();
            CancellationToken ct = taskCancelToken.Token;
            //常駐型Safety Check
            var safetyTask = Task.Run(() =>
            {

                while (!ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();

                    foreach (var judge in this.JudgeSpecs)
                        judge();

                    SpinWait.SpinUntil(() => false, 100);//Thread.Sleep(100);
                }

            }, ct);



            try
            {
                //執行主工作
                if (this.ExecuteMajorTaskWhenOnEntry())
                {
                    var isSuccess = true;
                    foreach (var g in this.GuardOfExecuteMajorTaskWhenOnEntry)
                        isSuccess &= g();

                    isSuccess &= this.ConfirmResult();

                    //Guard 確認全部OK
                    if (isSuccess)
                    {
                        //if (this.sm.CurrentState is AlterState)
                        //{
                        //    this.sm.SmTriggerToAlterNext(this.EvalList);
                        //}
                        //else
                        {
                            this.Sender.SmTriggerToSingleNext();
                        }
                    }

                }


            }
            catch (Exception ex) { this.ExceptionHandle(ex); }
            finally
            {
                taskCancelToken.Cancel();
                safetyTask.Wait(3 * 1000);
            }
        }




    }
}
