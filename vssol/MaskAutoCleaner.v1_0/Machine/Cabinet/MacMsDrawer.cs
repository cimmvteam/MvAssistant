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
        public IMacHalDrawer HalDrawer { get { return this.halAssembly  as IMacHalDrawer; } }
        private TimeOutController timeoutObj = new TimeOutController();
        
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

            // Load Prework1
            MacState sLoadAnyState = NewState(EnumMacDrawerState.LoadAnyState);
            MacState sLoadGotoInStart = NewState(EnumMacDrawerState.LoadGotoInStart);
            MacState sLoadGotoInIng = NewState(EnumMacDrawerState.LoadGotoInIng);
            MacState sLoadGotoInComplete = NewState(EnumMacDrawerState.LoadGotoInComplete);

            // Load Prework2
            MacState sIdleReadyForLoadBoxAtIn = NewState(EnumMacDrawerState.IdleReadyForLoadBoxAtIn);
            MacState sLoadBoxAtInComplete = NewState(EnumMacDrawerState.LoadBoxAtInComplete);

            // LoadMainWork
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

            // Exception
            MacState sExpInitialFail = NewState(EnumMacDrawerState.ExpInitialFail);
            MacState sExpInitialTimeout = NewState(EnumMacDrawerState.ExpInitialTimeout);
            MacState sExpGotoInTimeout = NewState(EnumMacDrawerState.ExpGotoInTimeout);
            MacState sExpGotoInFail = NewState(EnumMacDrawerState.ExpGotoInFail);
            MacState sExpGotoHomeTimeout = NewState(EnumMacDrawerState.ExpGotoHomeTimeout);
            MacState sExpGotoHomeFail = NewState(EnumMacDrawerState.ExpGotoHomeFail);
            MacState sExpGotoOutTimeout = NewState(EnumMacDrawerState.ExpGotoOutTimeout);
            MacState sExpGotoOutFail = NewState(EnumMacDrawerState.ExpGotoOutFail);

            #endregion State

         
            #region  Register Event OnEntry
            sAnyState.OnEntry += sAnyState_OnEntry;
            sWaitInitial.OnEntry += sWaitInitial_OnEntry;
            sInitialStart.OnEntry += sInitialStart_OnEntry;
            sInitialIng.OnEntry += sInitialIng_OnEntry;
            sInitialComplete.OnEntry += sInitialComplete_OnEntry;


            sLoadAnyState.OnEntry += sLoadAnyState_OnEntry;
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
            sLoadComplete.OnEntry += null;// sLoadComplete_OnEntry;
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
            sUnloadComplete.OnEntry += null;//sUnloadComplete_OnEntry;
            sIdleReadyForUnloadBoxAtIn.OnEntry += sIdleReadyForUnloadBoxAtIn_OnEntry;
            sUnloadBoxAtInComplete.OnEntry += sUnloadBoxAtInComplete_OnEntry;


            sExpInitialFail.OnEntry += sExpInitialFail_OnEntry;
            sExpInitialTimeout.OnEntry += sExpInitialTimeout_OnEntry;
            sExpGotoInTimeout.OnEntry += sExpGotoInTimeout_OnEntry;
            sExpGotoInFail.OnEntry += sExpGotoInFail_OnEntry;
            sExpGotoHomeTimeout.OnEntry += sExpGotoHomeTimeout_OnEntry;
            sExpGotoHomeFail.OnEntry += sExpGotoHomeFail_OnEntry;
            sExpGotoOutTimeout.OnEntry += sExpGotoOutTimeout_OnEntry;
            sExpGotoOutFail.OnEntry += sExpGotoOutFail_OnEntry;

            #endregion   Register Event OnEntry



        

            #region Register Event OnExit
            sAnyState.OnExit += sAnyState_OnExit;
            sWaitInitial.OnExit += sWaitInitial_OnExit;
            sInitialStart.OnExit += sInitialStart_OnExit;
            sInitialIng.OnExit += sInitialIng_OnExit;
            sInitialComplete.OnExit += sInitialComplete_OnExit;

            sLoadAnyState.OnExit += sLoadAnyState_OnExit;
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
            sLoadComplete.OnExit += null;// sLoadComplete_OnExit;

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
            sUnloadComplete.OnExit += null;// sUnloadComplete_OnExit;

            sIdleReadyForUnloadBoxAtIn.OnExit += sIdleReadyForUnloadBoxAtIn_OnExit;
            sUnloadBoxAtInComplete.OnExit += sUnloadBoxAtInComplete_OnExit;


            sExpInitialFail.OnExit += sExpInitialFail_OnExit;
            sExpInitialTimeout.OnExit += sExpInitialTimeout_OnExit;
            sExpGotoInTimeout.OnExit += sExpGotoInTimeout_OnExit;
            sExpGotoInFail.OnExit += sExpGotoInFail_OnExit;
            sExpGotoHomeTimeout.OnExit += sExpGotoHomeTimeout_OnExit;
            sExpGotoHomeFail.OnExit += sExpGotoHomeFail_OnExit;
            sExpGotoOutTimeout.OnExit += sExpGotoOutTimeout_OnExit;
            sExpGotoOutFail.OnExit += sExpGotoOutFail_OnExit;
            #endregion  Event OnExit

            #region Transition

            // Initial,
            MacTransition tAnyState_WaitInitial = NewTransition(sAnyState,sWaitInitial,EnumMacDrawerTransition.AnyState_WaitInitial);
            MacTransition tWaitInitial_InitialStart = NewTransition(sWaitInitial, sInitialStart, EnumMacDrawerTransition.WaitInitial_InitialStart);
            MacTransition tInitialStart_InitialIng = NewTransition(sInitialStart, sInitialIng, EnumMacDrawerTransition.InitialStart_InitialIng);
            MacTransition tInitialing_InitialComplete = NewTransition(sInitialIng, sInitialComplete, EnumMacDrawerTransition.Initialing_InitialComplete);

            // Load-Prework1(將 Tray 移到 定位~ Home的位置 )
            MacTransition tLoadAnyState_LoadGotoInStart = NewTransition(sLoadAnyState, sLoadGotoInStart, EnumMacDrawerTransition.LoadAnyState_LoadGotoInStart);
            MacTransition tLoadGotoInStart_LoadGotoInIng = NewTransition(sLoadGotoInStart, sLoadGotoInIng, EnumMacDrawerTransition.LoadGotoInStart_LoadGotoInIng);
            MacTransition tLoadGotoInIng_LoadGotoInComplete = NewTransition(sLoadGotoInIng, sLoadGotoInComplete, EnumMacDrawerTransition.LoadGotoInIng_LoadGotoInComplete);


            // Load-Prework2(放入 Box)
            MacTransition tLoadGotoInComplete_IdleReadyForLoadBoxAtIn = NewTransition(sLoadGotoInComplete, sIdleReadyForLoadBoxAtIn, EnumMacDrawerTransition.LoadGotoInComplete_IdleReadyForLoadBoxAtIn);
            MacTransition tIdleReadyForLoadBoxAtIn_LoadBoxAtInComplete = NewTransition(sIdleReadyForLoadBoxAtIn, sLoadBoxAtInComplete, EnumMacDrawerTransition.IdleReadyForLoadBoxAtIn_LoadBoxAtInComplete);

            
            // Load01 (將 Tray 從 In 移到 Home) 
            MacTransition tLoadBoxAtInComplete_LoadGotoHomeStart = NewTransition(sLoadBoxAtInComplete, sLoadGotoHomeStart, EnumMacDrawerTransition.LoadBoxAtInComplete_LoadGotoHomeStart);
            MacTransition tLoaGotoHomeStart_LoadGotoHomeIng= NewTransition(sLoadGotoHomeStart, sLoadGotoHomeIng, EnumMacDrawerTransition.LoadGotoHomeStart_LoadGotoHomeIng);
            MacTransition tLoadGotoHomeIng_LoadGotoHomeComplete = NewTransition(sLoadGotoHomeIng, sLoadGotoHomeComplete, EnumMacDrawerTransition.LoadGotoHomeIng_LoadGotoHomeComplete);
            // Load02 (將 Tray 從 Home 移到 Out)
            MacTransition tLoadGotoHomeComplete_LoadGotoOutStart = NewTransition(sLoadGotoHomeIng, sLoadGotoHomeComplete, EnumMacDrawerTransition.LoadGotoHomeComplete_LoadGotoOutStart);
            MacTransition tLoadGotoOutStart_LoadGotoOutIng= NewTransition(sLoadGotoHomeIng, sLoadGotoHomeComplete, EnumMacDrawerTransition.LoadGotoOutStart_LoadGotoOutIng);
            MacTransition tLoadGotoOutIng_LoadGotoOutComplete = NewTransition(sLoadGotoHomeIng, sLoadGotoHomeComplete, EnumMacDrawerTransition.LoadGotoOutIng_LoadGotoOutComplete);

            // Load (將 Box 移開)
            MacTransition tLoadGotoOutComplete_IdleReadyForGetBox = NewTransition(sLoadGotoHomeComplete,sIdleReadyForGetBox, EnumMacDrawerTransition.LoadGotoOutComplete_IdleReadyForGetBox);
            MacTransition tIdleReadyForGetBox_LoadBoxGetAtOut = NewTransition(sIdleReadyForGetBox, sLoadBoxGetAtOut, EnumMacDrawerTransition.IdleReadyForGetBox_LoadBoxGetAtOut);


            // Unload Prework 01(將 Tray 移到 Out 位置)
            MacTransition tUnloadAnyState_UnloadGotoOutStart = NewTransition(sUnloadAnyState, sUnloadGotoOutStart, EnumMacDrawerTransition.UnloadAnyState_UnloadGotoOutStart);
            MacTransition tUnloadGotoOutStart_UnloadGotoOutIng = NewTransition(sUnloadGotoOutStart, sUnloadGotoOutIng, EnumMacDrawerTransition.UnloadGotoOutStart_UnloadGotoOutIng);
            MacTransition tUnloadGotoOutIng_UnloadGotoOutComplete = NewTransition(sUnloadGotoOutIng, sUnloadGotoOutComplete, EnumMacDrawerTransition.UnloadGotoOutIng_UnloadGotoOutComplete);

            // Unload Prework 02(放入 Box)
            MacTransition tUnloadGotoOutComplete_IdleReadyForUnloadBoxAtOut = NewTransition(sUnloadGotoOutComplete, sIdleReadyForUnloadBoxAtOut, EnumMacDrawerTransition.UnloadGotoOutComplete_IdleReadyForUnloadBoxAtOut);
            MacTransition tIdleReadyForUnloadBoxAtOut_UnloadBoxAtOutComplete = NewTransition(sIdleReadyForUnloadBoxAtOut, sUnloadBoxAtOutComplete, EnumMacDrawerTransition.IdleReadyForUnloadBoxAtOut_UnloadBoxAtOutComplete);

            // UnLoad01(將Tray 移到 Home) 
            MacTransition tUnloadBoxAtOutComplete_UnloadGotoHomeStart= NewTransition(sUnloadBoxAtOutComplete, sUnloadGotoHomeStart, EnumMacDrawerTransition.UnloadBoxAtOutComplete_UnloadGotoHomeStart);
            MacTransition tUnloadGotoHomeStart_UnloadGotoHomeIng = NewTransition(sUnloadGotoHomeStart, sUnloadGotoHomeIng, EnumMacDrawerTransition.UnloadGotoHomeStart_UnloadGotoHomeIng);
            MacTransition tUnloadGotoHomeIng_UnloadGotoHomeComplete = NewTransition(sUnloadGotoHomeIng, sUnloadGotoHomeComplete, EnumMacDrawerTransition.UnloadGotoHomeIng_UnloadGotoHomeComplete);

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
            var thisState= (MacState)sender;
            Action guard = () =>
            {
                var startTime = DateTime.Now;
                while (true)
                {
                    var isGuard = (HalDrawer.CurrentWorkState == DrawerWorkState.ReadyToInitial);
                    if (isGuard)
                    {// Drawer 目前為
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if(timeoutObj.IsTimeOut(startTime))
                    {
                        // TODO: Throw 逾時
                    }
                    Thread.Sleep(10);
                }
            };
            var t=new Task(guard);
            t.Start();
            HalDrawer.SetDrawerWorkState(DrawerWorkState.ReadyToInitial);
        }
        void sWaitInitial_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            var startTime = DateTime.Now;
            Action guard = () =>
            {
                var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.InitialStart;
                while (true)
                {
                    if (isGuard)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if(timeoutObj.IsTimeOut(startTime))
                    {
                        // TODO: to Throw a Time out Exception; 
                    }
                    Thread.Sleep(10);
                }
                
            };
            new Task(guard).Start();
            HalDrawer.CommandINI();
        }
        void sInitialStart_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            var startTime = DateTime.Now;
            Action guard = () =>
            {
                var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.InitialIng;
                while (true)
                {
                    if (isGuard)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(startTime)) 
                    {
                        // TODO: to Throw a Time Out Exception
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }
        void sInitialIng_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            var startTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome;
                    if (isGuard)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(startTime))
                    {
                        // TODO: to Throw a Time Out  Exception
                    }
                    Thread.Sleep(10);
                }
                
            };
            new Task(guard).Start();
        }
        void sInitialComplete_OnEntry(object sender, MacStateEntryEventArgs e)
        {
          //Final State of Initial
        }


        void sLoadAnyState_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime startTime = DateTime.Now;
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
                    if (timeoutObj.IsTimeOut(startTime))
                    {
                        // TODO: to throw a time out Exception
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
            DateTime thisTime = DateTime.Now;
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
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
            HalDrawer.CommandTrayMotionHome();
        }
        void sLoadGotoInIng_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.TrayArraiveAtIn;
                    if (isGuard)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
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
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToHomeIng;
                    if (isGuard)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
            HalDrawer.CommandTrayMotionHome();
        }
        void sLoadGotoHomeIng_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome;
                    if (isGuard)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }
        void sLoadGotoHomeComplete_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToOutStart;
                    if (isGuard)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
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
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToOutIng;
                    if (isGuard)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
            //HalDrawer.CommandTrayMotionOut();
        }
        void sLoadGotoOutIng_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtOut;
                    if (isGuard)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
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
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToOutStart;
                    if (isGuard)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
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
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToOutIng;
                    if (isGuard)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
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
                    var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtOut;
                    if (isGuard)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();

        }
        void sUnloadGotoOutComplete_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            // Final State of Unload Prework1 
        }

        void sIdleReadyForUnloadBoxAtOut_OnEntry(object sender, MacStateEntryEventArgs e) { }
        void sUnloadBoxAtOutComplete_OnEntry(object sender, MacStateEntryEventArgs e) { }

        void sUnloadGotoHomeStart_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToHomeIng;
                    if (isGuard)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
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
                    var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome;
                    if (isGuard)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
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
                    var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToInStart;
                    if (isGuard)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
            HalDrawer.CommandTrayMotionIn();
        }
        void sUnloadGotoInStart_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
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
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
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
                    var isGuard = HalDrawer.CurrentWorkState == DrawerWorkState.TrayArraiveAtIn;
                    if (isGuard)
                    {
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }
        void sUnloadGotoInComplete_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            // final State of Unload MAin
        }
      //  void sUnloadComplete_OnEntry(object sender, MacStateEntryEventArgs e) { }
        void sIdleReadyForUnloadBoxAtIn_OnEntry(object sender, MacStateEntryEventArgs e) { }
        void sUnloadBoxAtInComplete_OnEntry(object sender, MacStateEntryEventArgs e) { }

        void sExpInitialFail_OnEntry(object sender, MacStateEntryEventArgs e) { }
        void sExpInitialTimeout_OnEntry(object sender, MacStateEntryEventArgs e) { }
        void sExpGotoInTimeout_OnEntry(object sender, MacStateEntryEventArgs e) { }
        void sExpGotoInFail_OnEntry(object sender, MacStateEntryEventArgs e) { }
        void sExpGotoHomeTimeout_OnEntry(object sender, MacStateEntryEventArgs e) { }
        void sExpGotoHomeFail_OnEntry(object sender, MacStateEntryEventArgs e) { }
        void sExpGotoOutTimeout_OnEntry(object sender, MacStateEntryEventArgs e) { }
        void sExpGotoOutFail_OnEntry(object sender, MacStateEntryEventArgs e) { }

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
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacDrawerTransition.Initialing_InitialComplete.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sInitialComplete_OnExit(object sender, MacStateExitEventArgs e)
        {
            // Final State of Initial
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
            var thisTransition = this.Transitions[EnumMacDrawerTransition.LoadGotoInIng_LoadGotoInComplete.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sLoadGotoInComplete_OnExit(object sender, MacStateExitEventArgs e)
        {
            // Final state of Load Prework1
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
            var thisTransition = this.Transitions[EnumMacDrawerTransition.LoadGotoHomeIng_LoadGotoHomeComplete.ToString()];
            var nextState = thisTransition.StateTo;
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
            var thisTransition = this.Transitions[EnumMacDrawerTransition.LoadGotoOutIng_LoadGotoOutComplete.ToString()];
            var nextState = thisTransition.StateTo;
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
            var thisTransition = this.Transitions[EnumMacDrawerTransition.UnloadGotoOutIng_UnloadGotoOutComplete.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sUnloadGotoOutComplete_OnExit(object sender, MacStateExitEventArgs e)
        {
            // Final State of unload Preload2 
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
            var thisTransition = this.Transitions[EnumMacDrawerTransition.UnloadGotoHomeIng_UnloadGotoHomeComplete.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sUnloadGotoHomeComplete_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisTransition = this.Transitions[EnumMacDrawerTransition.UnloadGotoHomeComplete_UnloadGotoInStart.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sUnloadGotoInStart_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisTransition = this.Transitions[EnumMacDrawerTransition.UnloadGotoInStart_UnloadGotoInIng.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sUnloadGotoInIng_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisTransition = this.Transitions[EnumMacDrawerTransition.UnloadGotoInIng_UnloadGotoInComplete.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        void sUnloadGotoInComplete_OnExit(object sender, MacStateExitEventArgs e)
        {
           // final state of Unload Main 
        }
        //void sUnloadComplete_OnExit(object sender, MacStateExitEventArgs e) { }
        void sIdleReadyForUnloadBoxAtIn_OnExit(object sender, MacStateExitEventArgs e) { }
        void sUnloadBoxAtInComplete_OnExit(object sender, MacStateExitEventArgs e) { }


        void sExpInitialFail_OnExit(object sender, MacStateExitEventArgs e) { }
        void sExpInitialTimeout_OnExit(object sender, MacStateExitEventArgs e) { }
        void sExpGotoInTimeout_OnExit(object sender, MacStateExitEventArgs e) { }
        void sExpGotoInFail_OnExit(object sender, MacStateExitEventArgs e) { }
        void sExpGotoHomeTimeout_OnExit(object sender, MacStateExitEventArgs e) { }
        void sExpGotoHomeFail_OnExit(object sender, MacStateExitEventArgs e) { }
        void sExpGotoOutTimeout_OnExit(object sender, MacStateExitEventArgs e) { }
        void sExpGotoOutFail_OnExit(object sender, MacStateExitEventArgs e) { }

        #endregion delegates of Event OnEntry
    }
    public class TimeOutController
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
