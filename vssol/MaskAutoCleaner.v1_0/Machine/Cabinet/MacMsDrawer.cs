using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.Mac.v1_0.Hal.CompDrawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    [Guid("204025E5-D96E-467B-A60A-C9997F8B1563")]
    public class MacMsDrawer : MacMachineStateBase
    {
        public IMacHalDrawer HalDrawer { get { return this.halAssembly as IMacHalDrawer; } }
        private MacDrawerStateTimeOutController timeoutObj = new MacDrawerStateTimeOutController();

        #region Temp
        public void Initial()
        {
            this.States[EnumMacDrawerState.AnyState.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }

        public void LoadPreWork1()
        {
            this.States[EnumMacDrawerState.LoadAnyState.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }

        public void LoadPreWork2()
        {

        }


        public void LoadMain()
        {
            this.States[EnumMacDrawerState.LoadGotoHomeStart.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }

        public void LoadPostWork()
        {

        }

        public void UnloadPreWork1()
        {
            this.States[EnumMacDrawerState.UnloadAnyState.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }
        public void UnloadPreWork2()
        {

        }

        public void UnLoadMain()
        {
            this.States[EnumMacDrawerState.UnloadGotoHomeStart.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }



        public void UnLoadPostWork()
        {

        }

        #endregion Temp
        public override void LoadStateMachine()
        {
            #region State;
            MacState sAnyState = NewState(EnumMacDrawerState.AnyState);
            // Normal Initial
            MacState sWaitInitial = NewState(EnumMacDrawerState.WaitInitial);
            MacState sInitialStart = NewState(EnumMacDrawerState.InitialStart);
            MacState sInitialIng = NewState(EnumMacDrawerState.InitialIng);
            MacState sInitialComplete = NewState(EnumMacDrawerState.InitialComplete);
            MacState sInitialFail = NewState(EnumMacDrawerState.InitialFail);
            MacState sInitialTimeOut = NewState(EnumMacDrawerState.InitialTimeout);


            // Load Prework1
            MacState sLoadAnyState = NewState(EnumMacDrawerState.LoadAnyState);
            MacState sLoadGotoInStart = NewState(EnumMacDrawerState.LoadGotoInStart);
            MacState sLoadGotoInIng = NewState(EnumMacDrawerState.LoadGotoInIng);
            MacState sLoadGotoInComplete = NewState(EnumMacDrawerState.LoadGotoInComplete);
            // LoadPrework1 Fail, Time Out
            MacState sLoadPrework1TimeOut = NewState(EnumMacDrawerState.LoadPrework1TimeOut);
            MacState sLoadPrework1Fail = NewState(EnumMacDrawerState.LoadPrework1Fail);


            // Load Prework2
            MacState sIdleReadyForLoadBoxAtIn = NewState(EnumMacDrawerState.IdleReadyForLoadBoxAtIn);
            MacState sLoadBoxAtInComplete = NewState(EnumMacDrawerState.LoadBoxAtInComplete);

            // LoadMainWork
            MacState sLoadGotoHomeStart = NewState(EnumMacDrawerState.LoadGotoHomeStart);
            MacState sLoadGotoHomeIng = NewState(EnumMacDrawerState.LoadGotoHomeIng);
            MacState sLoadGotoHomeComplete = NewState(EnumMacDrawerState.LoadGotoHomeComplete);
            MacState sLoadMainworkGotoHomeTimeOut = NewState(EnumMacDrawerState.LoadMainworkGotoHomeTimeOut);
            MacState sLoadMainworkGotoHomeFail = NewState(EnumMacDrawerState.LoadMainworkGotoHomeFail);

            MacState sLoadGotoOutStart = NewState(EnumMacDrawerState.LoadGotoOutStart);
            MacState sLoadGotoOutIng = NewState(EnumMacDrawerState.LoadGotoOutIng);
            MacState sLoadGotoOutComplete = NewState(EnumMacDrawerState.LoadGotoOutComplete);
            MacState sLoadComplete = NewState(EnumMacDrawerState.LoadComplete);
            MacState sLoadMainworkGotoOutTimeOut = NewState(EnumMacDrawerState.LoadMainworkGotoOutTimeOut);
            MacState sLoadMainworkGotoOutFail = NewState(EnumMacDrawerState.LoadMainworkGotoOutFail);

            // Load(Post)
            MacState sIdleReadyForGetBox = NewState(EnumMacDrawerState.IdleReadyForGet);
            MacState sLoadBoxGetAtOut = NewState(EnumMacDrawerState.LoadBoxGetAtOut);

            // Unload(Prework1)
            MacState sUnloadAnyState = NewState(EnumMacDrawerState.UnloadAnyState);
            MacState sUnloadGotoOutStart = NewState(EnumMacDrawerState.UnloadGotoOutStart);
            MacState sUnloadGotoOutIng = NewState(EnumMacDrawerState.UnloadGotoOutIng);
            MacState sUnloadGotoOutComplete = NewState(EnumMacDrawerState.UnloadGotoOutComplete);
            MacState sUnloadPrework1Fail = NewState(EnumMacDrawerState.UnloadPrework1Fail);
            MacState sUnloadPrework1TimeOut = NewState(EnumMacDrawerState.UnloadPrework1TimeOut);

            // Unload(Prework2)
            MacState sIdleReadyForUnloadBoxAtOut = NewState(EnumMacDrawerState.IdleReadyForUnloadBoxAtOut);
            MacState sUnloadBoxAtOutComplete = NewState(EnumMacDrawerState.UnloadBoxPutAtOut);

            // Unload
            MacState sUnloadGotoHomeStart = NewState(EnumMacDrawerState.UnloadGotoHomeStart);
            MacState sUnloadGotoHomeIng = NewState(EnumMacDrawerState.UnloadGotoHomeIng);
            MacState sUnloadGotoHomeComplete = NewState(EnumMacDrawerState.UnloadGotoHomeComplete);
            MacState sUnloadMainworkGotoHomeFail = NewState(EnumMacDrawerState.UnloadMainworkGotoHomeFail);
            MacState sUnloadMainworkGotoHomeTimeOut = NewState(EnumMacDrawerState.UnloadMainworkGotoHomeTimeOut);

            MacState sUnloadGotoInStart = NewState(EnumMacDrawerState.UnloadGotoInStart);
            MacState sUnloadGotoInIng = NewState(EnumMacDrawerState.UnloadGotoInIng);
            MacState sUnloadGotoInComplete = NewState(EnumMacDrawerState.UnloadGotoInComplete);
            MacState sUnloadMainworkGotoInFail=NewState(EnumMacDrawerState.UnloadMainworkGotoInFail);
            MacState sUnloadMainworkGotoInTimeOut = NewState(EnumMacDrawerState.UnloadMainworkGotoInTimeOut);
            MacState sUnloadComplete = NewState(EnumMacDrawerState.UnloadComplete);

            // Load(Post)
            MacState sIdleReadyForUnloadBoxAtIn = NewState(EnumMacDrawerState.IdleReadyForUnloadBoxAtIn);
            MacState sUnloadBoxAtInComplete = NewState(EnumMacDrawerState.UnloadBoxAtInComplete);

           

          


           

            #endregion State



            #region  Register Event OnEntry
            sAnyState.OnEntry += sAnyState_OnEntry;
            sWaitInitial.OnEntry += sWaitInitial_OnEntry;
            sInitialStart.OnEntry += sInitialStart_OnEntry;
            sInitialIng.OnEntry += sInitialIng_OnEntry;
            sInitialComplete.OnEntry += sInitialComplete_OnEntry;
            sInitialTimeOut.OnEntry += sInitialTimeOut_OnEntry;
            sInitialFail.OnEntry += sInitialFail_OnEntry;


            sLoadAnyState.OnEntry += sLoadAnyState_OnEntry;
            sLoadGotoInStart.OnEntry += sLoadGotoInStart_OnEntry;
            sLoadGotoInIng.OnEntry += sLoadGotoInIng_OnEntry;
            sLoadGotoInComplete.OnEntry += sLoadGotoInComplete_OnEntry;
            sLoadPrework1Fail.OnEntry += sLoadPrework1Fail_OnEntry;
            sLoadPrework1TimeOut.OnEntry += sLoadPrework1TimeOut_OnEntry;


            sIdleReadyForLoadBoxAtIn.OnEntry += sIdleReadyForLoadBoxAtIn_OnEntry;
            sLoadBoxAtInComplete.OnEntry += sLoadBoxAtInComplete_OnEntry;


            sLoadGotoHomeStart.OnEntry += sLoadGotoHomeStart_OnEntry;
            sLoadGotoHomeIng.OnEntry += sLoadGotoHomeIng_OnEntry;
            sLoadGotoHomeComplete.OnEntry += sLoadGotoHomeComplete_OnEntry;
            sLoadMainworkGotoHomeTimeOut.OnEntry += sLoadMainworkGotoHomeTimeout_OnEntry;
            sLoadMainworkGotoHomeFail.OnEntry += sLoadMainworkGotoHomeFail_OnEntry;

            sLoadGotoOutStart.OnEntry += sLoadGotoOutStart_OnEntry;
            sLoadGotoOutIng.OnEntry += sLoadGotoOutIng_OnEntry;
            sLoadGotoOutComplete.OnEntry += sLoadGotoOutComplete_OnEntry;
            sLoadComplete.OnEntry += null;// sLoadComplete_OnEntry;
            sLoadMainworkGotoOutTimeOut.OnEntry += sLoadMainworkGotoOutTimeout_OnEntry;
            sLoadMainworkGotoOutFail.OnEntry += sLoadMainworkGotoOutFail_OnEntry;


            sIdleReadyForGetBox.OnEntry += sIdleReadyForGetBox_OnEntry;
            sLoadBoxGetAtOut.OnEntry += sLoadBoxGetAtOut_OnEntry;

            sUnloadAnyState.OnEntry += sUnloadAnyState_OnEntry;
            sUnloadGotoOutStart.OnEntry += sUnloadGotoOutStart_OnEntry;
            sUnloadGotoOutIng.OnEntry += sUnloadGotoOutIng_OnEntry;
            sUnloadGotoOutComplete.OnEntry += sUnloadGotoOutComplete_OnEntry;
            sUnloadPrework1Fail.OnEntry += sUnloadPrework1Fail_OnEntry;
            sUnloadPrework1TimeOut.OnEntry += sUnloadPrework1Timeout_OnEntry;

            sIdleReadyForUnloadBoxAtOut.OnEntry += sIdleReadyForUnloadBoxAtOut_OnEntry;
            sUnloadBoxAtOutComplete.OnEntry += sUnloadBoxAtOutComplete_OnEntry;

            sUnloadGotoHomeStart.OnEntry += sUnloadGotoHomeStart_OnEntry;
            sUnloadGotoHomeIng.OnEntry += sUnloadGotoHomeIng_OnEntry;
            sUnloadGotoHomeComplete.OnEntry += sUnloadGotoHomeComplete_OnEntry;
            sUnloadMainworkGotoHomeFail.OnEntry += sUnloadGotoHomeFail_OnEntry;
            sUnloadMainworkGotoHomeTimeOut.OnEntry+= sUnloadGotoHomeTimeOut_OnEntry;

            sUnloadGotoInStart.OnEntry += sUnloadGotoInStart_OnEntry;
            sUnloadGotoInIng.OnEntry += sUnloadGotoInIng_OnEntry;
            sUnloadGotoInComplete.OnEntry += sUnloadGotoInComplete_OnEntry;
            sUnloadMainworkGotoInFail.OnEntry += sUnloadMainworkGotoInFail_OnEntry;
            sUnloadMainworkGotoInTimeOut.OnEntry += sUnloadMainworkGotoInTimeOut_OnEntry;
            sUnloadComplete.OnEntry += null;//sUnloadComplete_OnEntry;
            

            sIdleReadyForUnloadBoxAtIn.OnEntry += sIdleReadyForUnloadBoxAtIn_OnEntry;
            sUnloadBoxAtInComplete.OnEntry += sUnloadBoxAtInComplete_OnEntry;

         

            #endregion   Register Event OnEntry





            #region Register Event OnExit
            sAnyState.OnExit += sAnyState_OnExit;
            sWaitInitial.OnExit += sWaitInitial_OnExit;
            sInitialStart.OnExit += sInitialStart_OnExit;
            sInitialIng.OnExit += sInitialIng_OnExit;
            sInitialComplete.OnExit += sInitialComplete_OnExit;
            sInitialTimeOut.OnExit += sInitialTimeOut_OnExit;
            sInitialFail.OnExit += sInitialFail_OnExit;

            sLoadAnyState.OnExit += sLoadAnyState_OnExit;
            sLoadGotoInStart.OnExit += sLoadGotoInStart_OnExit;
            sLoadGotoInIng.OnExit += sLoadGotoInIng_OnExit;
            sLoadGotoInComplete.OnExit += sLoadGotoInComplete_OnExit;
            sLoadPrework1Fail.OnExit += sExpLoadPrework1Fail_OnExit;
            sLoadPrework1TimeOut.OnExit += sExpLoadPrework1TimeOut_OnExit;
            sLoadMainworkGotoHomeTimeOut.OnExit += sLoadMainworkGotoHomeTimeout_OnExit;
            sLoadMainworkGotoHomeFail.OnExit += sLoadMainworkGotoHomeFail_OnExit;
           

            sIdleReadyForLoadBoxAtIn.OnExit += sIdleReadyForLoadBoxAtIn_OnExit;
            sLoadBoxAtInComplete.OnExit += sLoadBoxAtInComplete_OnExit;


            sLoadGotoHomeStart.OnExit += sLoadGotoHomeStart_OnExit;
            sLoadGotoHomeIng.OnExit += sLoadGotoHomeIng_OnExit;
            sLoadGotoHomeComplete.OnExit += sLoadGotoHomeComplete_OnExit;
            sLoadGotoOutStart.OnExit += sLoadGotoOutStart_OnExit;
            sLoadGotoOutIng.OnExit += sLoadGotoOutIng_OnExit;
            sLoadGotoOutComplete.OnExit += sLoadGotoOutComplete_OnExit;
            sLoadMainworkGotoOutTimeOut.OnExit += sLoadMainworkGotoOutTimeout_OnExit;
            sLoadMainworkGotoOutFail.OnExit += sLoadMainworkGotoOutFail_OnExit;
            sLoadComplete.OnExit += null;// sLoadComplete_OnExit;

            sIdleReadyForGetBox.OnExit += sIdleReadyForGetBox_OnExit;
            sLoadBoxGetAtOut.OnExit += sLoadBoxGetAtOut_OnExit;

            sUnloadAnyState.OnExit += sUnloadAnyState_OnExit;
            sUnloadGotoOutStart.OnExit += sUnloadGotoOutStart_OnExit;
            sUnloadGotoOutIng.OnExit += sUnloadGotoOutIng_OnExit;
            sUnloadGotoOutComplete.OnExit += sUnloadGotoOutComplete_OnExit;
            sUnloadPrework1Fail.OnExit += sUnloadPrework1Fail_OnExit;
            sUnloadPrework1TimeOut.OnExit += sUnloadPrework1Timeout_OnExit;

            sIdleReadyForUnloadBoxAtOut.OnExit += sIdleReadyForUnloadBoxAtOut_OnExit;
            sUnloadBoxAtOutComplete.OnExit += sUnloadBoxAtOutComplete_OnExit;

            sUnloadGotoHomeStart.OnExit += sUnloadGotoHomeStart_OnExit;
            sUnloadGotoHomeIng.OnExit += sUnloadGotoHomeIng_OnExit;
            sUnloadGotoHomeComplete.OnExit += sUnloadGotoHomeComplete_OnExit;
            sUnloadMainworkGotoHomeFail.OnExit += sUnloadGotoHomeFail_OnExit;
            sUnloadMainworkGotoHomeTimeOut.OnExit += sUnloadGotoHomeTimeOut_OnExit;

            sUnloadGotoInStart.OnExit += sUnloadGotoInStart_OnExit;
            sUnloadGotoInIng.OnExit += sUnloadGotoInIng_OnExit;
            sUnloadGotoInComplete.OnExit += sUnloadGotoInComplete_OnExit;
            sUnloadMainworkGotoInFail.OnExit += sUnloadMainworkGotoInFail_OnExit;
            sUnloadMainworkGotoInTimeOut.OnExit += sUnloadMainworkGotoInTimeOut_OnExit;

            sUnloadComplete.OnExit += null;// sUnloadComplete_OnExit;

            sIdleReadyForUnloadBoxAtIn.OnExit += sIdleReadyForUnloadBoxAtIn_OnExit;
            sUnloadBoxAtInComplete.OnExit += sUnloadBoxAtInComplete_OnExit;


         




           
         
            #endregion  Event OnExit

            #region Transition

            // Initial,
            MacTransition tAnyState_WaitInitial = NewTransition(sAnyState, sWaitInitial, EnumMacDrawerTransition.AnyState_WaitInitial);
            MacTransition tWaitInitial_InitialStart = NewTransition(sWaitInitial, sInitialStart, EnumMacDrawerTransition.WaitInitial_InitialStart);
            MacTransition tInitialStart_InitialIng = NewTransition(sInitialStart, sInitialIng, EnumMacDrawerTransition.InitialStart_InitialIng);
            MacTransition tInitialing_InitialComplete = NewTransition(sInitialIng, sInitialComplete, EnumMacDrawerTransition.Initialing_InitialComplete);
            MacTransition tInitial_InitialFail = NewTransition(sInitialIng, sInitialFail, EnumMacDrawerTransition.Initial_InitialFail);
            MacTransition tInitial_InitialTimeOut = NewTransition(sInitialIng, sInitialTimeOut, EnumMacDrawerTransition.Initial_InitialTimeOut);

            // Load-Prework1(將 Tray 移到 定位~ Home的位置 )
            MacTransition tLoadAnyState_LoadGotoInStart = NewTransition(sLoadAnyState, sLoadGotoInStart, EnumMacDrawerTransition.LoadAnyState_LoadGotoInStart);
            MacTransition tLoadGotoInStart_LoadGotoInIng = NewTransition(sLoadGotoInStart, sLoadGotoInIng, EnumMacDrawerTransition.LoadGotoInStart_LoadGotoInIng);
            MacTransition tLoadGotoInIng_LoadGotoInComplete = NewTransition(sLoadGotoInIng, sLoadGotoInComplete, EnumMacDrawerTransition.LoadGotoInIng_LoadGotoInComplete);
            MacTransition tLoadPrework1_LoadPrework1TimeOut = NewTransition(sLoadGotoInIng, sLoadPrework1TimeOut, EnumMacDrawerTransition.LoadPrework1_LoadPrework1TimeOut);
            MacTransition tLoadPrework1_LoadPrework1Fail = NewTransition(sLoadGotoInIng, sLoadPrework1Fail, EnumMacDrawerTransition.LoadPrework1_LoadPrework1Fail);


            // Load-Prework2(放入 Box)
            MacTransition tLoadGotoInComplete_IdleReadyForLoadBoxAtIn = NewTransition(sLoadGotoInComplete, sIdleReadyForLoadBoxAtIn, EnumMacDrawerTransition.LoadGotoInComplete_IdleReadyForLoadBoxAtIn);
            MacTransition tIdleReadyForLoadBoxAtIn_LoadBoxAtInComplete = NewTransition(sIdleReadyForLoadBoxAtIn, sLoadBoxAtInComplete, EnumMacDrawerTransition.IdleReadyForLoadBoxAtIn_LoadBoxAtInComplete);


            // Load01 (將 Tray 從 In 移到 Home) 
            MacTransition tLoadBoxAtInComplete_LoadGotoHomeStart = NewTransition(sLoadBoxAtInComplete, sLoadGotoHomeStart, EnumMacDrawerTransition.LoadBoxAtInComplete_LoadGotoHomeStart);
            MacTransition tLoaGotoHomeStart_LoadGotoHomeIng = NewTransition(sLoadGotoHomeStart, sLoadGotoHomeIng, EnumMacDrawerTransition.LoadGotoHomeStart_LoadGotoHomeIng);
            MacTransition tLoadGotoHomeIng_LoadGotoHomeComplete = NewTransition(sLoadGotoHomeIng, sLoadGotoHomeComplete, EnumMacDrawerTransition.LoadGotoHomeIng_LoadGotoHomeComplete);
            MacTransition tLoadMainwork_LoadGotoToHomeTimeOut = NewTransition(sLoadGotoHomeIng, sLoadMainworkGotoHomeTimeOut, EnumMacDrawerTransition.LoadMainwork_LoadGotoHomeTimeOut);
            MacTransition tLoadMainwork_LoadGotoToHomeFail = NewTransition(sLoadGotoHomeIng, sLoadMainworkGotoHomeTimeOut, EnumMacDrawerTransition.LoadMainwork_LoadGotoHomeFail);
            // Load02 (將 Tray 從 Home 移到 Out)
            MacTransition tLoadGotoHomeComplete_LoadGotoOutStart = NewTransition(sLoadGotoHomeIng, sLoadGotoOutStart, EnumMacDrawerTransition.LoadGotoHomeComplete_LoadGotoOutStart);
            MacTransition tLoadGotoOutStart_LoadGotoOutIng = NewTransition(sLoadGotoOutStart, sLoadGotoOutIng, EnumMacDrawerTransition.LoadGotoOutStart_LoadGotoOutIng);
            MacTransition tLoadGotoOutIng_LoadGotoOutComplete = NewTransition(sLoadGotoOutIng, sLoadGotoOutComplete, EnumMacDrawerTransition.LoadGotoOutIng_LoadGotoOutComplete);
            MacTransition tLoadMainwork_LoadGotoOutTimeOut= NewTransition(sLoadGotoOutIng, sLoadMainworkGotoOutTimeOut, EnumMacDrawerTransition.LoadMainwork_LoadGotoOutTimeOut);
            MacTransition tLoadMainwork_LoadGotoOutFail = NewTransition(sLoadGotoOutIng, sLoadMainworkGotoOutFail, EnumMacDrawerTransition.LoadMainwork_LoadGotoOutFail);
            // Load (將 Box 移開)
            MacTransition tLoadGotoOutComplete_IdleReadyForGetBox = NewTransition(sLoadGotoHomeComplete, sIdleReadyForGetBox, EnumMacDrawerTransition.LoadGotoOutComplete_IdleReadyForGetBox);
            MacTransition tIdleReadyForGetBox_LoadBoxGetAtOut = NewTransition(sIdleReadyForGetBox, sLoadBoxGetAtOut, EnumMacDrawerTransition.IdleReadyForGetBox_LoadBoxGetAtOut);


            // Unload Prework 01(將 Tray 移到 Out 位置)
            MacTransition tUnloadAnyState_UnloadGotoOutStart = NewTransition(sUnloadAnyState, sUnloadGotoOutStart, EnumMacDrawerTransition.UnloadAnyState_UnloadGotoOutStart);
            MacTransition tUnloadGotoOutStart_UnloadGotoOutIng = NewTransition(sUnloadGotoOutStart, sUnloadGotoOutIng, EnumMacDrawerTransition.UnloadGotoOutStart_UnloadGotoOutIng);
            MacTransition tUnloadGotoOutIng_UnloadGotoOutComplete = NewTransition(sUnloadGotoOutIng, sUnloadGotoOutComplete, EnumMacDrawerTransition.UnloadGotoOutIng_UnloadGotoOutComplete);
            MacTransition tUnloadPrework1_UnloadPrework1Fail= NewTransition(sUnloadGotoOutIng, sUnloadPrework1Fail, EnumMacDrawerTransition.UnLoadPrework1_UnLoadPrework1Fail);
            MacTransition tUnloadPrework1_UnloadPrework1TimeOut = NewTransition(sUnloadGotoOutIng, sUnloadPrework1TimeOut, EnumMacDrawerTransition.UnLoadPrework1_UnLoadPrework1TimeOut);

            // Unload Prework 02(放入 Box)
            MacTransition tUnloadGotoOutComplete_IdleReadyForUnloadBoxAtOut = NewTransition(sUnloadGotoOutComplete, sIdleReadyForUnloadBoxAtOut, EnumMacDrawerTransition.UnloadGotoOutComplete_IdleReadyForUnloadBoxAtOut);
            MacTransition tIdleReadyForUnloadBoxAtOut_UnloadBoxAtOutComplete = NewTransition(sIdleReadyForUnloadBoxAtOut, sUnloadBoxAtOutComplete, EnumMacDrawerTransition.IdleReadyForUnloadBoxAtOut_UnloadBoxAtOutComplete);

            // UnLoad01(將Tray 移到 Home) 
            MacTransition tUnloadBoxAtOutComplete_UnloadGotoHomeStart = NewTransition(sUnloadBoxAtOutComplete, sUnloadGotoHomeStart, EnumMacDrawerTransition.UnloadBoxAtOutComplete_UnloadGotoHomeStart);
            MacTransition tUnloadGotoHomeStart_UnloadGotoHomeIng = NewTransition(sUnloadGotoHomeStart, sUnloadGotoHomeIng, EnumMacDrawerTransition.UnloadGotoHomeStart_UnloadGotoHomeIng);
            MacTransition tUnloadGotoHomeIng_UnloadGotoHomeComplete = NewTransition(sUnloadGotoHomeIng, sUnloadGotoHomeComplete, EnumMacDrawerTransition.UnloadGotoHomeIng_UnloadGotoHomeComplete);
            MacTransition tUnloadMainwork_UnloadGotoHomeTimeOut = NewTransition(sUnloadGotoHomeIng, sUnloadMainworkGotoHomeTimeOut, EnumMacDrawerTransition.UnloadMainwork_UnloadGotoHomeTimeOut);
            MacTransition tUnloadMainwork_UnloadGotoHomeFail = NewTransition(sUnloadGotoHomeIng, sUnloadMainworkGotoHomeFail, EnumMacDrawerTransition.UnloadMainwork_UnloadGotoHomeFail);
            //UnloadMainwork_UnloadGotoHomeFail,
            // Unload02(將 Tray 移到 In)
            MacTransition tUnloadGotoHomeComplete_UnloadGotoInStart = NewTransition(sUnloadGotoHomeComplete, sUnloadGotoInStart, EnumMacDrawerTransition.UnloadGotoHomeComplete_UnloadGotoInStart);
            MacTransition tUnloadGotoInStart_UnloadGotoInIng = NewTransition(sUnloadGotoInStart, sUnloadGotoInIng, EnumMacDrawerTransition.UnloadGotoInStart_UnloadGotoInIng);
            MacTransition tUnloadGotoInIng_UnloadGotoInComplete = NewTransition(sUnloadGotoHomeIng, sUnloadGotoHomeComplete, EnumMacDrawerTransition.UnloadGotoInIng_UnloadGotoInComplete);

            // Unload PostWork(取走 Box)
            MacTransition tUnloadGotoHomeComplete_IdleReadyForUnloadBoxAtIn = NewTransition(sUnloadGotoHomeComplete, sIdleReadyForUnloadBoxAtIn, EnumMacDrawerTransition.UnloadGotoHomeComplete_IdleReadyForUnloadBoxAtIn);
            MacTransition tIdleReadyForUnloadBoxAtIn_UnloadComplete = NewTransition(sIdleReadyForUnloadBoxAtIn, sUnloadComplete, EnumMacDrawerTransition.IdleReadyForUnloadBoxAtIn_UnloadComplete);
            #endregion




        }

        #region Event OnEntry Target
        void sAnyState_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var state = (MacState)sender;
            Action guard = () =>
            {
                while (true)
                {
                    if (HalDrawer.CurrentWorkState == DrawerWorkState.ReadyToInitial)
                    {// Drawer 目前為
                        state.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    Thread.Sleep(10);
                }
            };
            var t = new Task(guard);
            t.Start();
            HalDrawer.SetDrawerWorkState(DrawerWorkState.ReadyToInitial);
        }
        void sWaitInitial_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var state = (MacState)sender;

            Action guard = () =>
            {
                while (true)
                {
                    if (HalDrawer.CurrentWorkState == DrawerWorkState.InitialStart)
                    {
                        state.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    Thread.Sleep(10);
                }

            };
            new Task(guard).Start();
            HalDrawer.CommandINI();
        }
        void sInitialStart_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var state = (MacState)sender;
            Action guard = () =>
            {
                while (true)
                {
                    if (HalDrawer.CurrentWorkState == DrawerWorkState.InitialIng)
                    {
                        state.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }
        void sInitialIng_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var state = (MacState)sender;
            var startTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {

                    if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome)
                    {  // Initial  Complete
                        state.DoExit(new MacDrawerStateInitialExitEventArgs(MacDrawerStateInitialResult.Complete));
                        break;
                    }
                    else if (HalDrawer.CurrentWorkState == DrawerWorkState.InitialFailed)
                    {  // Initial Failed
                        state.DoExit(new MacDrawerStateInitialExitEventArgs(MacDrawerStateInitialResult.Failed));
                        break;
                    }
                    else if (timeoutObj.IsTimeOut(startTime))
                    {
                        state.DoExit(new MacDrawerStateInitialExitEventArgs(MacDrawerStateInitialResult.TimeOut));
                        break;
                    }
                    Thread.Sleep(10);
                }

            };
            new Task(guard).Start();
        }
        void sInitialComplete_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            //Final State of Initial
            var state = (MacState)sender;
            // TODO: 依實際狀況加 Code

            state.DoExit(new MacStateExitEventArgs());

        }


        void sLoadAnyState_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;

            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToInStart;
                    if (isGuard)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
            HalDrawer.CommandTrayMotionIn();

        }
        void sLoadGotoInStart_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToInIng;
                    if (isGuard)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }
        void sLoadGotoInIng_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var state = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    // var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.TrayArraiveAtIn;
                    if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArraiveAtIn)
                    {// Complete
                        state.DoExit(new MacDrawerStateLoadPrework1ExitEventArgs(MacDrawerStateLoadPrework1Result.Complete));
                        break;
                    }
                    else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                    {   // Failed
                        state.DoExit(new MacDrawerStateLoadPrework1ExitEventArgs(MacDrawerStateLoadPrework1Result.Failed));
                        break;
                    }
                    else if (timeoutObj.IsTimeOut(thisTime))
                    {    // Time Out
                        state.DoExit(new MacDrawerStateLoadPrework1ExitEventArgs(MacDrawerStateLoadPrework1Result.TimeOut));
                        break;
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }
        void sLoadGotoInComplete_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            // Final State of Load Prework1
        }
        void sIdleReadyForLoadBoxAtIn_OnEntry(object sender, MacStateEntryEventArgs e) { }
        void sLoadBoxAtInComplete_OnEntry(object sender, MacStateEntryEventArgs e) { }


        void sLoadGotoHomeStart_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var state = (MacState)sender;
            //DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToHomeIng;
                    if (isGuard)
                    {
                        state.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                  
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
            HalDrawer.CommandTrayMotionHome();
        }
        void sLoadGotoHomeIng_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var state = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                   // var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome;
                    if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome)
                    {
                        // Complete
                        state.DoExit(new MacDrawerStateLoadMainworkExitEventArgs(MacDrawerStateLoadMainworkResult.GotoHomeComplete));
                        break;
                    }
                    else if (timeoutObj.IsTimeOut(thisTime))
                    {
                        state.DoExit(new MacDrawerStateLoadMainworkExitEventArgs(MacDrawerStateLoadMainworkResult.GotoHomeTimeOut));
                        break;
                    }
                    else if(HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                    {
                        state.DoExit(new MacDrawerStateLoadMainworkExitEventArgs(MacDrawerStateLoadMainworkResult.GotoHomeFail));
                        break;
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }




        void sLoadGotoHomeComplete_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            
            Action guard = () =>
            {
                while (true)
                {
                    if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToOutStart)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
            HalDrawer.CommandTrayMotionOut();
        }
        void sLoadGotoOutStart_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
           
            Action guard = () =>
            {
                while (true)
                {
                  //  var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToOutIng;
                    if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToOutIng)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }
        void sLoadGotoOutIng_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtOut)
                    {
                        // 完成
                        thisState.DoExit(new MacDrawerStateLoadMainworkExitEventArgs(MacDrawerStateLoadMainworkResult.GotoOutComplete));
                        break;
                    }
                    else if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // 逾時
                        thisState.DoExit(new MacDrawerStateLoadMainworkExitEventArgs(MacDrawerStateLoadMainworkResult.GotoOutTimeOut));
                        break;
                    }
                    else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                    {
                        // 無法完成
                        thisState.DoExit(new MacDrawerStateLoadMainworkExitEventArgs(MacDrawerStateLoadMainworkResult.GotoOutFail));
                        break;
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
            //HalDrawer.CommandTrayMotionOut();
        }
        void sLoadGotoOutComplete_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            // Final State Of Load Main
        }

        void sIdleReadyForGetBox_OnEntry(object sender, MacStateEntryEventArgs e) { }
        void sLoadBoxGetAtOut_OnEntry(object sender, MacStateEntryEventArgs e) { }
        void sUnloadAnyState_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            Action guard = () =>
            {
                while (true)
                {
                    if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToOutStart)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
            HalDrawer.CommandTrayMotionOut();

        }
        void sUnloadGotoOutStart_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            Action guard = () =>
            {
                while (true)
                {
                    if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToOutIng)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();

        }

        void sUnloadGotoOutIng_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                   
                    if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtOut)
                    {
                        // 完成
                        thisState.DoExit(new MacDrawerStateUnloadPrework1ExitEventArgs(MacDrawerStateUnloadPrework1Result.Complete));
                        break;
                    }
                    else if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // 逾時
                        thisState.DoExit(new MacDrawerStateUnloadPrework1ExitEventArgs(MacDrawerStateUnloadPrework1Result.TimeOut));
                        break;
                    }
                    else if(HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                    {
                        // 執行不成功
                        thisState.DoExit(new MacDrawerStateUnloadPrework1ExitEventArgs(MacDrawerStateUnloadPrework1Result.Failed));
                        break;
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();

        }
        void sUnloadGotoOutComplete_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            // Final State of Unload Prework1
            var state = (MacState)sender;
            state.DoExit(new MacStateExitEventArgs());

        }
        void sUnloadPrework1Fail_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var state = (MacState)sender;
            state.DoExit(new MacStateExitEventArgs());
        }
        void sUnloadPrework1Timeout_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var state = (MacState)sender;
            state.DoExit(new MacStateExitEventArgs());
        }

        void sIdleReadyForUnloadBoxAtOut_OnEntry(object sender, MacStateEntryEventArgs e) { }
        void sUnloadBoxAtOutComplete_OnEntry(object sender, MacStateEntryEventArgs e) { }

        void sUnloadGotoHomeStart_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
          
            Action guard = () =>
            {
                while (true)
                {
                   if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToHomeIng)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                   Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
            HalDrawer.CommandTrayMotionHome();
        }
        void sUnloadGotoHomeIng_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome)
                    {
                        thisState.DoExit(new MacDrawerStateUnloadMainworkExitEventArgs(MacDrawerStateUnloadMainworkResult.GotoHomeComplete));
                        break;
                    }
                    else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                    {
                        thisState.DoExit(new MacDrawerStateUnloadMainworkExitEventArgs(MacDrawerStateUnloadMainworkResult.GotoHomeFail));
                        break;
                    }
                    else if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacDrawerStateUnloadMainworkExitEventArgs(MacDrawerStateUnloadMainworkResult.GotoHomeTimeOut));
                        break;
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
            // HalDrawer.CommandTrayMotionHome();
        }
        void sUnloadGotoHomeComplete_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToInStart)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                   Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
            HalDrawer.CommandTrayMotionIn();
        }
        void sUnloadGotoHomeFail_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var state = (MacState)sender;
            // Unload 從 out 走到 In 時失敗
            // TODO: 待實際動作確認之後再加上程式碼
            state.DoExit(new MacStateExitEventArgs());
        }
        void sUnloadGotoHomeTimeOut_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var state = (MacState)sender;
            // Unload 從 out 走到 In 時失敗
            // TODO: 待實際動作確認之後再加上程式碼
            state.DoExit(new MacStateExitEventArgs());
        }



        void sUnloadGotoInStart_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            Action guard = () =>
            {
                while (true)
                {
                    if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToInIng)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
            //   HalDrawer.CommandTrayMotionIn();
        }
        void sUnloadGotoInIng_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArraiveAtIn)
                    {
                        thisState.DoExit(new MacDrawerStateUnloadMainworkExitEventArgs(MacDrawerStateUnloadMainworkResult.GotoInComplete));
                        break;
                    }
                    else if(HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                    {
                        thisState.DoExit(new MacDrawerStateUnloadMainworkExitEventArgs(MacDrawerStateUnloadMainworkResult.GotoInFail));
                        break;
                    }
                    else if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacDrawerStateUnloadMainworkExitEventArgs(MacDrawerStateUnloadMainworkResult.GotoInTimeOut));
                        break;
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }
        void sUnloadGotoInComplete_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            // final State of Unload MAin
            var state = (MacState)sender;
            state.DoExit(new MacStateExitEventArgs());
        }
        void sUnloadMainworkGotoInFail_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var state = (MacState)sender;
            state.DoExit(new MacStateExitEventArgs());
        }
        void  sUnloadMainworkGotoInTimeOut_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var state = (MacState)sender;
            state.DoExit(new MacStateExitEventArgs());
        }
        //  void sUnloadComplete_OnEntry(object sender, MacStateEntryEventArgs e) { }
        void sIdleReadyForUnloadBoxAtIn_OnEntry(object sender, MacStateEntryEventArgs e) { }
        void sUnloadBoxAtInComplete_OnEntry(object sender, MacStateEntryEventArgs e) { }


        void sInitialFail_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            // Initial Fail  Entry的處理函式
            var thisState = (MacState)sender;
            // TODO: 補上實作
            thisState.DoExit(new MacStateExitEventArgs());
        }
        void sInitialTimeOut_OnEntry(object sender, MacStateEntryEventArgs e)
        {   // Initial TimeOut Entry 的處理函式
            var thisState = (MacState)sender;
            // TODO:  補上實作

            thisState.DoExit(new MacStateExitEventArgs());
        }
        void sLoadPrework1TimeOut_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            // Load 預先工作(將 Tray 移到 In 的位置) ,工作逾時
            var state = (MacState)sender;
            // TODO: 補上實作 
            state.DoExit(new MacStateExitEventArgs());
        }
        void sLoadPrework1Fail_OnEntry(object sender, MacStateEntryEventArgs e)
        {  // Load 預先工作(將 Tray 移到 In 的位置), 無法完成工作
            var state = (MacState)sender;
            // TODO: 補上實作
            state.DoExit(new MacStateExitEventArgs());
        }
        void sLoadMainworkGotoHomeTimeout_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var state = (MacState)sender;
            state.DoExit(new MacStateExitEventArgs());
        }
        void sLoadMainworkGotoHomeFail_OnEntry(object sender, MacStateEntryEventArgs e) 
        {
            var state = (MacState)sender;
            state.DoExit(new MacStateExitEventArgs());
        }

        void sLoadMainworkGotoOutTimeout_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var state = (MacState)sender;
            state.DoExit(new MacStateExitEventArgs());
        }
        void sLoadMainworkGotoOutFail_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var state = (MacState)sender;
            state.DoExit(new MacStateExitEventArgs());
        }

        #endregion  Event OnExit Target

        #region Event OnExit Target
        void sAnyState_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacDrawerTransition.AnyState_WaitInitial.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sWaitInitial_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacDrawerTransition.WaitInitial_InitialStart.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sInitialStart_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition= this.Transitions[EnumMacDrawerTransition.InitialStart_InitialIng.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sInitialIng_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacDrawerStateInitialExitEventArgs)e;
           // var state = (MacState)sender;
            if (args.Result== MacDrawerStateInitialResult.Complete)
            {  // Initial Complete
                var thisTransition = this.Transitions[EnumMacDrawerTransition.Initialing_InitialComplete.ToString()];
                var nextState = thisTransition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            }
            else if(args.Result == MacDrawerStateInitialResult.TimeOut)
            {  // Initial Timeout
                var thisTransition = this.Transitions[EnumMacDrawerTransition.Initial_InitialTimeOut.ToString()];
                var nextState = thisTransition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            }
            else // if (args.InitialResult == MacDrawerStateInitialResult.Failed)
            {  // Initial Failed
                var thisTransition = this.Transitions[EnumMacDrawerTransition.Initial_InitialFail.ToString()];
                var nextState = thisTransition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            }
        }
        void sInitialComplete_OnExit(object sender, MacStateExitEventArgs e)
        {
            // Final State of Initial
            var state = (MacState)sender;
            // TODO: 依實際狀況補上Code
        }
        void sInitialTimeOut_OnExit(object sender, MacStateExitEventArgs e)
        {
            // Initial Timeout Exit 的處理函式
            var thisState = (MacState)sender;
            // TODO: 看實務上如何處理 Initial Timeout再補上程式碼
        }
       
        void sInitialFail_OnExit(object sender, MacStateExitEventArgs e)
        {  // Initial Fail Exit 的處理函式
            var thisState = (MacState)sender;
            // TODO: 看實務上如何處理 Initial Fail再補上程式碼
        }
        void sLoadAnyState_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition= this.Transitions[EnumMacDrawerTransition.LoadAnyState_LoadGotoInStart.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));

        }
        void sLoadGotoInStart_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisTransition= this.Transitions[EnumMacDrawerTransition.LoadGotoInStart_LoadGotoInIng.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sLoadGotoInIng_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacDrawerStateLoadPrework1ExitEventArgs)e;
            MacTransition transition=null;
            if (args.Result == MacDrawerStateLoadPrework1Result.Complete)
            {   // 完成
                transition = this.Transitions[EnumMacDrawerTransition.LoadGotoInIng_LoadGotoInComplete.ToString()];
               
            }
            else if(args.Result == MacDrawerStateLoadPrework1Result.Failed)
            {   // 失敗
                transition = this.Transitions[EnumMacDrawerTransition.LoadPrework1_LoadPrework1Fail.ToString()];
            }
            else //if(args.InitialResult == MacDrawerStateLoadPrework1Result.TimeOut)
            {   // 逾時
                transition = this.Transitions[EnumMacDrawerTransition.LoadPrework1_LoadPrework1TimeOut.ToString()];
            }
            var nextState = transition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sLoadGotoInComplete_OnExit(object sender, MacStateExitEventArgs e)
        {
            // Final state of Load Prework1
            // TODO: 配合實務操作再補上 Code
        }

        void sExpLoadPrework1TimeOut_OnExit(object sender, MacStateExitEventArgs e)
        {
            // Load 的預先工作, 將 Tray 移到 In 的位置, 逾時未到
            // TODO: 配合操作再補上 Code 
        }
        void sExpLoadPrework1Fail_OnExit(object sender, MacStateExitEventArgs e)
        {
            // Load 的預先工作, 將 Tray 移到 In 的位置, 移動失敗
            // TODO: 配合操作再補上 Code 
        }
        void sLoadMainworkGotoHomeTimeout_OnExit(object sender, MacStateExitEventArgs e)
        {
            // Load 主要工作, 將 Tray 移到 Home 時逾時未到 
        }
        void sLoadMainworkGotoHomeFail_OnExit(object sender, MacStateExitEventArgs e)
        {
            // Load 主要工作, 將 Tray 移到 Home 時失敗
        }



        void sIdleReadyForLoadBoxAtIn_OnExit(object sender, MacStateExitEventArgs e) { }
        void sLoadBoxAtInComplete_OnExit(object sender, MacStateExitEventArgs e) { }


        void sLoadGotoHomeStart_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisTransition = this.Transitions[EnumMacDrawerTransition.LoadGotoHomeStart_LoadGotoHomeIng.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sLoadGotoHomeIng_OnExit(object sender, MacStateExitEventArgs e)
        {
            MacTransition transition = null;
            var args=(MacDrawerStateLoadMainworkExitEventArgs)e;
            if (args.Result == MacDrawerStateLoadMainworkResult.GotoHomeComplete)
            {
               transition= this.Transitions[EnumMacDrawerTransition.LoadGotoHomeIng_LoadGotoHomeComplete.ToString()];
            }
            else if (args.Result == MacDrawerStateLoadMainworkResult.GotoHomeTimeOut)
            {
                transition = this.Transitions[EnumMacDrawerTransition.LoadMainwork_LoadGotoHomeTimeOut.ToString()];
            }
            else //if(args.Result==MacDrawerStateLoadMainworkResult.GotoOutFail)
            {
                transition = this.Transitions[EnumMacDrawerTransition.LoadMainwork_LoadGotoHomeFail.ToString()];
            }
           
            var nextState = transition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sLoadGotoHomeComplete_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisTransition = this.Transitions[EnumMacDrawerTransition.LoadGotoHomeComplete_LoadGotoOutStart.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sLoadGotoOutStart_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisTransition = this.Transitions[EnumMacDrawerTransition.LoadGotoOutStart_LoadGotoOutIng.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sLoadGotoOutIng_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args=(MacDrawerStateLoadMainworkExitEventArgs)e;
            MacTransition transition = null;
            if (args.Result == MacDrawerStateLoadMainworkResult.GotoOutComplete)
            {
                transition = this.Transitions[EnumMacDrawerTransition.LoadGotoOutIng_LoadGotoOutComplete.ToString()];
            }
            else if(args.Result==MacDrawerStateLoadMainworkResult.GotoOutFail)
            {
                transition = this.Transitions[EnumMacDrawerTransition.LoadMainwork_LoadGotoOutFail.ToString()];
            }
            else// if (args.Result == MacDrawerStateLoadMainworkResult.GotoHomeTimeOut)
            {
                transition = this.Transitions[EnumMacDrawerTransition.LoadMainwork_LoadGotoOutTimeOut.ToString()];
            }
            
            var nextState = transition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sLoadGotoOutComplete_OnExit(object sender, MacStateExitEventArgs e)
        {
          // Final State Of Load Main
        }
        
        void sIdleReadyForGetBox_OnExit(object sender, MacStateExitEventArgs e) { }
        void sLoadBoxGetAtOut_OnExit(object sender, MacStateExitEventArgs e) { }
        void sUnloadAnyState_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisTransition = this.Transitions[EnumMacDrawerTransition.UnloadAnyState_UnloadGotoOutStart.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sUnloadGotoOutStart_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisTransition = this.Transitions[EnumMacDrawerTransition.UnloadGotoOutStart_UnloadGotoOutIng.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sUnloadGotoOutIng_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacDrawerStateUnloadPrework1ExitEventArgs)e;
            MacTransition transition = null;
            if (args.Result== MacDrawerStateUnloadPrework1Result.Complete)
            {
               transition= this.Transitions[EnumMacDrawerTransition.UnloadGotoOutIng_UnloadGotoOutComplete.ToString()];
            }
            else if(args.Result == MacDrawerStateUnloadPrework1Result.Failed)
            {
                transition = this.Transitions[EnumMacDrawerTransition.UnLoadPrework1_UnLoadPrework1Fail.ToString()];
            }
            else if(args.Result == MacDrawerStateUnloadPrework1Result.TimeOut)
            {
                transition = this.Transitions[EnumMacDrawerTransition.UnLoadPrework1_UnLoadPrework1TimeOut.ToString()];
            }
           
            var nextState = transition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sUnloadGotoOutComplete_OnExit(object sender, MacStateExitEventArgs e)
        {
            // Final State of unload Prework1
            // TODO: 依實際操作再加上程式碼
        }
        void sUnloadPrework1Fail_OnExit(object sender, MacStateExitEventArgs e)
        {
            // Unload 預置工作1 未完成
            // TODO: 按實際操作再補上程式碼
        }
        void sUnloadPrework1Timeout_OnExit(object sender, MacStateExitEventArgs e)
        {
            // Unload 前置工作1 逾時
            // TODO: 按實際工作再補上程式碼
        }
        void sIdleReadyForUnloadBoxAtOut_OnExit(object sender, MacStateExitEventArgs e) { }
        void sUnloadBoxAtOutComplete_OnExit(object sender, MacStateExitEventArgs e) { }

        void sUnloadGotoHomeStart_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisTransition = this.Transitions[EnumMacDrawerTransition.UnloadGotoHomeStart_UnloadGotoHomeIng.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sUnloadGotoHomeIng_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacDrawerStateUnloadMainworkExitEventArgs)e;
            MacTransition transition = null;
            if(args.Result== MacDrawerStateUnloadMainworkResult.GotoHomeComplete)
            {
                transition = this.Transitions[EnumMacDrawerTransition.UnloadGotoHomeIng_UnloadGotoHomeComplete.ToString()];
            }
            else if(args.Result == MacDrawerStateUnloadMainworkResult.GotoHomeFail)
            {
                transition = this.Transitions[EnumMacDrawerTransition.UnloadMainwork_UnloadGotoHomeFail.ToString()];
            }
            else //if(args.Result== MacDrawerStateUnloadMainworkResult.GotoHomeTimeOut)
            {
                transition = this.Transitions[EnumMacDrawerTransition.UnloadMainwork_UnloadGotoHomeTimeOut.ToString()];
            }
            var nextState = transition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sUnloadGotoHomeComplete_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisTransition = this.Transitions[EnumMacDrawerTransition.UnloadGotoHomeComplete_UnloadGotoInStart.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sUnloadGotoHomeFail_OnExit(object sender, MacStateExitEventArgs e)
        {
            // Unload 由Out 位置回到 Home 時移動失敗
            // TODO: 依實際動作再補上其他 Code
            // TODO: Transition ?
        }
        void sUnloadGotoHomeTimeOut_OnExit(object sender, MacStateExitEventArgs e)
        {
            // Unload 由Out 位置回到 Home 時逾時
            // TODO: 依實際動作再補上其他 Code
            // TODO: Transition ?
        }
        void sUnloadGotoInStart_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisTransition = this.Transitions[EnumMacDrawerTransition.UnloadGotoInStart_UnloadGotoInIng.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sUnloadGotoInIng_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args=(MacDrawerStateUnloadMainworkExitEventArgs)e;
            MacTransition transition = null;
            if(args.Result== MacDrawerStateUnloadMainworkResult.GotoInComplete)
            {
                transition = this.Transitions[EnumMacDrawerTransition.UnloadGotoInIng_UnloadGotoInComplete.ToString()];
            }
            else if (args.Result == MacDrawerStateUnloadMainworkResult.GotoInTimeOut)
            {
                transition = this.Transitions[EnumMacDrawerTransition.UnloadMainwork_UnloadGotoInFail.ToString()];
            }
            else //if (args.Result == MacDrawerStateUnloadMainworkResult.GotoInFail)
            {
                transition = this.Transitions[EnumMacDrawerTransition.UnloadMainwork_UnloadGotoInTimeOut.ToString()   ];
            }
            var nextState = transition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sUnloadGotoInComplete_OnExit(object sender, MacStateExitEventArgs e)
        {
           // final state of Unload Main 
           // TODO: Transition ?
        }

        void sUnloadMainworkGotoInFail_OnExit(object sender, MacStateExitEventArgs e)
        {
            // TODO: Transition ?
        }
        void sUnloadMainworkGotoInTimeOut_OnExit(object sender, MacStateExitEventArgs e)
        {
            // TODO: Transition ?
        }

        void sIdleReadyForUnloadBoxAtIn_OnExit(object sender, MacStateExitEventArgs e) { }
        void sUnloadBoxAtInComplete_OnExit(object sender, MacStateExitEventArgs e) { }


       
      
       
        void sLoadMainworkGotoOutTimeout_OnExit(object sender, MacStateExitEventArgs e)
        {  // TODO: Transition ?
        }
        void sLoadMainworkGotoOutFail_OnExit(object sender, MacStateExitEventArgs e)
        {  // TODO: Transition ?
        }

        #endregion delegates of Event OnEntry
    }
    public class MacDrawerStateTimeOutController
    {
        public bool IsTimeOut(DateTime startTime, int targetDiffSecs)
        {
            var thisTime = DateTime.Now;
            var diff = thisTime.Subtract(startTime).TotalSeconds;
            if (diff >= targetDiffSecs)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsTimeOut(DateTime startTime)
        {
            return IsTimeOut(startTime, 20);
        }
    }
}
