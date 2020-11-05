//#define NoConfig
#define NotCareState
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
            var transition = this.Transitions[EnumMacMsLoadPortTransition.TriggerToAlarmResetStart_AlarmResetIng.ToString()];
            var state = transition.StateFrom;
            state.ExecuteCommandAtEntry(new MacStateEntryEventArgs());

        }

        /// <summary></summary>
        /// <remarks>
        /// Idle(OnExit) => IdleForGetPOD(OnEntry)
        /// </remarks>
        public void  ToGetPOD()
        {
            Debug.WriteLine("Command: [ToGetPOD], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var transition = this.Transitions[EnumMacMsLoadPortTransition.TriggerToIdle_IdleForGetPOD.ToString()];

#if NotCareState
            var state = transition.StateFrom;
#else
            var state=this.CurrentState;
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
            var transition = this.Transitions[EnumMacMsLoadPortTransition.TriggerToIdleForGetPOD_DockStart.ToString()];
#if NotCareState
            var state = transition.StateFrom;

#else
           var state=this.CurrentState;
#endif
            state.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
          
        }


        /// <summary></summary>
        /// <remarks>
        /// IdleForGetMask(OnExit) => UndockWithMask(start, ing, complete) => IdleForReleasePODWithMask(OnEntry)
        /// </remarks>
        public void  UndockWithMaskFromIdleForGetMask()
        {
            Debug.WriteLine("Command: [UndockWithMaskFromIdleForGetMask], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var transition = this.Transitions[EnumMacMsLoadPortTransition.TriggerToIdleForGetMask_UndockWithMaskStart.ToString()];
#if NotCareState
            var state = transition.StateFrom;

#else
           var state=this.CurrentState;
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
            var transition = this.Transitions[EnumMacMsLoadPortTransition.TriggerToIdleForReleasePODWithMask_Idle.ToString()];
#if NotCareState
            var state = transition.StateFrom;

#else
           var state=this.CurrentState;
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
            var transition = this.Transitions[EnumMacMsLoadPortTransition.TriggerToIdle_IdleForGetPODWithMask.ToString()];

#if NotCareState
            var state = transition.StateFrom;
#else
            var state=this.CurrentState;
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
            var transition = this.Transitions[EnumMacMsLoadPortTransition.TriggerToIdleForGetPODWithMask_DockWithMaskStart.ToString()];

#if NotCareState
            var state = transition.StateFrom;
#else
            var state=this.CurrentState;
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
            var transition = this.Transitions[EnumMacMsLoadPortTransition.TriggerToIdleForGetMask_UndockStart.ToString()];

#if NotCareState
            var state = transition.StateFrom;
#else
            var state=this.CurrentState;
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
            var transition = this.Transitions[EnumMacMsLoadPortTransition.TriggerToIdleForReleasePOD_Idle.ToString()];

#if NotCareState
            var state = transition.StateFrom;
#else
            var state=this.CurrentState;
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
            var transition = this.Transitions[EnumMacMsLoadPortTransition.TriggerToIdleForReleaseMask_UndockWithMaskStart.ToString()];

#if NotCareState
            var state = transition.StateFrom;
#else
            var state=this.CurrentState;
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
            var transition = this.Transitions[EnumMacMsLoadPortTransition.TriggerToIdleForReleaseMask_UndockStart.ToString()];

#if NotCareState
            var state = transition.StateFrom;
#else
            var state=this.CurrentState;
#endif
            state.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }

//888888

        public void GetPOD()
        {
            /**
            // Transition:  Idle => IdleForGetPOD
            MacTransition transition = this.Transitions[""];
            */
        }
        public void GetPODWithMask()
        {
            /**
            // Transition: Idle=> IdleForGetPODWithMask
            MacTransition transition = this.Transitions[""];
            */
        }


        /// <summary>Alarm Reset</summary>
        public void AlarmReset()
        {
            /**
            Debug.WriteLine("Command: [AlarmReset], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var state = this.States[EnumMacMsLoadPortState.AlarmResetStart.ToString()];
            state.ExecuteCommand(new MacStateEntryEventArgs());
            */
        }

        public void Inintial()
        {
            /**
            Debug.WriteLine("Command: [Inintial], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var state = this.States[EnumMacMsLoadPortState.InitialStart.ToString()];
            state.ExecuteCommand(new MacStateEntryEventArgs());
            */
        }

        /**
        public void Dock()
        {
          
            Debug.WriteLine("Command: [Dock], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var state = this.States[EnumMacMsLoadPortState.DockStart.ToString()];
            state.ExecuteCommand(new MacStateEntryEventArgs());
            
        }
        */
        public void Undock()
        {
            /**
            Debug.WriteLine("Command: [Undock], Index:" + this.HalLoadPortUnit.DeviceIndex);
            var state = this.States[EnumMacMsLoadPortState.UndockStart.ToString()];
            state.ExecuteCommand(new MacStateEntryEventArgs());
            */
        }


        #endregion command

        public override void LoadStateMachine()
        {

            #region State
            // Any State
            MacState AnyState = null;


            MacState sAlarmResetStart = NewState(EnumMacMsLoadPortState.AlarmResetStart);
            MacState sAlarmResetIng = NewState(EnumMacMsLoadPortState.AlarmResetIng);
            MacState sAlarmResetComplete = NewState(EnumMacMsLoadPortState.AlarmResetComplete);
            MacState sInitialStart = NewState(EnumMacMsLoadPortState.InitialStart);
            MacState sInitialIng = NewState(EnumMacMsLoadPortState.InitialIng);
            MacState sInitialComplete = NewState(EnumMacMsLoadPortState.InitialComplete);

            MacState sIdle = NewState(EnumMacMsLoadPortState.Idle);

            MacState sIdleForGetPOD = NewState(EnumMacMsLoadPortState.IdleForGetPOD);  // 

            MacState sIdleForGetPODWithMask = NewState(EnumMacMsLoadPortState.IdleForGetPODWithMask);

            MacState sDockStart = NewState(EnumMacMsLoadPortState.DockStart);
            MacState sDockIng = NewState(EnumMacMsLoadPortState.DockIng);
            MacState sDockComplete = NewState(EnumMacMsLoadPortState.DockComplete);

            MacState sDockWithMaskStart = NewState(EnumMacMsLoadPortState.DockStartWithMaskStart);
            MacState sDockWithMaskIng = NewState(EnumMacMsLoadPortState.DockStartWithMaskIng);
            MacState sDockWithMaskComplete = NewState(EnumMacMsLoadPortState.DockStartWithMaskComplete);

            MacState sIdleForGetMask = NewState(EnumMacMsLoadPortState.IdleForReleasePOD);

            MacState sIdleForReleaseMask = NewState(EnumMacMsLoadPortState.IdleForReleasePOD);

            MacState sUndockWithMaskStart = NewState(EnumMacMsLoadPortState.UndockWithMaskStart); ;
            MacState sUndockWithMaskIng = NewState(EnumMacMsLoadPortState.UndockWithMaskIng); ;
            MacState sUndockWithMaskComplete = NewState(EnumMacMsLoadPortState.UndockWithMaskComplete); ;

            MacState sUndockStart = NewState(EnumMacMsLoadPortState.UndockStart); ;
            MacState sUndockIng = NewState(EnumMacMsLoadPortState.UndockIng); ;
            MacState sUndockComplete = NewState(EnumMacMsLoadPortState.UndockComplete); ;

            MacState sIdleForReleasePODWithMask = NewState(EnumMacMsLoadPortState.IdleForReleasePOD); ;
            MacState sIdleForReleasePOD = NewState(EnumMacMsLoadPortState.IdleForReleasePOD); ;
            #endregion

            #region Transition
            // Command: SystemStartUp
            MacTransition tAlarmResetStart_AlarmResetIng = NewTransition(sAlarmResetStart, sAlarmResetIng, EnumMacMsLoadPortTransition.TriggerToAlarmResetStart_AlarmResetIng);
            MacTransition tAlarmResetIng_AlarmResetComplete = NewTransition(sAlarmResetIng, sAlarmResetComplete, EnumMacMsLoadPortTransition.AlarmResetIng_AlarmResetComplete);
            MacTransition tAlarmResetComplete_InitialStart = NewTransition(sAlarmResetComplete, sInitialStart, EnumMacMsLoadPortTransition.AlarmResetComplete_InitialStart);
            MacTransition tInitialStart_InitialIng = NewTransition(sInitialStart, sInitialIng, EnumMacMsLoadPortTransition.InitialStart_InitialIng);
            MacTransition tInitialIng_InitialComplete = NewTransition(sInitialIng, sInitialComplete, EnumMacMsLoadPortTransition.InitialIng_InitialComplete);
            MacTransition tInitialComplete_Idle = NewTransition(sInitialComplete, sIdle, EnumMacMsLoadPortTransition.InitialComplete_Idle);
            MacTransition tIdle_NULL = NewTransition(sIdle, null, EnumMacMsLoadPortTransition.Idle_NULL);

            // Command: ToGetPOD
            MacTransition tIdle_IdleForGetPOD = NewTransition(sIdle, sIdleForGetPOD, EnumMacMsLoadPortTransition.TriggerToIdle_IdleForGetPOD);
            MacTransition tIdleForGetPOD_NULL = NewTransition(sIdleForGetPOD, null, EnumMacMsLoadPortTransition.IdleForGetPOD_NULL);

            // Command: ToGetPODWithMask
            MacTransition tIdle_IdleForGetPODWithMask = NewTransition(sIdle, sIdleForGetPODWithMask, EnumMacMsLoadPortTransition.TriggerToIdle_IdleForGetPODWithMask);
            MacTransition tIdleForGetPODWithMask_NULL = NewTransition(sIdleForGetPODWithMask, null, EnumMacMsLoadPortTransition.IdleForGetPODWithMask_NULL);

            // Command: Dock
            MacTransition tIdleForGetPOD_DockStart = NewTransition(sIdleForGetPOD, sDockStart, EnumMacMsLoadPortTransition.IdleForGetPOD_DockStart);
            MacTransition tDockStart_DockIng = NewTransition(sDockStart, sDockIng, EnumMacMsLoadPortTransition.DockStart_DockIng);
            MacTransition tDockIng_DockComplete = NewTransition(sDockIng, sDockComplete, EnumMacMsLoadPortTransition.DockWithMaskIng_DockWithMaskComplete);
            MacTransition tDockComplete_IdleForGetMask = NewTransition(sDockComplete, sIdleForGetMask, EnumMacMsLoadPortTransition.DockComplete_IdleForGetMask);
            MacTransition tIdleForGetMask_NULL = NewTransition(sIdleForGetMask, null, EnumMacMsLoadPortTransition.DockComplete_NULL);

            // Command: DockWithMask
            MacTransition tIdleForGetPODWithMask_DockWithMaskStart = NewTransition(sIdleForGetPODWithMask, sDockWithMaskStart, EnumMacMsLoadPortTransition.TriggerToIdleForGetPODWithMask_DockWithMaskStart);
            MacTransition tDockWithMaskStart_DockWithMaskIng = NewTransition(sDockWithMaskStart, sDockWithMaskIng, EnumMacMsLoadPortTransition.DockWithMaskStart_DockWithMaskIng);
            MacTransition tDockWithMaskIng_DockWithMaskComplete = NewTransition(sDockWithMaskIng, sDockWithMaskComplete, EnumMacMsLoadPortTransition.DockWithMaskIng_DockWithMaskComplete);
            MacTransition tDockWithMaskComplete_IdleForReleaseMask = NewTransition(sDockWithMaskComplete, sIdleForReleaseMask, EnumMacMsLoadPortTransition.IdleForReleaseMask_NULL);
            MacTransition tIdleForReleaseMask_NULL = NewTransition(sIdleForReleaseMask, null, EnumMacMsLoadPortTransition.IdleForReleaseMask_NULL);

            // Command: UndockWithMaskFromIdleForGetMask(V)
            MacTransition tIdleForGetMask_UndockWithMaskStart = NewTransition(sIdleForGetMask, sUndockWithMaskStart, EnumMacMsLoadPortTransition.TriggerToIdleForGetMask_UndockWithMaskStart);
            MacTransition tUndockWithMaskStart_UndockWithMaskIng = NewTransition(sUndockWithMaskStart, sUndockWithMaskIng, EnumMacMsLoadPortTransition.UndockWithMaskStart_UndockWithMaskIng);
            MacTransition tUndockWithMaskIng_UndockWithMaskComplete = NewTransition(sUndockWithMaskIng, sUndockWithMaskComplete, EnumMacMsLoadPortTransition.UndockWithMaskIng_UndockWithMaskComplete);
            MacTransition tUndockWithMaskComplete_IdleForReleasePODWithMask = NewTransition(sUndockWithMaskComplete, sIdleForReleasePODWithMask, EnumMacMsLoadPortTransition.UndockWithMaskComplete_IdleForReleasePODWithMask);
            MacTransition tIdleForReleasePODWithMask_NULL = NewTransition(sIdleForReleasePODWithMask, null, EnumMacMsLoadPortTransition.IdleForReleasePODWithMask_NULL);


            // Command: UndockFromIdleForRelesaseMask(O)
            MacTransition tIdleForReleaseMask_UndockStart = NewTransition(sIdleForReleaseMask, sUndockStart, EnumMacMsLoadPortTransition.TriggerToIdleForReleaseMask_UndockStart);
            MacTransition tUndockStart_UndockIng = NewTransition(sUndockStart, sUndockIng, EnumMacMsLoadPortTransition.UndockStart_UndockIng);
            MacTransition tUndockIng_UndockComplete = NewTransition(sUndockIng, sUndockComplete, EnumMacMsLoadPortTransition.tUndockIng_UndockComplete);
            MacTransition tUndockComplete_IdleForReleasePOD = NewTransition(sUndockComplete, sIdleForReleasePOD, EnumMacMsLoadPortTransition.UndockComplete_IdleForReleasePOD);
            MacTransition tIdleForReleasePOD_NULL = NewTransition(sIdleForReleasePOD, null, EnumMacMsLoadPortTransition.IdleForReleasePOD_UndockStart);


            // Command: UndockWithMaskFromIdleForRelesaseMask(X)
            MacTransition tIdleForReleaseMask_UndockWithMaskStart = NewTransition(sIdleForReleaseMask, sUndockWithMaskStart, EnumMacMsLoadPortTransition.TriggerToIdleForReleaseMask_UndockWithMaskStart);
            //MacTransition tUndockWithMaskStart_UndockWithMaskIng; // 有了
            //MacTransition tUndockWithMaskIng_UndockWithMaskComplete;// 有了
            //MacTransition tUndockWithMaskComplete_IdleForReleasePODWithMask;//有了
            //MacTransition tIdleForReleasePODWithMask_NULL;//有了

            // Command: UndockFromIdleForGetMask(@)
            MacTransition tIdleForGetMask_UndockStart = NewTransition(sIdleForGetMask, sUndockStart, EnumMacMsLoadPortTransition.TriggerToIdleForGetMask_UndockStart);
            //MacTransition tUndockStart_UndockIng; // 有了
            // MacTransition tUndockIng_UndockComplete; // 有了
            // MacTransition tUndockComplete_IdleForReleasePOD;// 有了
            //MacTransition tIdleForReleasePOD_NULL;// 有了

            // Command: ReleasePODWithMask
            MacTransition tIdleForReleasePODWithMask_Idle = NewTransition(sIdleForReleasePODWithMask, sIdle, EnumMacMsLoadPortTransition.TriggerToIdleForReleasePODWithMask_Idle);
            // Command: ReleasePOD
            MacTransition tIdleForReleasePOD_Idle = NewTransition(sIdleForReleasePOD, sIdle, EnumMacMsLoadPortTransition.TriggerToIdleForReleasePOD_Idle);
            #endregion Transition
            /**
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
            MacState sDockIng = NewState(EnumMacMsLoadPortState.DockIng);
            MacState sDockComplete = NewState(EnumMacMsLoadPortState.DockComplete);
            // dock 等待Robot 夾取光置
            MacState sIdleForGetMask = NewState(EnumMacMsLoadPortState.IdleForGetMask);

            // undock
            MacState sUndockStart = NewState(EnumMacMsLoadPortState.UndockStart);
            MacState sUndockIng = NewState(EnumMacMsLoadPortState.UndockIng);
            MacState sUndockComplete = NewState(EnumMacMsLoadPortState.UndockComplete);
            // Undock 等待POD 被取走
            MacState sIdleForGetPOD = NewState(EnumMacMsLoadPortState.IdleForGetPOD);
      
#endregion State    */

            /**
#region Transition
              // SystemBootUp

              MacTransition tSystemBootup_SystemBootupAlarmResetStart = NewTransition(sSystemBootup, sSystemBootupAlarmResetStart, EnumMacMsLoadPortTransition.SystemBootup_SystemBootupAlarmResetStart);
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
              MacTransition tInitialComplete_IdleForPutPOD = NewTransition(sInitialComplete, sIdleForPutPOD, EnumMacMsLoadPortTransition.InitialComplete_IdleForPutPOD);
              MacTransition tIdleForPutPOD_NULL = NewTransition(sIdleForPutPOD, null, EnumMacMsLoadPortTransition.IdleForPutPOD_NULL);

              // Dock
              MacTransition tDockStart_DockIng = NewTransition(sDockStart, sDockIng, EnumMacMsLoadPortTransition.DockStart_DockIng);
              MacTransition tDockIng_DockComplete = NewTransition(sDockIng, sDockComplete, EnumMacMsLoadPortTransition.DockWithMaskIng_DockWithMaskComplete);
              MacTransition tDockComplete_IdleForGetMask = NewTransition(sDockComplete, sIdleForGetMask, EnumMacMsLoadPortTransition.DockComplete_IdleForGetMask);
              MacTransition tIdleForGetMask_NULL = NewTransition(sIdleForGetMask, null, EnumMacMsLoadPortTransition.IdleForGetMask_NULL);

              // Undock
              MacTransition tUndockStart_UndockIng = NewTransition(sUndockStart, sUndockIng, EnumMacMsLoadPortTransition.UndockStart_UndockIng);
              MacTransition tUndockIng_UndockComplete = NewTransition(sUndockIng, sUndockComplete, EnumMacMsLoadPortTransition.UndockIng_UndockComplete);
              MacTransition tUndockComplete_IdleForGetPOD = NewTransition(sUndockComplete, sIdleForGetPOD, EnumMacMsLoadPortTransition.UndockComplete_IdleForGetPOD);
              MacTransition tIdleForGetPOD_NULL = NewTransition(sIdleForGetPOD, null, EnumMacMsLoadPortTransition.IdleForGetPOD_NULL);
#endregion

           */
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
                Trigger(transition);
            };
            sAlarmResetComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sAlarmResetComplete.Exit], Index: " + this.HalLoadPortUnit.DeviceIndex);
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

            };
            sIdleForGetMask.OnEntry += (sender, e) =>
             {
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

            };

            sUndockWithMaskStart.OnEntry += (sender, e) =>
            {

                // Sync
                Debug.WriteLine("State: [UndockWithMaskStart.OnEntry] Index: " + this.HalLoadPortUnit.DeviceIndex);
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

            };
            sUndockWithMaskIng.OnEntry += (sender, e) =>
            {

                // Async
                var transition = tUndockWithMaskIng_UndockWithMaskComplete;
                Debug.WriteLine("State: [UndockWithMaskIng.OnEntry, Index: " + this.HalLoadPortUnit.DeviceIndex);
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

            };

            sIdleForReleasePODWithMask.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [IdleForReleasePODWithMask.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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

            };
            sIdleForReleasePODWithMask.OnExit += (sender, e) =>
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
            sIdleForReleasePODWithMask.OnExit += (sender, e) => {

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

            };
            sDockWithMaskIng.OnEntry += (sender, e) =>
            {
                // Async
                Debug.WriteLine("State: [DockWithMaskIng.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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

            };

            sDockWithMaskComplete.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sDockComplete.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
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

            };
            sIdleForReleaseMask.OnEntry += (sender, e) =>
            {
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

            };
            sIdleForReleasePOD.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sIdleForReleasePOD.OnEntry], Index: " + this.HalLoadPortUnit.DeviceIndex);
                var transition = tIdleForReleasePOD_NULL;
                SetCurrentState((MacState)sender);
            };
            sIdleForReleasePOD.OnExit += (sender, e) =>
            {

            };
            /** sSystemBootup.OnEntry += (sender, e) =>
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
    */
            /** sSystemBootupAlarmResetStart.OnEntry += (sender, e) =>
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
            */
            /** sSystemBootupAlarmResetIng.OnEntry += (sender, e) =>
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
    */

            /**sSystemBootupAlarmResetComplete.OnEntry += (sender, e) =>
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
    */
            /**sSystemBootupInitialStart.OnEntry += (sender, e) =>
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
            };*/

            /** sSystemBootupInitialIng.OnEntry += (sender, e) =>
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
             };*/
            /**sSystemBootupInitialComplete.OnEntry += (sender, e) =>
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
           */

            /**sAlarmResetStart.OnEntry += (sender, e) =>
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
                    Guard = () => { return true; },
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
    */
            /** sAlarmResetIng.OnEntry += (sender, e) =>
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
                            ThisStateExitEventArgs = new MacStateExitEventArgs()
                        };
                        transition.SetTriggerMembers(triggerMemberAsync);
                        TriggerAsync(transition);
                    };
                    sAlarmResetIng.OnExit += (sender, e) =>
                    {
                        Debug.WriteLine("State: [sAlarmResetIng.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
                        // 視實況新增 Code 
                    };
            */

            /** sAlarmResetComplete.OnEntry += (sender, e) =>
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
          */
            /** sInitialStart.OnEntry += (sender, e) =>
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
                      Guard = () => { return true; },
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
      */
            /** sInitialIng.OnEntry += (sender, e) =>
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
                              else if (this.TimeoutObject.IsTimeOut(startTime))
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
          */
            /** sInitialComplete.OnEntry += (sender, e) =>
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

            */
            /**sIdleForPutPOD.OnEntry += (sender, e) =>
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
           */
            /**sDockStart.OnEntry += (sender, e) =>
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
                 // 視狀況增加 Code
                 Debug.WriteLine("State: [sDockStart.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
             };
     */

            /**sDockIng.OnEntry += (sender, e) =>
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
                // 視狀況增加 Code
                Debug.WriteLine("State: [sDockIng.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
            };
            */
            /** sDockComplete.OnEntry += (sender, e) =>
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
                // 視狀況加 Code
                Debug.WriteLine("State: [sDockComplete.OnExit], Index: " + this.HalLoadPortUnit.DeviceIndex);
             };
            */
            /**sIdleForGetMask.OnEntry += (sender, e) =>
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

    */
            /** sUndockStart.OnEntry += (sender, e) =>
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
             };*/

            /**  sUndockIng.OnEntry += (sender, e) =>
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
    */
            /** sUndockComplete.OnEntry += (sender, e) =>
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
            */
            /**sIdleForGetPOD.OnEntry += (sender, e) =>
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
            };*/
            #endregion


        }
    }

}
