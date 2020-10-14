//#define NoConfig
using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.Mac.v1_0.Hal.CompLoadPort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MaskAutoCleaner.v1_0.StateMachineAlpha;
using MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException;
using MvAssistant.Mac.v1_0.Hal.Assembly;

namespace MaskAutoCleaner.v1_0.Machine.LoadPort
{
    [Guid("B6CCEC0B-9042-4B88-A306-E29B87B6469C")]
    public class MacMsLoadPort : MacMachineStateBase
    {
        /// <summary>Load Port A 的 Instance, </summary>
        private static MacMsLoadPort _loadPortStateMachineA = null;
        /// <summary>Load Port B 的 Instance </summary>
        private static MacMsLoadPort _loadPortStateMachineB = null;
        /// <summary>取得 Loadport A  Instance 時 Lock 的物件</summary>
        private static readonly object _loportAlockObject = new object();
        /// <summary>取得 Loadport B Instance 時 Lock 物件</summary>
        private static readonly object _loportBlockObject = new object();
        /// <summary>控制逾時與否的物件</summary>
        private MacMsTimeOutController TimeController = new MacMsTimeOutController(50);


        /**
       
        public static MacMsLoadPort LoadPortStateMachineA
        {
            get
            {
                if (_loadPortStateMachineA == null)
                {
                    lock (_loportAlockObject)
                    {
                        if (_loadPortStateMachineA == null)
                        {
                            var MachineMgr = new MacMachineMgr();
                            MachineMgr.MvCfInit();
                            var MachineCtrlA = MachineMgr.CtrlMachines[EnumLoadportStateMachineID.MID_LP_A_ASB.ToString()] as MacMcLoadPort;
                            _loadPortStateMachineA = MachineCtrlA.StateMachine;
                            var B = LoadPortStateMachineB;

                        }
                    }
                }
                return _loadPortStateMachineA;
            }
        }
        public static MacMsLoadPort LoadPortStateMachineB
        {
            get
            {
                if (_loadPortStateMachineB == null)
                {
                    lock (_loportAlockObject)
                    {
                        if (_loadPortStateMachineB == null)
                        {
                            var MachineMgr = new MacMachineMgr();
                            MachineMgr.MvCfInit();
                            var MachineCtrl = MachineMgr.CtrlMachines[EnumLoadportStateMachineID.MID_LP_B_ASB.ToString()] as MacMcLoadPort;
                            _loadPortStateMachineB = MachineCtrl.StateMachine;
                            var A = LoadPortStateMachineA;
                        }
                    }
                }
                return _loadPortStateMachineB;
            }
        }
        */


#if NoConfig
        IMacHalLoadPortUnit HalLoadPort = null;
#endif
        public IMacHalLoadPort HalLoadPortUniversal { get { return this.halAssembly as IMacHalLoadPort; } }
        public IMacHalLoadPortUnit HalLoadPortUnit
        {
#if NoConfig
            get
            {
           
                return HalLoadPort;
            }
#else
            get
            {
                try
                {
                     var rtnV= HalLoadPortUniversal.LoadPortUnit;
                    return rtnV;
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
#endif

        }


#if NoConfig
        public MacMsLoadPort()
        {
            //HalLoadPort = new MacHalGudengLoadPort();
            //HalLoadPort.HalConnect();
            LoadStateMachine();
        }
#else
        public MacMsLoadPort()
        {
             LoadStateMachine();
        }
#endif


        #region  Command
        /// <summary>啟動系統</summary>
        /// <remarks>
        /// 從 系統啟動 => 啟動後 AlarmReset
        /// </remarks>
        public void SystemBootup()
        {
            var state = this.States[EnumMacMsLoadPortState.SystemBootup.ToString()];
            state.DoEntry(new MacStateEntryEventArgs());
        }

        



       
        /// <summary>Alarm Reset</summary>
        public void AlarmReset()
        {
            var state = this.States[EnumMacMsLoadPortState.AlarmResetStart.ToString()];
            state.DoEntry(new MacStateEntryEventArgs());
        }

        public void Inintial()
        {
            var state = this.States[EnumMacMsLoadPortState.InitialStart.ToString()];
            state.DoEntry(new MacStateEntryEventArgs());
        }


        public void Dock()
        {
            var state = this.States[EnumMacMsLoadPortState.DockStart.ToString()];
            state.DoEntry(new MacStateEntryEventArgs());
        }
         
        public void Undock()
        {

            var state = this.States[EnumMacMsLoadPortState.UndockStart.ToString()];
            state.DoEntry(new MacStateEntryEventArgs());
        }
        #endregion command

        public override void LoadStateMachine()
        {
            #region State

            // 系統啟動
            MacState sSystemBootup = NewState(EnumMacMsLoadPortState.SystemBootup);
            MacState sSystemBootupAlarmResetStart = NewState(EnumMacMsLoadPortState.SystemBootupAlarmResetStart);
            MacState sSystemBootupAlarmResetIng = NewState(EnumMacMsLoadPortState.SystemBootupAlarmResetIng);
            MacState sSystemBootupAlarmResetComplete = NewState(EnumMacMsLoadPortState.SystemBootupInitialComplete);
            MacState sSystemBootupInitialStart = NewState(EnumMacMsLoadPortState.SystemBootupInitialStart);
            MacState sSystemBootupInitialIng = NewState(EnumMacMsLoadPortState.SystemBootupInitialIng);
            MacState sSystemBootupInitialComplete = NewState(EnumMacMsLoadPortState.SystemBootupInitialComplete);


            // AlarmReset 開始
            MacState sAlarmResetStart = NewState(EnumMacMsLoadPortState.SystemBootup);
            // AlarmReset 進行中
            MacState sAlarmResetIng = NewState(EnumMacMsLoadPortState.AlarmResetIng);
            // AlarmReset 完成
            MacState sAlarmResetComplete = NewState(EnumMacMsLoadPortState.AlarmResetComplete);
          

            // Initial
            MacState sInitialStart = NewState(EnumMacMsLoadPortState.InitialStart);
            MacState sInitialIng = NewState(EnumMacMsLoadPortState.InitialIng);
            MacState sInitialComplete = NewState(EnumMacMsLoadPortState.InitialComplete);
            // 等待將 POD 放到 Load Port 上
            MacState sIdleForPutPOD = NewState(EnumMacMsLoadPortState.IdleForPutPOD);

            // dock
            MacState sDockStart = NewState(EnumMacMsLoadPortState.DockStart);
            MacState sDockIng= NewState(EnumMacMsLoadPortState.DockIng);
            MacState sDockComplete = NewState(EnumMacMsLoadPortState.DockComplete);
            // dock 等待Robot 夾取光置
            MacState sIdleForGetMask = NewState(EnumMacMsLoadPortState.IdleForGetMask);
            
            // undock
            MacState sUndockStart = NewState(EnumMacMsLoadPortState.UndockStart);
            MacState sUndockIng= NewState(EnumMacMsLoadPortState.UndockIng);
            MacState sUndockComplete = NewState(EnumMacMsLoadPortState.UndockComplete);
            // Undock 等待POD 被取走
            MacState sIdleForGetPOD = NewState(EnumMacMsLoadPortState.IdleForGetPOD);
            #endregion State


            #region Transition
            // SystemBootUp
            
            MacTransition tSystemBootup_SystemBootupAlarmResetStart =  NewTransition(sSystemBootup, sSystemBootupAlarmResetStart, EnumMacMsLoadPortTransition.SystemBootup_SystemBootupAlarmResetStart);
            MacTransition tSystemBootupAlarmResetStart_SystemBootupAlarmResetIng = NewTransition(sSystemBootupAlarmResetStart, sSystemBootupAlarmResetIng, EnumMacMsLoadPortTransition.SystemBootupAlarmResetStart_SystemBootupAlarmResetIng);
            MacTransition tSystemBootupAlarmResetIng_SystemBootupAlarmResetComplete = NewTransition(sSystemBootupAlarmResetIng, sSystemBootupAlarmResetComplete, EnumMacMsLoadPortTransition.SystemBootupAlarmResetIng_SystemBootupAlarmResetComplete);
            MacTransition tSystemBootupAlarmResetComplete_SystemBootupInitialStart = NewTransition(sSystemBootupAlarmResetComplete, sSystemBootupInitialStart, EnumMacMsLoadPortTransition.SystemBootupAlarmResetComplete_SystemBootupInitialStart);
            MacTransition tSystemBootupInitialStart_SystemBootupInitialIng = NewTransition(sSystemBootupInitialStart, sSystemBootupInitialIng, EnumMacMsLoadPortTransition.SystemBootupInitialStart_SystemBootupInitialIng);
            MacTransition tSystemBootupInitialIng_SystemBootupInitialComplete = NewTransition(sSystemBootupInitialIng, sSystemBootupInitialComplete, EnumMacMsLoadPortTransition.SystemBootupInitialIng_SystemBootupInitialComplete);
            MacTransition tSystemBootupInitialComplete_IdleForPutPOD = NewTransition(sSystemBootupInitialComplete, sIdleForPutPOD, EnumMacMsLoadPortTransition.SystemBootupInitialComplete_IdleForPutPOD);


            // AlarmReset
            MacTransition tAlarmResetStart_AlarmResetIng = NewTransition(sAlarmResetStart, sAlarmResetIng, EnumMacMsLoadPortTransition.AlarmResetStart_AlarmResetIng);
            MacTransition tAlarmResetIng_AlarmResetComplete = NewTransition(sAlarmResetIng, sAlarmResetComplete, EnumMacMsLoadPortTransition.AlarmResetIng_AlarmResetComplete);
            MacTransition tAlarmResetComplete_NULL = NewTransition(sAlarmResetComplete, null, EnumMacMsLoadPortTransition.AlarmResetComplete_NULL);

            //Initial
            MacTransition tInitialStart_InitialIng = NewTransition(sInitialStart, sInitialIng, EnumMacMsLoadPortTransition.InitialStart_InitialIng);
            MacTransition tInitialIng_InitialComplete = NewTransition(sInitialIng, sInitialComplete, EnumMacMsLoadPortTransition.InitialIng_InitialComplete);
            MacTransition tInitialComplete_IdleForPutPOD= NewTransition(sInitialComplete, sIdleForPutPOD, EnumMacMsLoadPortTransition.InitialComplete_IdleForPutPOD);
            MacTransition tIdleForPutPOD_NULL = NewTransition(sIdleForPutPOD, null, EnumMacMsLoadPortTransition.IdleForPutPOD_NULL); 

            // Dock
            MacTransition tDockStart_DockIng = NewTransition(sDockStart, sDockIng, EnumMacMsLoadPortTransition.DockStart_DockIng);
            MacTransition tDockIng_DockComplete = NewTransition(sDockIng, sDockComplete, EnumMacMsLoadPortTransition.DockIng_DockComplete);
            MacTransition tDockComplete_IdleForGetMask= NewTransition(sDockComplete, sIdleForGetMask, EnumMacMsLoadPortTransition.DockComplete_IdleForGetMask);
            MacTransition tIdleForGetMask_NULL = NewTransition(sIdleForGetMask,null, EnumMacMsLoadPortTransition.IdleForGetMask_NULL);

            // Undock
            MacTransition tUndockStart_UndockIng = NewTransition(sUndockStart, sUndockIng, EnumMacMsLoadPortTransition.UndockStart_UndockIng);
            MacTransition tUndockIng_UndockComplete = NewTransition(sUndockIng, sUndockComplete, EnumMacMsLoadPortTransition.UndockIng_UndockComplete);
            MacTransition tUndockComplete_IdleForGetPOD= NewTransition(sUndockComplete, sIdleForGetPOD, EnumMacMsLoadPortTransition.UndockComplete_IdleForGetPOD);
            MacTransition tIdleForGetPOD_NULL = NewTransition(sIdleForGetPOD,null, EnumMacMsLoadPortTransition.IdleForGetPOD_NULL);
            #endregion


            #region  Register OnEntry, OnExit Event Handler

            sSystemBootup.OnEntry += (sender, e) =>
            {   // Synch
                SetCurrentState((MacState)sender);
                var transition = tSystemBootup_SystemBootupAlarmResetStart;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sSystemBootup.OnExit += (sender, e) =>
            {

            };

            sSystemBootupAlarmResetStart.OnEntry += (sender, e) =>
            { // Synch
                SetCurrentState((MacState)sender);
                var transition = tSystemBootupAlarmResetStart_SystemBootupAlarmResetIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { this.HalLoadPortUnit.CommandAlarmReset(); },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    { },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);

            };
            sSystemBootupAlarmResetStart.OnExit += (sender, e) =>
            {

            };

            sSystemBootupAlarmResetIng.OnEntry += (sender, e) =>
            { //Async
                SetCurrentState((MacState)sender);
                var transition = tSystemBootupAlarmResetIng_SystemBootupAlarmResetComplete;
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                    },
                    Guard = (startTime) =>
                    {
                        if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.AlarmResetComplete)
                        {
                            return true;
                        }
                        else if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.AlarmResetFail)
                        {
                            throw new LoadportSystemBootupAlarmResetFailException();
                        }
                        else if (TimeController.IsTimeOut(startTime))
                        {
                            throw new LoadportSystemBootupAlarmResetTimeOutException();
                        }
                        else
                        {
                            return false;
                        }
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);

            };
            sSystemBootupAlarmResetIng.OnExit += (sender, e) =>
            {
               
            };


