using MaskAutoCleaner.Machine;
using MaskAutoCleaner.MqttLike;
using MaskAutoCleaner.Msg;
using MaskAutoCleaner.Msg.PrescribedJobNotify;
using MaskAutoCleaner.Msg.PrescribedSecs;
using MaskAutoCleaner.Spec;
using MaskAutoCleaner.StateJob;
using MaskAutoCleaner.StateOp;
using MaskAutoCleaner.Tasking;
using MvLib;
using MvLib.Logging;
using MvLib.StateMachine;
using MvLib.StateMachine.SmExp;
using MvLib.Tasking;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace MaskAutoCleaner
{
    public abstract class MacMachineBase : MvLib.StateMachine.StateMachine
        , IMsgProcessable
        , IStateMachineResettable
        , IPauseControllable
        , IStopControllable
        , IDisposable
    {

        public bool IsNeedPause = false;
        public MacMachineMediater MachineMediater;
        public MacMachineBase ParentMachine;
        public Task SensorAlarmControlTask;
        public EnumMachineStatus Status = EnumMachineStatus.Created;
        protected ConcurrentQueue<MsgBase> _msgQueue = new ConcurrentQueue<MsgBase>();
        //for ExecRepeatMajor, 建議不要在其它地方取Queue, 避免衝突
        protected MvSpinWait _mvSpinWaitMajor = new MvSpinWait(1000, 100);

        protected StateJobHandler _stateJobHdl;
        protected IStateParam _stateParamMajor;
        protected MacTask _taskMarjoJob;
        /// <summary>
        /// State Machine 在某個State的計時器
        /// </summary>
        protected System.Timers.Timer TimerStateTimeout;
        public MacMachineBase()
        {
            TimerStateTimeout = new System.Timers.Timer();
            TimerStateTimeout.AutoReset = false;
        }

        public IMvLoggable Logger { get { return this.MachineMediater.Logger; } }
        public bool CheckMachineChildInStopMode(MacMachineBase sm)
        {
            foreach (KeyValuePair<string, StateMachine> s in sm.SubStateMachines)
            {
                if (s.Value is MacMachineBase)
                {
                    var smb = ((MacMachineBase)s.Value);
                    if (!smb.IsAutoRunStateMachine)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void DistinctEnqueue(IStateParam param)
        {
            if (this.SmTriggerQueue.Where(x => x.TransName == param.TransName).Count() > 0) return;
            this.SmTriggerQueue.Enqueue(param);
        }


        public override State NewState(string name, StateType st = StateType.Normal)
        {
            State state;
            if (st == StateType.Exception) { state = new MacExceptionState(name); }
            else { state = new MacState(name); }
            AddState(state);
            return state;
        }
        public MacExceptionState NewState(string name, IStateErrorInfo ex)
        {
            MacExceptionState state = new MacExceptionState(name, ex);
            AddState(state);
            return state;
        }

        public void SmStateMachineReboot()
        {
            throw new NotImplementedException();
        }


        #region Machine Log snap shot
        public void SnapShot()
        {

            var folder = Path.Combine(@"UserData\LOG\", this.ID);
            var di = new DirectoryInfo(folder);
            if (!di.Exists) di.Create();
            var file = string.Format("{0}_d{1}.xml", this.ID, DateTime.Now.ToString("yyyyMMdd"));
            string filePath = Path.Combine(folder, file);
            try
            {
                //超過一秒無法進入, 不強制Snapshot, 避免機台異常
                if (!Monitor.TryEnter(this, 1000)) { return; }
                SnapShot(filePath);
            }
            finally { Monitor.Exit(this); }
        }

        public void SnapShot(string filePath)
        {

            var timeFormat = "yyyy/MM/dd HH:mm:ss.ffff zzz";
            FileInfo fi = new FileInfo(filePath);
            try
            {
                var doc = new XmlDocument();
                XmlElement xSnap;
                if (fi.Exists)
                {
                    doc.Load(filePath);
                    xSnap = doc.DocumentElement;
                }
                else
                {
                    xSnap = doc.CreateElement("SnapShot");
                    doc.AppendChild(xSnap);
                }
                XmlElement xState = doc.CreateElement("State");
                xSnap.AppendChild(xState);
                XmlElement xTime = doc.CreateElement("Time");
                xTime.InnerText = DateTime.Now.ToString(timeFormat);
                xState.AppendChild(xTime);
                XmlElement xPer = doc.CreateElement("Proformance");
                var per = Process.GetCurrentProcess();
                xPer.InnerText = per.WorkingSet64.ToString();
                xState.AppendChild(xPer);
                XmlElement xPro = doc.CreateElement("ProcessID");
                var pro = Process.GetCurrentProcess().Id;
                xPro.InnerText = pro.ToString();
                xState.AppendChild(xPro);
                XmlElement xThr = doc.CreateElement("ThreadID");
                var thr = Thread.CurrentThread.ManagedThreadId;
                xThr.InnerText = thr.ToString();
                xState.AppendChild(xThr);
                XmlElement xStateMachine = doc.CreateElement("StateMachine");
                xState.AppendChild(xStateMachine);
                xStateMachine.InnerText = this.GetType().ToString();
                XmlElement xStateID = doc.CreateElement("ID");
                xState.AppendChild(xStateID);
                xStateID.InnerText = ID;
                XmlElement xCur = doc.CreateElement("CurrentState");
                xCur.InnerText = CurrentState.StateName.ToString();
                xState.AppendChild(xCur);
                //路徑確認該資料夾及檔案是否存在
                DirectoryInfo di = fi.Directory;
                if (di.Exists != true)
                {
                    di.Create();
                }
                 doc.Save(filePath);
            }
            catch (Exception ex)
            {
                fi.Delete();
            }
        }
        #endregion

        #region common function
        public void sOnEvtWaitThreeSec(object sender, StateEntryEventArgs e)
        {
            Thread.Sleep(100);
        }
        #endregion

        #region Get Sub Machine
        public MacMachineBase this[string key] { get { return this.SubStateMachines[key] as MacMachineBase; } set { this.SubStateMachines[key] = value; } }
        public MacMachineBase this[Enum key] { get { return this.SubStateMachines[key.ToString()] as MacMachineBase; } set { this.SubStateMachines[key.ToString()] = value; } }


        public Type GetAssemblyTypeById(string id) { return this.MachineMediater.GetAssemblyTypeById(id); }

        public T GetSubMachine<T>() where T : MacMachineBase { return this.SubStateMachines.Where(x => x.Value is T).FirstOrDefault().Value as T; }
        public T GetSubMachine<T>(string key) where T : MacMachineBase { return this.SubStateMachines[key] as T; }
        public T GetSubMachine<T>(Enum key) where T : MacMachineBase { return this.SubStateMachines[key.ToString()] as T; }
        public MacMachineBase GetSubMachine(Enum key) { return this.SubStateMachines[key.ToString()] as MacMachineBase; }

        public bool ContainSubMachine(Enum key) { return this.ContainSubMachine(key.ToString()); }
        public bool ContainSubMachine(string key) { return this.SubStateMachines.ContainsKey(key); }

        public void ConfirmOnlyOneTransition()
        {
            //20190917 由於有兩種控制方法，嘗試確保單一TRANSITION會被執行
            IStateParam temp;
            while (this.CountOfTriggerQueue() > 0)
            {
                this.SmTriggerQueue.TryDequeue(out temp);
            }
        }

        #endregion
        
        #region Machine Operation
        public virtual void Close()
        {
            //throw new NotImplementedException("Implementation by yourself");
        }
        public void CloseRcs()
        {
            foreach (var sm in this.SubStateMachines)
            {
                try
                {
                    var machine = sm.Value as MacMachineBase;
                    if (machine == null) continue;
                    machine.CloseRcs();
                }
                catch (Exception ex) { MvLog.Write(ex, MvLib.Logging.MvLoggerEnumLevel.Warn); }
            }
            try
            {
                this.Status = EnumMachineStatus.IsRequestCancel;
                if (this._taskMarjoJob != null)
                {
                    this._taskMarjoJob.Wait(3 * 1000);
                }
                this.Close();
            }
            catch (Exception ex) { MvLog.Write(ex, MvLib.Logging.MvLoggerEnumLevel.Warn); }

        }

        public virtual void RunStateMachineRcs()
        {

            Debug.Assert(this.MachineMediater != null, "需要Mediater");//執行前必須給定EqpMediate

            this.Logger.Info("{0} Loading...", this.ID);

            //--- Iniitalized ---
            this.Status = EnumMachineStatus.Initialized;



            this.Load();//給各assembly需要load物件的, 包含物件載入&初始化
            this.LoadMqtt();
            this.LoadStateMachine();
            this.LoadCurrentState();
            this.MessageHandler = this.MachineMediater;//State machine MessageHandler 目前由 MachineMediate 實作

            this._stateJobHdl = new StateJobHandler(this);




            //--- Loaded ---
            this.Status = EnumMachineStatus.Loaded;


            //在類別裡流程控制Method, 其父類別會逐一對其Sub Device執行對應Method
            foreach (var row in this.SubStateMachines)
            {
                var machine = row.Value as MacMachineBase;
                if (machine == null) continue;
                machine.ParentMachine = this;
                machine.MachineMediater = this.MachineMediater;
                machine.RunStateMachineRcsAsyn();
            }


            this.IsAutoRunStateMachine = true;
            this.Status = EnumMachineStatus.Running;
            this.ExecRepeatTemplate(this._mvSpinWaitMajor, ref this._stateParamMajor, this.SmTriggerQueue);


        }

        public virtual void RunStateMachineRcsAsyn()
        {
            if (this._taskMarjoJob == null)
            {
                this._taskMarjoJob = MacTask.Run(MacEnumTaskPurpose.MachineMajorJob, this.ID, this.RunStateMachineRcs);
            }
        }

        public virtual void SetMachineRunningModeRcs(MacMachineBase sm, EnumMachineRunningMode emrm)
        {
            foreach (KeyValuePair<string, StateMachine> s in sm.SubStateMachines)
            {
                if (s.Value is MacMachineBase)
                {
                    var smb = ((MacMachineBase)s.Value);
                    smb.SetMachineRunningMode(emrm);
                    smb.SetMachineRunningModeRcs(smb, emrm);
                }
            }
        }


        /// <summary>
        /// 將會重複執行
        /// 主要處理 msgQueue, 也可處理其它事情
        /// 接收msg處理要求後, 應如何處理訊息 (State Machine 需進行特定狀態才可處理)
        /// </summary>
        protected virtual MacExecuteRepeatResult ExecMajorJob() { return new MacExecuteRepeatResult(); }
        protected virtual void ExecRepeatTemplate(MvSpinWait mvSpinWait, ref IStateParam sp, ConcurrentQueue<IStateParam> queue)
        {
            while (!this.disposed && this.Status == EnumMachineStatus.Running)
            {
                var result = new MacExecuteRepeatResult();
                try
                {
                    mvSpinWait.WaitUntilWithLast(() => queue.Count > 0 || this._msgQueue.Count > 0);
                    SpinWait.SpinUntil(() => !this.IsNeedPause);//沒被要求暫停, 才能通過


                    //State Machine Consumer / 若設定為Auto Run, 自動取Queue
                    if (this.IsAutoRunStateMachine)
                    {
                        sp = null;
                        if (queue.Count > 0)
                            if (!queue.TryDequeue(out sp)) continue;
                        if (sp != null)
                            this.SmProcTransition(sp);
                    }


                    result = this.ExecMajorJob();
                    if (!result.IsKeepRepeat)
                        break;


                }
                catch (Exception ex) { this.Logger.Write(ex); }
                finally { mvSpinWait.WaitMillisecondsMin = result.NextExecuteInterval; }
            }
        }


        protected virtual int Load() { return 0; }
        protected virtual void LoadCurrentState() { throw new NotImplementedException("Please implement by yourself"); }
        protected virtual void LoadMqtt() { }
        protected virtual void LoadStateMachine() { throw new NotImplementedException("Please implement by yourself"); }

        #endregion

        #region MQTT
        public MqttTopic MqttTopic_Get(EnumMqttTopicId topicId) { return this.MachineMediater.MqttBroker.Topics.Where(x => x.Key == topicId).FirstOrDefault().Value; }

        public MqttTopic MqttTopic_Publish<T>(EnumMqttTopicId topicId, T paramObj, Func<T, MqttSignal> readSensor, bool hasNoUpdateIfNoSubscribers = false)
        {
            var topic = this.MachineMediater.MqttBroker.TopicPublish(topicId, this, () => readSensor(paramObj));
            topic.HasNoUpdateIfNoSubScribers = hasNoUpdateIfNoSubscribers;
            return topic;
        }

        public MqttTopic MqttTopic_Publish(EnumMqttTopicId topicId, Func<MqttSignal> readSensor, bool hasNoUpdateIfNoSubscribers = false)
        {
            var topic = this.MachineMediater.MqttBroker.TopicPublish(topicId, this, readSensor);
            topic.HasNoUpdateIfNoSubScribers = hasNoUpdateIfNoSubscribers;
            return topic;
        }

        public MqttSubscriber MqttTopic_Subscribe(EnumMqttTopicId topicId, Action<MqttBroadcastInfo> info)
        {
            var subscriber = new MqttSubscriber()
            {
                Subscriber = this,
                Broadcast = info,
            };
            this.MachineMediater.MqttBroker.TopicSubscribe(topicId, subscriber);

            return subscriber;
        }

        public void MqttTopic_Unscribe(MqttSubscriber subscriber)
        {
            this.MachineMediater.MqttBroker.TopicUnsubscribe(subscriber);
        }

        #endregion

        #region Message Process


        public void ReportAlarm(EnumAlarmId alarmId, Exception ex = null)
        {
            var alarm = MsgFactory.CreateAlarm(this, alarmId);
            alarm.Exception = ex;
            this.MachineMediater.ProcAlarm(this, alarm);
        }

        public void ReportAlarm(Exception ex) { this.MachineMediater.ProcAlarm(this, ex); }

        public void ReportAlarmIfOoc(MacSpecItem spec, MqttSignal val, EnumAlarmId alarmId)
        {
            var level = spec.CheckLevel(val);
            if (level == EnumAlarmLevel.None) return;
            var alarm = MsgFactory.CreateAlarm(this, alarmId, level);
            this.MachineMediater.ProcAlarm(this, alarm);
        }
        public void BroadcastJobNotify(EnumJobNotify jn, PrescribedJobNotifyBase jobNotify = null)
        {
            var msg = MsgFactory.CreateJobNotify(this, jn, jobNotify);
            this.MachineMediater.ProcJobNotify(this, msg);
        }


        public void ReportSecs(MsgSecs msgSecs)
        {
            this.MachineMediater.ProcSecs(this, msgSecs);
        }
        public void ReportSecsCeid(EnumCeid ceid, PrescribedSecsBase secs)
        {
            var msg = MsgFactory.CreateSecsCeid(this, ceid);
            var s6f11 = secs as S6F11;
            if (s6f11 != null)
            {
                if (string.IsNullOrEmpty(s6f11.MessageName)) s6f11.MessageName = ceid.ToString();
                s6f11.CommonUnitId = this.ID;
            }
            msg.PrescribedSecs = secs;
            this.MachineMediater.ProcSecs(this, msg);
        }

        /// <summary>
        /// 訊息接收盡量不要擔誤到其它執行緒運行
        /// 盡快Enqueue後離開
        /// 工作執行交給 ExecRepeatMajor 或 State Machine
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual int RequestProcMsg(MsgBase msg)
        {
            throw new NotImplementedException("無實作接受訊息");//目前暫訂: 未被實作者實作的訊息, 卻被要求處理, 此時應警示開發者
        }

        #endregion

        #region IPauseControllable / IStopControllable


        public virtual void SetMachineRunningMode(EnumMachineRunningMode emrm)
        {
            switch (emrm)
            {
                case EnumMachineRunningMode.Assist:
                    SetStateMachineFatel();
                    break;
                case EnumMachineRunningMode.Pause:
                    if (this.IsNeedPause)
                        SetStateMAchinePause();
                    break;
                case EnumMachineRunningMode.AutoRun:
                    SetStateMachineRun();
                    break;
                default:
                    throw new Exception(this.ID + ":Not implement this running mode");
            }
        }

        public virtual void SetStateMachineFatel()
        {
            IsAutoRunStateMachine = false;
            if (TimerStateTimeout.Enabled)
            {
                TimerStateTimeout.Enabled = false;
            }
        }

        public virtual void SetStateMAchinePause()
        {
            IsAutoRunStateMachine = false;
            if (TimerStateTimeout.Enabled)
            {
                TimerStateTimeout.Enabled = false;
            }
        }

        public virtual void SetStateMAchineResume()
        {
            throw new NotImplementedException();
        }

        public virtual void SetStateMachineRun()
        {
            IsAutoRunStateMachine = true;
            if (!TimerStateTimeout.Enabled)
            {
                TimerStateTimeout.Enabled = true;
            }
        }
        #endregion

        #region Event

        event EventHandler<EventArgs> assistEvent;

        public void DoAssistEvent()
        {
            if (assistEvent != null)
            {
                assistEvent(this, new EventArgs());
            }
        }

        #endregion

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
                this.DisposeManaged();
            }

            // Free any unmanaged objects here.
            //
            this.DisposeUnmanaged();

            this.DisposeSelf();

            disposed = true;
        }



        protected virtual void DisposeManaged()
        {
        }

        protected virtual void DisposeSelf()
        {
            this.CloseRcs();
        }

        protected virtual void DisposeUnmanaged()
        {

        }
        #endregion
    }
}
