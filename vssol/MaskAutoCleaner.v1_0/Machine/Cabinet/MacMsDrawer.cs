using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.Mac.v1_0.Hal.CompDrawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    [Guid("00000000-0000-0000-0000-000000000000")]
    public class MacMsDrawer : MacMachineStateBase
    {
        public IMacHalDrawer HalDrawer { get { return this.halAssembly as IMacHalDrawer; } }
        public override void LoadStateMachine()
        {
            #region State;
            MacState sAnyState = NewState(EnumMacDrawerState.AnyState);
            // Normal Initial
            MacState sWaitInitial = NewState(EnumMacDrawerState.WaitInitial);
            MacState sInitialStart = NewState(EnumMacDrawerState.InitialStart);
            MacState sInitialIng = NewState(EnumMacDrawerState.InitialIng);
            MacState sInitialComplete = NewState(EnumMacDrawerState.InitialComplete);

            // Load(Pre)
            MacState sLoadAnyState = NewState(EnumMacDrawerState.LoadAnyState);
            MacState sLoadGotoInStart = NewState(EnumMacDrawerState.LoadGotoInStart);
            MacState sLoadGotoInIng = NewState(EnumMacDrawerState.LoadGotoInIng);
            MacState sLoadGotoInComplete = NewState(EnumMacDrawerState.LoadGotoInComplete);
            MacState sIdleReadyForLoadBoxAtIn = NewState(EnumMacDrawerState.IdleReadyForLoadBoxAtIn);
            MacState sLoadBoxAtInComplete = NewState(EnumMacDrawerState.LoadBoxAtInComplete);

            // Load
            MacState sLoadGotoHomeStart = NewState(EnumMacDrawerState.LoadGotoHomeStart);
            MacState sLoadGotoHomeIng = NewState(EnumMacDrawerState.LoadGotoHomeIng);
            MacState sLoadGotoHomeComplete = NewState(EnumMacDrawerState.LoadGotoHomeComplete);
            MacState sLoadGotoOutStart = NewState(EnumMacDrawerState.LoadGotoOutStart);
            MacState sLoadGotoOutIng = NewState(EnumMacDrawerState.LoadGotoOutIng);
            MacState sLoadGotoOutComplete = NewState(EnumMacDrawerState.LoadGotoOutComplete);
            MacState sLoadComplete = NewState(EnumMacDrawerState.LoadComplete);

            // Load(Post)
            MacState sIdleReadyForGetBox = NewState(EnumMacDrawerState.IdleReadyForGet);
            MacState sLoadBoxGetAtOut = NewState(EnumMacDrawerState.LoadBoxGetAtOut);

            // Unload(Pre)
            MacState sUnloadAnyState = NewState(EnumMacDrawerState.UnloadAnyState);
            MacState sUnloadGotoOutStart = NewState(EnumMacDrawerState.UnloadGotoOutStart);
            MacState sUnloadGotoOutIng = NewState(EnumMacDrawerState.UnloadGotoOutIng);
            MacState sUnloadGotoOutComplete = NewState(EnumMacDrawerState.UnloadGotoOutComplete);
            MacState sIdleReadyForUnloadBoxAtOut = NewState(EnumMacDrawerState.IdleReadyForUnloadBoxAtOut);
            MacState sUnloadBoxAtOutComplete = NewState(EnumMacDrawerState.UnloadBoxPutAtOut);

            // Unload
            MacState sUnloadGotoHomeStart = NewState(EnumMacDrawerState.UnloadGotoHomeStart);
            MacState sUnloadGotoHomeIng = NewState(EnumMacDrawerState.UnloadGotoHomeIng);
            MacState sUnloadGotoHomeComplete= NewState(EnumMacDrawerState.UnloadGotoHomeComplete);
            MacState sUnloadGotoInStart = NewState(EnumMacDrawerState.UnloadGotoInStart);
            MacState sUnloadGotoInIng = NewState(EnumMacDrawerState.UnloadGotoInIng);
            MacState sUnloadGotoInComplete = NewState(EnumMacDrawerState.UnloadGotoInComplete);
            MacState sUnloadComplete = NewState(EnumMacDrawerState.UnloadComplete);

            // Load(Post)
            MacState sIdleReadyForUnloadBoxAtIn = NewState(EnumMacDrawerState.IdleReadyForUnloadBoxAtIn);
            MacState sUnloadBoxAtInComplete = NewState(EnumMacDrawerState.UnloadBoxAtInComplete);

            #endregion State

            #region delegates of Event OnEntry
            void sAnyState_OnEntry (object sender, MacStateEntryEventArgs e) { }
            void sWaitInitial_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sInitialStart_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sInitialIng_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sInitialComplete_OnEntry(object sender, MacStateEntryEventArgs e) { }

            void sLoadGotoInStart_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sLoadGotoInIng_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sLoadGotoInComplete_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sIdleReadyForLoadBoxAtIn_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sLoadBoxAtInComplete_OnEntry(object sender, MacStateEntryEventArgs e) { }


            void sLoadGotoHomeStart_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sLoadGotoHomeIng_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sLoadGotoHomeComplete_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sLoadGotoOutStart_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sLoadGotoOutIng_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sLoadGotoOutComplete_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sLoadComplete_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sIdleReadyForGetBox_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sLoadBoxGetAtOut_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sUnloadAnyState_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sUnloadGotoOutStart_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sUnloadGotoOutIng_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sUnloadGotoOutComplete_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sIdleReadyForUnloadBoxAtOut_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sUnloadBoxAtOutComplete_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sUnloadGotoHomeStart_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sUnloadGotoHomeIng_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sUnloadGotoHomeComplete_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sUnloadGotoInStart_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sUnloadGotoInIng_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sUnloadGotoInComplete_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sUnloadComplete_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sIdleReadyForUnloadBoxAtIn_OnEntry(object sender, MacStateEntryEventArgs e) { }
            void sUnloadBoxAtInComplete_OnEntry(object sender, MacStateEntryEventArgs e) { }

            #endregion delegates of Event OnEntry
            #region  Event OnEntry
            sAnyState.OnEntry += sAnyState_OnEntry;
            sWaitInitial.OnEntry += sWaitInitial_OnEntry;
            sInitialStart.OnEntry += sInitialStart_OnEntry;
            sInitialIng.OnEntry += sInitialIng_OnEntry;
            sInitialComplete.OnEntry += sInitialComplete_OnEntry;

            sLoadGotoInStart.OnEntry += sLoadGotoInStart_OnEntry;
            sLoadGotoInIng.OnEntry += sLoadGotoInIng_OnEntry;
            sLoadGotoInComplete.OnEntry += sLoadGotoInComplete_OnEntry;
            sIdleReadyForLoadBoxAtIn.OnEntry += sIdleReadyForLoadBoxAtIn_OnEntry;
            sLoadBoxAtInComplete.OnEntry += sLoadBoxAtInComplete_OnEntry;


            sLoadGotoHomeStart.OnEntry += sLoadGotoHomeStart_OnEntry;
            sLoadGotoHomeIng.OnEntry += sLoadGotoHomeIng_OnEntry;
            sLoadGotoHomeComplete.OnEntry += sLoadGotoHomeComplete_OnEntry;
            sLoadGotoOutStart.OnEntry += sLoadGotoOutStart_OnEntry;
            sLoadGotoOutIng.OnEntry += sLoadGotoOutIng_OnEntry;
            sLoadGotoOutComplete.OnEntry += sLoadGotoOutComplete_OnEntry;
            sLoadComplete.OnEntry += sLoadComplete_OnEntry;
            sIdleReadyForGetBox.OnEntry += sIdleReadyForGetBox_OnEntry;
            sLoadBoxGetAtOut.OnEntry += sLoadBoxGetAtOut_OnEntry;
            sUnloadAnyState.OnEntry += sUnloadAnyState_OnEntry;
            sUnloadGotoOutStart.OnEntry += sUnloadGotoOutStart_OnEntry;
            sUnloadGotoOutIng.OnEntry += sUnloadGotoOutIng_OnEntry;
            sUnloadGotoOutComplete.OnEntry += sUnloadGotoOutComplete_OnEntry;
            sIdleReadyForUnloadBoxAtOut.OnEntry += sIdleReadyForUnloadBoxAtOut_OnEntry;
            sUnloadBoxAtOutComplete.OnEntry += sUnloadBoxAtOutComplete_OnEntry;
            sUnloadGotoHomeStart.OnEntry += sUnloadGotoHomeStart_OnEntry;
            sUnloadGotoHomeIng.OnEntry += sUnloadGotoHomeIng_OnEntry;
            sUnloadGotoHomeComplete.OnEntry += sUnloadGotoHomeComplete_OnEntry;
            sUnloadGotoInStart.OnEntry += sUnloadGotoInStart_OnEntry;
            sUnloadGotoInIng.OnEntry += sUnloadGotoInIng_OnEntry;
            sUnloadGotoInComplete.OnEntry += sUnloadGotoInComplete_OnEntry;
            sUnloadComplete.OnEntry += sUnloadComplete_OnEntry;
            sIdleReadyForUnloadBoxAtIn.OnEntry += sIdleReadyForUnloadBoxAtIn_OnEntry;
            sUnloadBoxAtInComplete.OnEntry += sUnloadBoxAtInComplete_OnEntry;
            #endregion   Event OnEntry



            #region delegates of Event OnExit
            void sAnyState_OnExit(object sender, MacStateExitEventArgs e) { }
            void sWaitInitial_OnExit(object sender, MacStateExitEventArgs e) { }
            void sInitialStart_OnExit(object sender, MacStateExitEventArgs e) { }
            void sInitialIng_OnExit(object sender, MacStateExitEventArgs e) { }
            void sInitialComplete_OnExit(object sender, MacStateExitEventArgs e) { }

            void sLoadGotoInStart_OnExit(object sender, MacStateExitEventArgs e) { }
            void sLoadGotoInIng_OnExit(object sender, MacStateExitEventArgs e) { }
            void sLoadGotoInComplete_OnExit(object sender, MacStateExitEventArgs e) { }
            void sIdleReadyForLoadBoxAtIn_OnExit(object sender, MacStateExitEventArgs e) { }
            void sLoadBoxAtInComplete_OnExit(object sender, MacStateExitEventArgs e) { }


            void sLoadGotoHomeStart_OnExit(object sender, MacStateExitEventArgs e) { }
            void sLoadGotoHomeIng_OnExit(object sender, MacStateExitEventArgs e) { }
            void sLoadGotoHomeComplete_OnExit(object sender, MacStateExitEventArgs e) { }
            void sLoadGotoOutStart_OnExit(object sender, MacStateExitEventArgs e) { }
            void sLoadGotoOutIng_OnExit(object sender, MacStateExitEventArgs e) { }
            void sLoadGotoOutComplete_OnExit(object sender, MacStateExitEventArgs e) { }
            void sLoadComplete_OnExit(object sender, MacStateExitEventArgs e) { }
            void sIdleReadyForGetBox_OnExit(object sender, MacStateExitEventArgs e) { }
            void sLoadBoxGetAtOut_OnExit(object sender, MacStateExitEventArgs e) { }
            void sUnloadAnyState_OnExit(object sender, MacStateExitEventArgs e) { }
            void sUnloadGotoOutStart_OnExit(object sender, MacStateExitEventArgs e) { }
            void sUnloadGotoOutIng_OnExit(object sender, MacStateExitEventArgs e) { }
            void sUnloadGotoOutComplete_OnExit(object sender, MacStateExitEventArgs e) { }
            void sIdleReadyForUnloadBoxAtOut_OnExit(object sender, MacStateExitEventArgs e) { }
            void sUnloadBoxAtOutComplete_OnExit(object sender, MacStateExitEventArgs e) { }
            void sUnloadGotoHomeStart_OnExit(object sender, MacStateExitEventArgs e) { }
            void sUnloadGotoHomeIng_OnExit(object sender, MacStateExitEventArgs e) { }
            void sUnloadGotoHomeComplete_OnExit(object sender, MacStateExitEventArgs e) { }
            void sUnloadGotoInStart_OnExit(object sender, MacStateExitEventArgs e) { }
            void sUnloadGotoInIng_OnExit(object sender, MacStateExitEventArgs e) { }
            void sUnloadGotoInComplete_OnExit(object sender, MacStateExitEventArgs e) { }
            void sUnloadComplete_OnExit(object sender, MacStateExitEventArgs e) { }
            void sIdleReadyForUnloadBoxAtIn_OnExit(object sender, MacStateExitEventArgs e) { }
            void sUnloadBoxAtInComplete_OnExit(object sender, MacStateExitEventArgs e) { }

            #endregion delegates of Event OnEntry

            #region Event OnExit
            sAnyState.OnExit += sAnyState_OnExit;
            sWaitInitial.OnExit += sWaitInitial_OnExit;
            sInitialStart.OnExit += sInitialStart_OnExit;
            sInitialIng.OnExit += sInitialIng_OnExit;
            sInitialComplete.OnExit += sInitialComplete_OnExit;

            sLoadGotoInStart.OnExit += sLoadGotoInStart_OnExit;
            sLoadGotoInIng.OnExit += sLoadGotoInIng_OnExit;
            sLoadGotoInComplete.OnExit += sLoadGotoInComplete_OnExit;
            sIdleReadyForLoadBoxAtIn.OnExit += sIdleReadyForLoadBoxAtIn_OnExit;
            sLoadBoxAtInComplete.OnExit += sLoadBoxAtInComplete_OnExit;


            sLoadGotoHomeStart.OnExit += sLoadGotoHomeStart_OnExit;
            sLoadGotoHomeIng.OnExit += sLoadGotoHomeIng_OnExit;
            sLoadGotoHomeComplete.OnExit += sLoadGotoHomeComplete_OnExit;
            sLoadGotoOutStart.OnExit += sLoadGotoOutStart_OnExit;
            sLoadGotoOutIng.OnExit += sLoadGotoOutIng_OnExit;
            sLoadGotoOutComplete.OnExit += sLoadGotoOutComplete_OnExit;
            sLoadComplete.OnExit += sLoadComplete_OnExit;

            sIdleReadyForGetBox.OnExit += sIdleReadyForGetBox_OnExit;
            sLoadBoxGetAtOut.OnExit += sLoadBoxGetAtOut_OnExit;

            sUnloadAnyState.OnExit += sUnloadAnyState_OnExit;
            sUnloadGotoOutStart.OnExit += sUnloadGotoOutStart_OnExit;
            sUnloadGotoOutIng.OnExit += sUnloadGotoOutIng_OnExit;
            sUnloadGotoOutComplete.OnExit += sUnloadGotoOutComplete_OnExit;
            sIdleReadyForUnloadBoxAtOut.OnExit += sIdleReadyForUnloadBoxAtOut_OnExit;
            sUnloadBoxAtOutComplete.OnExit += sUnloadBoxAtOutComplete_OnExit;

            sUnloadGotoHomeStart.OnExit += sUnloadGotoHomeStart_OnExit;
            sUnloadGotoHomeIng.OnExit += sUnloadGotoHomeIng_OnExit;
            sUnloadGotoHomeComplete.OnExit += sUnloadGotoHomeComplete_OnExit;
            sUnloadGotoInStart.OnExit += sUnloadGotoInStart_OnExit;
            sUnloadGotoInIng.OnExit += sUnloadGotoInIng_OnExit;
            sUnloadGotoInComplete.OnExit += sUnloadGotoInComplete_OnExit;
            sUnloadComplete.OnExit += sUnloadComplete_OnExit;

            sIdleReadyForUnloadBoxAtIn.OnExit += sIdleReadyForUnloadBoxAtIn_OnExit;
            sUnloadBoxAtInComplete.OnExit += sUnloadBoxAtInComplete_OnExit;

            #endregion  Event OnExit

            #region Transition

            // Initial,
            MacTransition tAnyState_InitialStart = NewTransition(sAnyState,sWaitInitial,EnumMacDrawerTransition.Initial);
            MacTransition tInitialStart_InitialIng = NewTransition(sWaitInitial,sInitialIng, EnumMacDrawerTransition.Initial);
            MacTransition tInitialStart_InitialComplete = NewTransition(sInitialIng, sInitialComplete, EnumMacDrawerTransition.Initial);

            // Load
            MacTransition tLoadAnyState_LoadGotoInStart = NewTransition(sLoadAnyState, sLoadGotoInStart, EnumMacDrawerTransition.Load);
            MacTransition tLoadGotoInStart_LoadGotoInIng = NewTransition(sLoadGotoInStart, sLoadGotoInIng, EnumMacDrawerTransition.Load);
            MacTransition tLoadGotoInIng_LoadGotoInComplete = NewTransition(sLoadGotoInIng, sLoadGotoInComplete, EnumMacDrawerTransition.Load);
            MacTransition tLoadGotoInComplete_IdleReadyForLoadBoxAtIn = NewTransition(sLoadGotoInComplete,sIdleReadyForLoadBoxAtIn, EnumMacDrawerTransition.Load);
            MacTransition tIdleReadyForLoadBoxAtIn_LoadBoxAtInComplete = NewTransition(sIdleReadyForLoadBoxAtIn, sLoadBoxAtInComplete, EnumMacDrawerTransition.Load);
            MacTransition tLoadBoxAtInComplete_LoadGotoHomeStart = NewTransition(sLoadBoxAtInComplete, sLoadGotoHomeStart, EnumMacDrawerTransition.Load);
            MacTransition tLoaGotoHomeStart_LoadGotoHomeIng= NewTransition(sLoadGotoHomeStart, sLoadGotoHomeIng, EnumMacDrawerTransition.Load);
            MacTransition tLoadGotoHomeIng_LoadGotoHomeComplete = NewTransition(sLoadGotoHomeIng, sLoadGotoHomeComplete, EnumMacDrawerTransition.Load);
            MacTransition tLoadGotoHomeComplete_IdleReadyForGetBox = NewTransition(sLoadGotoHomeComplete,sIdleReadyForGetBox, EnumMacDrawerTransition.Load);
            MacTransition tIdleReadyForGetBox_LoadBoxGetAtOut = NewTransition(sIdleReadyForGetBox, sLoadBoxGetAtOut, EnumMacDrawerTransition.Load);

            // Unload
            MacTransition tUnloadAnyState_UnloadGotoOutStart = NewTransition(sUnloadAnyState, sUnloadGotoOutStart, EnumMacDrawerTransition.Unload);
            MacTransition tUnloadGotoOutStart_UnloadGotoOutIng = NewTransition(sUnloadGotoOutStart, sUnloadGotoOutIng, EnumMacDrawerTransition.Unload);
            MacTransition tUnloadGotoOutIng_UnloadGotoOutComplete = NewTransition(sUnloadGotoOutIng, sUnloadGotoOutComplete, EnumMacDrawerTransition.Unload);
            MacTransition tUnloadGotoOutComplete_IdleReadyForUnloadBoxAtOut = NewTransition(sUnloadGotoOutComplete, sIdleReadyForUnloadBoxAtOut, EnumMacDrawerTransition.Unload);
            MacTransition tIdleReadyForUnloadBoxAtOut_UnloadBoxAtOutComplete = NewTransition(sIdleReadyForUnloadBoxAtOut, sUnloadBoxAtOutComplete, EnumMacDrawerTransition.Unload);
            MacTransition tUnloadBoxAtOutComplete_UnloadGotoHomeStart= NewTransition(sUnloadBoxAtOutComplete, sUnloadGotoHomeStart, EnumMacDrawerTransition.Unload);
            MacTransition tUnloadGotoHomeStart_UnloadGotoHomeIng = NewTransition(sUnloadGotoHomeStart, sUnloadGotoHomeIng, EnumMacDrawerTransition.Unload);
            MacTransition tUnloadGotoHomeIng_UnloadGotoHomeComplete = NewTransition(sUnloadGotoHomeIng, sUnloadGotoHomeComplete, EnumMacDrawerTransition.Unload);

            MacTransition tUnloadGotoHomeComplete_IdleReadyForUnloadBoxAtIn = NewTransition(sUnloadGotoHomeComplete, sIdleReadyForUnloadBoxAtIn, EnumMacDrawerTransition.Unload);
            MacTransition tIdleReadyForUnloadBoxAtIn_UnloadComplete = NewTransition(sIdleReadyForUnloadBoxAtIn, sUnloadComplete, EnumMacDrawerTransition.Unload);
            #endregion




        }
    }
}