            sSystemBootupAlarmResetComplete.OnEntry += (sender, e) =>
            {   // Sync
                SetCurrentState((MacState)sender);
                var transition = tSystemBootupAlarmResetComplete_SystemBootupInitialStart;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {

                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sSystemBootupAlarmResetComplete.OnExit += (sender, e) =>
            {

            };

            sSystemBootupInitialStart.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                var transition = tSystemBootupInitialStart_SystemBootupInitialIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { this.HalLoadPortUnit.CommandInitialRequest(); },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                     
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition); 
            };
            sSystemBootupInitialStart.OnExit += (sender, e) =>
            {

            };

            sSystemBootupInitialIng.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                var transition = tSystemBootupInitialIng_SystemBootupInitialComplete;
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    { },
                    Guard = (startTime) =>
                    {
                        if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.InitialComplete)
                        {
                            return true;
                        }
                        else if (HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.MustResetFirst)
                        {
                            throw new LoadportSystemBootupInitialMustResetException();
                        }
                        else if (TimeController.IsTimeOut(startTime))
                        {
                            throw new LoadportSystemBootupInitialTimeOutException();
                        }
                        else
                        {
                            return false;
                        }
                       
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                Trigger(transition);
            };
            sSystemBootupInitialIng.OnExit += (sender, e) =>
            {

            };

