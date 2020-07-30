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
            this.States[EnumMacDrawerState.InitialStart.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }

        public void Load_TrayGotoIn()
        {
            this.States[EnumMacDrawerState.LoadGotoInStart.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }

        public void Load_PutBox()
        {

        }


        public void Load_TrayGotoOut()
        {
            this.States[EnumMacDrawerState.LoadGotoHomeStart.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }

        public void Load_GetBox()
        {

        }

        public void Unload_TrayGotoOut()
        {
            this.States[EnumMacDrawerState.UnloadGotoOutStart.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }
        public void Unload_PutBox()
        {
        }

        public void Unload_TrayGotoIn()
        {
            this.States[EnumMacDrawerState.UnloadGotoHomeStart.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }

        public void UnLoadPostWork()
        {

        }

        #endregion Temp
        public override void LoadStateMachine()
        {
            #region State
            // Initial
            MacState sInitialStart = NewState(EnumMacDrawerState.InitialStart);
            MacState sInitialIng = NewState(EnumMacDrawerState.InitialIng);
            MacState sInitialComplete = NewState(EnumMacDrawerState.InitialComplete);
            MacState sInitialFail = NewState(EnumMacDrawerState.InitialFail);
            MacState sInitialTimeOut = NewState(EnumMacDrawerState.InitialTimeout);


            // Load, move tray to In from anywhere
            MacState sLoadGotoInStart = NewState(EnumMacDrawerState.LoadGotoInStart);
            MacState sLoadGotoInIng = NewState(EnumMacDrawerState.LoadGotoInIng);
            MacState sLoadGotoInComplete = NewState(EnumMacDrawerState.LoadGotoInComplete);
            MacState sLoadGotoInTimeOut = NewState(EnumMacDrawerState.LoadGotoInTimeOut);
            MacState sLoadGotoInFail = NewState(EnumMacDrawerState.LoadGotoInFail);


            // Load, put box on tray 
            MacState sIdleReadyForLoadBoxAtIn = NewState(EnumMacDrawerState.IdleReadyForLoadBoxAtIn);
            MacState sLoadBoxAtInComplete = NewState(EnumMacDrawerState.LoadBoxAtInComplete);

            // Load, move tray to Home from In 
            MacState sLoadGotoHomeStart = NewState(EnumMacDrawerState.LoadGotoHomeStart);
            MacState sLoadGotoHomeIng = NewState(EnumMacDrawerState.LoadGotoHomeIng);
            MacState sLoadGotoHomeComplete = NewState(EnumMacDrawerState.LoadGotoHomeComplete);
            MacState sLoadGotoHomeTimeOut = NewState(EnumMacDrawerState.LoadGotoHomeTimeOut);
            MacState sLoadGotoHomeFail = NewState(EnumMacDrawerState.LoadGotoHomeFail);

            // Load, Move tray to Out from Home
            MacState sLoadGotoOutStart = NewState(EnumMacDrawerState.LoadGotoOutStart);
            MacState sLoadGotoOutIng = NewState(EnumMacDrawerState.LoadGotoOutIng);
            MacState sLoadGotoOutComplete = NewState(EnumMacDrawerState.LoadGotoOutComplete);
            MacState sLoadGotoOutTimeOut = NewState(EnumMacDrawerState.LoadGotoOutTimeOut);
            MacState sLoadGotoOutFail = NewState(EnumMacDrawerState.LoadGotoOutFail);

            // Load, Get box at Out
            MacState sIdleReadyForGetBox = NewState(EnumMacDrawerState.IdleReadyForGet);
            MacState sLoadBoxGetAtOut = NewState(EnumMacDrawerState.LoadBoxGetAtOut);

            // Unload
            // Move tray to out from anywhere
            MacState sUnloadGotoOutStart = NewState(EnumMacDrawerState.UnloadGotoOutStart);
            MacState sUnloadGotoOutIng = NewState(EnumMacDrawerState.UnloadGotoOutIng);
            MacState sUnloadGotoOutComplete = NewState(EnumMacDrawerState.UnloadGotoOutComplete);
            MacState sUnloadGotoOutFail= NewState(EnumMacDrawerState.UnloadGotoOutFail);
            MacState sUnloadGotoOutTimeOut = NewState(EnumMacDrawerState.UnloadGotoOutTimeOut);

            // put box on tray at out
            MacState sIdleReadyForUnloadBoxAtOut = NewState(EnumMacDrawerState.IdleReadyForUnloadBoxAtOut);
            MacState sUnloadBoxAtOutComplete = NewState(EnumMacDrawerState.UnloadBoxPutAtOut);

            // move tray to Home from Out
            MacState sUnloadGotoHomeStart = NewState(EnumMacDrawerState.UnloadGotoHomeStart);
            MacState sUnloadGotoHomeIng = NewState(EnumMacDrawerState.UnloadGotoHomeIng);
            MacState sUnloadGotoHomeComplete = NewState(EnumMacDrawerState.UnloadGotoHomeComplete);
            MacState sUnloadGotoHomeFail = NewState(EnumMacDrawerState.UnloadGotoHomeFail);
            MacState sUnloadGotoHomeTimeOut = NewState(EnumMacDrawerState.UnloadGotoHomeTimeOut);

            // move tray to In from Home
            MacState sUnloadGotoInStart = NewState(EnumMacDrawerState.UnloadGotoInStart);
            MacState sUnloadGotoInIng = NewState(EnumMacDrawerState.UnloadGotoInIng);
            MacState sUnloadGotoInComplete = NewState(EnumMacDrawerState.UnloadGotoInComplete);
            MacState sUnloadGotoInFail=NewState(EnumMacDrawerState.UnloadGotoInFail);
            MacState sUnloadGotoInTimeOut = NewState(EnumMacDrawerState.UnloadGotoInTimeOut);
            

            // get box at In
            MacState sIdleReadyForUnloadBoxAtIn = NewState(EnumMacDrawerState.IdleReadyForUnloadBoxAtIn);
            MacState sUnloadBoxAtInComplete = NewState(EnumMacDrawerState.UnloadBoxAtInComplete);

            #endregion State

            #region Transition

            // Initial,

            MacTransition tInitialStart_InitialIng = NewTransition(sInitialStart, sInitialIng, EnumMacDrawerTransition.InitialStart_InitialIng);
            MacTransition tInitialing_InitialComplete = NewTransition(sInitialIng, sInitialComplete, EnumMacDrawerTransition.Initialing_InitialComplete);
            MacTransition tInitialing_InitialFail = NewTransition(sInitialIng, sInitialFail, EnumMacDrawerTransition.Initialing_InitialFail);
            MacTransition tInitialing_InitialTimeOut = NewTransition(sInitialIng, sInitialTimeOut, EnumMacDrawerTransition.Initialing_InitialTimeOut);

            // Load(將 Tray 移到 定位~ Home的位置 )
            MacTransition tLoadGotoInStart_LoadGotoInIng = NewTransition(sLoadGotoInStart, sLoadGotoInIng, EnumMacDrawerTransition.LoadGotoInStart_LoadGotoInIng);
            MacTransition tLoadGotoInIng_LoadGotoInComplete = NewTransition(sLoadGotoInIng, sLoadGotoInComplete, EnumMacDrawerTransition.LoadGotoInIng_LoadGotoInComplete);
            MacTransition tLoadGotoIn_LoadGotoInTimeOut = NewTransition(sLoadGotoInIng, sLoadGotoInTimeOut, EnumMacDrawerTransition.LoadGotoInIng_LoadGotoInTimeOut);
            MacTransition tLoadGotoIn_LoadGotoInFail = NewTransition(sLoadGotoInIng, sLoadGotoInFail, EnumMacDrawerTransition.LoadGotoInIng_LoadGotoInFail);


            // Load(放入 Box)
            MacTransition tLoadGotoInComplete_IdleReadyForLoadBoxAtIn = NewTransition(sLoadGotoInComplete, sIdleReadyForLoadBoxAtIn, EnumMacDrawerTransition.LoadGotoInComplete_IdleReadyForLoadBoxAtIn);
            MacTransition tIdleReadyForLoadBoxAtIn_LoadBoxAtInComplete = NewTransition(sIdleReadyForLoadBoxAtIn, sLoadBoxAtInComplete, EnumMacDrawerTransition.IdleReadyForLoadBoxAtIn_LoadBoxAtInComplete);


            // Load (將 Tray 從 In 移到 Home) 
            MacTransition tLoadBoxAtInComplete_LoadGotoHomeStart = NewTransition(sLoadBoxAtInComplete, sLoadGotoHomeStart, EnumMacDrawerTransition.LoadBoxAtInComplete_LoadGotoHomeStart);
            MacTransition tLoaGotoHomeStart_LoadGotoHomeIng = NewTransition(sLoadGotoHomeStart, sLoadGotoHomeIng, EnumMacDrawerTransition.LoadGotoHomeStart_LoadGotoHomeIng);
            MacTransition tLoadGotoHomeIng_LoadGotoHomeComplete = NewTransition(sLoadGotoHomeIng, sLoadGotoHomeComplete, EnumMacDrawerTransition.LoadGotoHomeIng_LoadGotoHomeComplete);
            MacTransition tLoadGotoHome_LoadGotoToHomeTimeOut = NewTransition(sLoadGotoHomeIng, sLoadGotoHomeTimeOut, EnumMacDrawerTransition.LoadGotoHomeIng_LoadGotoHomeTimeOut);
            MacTransition tLoadGotoHome_LoadGotoToHomeFail = NewTransition(sLoadGotoHomeIng, sLoadGotoHomeTimeOut, EnumMacDrawerTransition.LoadGotoHomeIng_LoadGotoHomeFail);
            // Load (將 Tray 從 Home 移到 Out)
            MacTransition tLoadGotoHomeComplete_LoadGotoOutStart = NewTransition(sLoadGotoHomeIng, sLoadGotoOutStart, EnumMacDrawerTransition.LoadGotoHomeComplete_LoadGotoOutStart);
            MacTransition tLoadGotoOutStart_LoadGotoOutIng = NewTransition(sLoadGotoOutStart, sLoadGotoOutIng, EnumMacDrawerTransition.LoadGotoOutStart_LoadGotoOutIng);
            MacTransition tLoadGotoOutIng_LoadGotoOutComplete = NewTransition(sLoadGotoOutIng, sLoadGotoOutComplete, EnumMacDrawerTransition.LoadGotoOutIng_LoadGotoOutComplete);
            MacTransition tLoadGotoOut_LoadGotoOutTimeOut = NewTransition(sLoadGotoOutIng, sLoadGotoOutTimeOut, EnumMacDrawerTransition.LoadGotoOutIng_LoadGotoOutTimeOut);
            MacTransition tLoadGotoOut_LoadGotoOutFail = NewTransition(sLoadGotoOutIng, sLoadGotoOutFail, EnumMacDrawerTransition.LoadGotoOutIng_LoadGotoOutFail);
            // Load (將 Box 移開)
            MacTransition tLoadGotoOutComplete_IdleReadyForGetBox = NewTransition(sLoadGotoHomeComplete, sIdleReadyForGetBox, EnumMacDrawerTransition.LoadGotoOutComplete_IdleReadyForGetBox);
            MacTransition tIdleReadyForGetBox_LoadBoxGetAtOut = NewTransition(sIdleReadyForGetBox, sLoadBoxGetAtOut, EnumMacDrawerTransition.IdleReadyForGetBox_LoadBoxGetAtOut);


            // Unload (將 Tray 移到 Out 位置)
            MacTransition tUnloadGotoOutStart_UnloadGotoOutIng = NewTransition(sUnloadGotoOutStart, sUnloadGotoOutIng, EnumMacDrawerTransition.UnloadGotoOutStart_UnloadGotoOutIng);
            MacTransition tUnloadGotoOutIng_UnloadGotoOutComplete = NewTransition(sUnloadGotoOutIng, sUnloadGotoOutComplete, EnumMacDrawerTransition.UnloadGotoOutIng_UnloadGotoOutComplete);
            MacTransition tUnloadPrework1_UnloadPrework1Fail = NewTransition(sUnloadGotoOutIng, sUnloadGotoOutFail, EnumMacDrawerTransition.UnloadGotoOutIng_UnloadGotoOutFail);
            MacTransition tUnloadPrework1_UnloadPrework1TimeOut = NewTransition(sUnloadGotoOutIng, sUnloadGotoOutTimeOut, EnumMacDrawerTransition.UnloadGotoOutIng_UnloadGotoOutTimeOut);

            // Unload (放入 Box)
            MacTransition tUnloadGotoOutComplete_IdleReadyForUnloadBoxAtOut = NewTransition(sUnloadGotoOutComplete, sIdleReadyForUnloadBoxAtOut, EnumMacDrawerTransition.UnloadGotoOutComplete_IdleReadyForUnloadBoxAtOut);
            MacTransition tIdleReadyForUnloadBoxAtOut_UnloadBoxAtOutComplete = NewTransition(sIdleReadyForUnloadBoxAtOut, sUnloadBoxAtOutComplete, EnumMacDrawerTransition.IdleReadyForUnloadBoxAtOut_UnloadBoxAtOutComplete);

            // UnLoad(將Tray 移到 Home) 
            MacTransition tUnloadBoxAtOutComplete_UnloadGotoHomeStart = NewTransition(sUnloadBoxAtOutComplete, sUnloadGotoHomeStart, EnumMacDrawerTransition.UnloadBoxAtOutComplete_UnloadGotoHomeStart);
            MacTransition tUnloadGotoHomeStart_UnloadGotoHomeIng = NewTransition(sUnloadGotoHomeStart, sUnloadGotoHomeIng, EnumMacDrawerTransition.UnloadGotoHomeStart_UnloadGotoHomeIng);
            MacTransition tUnloadGotoHomeIng_UnloadGotoHomeComplete = NewTransition(sUnloadGotoHomeIng, sUnloadGotoHomeComplete, EnumMacDrawerTransition.UnloadGotoHomeIng_UnloadGotoHomeComplete);
            MacTransition tUnloadGotoHome_UnloadGotoHomeTimeOut = NewTransition(sUnloadGotoHomeIng, sUnloadGotoHomeTimeOut, EnumMacDrawerTransition.UnloadGotoHomeIng_UnloadGotoHomeTimeOut);
            MacTransition tUnloadGotoHome_UnloadGotoHomeFail = NewTransition(sUnloadGotoHomeIng, sUnloadGotoHomeFail, EnumMacDrawerTransition.UnloadGotoHomeIng_UnloadGotoHomeFail);

            // Unload(將 Tray 移到 In)
            MacTransition tUnloadGotoHomeComplete_UnloadGotoInStart = NewTransition(sUnloadGotoHomeComplete, sUnloadGotoInStart, EnumMacDrawerTransition.UnloadGotoHomeComplete_UnloadGotoInStart);
            MacTransition tUnloadGotoInStart_UnloadGotoInIng = NewTransition(sUnloadGotoInStart, sUnloadGotoInIng, EnumMacDrawerTransition.UnloadGotoInStart_UnloadGotoInIng);
            MacTransition tUnloadGotoInIng_UnloadGotoInComplete = NewTransition(sUnloadGotoHomeIng, sUnloadGotoHomeComplete, EnumMacDrawerTransition.UnloadGotoInIng_UnloadGotoInComplete);

            // Unload PostWork(取走 Box)
            MacTransition tUnloadGotoHomeComplete_IdleReadyForUnloadBoxAtIn = NewTransition(sUnloadGotoHomeComplete, sIdleReadyForUnloadBoxAtIn, EnumMacDrawerTransition.UnloadGotoHomeComplete_IdleReadyForUnloadBoxAtIn);
            #endregion


            #region  Event

            sInitialStart.OnEntry+= ( sender,  e)=>
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
                HalDrawer.CommandINI();
             };
            sInitialStart.OnExit += (sender, e) =>
            {
                var thisState = (MacState)sender;
                var thisTransition = this.Transitions[EnumMacDrawerTransition.InitialStart_InitialIng.ToString()];
                var nextState = thisTransition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sInitialIng.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                var startTime = DateTime.Now;
                var dicTransition = this.Transitions;
                Action guard = () =>
                {
                    while (true)
                    {
                        MacTransition transition = null;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome)
                        {  // Initial  Complete
                            transition = dicTransition[EnumMacDrawerTransition.Initialing_InitialComplete.ToString()];
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.InitialFailed)
                        {  // Initial Failed
                            transition = dicTransition[EnumMacDrawerTransition.Initialing_InitialFail.ToString()];
                        }
                        else if (timeoutObj.IsTimeOut(startTime))
                        {  // 逾時
                            transition = dicTransition[EnumMacDrawerTransition.Initialing_InitialTimeOut.ToString()];
                        }
                        if (transition != null)
                        {
                            var eventArgs = new MacStateExitWithTransitionEventArgs(transition);
                            state.DoExit(eventArgs);
                            break;
                        }
                        Thread.Sleep(10);
                    }

                };
                new Task(guard).Start();
            };
            sInitialIng.OnExit += (sender, e) =>
            {
                var args = (MacStateExitWithTransitionEventArgs)e;
                var nextState = args.Transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sInitialComplete.OnEntry += (sender, e)=>
            {
                var state = (MacState)sender;
                // TODO: 依實際狀況加 Code
                // 
                state.DoExit(new MacStateExitEventArgs());
            };
            sInitialComplete.OnExit += (sender, e) =>
            {
                var state = (MacState)sender;
                // TODO: 依實際狀況補上Code
                //
            };

            sInitialTimeOut.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                // TODO:  補上實作
                //
                state.DoExit(new MacStateExitEventArgs());
            };
            sInitialTimeOut.OnExit += (sender, e) =>
            {
                var thisState = (MacState)sender;
                // TODO: 看實務上如何處理 Initial Timeout再補上程式碼
                //
            };

            sInitialFail.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                // TODO: 補上實作
                //
                state.DoExit(new MacStateExitEventArgs());
            };
            sInitialFail.OnExit += (sender, e) =>
            {
                var state = (MacState)sender;
                // TODO: 看實務上如何處理 Initial Fail再補上程式碼
                //
            };

            sLoadGotoInStart.OnEntry += (sender, e) =>
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
                HalDrawer.CommandTrayMotionIn();
            };
            sLoadGotoInStart.OnExit += (sender, e) =>
            {
                var thisTransition = this.Transitions[EnumMacDrawerTransition.LoadGotoInStart_LoadGotoInIng.ToString()];
                var nextState = thisTransition.StateTo;
                 nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sLoadGotoInIng.OnEntry += (sender, e)=>
            {
                var state = (MacState)sender;
                DateTime thisTime = DateTime.Now;
                var dicTransition = this.Transitions;
                Action guard = () =>
                {
                    while (true)
                    {
                        MacTransition transition = null;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArraiveAtIn)
                        {// Complete
                            transition = dicTransition[EnumMacDrawerTransition.LoadGotoInIng_LoadGotoInComplete.ToString()];
                       }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {   // Failed
                          transition = dicTransition[EnumMacDrawerTransition.LoadGotoInIng_LoadGotoInFail.ToString()];
                        }
                        else if (timeoutObj.IsTimeOut(thisTime))
                        {    // Time Out
                            transition = this.Transitions[EnumMacDrawerTransition.LoadGotoInIng_LoadGotoInTimeOut.ToString()];

                        }
                        if (transition != null)
                        {
                            var eventArgs = new MacStateExitWithTransitionEventArgs(transition);
                            state.DoExit(eventArgs);
                            break;
                        }

                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sLoadGotoInIng.OnExit += (sender, e)=>
            {
                var args = (MacStateExitWithTransitionEventArgs)e;
                var nextState = args.Transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sLoadGotoInComplete.OnEntry += (sender, e) =>
            {
                // Final State of Load for moving tray to In 
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sLoadGotoInComplete.OnExit += (sender, e) =>
            {
                // Final state of Load Prework1
                // TODO: 配合實務操作再補上 Code
            };

            sLoadGotoInFail.OnEntry += (sender, e) =>
            {
                // Load 預先工作(將 Tray 移到 In 的位置), 無法完成工作
                var state = (MacState)sender;
                // TODO: 補上實作
                state.DoExit(new MacStateExitEventArgs());
            };
            sLoadGotoInFail.OnExit += (sender, e) =>
            {
                // Load 的預先工作, 將 Tray 移到 In 的位置, 移動失敗
                // TODO: 配合操作再補上 Code 
            };

            sLoadGotoInTimeOut.OnEntry += (sender, e) =>
            {
                // Load 預先工作(將 Tray 移到 In 的位置) ,工作逾時
                var state = (MacState)sender;
                // TODO: 補上實作 
                state.DoExit(new MacStateExitEventArgs());
            };
            sLoadGotoInTimeOut.OnExit += (sender, e) =>
            {
                // Load 的預先工作, 將 Tray 移到 In 的位置, 逾時未到
                var state = (MacState)sender;
                // TODO: 補上實作 
                state.DoExit(new MacStateExitEventArgs());
            };

          
            sIdleReadyForLoadBoxAtIn.OnEntry += (sender, e) =>
            {

            };
            sIdleReadyForLoadBoxAtIn.OnExit += (sender, e) =>
            {

            };

            sLoadBoxAtInComplete.OnEntry += (sender, e) =>
            {

            };
            sLoadBoxAtInComplete.OnExit += (sender, e) =>
            {

            };

            sLoadGotoHomeStart.OnEntry += (sender,e) =>
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
            };
            sLoadGotoHomeStart.OnExit += (sender, e) =>
            {
                var thisTransition = this.Transitions[EnumMacDrawerTransition.LoadGotoHomeStart_LoadGotoHomeIng.ToString()];
                var nextState = thisTransition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sLoadGotoHomeIng.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                DateTime thisTime = DateTime.Now;
                var dicTransition = this.Transitions;
                Action guard = () =>
                {
                    while (true)
                    {
                        MacTransition transition=null;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome)
                        {  // Complete
                            transition = dicTransition[EnumMacDrawerTransition.LoadGotoHomeIng_LoadGotoHomeComplete.ToString()];
                        }
                        else if (timeoutObj.IsTimeOut(thisTime))
                        {  // 逾時
                            transition = dicTransition[EnumMacDrawerTransition.LoadGotoHomeIng_LoadGotoHomeTimeOut.ToString()];
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            transition = dicTransition[EnumMacDrawerTransition.LoadGotoHomeIng_LoadGotoHomeFail.ToString()];
                        }
                        if (transition != null)
                        {
                            var eventArgs = new MacStateExitWithTransitionEventArgs(transition);
                            state.DoExit(eventArgs);
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sLoadGotoHomeIng.OnExit += (sender, e) =>
              {
                  var args = (MacStateExitWithTransitionEventArgs)e;
                  var nextState = args.Transition.StateTo;
                  nextState.DoEntry(new MacStateEntryEventArgs(args));
              };
            
            sLoadGotoHomeComplete.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToOutStart)
                        {
                            state.DoExit(new MacStateExitEventArgs());
                            break;
                        }

                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
                HalDrawer.CommandTrayMotionOut();
            };
            sLoadGotoHomeComplete.OnExit += (sender, e) =>
            {
                var transition = this.Transitions[EnumMacDrawerTransition.LoadGotoHomeComplete_LoadGotoOutStart.ToString()];
                var nextState = transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sLoadGotoHomeTimeOut.OnEntry += (sender, e)=>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sLoadGotoHomeTimeOut.OnExit += (sender, e) =>
            {
                // Load 主要工作, 將 Tray 移到 Home 時逾時未到 
            };
            
            sLoadGotoHomeFail.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sLoadGotoHomeFail.OnExit += (sender, e) =>
            {
                //Load 主要工作, 將 Tray 移到 Home 時失敗
            };

            sLoadGotoOutStart.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToOutIng)
                        {
                            state.DoExit(new MacStateExitEventArgs());
                            break;
                        }

                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sLoadGotoOutStart.OnExit += (sender, e) =>
            {
                var transition = this.Transitions[EnumMacDrawerTransition.LoadGotoOutStart_LoadGotoOutIng.ToString()];
                var nextState = transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sLoadGotoOutIng.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                DateTime thisTime = DateTime.Now;
                var dicTransition = this.Transitions;
                Action guard = () =>
                {
                    while (true)
                    {
                        MacTransition transition = null;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtOut)
                        {   // 完成
                            transition = dicTransition[EnumMacDrawerTransition.LoadGotoOutIng_LoadGotoOutComplete.ToString()];
                        }
                        else if (timeoutObj.IsTimeOut(thisTime))
                        {   // 逾時
                            transition = dicTransition[EnumMacDrawerTransition.LoadGotoOutIng_LoadGotoOutTimeOut.ToString()];
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {   // 無法完成
                            transition = dicTransition[EnumMacDrawerTransition.LoadGotoOutIng_LoadGotoOutFail.ToString()];
                        }
                        if (transition != null)
                        {
                            var eventArgs = new MacStateExitWithTransitionEventArgs(transition);
                            state.DoExit(eventArgs);
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sLoadGotoOutIng.OnExit += (sender, e) =>
            {
                var args = (MacStateExitWithTransitionEventArgs)e;
                var nextState = args.Transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sLoadGotoOutComplete.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sLoadGotoOutComplete.OnExit += (sender, e) =>
            {
                // Final State Of Load 
            };

            sLoadGotoOutTimeOut.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sLoadGotoOutTimeOut.OnExit += (sender, e) =>
            {
                // TODO: Transition ?
            };

            sLoadGotoOutFail.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sLoadGotoOutFail.OnExit += (sender, e) =>
            {
                // TODO: Transition ?
            };

          
            sIdleReadyForGetBox.OnEntry +=(sender,e)=>
            {
            };
            sIdleReadyForGetBox.OnExit += (sender, e) =>
            {
            };

           sLoadBoxGetAtOut.OnEntry += (sender, e) =>
            {

            };
             sLoadBoxGetAtOut.OnExit += (sender, e) =>
            {

            };

            sUnloadGotoOutStart.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToOutIng)
                        {
                            state.DoExit(new MacStateExitEventArgs());
                            break;
                        }

                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
                HalDrawer.CommandTrayMotionOut();
            };
            sUnloadGotoOutStart.OnExit += (sender, e) =>
            {
                var transition = this.Transitions[EnumMacDrawerTransition.UnloadGotoOutStart_UnloadGotoOutIng.ToString()];
                var nextState = transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sUnloadGotoOutIng.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                DateTime thisTime = DateTime.Now;
                var dicTransition = this.Transitions;
                Action guard = () =>
                {
                    while (true)
                    {
                        MacTransition transition = null;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtOut)
                        {   // 完成
                            transition = dicTransition[EnumMacDrawerTransition.UnloadGotoOutIng_UnloadGotoOutComplete.ToString()];
                        }
                        else if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // 逾時
                            transition = dicTransition[EnumMacDrawerTransition.UnloadGotoOutIng_UnloadGotoOutTimeOut.ToString()];
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            // 執行不成功
                            transition = dicTransition[EnumMacDrawerTransition.UnloadGotoOutIng_UnloadGotoOutFail.ToString()];
                        }
                        if (transition != null)
                        {
                            var eventArgs = new MacStateExitWithTransitionEventArgs(transition);
                            state.DoExit(eventArgs);
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sUnloadGotoOutIng.OnExit += (sender, e) =>
            {
                var args = (MacStateExitWithTransitionEventArgs)e;
                var nextState = args.Transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sUnloadGotoOutComplete.OnEntry += (sender, e) =>
            {
                // Final State of Unload Prework1
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());

            };
            sUnloadGotoOutComplete.OnExit += (sender, e) =>
            {
                // Final State of unload Prework1
                // TODO: 依實際操作再加上程式碼
            };

            sUnloadGotoOutFail.OnEntry += (sender, e)=>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sUnloadGotoOutFail.OnExit += (sender, e) =>
            {
                // Unload 預置工作1 未完成
                // TODO: 按實際操作再補上程式碼
            };

            sUnloadGotoOutTimeOut.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sUnloadGotoOutTimeOut.OnExit += (sender, e) =>
            {
                // Unload 前置工作1 逾時
                // TODO: 按實際工作再補上程式碼
            };

            sIdleReadyForUnloadBoxAtOut.OnEntry += (sender, e) =>
            {

            };
            sIdleReadyForUnloadBoxAtOut.OnExit += (sender, e) =>
            {

            };

             sUnloadBoxAtOutComplete.OnEntry += (sender, e) =>
            {

            };
            sUnloadBoxAtOutComplete.OnExit += (sender, e) =>
            {

            };


            sUnloadGotoHomeStart.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToHomeIng)
                        {
                            state.DoExit(new MacStateExitEventArgs());
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
                HalDrawer.CommandTrayMotionHome();
            };
            sUnloadGotoHomeStart.OnExit += (sender, e) =>
            {
                var transition = this.Transitions[EnumMacDrawerTransition.UnloadGotoHomeStart_UnloadGotoHomeIng.ToString()];
                var nextState = transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sUnloadGotoHomeIng.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                DateTime thisTime = DateTime.Now;
                var dicTransition = this.Transitions;
                Action guard = () =>
                {
                    while (true)
                    {
                        MacTransition transition = null;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome)
                        {
                            transition = dicTransition[EnumMacDrawerTransition.UnloadGotoHomeIng_UnloadGotoHomeComplete.ToString()];
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            transition = dicTransition[EnumMacDrawerTransition.UnloadGotoHomeIng_UnloadGotoHomeFail.ToString()];
                        }
                        else if (timeoutObj.IsTimeOut(thisTime))
                        {
                            transition = dicTransition[EnumMacDrawerTransition.UnloadGotoHomeIng_UnloadGotoHomeTimeOut.ToString()];
                        }
                        if(transition != null)
                        {
                            var eventArgs = new MacStateExitWithTransitionEventArgs(transition);
                            state.DoExit(eventArgs);
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
             };
             sUnloadGotoHomeIng.OnExit += (sender, e) =>
             {
                var args = (MacStateExitWithTransitionEventArgs)e;
                var nextState = args.Transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
             };

            sUnloadGotoHomeComplete.OnEntry += (sender, e) =>
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
            };
            sUnloadGotoHomeComplete.OnExit += (sender, e) =>
            {
                var transition = this.Transitions[EnumMacDrawerTransition.UnloadGotoHomeComplete_UnloadGotoInStart.ToString()];
                var nextState = transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

           sUnloadGotoHomeFail.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                // Unload 從 out 走到 In 時失敗
                // TODO: 待實際動作確認之後再加上程式碼
                state.DoExit(new MacStateExitEventArgs());
            };
            sUnloadGotoHomeFail.OnExit += (sender, e) =>
            {
                // Unload 由Out 位置回到 Home 時移動失敗
                // TODO: 依實際動作再補上其他 Code
                // TODO: Transition ?
            };

            sUnloadGotoHomeTimeOut.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                // Unload 從 out 走到 In 時失敗
                // TODO: 待實際動作確認之後再加上程式碼
                state.DoExit(new MacStateExitEventArgs());
            };
            sUnloadGotoHomeTimeOut.OnExit += (sender, e) =>
            {
                // Unload 由Out 位置回到 Home 時逾時
                // TODO: 依實際動作再補上其他 Code
                // TODO: Transition ?
            };

           sUnloadGotoInStart.OnEntry += (sender, e) => 
            {
                var state = (MacState)sender;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMoveToInIng)
                        {
                            state.DoExit(new MacStateExitEventArgs());
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sUnloadGotoInStart.OnExit += (sender, e) =>
            {
                var transition = this.Transitions[EnumMacDrawerTransition.UnloadGotoInStart_UnloadGotoInIng.ToString()];
                var nextState = transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sUnloadGotoInIng.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                DateTime thisTime = DateTime.Now;
                var dicTransition = this.Transitions;
                Action guard = () =>
                {
                    while (true)
                    {
                        MacTransition transition = null;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArraiveAtIn)
                        {
                            transition = dicTransition[EnumMacDrawerTransition.UnloadGotoInIng_UnloadGotoInComplete.ToString()];
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            transition = dicTransition[EnumMacDrawerTransition.UnloadGotoInIng_UnloadGotoInFail.ToString()];
                        }
                        else if (timeoutObj.IsTimeOut(thisTime))
                        {
                            transition = dicTransition[EnumMacDrawerTransition.UnloadGotoInIng_UnloadGotoInTimeOut.ToString()];
                        }
                        if (transition != null)
                        {
                            var eventArgs = new MacStateExitWithTransitionEventArgs(transition);
                            state.DoExit(eventArgs);
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sUnloadGotoInIng.OnExit += (sender, e) =>
            {
                var args = (MacStateExitWithTransitionEventArgs)e;
                var nextState = args.Transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

           sUnloadGotoInComplete.OnEntry+=(sender,e)=>
            {
                // final State of Unload MAin
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sUnloadGotoInComplete.OnExit += (sender, e) =>
            {
                // final state of Unload Main 
                // TODO: Transition ?
            };

            sUnloadGotoInFail.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sUnloadGotoInFail.OnExit += (sender, e) =>
            {
                // TODO: Transition ?
            };

            sUnloadGotoInTimeOut.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sUnloadGotoInTimeOut.OnExit += (sender, e) =>
            {
                // TODO: Transition ?
            };

            sIdleReadyForUnloadBoxAtIn.OnEntry += (sender, e) =>
            {
            };
            sIdleReadyForUnloadBoxAtIn.OnExit += (sender, e) =>
            {

            };

            sUnloadBoxAtInComplete.OnEntry += (sender, e) =>
            {
            };
            sUnloadBoxAtInComplete.OnExit += (sender, e) =>
            {
            };


            #endregion   Register Event 
            

      



        }


     }
    public class MacDrawerStateTimeOutController
    {
        const int defTimeOutSec = 20;
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
            return IsTimeOut(startTime, defTimeOutSec);
        }
    }
}
