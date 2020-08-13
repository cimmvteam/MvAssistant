using MaskAutoCleaner.v1_0.Machine.StateExceptions;
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
    /** Initial
       
     */

    [Guid("204025E5-D96E-467B-A60A-C9997F8B1563")]
    public class MacMsDrawer : MacMachineStateBase
    {
        public IMacHalDrawer HalDrawer { get { return this.halAssembly as IMacHalDrawer; } }
        private MacDrawerStateTimeOutController timeoutObj = new MacDrawerStateTimeOutController();

        #region State Instruction
        /// <summary>初始化</summary>
        /// <remarks>
        /// </remarks>
        public void InitialFromAnyState()
        {
            HalDrawer.SetDrawerWorkState(DrawerWorkState.InitialStart);
            this.States[EnumMacDrawerState.InitialStart.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }

        /// <summary>Load 時, 將 Tray 從任何地方移到 Out 的位置</summary>
        /// <remarks>Out 在機器外</remarks>
        public void Load_MoveTrayToPositionOutFromAnywhere()
        {
            HalDrawer.SetDrawerWorkState(DrawerWorkState.MoveTrayToPositionOutStart);
            this.States[EnumMacDrawerState.LoadMoveTrayToPositionOutStart.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }

        /// <summary>Load 時,  將 Tray 從Out 的位置移到 In 的位置  </summary>
        /// <remarks>
        /// 主要動作:
        /// 從 Out 移到 Home,
        /// 到逹 Home 後檢查 有没有 Box,
        ///    有Box => 繼續移到 In
        ///    没有 Box => 回退到 Out
        /// </remarks>
        public void Load_MoveTrayToPositionInFromPositionIn()
        {
            HalDrawer.SetDrawerWorkState(DrawerWorkState.MoveTrayToPositionHomeStart);
            this.States[EnumMacDrawerState.LoadMoveTrayToPositionHomeStart.ToString()].DoEntry(new MacStateEntryEventArgs(null));
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
            /** Initial **/
            //  開始
            MacState sInitialStart = NewState(EnumMacDrawerState.InitialStart);
            //  進行中
            MacState sInitialIng = NewState(EnumMacDrawerState.InitialIng);
            // 完成 
            MacState sInitialComplete = NewState(EnumMacDrawerState.InitialComplete);
            //  失敗 
            MacState sInitialFail = NewState(EnumMacDrawerState.InitialFail);
            //  逾時未完成
            MacState sInitialTimeOut = NewState(EnumMacDrawerState.InitialTimeout);


            /** Load, 將托盤移到 Out**/
            // 開始
            MacState sLoadMoveTrayToPositionOutStart = NewState(EnumMacDrawerState.LoadMoveTrayToPositionOutStart);
            // 進行中 
            MacState sLoadMoveTrayToPositionOutIng = NewState(EnumMacDrawerState.LoadMoveTrayToPositionOutIng);
            // 完成
            MacState sLoadMoveTrayToPositionOutComplete = NewState(EnumMacDrawerState.LoadMoveTrayToPositionOutComplete);
            // 逾時未到逹
            MacState sLoadMoveTrayToPositionOutTimeOut = NewState(EnumMacDrawerState.LoadMoveTrayToPositionOutTimeOut);
            // 失敗
            MacState sLoadMoveTrayToPositionOutFail = NewState(EnumMacDrawerState.LoadMoveTrayToPositionOutFail);
            // 等待放盒子
            MacState sIdleForPutBoxOnTrayAtPositionOut = NewState(EnumMacDrawerState.IdleForPutBoxOnTrayAtPositionOut);

            /* Load,將托盤移到 Home */
            // 開始
            MacState sLoadMoveTrayToPositionHomeStart = NewState(EnumMacDrawerState.LoadMoveTrayToPositionHomeStart);
            // 進行中
            MacState sLoadMoveTrayToPositionHomeIng = NewState(EnumMacDrawerState.LoadMoveTrayToPositionHomeIng);
            // 完成
            MacState sLoadMoveTrayToPositionHomeComplete = NewState(EnumMacDrawerState.LoadMoveTrayToPositionHomeComplete);
            // 逾時未完成
            MacState sLoadMoveTrayToPositionHomeTimeOut = NewState(EnumMacDrawerState.LoadMoveTrayToPositionHomeTimeOut);
            // 失敗
            MacState sLoadMoveTrayToPositionHomeFail = NewState(EnumMacDrawerState.LoadMoveTrayToPositionHomeFail);


            // Load, 在HOme 點檢查Tray 上是否有盒子
            MacState sLoadCheckBoxExistenceAtPositionHome = NewState(EnumMacDrawerState.LoadCheckBoxExistenceAtPositionHome);
            // Load, 在Home 點檢查 是否有盒子=> 有, 2020/08/03 King Liu Add New 
            MacState sLoadBoxExistAtPositionHome = NewState(EnumMacDrawerState.LoadBoxExistAtPositionHome);
            // Load, 在Home 點查 是否有 盒子=> 没有, 2020/08/03 King Liu Add New 
            MacState sLoadBoxNotExistAtPositionHome = NewState(EnumMacDrawerState.LoadBoxNotExistAtPositionHome);
            // Load, 在Home 點檢查是否有盒子時逾時,2020/08/03 King Liu Add New  
            MacState sLoadCheckBoxExistenceAtPositionHomeTimeOut = NewState(EnumMacDrawerState.LoadCheckBoxExistenceAtPositionHomeTimeOut);

            /**Load, 在Home時被檢出没有盒子, 回退到Out**/
            // 開始
            MacState sLoadNoBoxRejectTrayToPositionOutFromPositionHomeStart = NewState(EnumMacDrawerState.LoadNoBoxRejectTrayToPositionOutFromPositionHomeStart);
            // 進行中
            MacState sLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng = NewState(EnumMacDrawerState.LoadNoBoxRejectTrayToPositionOutFromPositionHomeIng);
            // 完成
            MacState sLoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete = NewState(EnumMacDrawerState.LoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete);
            // 失敗
            MacState sLoadNoBoxRejectTrayToPositionOutFromPositionHomeFail = NewState(EnumMacDrawerState.LoadNoBoxRejectTrayToPositionOutFromPositionHomeFail);
            // 逾時未完成
            MacState sLoadNoBoxRejectTrayToPositionOutFromPositionHomeTimeOut = NewState(EnumMacDrawerState.LoadNoBoxRejectToPositionOutFromPositionHomeTimeOut);


            /** Load, 將Tray 自Home 移到 In **/
            // 開始
            MacState sLoadMoveTrayToPositionInStart = NewState(EnumMacDrawerState.LoadMoveTrayToPositionInStart);
            // 動作中
            MacState sLoadMoveTrayToPositionInIng = NewState(EnumMacDrawerState.LoadMoveTrayToPositionInIng);
            // 完成 
            MacState sLoadMoveTrayToPositionInComplete = NewState(EnumMacDrawerState.LoadMoveTrayToPositionInComplete);
            // 逾時未完成 
            MacState sLoadMoveTrayToPositionInTimeOut = NewState(EnumMacDrawerState.LoadMoveTrayToPositionInTimeOut);
            // 動作失敗
            MacState sLoadMoveTrayToPositionInFail = NewState(EnumMacDrawerState.LoadMoveTrayToPositionInFail);
            // 等待取走盒子
            MacState sIdleForGetBoxOnTrayAtPositionIn = NewState(EnumMacDrawerState.IdleForGetBoxOnTrayAtPositionIn);


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
            MacTransition tInitialComplete_NULL = NewTransition(sInitialComplete, null, EnumMacDrawerTransition.InitialComplete_NULL); 
            MacTransition tInitialing_InitialFail = NewTransition(sInitialIng, sInitialFail, EnumMacDrawerTransition.Initialing_InitialFail);
            MacTransition tInitialing_InitialTimeOut = NewTransition(sInitialIng, sInitialTimeOut, EnumMacDrawerTransition.Initialing_InitialTimeOut);

            /** Load(將 Tray 移到 定位~ Out的位置 )**/
            // 開始-進行中
            MacTransition tLoadMoveTrayToPositionOutStart_LoadMoveTrayToPositionOutIng = NewTransition(sLoadMoveTrayToPositionOutStart, sLoadMoveTrayToPositionOutIng, 
                                                                                         EnumMacDrawerTransition.LoadMoveTrayToPositionOutStart_LoadMoveTrayToPositionOutIng);
            // 進行中-完成
            MacTransition tLoadMoveTrayToPositionOutIng_LoadMoveTrayToPositionOutComplete = NewTransition(sLoadMoveTrayToPositionOutIng, sLoadMoveTrayToPositionOutComplete, 
                                                                                         EnumMacDrawerTransition.LoadMoveTrayToPositionOutIng_LoadMoveTrayToPositionOutComplete);
            // 進行中~逾時未完成
            MacTransition tLoadMoveTrayToPositionOutIng_LoadMoveTrayToPositionOutTimeOut = NewTransition(sLoadMoveTrayToPositionOutIng, sLoadMoveTrayToPositionOutTimeOut, 
                                                                                          EnumMacDrawerTransition.LoadMoveTrayToPositionOutIng_LoadMoveTrayToPositionOutTimeOut);
            // 進行中~失敗
            MacTransition tLoadMoveTrayToPositionOutIng_LoadMoveTrayToPositionOutFail = NewTransition(sLoadMoveTrayToPositionOutIng, sLoadMoveTrayToPositionOutFail, 
                                                                                          EnumMacDrawerTransition.LoadMoveTrayToPositionOutIng_LoadMoveTrayToPositionOutFail);
            // 完成~等待將Box 放入托盤
            MacTransition tLoadMoveTrayToPositionOutComplete_LoadIdleForPutBoxOnTrayAtPositionOut = NewTransition(sLoadMoveTrayToPositionOutIng, sIdleForPutBoxOnTrayAtPositionOut, 
                                                                                            EnumMacDrawerTransition.LoadMoveTrayToPositionOutComplete_IdleForPutBoxOnTrayAtPositionOut); 
            MacTransition tIdleForPutBoxOnTrayAtPositionOut_NULL= NewTransition(sIdleForPutBoxOnTrayAtPositionOut, null,
                                                                                            EnumMacDrawerTransition.IdleForPutBoxOnTrayAtPositionOut_NULL);


            /** Load (將 Tray 從 Out 移到 Home)**/
            // 開始->移動中
            MacTransition tLoadMoveTrayToPositionHomeStart_LoadMoveTrayToPositionHomeIng = NewTransition(sLoadMoveTrayToPositionHomeStart, sLoadMoveTrayToPositionHomeIng, 
                                                                                            EnumMacDrawerTransition.LoadMoveTrayToPositionHomeStart_LoadMoveTrayToPositionHomeIng);
            // 移動中->完成
            MacTransition tLoadMoveTrayToPositionHomeIng_LoadMoveTrayToPositionHomeComplete = NewTransition(sLoadMoveTrayToPositionHomeIng, sLoadMoveTrayToPositionHomeComplete, 
                                                                                           EnumMacDrawerTransition.LoadMoveTrayToPositionHomeIng_LoadMoveTrayToPositionHomeComplete);
            // 移動中->逾時未完成
            MacTransition tLoadMoveTrayToPositionHomeIng_LoadMoveTrayToPositionHomeTimeOut = NewTransition(sLoadMoveTrayToPositionHomeIng, sLoadMoveTrayToPositionHomeTimeOut, 
                                                                                           EnumMacDrawerTransition.LoadMoveTrayToPositionHomeIng_LoadMoveTrayToPositionHomeTimeOut);
            // 移動中-> 失敗
            MacTransition tLoadMoveTrayToPositionHomeIng_LoadMoveTrayToPositionHomeFail = NewTransition(sLoadMoveTrayToPositionHomeIng, sLoadMoveTrayToPositionHomeTimeOut, 
                                                                                           EnumMacDrawerTransition.LoadMoveTrayToPositionHomeIng_LoadMoveTrayToPositionHomeFail);

            /** Load(將 Tray 移到 Home之後,檢查Box**/
            // 移到Home=> 檢查
            MacTransition tLoadMoveTrayToPositionHomeComplete_LoadCheckBoxExistenceAtPositionHome = NewTransition(sLoadMoveTrayToPositionHomeComplete, sLoadCheckBoxExistenceAtPositionHome, 
                                                                                            EnumMacDrawerTransition.LoadMoveTrayToPositionHomeComplete_LoadCheckBoxExistenceAtPositionHome);
            // 檢查=> 有盒子 
            MacTransition tLoadCheckBoxExistenceAtPositionHome_LoadBoxExistAtPositionHome = NewTransition(sLoadCheckBoxExistenceAtPositionHome,sLoadBoxExistAtPositionHome, 
                                                                                            EnumMacDrawerTransition.LoadCheckBoxExistenceAtPositionHome_LoadBoxExistAtPositionHome);
            // 檢查=> 没有盒子
            MacTransition tLoadCheckBoxExistenceAtPositionHome_LoadBoxNotExistAtPositionHome = NewTransition(sLoadCheckBoxExistenceAtPositionHome, sLoadBoxExistAtPositionHome, 
                                                                                            EnumMacDrawerTransition.LoadCheckBoxExistenceAtPositionHome_LoadBoxNotExistAtPositionHome);
            // 檢查=> 逾時 
            MacTransition tLoadCheckBoxExistenceAtPositionHome_LoadCheckBoxExistenceAtPositionHomeTimeOut = NewTransition(sLoadCheckBoxExistenceAtPositionHome, sLoadBoxExistAtPositionHome, 
                                                                                            EnumMacDrawerTransition.LoadCheckBoxExistenceAtPositionHome_LoadCheckBoxExistenceAtPositionHomeTimeOut);

            // Load (檢查有盒子之後再回 LoadGotoHomeComplete)
            MacTransition tLoadBoxExistAtPositionHome_LoadMoveTrayToPositionHomeComplete = NewTransition(sLoadBoxExistAtPositionHome, sLoadMoveTrayToPositionHomeComplete, 
                                                                                            EnumMacDrawerTransition.LoadBoxExistAtPositionHome_LoadMoveTrayToPositionHomeComplete);
            // Load (檢查没有盒子後再回 LoadGotoHomeComplete)
            MacTransition tLoadBoxNotExistAtPositionHome_LoadMoveTrayToPositionHomeComplete = NewTransition(sLoadBoxNotExistAtPositionHome, sLoadMoveTrayToPositionHomeComplete, 
                                                                                            EnumMacDrawerTransition.LoadBoxNotExistAtPositionHome_LoadMoveTrayToPositionHomeComplete);
            /**到達 Home 之後, 經檢查没有盒子, 回退到 Out**/
            // 到達Home=> 回退到 Out 開始 
            MacTransition tLoadMoveTrayToPositionHomeComplete_LoadNoBoxRejectToPositionOutFromPositionHomeStart = NewTransition(sLoadMoveTrayToPositionHomeComplete, sLoadNoBoxRejectTrayToPositionOutFromPositionHomeStart,
                                                                                            EnumMacDrawerTransition.LoadMoveToPositionHomeComplete_LoadNoBoxRejectToPositionOutFromPositionHomeStart);
            // 回退到 Home 開始=>回退中
            MacTransition tLoadNoBoxRejectTrayToPositionOutFromPositionHomeStart_LoadNoBoxRejectTrayToPositionOutFromHomeIng =
                                                                                            NewTransition(sLoadNoBoxRejectTrayToPositionOutFromPositionHomeStart, sLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng, 
                                                                                            EnumMacDrawerTransition.LoadNoBoxRejectTrayToPositionOutFromPositionHomeStart_LoadNoBoxRejectTrayToPositionOutFromHomeIng);
            // 回退中=> 完成 
            MacTransition tLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng_LoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete =  
                                                                                           NewTransition(sLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng, sLoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete, 
                                                                                           EnumMacDrawerTransition.LoadNoBoxRejectTrayToPositionOutFromPositionHomeIng_LoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete);
            // 回退中=>失敗
            MacTransition tLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng_LoadNoBoxRejectTrayToPositionOutFromPositionHomeFail= 
                                                                                           NewTransition(sLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng, sLoadNoBoxRejectTrayToPositionOutFromPositionHomeFail, 
                                                                                           EnumMacDrawerTransition.LoadNoBoxRejectTrayToPositionOutFromPositionHomeIng_LoadNoBoxRejectTrayToOutFromPositionHomeFail);
            // 回退中 => 逾時 
            MacTransition tLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng_LoadNoBoxRejectTrayToPositionOutFromPositionHomeTimeOut = 
                                                                                           NewTransition(sLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng, sLoadNoBoxRejectTrayToPositionOutFromPositionHomeTimeOut, 
                                                                                           EnumMacDrawerTransition.LoadNoBoxRejectTrayToPositionOutFromPositionHomeIng_LoadNoBoxRejectTrayToPositionOutFromPositionHomeTimeOut);
            // 回退完成=>在 Out 等待放入Box
            MacTransition tLoadNoBoxRejectTrayToPositionOutFromPOsitionHomeComplete_IdleForPutBoxOnTrayAtPositionOut = 
                                                                                           NewTransition(sLoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete, sIdleForPutBoxOnTrayAtPositionOut, 
                                                                                           EnumMacDrawerTransition.LoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete_IdleForPutBoxOnTrayAtPositionOut);

            /** Load (將 Tray 從 Home 移到 In)**/
            // 到達 Home=> 開始移動
            MacTransition tLoadMoveTrayToPositionHomeComplete_LoadMoveTrayToPositionInStart = NewTransition(sLoadMoveTrayToPositionHomeComplete, sLoadMoveTrayToPositionInStart,
                                                                                           EnumMacDrawerTransition.LoadMoveTrayToPositionHomeComplete_LoadMoveTrayToPositionInStart);
            // 開始移動=> 移動中
            MacTransition tLoadMoveTrayToPositionInStart_LoadMoveTrayToPositionInIng = NewTransition(sLoadMoveTrayToPositionInStart, sLoadMoveTrayToPositionInIng, 
                                                                                            EnumMacDrawerTransition.LoadMoveTrayToPositionInStart_LoadMoveTrayToPositionInIng);
            // 移動中=>完成
            MacTransition tLoadMoveTrayToPositionInIng_LoadMoveTrayToPositionInComplete = NewTransition(sLoadMoveTrayToPositionInIng, sLoadMoveTrayToPositionInComplete, 
                                                                                            EnumMacDrawerTransition.LoadMoveTrayToPositionInIng_LoadMoveTrayToPositionInComplete);
            // 移動中=> 逾時未完成
            MacTransition tLoadMoveTrayToPositionInIng_LoadMoveTrayToPositionInTimeOut = NewTransition(sLoadMoveTrayToPositionInIng, sLoadMoveTrayToPositionInTimeOut, 
                                                                                           EnumMacDrawerTransition.LoadMoveTrayToPositionInIng_LoadMoveTrayToPositionInTimeOut);

            // 移動中=> 移動失敗
            MacTransition tLoadMoveTrayToPositionInIng_LoadMoveTrayToPositionInFail = NewTransition(sLoadMoveTrayToPositionInIng, sLoadMoveTrayToPositionInFail, 
                                                                                          EnumMacDrawerTransition.LoadMoveTrayToPositionInIng_LoadMoveTrayToPositionInFail);
            // 完成=> 等待夾走
            MacTransition tLoadMoveTrayToPositionInComplete_IdleForGetBoxOnTrayAtPositionIn = NewTransition(sLoadMoveTrayToPositionInComplete, sIdleForGetBoxOnTrayAtPositionIn, 
                                                                                          EnumMacDrawerTransition.LoadMoveTrayToPositionInComplete_IdleForGetBoxOnTrayAtPositionIn);
            // 等待夾走
            MacTransition tIdleForGetBoxOnTrayAtPositionIn_NULL = NewTransition(sIdleForGetBoxOnTrayAtPositionIn, null,
                                                                                          EnumMacDrawerTransition.IdleForGetBoxOnTrayAtPositionIn_NULL);

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
                 Func<StateGuardRtns> guard = () =>
                 {
                     StateGuardRtns rtn = new StateGuardRtns
                     {
                         Transition = tInitialStart_InitialIng,
                         ThisStateExitEventArgs=new MacStateExitEventArgs(),
                         NextStateEntryEventArgs=new MacStateEntryEventArgs(null),
                     };
                     return rtn;
                 };
                 Action<object> action = (parameter) =>
                 {
                     HalDrawer.CommandINI(); 
                 };
                 Action<Exception> exceptionHandler = (ex) =>
                 {
                     throw ex;
                 };
                 object actionParameter = null;
                 Trigger(guard, action, actionParameter, exceptionHandler);
             };
            sInitialStart.OnExit += (sender, e) =>
            {
               // Nothing
            };

            sInitialIng.OnEntry += (sender, e) =>
            {
                Func<DateTime, StateGuardRtns> guard = (startTime) =>
                {
                    StateGuardRtns rtn = null;
                    if(HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome)
                    {
                        rtn = new StateGuardRtns
                        {
                            Transition = tInitialing_InitialComplete,
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                        };
                    }
                    else if(HalDrawer.CurrentWorkState == DrawerWorkState.InitialFailed)
                    {
                        throw new StateFailException();
                    }
                    else   if (timeoutObj.IsTimeOut(startTime))
                    {
                        throw new StateTimeoutException();
                    }
                    return rtn;
                };
                Action<Exception> exceptionHandler = (ex) => 
                { throw ex; };
                Action<object> action = null;
                object actionParemeter = null;
                TriggerAsync(guard, action, actionParemeter, exceptionHandler);
                              
            };
            sInitialIng.OnExit += (sender, e) =>
            {
               // Nothing 
            };

            sInitialComplete.OnEntry += (sender, e)=>
            {
                Func<StateGuardRtns> guard = () =>
                {
                    return new StateGuardRtns
                    {
                        NextStateEntryEventArgs = null,
                        ThisStateExitEventArgs = new MacStateExitEventArgs(),
                        Transition = tInitialComplete_NULL
                    };
                };
                Action<object> action = null;
                object actionParameter = null;
                Action<Exception> exceptionHandler = null;
                Trigger(guard, action, actionParameter, exceptionHandler);

            };
            
            sInitialComplete.OnExit += (sender, e) =>
            {
               // Nothing
            };

            // 初始化逾時, throw an Exception
            sInitialTimeOut.OnEntry += (sender, e) =>
            {
                
            };
            // 初始化逾時, throw an Exception 
            sInitialTimeOut.OnExit += (sender, e) =>
            {
                
            };
            // 初始化失敗, throw an Exception
            sInitialFail.OnEntry += (sender, e) =>
            {
                
            };
            // 初始化失敗, throw an Exception
            sInitialFail.OnExit += (sender, e) =>
            {
                
            };

            sLoadMoveTrayToPositionOutStart.OnEntry += (sender, e) =>
            {
                Action<Exception> exceptionHandler = (ex) =>
                {
                    throw ex;
                };
                Action<object> action = (parameter) =>
                {
                    HalDrawer.CommandTrayMotionOut();
                };
                Func<StateGuardRtns> guard = () =>
                {
                    var rtnV = new StateGuardRtns
                    {
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        ThisStateExitEventArgs = new MacStateExitEventArgs(),
                        Transition=tLoadMoveTrayToPositionOutStart_LoadMoveTrayToPositionOutIng,
                    };
                    return rtnV;
                };
                object actionParameter = null; 
                this.Trigger(guard, action, actionParameter, exceptionHandler);
            };
            sLoadMoveTrayToPositionOutStart.OnExit += (sender, e) =>
            {
                // Nothing
            };
            

            sLoadMoveTrayToPositionOutIng.OnEntry += (sender, e) =>
            {
                Action<Exception> exceptionHandler=(ex)=>
                {
                    throw ex;
                };
                
                Func<DateTime , StateGuardRtns> guard = (startTime) =>
                {
                    StateGuardRtns rtn = null;
                    if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionOut)
                    {// Complete
                        rtn = new StateGuardRtns
                        {
                            Transition = tLoadMoveTrayToPositionOutIng_LoadMoveTrayToPositionOutComplete,
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                            ThisStateExitEventArgs = new MacStateExitEventArgs()
                        };
                        
                    }
                    else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                    {   // Failed
                        throw new StateFailException();
                    }
                    else if (timeoutObj.IsTimeOut(startTime))
                    {    // Time Out
                         throw new StateTimeoutException();
                    }
                  
                    return rtn;
                };
                Action<object> action = null;
                object actionParameter = null;
                this.TriggerAsync(guard, action, actionParameter, exceptionHandler);
            };
            sLoadMoveTrayToPositionOutIng.OnExit += (sender, e) =>
            {
               // Nothing
            };
        
            sLoadMoveTrayToPositionOutComplete.OnEntry += (sender, e) =>
            {
                Func<StateGuardRtns> guard = () =>
                  {
                      StateGuardRtns rtn = new StateGuardRtns
                      {
                          NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                          Transition = tLoadMoveTrayToPositionOutComplete_LoadIdleForPutBoxOnTrayAtPositionOut,
                          ThisStateExitEventArgs = new MacStateExitEventArgs(),
                      };
                      return rtn;
                  };
                Action<object> action = null;
                object actionParameter = null;
                Action<Exception> exceptionHandler = null;
                this.Trigger(guard,action,actionParameter,exceptionHandler);
            };
            sLoadMoveTrayToPositionOutComplete.OnExit += (sender, e) =>
            {
                // Nothing
            };
           
            sIdleForPutBoxOnTrayAtPositionOut.OnEntry += (sender, e) =>
              {
                  Func<StateGuardRtns> guard = () =>
                  {
                      StateGuardRtns rtn = new StateGuardRtns
                      {
                          NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                          ThisStateExitEventArgs = new MacStateExitEventArgs(),
                          Transition = tIdleForPutBoxOnTrayAtPositionOut_NULL
                      };
                      return rtn;
                  };
                  Action<object> action = null;
                  object actionParameter = null;
                  Action<Exception> ExceptionHandler = null;
                  this.Trigger(guard,action,actionParameter,ExceptionHandler);

              };
            sIdleForPutBoxOnTrayAtPositionOut.OnExit += (sender, e) =>
            {
                // Nothing

            };

            // 移動失敗(throw an Exception )
            sLoadMoveTrayToPositionOutFail.OnEntry += (sender, e) =>
            {
               
            };
            sLoadMoveTrayToPositionOutFail.OnExit += (sender, e) =>
            {
                
            };

            // 移動逾時(throw an Exception )
            sLoadMoveTrayToPositionOutTimeOut.OnEntry += (sender, e) =>
            {
                
                
            };
            sLoadMoveTrayToPositionOutTimeOut.OnExit += (sender, e) =>
            {
                
            };

            // 
            sLoadMoveTrayToPositionHomeStart.OnEntry += (sender,e) =>
            {
                Func<StateGuardRtns> guard = () =>
                {
                    var rtn = new StateGuardRtns
                    {
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        ThisStateExitEventArgs=new MacStateExitEventArgs(),
                        Transition=tLoadMoveTrayToPositionHomeStart_LoadMoveTrayToPositionHomeIng,
                    };
                    return rtn;
                };
                Action<object> action = (parameter)=>
                {
                    HalDrawer.CommandTrayMotionHome();
                };
                object actionParameter = null;
                Action<Exception> exceptionHandler = (ex) =>
                {
                    throw ex;
                };
                this.Trigger(guard,action,actionParameter,exceptionHandler);
                //var state = (MacState)sender;
               // HalDrawer.CommandTrayMotionHome();
               // state.DoExit(new MacStateExitEventArgs());
            };
            sLoadMoveTrayToPositionHomeStart.OnExit += (sender, e) =>
            {
               // Nothing
            };

            sLoadMoveTrayToPositionHomeIng.OnEntry += (sender, e) =>
            {
                Func<DateTime, StateGuardRtns> guard = (startTime) =>
                {
                    StateGuardRtns rtn = null;
                    if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome)
                    {
                        rtn = new StateGuardRtns
                        {
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(EnumMacDrawerLoadToHomeCompleteSource.MoveTrayToPositionHome),
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                            Transition = tLoadMoveTrayToPositionInIng_LoadMoveTrayToPositionInComplete
                        };
                    }
                    else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                    {
                        throw new StateFailException();
                    }
                    else if (timeoutObj.IsTimeOut(startTime))
                    {
                        throw new StateTimeoutException();
                    }
                    return rtn;
                };
                Action<object> action = null;
                object actionParameter = null;
                Action<Exception> exceptionHandler = (ex) =>
                {
                    throw ex;
                };
                this.TriggerAsync(guard, action, actionParameter, exceptionHandler);
                /** ori
                Action guard = () =>
                {
                    while (true)
                    {
                        MacTransition transition=null;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome)
                        {  // Complete
                            transition = tLoadMoveTrayToPositionHomeIng_LoadMoveTrayToPositionHomeComplete;// dicTransition[EnumMacDrawerTransition.LoadGotoHomeIng_LoadGotoHomeComplete.ToString()];
                        }
                        else if (timeoutObj.IsTimeOut(thisTime))
                        {  // 逾時
                            
                            transition = tLoadMoveTrayToPositionHomeIng_LoadMoveTrayToPositionHomeTimeOut;

                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            transition = tLoadMoveTrayToPositionHomeIng_LoadMoveTrayToPositionHomeFail;
                        }
                        if (transition != null)
                        {
                            var eventArgs = new MacStateExitWithTransitionEventArgs(transition);
                            state.DoExit(eventArgs);
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };*/

            };
            sLoadMoveTrayToPositionHomeIng.OnExit += (sender, e) =>
              {
                // Nothing
              };
            
            sLoadMoveTrayToPositionHomeComplete.OnEntry += (sender, e) =>
            {
                // Load 時, 在 Home 位置
                // 1. 由 Out 進入本狀態,
                // 2. 由 Out 進入本狀態=> 檢查有盒子=> 回到本狀態
                // 3. 由 Out 進入本狀態=> 檢查没有盒子=>回到本狀態
                var eventArgs = (MacStateEntryEventArgs)e;
                var src = (EnumMacDrawerLoadToHomeCompleteSource)(eventArgs.Parameter);
                Func<StateGuardRtns> guard = null;
                Action<object> action = null;
                Action<object> actionFromCheckHasBox = null;
                Action<object> actionFromCheckHasNoBox = null;
                Action<object> actionFromHomneIng =(parameter)=> { HalDrawer.CommandBoxDetection(); };
                object actionParameter = null;
                object actionSrcFromCheckHasBoxParameter = null;
                object actionSrcFromCheckHasNoBoxParameter = null;
                object actionSrcFromMoveToPositionHomeIng = null;
                Action<Exception> exceptionHandler = null;
                Action<Exception> exceptionHandlerFromHomIng = (ex)=>{ throw ex; };
                Action<Exception> exceptionHandlerFromCheckHasNoBox = (ex) => { throw ex; };
                Action<Exception> exceptionHandlerFromCheckHasBox = (ex) => { throw ex; };
                Func<StateGuardRtns> guardFromHomeIng = () =>
                {  // 從  MoveTrayToPositionHomeIng 進來
                    StateGuardRtns rtn = null;
                    rtn = new StateGuardRtns
                    {
                            NextStateEntryEventArgs=new MacStateEntryEventArgs(null),
                            ThisStateExitEventArgs=new MacStateExitEventArgs(),
                            // 下個 State: 檢查盒子在不在
                            Transition = tLoadMoveTrayToPositionHomeComplete_LoadCheckBoxExistenceAtPositionHome,
                    };
                   return rtn;
                };
                Func<StateGuardRtns> guardFromFromCheckHasNoBox = () =>
                {
                     StateGuardRtns rtn = null;
                    // 檢查完, 盒子不存在
                    rtn = new StateGuardRtns
                        {
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                            // 下個 State: 將 Tray 回退到 Out
                            Transition = tLoadMoveTrayToPositionHomeComplete_LoadNoBoxRejectToPositionOutFromPositionHomeStart,
                        };
                      return rtn;
                };
                Func<StateGuardRtns> guardFromFromCheckHasBox = () =>
                {
                    StateGuardRtns rtn = null;
                    // 從 GomeIng 到 GotoHomeComplete=> 去檢查合子在不在
                    rtn = new StateGuardRtns
                     {
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                            // 下個 State: 將 Tray 送到 Position In 
                            Transition=tLoadMoveTrayToPositionHomeComplete_LoadMoveTrayToPositionInStart,
                    };
                   return rtn;
                };
                if(src == EnumMacDrawerLoadToHomeCompleteSource.MoveTrayToPositionHome)
                {
                    guard = guardFromHomeIng;
                    action = actionFromHomneIng;
                    actionParameter = actionSrcFromMoveToPositionHomeIng;
                    exceptionHandler = exceptionHandlerFromHomIng;
                }
                else if(src==EnumMacDrawerLoadToHomeCompleteSource.LoadCheckBoxExist)
                {
                    guard = guardFromFromCheckHasBox;
                    action = actionFromCheckHasBox;
                    actionParameter = actionSrcFromCheckHasBoxParameter;
                    exceptionHandler = exceptionHandlerFromCheckHasBox;
                }
                else //if(src== EnumMacDrawerLoadToHomeCompleteSource.LoadCheckBoxNotExist)
                {
                    guard = guardFromFromCheckHasNoBox;
                    action = actionFromCheckHasNoBox;
                    actionParameter = actionSrcFromCheckHasNoBoxParameter;
                    exceptionHandler = exceptionHandlerFromCheckHasNoBox;
                }
                Trigger(guard, action, actionParameter, exceptionHandler);                
                /** Original
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
                        transition = tLoadMoveTrayToPositionHomeComplete_LoadNoBoxRejectToPositionOutFromPositionHomeStart;

                    }
                    else if (source == EnumMacDrawerLoadToHomeCompleteSource.LoadCheckBoxExist)
                    {    // 檢查完盒子存在=> 將盒子移到 Out 
                        transition = tLoadMoveTrayToPositionHomeComplete_LoadMoveTrayToPositionInStart;
                    }
                }
                
                state.DoExit(new MacStateExitWithTransitionEventArgs(transition));
                */
            };
            sLoadMoveTrayToPositionHomeComplete.OnExit += (sender, e) =>
            {
              // Nothing
                
            };

            // 逾時, Throw an Exception
            sLoadMoveTrayToPositionHomeTimeOut.OnEntry += (sender, e)=>
            {
                
            };
            sLoadMoveTrayToPositionHomeTimeOut.OnExit += (sender, e) =>
            {
                
            };
            
            // 失敗, Throw an Exception
            sLoadMoveTrayToPositionHomeFail.OnEntry += (sender, e) =>
            {
                
            };
            sLoadMoveTrayToPositionHomeFail.OnExit += (sender, e) =>
            {
                
            };
            // Load 時, 在 Home 點檢查有没有盒子
            sLoadCheckBoxExistenceAtPositionHome.OnEntry += (sender, e) =>
            {

                Action<object> action = null;
                object actiontParameter = null;
                Action<Exception> exceptionHandler = (ex) => { throw ex; };
                Func<DateTime, StateGuardRtns> guard = (startTime) =>
                 {
                     StateGuardRtns rtn = null;
                     if (DrawerWorkState.BoxNotExist == HalDrawer.CurrentWorkState)
                     {   // 没有盒子 
                         rtn = new StateGuardRtns
                         {
                              NextStateEntryEventArgs=new MacStateEntryEventArgs(null),
                              ThisStateExitEventArgs=new MacStateExitEventArgs(),
                             Transition =tLoadCheckBoxExistenceAtPositionHome_LoadBoxNotExistAtPositionHome
                         };
                     }
                     else if (DrawerWorkState.BoxExist == HalDrawer.CurrentWorkState)
                     {   // 有盒子 
                         rtn = new StateGuardRtns
                         {
                             NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                             ThisStateExitEventArgs = new MacStateExitEventArgs(),
                             Transition =tLoadCheckBoxExistenceAtPositionHome_LoadBoxExistAtPositionHome
                         };
                     }
                     else if (timeoutObj.IsTimeOut(startTime))
                     {   // 逾時
                         throw new StateTimeoutException();
                     }
                     return rtn;
                 };
                this.TriggerAsync(guard, action, actiontParameter, exceptionHandler);
                /** Original
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
                            transition = tLoadCheckBoxExistenceAtPositionHome_LoadBoxExistAtPositionHome;
                        }
                        else if(HalDrawer.CurrentWorkState == DrawerWorkState.BoxNotExist)
                        {   // 没盒子
                            transition = tLoadCheckBoxExistenceAtPositionHome_LoadBoxNotExistAtPositionHome;
                        }
                        else if(new MacDrawerStateTimeOutController().IsTimeOut(startTime))
                        {   // 逾時
                            transition = tLoadCheckBoxExistenceAtPositionHome_LoadCheckBoxExistenceAtPositionHomeTimeOut;
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
                 */
            };
            sLoadCheckBoxExistenceAtPositionHome.OnExit += (sender, e) =>
            {
                /** Nothing
                var args = (MacStateExitWithTransitionEventArgs)e;
                var nextState = args.Transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
               */
            };

            

            // Load 時在 HOME 點檢查有盒子
            sLoadBoxExistAtPositionHome.OnEntry += (sender, e) =>
            {
                Func<StateGuardRtns> guard = () =>
                {
                    StateGuardRtns rtn = new StateGuardRtns
                    {
                        NextStateEntryEventArgs =new  MacStateEntryEventArgs(EnumMacDrawerLoadToHomeCompleteSource.LoadCheckBoxExist),
                        ThisStateExitEventArgs = new MacStateExitEventArgs(),
                        Transition = tLoadBoxExistAtPositionHome_LoadMoveTrayToPositionHomeComplete
                    };
                    return rtn;
                };
                Action<object> action = null;
                object actionParameter = null;
                Action<Exception> exceptionHandler = (ex) => { throw ex; };
                this.Trigger(guard, action, actionParameter, exceptionHandler);
                /** Original
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
                */
            };
            sLoadBoxExistAtPositionHome.OnExit += (sender, e) =>
            {
                /** Nothing
                //var transition = tLoadBoxExistAtHome_LoadGotoHomeComplete;
                var nextState = tLoadBoxExistAtPositionHome_LoadMoveTrayToPositionHomeComplete.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(EnumMacDrawerLoadToHomeCompleteSource.LoadCheckBoxExist));
                */        
            };

            // Load 時在 Home 點檢查没盒子
            sLoadBoxNotExistAtPositionHome.OnEntry += (sender, e) =>
            {
                Func<StateGuardRtns> guard = () =>
                {
                    StateGuardRtns rtn = new StateGuardRtns
                    {
                        Transition = tLoadBoxNotExistAtPositionHome_LoadMoveTrayToPositionHomeComplete,
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(EnumMacDrawerLoadToHomeCompleteSource.LoadCheckBoxNotExist),
                        ThisStateExitEventArgs=new MacStateExitEventArgs(),
                    };
                    return rtn; 
                };
                Action<object> action = default(Action<object>);
                object actionParameter = default(object);
                Action<Exception> exceptionHandler = (ex) => { throw ex; };
                this.Trigger(guard,action,actionParameter,exceptionHandler);
                /** Original
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
                */
            };
            sLoadBoxNotExistAtPositionHome.OnExit += (sender, e) =>
            {
              
                /** Nothing
                //var transition = tLoadBoxNotExistAtHome_LoadGotoHomeComplete;
                var nextState = tLoadBoxNotExistAtPositionHome_LoadMoveTrayToPositionHomeComplete.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(EnumMacDrawerLoadToHomeCompleteSource.LoadCheckBoxNotExist));
                */
            };

            // Load 時在 Home 點檢查有没有盒子, 逾時無結果 to Throw an Exception 
            sLoadCheckBoxExistenceAtPositionHomeTimeOut.OnEntry += (sender, e) =>
            {
                
                /** Original
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
                */
            };
            sLoadCheckBoxExistenceAtPositionHomeTimeOut.OnExit += (sender, e) =>
            {
                
                // TODO: Tray 到達 Home 之後, 檢查Box 是否存在時, 逾時
            };

            // Load 時, 在 Home 點檢查没有盒子, 將 Tray 回退到 Home, 開始
            sLoadNoBoxRejectTrayToPositionOutFromPositionHomeStart.OnEntry += (sender, e) =>
            {
                Func<StateGuardRtns> guard = () =>
                {
                    StateGuardRtns rtn = new StateGuardRtns
                    {
                        Transition = tLoadNoBoxRejectTrayToPositionOutFromPositionHomeStart_LoadNoBoxRejectTrayToPositionOutFromHomeIng,
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                         ThisStateExitEventArgs=new MacStateExitEventArgs(),
                    };
                    return rtn;
                };
                Action<object> action = (parameter) =>
                {
                    HalDrawer.CommandTrayMotionOut();
                };
                object actionParameter = null;
                Action<Exception> exceptionHandler = (ex) => { throw ex; };
                this.Trigger(guard, action, actionParameter, exceptionHandler);
                /** Original
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
                 */
            };
            sLoadNoBoxRejectTrayToPositionOutFromPositionHomeStart.OnExit += (sender, e) =>
            {
                /**  Nothing
                var nextState = tLoadNoBoxRejectTrayToPositionOutFromPositionHomeStart_LoadNoBoxRejectTrayToPositionOutFromHomeIng.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
                */
            };

            // Load 時, 在Home 點檢查時没有盒子,  將Tray 回退到 Out,Tray 移動中
            sLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng.OnEntry += (sender, e) =>
            {
                Func<DateTime, StateGuardRtns> guard=(startTime) =>
                {
                    StateGuardRtns rtn = null;
                    if (HalDrawer.CurrentWorkState==DrawerWorkState.TrayArriveAtPositionOut )
                    {
                        rtn = new StateGuardRtns
                        {
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                            Transition = tLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng_LoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete,
                        };
                    }
                    else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                    {
                        throw new StateFailException();
                    }
                    else if (timeoutObj.IsTimeOut(startTime))
                    {
                        throw new StateTimeoutException();
                    }
                    return rtn;
                };
                Action<object> action = default(Action<object>);
                object actionParameter = default(object);
                Action<Exception> exceptionHandler = ( ex) =>
                {
                      throw ex;
                };
                this.TriggerAsync(guard,action,actionParameter,exceptionHandler);
                
                /** Original
                var state = (MacState)sender;
                DateTime startTime = DateTime.Now;
                Action guard = () =>
                {
                    MacTransition transition = null;
                    while (true)
                    {
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            transition = tLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng_LoadNoBoxRejectTrayToPositionOutFromPositionHomeFail;
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionIn)
                        {
                            transition = tLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng_LoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete;
                        }
                        else if(new MacDrawerStateTimeOutController().IsTimeOut(startTime))
                        {
                            transition = tLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng_LoadNoBoxRejectTrayToPositionOutFromPositionHomeTimeOut;
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
                HalDrawer.CommandTrayMotionIn();*/
            };
            sLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng.OnExit += (sender, e) =>
            {
                /** Nothing
                var nextState = ((MacStateExitWithTransitionEventArgs)e).Transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
                */
            };

            // Load 時, 在 Home 點檢查没有盒子, 將 Tray 回退到 Out, 完成
            sLoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete.OnEntry += (sender, e) =>
            {
                Func<StateGuardRtns> guard = () =>
                {
                    StateGuardRtns rtn = new StateGuardRtns
                    {
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        ThisStateExitEventArgs=new MacStateExitEventArgs(),
                        Transition =tLoadNoBoxRejectTrayToPositionOutFromPOsitionHomeComplete_IdleForPutBoxOnTrayAtPositionOut,
                    };
                    return rtn;
                };
                Action<object> action = default(Action<object>);
                object actionParameter = default(object);
                Action<Exception> exceptionHandler = default(Action<Exception>);
                this.Trigger(guard,action,actionParameter,exceptionHandler);

                /** Original
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs() );
                */
            };
            sLoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete.OnExit += (sender, e) =>
            {
                /** Nothing
                var nextState = tLoadNoBoxRejectTrayToPositionOutFromPOsitionHomeComplete_IdleForPutBoxOnTrayAtPositionOut.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
                */
            };

            // Load, 没有盒子時無法回退到 Out, to Throw an Exception 
            sLoadNoBoxRejectTrayToPositionOutFromPositionHomeFail.OnEntry += (sender, e) =>
            {
                /** Original
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
               */        
            };
            sLoadNoBoxRejectTrayToPositionOutFromPositionHomeFail.OnExit += (sender, e) =>
            {
                // Load 時, Tray 移到 Home 檢查没有Box, 回退到 In 時 無法移動
                // 此為 無法移動的最後一個狀態
            };

            // Load, 没有盒子回退逾時, to throw an Exception
            sLoadNoBoxRejectTrayToPositionOutFromPositionHomeTimeOut.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sLoadNoBoxRejectTrayToPositionOutFromPositionHomeTimeOut.OnExit += (sender, e) =>
            {
                // Load 時, Tray 移到 Home 檢查没有Box, 回退到 In 時 逾時未到達 In
                // 此為 逾時的最後一個狀態
            };

            // Load 時, 檢查 有盒子之後, 將Tray 移到  Position In, 動作開始
            sLoadMoveTrayToPositionInStart.OnEntry += (sender, e) =>
            {
                Action<object> action = (parameter) =>
                {
                    HalDrawer.CommandTrayMotionOut();
                };
                Func<StateGuardRtns> guard = () =>
                {
                    StateGuardRtns rtn = new StateGuardRtns
                    {
                        Transition = tLoadMoveTrayToPositionInStart_LoadMoveTrayToPositionInIng,
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        ThisStateExitEventArgs=new MacStateExitEventArgs(),
                    };
                    return rtn;
                };
                object actionParameter = null;
                Action<Exception> exceptionHandler = null;
                Trigger(guard, action, actionParameter, exceptionHandler);

                /** Original
                  var state = (MacState)sender;
                  HalDrawer.CommandTrayMotionOut();
                  state.DoExit(new MacStateExitEventArgs());
               */
            };
            sLoadMoveTrayToPositionInStart.OnExit += (sender, e) =>
            {
              /** Nothing
                // var transition = this.Transitions[EnumMacDrawerTransition.LoadGotoOutStart_LoadGotoOutIng.ToString()];
                var nextState = tLoadMoveTrayToPositionInStart_LoadMoveTrayToPositionInIng.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
               */
            };

            // Load 時, 檢查 有盒子之後, 將Tray 移到  Position In, 動作中 
            sLoadMoveTrayToPositionInIng.OnEntry += (sender, e) =>
            {
                Func<DateTime, StateGuardRtns> guard = (startTime) =>
                 {
                     StateGuardRtns rtn = null;
                     if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionIn)
                     {
                         rtn = new StateGuardRtns
                         {
                             NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                             ThisStateExitEventArgs = new MacStateExitEventArgs(),
                             Transition = tLoadMoveTrayToPositionInIng_LoadMoveTrayToPositionInComplete,
                         };
                         
                     }
                     else if(HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                     {
                         throw new StateFailException();
                     }
                     else if (timeoutObj.IsTimeOut(startTime))
                     {
                         throw new StateTimeoutException();
                     }
                     return rtn;
                 };
                Action<object> action = null;
                object actionParameter = null;
                Action<Exception> exceptionHandler = (ex) => { throw ex; };
                this.TriggerAsync(guard, action, actionParameter, exceptionHandler);
                /** Original
                var state = (MacState)sender;
                DateTime thisTime = DateTime.Now;
               // var dicTransition = this.Transitions;
                Action guard = () =>
                {
                    while (true)
                    {
                        MacTransition transition = null;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionOut)
                        {   // 完成
                            transition = tLoadMoveTrayToPositionInIng_LoadMoveTrayToPositionInComplete;// dicTransition[EnumMacDrawerTransition.LoadGotoOutIng_LoadGotoOutComplete.ToString()];
                        }
                        else if (timeoutObj.IsTimeOut(thisTime))
                        {   // 逾時
                            transition = tLoadMoveTrayToPositionInIng_LoadMoveTrayToPositionInTimeOut;// dicTransition[EnumMacDrawerTransition.LoadGotoOutIng_LoadGotoOutTimeOut.ToString()];
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {   // 無法完成
                            transition = tLoadMoveTrayToPositionInIng_LoadMoveTrayToPositionInFail;// dicTransition[EnumMacDrawerTransition.LoadGotoOutIng_LoadGotoOutFail.ToString()];
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
              */
            };
            sLoadMoveTrayToPositionInIng.OnExit += (sender, e) =>
            {
                /** Original, Nothing
                var args = (MacStateExitWithTransitionEventArgs)e;
                var nextState = args.Transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
               */        
            };

            sLoadMoveTrayToPositionInComplete.OnEntry += (sender, e) =>
            {
                Func<StateGuardRtns> guard = () =>
                {
                    StateGuardRtns rtn = new StateGuardRtns
                    {
                        Transition = tLoadMoveTrayToPositionInComplete_IdleForGetBoxOnTrayAtPositionIn,
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        ThisStateExitEventArgs=new MacStateExitEventArgs()

                    };
                    return rtn;
                };
                Action<object> action = null;
                object actionParameter = null;
                Action<Exception> exceptionHandler = null;
                this.Trigger(guard,action,actionParameter,exceptionHandler);
                /** Original
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
               */
            };
            sLoadMoveTrayToPositionInComplete.OnExit += (sender, e) =>
            {
               /** Original, Nothing
                // var transition = this.Transitions[EnumMacDrawerTransition.LoadGotoOutComplete_IdleForGetBoxOnTrayAtOut.ToString()];
                var nextState = tLoadMoveTrayToPositionInComplete_IdleForGetBoxOnTrayAtPositionIn.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
                */
            };
            sIdleForGetBoxOnTrayAtPositionIn.OnEntry += (sender, e) =>
            {
                Func<StateGuardRtns> guard = () =>
                {
                    var rtn = new StateGuardRtns
                    {
                        Transition = tIdleForGetBoxOnTrayAtPositionIn_NULL,
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                         ThisStateExitEventArgs=new MacStateExitEventArgs(),
                    };
                    return rtn; ;
                };
                Action<object> action = null;
                object actionParameter = null;
                Action<Exception> exceptionHandler = (ex) =>
                {
                    throw ex;
                };
                Trigger(guard, action, actionParameter, exceptionHandler);
                
            };
            sIdleForGetBoxOnTrayAtPositionIn.OnExit += (sender, e) =>
            {
                //  Nothing, Load  Tray 到達 Out 位置, 等待將盒子取走 
            };

            // Load, 移到 Position 時逾時, Throw an Exception
            sLoadMoveTrayToPositionInTimeOut.OnEntry += (sender, e) =>
            {
                /** Original
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
                */
            };
            sLoadMoveTrayToPositionInTimeOut.OnExit += (sender, e) =>
            {
                // Nothing 
            };

            // Load, 無法移動 Tray 到 Position In, Throw an Exception
            sLoadMoveTrayToPositionInFail.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sLoadMoveTrayToPositionInFail.OnExit += (sender, e) =>
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
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionOut)
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
                         if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionIn)
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
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionIn)
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
