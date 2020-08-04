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

      


        public void Load_TrayGotoOut()
        {
            this.States[EnumMacDrawerState.LoadGotoHomeStart.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }

      

        public void Unload_TrayGotoOut()
        {
            this.States[EnumMacDrawerState.UnloadGotoOutStart.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }
        
        public void Unload_TrayGotoIn()
        {
            this.States[EnumMacDrawerState.UnloadGotoHomeStart.ToString()].DoEntry(new MacStateEntryEventArgs(null));
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
            // 等待放盒子
            MacState sIdleForPutBoxOnTrayAtIn = NewState(EnumMacDrawerState.IdleForPutBoxOnTrayAtIn);

            // Load, move tray to Home from In 
            MacState sLoadGotoHomeStart = NewState(EnumMacDrawerState.LoadGotoHomeStart);
            MacState sLoadGotoHomeIng = NewState(EnumMacDrawerState.LoadGotoHomeIng);
            MacState sLoadGotoHomeComplete = NewState(EnumMacDrawerState.LoadGotoHomeComplete);
        

            MacState sLoadGotoHomeTimeOut = NewState(EnumMacDrawerState.LoadGotoHomeTimeOut);
            MacState sLoadGotoHomeFail = NewState(EnumMacDrawerState.LoadGotoHomeFail);


            // Load, 在HOme 點檢查Tray 上是否有盒子
            MacState sLoadCheckBoxExistenceAtHome = NewState(EnumMacDrawerState.LoadCheckBoxExistenceAtHome);
            // Load, 在Home 點檢查 是否有盒子=> 有, 2020/08/03 King Liu Add New 
            MacState sLoadBoxExistAtHome = NewState(EnumMacDrawerState.LoadBoxExistAtHome);
            // Load, 在Home 點查 是否有 盒子=> 没有, 2020/08/03 King Liu Add New 
            MacState sLoadBoxNotExistAtHome = NewState(EnumMacDrawerState.LoadBoxNotExistAtHome);
            // Load, 在Home 點檢查是否有盒子時逾時,2020/08/03 King Liu Add New  
            MacState sLoadCheckBoxExistenceAtHomeTimeOut = NewState(EnumMacDrawerState.LoadCheckBoxExistenceAtHomeTimeOut);
          
            // Load, 在Home時被檢出没有盒子, 回退到 In 
            MacState sLoadNoBoxRejectToInFromHomeStart = NewState(EnumMacDrawerState.LoadNoBoxRejectToInFromHomeStart);
            MacState sLoadNoBoxRejectToInFromHomeIng = NewState(EnumMacDrawerState.LoadNoBoxRejectToInFromHomeIng);
            MacState sLoadNoBoxRejectToInFromHomeComplete = NewState(EnumMacDrawerState.LoadNoBoxRejectToInFromHomeComplete);
            MacState sLoadNoBoxRejectToInFromHomeFail = NewState(EnumMacDrawerState.LoadNoBoxRejectToInFromHomeFail);
            MacState sLoadNoBoxRejectToInFromHomeTimeOut = NewState(EnumMacDrawerState.LoadNoBoxRejectToInFromHomeTimeOut);

            // Load, Move tray to Out from Home
            MacState sLoadGotoOutStart = NewState(EnumMacDrawerState.LoadGotoOutStart);
            MacState sLoadGotoOutIng = NewState(EnumMacDrawerState.LoadGotoOutIng);
            MacState sLoadGotoOutComplete = NewState(EnumMacDrawerState.LoadGotoOutComplete);
            MacState sLoadGotoOutTimeOut = NewState(EnumMacDrawerState.LoadGotoOutTimeOut);
            MacState sLoadGotoOutFail = NewState(EnumMacDrawerState.LoadGotoOutFail);
            // 等待取走盒子
            MacState sIdleForGetBoxOnTrayAtOut = NewState(EnumMacDrawerState.IdleForGetBoxOnTrayAtOut);


            // Unload
            // Move tray to out from anywhere
            MacState sUnloadGotoOutStart = NewState(EnumMacDrawerState.UnloadGotoOutStart);
            MacState sUnloadGotoOutIng = NewState(EnumMacDrawerState.UnloadGotoOutIng);
            MacState sUnloadGotoOutComplete = NewState(EnumMacDrawerState.UnloadGotoOutComplete);
            MacState sUnloadGotoOutFail= NewState(EnumMacDrawerState.UnloadGotoOutFail);
            MacState sUnloadGotoOutTimeOut = NewState(EnumMacDrawerState.UnloadGotoOutTimeOut);
            // 等待盒子放進來
            MacState sIdleForPutBoxOnTrayAtOut = NewState(EnumMacDrawerState.IdleForPutBoxOnTrayAtOut);


            // move tray to Home from Out
            MacState sUnloadGotoHomeStart = NewState(EnumMacDrawerState.UnloadGotoHomeStart);
            MacState sUnloadGotoHomeIng = NewState(EnumMacDrawerState.UnloadGotoHomeIng);
            MacState sUnloadGotoHomeComplete = NewState(EnumMacDrawerState.UnloadGotoHomeComplete);
            MacState sUnloadGotoHomeFail = NewState(EnumMacDrawerState.UnloadGotoHomeFail);
            MacState sUnloadGotoHomeTimeOut = NewState(EnumMacDrawerState.UnloadGotoHomeTimeOut);

            // UnLoad, 在HOme 點檢查Tray 上是否有盒子
            MacState sUnloadCheckBoxExistenceAtHome = NewState(EnumMacDrawerState.UnloadCheckBoxExistenceAtHome);
            // UnLoad, 在Home 點檢查 是否有盒子=> 有, 2020/08/03 King Liu Add New 
            MacState sUnloadBoxExistAtHome = NewState(EnumMacDrawerState.UnloadBoxExistAtHome);
            // UnLoad, 在Home 點查 是否有 盒子=> 没有, 2020/08/03 King Liu Add New 
            MacState sUnloadBoxNotExistAtHome = NewState(EnumMacDrawerState.UnloadBoxNotExistAtHome);
            // UnLoad, 在Home 點檢查是否有盒子時逾時,2020/08/03 King Liu Add New  
            MacState sUnloadCheckBoxExistenceAtHomeTimeOut = NewState(EnumMacDrawerState.UnloadCheckBoxExistenceAtHomeTimeOut);

            // UnLoad, 在Home時被檢出没有盒子, 回退到 Out 
            MacState sUnloadNoBoxRejectToOutFromHomeStart = NewState(EnumMacDrawerState.UnloadNoBoxRejectToOutFromHomeStart);
            MacState sUnloadNoBoxRejectToOutFromHomeIng = NewState(EnumMacDrawerState.UnloadNoBoxRejectToOutFromHomeIng);
            MacState sUnloadNoBoxRejectToOutFromHomeComplete = NewState(EnumMacDrawerState.UnloadNoBoxRejectToOutFromHomeComplete);
            MacState sUnloadNoBoxRejectToOutFromHomeFail = NewState(EnumMacDrawerState.UnloadNoBoxRejectToOutFromHomeFail);
            MacState sUnloadNoBoxRejectToOutFromHomeTimeOut = NewState(EnumMacDrawerState.UnloadNoBoxRejectToOutFromHomeTimeOut);


            // move tray to In from Home
            MacState sUnloadGotoInStart = NewState(EnumMacDrawerState.UnloadGotoInStart);
            MacState sUnloadGotoInIng = NewState(EnumMacDrawerState.UnloadGotoInIng);
            MacState sUnloadGotoInComplete = NewState(EnumMacDrawerState.UnloadGotoInComplete);
            MacState sUnloadGotoInFail=NewState(EnumMacDrawerState.UnloadGotoInFail);
            MacState sUnloadGotoInTimeOut = NewState(EnumMacDrawerState.UnloadGotoInTimeOut);
            // 等待將盒子取走
            MacState sIdleForGetBoxOnTrayAtIn = NewState(EnumMacDrawerState.IdleForGetBoxOnTrayAtIn);

        

            #endregion State

            #region Transition

            // Initial,

            MacTransition tInitialStart_InitialIng = NewTransition(sInitialStart, sInitialIng, EnumMacDrawerTransition.InitialStart_InitialIng);
            MacTransition tInitialing_InitialComplete = NewTransition(sInitialIng, sInitialComplete, EnumMacDrawerTransition.Initialing_InitialComplete);
            MacTransition tInitialing_InitialFail = NewTransition(sInitialIng, sInitialFail, EnumMacDrawerTransition.Initialing_InitialFail);
            MacTransition tInitialing_InitialTimeOut = NewTransition(sInitialIng, sInitialTimeOut, EnumMacDrawerTransition.Initialing_InitialTimeOut);

            // Load(將 Tray 移到 定位~ In的位置 )
            MacTransition tLoadGotoInStart_LoadGotoInIng = NewTransition(sLoadGotoInStart, sLoadGotoInIng, EnumMacDrawerTransition.LoadGotoInStart_LoadGotoInIng);
            MacTransition tLoadGotoInIng_LoadGotoInComplete = NewTransition(sLoadGotoInIng, sLoadGotoInComplete, EnumMacDrawerTransition.LoadGotoInIng_LoadGotoInComplete);
            MacTransition tLoadGotoInIng_LoadGotoInTimeOut = NewTransition(sLoadGotoInIng, sLoadGotoInTimeOut, EnumMacDrawerTransition.LoadGotoInIng_LoadGotoInTimeOut);
            MacTransition tLoadGotoInIng_LoadGotoInFail = NewTransition(sLoadGotoInIng, sLoadGotoInFail, EnumMacDrawerTransition.LoadGotoInIng_LoadGotoInFail);
            MacTransition tLoadGotoInComplete_IdleForPutBoxOnTrayAtIn = NewTransition(sLoadGotoInIng, sIdleForPutBoxOnTrayAtIn, EnumMacDrawerTransition.LoadGotoInComplete_IdleForPutBoxOnTrayAtIn); 

          
            // Load (將 Tray 從 In 移到 Home) 
            MacTransition tLoadGotoHomeStart_LoadGotoHomeIng = NewTransition(sLoadGotoHomeStart, sLoadGotoHomeIng, EnumMacDrawerTransition.LoadGotoHomeStart_LoadGotoHomeIng);
            MacTransition tLoadGotoHomeIng_LoadGotoHomeComplete = NewTransition(sLoadGotoHomeIng, sLoadGotoHomeComplete, EnumMacDrawerTransition.LoadGotoHomeIng_LoadGotoHomeComplete);
            MacTransition tLoadGotoHomeIng_LoadGotoHomeTimeOut = NewTransition(sLoadGotoHomeIng, sLoadGotoHomeTimeOut, EnumMacDrawerTransition.LoadGotoHomeIng_LoadGotoHomeTimeOut);
            MacTransition tLoadGotoHomeIng_LoadGotoHomeFail = NewTransition(sLoadGotoHomeIng, sLoadGotoHomeTimeOut, EnumMacDrawerTransition.LoadGotoHomeIng_LoadGotoHomeFail);

            // Load(將 Tray 移到 Home之後,檢查Box
            MacTransition tLoadGotoHomeComplete_LoadCheckBoxExistenceAtHome = NewTransition(sLoadGotoHomeComplete, sLoadCheckBoxExistenceAtHome, EnumMacDrawerTransition.LoadGotoHomeComplete_LoadCheckBoxExistenceAtHome);
            MacTransition tLoadCheckBoxExistenceAtHome_LoadBoxExistAtHome = NewTransition(sLoadCheckBoxExistenceAtHome,sLoadBoxExistAtHome, EnumMacDrawerTransition.LoadCheckBoxExistenceAtHome_LoadBoxExistAtHome);
            MacTransition tLoadCheckBoxExistenceAtHome_LoadBoxNotExistAtHome = NewTransition(sLoadCheckBoxExistenceAtHome, sLoadBoxExistAtHome, EnumMacDrawerTransition.LoadCheckBoxExistenceAtHome_LoadBoxNotExistAtHome);
            MacTransition tLoadCheckBoxExistenceAtHome_LoadCheckBoxExistenceAtHomeTimeOut = NewTransition(sLoadCheckBoxExistenceAtHome, sLoadBoxExistAtHome, EnumMacDrawerTransition.LoadCheckBoxExistenceAtHome_LoadCheckBoxExistenceAtHomeTimeOut);

            // Load (檢查有盒子之後再回 LoadGotoHomeComplete)
            MacTransition tLoadBoxExistAtHome_LoadGotoHomeComplete = NewTransition(sLoadBoxExistAtHome, sLoadGotoHomeComplete, EnumMacDrawerTransition.LoadBoxExistAtHome_LoadGotoHomeComplete);
            // Load (檢查没有盒子後再回 LoadGotoHomeComplete)
            MacTransition tLoadBoxNotExistAtHome_LoadGotoHomeComplete = NewTransition(sLoadBoxNotExistAtHome, sLoadGotoHomeComplete, EnumMacDrawerTransition.LoadBoxNotExistAtHome_LoadGotoHomeComplete);
            MacTransition tLoadGotoHomeComplete_LoadNoBoxRejectToInFromHomeStart = NewTransition(sLoadGotoHomeComplete, sLoadNoBoxRejectToInFromHomeStart,EnumMacDrawerTransition.LoadGotoHomeComplete_LoadNoBoxRejectToInFromHomeStart);
            MacTransition tLoadNoBoxRejectToInFromHomeStart_LoadNoBoxRejectToInFromHomeIng = NewTransition(sLoadNoBoxRejectToInFromHomeStart, sLoadNoBoxRejectToInFromHomeIng, EnumMacDrawerTransition.UnloadNoBoxRejectToInFromHomeStart_UnloadNoBoxRejectToOutFromHomeIng);
            MacTransition tLoadNoBoxRejectToInFromHomeIng_LoadNoBoxRejectToInFromHomeComplete = NewTransition(sLoadNoBoxRejectToInFromHomeIng, sLoadNoBoxRejectToInFromHomeComplete, EnumMacDrawerTransition.LoadNoBoxRejectToInFromHomeIng_LoadNoBoxRejectToInFromHomeComplete);
            MacTransition tLoadNoBoxRejectToInFromHomeIng_LoadNoBoxRejectToInFromHomeFail= NewTransition(sLoadNoBoxRejectToInFromHomeIng, sLoadNoBoxRejectToInFromHomeFail, EnumMacDrawerTransition.LoadNoBoxRejectToInFromHomeIng_LoadNoBoxRejectToInFromHomeFail);
            MacTransition tLoadNoBoxRejectToInFromHomeIng_LoadNoBoxRejectToInFromHomeTimeOut = NewTransition(sLoadNoBoxRejectToInFromHomeIng, sLoadNoBoxRejectToInFromHomeTimeOut, EnumMacDrawerTransition.LoadNoBoxRejectToInFromHomeIng_LoadNoBoxRejectToInFromHomeTimeOut);
            MacTransition tLoadNoBoxRejectToInFromHomeComplete_IdleForPutBoxOnTrayAtIn = NewTransition(sLoadNoBoxRejectToInFromHomeComplete, sIdleForPutBoxOnTrayAtIn, EnumMacDrawerTransition.LoadNoBoxRejectToInFromHomeComplete_IdleForPutBoxOnTrayAtIn);

            // Load (將 Tray 從 Home 移到 Out)
            MacTransition tLoadGotoHomeComplete_LoadGotoOutStart = NewTransition(sLoadGotoHomeIng, sLoadGotoOutStart, EnumMacDrawerTransition.LoadGotoHomeComplete_LoadGotoOutStart);
            MacTransition tLoadGotoOutStart_LoadGotoOutIng = NewTransition(sLoadGotoOutStart, sLoadGotoOutIng, EnumMacDrawerTransition.LoadGotoOutStart_LoadGotoOutIng);
            MacTransition tLoadGotoOutIng_LoadGotoOutComplete = NewTransition(sLoadGotoOutIng, sLoadGotoOutComplete, EnumMacDrawerTransition.LoadGotoOutIng_LoadGotoOutComplete);
            MacTransition tLoadGotoOutIng_LoadGotoOutTimeOut = NewTransition(sLoadGotoOutIng, sLoadGotoOutTimeOut, EnumMacDrawerTransition.LoadGotoOutIng_LoadGotoOutTimeOut);
            MacTransition tLoadGotoOutIng_LoadGotoOutFail = NewTransition(sLoadGotoOutIng, sLoadGotoOutFail, EnumMacDrawerTransition.LoadGotoOutIng_LoadGotoOutFail);
            // 可以夾走盒子
            MacTransition tLoadGotoOutComplete_IdleForGetBoxOnTrayAtOut = NewTransition(sLoadGotoOutComplete, sIdleForGetBoxOnTrayAtOut, EnumMacDrawerTransition.LoadGotoOutComplete_IdleForGetBoxOnTrayAtOut);

            // Unload (將 Tray 移到 Out 位置)
            MacTransition tUnloadGotoOutStart_UnloadGotoOutIng = NewTransition(sUnloadGotoOutStart, sUnloadGotoOutIng, EnumMacDrawerTransition.UnloadGotoOutStart_UnloadGotoOutIng);
            MacTransition tUnloadGotoOutIng_UnloadGotoOutComplete = NewTransition(sUnloadGotoOutIng, sUnloadGotoOutComplete, EnumMacDrawerTransition.UnloadGotoOutIng_UnloadGotoOutComplete);
            MacTransition tUnloadGotoOutIng_UnloadGotoOutFail = NewTransition(sUnloadGotoOutIng, sUnloadGotoOutFail, EnumMacDrawerTransition.UnloadGotoOutIng_UnloadGotoOutFail);
            MacTransition tUnloadGotoOutIng_UnloadGotoOutTimeOut = NewTransition(sUnloadGotoOutIng, sUnloadGotoOutTimeOut, EnumMacDrawerTransition.UnloadGotoOutIng_UnloadGotoOutTimeOut);
            // Unload 可以放進盒子
            MacTransition tUnloadGotoOutComplete_IdleForPutBoxOnTrayAtOut = NewTransition(sUnloadGotoOutComplete, sIdleForPutBoxOnTrayAtOut, EnumMacDrawerTransition.UnloadGotoOutComplete_IdleForPutBoxOnTrayAtOut);
            
            // UnLoad(將Tray 移到 Home) 
            MacTransition tUnloadGotoHomeStart_UnloadGotoHomeIng = NewTransition(sUnloadGotoHomeStart, sUnloadGotoHomeIng, EnumMacDrawerTransition.UnloadGotoHomeStart_UnloadGotoHomeIng);
            MacTransition tUnloadGotoHomeIng_UnloadGotoHomeComplete = NewTransition(sUnloadGotoHomeIng, sUnloadGotoHomeComplete, EnumMacDrawerTransition.UnloadGotoHomeIng_UnloadGotoHomeComplete);
            MacTransition tUnloadGotoHomeIng_UnloadGotoHomeTimeOut = NewTransition(sUnloadGotoHomeIng, sUnloadGotoHomeTimeOut, EnumMacDrawerTransition.UnloadGotoHomeIng_UnloadGotoHomeTimeOut);
            MacTransition tUnloadGotoHomeIng_UnloadGotoHomeFail = NewTransition(sUnloadGotoHomeIng, sUnloadGotoHomeFail, EnumMacDrawerTransition.UnloadGotoHomeIng_UnloadGotoHomeFail);

            // Load(將 Tray 移到 Home之後,檢查Box
            MacTransition tUnloadGotoHomeComplete_UnloadCheckBoxExistenceAtHome = NewTransition(sUnloadGotoHomeComplete, sUnloadCheckBoxExistenceAtHome, EnumMacDrawerTransition.UnloadGotoHomeComplete_UnloadCheckBoxExistenceAtHome);
            MacTransition tUnloadCheckBoxExistenceAtHome_UnloadBoxExistAtHome = NewTransition(sUnloadCheckBoxExistenceAtHome, sUnloadBoxExistAtHome, EnumMacDrawerTransition.UnloadCheckBoxExistenceAtHome_UnloadBoxExistAtHome);
            MacTransition tUnloadCheckBoxExistenceAtHome_UnloadBoxNotExistAtHome = NewTransition(sUnloadCheckBoxExistenceAtHome, sUnloadBoxExistAtHome, EnumMacDrawerTransition.UnloadCheckBoxExistenceAtHome_UnloadBoxNotExistAtHome);
            MacTransition tUnloadCheckBoxExistenceAtHome_UnloadCheckBoxExistenceAtHomeTimeOut = NewTransition(sUnloadCheckBoxExistenceAtHome, sUnloadBoxExistAtHome, EnumMacDrawerTransition.UnloadCheckBoxExistenceAtHome_UnloadCheckBoxExistenceAtHomeTimeOut);

            // Load (檢查有盒子之後再回 LoadGotoHomeComplete)
            MacTransition tUnloadBoxExistAtHome_UnloadGotoHomeComplete = NewTransition(sUnloadBoxExistAtHome, sUnloadGotoHomeComplete, EnumMacDrawerTransition.UnloadBoxExistAtHome_UnloadGotoHomeComplete);
            // Load (檢查没有盒子後再回 LoadGotoHomeComplete)
            MacTransition tUnloadBoxNotExistAtHome_UnloadGotoHomeComplete = NewTransition(sUnloadBoxNotExistAtHome, sUnloadGotoHomeComplete, EnumMacDrawerTransition.UnloadBoxNotExistAtHome_UnloadGotoHomeComplete);
            MacTransition tUnloadGotoHomeComplete_UnloadNoBoxRejectToOutFromHomeStart = NewTransition(sUnloadGotoHomeComplete, sUnloadNoBoxRejectToOutFromHomeStart, EnumMacDrawerTransition.UnloadGotoHomeComplete_UnloadNoBoxRejectToOutFromHomeStart);
            MacTransition tUnloadNoBoxRejectToOutFromHomeStart_UnloadNoBoxRejectToOutFromHomeIng = NewTransition(sUnloadNoBoxRejectToOutFromHomeStart, sUnloadNoBoxRejectToOutFromHomeIng, EnumMacDrawerTransition.UnloadNoBoxRejectToInFromHomeStart_UnloadNoBoxRejectToOutFromHomeIng);
            MacTransition tUnloadNoBoxRejectToOutFromHomeIng_UnloadNoBoxRejectToOutFromHomeComplete = NewTransition(sUnloadNoBoxRejectToOutFromHomeIng, sUnloadNoBoxRejectToOutFromHomeComplete, EnumMacDrawerTransition.UnloadNoBoxRejectToInFromHomeIng_UnloadNoBoxRejectToOutFromHomeComplete);
            MacTransition tUnloadNoBoxRejectToOutFromHomeIng_UnloadNoBoxRejectToOutFromHomeFail = NewTransition(sUnloadNoBoxRejectToOutFromHomeIng, sUnloadNoBoxRejectToOutFromHomeFail, EnumMacDrawerTransition.UnloadNoBoxRejectToInFromHomeIng_UnloadNoBoxRejectToOutFromHomeFail);
            MacTransition tUnloadNoBoxRejectToOutFromHomeIng_UnloadNoBoxRejectToOutFromHomeTimeOut = NewTransition(sUnloadNoBoxRejectToOutFromHomeIng, sUnloadNoBoxRejectToOutFromHomeTimeOut, EnumMacDrawerTransition.UnloadNoBoxRejectToInFromHomeIng_UnloadNoBoxRejectToOutFromHomeTimeOut);
            MacTransition tUnloadNoBoxRejectToOutFromHomeComplete_IdleForPutBoxOnTrayAtOut = NewTransition(sUnloadNoBoxRejectToOutFromHomeComplete, sIdleForPutBoxOnTrayAtOut, EnumMacDrawerTransition.UnloadNoBoxRejectToOutFromHomeComplete_IdleForPutBoxOnTrayAtOut);



            // Unload(將 Tray 移到 In)
            MacTransition tUnloadGotoHomeComplete_UnloadGotoInStart = NewTransition(sUnloadGotoHomeComplete, sUnloadGotoInStart, EnumMacDrawerTransition.UnloadGotoHomeComplete_UnloadGotoInStart);
            MacTransition tUnloadGotoInStart_UnloadGotoInIng = NewTransition(sUnloadGotoInStart, sUnloadGotoInIng, EnumMacDrawerTransition.UnloadGotoInStart_UnloadGotoInIng);
            MacTransition tUnloadGotoInIng_UnloadGotoInComplete = NewTransition(sUnloadGotoHomeIng, sUnloadGotoHomeComplete, EnumMacDrawerTransition.UnloadGotoInIng_UnloadGotoInComplete);
            MacTransition tUnloadGotoInIng_UnloadGotoInFail = NewTransition(sUnloadGotoInIng, sUnloadGotoInFail, EnumMacDrawerTransition.UnloadGotoInIng_UnloadGotoInFail);
            MacTransition tUnloadGotoInIng_UnloadGotoInTimeOut = NewTransition(sUnloadGotoInIng, sUnloadGotoInTimeOut, EnumMacDrawerTransition.UnloadGotoInIng_UnloadGotoInTimeOut);
            // Unload 可以將 Box 從 位於 In 的Tray 取走
            MacTransition tUnloadGotoInComplete_IdleForGetBoxOnTrayAtIn = NewTransition(sUnloadGotoInComplete, sIdleForGetBoxOnTrayAtIn, EnumMacDrawerTransition.UnloadGotoInComplete_IdleForGetBoxOnTrayAtIn);

            
               #endregion


            #region  Event

            sInitialStart.OnEntry+= ( sender,  e)=>
             {
                 var state = (MacState)sender;
                 HalDrawer.CommandINI();
                 state.DoExit(new MacStateExitEventArgs());
             };
            sInitialStart.OnExit += (sender, e) =>
            {
              //  var thisState = (MacState)sender;
                //var thisTransition = this.Transitions[EnumMacDrawerTransition.InitialStart_InitialIng.ToString()];
                var nextState = tInitialStart_InitialIng.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sInitialIng.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                var startTime = DateTime.Now;
               // var dicTransition = this.Transitions;
                Action guard = () =>
                {
                    while (true)
                    {
                        MacTransition transition = null;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome)
                        {  // Initial  Complete
                            transition = tInitialing_InitialComplete;// dicTransition[EnumMacDrawerTransition.Initialing_InitialComplete.ToString()];
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.InitialFailed)
                        {  // Initial Failed
                            transition = tInitialing_InitialFail;//dicTransition[EnumMacDrawerTransition.Initialing_InitialFail.ToString()];
                        }
                        else if (timeoutObj.IsTimeOut(startTime))
                        {  // 逾時
                            transition = tInitialing_InitialTimeOut;//dicTransition[EnumMacDrawerTransition.Initialing_InitialTimeOut.ToString()];
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
                var state = (MacState)sender;
                HalDrawer.CommandTrayMotionIn();
                state.DoExit(new MacStateExitEventArgs());
            };
            sLoadGotoInStart.OnExit += (sender, e) =>
            {
              //  var thisTransition = this.Transitions[EnumMacDrawerTransition.LoadGotoInStart_LoadGotoInIng.ToString()];
                var nextState = tLoadGotoInStart_LoadGotoInIng.StateTo;
                 nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sLoadGotoInIng.OnEntry += (sender, e)=>
            {
                var state = (MacState)sender;
                DateTime thisTime = DateTime.Now;
              //  var dicTransition = this.Transitions;
                Action guard = () =>
                {
                    while (true)
                    {
                        MacTransition transition = null;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArraiveAtIn)
                        {// Complete
                            transition = tLoadGotoInIng_LoadGotoInComplete;// dicTransition[EnumMacDrawerTransition.LoadGotoInIng_LoadGotoInComplete.ToString()];
                       }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {   // Failed
                            transition = tLoadGotoInIng_LoadGotoInFail;//dicTransition[EnumMacDrawerTransition.LoadGotoInIng_LoadGotoInFail.ToString()];
                        }
                        else if (timeoutObj.IsTimeOut(thisTime))
                        {    // Time Out
                            transition = tLoadGotoInIng_LoadGotoInTimeOut;// this.Transitions[EnumMacDrawerTransition.LoadGotoInIng_LoadGotoInTimeOut.ToString()];

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
                
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sLoadGotoInComplete.OnExit += (sender, e) =>
            {
                //var transition = this.Transitions[EnumMacDrawerTransition.LoadGotoInComplete_IdleForPutBoxOnTrayAtIn.ToString()];
                var nextState = tLoadGotoInComplete_IdleForPutBoxOnTrayAtIn.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };
           
            sIdleForPutBoxOnTrayAtIn.OnEntry += (sender, e) =>
              {
                  var state = (MacState)sender;
                  state.DoExit(new MacStateExitEventArgs());

              };
            sIdleForPutBoxOnTrayAtIn.OnExit += (sender, e) =>
            {
                // Load 之前置工作,可以將 盒子放上 Tray

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

            sLoadGotoHomeStart.OnEntry += (sender,e) =>
            {
                var state = (MacState)sender;
                HalDrawer.CommandTrayMotionHome();
                state.DoExit(new MacStateExitEventArgs());
            };
            sLoadGotoHomeStart.OnExit += (sender, e) =>
            {
               // var thisTransition = this.Transitions[EnumMacDrawerTransition.LoadGotoHomeStart_LoadGotoHomeIng.ToString()];
                var nextState = tLoadGotoHomeStart_LoadGotoHomeIng.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sLoadGotoHomeIng.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                DateTime thisTime = DateTime.Now;
              //  var dicTransition = this.Transitions;
                Action guard = () =>
                {
                    while (true)
                    {
                        MacTransition transition=null;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome)
                        {  // Complete
                            transition = tLoadGotoHomeIng_LoadGotoHomeComplete;// dicTransition[EnumMacDrawerTransition.LoadGotoHomeIng_LoadGotoHomeComplete.ToString()];
                        }
                        else if (timeoutObj.IsTimeOut(thisTime))
                        {  // 逾時
                            transition = tLoadGotoHomeIng_LoadGotoHomeTimeOut;//dicTransition[EnumMacDrawerTransition.LoadGotoHomeIng_LoadGotoHomeTimeOut.ToString()];
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            transition = tLoadGotoHomeIng_LoadGotoHomeFail;//dicTransition[EnumMacDrawerTransition.LoadGotoHomeIng_LoadGotoHomeFail.ToString()];
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
                  nextState.DoEntry(new MacStateEntryEventArgs(EnumMacDrawerLoadToHomeCompleteSource.GotoHomeIng));
              };
            
            sLoadGotoHomeComplete.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                var args = (MacStateEntryEventArgs)e;
                EnumMacDrawerLoadToHomeCompleteSource source;
                MacTransition transition = null; //tLoadGotoHomeComplete_LoadCheckBoxExistenceAtHome;
                if (args.Parameter != null)
                {                    
                    source = (EnumMacDrawerLoadToHomeCompleteSource)(args.Parameter);
                    if(source== EnumMacDrawerLoadToHomeCompleteSource.GotoHomeIng)
                    {    // 從 GomeIng 到 GotoHomeComplete=> 去檢查合子在不在
                        transition = tLoadGotoHomeComplete_LoadCheckBoxExistenceAtHome;
                    }
                  
                    else if (source == EnumMacDrawerLoadToHomeCompleteSource.LoadCheckBoxNotExist)
                    {   // 檢查完, 盒子不存在回退至 In
                        transition = tLoadGotoHomeComplete_LoadNoBoxRejectToInFromHomeStart;

                    }
                    else if (source == EnumMacDrawerLoadToHomeCompleteSource.LoadCheckBoxExist)
                    {    // 檢查完盒子存在=> 將盒子移到 Out 
                        transition = tLoadGotoHomeComplete_LoadGotoOutStart;
                    }
                }
                
                state.DoExit(new MacStateExitWithTransitionEventArgs(transition));
            };
            sLoadGotoHomeComplete.OnExit += (sender, e) =>
            {
              
                var args = (MacStateExitWithTransitionEventArgs)e;
                 var nextState = args.Transition.StateTo;
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

            sLoadCheckBoxExistenceAtHome.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                var startTime = DateTime.Now;
                MacTransition transition = null;
                // guard
                Action guard = () =>
                {
                   while (true)
                    {
                        if(HalDrawer.CurrentWorkState== DrawerWorkState.BoxExist)
                        {   // 有盒子 
                            transition = tLoadCheckBoxExistenceAtHome_LoadBoxExistAtHome;
                        }
                        else if(HalDrawer.CurrentWorkState == DrawerWorkState.BoxNotExist)
                        {   // 没盒子
                            transition = tLoadCheckBoxExistenceAtHome_LoadBoxNotExistAtHome;
                        }
                        else if(new MacDrawerStateTimeOutController().IsTimeOut(startTime))
                        {   // 逾時
                            transition = tLoadCheckBoxExistenceAtHome_LoadCheckBoxExistenceAtHomeTimeOut;
                        }

                        if(transition != null)
                        {
                            state.DoExit(new MacStateExitWithTransitionEventArgs(transition));
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };

                new Task(guard).Start();
                HalDrawer.CommandBoxDetection();
                state.DoExit(new MacStateExitWithTransitionEventArgs(null));
            };
            sLoadCheckBoxExistenceAtHome.OnExit += (sender, e) =>
            {
                var args = (MacStateExitWithTransitionEventArgs)e;
                var nextState = args.Transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sLoadBoxExistAtHome.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sLoadBoxExistAtHome.OnExit += (sender, e) =>
            {
                //var transition = tLoadBoxExistAtHome_LoadGotoHomeComplete;
                var nextState = tLoadBoxExistAtHome_LoadGotoHomeComplete.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(EnumMacDrawerLoadToHomeCompleteSource.LoadCheckBoxExist));
            };

            sLoadBoxNotExistAtHome.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sLoadBoxNotExistAtHome.OnExit += (sender, e) =>
            {
                //var transition = tLoadBoxNotExistAtHome_LoadGotoHomeComplete;
                var nextState = tLoadBoxNotExistAtHome_LoadGotoHomeComplete.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(EnumMacDrawerLoadToHomeCompleteSource.LoadCheckBoxNotExist));
            };

            sLoadCheckBoxExistenceAtHomeTimeOut.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sLoadCheckBoxExistenceAtHomeTimeOut.OnExit += (sender, e) =>
            {
                // TODO: Tray 到達 Home 之後, 檢查Box 是否存在時, 逾時
            };

            sLoadNoBoxRejectToInFromHomeStart.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sLoadNoBoxRejectToInFromHomeStart.OnExit += (sender, e) =>
            {
                var nextState = tLoadNoBoxRejectToInFromHomeStart_LoadNoBoxRejectToInFromHomeIng.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
                
            };

            sLoadNoBoxRejectToInFromHomeIng.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                DateTime startTime = DateTime.Now;
                Action guard = () =>
                {
                    MacTransition transition = null;
                    while (true)
                    {
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            transition = tLoadNoBoxRejectToInFromHomeIng_LoadNoBoxRejectToInFromHomeFail;
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArraiveAtIn)
                        {
                            transition = tLoadNoBoxRejectToInFromHomeIng_LoadNoBoxRejectToInFromHomeComplete;
                        }
                        else if(new MacDrawerStateTimeOutController().IsTimeOut(startTime))
                        {
                            transition = tLoadNoBoxRejectToInFromHomeIng_LoadNoBoxRejectToInFromHomeTimeOut;
                        }
                        if (transition != null)
                        {
                            state.DoExit(new MacStateExitWithTransitionEventArgs(transition) );
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
                HalDrawer.CommandTrayMotionIn();
            };
            sLoadNoBoxRejectToInFromHomeIng.OnExit += (sender, e) =>
            {
                var nextState = ((MacStateExitWithTransitionEventArgs)e).Transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sLoadNoBoxRejectToInFromHomeComplete.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs() );
            };
            sLoadNoBoxRejectToInFromHomeComplete.OnExit += (sender, e) =>
            {
                var nextState = tLoadNoBoxRejectToInFromHomeComplete_IdleForPutBoxOnTrayAtIn.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sLoadNoBoxRejectToInFromHomeFail.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sLoadNoBoxRejectToInFromHomeFail.OnExit += (sender, e) =>
            {
                // Load 時, Tray 移到 Home 檢查没有Box, 回退到 In 時 無法移動
                // 此為 無法移動的最後一個狀態
            };

            sLoadNoBoxRejectToInFromHomeTimeOut.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sLoadNoBoxRejectToInFromHomeTimeOut.OnExit += (sender, e) =>
            {
                // Load 時, Tray 移到 Home 檢查没有Box, 回退到 In 時 逾時未到達 In
                // 此為 逾時的最後一個狀態
            };


            sLoadGotoOutStart.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                HalDrawer.CommandTrayMotionOut();
                state.DoExit(new MacStateExitEventArgs());
            };
            sLoadGotoOutStart.OnExit += (sender, e) =>
            {
               // var transition = this.Transitions[EnumMacDrawerTransition.LoadGotoOutStart_LoadGotoOutIng.ToString()];
                var nextState = tLoadGotoOutStart_LoadGotoOutIng.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sLoadGotoOutIng.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                DateTime thisTime = DateTime.Now;
               // var dicTransition = this.Transitions;
                Action guard = () =>
                {
                    while (true)
                    {
                        MacTransition transition = null;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtOut)
                        {   // 完成
                            transition = tLoadGotoOutIng_LoadGotoOutComplete;// dicTransition[EnumMacDrawerTransition.LoadGotoOutIng_LoadGotoOutComplete.ToString()];
                        }
                        else if (timeoutObj.IsTimeOut(thisTime))
                        {   // 逾時
                            transition = tLoadGotoOutIng_LoadGotoOutTimeOut;// dicTransition[EnumMacDrawerTransition.LoadGotoOutIng_LoadGotoOutTimeOut.ToString()];
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {   // 無法完成
                            transition = tLoadGotoOutIng_LoadGotoOutFail;// dicTransition[EnumMacDrawerTransition.LoadGotoOutIng_LoadGotoOutFail.ToString()];
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
               // var transition = this.Transitions[EnumMacDrawerTransition.LoadGotoOutComplete_IdleForGetBoxOnTrayAtOut.ToString()];
                var nextState = tLoadGotoOutComplete_IdleForGetBoxOnTrayAtOut.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };
            sIdleForGetBoxOnTrayAtOut.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sIdleForGetBoxOnTrayAtOut.OnExit += (sender, e) =>
            {
                 // Load  Tray 到達 Out 位置, 等待將盒子取走 
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


            sUnloadGotoOutStart.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                HalDrawer.CommandTrayMotionOut();
                state.DoExit(new MacStateExitEventArgs());
            };
            sUnloadGotoOutStart.OnExit += (sender, e) =>
            {
               // var transition = this.Transitions[EnumMacDrawerTransition.UnloadGotoOutStart_UnloadGotoOutIng.ToString()];
                var nextState = tUnloadGotoOutStart_UnloadGotoOutIng.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sUnloadGotoOutIng.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                DateTime thisTime = DateTime.Now;
              //  var dicTransition = this.Transitions;
                Action guard = () =>
                {
                    while (true)
                    {
                        MacTransition transition = null;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtOut)
                        {   // 完成
                            transition = tUnloadGotoOutIng_UnloadGotoOutComplete;// dicTransition[EnumMacDrawerTransition.UnloadGotoOutIng_UnloadGotoOutComplete.ToString()];
                        }
                        else if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // 逾時
                            transition = tUnloadGotoOutIng_UnloadGotoOutTimeOut;//dicTransition[EnumMacDrawerTransition.UnloadGotoOutIng_UnloadGotoOutTimeOut.ToString()];
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            // 執行不成功
                            transition = tUnloadGotoOutIng_UnloadGotoOutFail;//dicTransition[EnumMacDrawerTransition.UnloadGotoOutIng_UnloadGotoOutFail.ToString()];
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
               // var transition = this.Transitions[EnumMacDrawerTransition.UnloadGotoOutComplete_IdleForPutBoxOnTrayAtOut.ToString()];
                var nextState = tUnloadGotoOutComplete_IdleForPutBoxOnTrayAtOut.StateTo;
                nextState.DoExit(new MacStateExitEventArgs());
            };
            sIdleForPutBoxOnTrayAtOut.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sIdleForPutBoxOnTrayAtOut.OnExit += (sender, e) =>
            {
                // Unload, Tray 己經移到 Out 的位置, 等待將盒子到 Tray
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

          

            sUnloadGotoHomeStart.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                HalDrawer.CommandTrayMotionHome();
                state.DoExit(new MacStateExitEventArgs());
            };
            sUnloadGotoHomeStart.OnExit += (sender, e) =>
            {
               // var transition = this.Transitions[EnumMacDrawerTransition.UnloadGotoHomeStart_UnloadGotoHomeIng.ToString()];
                var nextState = tUnloadGotoHomeStart_UnloadGotoHomeIng.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sUnloadGotoHomeIng.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                DateTime thisTime = DateTime.Now;
               // var dicTransition = this.Transitions;
                Action guard = () =>
                {
                    while (true)
                    {
                        MacTransition transition = null;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome)
                        {
                            transition = tUnloadGotoHomeIng_UnloadGotoHomeComplete;// dicTransition[EnumMacDrawerTransition.UnloadGotoHomeIng_UnloadGotoHomeComplete.ToString()];
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            transition = tUnloadGotoHomeIng_UnloadGotoHomeFail;//dicTransition[EnumMacDrawerTransition.UnloadGotoHomeIng_UnloadGotoHomeFail.ToString()];
                        }
                        else if (timeoutObj.IsTimeOut(thisTime))
                        {
                            transition = tUnloadGotoHomeIng_UnloadGotoHomeTimeOut;//dicTransition[EnumMacDrawerTransition.UnloadGotoHomeIng_UnloadGotoHomeTimeOut.ToString()];
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
                nextState.DoEntry(new MacStateEntryEventArgs(EnumMacDrawerUnloadToHomeCompleteSource.GotoHomeIng));
             };

            sUnloadGotoHomeComplete.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                var args = (MacStateEntryEventArgs)e;
                if(args.Parameter != null)
                {
                    MacTransition transition = null;
                    var source = (EnumMacDrawerUnloadToHomeCompleteSource)(args.Parameter);
                    if (source == EnumMacDrawerUnloadToHomeCompleteSource.GotoHomeIng)
                    {  // 從 正移動到 Home 完成, 下個Transition 為 檢查有没有盒子 
                        transition = tUnloadGotoHomeComplete_UnloadCheckBoxExistenceAtHome;  
                    }
                    else if(source== EnumMacDrawerUnloadToHomeCompleteSource.UnloadCheckBoxExist)
                    {   //  檢查有盒子之後回到 Home 完成, 要開始移向 In
                        transition = tUnloadGotoHomeComplete_UnloadGotoInStart;
                    }
                    else //if(source == EnumMacDrawerUnloadToHomeCompleteSource.UnloadCheckBoxNotExist)
                    {  // 檢查結果, 没有盒子, 回退到 Out
                        transition = tUnloadGotoHomeComplete_UnloadNoBoxRejectToOutFromHomeStart;
                    }
                   
                }
                state.DoExit(new MacStateExitEventArgs());
               
            };
            sUnloadGotoHomeComplete.OnExit += (sender, e) =>
            {
               //var transition = this.Transitions[EnumMacDrawerTransition.UnloadGotoHomeComplete_UnloadGotoInStart.ToString()];
                var nextState = tUnloadGotoHomeComplete_UnloadGotoInStart.StateTo;
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

            sUnloadCheckBoxExistenceAtHome.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                var startTime = DateTime.Now;
                MacTransition transition = null;
                // guard
                Action guard = () =>
                {
                    while (true)
                    {
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.BoxExist)
                        {   // 有盒子 
                            transition = tUnloadCheckBoxExistenceAtHome_UnloadBoxExistAtHome;
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.BoxNotExist)
                        {   // 没盒子
                            transition = tUnloadCheckBoxExistenceAtHome_UnloadBoxNotExistAtHome;
                        }
                        else if (new MacDrawerStateTimeOutController().IsTimeOut(startTime))
                        {   // 逾時
                            transition = tUnloadCheckBoxExistenceAtHome_UnloadCheckBoxExistenceAtHomeTimeOut;
                        }

                        if (transition != null)
                        {
                            state.DoExit(new MacStateExitWithTransitionEventArgs(transition));
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };

                new Task(guard).Start();
                HalDrawer.CommandBoxDetection();
                state.DoExit(new MacStateExitWithTransitionEventArgs(null));
            };
            sUnloadCheckBoxExistenceAtHome.OnExit += (sender, e) =>
            {
                var args = (MacStateExitWithTransitionEventArgs)e;
                var nextState = args.Transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sUnloadBoxExistAtHome.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sUnloadBoxExistAtHome.OnExit += (sender, e) =>
            {
                var nextState = tUnloadBoxExistAtHome_UnloadGotoHomeComplete.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(EnumMacDrawerUnloadToHomeCompleteSource.UnloadCheckBoxExist));
            };

            sUnloadBoxNotExistAtHome.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sUnloadBoxNotExistAtHome.OnExit += (sender, e) =>
            {
                var nextState = tUnloadBoxExistAtHome_UnloadGotoHomeComplete.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(EnumMacDrawerUnloadToHomeCompleteSource.UnloadCheckBoxNotExist));
            };

            sUnloadCheckBoxExistenceAtHomeTimeOut.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sUnloadCheckBoxExistenceAtHomeTimeOut.OnExit += (sender, e) =>
            {  // Unload 時, 檢查有没有盒子,~逾時檢查不到 
               // TODO: 後續動作, 再討論
            };

            sUnloadNoBoxRejectToOutFromHomeStart.OnEntry += (sender, e) =>
            {  // Unload, 檢查到没有盒子時要回將Tray回退到 Out的位置
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sUnloadNoBoxRejectToOutFromHomeStart.OnExit += (sender, e) =>
            {
                var nextState = tUnloadNoBoxRejectToOutFromHomeStart_UnloadNoBoxRejectToOutFromHomeIng.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sUnloadNoBoxRejectToOutFromHomeIng.OnEntry += (sender, e) =>
             {
                 var state = (MacState)sender;
                 var startTime = DateTime.Now;
                 Action guard = () =>
                 {
                     while (true)
                     {
                         MacTransition transition = null;
                         if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArraiveAtIn)
                         {
                             transition = tUnloadNoBoxRejectToOutFromHomeIng_UnloadNoBoxRejectToOutFromHomeComplete;
                         }
                         else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                         {
                             transition = tUnloadNoBoxRejectToOutFromHomeIng_UnloadNoBoxRejectToOutFromHomeFail;
                         }
                         else if(new MacDrawerStateTimeOutController().IsTimeOut(startTime))
                         {
                             transition = tUnloadNoBoxRejectToOutFromHomeIng_UnloadNoBoxRejectToOutFromHomeTimeOut;
                         }
                         if (transition != null)
                         {
                             state.DoExit(new MacStateExitWithTransitionEventArgs(transition));
                             break;
                         }
                         Thread.Sleep(10);
                     }
                 };
                 new Task(guard).Start();
                 HalDrawer.CommandTrayMotionOut();
             };
            sUnloadNoBoxRejectToOutFromHomeIng.OnExit += (sender, e) =>
            {
                var args = (MacStateExitWithTransitionEventArgs)e;
                var nextState = args.Transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sUnloadNoBoxRejectToOutFromHomeComplete.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sUnloadNoBoxRejectToOutFromHomeComplete.OnExit += (sender, e) =>
            {
                var nextState = tUnloadNoBoxRejectToOutFromHomeComplete_IdleForPutBoxOnTrayAtOut.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

         

            sUnloadNoBoxRejectToOutFromHomeFail.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sUnloadNoBoxRejectToOutFromHomeFail.OnExit += (sender, e) =>
            {  // UNload 時, 在 Home 位置檢查不到 Box, 將Tray 回退到 Out 失敗 
                // TODO: 下一步待討論
            };

            sUnloadNoBoxRejectToOutFromHomeTimeOut.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sUnloadNoBoxRejectToOutFromHomeTimeOut.OnExit += (sender, e) =>
            {
                // UNload 時, 在 Home 位置檢查不到 Box, 將Tray 回退到逾時 
                // TODO: 下一步待討論
            };
            


            sUnloadGotoInStart.OnEntry += (sender, e) => 
            {
                var state = (MacState)sender;
                HalDrawer.CommandTrayMotionIn();
                state.DoExit(new MacStateExitEventArgs());
            };
            sUnloadGotoInStart.OnExit += (sender, e) =>
            {
             //   var transition = this.Transitions[EnumMacDrawerTransition.UnloadGotoInStart_UnloadGotoInIng.ToString()];
                var nextState = tUnloadGotoInStart_UnloadGotoInIng.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sUnloadGotoInIng.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                DateTime thisTime = DateTime.Now;
                //var dicTransition = this.Transitions;
                Action guard = () =>
                {
                    while (true)
                    {
                        MacTransition transition = null;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArraiveAtIn)
                        {
                            transition = tUnloadGotoInIng_UnloadGotoInComplete;// dicTransition[EnumMacDrawerTransition.UnloadGotoInIng_UnloadGotoInComplete.ToString()];
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            transition = tUnloadGotoInIng_UnloadGotoInFail;// dicTransition[EnumMacDrawerTransition.UnloadGotoInIng_UnloadGotoInFail.ToString()];
                        }
                        else if (timeoutObj.IsTimeOut(thisTime))
                        {
                            transition = tUnloadGotoInIng_UnloadGotoInTimeOut;//dicTransition[EnumMacDrawerTransition.UnloadGotoInIng_UnloadGotoInTimeOut.ToString()];
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
                //var transition = this.Transitions[EnumMacDrawerTransition.UnloadGotoInComplete_IdleForGetBoxOnTrayAtIn.ToString()];
                var nextState = tUnloadGotoInComplete_IdleForGetBoxOnTrayAtIn.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sIdleForGetBoxOnTrayAtIn.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sIdleForGetBoxOnTrayAtIn.OnExit += (sender, e) =>
            {
                // Unload, Tray 已經移到 In, 可以將Box取走
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
