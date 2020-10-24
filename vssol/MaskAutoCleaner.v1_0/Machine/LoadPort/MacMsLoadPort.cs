﻿//#define NoConfig
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
using System.Diagnostics;

namespace MaskAutoCleaner.v1_0.Machine.LoadPort
{
    [Guid("B6CCEC0B-9042-4B88-A306-E29B87B6469C")]
    public class MacMsLoadPort : MacMachineStateBase
    {
         /// <summary>控制逾時與否的物件</summary>
       // private MacMsTimeOutController TimeController = new MacMsTimeOutController(20);
 
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
           TimeoutObject = new MacMsTimeOutController(20);
           LoadStateMachine();
        }
#else
        public MacMsLoadPort()
        {
            TimeoutObject = new MacMsTimeOutController(20);
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
            Debug.WriteLine("Command: [SystemBootup], Index:" + this.HalLoadPortUnit.DeviceIndex );
            var state = this.States[EnumMacMsLoadPortState.SystemBootup.ToString()];
            state.DoEntry(new MacStateEntryEventArgs());
            
        }

        



       
        /// <summary>Alarm Reset</summary>
        public void AlarmReset()
        {
            Debug.WriteLine("Command: [AlarmReset], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var state = this.States[EnumMacMsLoadPortState.AlarmResetStart.ToString()];
            state.DoEntry(new MacStateEntryEventArgs());
        }

        public void Inintial()
        {
            Debug.WriteLine("Command: [Inintial], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var state = this.States[EnumMacMsLoadPortState.InitialStart.ToString()];
            state.DoEntry(new MacStateEntryEventArgs());
        }


        public void Dock()
        {
            Debug.WriteLine("Command: [Dock], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var state = this.States[EnumMacMsLoadPortState.DockStart.ToString()];
            state.DoEntry(new MacStateEntryEventArgs());
        }
         
        public void Undock()
        {
            Debug.WriteLine("Command: [Undock], Index:" + this.HalLoadPortUnit.DeviceIndex);
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
            MacState sAlarmResetStart = NewState(EnumMacMsLoadPortState.AlarmResetStart);
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
                Debug.WriteLine("State: [sSystemBootup.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                Debug.WriteLine("State: [sSystemBootup.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };

            sSystemBootupAlarmResetStart.OnEntry += (sender, e) =>
            { // Synch
                Debug.WriteLine("State: [sSystemBootupAlarmResetStart.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                Debug.WriteLine("State: [sSystemBootupAlarmResetStart.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };

            sSystemBootupAlarmResetIng.OnEntry += (sender, e) =>
            { //Async
                Debug.WriteLine("State: [sSystemBootupAlarmResetIng.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                        else if (this.TimeoutObject.IsTimeOut(startTime))
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
                Debug.WriteLine("State: [sSystemBootupAlarmResetIng.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };


            sSystemBootupAlarmResetComplete.OnEntry += (sender, e) =>
            {   // Sync
                Debug.WriteLine("State: [sSystemBootupAlarmResetComplete.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                Debug.WriteLine("State: [sSystemBootupAlarmResetComplete.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };

            sSystemBootupInitialStart.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sSystemBootupInitialStart.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                Debug.WriteLine("State: [sSystemBootupInitialStart.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };

            sSystemBootupInitialIng.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sSystemBootupInitialIng.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                        else if (this.TimeoutObject.IsTimeOut(startTime))
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
                TriggerAsync(transition);
            };
            sSystemBootupInitialIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sSystemBootupInitialIng.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };

            sSystemBootupInitialComplete.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sSystemBootupInitialComplete.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                Debug.WriteLine("State: [sSystemBootupInitialComplete.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };


            sAlarmResetStart.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sAlarmResetStart.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                // Sync
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
                Debug.WriteLine("State: [sAlarmResetStart.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
                // 視狀況新增 Code
            };

            sAlarmResetIng.OnEntry += (sender, e) => 
            {
                Debug.WriteLine("State: [sAlarmResetIng.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                // Async
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
                        else if (this.TimeoutObject.IsTimeOut(startTime))
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
                Debug.WriteLine("State: [sAlarmResetIng.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
                // 視實況新增 Code 
            };

            sAlarmResetComplete.OnEntry += (sender, e) => 
            {  // Sync
                Debug.WriteLine("State: [sAlarmResetComplete.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                Debug.WriteLine("State: [sAlarmResetComplete.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };

            sInitialStart.OnEntry += (sender, e) =>
            {  // Sync 
                Debug.WriteLine("State: [sInitialStart.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                Debug.WriteLine("State: [sInitialStart.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
                // 視狀況增加 Code
            };

            sInitialIng.OnEntry += (sender, e) =>
            {   // Async
                Debug.WriteLine("State: [sInitialIng.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                        else if(this.TimeoutObject.IsTimeOut(startTime))
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
                Debug.WriteLine("State: [sInitialIng.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };

            sInitialComplete.OnEntry += (sender, e) =>
            {   // Sync
                Debug.WriteLine("State: [sInitialComplete.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                Debug.WriteLine("State: [sInitialComplete.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };

          
            sIdleForPutPOD.OnEntry += (sender, e) =>
            {  // Sync
                Debug.WriteLine("State: [sIdleForPutPOD.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                Debug.WriteLine("State: [sIdleForPutPOD.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
          
            sDockStart.OnEntry += (sender, e) =>
            {   // Sync
                Debug.WriteLine("State: [sDockStart.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                Debug.WriteLine("State: [sDockStart.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };

            sDockIng.OnEntry += (sender,e)=>
            {  // Async
                SetCurrentState((MacState)sender);
                Debug.WriteLine("State: [sDockIng.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                        else if (this.TimeoutObject.IsTimeOut(startTime))
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
                Debug.WriteLine("State: [sDockIng.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };

            
            sDockComplete.OnEntry += (sender, e) =>
            {   // Sync 
                var transition = tDockComplete_IdleForGetMask;
                Debug.WriteLine("State: [sDockComplete.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                Debug.WriteLine("State: [sDockComplete.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };

            sIdleForGetMask.OnEntry += (sender, e) =>
            {   // Sync
                Debug.WriteLine("State: [sIdleForGetMask.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                Debug.WriteLine("State: [sIdleForGetMask.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };


            sUndockStart.OnEntry += (sender, e) =>
            {  // Sync
                Debug.WriteLine("State: [sUndockStart.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                Debug.WriteLine("State: [sUndockStart.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };

            sUndockIng.OnEntry += (sender, e) =>
            {
                // Async
                var transition = tUndockIng_UndockComplete;
                Debug.WriteLine("State: [sUndockIng.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                        else if (this.TimeoutObject.IsTimeOut(startTime))
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
                Debug.WriteLine("State: [sUndockIng.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };


            sUndockComplete.OnEntry += (sender, e) =>
            {  // Sync 
                Debug.WriteLine("State: [sUndockComplete.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                Debug.WriteLine("State: [sUndockComplete.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
          
            sIdleForGetPOD.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sIdleForGetPOD.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                Debug.WriteLine("State: [sIdleForGetPOD.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);

                // 視狀況新增 Code
            };
            #endregion


        }
    }
   
}
