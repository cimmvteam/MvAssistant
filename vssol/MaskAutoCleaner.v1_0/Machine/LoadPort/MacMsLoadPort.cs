using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.Mac.v1_0.Hal.CompLoadPort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.LoadPort
{
    [Guid("B6CCEC0B-9042-4B88-A306-E29B87B6469C")]
    public class MacMsLoadPort : MacMachineStateBase
    {
        public IMacHalLoadPortUnit HalMaskTransfer { get { return this.halAssembly as IMacHalLoadPortUnit; } }

        public override void LoadStateMachine()
        {
            #region State
            

            // IdleReadyToDock
            MacState sIdleReadyToDock = NewState(EnumMacMsLoadPortState.IdleReadyToDock);
            // DockStart 
            MacState sDockStart = NewState(EnumMacMsLoadPortState.DockStart);
            // DockComplete
            MacState sDockComplete = NewState(EnumMacMsLoadPortState.DockComplete);
            // IdleReadyToUndock 
            MacState sIdleReadyToUndock = NewState(EnumMacMsLoadPortState.IdleReadyToUndock);
            // UndockStart
            MacState sUndockStart = NewState(EnumMacMsLoadPortState.UndockStart);
            // UndockComplete
            MacState sUndockComplete = NewState(EnumMacMsLoadPortState.UndockComplete);
            // IdleReadyToUnload
            MacState sIdleReadyToUnload = NewState(EnumMacMsLoadPortState.IdleReadyToUnload);
            MacState sUnloadExecuted = NewState(EnumMacMsLoadPortState.UnloadExecuted);

            #endregion State

            #region  Register OnEntry Event Handler
            sIdleReadyToDock.OnEntry += sIdleReadyToDock_OnEntry;
            sDockStart.OnEntry += sDockStart_OnEntry;
            sDockComplete.OnEntry += sDockComplete_OnEntry;
            sIdleReadyToUndock.OnEntry += sIdleReadyToUndock_OnEntry;
            sUndockStart.OnEntry += sUndockStart_OnEntry;
            sUndockComplete.OnEntry += sUndockComplete_OnEntry;
            sIdleReadyToUnload.OnEntry += sIdleReadyToUnload_OnEntry;
            sUnloadExecuted.OnEntry += sUnloadExecuted_OnEntry;
            #endregion

            #region Register OnExit Event Handler
            sIdleReadyToDock.OnExit += sIdleReadyToDock_OnExit;
            sDockStart.OnExit += sDockStart_OnExit;
            sDockComplete.OnExit += sDockComplete_OnExit;
            sIdleReadyToUndock.OnExit += sIdleReadyToUndock_OnExit;
            sUndockStart.OnExit += sUndockStart_OnExit;
            sUndockComplete.OnExit += sUndockComplete_OnExit;
            sIdleReadyToUnload.OnExit += sIdleReadyToUnload_OnExit;
            sUnloadExecuted.OnExit += sUnloadExecuted_OnExit;
            #endregion



            #region OnEntry  Method
            void sIdleReadyToDock_OnEntry(object sender, MacStateEntryEventArgs e)
            {

            }
            void sDockStart_OnEntry(object sender, MacStateEntryEventArgs e)
            {

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
            void sDockComplete_OnExit(object sender, MacStateExitEventArgs e)
            {

            }
            void sIdleReadyToUndock_OnExit(object sender, MacStateExitEventArgs e)
            {

            }
            void sUndockStart_OnExit(object sender, MacStateExitEventArgs e)
            {

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

            MacTransition tIdleReadyToDock_DockStart = NewTransition(sIdleReadyToDock, sDockStart, EnumMacMsLoadPortTransition.Dock);
            MacTransition tDockStart_DockComplete = NewTransition(sDockStart, sDockComplete, EnumMacMsLoadPortTransition.Dock);

            MacTransition tDockComplete_IdleReadyToUndock = NewTransition(sDockComplete, sIdleReadyToUndock, EnumMacMsLoadPortTransition.ReadyToUndock);
            
            MacTransition tIdleReadyToUndock_UndockStart= NewTransition(sIdleReadyToUndock, sUndockStart, EnumMacMsLoadPortTransition.Undock);
            MacTransition tUndockStart_UndockComplete = NewTransition(sUndockStart,sUndockComplete,EnumMacMsLoadPortTransition.Undock);

            MacTransition tUndockComplete_IdelReadyToUnload = NewTransition(sUndockComplete, sIdleReadyToUnload, EnumMacMsLoadPortTransition.ReadyToUnload);


            MacTransition tIdelReadyToUnload_Unload = NewTransition(sIdleReadyToUnload, sUnloadExecuted, EnumMacMsLoadPortTransition.Unload);
            #endregion

        }
    }
}
