﻿//#define NoConfig
//#define NotCareState
using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.v0_2.Mac.Hal.CompLoadPort;
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
using MvAssistant.v0_2.Mac.Hal.Assembly;
using System.Diagnostics;
//using MaskAutoCleaner.v1_0.Machine.LoadPort.OnEntryEventArgs;

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
                    var rtnV = HalLoadPortUniversal.LoadPortUnit;
                    return rtnV;
                }
                catch (Exception ex)
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
        /// AlarmReset[Start, Ing, Complete] => Initial[Start, Ing, Complete]=> Idle(OnEntry)
        /// </remarks>
        public override void SystemBootup()
        {
            Debug.WriteLine("Command: [SystemBootup], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var transition = this.Transitions[EnumMacLoadPortTransition.AlarmResetStart_AlarmResetIng.ToString()];
            var state = transition.StateFrom;
            state.ExecuteCommandAtEntry(new MacStateEntryEventArgs());

        }

        /// <summary></summary>
        /// <remarks>
        /// Idle(OnExit) => IdleForGetPOD(OnEntry)
        /// </remarks>
        public void ToGetPOD()
        {
            Debug.WriteLine("Command: [ToGetPOD], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var transition = this.Transitions[EnumMacLoadPortTransition.Idle_IdleForGetPOD.ToString()];

#if GNotCareState
            var state = transition.StateFrom;
#else
            var state = this.CurrentState;
#endif
            state.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }

        /// <summary>Dock (No Mask)</summary>
        /// <remarks>
        /// IdleForGetPOD(OnExit)=> Dock[Start, Ing, Complete]IdleForGetMask(OnEntry)
        /// </remarks>
        public void Dock()
        {
            Debug.WriteLine("Command: [Dock], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var transition = this.Transitions[EnumMacLoadPortTransition.IdleForGetPOD_DockStart.ToString()];
#if GNotCareState
            var state = transition.StateFrom;

#else
            var state = this.CurrentState;
#endif
            state.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());

        }


        /// <summary></summary>
        /// <remarks>
        /// IdleForGetMask(OnExit) => UndockWithMask(start, ing, complete) => IdleForReleasePODWithMask(OnEntry)
        /// </remarks>
        public void UndockWithMaskFromIdleForGetMask()
        {
            Debug.WriteLine("Command: [UndockWithMaskFromIdleForGetMask], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var transition = this.Transitions[EnumMacLoadPortTransition.IdleForGetMask_UndockWithMaskStart.ToString()];
#if GNotCareState
            var state = transition.StateFrom;

#else
            var state = this.CurrentState;
#endif
            state.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// IdleForReleasePODWithMask(OnExit) => Idle(OnEntry)
        /// </remarks>
        public void ReleasePODWithMask()
        {
            Debug.WriteLine("Command: [ReleasePODWithMask], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var transition = this.Transitions[EnumMacLoadPortTransition.IdleForReleasePODWithMask_Idle.ToString()];
#if GNotCareState
            var state = transition.StateFrom;

#else
            var state = this.CurrentState;
#endif
            state.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }

        /// <summary> </summary>
        /// <remarks>
        /// Idle(OnExit) => IdleForGetPODWithMask(OnExit) 
        /// </remarks>
        public void ToGetPODWithMask()
        {
            Debug.WriteLine("Command: [ToGetPODWithMask], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var transition = this.Transitions[EnumMacLoadPortTransition.Idle_IdleForGetPODWithMask.ToString()];

#if GNotCareState
            var state = transition.StateFrom;
#else
            var state = this.CurrentState;
#endif
            state.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }

        /// <summary></summary>
        /// <remarks>
        /// IdleForGetPODWithMask(OnExit) => DockWithMask[start, Ing, Complete] => IdleForReleaseMask(OnEntry)
        /// </remarks>
        public void DockWithMask()
        {
            Debug.WriteLine("Command: [DockWithMask], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var transition = this.Transitions[EnumMacLoadPortTransition.IdleForGetPODWithMask_DockWithMaskStart.ToString()];

#if GNotCareState
            var state = transition.StateFrom;
#else
            var state = this.CurrentState;
#endif
            state.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }


        /// <summary></summary>
        /// <remarks>
        /// IdleForReleaseMask(OnExit) => Undock[Start, Ing, Complete] => IdleForReleasePOD(OnEntry) 
        /// </remarks>
        public void UndockFromIdleForRelesaseMask()
        {
            Debug.WriteLine("Command: [UndockFromIdleForRelesaseMask], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var transition = this.Transitions[EnumMacLoadPortTransition.IdleForReleaseMask_UndockStart.ToString()];

#if GNotCareState
            var state = transition.StateFrom;
#else
            var state = this.CurrentState;
#endif
            state.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }

        /// <summary></summary>
        /// <remarks>
        /// IdleForReleasePOD(OnExit)=> Idle(OnEntry) 
        /// </remarks>
        public void ReleasePOD()
        {
            Debug.WriteLine("Command: [ReleasePOD], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var transition = this.Transitions[EnumMacLoadPortTransition.IdleForReleasePOD_Idle.ToString()];

#if GNotCareState
            var state = transition.StateFrom;
#else
            var state = this.CurrentState;
#endif
            state.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }

        /// <summary></summary>
        /// <remarks>
        /// IdleForReleaseMask(OnExit)=>UndockWithMask[Start, Ing, Complete ] =>IdleForReleasePODWithMask(OnEntry, ......)
        /// </remarks>
        public void UndockWithMaskFromIdleForRelesaseMask()
        {
            Debug.WriteLine("Command: [UndockWithMaskFromIdleForRelesaseMask], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var transition = this.Transitions[EnumMacLoadPortTransition.IdleForReleaseMask_UndockWithMaskStart.ToString()];

#if GNotCareState
            var state = transition.StateFrom;
#else
            var state = this.CurrentState;
#endif
            state.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }

        /// <summary></summary>
        /// <remarks>
        /// IdleForReleaseMask(OnExit) => Undock[State, Ing, Complete]
        /// </remarks>
        public void UndockFromIdleForGetMask()
        {
            Debug.WriteLine("Command: [UndockFromIdleForGetMask], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var transition = this.Transitions[EnumMacLoadPortTransition.ToIdleForGetMask_UndockStart.ToString()];

#if GNotCareState
            var state = transition.StateFrom;
#else
            var state = this.CurrentState;
#endif
            state.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }



        [Obsolete]
        public void GetPOD()
        {
            /**
            // Transition:  Idle => IdleForGetPOD
            MacTransition transition = this.Transitions[""];
            */
        }

        [Obsolete]
        public void GetPODWithMask()
        {
            /**
            // Transition: Idle=> IdleForGetPODWithMask
            MacTransition transition = this.Transitions[""];
            */
        }

        [Obsolete]
        /// <summary>Alarm Reset</summary>
        public void AlarmReset()
        {
            /**
            Debug.WriteLine("Command: [AlarmReset], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var state = this.States[EnumMacLoadPortState.AlarmResetStart.ToString()];
            state.ExecuteCommand(new MacStateEntryEventArgs());
            */
        }
        [Obsolete]
        public void Inintial()
        {
            /**
            Debug.WriteLine("Command: [Inintial], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var state = this.States[EnumMacLoadPortState.InitialStart.ToString()];
            state.ExecuteCommand(new MacStateEntryEventArgs());
            */
        }


        [Obsolete]
        public void Undock()
        {
            /**
            Debug.WriteLine("Command: [Undock], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var state = this.States[EnumMacLoadPortState.UndockStart.ToString()];
            state.ExecuteCommand(new MacStateEntryEventArgs());
            */
        }


        #endregion command

        public override void LoadStateMachine()
        {

            #region State


            MacState sAlarmResetStart = NewState(EnumMacLoadPortState.AlarmResetStart);
            MacState sAlarmResetIng = NewState(EnumMacLoadPortState.AlarmResetIng);
            MacState sAlarmResetComplete = NewState(EnumMacLoadPortState.AlarmResetComplete);
            MacState sInitialStart = NewState(EnumMacLoadPortState.InitialStart);
            MacState sInitialIng = NewState(EnumMacLoadPortState.InitialIng);
            MacState sInitialComplete = NewState(EnumMacLoadPortState.InitialComplete);

            MacState sIdle = NewState(EnumMacLoadPortState.Idle);

            MacState sIdleForGetPOD = NewState(EnumMacLoadPortState.IdleForGetPOD);  // 

            MacState sIdleForGetPODWithMask = NewState(EnumMacLoadPortState.IdleForGetPODWithMask);

            MacState sDockStart = NewState(EnumMacLoadPortState.DockStart);
            MacState sDockIng = NewState(EnumMacLoadPortState.DockIng);
            MacState sDockComplete = NewState(EnumMacLoadPortState.DockComplete);

            MacState sDockWithMaskStart = NewState(EnumMacLoadPortState.DockWithMaskStart);
            MacState sDockWithMaskIng = NewState(EnumMacLoadPortState.DockWithMaskIng);
            MacState sDockWithMaskComplete = NewState(EnumMacLoadPortState.DockWithMaskComplete);

            MacState sIdleForGetMask = NewState(EnumMacLoadPortState.IdleForGetMask);

            MacState sIdleForReleaseMask = NewState(EnumMacLoadPortState.IdleForReleaseMask);

            MacState sUndockWithMaskStart = NewState(EnumMacLoadPortState.UndockWithMaskStart); ;
            MacState sUndockWithMaskIng = NewState(EnumMacLoadPortState.UndockWithMaskIng); ;
            MacState sUndockWithMaskComplete = NewState(EnumMacLoadPortState.UndockWithMaskComplete); ;

            MacState sUndockStart = NewState(EnumMacLoadPortState.UndockStart); ;
            MacState sUndockIng = NewState(EnumMacLoadPortState.UndockIng); ;
            MacState sUndockComplete = NewState(EnumMacLoadPortState.UndockComplete); ;

            MacState sIdleForReleasePODWithMask = NewState(EnumMacLoadPortState.IdleForReleasePODWithMask); ;
            MacState sIdleForReleasePOD = NewState(EnumMacLoadPortState.IdleForReleasePOD); ;
            #endregion

            #region Transition
            // Command: SystemStartUp
            MacTransition tAlarmResetStart_AlarmResetIng = NewTransition(sAlarmResetStart, sAlarmResetIng, EnumMacLoadPortTransition.AlarmResetStart_AlarmResetIng);
            MacTransition tAlarmResetIng_AlarmResetComplete = NewTransition(sAlarmResetIng, sAlarmResetComplete, EnumMacLoadPortTransition.AlarmResetIng_AlarmResetComplete);
            MacTransition tAlarmResetComplete_InitialStart = NewTransition(sAlarmResetComplete, sInitialStart, EnumMacLoadPortTransition.AlarmResetComplete_InitialStart);
            MacTransition tInitialStart_InitialIng = NewTransition(sInitialStart, sInitialIng, EnumMacLoadPortTransition.InitialStart_InitialIng);
            MacTransition tInitialIng_InitialComplete = NewTransition(sInitialIng, sInitialComplete, EnumMacLoadPortTransition.InitialIng_InitialComplete);
            MacTransition tInitialComplete_Idle = NewTransition(sInitialComplete, sIdle, EnumMacLoadPortTransition.InitialComplete_Idle);
            MacTransition tIdle_NULL = NewTransition(sIdle, null, EnumMacLoadPortTransition.Idle_NULL);

            // Command: ToGetPOD
            MacTransition tIdle_IdleForGetPOD = NewTransition(sIdle, sIdleForGetPOD, EnumMacLoadPortTransition.Idle_IdleForGetPOD);
            MacTransition tIdleForGetPOD_NULL = NewTransition(sIdleForGetPOD, null, EnumMacLoadPortTransition.IdleForGetPOD_NULL);

            // Command: ToGetPODWithMask
            MacTransition tIdle_IdleForGetPODWithMask = NewTransition(sIdle, sIdleForGetPODWithMask, EnumMacLoadPortTransition.Idle_IdleForGetPODWithMask);
            MacTransition tIdleForGetPODWithMask_NULL = NewTransition(sIdleForGetPODWithMask, null, EnumMacLoadPortTransition.IdleForGetPODWithMask_NULL);

            // Command: Dock
            MacTransition tIdleForGetPOD_DockStart = NewTransition(sIdleForGetPOD, sDockStart, EnumMacLoadPortTransition.IdleForGetPOD_DockStart);
            MacTransition tDockStart_DockIng = NewTransition(sDockStart, sDockIng, EnumMacLoadPortTransition.DockStart_DockIng);
            MacTransition tDockIng_DockComplete = NewTransition(sDockIng, sDockComplete, EnumMacLoadPortTransition.DockIng_DockComplete);
            MacTransition tDockComplete_IdleForGetMask = NewTransition(sDockComplete, sIdleForGetMask, EnumMacLoadPortTransition.DockComplete_IdleForGetMask);
            MacTransition tIdleForGetMask_NULL = NewTransition(sIdleForGetMask, null, EnumMacLoadPortTransition.IdleForGetMask_NULL);//   .DockComplete_NULL);

            // Command: DockWithMask
            MacTransition tIdleForGetPODWithMask_DockWithMaskStart = NewTransition(sIdleForGetPODWithMask, sDockWithMaskStart, EnumMacLoadPortTransition.IdleForGetPODWithMask_DockWithMaskStart);
            MacTransition tDockWithMaskStart_DockWithMaskIng = NewTransition(sDockWithMaskStart, sDockWithMaskIng, EnumMacLoadPortTransition.DockWithMaskStart_DockWithMaskIng);
            MacTransition tDockWithMaskIng_DockWithMaskComplete = NewTransition(sDockWithMaskIng, sDockWithMaskComplete, EnumMacLoadPortTransition.DockWithMaskIng_DockWithMaskComplete);
            MacTransition tDockWithMaskComplete_IdleForReleaseMask = NewTransition(sDockWithMaskComplete, sIdleForReleaseMask, EnumMacLoadPortTransition.DockWithMaskComplete_IdleForReleaseMask);
            MacTransition tIdleForReleaseMask_NULL = NewTransition(sIdleForReleaseMask, null, EnumMacLoadPortTransition.IdleForReleaseMask_NULL);

            // Command: UndockWithMaskFromIdleForGetMask(V)
            MacTransition tIdleForGetMask_UndockWithMaskStart = NewTransition(sIdleForGetMask, sUndockWithMaskStart, EnumMacLoadPortTransition.IdleForGetMask_UndockWithMaskStart);
            MacTransition tUndockWithMaskStart_UndockWithMaskIng = NewTransition(sUndockWithMaskStart, sUndockWithMaskIng, EnumMacLoadPortTransition.UndockWithMaskStart_UndockWithMaskIng);
            MacTransition tUndockWithMaskIng_UndockWithMaskComplete = NewTransition(sUndockWithMaskIng, sUndockWithMaskComplete, EnumMacLoadPortTransition.UndockWithMaskIng_UndockWithMaskComplete);
            MacTransition tUndockWithMaskComplete_IdleForReleasePODWithMask = NewTransition(sUndockWithMaskComplete, sIdleForReleasePODWithMask, EnumMacLoadPortTransition.UndockWithMaskComplete_IdleForReleasePODWithMask);
            MacTransition tIdleForReleasePODWithMask_NULL = NewTransition(sIdleForReleasePODWithMask, null, EnumMacLoadPortTransition.IdleForReleasePODWithMask_NULL);


            // Command: UndockFromIdleForRelesaseMask(O)
            MacTransition tIdleForReleaseMask_UndockStart = NewTransition(sIdleForReleaseMask, sUndockStart, EnumMacLoadPortTransition.IdleForReleaseMask_UndockStart);
            MacTransition tUndockStart_UndockIng = NewTransition(sUndockStart, sUndockIng, EnumMacLoadPortTransition.UndockStart_UndockIng);
            MacTransition tUndockIng_UndockComplete = NewTransition(sUndockIng, sUndockComplete, EnumMacLoadPortTransition.tUndockIng_UndockComplete);
            MacTransition tUndockComplete_IdleForReleasePOD = NewTransition(sUndockComplete, sIdleForReleasePOD, EnumMacLoadPortTransition.UndockComplete_IdleForReleasePOD);
            MacTransition tIdleForReleasePOD_NULL = NewTransition(sIdleForReleasePOD, null, EnumMacLoadPortTransition.IdleForReleasePOD_NULL        );//IdleForReleasePOD_UndockStart);


            // Command: UndockWithMaskFromIdleForRelesaseMask(X)
            MacTransition tIdleForReleaseMask_UndockWithMaskStart = NewTransition(sIdleForReleaseMask, sUndockWithMaskStart, EnumMacLoadPortTransition.IdleForReleaseMask_UndockWithMaskStart);


            // Command: UndockFromIdleForGetMask(@)
            MacTransition tIdleForGetMask_UndockStart = NewTransition(sIdleForGetMask, sUndockStart, EnumMacLoadPortTransition.ToIdleForGetMask_UndockStart);


            // Command: ReleasePODWithMask
            MacTransition tIdleForReleasePODWithMask_Idle = NewTransition(sIdleForReleasePODWithMask, sIdle, EnumMacLoadPortTransition.IdleForReleasePODWithMask_Idle);
            // Command: ReleasePOD
            MacTransition tIdleForReleasePOD_Idle = NewTransition(sIdleForReleasePOD, sIdle, EnumMacLoadPortTransition.IdleForReleasePOD_Idle);
            #endregion Transition

            #region  Register OnEntry, OnExit Event Handler

            sAlarmResetStart.OnEntry += (sender, e) =>
            {// Sync
                Debug.WriteLine("State: [sAlarmResetStart.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tAlarmResetStart_AlarmResetIng;
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
            sAlarmResetStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sAlarmResetStart.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
            sAlarmResetIng.OnEntry += (sender, e) =>
            { // Async
                Debug.WriteLine("State: [sAlarmResetIng.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tAlarmResetIng_AlarmResetComplete;
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
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);

            };
            sAlarmResetIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sAlarmResetIng.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
            sAlarmResetComplete.OnEntry += (sender, e) =>
            {// Sync
                Debug.WriteLine("State: [sAlarmResetComplete.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tAlarmResetComplete_InitialStart;
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
                this.Trigger(transition);
            };
            sAlarmResetComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sAlarmResetComplete.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
            sInitialStart.OnEntry += (sender, e) =>
            {  // Sync 
                Debug.WriteLine("State: [sInitialStart.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tInitialStart_InitialIng;
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
            sInitialStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sInitialStart.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
            sInitialIng.OnEntry += (sender, e) =>
            {// Async
                Debug.WriteLine("State: [sInitialIng.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tInitialIng_InitialComplete;
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
                            throw new LoadportInitialMustResetException();
                        }
                        else if (this.TimeoutObject.IsTimeOut(startTime))
                        {
                            throw new LoadportInitialTimeOutException();
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
            sInitialIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sInitialIng.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
            sInitialComplete.OnEntry += (sender, e) =>
            { // Sync 
                Debug.WriteLine("State: [sInitialComplete.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tInitialComplete_Idle;
                var triggerMmember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {

                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMmember);
                Trigger(transition);

            };
            sInitialComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sInitialComplete.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
            sIdle.OnEntry += (sender, e) =>
            {  // Sync
                Debug.WriteLine("State: [sIdle.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tIdle_NULL;
                var triggerMmember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {

                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMmember);
                Trigger(transition);


            };
            sIdle.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sIdle.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };

            sIdleForGetPOD.OnEntry += (sender, e) =>
            {  // Sync
                Debug.WriteLine("State: [sIdleForGetPOD.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tIdleForGetPOD_NULL;
                var triggerMmember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {

                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMmember);
                Trigger(transition);

            };
            sIdleForGetPOD.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sIdleForGetPOD.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
            sIdleForGetPODWithMask.OnEntry += (sender, e) =>
            {  // Sync
                Debug.WriteLine("State: [sIdleForGetPODWithMask.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tIdleForGetPODWithMask_NULL;
                var triggerMmember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {

                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMmember);
                Trigger(transition);

            };
            sIdleForGetPODWithMask.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sIdleForGetPODWithMask.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
            sDockStart.OnEntry += (sender, e) =>
            {
                // Sync
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
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()

                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sDockStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sDockStart.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
            sDockIng.OnEntry += (sender, e) =>
            {   // Async
                Debug.WriteLine("State: [sDockIng.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                        if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.DockComplete)
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
                Debug.WriteLine("State: [sDockIng.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
            sDockComplete.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sDockComplete.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sDockComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sDockComplete.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
            sIdleForGetMask.OnEntry += (sender, e) =>
             {
                 Debug.WriteLine("State: [sIdleForGetMask.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                 var transition = tIdleForGetMask_NULL;
                 SetCurrentState((MacState)sender);
                 var triggerMember = new TriggerMember
                 {
                     Action = null,
                     ActionParameter = null,
                     ExceptionHandler = (state, ex) =>
                     {
                         // TODO : do something
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
                Debug.WriteLine("State: [sIdleForGetMask.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };

            sUndockWithMaskStart.OnEntry += (sender, e) =>
            {

                // Sync
                Debug.WriteLine("State: [sUndockWithMaskStart.OnEntry] Index: " + this.HalLoadPortUnit.DeviceIndex);
                var transition = tUndockWithMaskStart_UndockWithMaskIng;
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
            sUndockWithMaskStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sUndockWithMaskStart.OnExit] Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
            sUndockWithMaskIng.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sUndockWithMaskIng.OnEntry] Index: " + this.HalLoadPortUnit.DeviceIndex);
                // Async
                var transition = tUndockWithMaskIng_UndockWithMaskComplete;
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
                            throw new LoadportUndockWithMaskMustInitialException();
                        }
                        else if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.MustResetFirst)
                        {
                            throw new LoadportUndockWithMaskMustResetException();
                        }
                        else if (this.TimeoutObject.IsTimeOut(startTime))
                        {
                            throw new LoadportUndockWithMaskTimeOutException();
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
            sUndockWithMaskIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sUndockWithMaskIng.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
            sUndockWithMaskComplete.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sUndockWithMaskComplete.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                var transition = tUndockWithMaskComplete_IdleForReleasePODWithMask;
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
            sUndockWithMaskComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sUndockWithMaskComplete.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };


            sIdleForReleasePODWithMask.OnEntry += (sender, e) =>
            {  // Sync
                Debug.WriteLine("State: [sIdleForReleasePODWithMask.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                var transition = tIdleForReleasePODWithMask_NULL;
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
            sIdleForReleasePODWithMask.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sIdleForReleasePODWithMask.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
            sDockWithMaskStart.OnEntry += (sender, e) =>
            {
                // Sync
                Debug.WriteLine("State: [sDockWithMaskStart.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                var transition = tDockWithMaskStart_DockWithMaskIng;
                SetCurrentState((MacState)sender);
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { this.HalLoadPortUnit.CommandDockRequest(); },
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
            sDockWithMaskStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sDockWithMaskStart.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
            sDockWithMaskIng.OnEntry += (sender, e) =>
            {
                // Async
                Debug.WriteLine("State: [sDockWithMaskIng.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                SetCurrentState((MacState)sender);

                var transition = tDockWithMaskIng_DockWithMaskComplete;
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
                        if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.DockComplete)
                        {
                            return true;
                        }
                        else if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.MustInitialFirst)
                        {
                            throw new LoadportDockWithMaskMustInitialException();
                        }
                        else if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.MustResetFirst)
                        {
                            throw new LoadportDockWithMaskMustResetException();
                        }
                        else if (this.TimeoutObject.IsTimeOut(startTime))
                        {
                            throw new LoadportDockWithMaskTimeOutException();
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
            sDockWithMaskIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sDockWithMaskIng.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };

            sDockWithMaskComplete.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sDockWithMaskComplete.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                var transition = tDockWithMaskComplete_IdleForReleaseMask;
                SetCurrentState((MacState)sender);
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO : do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sDockWithMaskComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sDockWithMaskComplete.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
            sIdleForReleaseMask.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sIdleForReleaseMask.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                var transition = tIdleForReleaseMask_NULL;
                SetCurrentState((MacState)sender);
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO : do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sIdleForReleaseMask.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sIdleForReleaseMask.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };

            sUndockStart.OnEntry += (sender, e) =>
            {
                // Sync
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
                Debug.WriteLine("State: [sUndockIng.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
            sUndockComplete.OnEntry += (sender, e) =>
            {
                // Sync 
                Debug.WriteLine("State: [sUndockComplete.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                var transition = tUndockComplete_IdleForReleasePOD;
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
                Debug.WriteLine("State: [sUndockComplete.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
            sIdleForReleasePOD.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sIdleForReleasePOD.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                var transition = tIdleForReleasePOD_NULL;
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
            sIdleForReleasePOD.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sIdleForReleasePOD.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
            #endregion


        }
    }

}
