using MvAssistant;
using MvAssistant.Logging;
using MaskAutoCleaner.v1_0.StateMachineAlpha.SmExp;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
namespace MaskAutoCleaner.v1_0.StateMachineAlpha
{
    /// <summary>
    /// 提供State Machine容器實做&與HAL溝通處
    /// </summary>
    public abstract class StateMachine : IStateMachinePersist
    {
        public List<TransitionEvaluation> AlterList = new List<TransitionEvaluation>(16);
        public bool FlageNormalStatus;
        /// <summary>
        /// State Machine 自動取Queue 判定是否要Trigger
        /// 設為false會暫止State Machine運作
        /// </summary>
        public bool IsAutoRunStateMachine = false;
        public string Name = "";
        public Dictionary<string, StateMachine> SubStateMachines = new Dictionary<string, StateMachine>();

        protected CancellationTokenSource mCts;
        protected Dictionary<string, State> states = new Dictionary<string, State>();
        protected List<Transition> transitions = new List<Transition>();

        /// <summary>
        /// please call this function when initial and register states and transitions of their state machine 
        /// hardware exception is a state 
        /// </summary>
        public StateMachine()
        {
            FlageNormalStatus = true;
            Name = this.GetType().ToString();
            mCts = new CancellationTokenSource();
            SmTriggerQueue = new ConcurrentQueue<IStateParam>();
            SmAlarmQueue = new ConcurrentQueue<IStateParam>();
            //ExceptionState NoCurState = new ExceptionState("NotCurrentStateExcetion");
            //SetupState(NoCurState);
            //ExceptionState FatelState = new ExceptionState("FatelExcetion");
            //SetupState(FatelState);
        }

        public enum StateType { Normal, Exception, Alter };

        public enum TransitionType { Normal, Deferral, Internal };

        public State CurrentState { get; set; }
        public Transition CurrentTimeoutTranisition { get; set; }
        public string ID { get; set; }
        public IStateMachineAlarmHandler MessageHandler { get; set; }
        public ConcurrentQueue<IStateParam> SmAlarmQueue { get; set; }
        protected ConcurrentQueue<IStateParam> SmTriggerQueue { get; set; }


        public bool TriggerQueueIsEmpty { get { return this.SmTriggerQueue.IsEmpty; } }
        public bool TryDequeueTrigger(out IStateParam param) { return this.SmTriggerQueue.TryDequeue(out param); }
        public int CountOfTriggerQueue() { return this.SmTriggerQueue.Count; }

        //對應到MANIFEST
        public Dictionary<string, State> States { get { return states; } }

        public bool ChangeState(State s)
        {
            bool trulyChange = false;
            if (FlageNormalStatus)
            {
                if (!s.Equals(CurrentState))
                {
                    trulyChange = true;
                    MvLog.Info(ID + " change state from {0} to {1}", this.CurrentState.StateName, s.StateName);
                    CurrentState = s;
                }
            }
            return trulyChange;
        }

        public virtual bool CheckFinished()
        {
            return true;
            //throw new NotImplementedException();
        }

        public bool IsInCurrentState(State fromState)
        {
            return (fromState == CurrentState);
        }

