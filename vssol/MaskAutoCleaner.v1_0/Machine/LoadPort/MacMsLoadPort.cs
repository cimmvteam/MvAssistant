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

            // Normal Reset
            MacState sWaitNormalReset = NewState(EnumMacMsLoadPortState.WaitNormalReset);// TODO: Event
            MacState sNormalResetStart = NewState(EnumMacMsLoadPortState.NormalResetStart);// TODO: Event
            MacState sNormalReseting = NewState(EnumMacMsLoadPortState.NormalReseting);// TODO: Event
            MacState sNormalResetComplete = NewState(EnumMacMsLoadPortState.NormalResetComplete);// TODO: Event

            // Normal Initial
            MacState sWaitNormalInitial= NewState(EnumMacMsLoadPortState.WaitNormalInitial);// TODO: Event
            MacState sNormalInitialStart = NewState(EnumMacMsLoadPortState.NormalInitialStart);// TODO: Event
            MacState sNormalInitialing = NewState(EnumMacMsLoadPortState.NormalInitialing);// TODO: Event
            MacState sMustResetDuringNormalInitialing = NewState(EnumMacMsLoadPortState.MustResetDuringNormalInitialing);// TODO: Event
            MacState sNormalInitialComplete = NewState(EnumMacMsLoadPortState.NormalInitialComplete);// TODO: Event
         
            



            // IdleReadyToDock
            MacState sIdleReadyToDock = NewState(EnumMacMsLoadPortState.IdleReadyToDock);
            // DockStart 
            MacState sDockStart = NewState(EnumMacMsLoadPortState.DockStart);
          
            // Docking
            MacState sDocking= NewState(EnumMacMsLoadPortState.Docking);
            MacState sMustResetDuringDucking = NewState(EnumMacMsLoadPortState.MustResetDuringDocking);// TODO: Event
            MacState sMustInitialDuringDucking = NewState(EnumMacMsLoadPortState.MustInitialDuringDocking);// TODO: Event
            // DockComplete
            MacState sDockComplete = NewState(EnumMacMsLoadPortState.DockComplete);
            // IdleReadyToUndock 
            MacState sIdleReadyToUndock = NewState(EnumMacMsLoadPortState.IdleReadyToUndock);
            // UndockStart
            MacState sUndockStart = NewState(EnumMacMsLoadPortState.UndockStart);
          
            // Undocking
            MacState sUndocking= NewState(EnumMacMsLoadPortState.Undocking);
            MacState sMustResetDuringUndocking = NewState(EnumMacMsLoadPortState.MustResetDuringUndocking); // TODO: Event
            MacState sMustInitialDuringUndocking = NewState(EnumMacMsLoadPortState.MustInitialDuringUndocking); // TODO: Event


            // UndockComplete
            MacState sUndockComplete = NewState(EnumMacMsLoadPortState.UndockComplete);
            // IdleReadyToUnload
            MacState sIdleReadyToUnload = NewState(EnumMacMsLoadPortState.IdleReadyToUnload);
            MacState sUnloadExecuted = NewState(EnumMacMsLoadPortState.UnloadExecuted);

            #endregion State

            #region  Register OnEntry Event Handler
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
            #endregion

            #region Register OnExit Event Handler
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
            #endregion



            #region OnEntry  Method
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
            #endregion

            #region OnExit Method
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
            #endregion



            #region Transition 
            // Normal Reset
            MacTransition tWaitNormalReset_NomalResetStart = NewTransition(sWaitNormalReset, sNormalResetStart, EnumMacMsLoadPortTransition.NormalReset);
            MacTransition tNomalResetStart_NomalReseting = NewTransition(sNormalResetStart, sNormalReseting, EnumMacMsLoadPortTransition.NormalReset);
            MacTransition tNomalReseting_NomalResetComplete = NewTransition(sNormalReseting, sNormalResetComplete, EnumMacMsLoadPortTransition.NormalReset);
          


            // Normal Initial
            MacTransition tWaitNormalInitial_NomalInitialStart = NewTransition(sWaitNormalInitial, sNormalInitialStart, EnumMacMsLoadPortTransition.NormalInitial);
            MacTransition tNomalInitialStart_NomalInitialing = NewTransition(sNormalInitialStart, sNormalInitialing, EnumMacMsLoadPortTransition.NormalInitial);
            MacTransition tNomalInitialing_MustResetDuringInitialing = NewTransition(sNormalInitialing, sMustResetDuringNormalInitialing, EnumMacMsLoadPortTransition.NormalInitial);
            MacTransition tNomalInitialing_NomalInitialComplete = NewTransition(sNormalInitialing, sNormalInitialComplete, EnumMacMsLoadPortTransition.NormalInitial);
           



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
