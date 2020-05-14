using MaskAutoCleaner.Alarm;
using MaskAutoCleaner.Msg;
using MaskAutoCleaner.Spec;
using MaskAutoCleaner.MqttLike;
using MaskAutoCleaner.Tasking;
using MvLib.TaskDispatch;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvLib.StateMachine;
using MvLib;
using MvLib.Logging;

namespace MaskAutoCleaner.StateJob
{
    /// <summary>
    /// Do original task defined in the State with same thread of trigger queue consumer
    /// </summary>
    public class StateJobHandler : IDisposable
    {

        public int IndexOfExecMajorObj = 0;
        public StateJobJudgeSpec JudgeSpecJobs = new StateJobJudgeSpec();
        public MacMachineBase Machine;
        public StateJobBasic MajorJobs = new StateJobBasic();
        public StateJobBasic PeriodJobs = new StateJobBasic();
        public StateJobBasic PostJobs = new StateJobBasic();
        public StateJobBasic PreJobs = new StateJobBasic();
        public List<MqttSubscriber> Subscribers = new List<MqttSubscriber>(16);
        volatile bool IsCancel = false;
        MacTask statePeriodTask;
        public StateJobHandler(MacMachineBase machine)
        {
            this.Machine = machine;
        }

        ~StateJobHandler() { this.Dispose(false); }

        MacMachineMediater eqpMediater { get { return this.Machine.MachineMediater; } }
        MqttBroker sensorTopicContext { get { return this.eqpMediater.MqttBroker; } }
        public void Cancel()
        {
            this.IsCancel = true;
            if (this.statePeriodTask != null)
            {
                this.statePeriodTask.Cancel();
                if (!this.statePeriodTask.Wait(10 * 1000))
                {
                    //10秒一定要終止工作
                    throw new MacAlarmException(EnumAlarmId.StateJobWorkerCannotAbort);
                }
            }

        }

        public void Clear()
        {
            this.Cancel();
            this.MajorJobs.Clear();
            this.PeriodJobs.Clear();
            this.PreJobs.Clear();
            this.PostJobs.Clear();
            this.JudgeSpecJobs.Clear();
            this.IndexOfExecMajorObj = 0;


            this.ClearSubscribers();

            this.IsCancel = false;
        }

        public void ClearSubscribers()
        {
            foreach (var subscriber in this.Subscribers)
                this.sensorTopicContext.TopicUnsubscribe(subscriber);

            this.Subscribers.Clear();
        }

        public void ExceptionProc(Exception ex)
        {
            //TODO:
            //Allow EE to do first aid
            //Keep tracable (what state cause, which sensor) record for EE/CIM diagnosis -> 結合UI
            //Recover to normal
            //Ensure all software control mechansim continued after recover without any lost of control focus
            //       state machine job trigger
            //       recipe step move on
            //       TAP


            var macex = MacAlarmException.GetOrCreate(ex);
            try
            {
                //terminate useless thread
                this.Cancel();//出例外 -> 終止作業

                //transfer info to duty object (AlarmMgr, EQPsm, TAP)
                this.Machine.ReportAlarm(macex);
            }
            catch (Exception)
            {
                //TODO: 需可記錄多個Alarm
                //TODO: 目前無Alarm Queue
            }
            finally
            {
                MvLog.Write(macex, MvLoggerEnumLevel.Warn);
                //transfer State Graphic to corresponding ErrorState
                this.Machine.SmTriggerAlarmByAlarmId(macex.AlarmId);
            }


        }

        public List<EnumAlarmId> JudgeSpecAlarms()
        {
            var list = new List<EnumAlarmId>();
            foreach (StateJobJudgeSpec js in this.JudgeSpecJobs)
            {
                if (js.AlarmLevel >= js.NeedThrowLevel)
                    list.Add(js.AlarmId);
            }
            return list;
        }

        public void RunInStateEntry()
        {

            try
            {

                if (!this.IsCancel)
                {
                    if (!this.PreJob())
                        throw new MacAlarmException(EnumAlarmId.StateJobPreWorkerFail);
                }


                for (; IndexOfExecMajorObj < this.MajorJobs.Count; IndexOfExecMajorObj++)
                {
                    var alarms = this.JudgeSpecAlarms();
                    if (alarms.Count > 0) throw new MacAlarmException(alarms[0]);

                    if (this.IsCancel) break;

                    var job = this.MajorJobs[IndexOfExecMajorObj];
                    if (!job.Execute(this).IsSuccess)
                        throw new MacAlarmException(EnumAlarmId.StateJobMajorWorkerFail);

                }



                if (!this.IsCancel)
                {
                    if (!this.PostJob())
                        throw new MacAlarmException(EnumAlarmId.StateJobPostWorkerFail);
                }


            }
            catch (Exception ex)
            {
                this.ExceptionProc(ex);
            }
        }

        public void StopInStateExit()
        {
            this.Cancel();
        }





        public MqttSubscriber Subscribe(EnumMqttTopicId topicId, Action<MqttBroadcastInfo> info)
        {
            var subcriber = this.Machine.MqttTopic_Subscribe(topicId, info);
            this.Subscribers.Add(subcriber);
            return subcriber;
        }




        bool PostJob()
        {
            var result = true;
            foreach (var job in this.PostJobs)
                result &= job.Execute(this).IsSuccess;
            return result;
        }
        bool PreJob()
        {
            var result = true;
            foreach (var job in this.PreJobs)
                result &= job.Execute(this).IsSuccess;
            return result;
        }







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
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
                this.DisposeManaged();
            }

            // Free any unmanaged objects here.
            //
            this.DisposeUnmanaged();

            this.DisposeSelf();

            disposed = true;
        }



        void DisposeManaged()
        {
        }

        void DisposeSelf()
        {
            this.Cancel();
            this.Clear();
        }

        void DisposeUnmanaged()
        {

        }
        #endregion



    }
}