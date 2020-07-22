using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.Mac.v1_0.Hal.CompLoadPort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.LoadPort
{
    [Guid("B6CCEC0B-9042-4B88-A306-E29B87B6469C")]
    public class MacMsLoadPort : MacMachineStateBase
    {
        public IMacHalLoadPortUnit HalLoadPortUnit { get { return this.halAssembly as IMacHalLoadPortUnit; } }

        public override void LoadStateMachine()
        {
            #region State

            // Reset
            MacState sWaitReset = NewState(EnumMacMsLoadPortState.WaitReset);// TODO: Event
            MacState sResetStart = NewState(EnumMacMsLoadPortState.ResetStart);// TODO: Event
            MacState sReseting = NewState(EnumMacMsLoadPortState.Reseting);// TODO: Event
            MacState sResetComplete = NewState(EnumMacMsLoadPortState.ResetComplete);// TODO: Event

            // Initial
            MacState sWaitInitial= NewState(EnumMacMsLoadPortState.WaitInitial);// TODO: Event
            MacState sInitialStart = NewState(EnumMacMsLoadPortState.InitialStart);// TODO: Event
            MacState sNormalInitialing = NewState(EnumMacMsLoadPortState.Initialing);// TODO: Event
            MacState sInitialComplete = NewState(EnumMacMsLoadPortState.InitialComplete);// TODO: Event
         
            



            // dock
            MacState sIdleReadyToDock = NewState(EnumMacMsLoadPortState.IdleReadyToDock);
            MacState sDockStart = NewState(EnumMacMsLoadPortState.DockStart);
            MacState sDocking= NewState(EnumMacMsLoadPortState.Docking);
            MacState sDockComplete = NewState(EnumMacMsLoadPortState.DockComplete);

            // undock
            MacState sIdleReadyToUndock = NewState(EnumMacMsLoadPortState.IdleReadyToUndock);
            MacState sUndockStart = NewState(EnumMacMsLoadPortState.UndockStart);
            MacState sUndocking= NewState(EnumMacMsLoadPortState.Undocking);
            MacState sUndockComplete = NewState(EnumMacMsLoadPortState.UndockComplete);
            
            // unload
            MacState sIdleReadyToUnload = NewState(EnumMacMsLoadPortState.IdleReadyToUnload);
            MacState sUnloadExecuted = NewState(EnumMacMsLoadPortState.UnloadExecuted);

            // Exception
            MacState sExpMustReset = NewState(EnumMacMsLoadPortState.ExpMustReset);
            MacState sExpResetTimeout = NewState(EnumMacMsLoadPortState.ExpResetTimeout);
            MacState sExpResetFail = NewState(EnumMacMsLoadPortState.ExpResetFail);
            MacState sExpMustInitial = NewState(EnumMacMsLoadPortState.ExpMustInitial);
            MacState sExpInitialFail = NewState(EnumMacMsLoadPortState.ExpInitialFail);
            MacState sExpInitialTimeout = NewState(EnumMacMsLoadPortState.ExpInitialTimeout);
            MacState sExpDockTimeout = NewState(EnumMacMsLoadPortState.ExpDockTimeout);
            MacState sExpUndockTimeout = NewState(EnumMacMsLoadPortState.ExpUndockTimeOut);
            #endregion State

            #region  Register OnEntry Event Handler

            sWaitReset.OnEntry += sWaitReset_OnEntry;
            sResetStart.OnEntry += sResetStart_OnEntry;
            sReseting.OnEntry += sReseting_OnEntry;
            sResetComplete.OnEntry += sResetComplete_OnEntry;
            sWaitInitial.OnEntry += sWaitInitial_OnEntry;
            sInitialStart.OnEntry += sInitialStart_OnEntry;
            sNormalInitialing.OnEntry += sNormalInitialing_OnEntry;
            sInitialComplete.OnEntry += sInitialComplete_OnEntry;
  
            sIdleReadyToDock.OnEntry += sIdleReadyToDock_OnEntry;
            sDockStart.OnEntry += sDockStart_OnEntry;
            sDocking.OnEntry += sDocking_OnEntry;
            sDockComplete.OnEntry += sDockComplete_OnEntry;
            sIdleReadyToUndock.OnEntry += sIdleReadyToUndock_OnEntry;
            sUndockStart.OnEntry += sUndockStart_OnEntry;
            sUndocking.OnEntry += sUndocking_OnEntry;
            sUndockComplete.OnEntry += sUndockComplete_OnEntry;
            sIdleReadyToUnload.OnEntry += sIdleReadyToUnload_OnEntry;
            sUnloadExecuted.OnEntry += sUnloadExecuted_OnEntry;

            sExpMustReset.OnEntry += sExpMustReset_OnEntry;
            sExpResetTimeout.OnEntry += sExpResetTimeout_OnEntry;
            sExpResetFail.OnEntry += sExpResetFail_OnEntry;
            sExpMustInitial.OnEntry += sExpMustInitial_OnEntry;
            sExpInitialFail.OnEntry += sExpInitialFail_OnEntry;
            sExpInitialTimeout.OnEntry += sExpInitialTimeout_OnEntry;
            sExpDockTimeout.OnEntry += sExpDockTimeout_OnEntry;
            sExpUndockTimeout.OnEntry += sExpUndockTimeout_OnEntry;

            #endregion

            #region Register OnExit Event Handler

            sWaitReset.OnExit += sWaitReset_OnExit;
            sResetStart.OnExit += sResetStart_OnExit;
            sReseting.OnExit += sReseting_OnExit;
            sResetComplete.OnExit += sResetComplete_OnExit;
            sWaitInitial.OnExit += sWaitInitial_OnExit;
            sInitialStart.OnExit += sInitialStart_OnExit;
            sNormalInitialing.OnExit += sNormalInitialing_OnExit;
            sInitialComplete.OnExit += sInitialComplete_OnExit;

            sIdleReadyToDock.OnExit += sIdleReadyToDock_OnExit;
            sDockStart.OnExit += sDockStart_OnExit;
            sDocking.OnExit += sDocking_OnExit;
            sDockComplete.OnExit += sDockComplete_OnExit;
            sIdleReadyToUndock.OnExit += sIdleReadyToUndock_OnExit;
            sUndockStart.OnExit += sUndockStart_OnExit;
            sUndocking.OnExit += sUndocking_OnExit;
            sUndockComplete.OnExit += sUndockComplete_OnExit;
            sIdleReadyToUnload.OnExit += sIdleReadyToUnload_OnExit;
            sUnloadExecuted.OnExit += sUnloadExecuted_OnExit;

            sExpMustReset.OnExit += sExpMustReset_OnExit;
            sExpResetTimeout.OnExit += sExpResetTimeout_OnExit;
            sExpResetFail.OnExit += sExpResetFail_OnExit;
            sExpMustInitial.OnExit += sExpMustInitial_OnExit;
            sExpInitialFail.OnExit += sExpInitialFail_OnExit;
            sExpInitialTimeout.OnExit += sExpInitialTimeout_OnExit;
            sExpDockTimeout.OnExit += sExpDockTimeout_OnExit;
            sExpUndockTimeout.OnExit += sExpUndockTimeout_OnExit;
            #endregion



            #region OnEntry  Method
            void sWaitReset_OnEntry(object sender, MacStateEntryEventArgs e)
            {

            }
            void sResetStart_OnEntry(object sender, MacStateEntryEventArgs e)
            {

            }
            void sReseting_OnEntry(object sender, MacStateEntryEventArgs e)
            {

            }
            void sResetComplete_OnEntry(object sender, MacStateEntryEventArgs e)
            {

            }

           void  sWaitInitial_OnEntry(object sender, MacStateEntryEventArgs e)
            {

            }
            void sInitialStart_OnEntry(object sender, MacStateEntryEventArgs e)
            {

            }
            void sNormalInitialing_OnEntry(object sender, MacStateEntryEventArgs e)
            {

            }
            void sInitialComplete_OnEntry(object sender, MacStateEntryEventArgs e)
            {

            }





            void sIdleReadyToDock_OnEntry(object sender, MacStateEntryEventArgs e)
            {
               // sIdleReadyToDock.DoExit(null);
            }
            void sDockStart_OnEntry(object sender, MacStateEntryEventArgs e)
            {
                // 檢查 Flag, 如果 Flag 為 DockComplete , 跳到下一個 State 
            }
            void sDocking_OnEntry(object sender, MacStateEntryEventArgs e)
            {
                // 檢查 Flag, 如果 Flag 為 DockComplete , 跳到下一個 State 
            }
            void sDockComplete_OnEntry(object sender, MacStateEntryEventArgs e)
            {

            }
            void sIdleReadyToUndock_OnEntry(object sender, MacStateEntryEventArgs e)
            {

            }
            void sUndockStart_OnEntry(object sender, MacStateEntryEventArgs e)
            {

            }
            void sUndocking_OnEntry(object sender, MacStateEntryEventArgs e)
            {

            }
            void sUndockComplete_OnEntry(object sender, MacStateEntryEventArgs e)
            {

            }
            void sIdleReadyToUnload_OnEntry(object sender, MacStateEntryEventArgs e)
            {

            }
            void sUnloadExecuted_OnEntry(object sender, MacStateEntryEventArgs e)
            {

            }


            void sExpMustReset_OnEntry(object sender, MacStateEntryEventArgs e){ }
            void sExpResetTimeout_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sExpResetFail_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sExpMustInitial_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sExpInitialFail_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sExpInitialTimeout_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sExpDockTimeout_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sExpUndockTimeout_OnEntry(object sender, MacStateEntryEventArgs e) { }
            #endregion

            #region OnExit Method
            void sWaitReset_OnExit(object sender, MacStateExitEventArgs e)
            {

            }
            void sResetStart_OnExit(object sender, MacStateExitEventArgs e)
            {

            }
            void sReseting_OnExit(object sender, MacStateExitEventArgs e)
            {

            }
            void sResetComplete_OnExit(object sender, MacStateExitEventArgs e)
            {

            }

            void sWaitInitial_OnExit(object sender, MacStateExitEventArgs e)
            {

            }
            void sInitialStart_OnExit(object sender, MacStateExitEventArgs e)
            {

            }
            void sNormalInitialing_OnExit(object sender, MacStateExitEventArgs e)
            {

            }
            void sInitialComplete_OnExit(object sender, MacStateExitEventArgs e)
            {

            }




            void sIdleReadyToDock_OnExit(object sender, MacStateExitEventArgs e)
            {

            }
            void sDockStart_OnExit(object sender, MacStateExitEventArgs e)
            {

            }
            void sDocking_OnExit(object sender, MacStateExitEventArgs e)
            {
                // 檢查 Flag, 如果 Flag 為 DockComplete , 跳到下一個 State 
            }
            void sDockComplete_OnExit(object sender, MacStateExitEventArgs e)
            {

            }
            void sIdleReadyToUndock_OnExit(object sender, MacStateExitEventArgs e)
            {

            }
            void sUndockStart_OnExit(object sender, MacStateExitEventArgs e)
            {

            }
            void sUndocking_OnExit(object sender, MacStateExitEventArgs e)
            {
                // 檢查 Flag, 如果 Flag 為 DockComplete , 跳到下一個 State 
            }
            void sUndockComplete_OnExit(object sender, MacStateExitEventArgs e)
            {

            }
            void sIdleReadyToUnload_OnExit(object sender, MacStateExitEventArgs e)
            {

            }
            void sUnloadExecuted_OnExit(object sender, MacStateExitEventArgs e)
            {

            }


            void sExpMustReset_OnExit(object sender, MacStateExitEventArgs e) { }
            void sExpResetTimeout_OnExit(object sender, MacStateExitEventArgs e) { }
            void sExpResetFail_OnExit(object sender, MacStateExitEventArgs e) { }
            void sExpMustInitial_OnExit(object sender, MacStateExitEventArgs e) { }
            void sExpInitialFail_OnExit(object sender, MacStateExitEventArgs e) { }
            void sExpInitialTimeout_OnExit(object sender, MacStateExitEventArgs e) { }
            void sExpDockTimeout_OnExit(object sender, MacStateExitEventArgs e) { }
            void sExpUndockTimeout_OnExit(object sender, MacStateExitEventArgs e) { }
            #endregion



            #region Transition 
            // Normal Reset
            MacTransition tWaitNormalReset_NomalResetStart = NewTransition(sWaitReset, sResetStart, EnumMacMsLoadPortTransition.NormalReset);
            MacTransition tNomalResetStart_NomalReseting = NewTransition(sResetStart, sReseting, EnumMacMsLoadPortTransition.NormalReset);
            MacTransition tNomalReseting_NomalResetComplete = NewTransition(sReseting, sResetComplete, EnumMacMsLoadPortTransition.NormalReset);
          


            // Normal Initial
            MacTransition tWaitNormalInitial_NomalInitialStart = NewTransition(sWaitInitial, sInitialStart, EnumMacMsLoadPortTransition.NormalInitial);
            MacTransition tNomalInitialStart_NomalInitialing = NewTransition(sInitialStart, sNormalInitialing, EnumMacMsLoadPortTransition.NormalInitial);
            /**
              MacTransition tNomalInitialing_MustResetDuringInitialing = NewTransition(sNormalInitialing, sMustResetDuringNormalInitialing, EnumMacMsLoadPortTransition.NormalInitial);
            */
            MacTransition tNomalInitialing_NomalInitialComplete = NewTransition(sNormalInitialing, sInitialComplete, EnumMacMsLoadPortTransition.NormalInitial);
           



            // Dock;
            MacTransition tIdleReadyToDock_DockStart = NewTransition(sIdleReadyToDock, sDockStart, EnumMacMsLoadPortTransition.Dock);
            MacTransition tDocStart_Docking = NewTransition(sDockStart, sDocking, EnumMacMsLoadPortTransition.Dock);
            MacTransition tDocking_DockComplete = NewTransition(sDocking, sDockComplete, EnumMacMsLoadPortTransition.Dock);

            MacTransition tDockComplete_IdleReadyToUndock = NewTransition(sDockComplete, sIdleReadyToUndock, EnumMacMsLoadPortTransition.ReadyToUndock);
            
            // Undock
            MacTransition tIdleReadyToUndock_UndockStart= NewTransition(sIdleReadyToUndock, sUndockStart, EnumMacMsLoadPortTransition.Undock);
            MacTransition tUndockStart_Undocking = NewTransition(sUndockStart, sUndocking, EnumMacMsLoadPortTransition.Undock);
            MacTransition tUndocking_UndockComplete = NewTransition(sUndocking, sUndockComplete,EnumMacMsLoadPortTransition.Undock);

            MacTransition tUndockComplete_IdelReadyToUnload = NewTransition(sUndockComplete, sIdleReadyToUnload, EnumMacMsLoadPortTransition.ReadyToUnload);


            MacTransition tIdelReadyToUnload_Unload = NewTransition(sIdleReadyToUnload, sUnloadExecuted, EnumMacMsLoadPortTransition.Unload);
            #endregion

        }
    }
}