        //factory mode
        public virtual void CreateMachine(string document)
        {
            XmlDocument doc = Load(document);
            XmlNode sm = doc.FirstChild;
            string smClassName = sm.Attributes["Name"].Value;
            XmlNodeList tempNodes = sm.SelectNodes("State");
            foreach (XmlNode stateNode in tempNodes)
            {
                State s = new State(stateNode.Attributes["Name"].Value);
                XmlNodeList sEntries = stateNode.SelectNodes("Entry");
                if (sEntries != null)
                {
                    foreach (XmlNode sEntry in sEntries)
                    {
                        string className = sEntry.Attributes["ClassName"].Value;
                        string eventName = sEntry.Attributes["EventName"].Value;
                        EventInfo eventInfo = s.GetType().GetEvent("OnEntry");
                        MethodInfo minfo = this.GetType().GetMethod(eventName);
                        Delegate handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, minfo);
                        eventInfo.AddEventHandler(s, handler);
                    }
                }
                sEntries = stateNode.SelectNodes("Exit");
                if (sEntries != null)
                {
                    foreach (XmlNode sEntry in sEntries)
                    {
                        string className = sEntry.Attributes["ClassName"].Value;
                        string eventName = sEntry.Attributes["EventName"].Value;
                        Type type = this.GetType();
                        MethodInfo mi = type.GetMethod(eventName, BindingFlags.NonPublic);
                        EventInfo eventInfo = s.GetType().GetEvent("OnExit");
                        MethodInfo minfo = this.GetType().GetMethod(eventName);
                        Delegate handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, minfo);
                        eventInfo.AddEventHandler(s, handler);
                    }
                }
                AddState(s);
            }
            tempNodes = sm.SelectNodes("Transition");
            foreach (XmlNode transitionNode in tempNodes)
            {
                State FromState = states[transitionNode.SelectSingleNode("FromState").Attributes["Name"].Value];
                State ToState = states[transitionNode.SelectSingleNode("ToState").Attributes["Name"].Value];
                Transition s = new Transition(FromState, ToState,
                    this.GetType().GetMethod(transitionNode.SelectSingleNode("Guard").Attributes["Name"].Value),
                    this.GetType().GetMethod(transitionNode.SelectSingleNode("Action").Attributes["Name"].Value),
                    transitionNode.Attributes["Name"].Value);
                AddTransition(s);
            }
            XmlNode xnIdle = sm.SelectSingleNode("IdleState");
            CurrentState = states[xnIdle.Attributes["Name"].Value];
            //    Transition initialize = new Transition(raInitial, raIdle, PassGuard, StateMachineInitial, RecipeAgentEnumTransition.SMInitialize);
            //    Object XmlDocument = null;//XML文件
            //    initialize.RunTrigger(XmlDocument, this);
        }
        public void DoTimeoutEvent()
        {
            this.SmTriggerByName(CurrentTimeoutTranisition.TransName);
        }

        //部分exception state 的 transition 依靠動態生成
        public Transition GetDynamicExceptionTransition(State cur, EnumStateMachineMsgType type, string param = "NoUse")
        {
            Transition excepTrans;
            switch (type)
            {
                case EnumStateMachineMsgType.NotCurrentStateTransition:
                    if (transitions.Find(a => a.TransName.Equals(cur.StateName + "IsNotCurrentStateTransition")) != null)
                    {
                        excepTrans = transitions.Find(a => a.TransName.Equals(cur.StateName + "IsNotCurrentStateTransition"));
                    }
                    else
                    {
                        excepTrans = NewTransition(cur, states["NotCurrentStateExcetion"], PassGuard, LetNormalStateChangeDie, cur.StateName + "IsNotCurrentStateTransition");
                    }
                    break;
                case EnumStateMachineMsgType.StateMachineFatel:
                    if (transitions.Find(a => a.TransName.Equals(param + "GetFatelTransitionFrom" + cur.StateName)) != null)
                    {
                        excepTrans = transitions.Find(a => a.TransName.Equals(param + "GetFatelTransitionFrom" + cur.StateName));
                    }
                    else
                    {
                        excepTrans = NewTransition(cur, states["FatelExcetion"], PassGuard, LetNormalStateChangeDie, param + "GetFatelTransitionFrom" + cur.StateName);
                    }
                    break;
                default:
                    throw new Exception(this.ID + " : no implement this transition");
            }
            return excepTrans;
        }

        //當STATE MACHINE進入EXCEPTION時強制NORMAL的STATE CHANGE FUNCTION失效
        public void LetNormalStateChangeDie()
        {
            FlageNormalStatus = false;
        }

        public virtual XmlDocument Load(string path)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                return doc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw ex;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="alarmEnum"></param>
        /// <param name="alarmMsg">Alarm ID or Message</param>
        public void PopupAlarm(StateMachine sender, Enum alarmEnum, string alarmMsg)
        {
            this.MessageHandler.ProcAlarm(sender, alarmEnum, alarmMsg);
        }

        public List<Transition> QueryCurrentTransition(string type)
        {
            if (type == "Fatel")
            {
                List<Transition> s = CurrentState.TransitionsInState.FindAll(a => a.TransName.Contains("Fatel"));
                return s;
            }
            return null;
        }

        public virtual void SaveDeferralEvent(IStateParam param)
        {
            //throw new NotImplementedException("DeferralEvent not implement");
            this.SmTriggerByParam(param);
        }

        public void SaveStateMachine(string file)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlElement xStateMachine = doc.CreateElement("StateMachine");
                doc.AppendChild(xStateMachine);
                xStateMachine.SetAttribute("Name", this.Name);
                xStateMachine.SetAttribute("Class", this.GetType().ToString());
                //匯出STATE 狀態
                foreach (KeyValuePair<string, State> s in states)
                {
                    s.Value.GetElements(ref doc, ref xStateMachine);
                }
                //匯出Transition 狀態
                //foreach (KeyValuePair<Enum, Transition> t in transitions)
                //{
                //    t.Value.GetElements(ref doc, ref xStateMachine);
                //}
                foreach (Transition t in transitions)
                {
                    t.GetElements(ref doc, ref xStateMachine);
                }
                doc.Save(file);
            }
            catch (Exception ex)
            {
                MvLog.Write(ex, MvLoggerEnumLevel.Warn);
            }
        }

        public void SetCurrentTimeoutTranisition(Transition timeoutExecuteTransition)
        {
            CurrentTimeoutTranisition = timeoutExecuteTransition;
        }

        /// <summary>
        /// 設定一個state 的TIME OUT event 數字為秒數
        /// </summary>
        /// <param name="fromState"></param>
        /// <param name="timeoutTrans"></param>
        /// <param name="second"></param>
        public void SetupTimeoutConfig(Transition timeoutTrans, int second)
        {
            timeoutTrans.ToState.TimeoutLimit = second;
            timeoutTrans.ToState.timeoutExecuteTransition = timeoutTrans;
        }



        #region Request/Trigger State Transfer

        public virtual int SmTriggerAlarmByAlarmId(Enum alarmID)
        {
            lock (this)
            {
                List<string> list = new List<string>();
                var queryTranEx = from row in this.CurrentState.TransitionsInState
                                  where row.ToState is ExceptionState
                                  select new { tran = row, exs = row.ToState as ExceptionState };
                var queryFilter = from row in queryTranEx
                                  where row.exs.ReasonOfState != null
                                  && row.exs.ReasonOfState.StateAlarmId == alarmID
                                  select row.tran;
                list.AddRange(queryFilter.Select(x => x.TransName).Distinct());
                if (list.Count == 0)
                {
                    throw new StateMachineAlterFailException(this.ID + " : No find next trigger in this state");
                }
                else if (list.Count > 1)
                {
                    throw new StateMachineAlterFailException(this.ID + " : more than one trigger in this state");
                }
                else
                {
                    this.SmTriggerByName(list[0]);
                }
                return 0;
            }
        }
        public virtual int SmTriggerAlarmByException(Exception ex)
        {
            lock (this)
            {
                List<string> list = new List<string>();
                var queryTranEx = from row in this.CurrentState.TransitionsInState
                                  where row.ToState is ExceptionState
                                  select new { tran = row, exs = row.ToState as ExceptionState };
                var queryFilter = from row in queryTranEx
                                  where row.exs.ReasonOfState != null
                                  && row.exs.ReasonOfState.GetType() == ex.GetType()
                                  select row.tran;
                list.AddRange(queryFilter.Select(x => x.TransName).Distinct());
                if (list.Count == 0)
                {
                    var msg = string.Format("Machine {0}: No find next trigger in the state {1}", this.ID, this.CurrentState.StateName);
                    throw new StateMachineAlterFailException(msg, ex);
                }
                else if (list.Count > 1)
                {
                    throw new StateMachineAlterFailException(this.ID + " : more than one trigger in this state", ex);
                }
                else
                {
                    this.SmTriggerByName(list[0]);
                }
                return 0;
            }
        }
        public virtual int SmTriggerAlarmByTransition(string tranName)
        {
            lock (this)
            {
                List<string> list = new List<string>();
                foreach (Transition t in CurrentState.TransitionsInState.FindAll(t => (t.ToState is ExceptionState) && (t.TransName == tranName)))
                {
                    if (list.Find(s => s == t.TransName) == null) list.Add(t.TransName.ToString());
                }
                if (list.Count == 0)
                {
                    throw new StateMachineAlterFailException(this.ID + " : No find next trigger in this state");
                }
                else if (list.Count > 1)
                {
                    throw new StateMachineAlterFailException(this.ID + " : more than one trigger in this state");
                }
                else
                {
                    this.SmTriggerByName(list[0]);
                }
                return 0;
            }
        }
        public virtual int SmTriggerByName(string transitionName) { return this.SmTriggerByParam(new StateParameter(transitionName)); }
        public virtual int SmTriggerByName(Enum transition) { return this.SmTriggerByParam(new StateParameter(transition.ToString())); }
        public virtual int SmTriggerByParam(IStateParam msg)
        {
            this.SmTriggerQueue.Enqueue(msg);
            return 0;
        }
        public virtual int SmTriggerToAlterNext(List<TransitionEvaluation> alterList)
        {
            lock (this)
            {
                if (alterList.Count != CurrentState.TransitionsInState.FindAll(t => !(t.ToState is ExceptionState)).Count)
                {
                    throw new StateMachineAlterFailException(this.ID + " : alter list count not equals to current state normal transition count");
                }
                var list = alterList.FindAll(t => t.transBool.Invoke());
                if (list.Count == 0)
                {
                    throw new StateMachineAlterFailException(this.ID + " : No alter is successful");
                }
                else if (list.Count > 1)
                {
                    throw new StateMachineAlterFailException(this.ID + " : more than one alters are successful");
                }
                else
                {
                    this.SmTriggerByName(list[0].transName);
                }
                //this.TriggerQueue.Enqueue(list.First());
                return 0;
            }
        }
        public virtual int SmTriggerToNext(string tranName = null)
        {
            if (!string.IsNullOrEmpty(tranName))
            {
                this.SmTriggerByName(tranName);
                return 0;
            }

            if (this.AlterList.Count == 0)
                this.SmTriggerToSingleNext();
            else
                this.SmTriggerToAlterNext(this.AlterList);


            return 0;
        }
        public virtual int SmTriggerToNext(Enum tranName) { return this.SmTriggerToNext(tranName.ToString()); }
        public virtual int SmTriggerToSingleNext()
        {
            lock (this)
            {
                List<string> list = new List<string>();
                foreach (Transition t in CurrentState.TransitionsInState.FindAll(t => !(t.ToState is ExceptionState)))
                {
                    if (list.Find(s => s == t.TransName) == null) list.Add(t.TransName.ToString());
                }
                if (list.Count == 0)
                {
                    throw new StateMachineAlterFailException(this.ID + " : No find next trigger in this state");
                }
                else if (list.Count > 1)
                {
                    //TODO: Load Port 有2個Trigger
                    throw new StateMachineAlterFailException(this.ID + " : more than one trigger in this state");
                }
                else
                {
                    this.SmTriggerByName(list[0]);
                }
                return 0;
            }
        }






        /// <summary>
        /// transitions runTrigger with step name
        /// </summary>
        /// <param name="param"></param>
        /// <returns>-1 is action can't run</returns>
        public EnumStateMachineMsgType SmProcTransition(IStateParam param)
        {
            try
            {
                bool executeFlag = false;

                Transition runTriggerSingle = null;

                //第一步檢查有沒有此TRANSITION
                if (transitions.Find(x => x.TransName.Equals(param.TransName)) == null)
                    throw new StateMachineException(ID + " No Find Transition: " + param.TransName);


                //找出同名Transition
                var trans = (from t in transitions
                             where t.TransName == param.TransName
                             select t).ToList();
                var currTrans = trans.Where(t => t.FromState == this.CurrentState).ToList();//過濾from state

                //確認所有Guard是否成立
                foreach (var tran in currTrans)
                {

                    if (tran.RunGuard(this))
                    {
                        //若確實只找到一個成立, 記錄下來, 超過一個不可執行
                        if (runTriggerSingle == null)
                            runTriggerSingle = tran;
                        else
                            throw new PluralTriggerSameTimeException(this.GetType().ToString() + " : current is " + this.CurrentState.StateName + " with Transition : " + param.TransName + ", trigger more than one same time");//只能有一個transition成立
                    }
                }


                if (runTriggerSingle != null)
                {
                    executeFlag = true;
                    runTriggerSingle.RunTrigger(param, this);
                }
                else if (currTrans.Count == 0 && trans.Where(t => t is DeferralTransition).Count() == 0)
                {
                    //如果要求的transaction不在current state, 且不具有Deferral Transaction, 就拋出例外
                    //不應存在無法trigger的transition
                    throw new NotCurrentStateException(this.GetType().ToString() + " : current is " + this.CurrentState.StateName + " does not have Transition : " + param.TransName);
                }
                else
                {
                    //否則: 這個State Machine 的 Deferral Transition 具有 param.TranName
                    //在這裡代表沒有transition被觸發, 因此需要判斷是否要存回Queue裡
                    //State Machine中, 並沒有定義 Defferal Guard 失敗時的行為, 因此暫以 Normal Transition 的行為作準則
                    // 不回存(or, 其中一個成立)
                    // 1. Trigger 成功
                    // 2. Current State 具同名 Transition (不論 Guard 成功與否)
                    // 即=>回存(and, 全部成立)
                    // 1. Trigger失敗
                    // 2. Curr State 不具同名 Transition
                    // 3. 其它State具有同名 DeferralTransition



                    //找出同名且不在Current State裡的Defferal的數量
                    var countDefferalOfNotInCurr = (from row in trans
                                                    where row is DeferralTransition
                                                    select row).Count();

                    if (currTrans.Count == 0 && countDefferalOfNotInCurr > 0)
                        SaveDeferralEvent(param);//TODO: 確認加回最後一個是否合理



                    //Deferral 請先確認是否有在CURRENT STATE有在的話，不管GUARD的狀況與否都不會重新放回JOB LIST
                    //bool noTransitionInCurrentState = true;
                    //foreach (Transition t in trans.Where(t => t is DeferralTransition))
                    //{
                    //    if (((DeferralTransition)t).IsInCurrentState(this))
                    //    {
                    //        noTransitionInCurrentState = false;
                    //    }
                    //}


                    //if (noTransitionInCurrentState)
                    //{
                    //    SaveDeferralEvent(param);
                    //}
                    //else
                    //{
                    //    //執行Defferal transition on not current state (save to event queue)
                    //    foreach (Transition t in trans.Where(t => t is DeferralTransition))
                    //    {
                    //        if (t.RunGuard(this))
                    //        {
                    //            if (runTriggerSingle == null)
                    //            {
                    //                runTriggerSingle = (DeferralTransition)t;
                    //            }
                    //            else
                    //            {
                    //                throw new PluralTriggerSameTimeException(this.GetType().ToString() + " : current is " + this.CurrentState.StateName + " with Transition : " + param.TransName + ", trigger more than one same time");
                    //            }
                    //        }
                    //    }
                    //    runTriggerSingle.RunTrigger(param, this);
                    //}
                }

                if (executeFlag)
                {
                    return EnumStateMachineMsgType.CommandSuccessful;
                }
                else
                {
                    //run deferral or strange transition
                    /*foreach(Transition t in (from transObj in transitions where transObj.TransName.Equals(param.TransName.ToString()) && !currentState.transs.Contains(transObj) select transObj))
                    {
                        t.RunTrigger(param, this);
                    }*/
                    return EnumStateMachineMsgType.GuardFail;
                }
            }
            catch (PluralTriggerSameTimeException ex)
            {
                PopupAlarm(this, EnumStateMachineMsgType.PluralTriggerSameTime, "TransitionPluralTrigger");
                MvLog.Write(ex, MvLoggerEnumLevel.Warn);
                return EnumStateMachineMsgType.PluralTriggerSameTime;
            }
            catch (NotCurrentStateException ex)
            {
                //Transition nocurTrans = GetDynamicExceptionTransition(CurrentState, EnumStateMachineMsgType.NotCurrentStateTransition);
                //nocurTrans.RunTrigger(new StateParameter(), this);
                MvLog.Write(ex, MvLoggerEnumLevel.Error);
                return EnumStateMachineMsgType.StateMachineException;
            }
            catch (StateMachineException ex)
            {
                System.Diagnostics.Debug.WriteLine(this.ID + ":Error");
                MvLog.Write(ex, MvLoggerEnumLevel.Error);
                throw ex;
            }
            catch (Exception ex)
            {
                MvLog.Write(ex, MvLoggerEnumLevel.Error);
                throw ex;
            }
        }
        public EnumStateMachineMsgType SmProcTransition(string transName)
        {
            StateParameter sp = new StateParameter(transName);
            return SmProcTransition(sp);
        }

        #endregion


        #region Provide state machine initial method

        public static void NoAction()
        {
        }

        public static bool PassGuard() { return true; }

        public virtual State NewState(string name, StateType st = StateType.Normal)
        {
            State state;
            if (st == StateType.Exception) { state = new ExceptionState(name); }
            else { state = new State(name); }
            AddState(state);
            return state;
        }

        public virtual State NewState(Enum name, StateType st = StateType.Normal) { return this.NewState(name.ToString(), st); }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromState"></param>
        /// <param name="toState"></param>
        /// <param name="guard"></param>
        /// <param name="action"></param>
        /// <param name="transName"></param>
        /// <param name="tt"></param>
        /// <param name="timeout">有設置TIMEOUT時間時，代表此transition 是timeout類型且在tostate timeout時切到這條TRANSITION，MAXVALUE被設定為不設定</param>
        /// <returns></returns>
        public virtual Transition NewTransition(State fromState, State toState
            , Func<bool> guard, Action action, string transName
            , TransitionType tt = TransitionType.Normal, int timeout = int.MaxValue)
        {
            Transition transition;
            if (tt == TransitionType.Deferral)
            {
                transition = new DeferralTransition(fromState, toState, guard, action, transName);
            }
            else if (tt == TransitionType.Internal)
            {
                transition = new InternalTransition(fromState, guard, action, transName);
            }
            else
            {
                transition = new Transition(fromState, toState, guard, action, transName);
            }
            if (timeout != int.MaxValue)
            {
                if (timeout > 0)
                {
                    toState.timeoutExecuteTransition = transition;
                    toState.TimeoutLimit = timeout;
                }
                else
                {
                    throw new Exception("wrong transition timeout config");
                }
            }
            AddTransition(transition);
            return transition;
        }
        public virtual Transition NewTransition(State fromState, State toState
         , Func<bool> guard, Action action, Enum trans
         , TransitionType tt = TransitionType.Normal, int timeout = int.MaxValue)
        {
            return this.NewTransition(fromState, toState, guard, action, trans.ToString(), tt, timeout);
        }



        public void AddState(State s)
        {
            try
            {
                states.Add(s.StateName, s);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Same State Name in States");
            }
        }
        public void AddTransition(Transition t)
        {
            if (t == null) { throw new NullReferenceException("reference transition is null"); }
            try
            {
                //transitions.Add(t.TransName, t);
                transitions.Add(t);
            }
            catch (ArgumentException) { throw new ArgumentException("Same Transition Name in Transitions"); }
        }


        #endregion


    }
}