            sSystemBootupInitialComplete.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                var transition = tSystemBootupInitialComplete_IdleForPutPOD;
                var triggerMmember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {

                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMmember);
                Trigger(transition);
            };
            sSystemBootupInitialComplete.OnExit += (sender, e) =>
            {

            };


            sAlarmResetStart.OnEntry += (sender, e) =>
            {   // Sync
                SetCurrentState((MacState)sender);
                var transition = tAlarmResetStart_AlarmResetIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { this.HalLoadPortUnit.CommandAlarmReset(); }, 
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        //TODO: do something
                    },
                    Guard = () => {return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sAlarmResetStart.OnExit += (sender, e) =>
            {
                // 視狀況新增 Code
            };

            sAlarmResetIng.OnEntry += (sender, e) => 
            {   // Async
                SetCurrentState((MacState)sender);
                var transition = tAlarmResetIng_AlarmResetComplete;
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    Guard = (startTime) =>
                    {
                        if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.AlarmResetComplete)
                        {
                            return true;
                        }
                        else if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.AlarmResetFail)
                        {
                            throw new LoadportAlarmResetFailException();
                        }
                        else if (TimeController.IsTimeOut(startTime))
                        {
                            throw new LoadportAlarmResetTimeOutException();
                        }
                        else
                        {
                            return false;
                        }
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs=new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(   transition);
            };
            sAlarmResetIng.OnExit += (sender, e) => 
            {
                // 視實況新增 Code 
            };

            sAlarmResetComplete.OnEntry += (sender, e) => 
            {  // Sync
                var transition = tAlarmResetComplete_NULL;
                SetCurrentState((MacState)sender);
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sAlarmResetComplete.OnExit += (sender, e) => 
            {
                // 視狀況增加 Code
            };

            sInitialStart.OnEntry += (sender, e) =>
            {  // Sync 
                var transition = tInitialStart_InitialIng;
                SetCurrentState((MacState)sender);
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { this.HalLoadPortUnit.CommandInitialRequest(); },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    Guard = () => {return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                 };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInitialStart.OnExit += (sender, e) =>
            {
                // 視狀況增加 Code
            };

            sInitialIng.OnEntry += (sender, e) =>
            {   // Async
                var transition = tInitialIng_InitialComplete;
                SetCurrentState((MacState)sender);
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    Guard = (startTime) =>
                    {
                        if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.InitialComplete)
                        {
                            return true;
                        }
                        else if (HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.MustResetFirst)
                        {
                            throw new LoadportInitialMustResetException();
                        }
                        else if(TimeController.IsTimeOut(startTime))
                        {
                            throw new LoadportInitialTimeOutException();
                        }
                        else
                        {
                            return false;
                        }
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
    
            };
            sInitialIng.OnExit += (sender, e) =>
            {
                // 視狀況新增 Code
            };

            sInitialComplete.OnEntry += (sender, e) =>
            {   // Sync
                SetCurrentState((MacState)sender);
                var transition = tInitialComplete_IdleForPutPOD;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInitialComplete.OnExit += (sender, e) =>
            {
                // 視狀況增加Code
            };

          
            sIdleForPutPOD.OnEntry += (sender, e) =>
            {  // Sync
                var transition = tIdleForPutPOD_NULL;
                SetCurrentState((MacState)sender);
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sIdleForPutPOD.OnExit += (sender, e) =>
            {
                // 依實際狀況 增加 Code
            };
          
            sDockStart.OnEntry += (sender, e) =>
            {   // Sync
                var transition = tDockStart_DockIng;
                SetCurrentState((MacState)sender);
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { this.HalLoadPortUnit.CommandDockRequest(); },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    Guard = () => {return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                     
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sDockStart.OnExit += (sender, e) =>
            {
                // 視狀況增加 Code
            };

            sDockIng.OnEntry += (sender,e)=>
            {  // Async
                SetCurrentState((MacState)sender);
                var transition = tDockIng_DockComplete;
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: dosomething
                    },
                    Guard = (startTime) =>
                    {
                        if(this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.DockComplete)
                        {
                            return true;
                        }
                        else if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.MustInitialFirst)
                        {
                            throw new LoadportDockMustInitialException();
                        }
                        else if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.MustResetFirst)
                        {
                            throw new LoadportDockMustResetException();
                        }
                        else if (TimeController.IsTimeOut(startTime))
                        {
                            throw new LoadportDockTimeOutException();
                        }
                        else
                        {
                            return false;
                        }
                    },
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sDockIng.OnExit += (sender, e) =>
            {
                // 視狀況增加 Code
            };

            
            sDockComplete.OnEntry += (sender, e) =>
            {   // Sync 
                var transition = tDockComplete_IdleForGetMask;
                SetCurrentState((MacState)sender);
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO : do something
                    },
                    Guard = () =>{ return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sDockComplete.OnExit+= (sender, e) =>
            {
                // 視狀況加 Code
            };

            sIdleForGetMask.OnEntry += (sender, e) =>
            {   // Sync
                var transition = tIdleForGetMask_NULL;
                SetCurrentState((MacState)sender);
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sIdleForGetMask.OnExit += (sender, e) =>
            {
                // TODO: Depends on,......
            };


            sUndockStart.OnEntry += (sender, e) =>
            {  // Sync
                var transition = tUndockStart_UndockIng;
                SetCurrentState((MacState)sender);
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { this.HalLoadPortUnit.CommandUndockRequest(); },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                      {
                          // TODO: do something
                      },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sUndockStart.OnExit += (sender, e) =>
            {
                // 視狀況增加 Code
            };

            sUndockIng.OnEntry += (sender, e) =>
            {
                // Async
                var transition = tUndockIng_UndockComplete;
                SetCurrentState((MacState)sender);
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (State, ex) =>
                    {
                        // TODO: do something 
                    },
                    Guard = (startTime) =>
                    {
                        if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.UndockComplete)
                        {
                            return true;
                        }
                        else if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.MustInitialFirst)
                        {
                            throw new LoadportUndockMustInitialException();
                        }
                        else if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.MustResetFirst)
                        {
                            throw new LoadportUndockMustResetException();
                        }
                        else if (TimeController.IsTimeOut(startTime))
                        {
                            throw new LoadportUndockTimeOutException();
                        }
                        else
                        {
                            return false;
                        }
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sUndockIng.OnExit += (sender, e) =>
            {
               // 視狀況增加 Code
            };


            sUndockComplete.OnEntry += (sender, e) =>
            {  // Sync 
                var transition = tUndockComplete_IdleForGetPOD;
                SetCurrentState((MacState)sender);
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something 
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sUndockComplete.OnExit += (sender, e) =>
            {
                // 視狀況新增 Code
            };
          
            sIdleForGetPOD.OnEntry += (sender, e) =>
            {
                var transition = tIdleForGetPOD_NULL;
                SetCurrentState((MacState)sender);
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something 
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()

                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sIdleForGetPOD.OnExit += (sender, e) =>
            {
                // 視狀況新增 Code
            };
            #endregion


        }
    }
   
}
