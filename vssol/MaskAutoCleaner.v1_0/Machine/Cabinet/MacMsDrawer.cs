using MaskAutoCleaner.v1_0.Machine.StateExceptions;
using MaskAutoCleaner.v1_0.StateMachineBeta;
using MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException;
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
        public void Load_MoveTrayToPositionInFromPositionOut()
        {
            HalDrawer.SetDrawerWorkState(DrawerWorkState.MoveTrayToPositionHomeStart);
            this.States[EnumMacDrawerState.LoadMoveTrayToPositionHomeStart.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }

      

        public void Unload_MoveTrayToPositionInFromAnywhere()
        {
            HalDrawer.SetDrawerWorkState(DrawerWorkState.MoveTrayToPositionOutStart);
            //this.States[EnumMacDrawerState.LoadMoveTrayToPositionOutStart.ToString()].DoEntry(new MacStateEntryEventArgs(null));
            this.States[EnumMacDrawerState.UnloadMoveTrayToPositionInStart.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }
        
        public void Unload_MoveTrayToPositionOutFromPositionIn()
        {
            HalDrawer.SetDrawerWorkState(DrawerWorkState.MoveTrayToPositionInStart);
            this.States[EnumMacDrawerState.UnloadMoveTrayToPositionHomeStart.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }


        #endregion State Instruction
        public override void LoadStateMachine()
        {
            #region State
            /** Initial **/
            //  開始
            MacState sInitialStart = NewState(EnumMacDrawerState.InitialStart);
            // 
            //  進行中
            MacState sInitialIng = NewState(EnumMacDrawerState.InitialIng);
            // 完成 
            MacState sInitialComplete = NewState(EnumMacDrawerState.InitialComplete);
       


            /** Load, 將托盤移到 Out**/
            
            
            // 開始
            MacState sLoadMoveTrayToPositionOutStart = NewState(EnumMacDrawerState.LoadMoveTrayToPositionOutStart);
            // 進行中 
            MacState sLoadMoveTrayToPositionOutIng = NewState(EnumMacDrawerState.LoadMoveTrayToPositionOutIng);
            // 完成
            MacState sLoadMoveTrayToPositionOutComplete = NewState(EnumMacDrawerState.LoadMoveTrayToPositionOutComplete);
            
            // 等待放盒子
            MacState sIdleForPutBoxOnTrayAtPositionOut = NewState(EnumMacDrawerState.IdleForPutBoxOnTrayAtPositionOut);

            /** Load,將托盤移到 Home **/
            // 開始
            MacState sLoadMoveTrayToPositionHomeStart = NewState(EnumMacDrawerState.LoadMoveTrayToPositionHomeStart);
            // 進行中
            MacState sLoadMoveTrayToPositionHomeIng = NewState(EnumMacDrawerState.LoadMoveTrayToPositionHomeIng);
            // 完成
            MacState sLoadMoveTrayToPositionHomeComplete = NewState(EnumMacDrawerState.LoadMoveTrayToPositionHomeComplete);
          


            // Load, 在HOme 點檢查Tray 上是否有盒子
            MacState sLoadCheckBoxExistenceAtPositionHome = NewState(EnumMacDrawerState.LoadCheckBoxExistenceAtPositionHome);
            // Load, 在Home 點檢查 是否有盒子=> 有, 2020/08/03 King Liu Add New 
            MacState sLoadBoxExistAtPositionHome = NewState(EnumMacDrawerState.LoadBoxExistAtPositionHome);
            // Load, 在Home 點查 是否有 盒子=> 没有, 2020/08/03 King Liu Add New 
            MacState sLoadBoxNotExistAtPositionHome = NewState(EnumMacDrawerState.LoadBoxNotExistAtPositionHome);
          
            /**Load, 在Home時被檢出没有盒子, 回退到Out**/
            // 開始
            MacState sLoadNoBoxRejectTrayToPositionOutFromPositionHomeStart = NewState(EnumMacDrawerState.LoadNoBoxRejectTrayToPositionOutFromPositionHomeStart);
            // 進行中
            MacState sLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng = NewState(EnumMacDrawerState.LoadNoBoxRejectTrayToPositionOutFromPositionHomeIng);
            // 完成
            MacState sLoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete = NewState(EnumMacDrawerState.LoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete);
           


            /** Load, 將Tray 自Home 移到 In **/
            // 開始
            MacState sLoadMoveTrayToPositionInStart = NewState(EnumMacDrawerState.LoadMoveTrayToPositionInStart);
            // 動作中
            MacState sLoadMoveTrayToPositionInIng = NewState(EnumMacDrawerState.LoadMoveTrayToPositionInIng);
            // 完成 
            MacState sLoadMoveTrayToPositionInComplete = NewState(EnumMacDrawerState.LoadMoveTrayToPositionInComplete);
            
            // 等待取走盒子
            MacState sIdleForGetBoxOnTrayAtPositionIn = NewState(EnumMacDrawerState.IdleForGetBoxOnTrayAtPositionIn);


            /** Unload **/
            /** 將 Tray 從任何地移到 Position In */
            // 開始
            MacState sUnloadMoveTrayToPositionInStart = NewState(EnumMacDrawerState.UnloadMoveTrayToPositionInStart);
            // 移動中
            MacState sUnloadMoveTrayToPositionInIng = NewState(EnumMacDrawerState.UnloadMoveTrayToPositionInIng);
            // 完成
            MacState sUnloadMoveTrayToPositionInComplete = NewState(EnumMacDrawerState.UnloadMoveTrayToPositionInComplete);
           
       
            // 等待盒子放進來 
            MacState sIdleForPutBoxOnTrayAtPositionIn = NewState(EnumMacDrawerState.IdleForPutBoxOnTrayAtPositionIn);


            /**將 Tray 從 Position In 移到 Home(檢查盒子) => Out*/
            // 將 Tray 移向 Home, 開始 
            MacState sUnloadMoveTrayToPositionHomeStart = NewState(EnumMacDrawerState.UnloadMoveTrayToPositionHomeStart);
            // 將 Tray 移向 Home, 移動中
            MacState sUnloadMoveTrayToPositionHomeIng = NewState(EnumMacDrawerState.UnloadMoveTrayToPositionHomeIng);
            // 將Tray 移向 Home 完成 
            MacState sUnloadMoveTrayToPositionHomeComplete = NewState(EnumMacDrawerState.UnloadMoveTrayToPositionHomeComplete);
         

            // UnLoad, 在HOme 點檢查Tray 上是否有盒子
            MacState sUnloadCheckBoxExistenceAtPositionHome = NewState(EnumMacDrawerState.UnloadCheckBoxExistenceAtPositionHome);
            // UnLoad, 在Home 點檢查 是否有盒子=> 有, 2020/08/03 King Liu Add New 
            MacState sUnloadBoxExistAtPositionHome = NewState(EnumMacDrawerState.UnloadBoxExistAtPositionHome);
            // UnLoad, 在Home 點查 是否有 盒子=> 没有, 2020/08/03 King Liu Add New 
            MacState sUnloadBoxNotExistAtPositionHome = NewState(EnumMacDrawerState.UnloadBoxNotExistAtPositionHome);
           

            /** UnLoad, 在Home時被檢出没有盒子, 回退到 In */
            // 回退到Home 開始
            MacState sUnloadNoBoxRejectTrayToPositionInFromPositionHomeStart = NewState(EnumMacDrawerState.UnloadNoBoxRejectTrayToPositionInFromPositionHomeStart);
            // 回退到 Home 移動中
            MacState sUnloadNoBoxRejectTrayToPositionInFromPositionHomeIng = NewState(EnumMacDrawerState.UnloadNoBoxRejectTrayToPositionInFromPositionHomeIng);
            // 回退到In 完成
            MacState sUnloadNoBoxRejectTrayToPositionInFromPositionHomeComplete = NewState(EnumMacDrawerState.UnloadNoBoxRejectTrayToPositionInFromPositionHomeComplete);
           


            /**Unload, 將 Tray 從 Home 移到 Out*/
            // 開始
            MacState sUnloadMoveTrayToPositionOutFromPositionHomeStart = NewState(EnumMacDrawerState.UnloadMoveTrayToPositionOutFromPositionHomeStart);
            // 移動中
            MacState sUnloadMoveTrayToPositionOutFromPositionHomeIng = NewState(EnumMacDrawerState.UnloadMoveTrayToPositionOutFromPositionHomeIng);
            // 完成 
            MacState sUnloadMoveTrayToPositionOutFromPositionHomeComplete = NewState(EnumMacDrawerState.UnloadMoveTrayToPositionOutFromPOsitionHomeComplete);
            
            // 等待將盒子取走
            MacState sIdleForGetBoxOnTrayAtPositionOut = NewState(EnumMacDrawerState.IdleForGetBoxOnTrayAtIn);

        

            #endregion State

            #region Transition

            // Initial,

            MacTransition tInitialStart_InitialIng = NewTransition(sInitialStart, sInitialIng, EnumMacDrawerTransition.InitialStart_InitialIng);
            MacTransition tInitialing_InitialComplete = NewTransition(sInitialIng, sInitialComplete, EnumMacDrawerTransition.Initialing_InitialComplete);
            MacTransition tInitialComplete_NULL = NewTransition(sInitialComplete, null, EnumMacDrawerTransition.InitialComplete_NULL); 
       

            /** Load(將 Tray 移到 定位~ Out的位置 )**/
            // 開始-進行中
            MacTransition tLoadMoveTrayToPositionOutStart_LoadMoveTrayToPositionOutIng = NewTransition(sLoadMoveTrayToPositionOutStart, sLoadMoveTrayToPositionOutIng, 
                                                                                         EnumMacDrawerTransition.LoadMoveTrayToPositionOutStart_LoadMoveTrayToPositionOutIng);
            // 進行中-完成
            MacTransition tLoadMoveTrayToPositionOutIng_LoadMoveTrayToPositionOutComplete = NewTransition(sLoadMoveTrayToPositionOutIng, sLoadMoveTrayToPositionOutComplete, 
                                                                                         EnumMacDrawerTransition.LoadMoveTrayToPositionOutIng_LoadMoveTrayToPositionOutComplete);
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
          
            // Load (檢查有盒子之後再回 LoadGotoHomeComplete)
            MacTransition tLoadBoxExistAtPositionHome_LoadMoveTrayToPositionHomeComplete = NewTransition(sLoadBoxExistAtPositionHome, sLoadMoveTrayToPositionHomeComplete, 
                                                                                            EnumMacDrawerTransition.LoadBoxExistAtPositionHome_LoadMoveTrayToPositionHomeComplete);
            // Load (檢查没有盒子後再回 LoadGotoHomeComplete)
            MacTransition tLoadBoxNotExistAtPositionHome_LoadMoveTrayToPositionHomeComplete = NewTransition(sLoadBoxNotExistAtPositionHome, sLoadMoveTrayToPositionHomeComplete, 
                                                                                            EnumMacDrawerTransition.LoadBoxNotExistAtPositionHome_LoadMoveTrayToPositionHomeComplete);
            /**到達 Home 之後, 經檢查没有盒子, 回退到 Out**/
            // 到達Home=> 回退到 Out 開始 
            MacTransition tLoadMoveTrayToPositionHomeComplete_LoadNoBoxRejectTrayToPositionOutFromPositionHomeStart = NewTransition(sLoadMoveTrayToPositionHomeComplete, sLoadNoBoxRejectTrayToPositionOutFromPositionHomeStart,
                                                                                            EnumMacDrawerTransition.LoadMoveToPositionHomeComplete_LoadNoBoxRejectToPositionOutFromPositionHomeStart);
            // 回退到 Home 開始=>回退中
            MacTransition tLoadNoBoxRejectTrayToPositionOutFromPositionHomeStart_LoadNoBoxRejectTrayToPositionOutFromHomeIng =
                                                                                            NewTransition(sLoadNoBoxRejectTrayToPositionOutFromPositionHomeStart, sLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng, 
                                                                                            EnumMacDrawerTransition.LoadNoBoxRejectTrayToPositionOutFromPositionHomeStart_LoadNoBoxRejectTrayToPositionOutFromHomeIng);
            // 回退中=> 完成 
            MacTransition tLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng_LoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete =  
                                                                                           NewTransition(sLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng, sLoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete, 
                                                                                           EnumMacDrawerTransition.LoadNoBoxRejectTrayToPositionOutFromPositionHomeIng_LoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete);
           
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
         
            // 完成=> 等待夾走
            MacTransition tLoadMoveTrayToPositionInComplete_IdleForGetBoxOnTrayAtPositionIn = NewTransition(sLoadMoveTrayToPositionInComplete, sIdleForGetBoxOnTrayAtPositionIn, 
                                                                                          EnumMacDrawerTransition.LoadMoveTrayToPositionInComplete_IdleForGetBoxOnTrayAtPositionIn);
            // 等待夾走
            MacTransition tIdleForGetBoxOnTrayAtPositionIn_NULL = NewTransition(sIdleForGetBoxOnTrayAtPositionIn, null,
                                                                                          EnumMacDrawerTransition.IdleForGetBoxOnTrayAtPositionIn_NULL);

            /**Unload**/
            /**將 Tray 移到  Poisition In */
            // 開始=> 移動中
            MacTransition tUnloadMoveTrayToPositionInStart_UnloadMoveTrayToPositionInIng = NewTransition(sUnloadMoveTrayToPositionInStart, sUnloadMoveTrayToPositionInIng, 
                                                                                           EnumMacDrawerTransition.UnloadMoveTrayToPositionInStart_UnloadMoveTrayToPosiotionInIng);
            // 移動中=> 完成
            MacTransition tUnloadMoveTrayToPositionInIng_UnloadMoveTrayToPositionInComplete  = NewTransition(sUnloadMoveTrayToPositionInIng, sUnloadMoveTrayToPositionInComplete,
                                                                                           EnumMacDrawerTransition.UnloadMoveTrayToPositionInIng_UnloadMoveTrayToPositionInComplete);
             // Unload 可以放進盒子
            MacTransition tUnloadMoveTrayToPositionInComplete_IdleForPutBoxOnTrayAtPositionIn = NewTransition(sUnloadMoveTrayToPositionInComplete, sIdleForPutBoxOnTrayAtPositionIn, 
                                                                                           EnumMacDrawerTransition.UnloadMoveTrayToInComplete_IdleForPutBoxOnTrayAtPositionIn);
            MacTransition tIdleForPutBoxOnTrayAtPositionIn_NULL = NewTransition(sIdleForPutBoxOnTrayAtPositionIn, null, EnumMacDrawerTransition.IdleForPutBoxOnTrayAtPositionIn_NULL);


            /** UnLoad(將Tray 移到 Home)*/
            // unload 開始移動 Tray 到 Home=> 移動中
            MacTransition tUnloadMoveTrayToPositionHomeStart_UnloadMoveTrayToPositionHomeIng = NewTransition(sUnloadMoveTrayToPositionHomeStart, sUnloadMoveTrayToPositionHomeIng,
                                                                                           EnumMacDrawerTransition.UnloadMoveTrayToPositionHomeStart_UnloadMoveTrayToPositionHomeIng);
            // unload, Tray 移動中=>完成
            MacTransition tUnloadMoveTrayToPositionHomeIng_UnloadMoveTrayToPositionHomeComplete = NewTransition(sUnloadMoveTrayToPositionHomeIng,sUnloadMoveTrayToPositionHomeComplete, 
                                                                                           EnumMacDrawerTransition.UnloadMoveTrayToPositionHomeIng_UnloadMoveTrayToPositionHomeComplete);
           
            /** Load(將 Tray 移到 Home之後,檢查Box*/
            // 移到 Position Home OK => 檢查 有没有盒子
            MacTransition tUnloadMoveTrayToHomeComplete_UnloadCheckBoxExistenceAtPositionHome = NewTransition(sUnloadMoveTrayToPositionHomeComplete, sUnloadCheckBoxExistenceAtPositionHome, 
                                                                                         EnumMacDrawerTransition.UnloadMoveTrayToPositionHomeComplete_UnloadCheckBoxExistenceAtPositionHome);
            // 檢查有没有 盒子=> 有盒子
            MacTransition tUnloadCheckBoxExistenceAtPositionHome_UnloadBoxExistAtPositionHome = NewTransition(sUnloadCheckBoxExistenceAtPositionHome, sUnloadBoxExistAtPositionHome,
                                                                                         EnumMacDrawerTransition.UnloadCheckBoxExistenceAtPositionHome_UnloadBoxExistAtPositionHome);
            // 檢查有没有盒子=> 没盒子
            MacTransition tUnloadCheckBoxExistenceAtPositionHome_UnloadBoxNotExistAtPositionHome = NewTransition(sUnloadCheckBoxExistenceAtPositionHome, sUnloadBoxExistAtPositionHome, 
                                                                                          EnumMacDrawerTransition.UnloadCheckBoxExistenceAtPositionHome_UnloadBoxNotExistAtPositionHome);
            // 檢查有没有盒子=>逾時
            MacTransition tUnloadCheckBoxExistenceAtPositionHome_UnloadCheckBoxExistenceAtPositionHomeTimeOut = NewTransition(sUnloadCheckBoxExistenceAtPositionHome, sUnloadBoxExistAtPositionHome, 
                                                                                          EnumMacDrawerTransition.UnloadCheckBoxExistenceAtPositionHome_UnloadCheckBoxExistenceAtPositionHomeTimeOut);

            // 檢查有盒子之後再回 到 Home 完成狀態 
            MacTransition tUnloadBoxExistAtPositionHome_UnloadMoveTrayToPositionHomeComplete = NewTransition(sUnloadBoxExistAtPositionHome, sUnloadMoveTrayToPositionHomeComplete, 
                                                                                          EnumMacDrawerTransition.UnloadBoxExistAtPositionHome_UnloadMoveTrayToHomeComplete);
            // 檢查没有盒子後再回 到Home 完成狀態
            MacTransition tUnloadBoxNotExistAtPositionHome_UnloadMoveTrayToPositionHomeComplete = 
                                                                            NewTransition(sUnloadBoxNotExistAtPositionHome, sUnloadMoveTrayToPositionHomeComplete, 
                                                                   EnumMacDrawerTransition.UnloadBoxNotExistAtPositionHome_UnloadMoveTrayToPositionHomeComplete);
            // Home 完成=>回退到 Position In 開始 
            MacTransition tUnloadMoveTrayToPositionHomeComplete_UnloadNoBoxRejectTrayToPositionInFromPositionHomeStart = 
                                                                            NewTransition(sUnloadMoveTrayToPositionHomeComplete,sUnloadNoBoxRejectTrayToPositionInFromPositionHomeStart, 
                                                                   EnumMacDrawerTransition.UnloadMoveTrayToPositionHomeComplete_UnloadNoBoxRejectTrayToPositionInFromPositionHomeStart);
            // 回退到 In 開始=> 回退到 In 移動中
            MacTransition tUnloadNoBoxRejectTrayToPositionInFromPositionHomeStart_UnloadNoBoxRejectTrayToPositionInFromPositionHomeIng =
                                                                            NewTransition(sUnloadNoBoxRejectTrayToPositionInFromPositionHomeStart,sUnloadNoBoxRejectTrayToPositionInFromPositionHomeIng, 
                                                                   EnumMacDrawerTransition.UnloadNoBoxRejectTrayToPositionInFromPositionHomeStart_UnloadNoBoxRejectTrayToPositionInFromPositionHomeIng);
            // 回退到In 移動中 => 到達 In
            MacTransition tUnloadNoBoxRejectTrayToPositionInFromPositionHomeIng_UnloadNoBoxRejectTrayToPositionInFromPositionHomeComplete = 
                                                                            NewTransition(sUnloadNoBoxRejectTrayToPositionInFromPositionHomeIng, sUnloadNoBoxRejectTrayToPositionInFromPositionHomeComplete, 
                                                                     EnumMacDrawerTransition.UnloadNoBoxRejectTrayToPositionInFromPositionHomeIng_UnloadNoBoxRejectTrayToPositionInFromPositionHomeComplete);
                      // 回退到達In => 等待放置盒子
            MacTransition tUnloadNoBoxRejectTrayToPOsitionInFromPositionHomeComplete_IdleForPutBoxOnTrayAtPositionIn = 
                                                                            NewTransition(sUnloadNoBoxRejectTrayToPositionInFromPositionHomeComplete, sIdleForPutBoxOnTrayAtPositionIn, 
                                                                     EnumMacDrawerTransition.UnloadNoBoxRejectTrayToPositionInFromPositionHomeComplete_IdleForPutBoxOnTrayAtPositionIn);



            // Unload(將 Tray 移到 Out)
            MacTransition tUnloadMoveTrayToPositionHomeComplete_UnloadMoveTrayToPositionOutFromPositionHomeStart = NewTransition(sUnloadMoveTrayToPositionHomeComplete, sUnloadMoveTrayToPositionOutFromPositionHomeStart,
                                                                                              EnumMacDrawerTransition.UnloadMoveTrayToPositionHomeComplete_UnloadMoveTrayToPositionOutFromPositionHomeStart);
            MacTransition tUnloadMoveTrayToPositionOutFromPositionHomeStart_UnloadMoveTrayToPositionOutFromPositionHomeIng = NewTransition(sUnloadMoveTrayToPositionOutFromPositionHomeStart, sUnloadMoveTrayToPositionOutFromPositionHomeIng, 
                                                                                               EnumMacDrawerTransition.UnloadMoveTrayToPositionOutFromPositionHomeStart_UnloadMoveTrayToPositionOutFromPositionHomeIng);
            MacTransition tUnloadMoveTrayToPositionOutFromPositionHomeIng_UnloadMoveTrayToPositionOutFromPositionHomeComplete = NewTransition(sUnloadMoveTrayToPositionOutFromPositionHomeIng, sUnloadMoveTrayToPositionOutFromPositionHomeComplete, 
                                                                                               EnumMacDrawerTransition.UnloadMoveTrayToPositionOutFromPositionHomeIng_UnloadMoveTrayToPositionOutFromPOsitionHomeComplete);
                      // Unload 可以將 Box 從 位於 In 的Tray 取走
            MacTransition tUnloadMoveTrayToPositionOutComplete_IdleForGetBoxOnTrayAtPositionOut = NewTransition(sUnloadMoveTrayToPositionOutFromPositionHomeComplete, sIdleForGetBoxOnTrayAtPositionOut,
                                                                                               EnumMacDrawerTransition.UnloadMoveTrayToPositionOutComplete_IdleForGetBoxOnTrayAtPositionOut);

            MacTransition tIdleForGetBoxOnTrayAtPositionOut_NULL = NewTransition(sIdleForGetBoxOnTrayAtPositionOut, null, EnumMacDrawerTransition.IdleForGetBoxOnTrayAtPositionOut_NULL);


            #endregion


            #region  Event

            sInitialStart.OnEntry+= ( sender,  e)=>
             { // Sync
                 var transition = tInitialStart_InitialIng;
                 TriggerMember triggerMember = new TriggerMember
                 {
                     Guard = () =>  true,
                     Action = (parameter) =>
                     {
                         HalDrawer.CommandINI();
                     },
                     ActionParameter = null,
                     ExceptionHandler = (state, ex) =>
                     {
                         // TODO: Do Something
                     },
                     NotGuardException = null,
                     NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                     ThisStateExitEventArgs = new MacStateExitEventArgs(),

                 };
                 transition.SetTriggerMembers(triggerMember);
                 Trigger(transition);
                
             };
            
            sInitialStart.OnExit += (sender, e) =>
            {
               // 視需要加上 Code,
            };

           sInitialIng.OnEntry += (sender, e) =>
            {// Async
                var transition = tInitialing_InitialComplete;
                TriggerMemberAsync triggerMemberAsync = new TriggerMemberAsync
                {
                    Guard = (startTime) =>
                    {
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome)
                        {
                            return true;
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.InitialFailed)
                        {
                            throw new DrawerInitialFailException();
                        }
                        else if (timeoutObj.IsTimeOut(startTime))
                        {
                            throw new DrawerInitialTimeOutException();
                        }
                        else
                        {
                            return false;
                        }
                    },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
               
            };
            sInitialIng.OnExit += (sender, e) =>
            {
               // 視需要加上 Code
            };

            sInitialComplete.OnEntry += (sender, e)=>
            {  // Sync
                var transition = tInitialComplete_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => true,
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler =(state,ex)=>
                    {
                        // TODO: do something
                    },
                    NextStateEntryEventArgs = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                    NotGuardException = null
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
                
            };
            
            sInitialComplete.OnExit += (sender, e) =>
            {
                // 視需要加上 Code
            };

         
            
            sLoadMoveTrayToPositionOutStart.OnEntry += (sender, e) =>
            { // Sync
                var transition = tLoadMoveTrayToPositionOutStart_LoadMoveTrayToPositionOutIng;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>true,
                    Action = (parameter) =>
                    {
                        HalDrawer.CommandTrayMotionOut();
                    },
                    ActionParameter = null,
                    ExceptionHandler =(state,ex)=> 
                    {
                        // TODO: Do Something
                    },
                    NotGuardException = null,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),

                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);

              
            };
            sLoadMoveTrayToPositionOutStart.OnExit += (sender, e) =>
            {
                // 視需要加上 Code
            };
            

            sLoadMoveTrayToPositionOutIng.OnEntry += (sender, e) =>
            {   //async
                var transition = tLoadMoveTrayToPositionOutIng_LoadMoveTrayToPositionOutComplete;
                TriggerMemberAsync triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: Do Something
                    },
                    Guard = (startTime) =>
                    {
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionOut)
                        {
                            return true;
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            throw new DrawerLoadMoveTrayToPositionOutFailException();
                        }
                        else if (timeoutObj.IsTimeOut(startTime))
                        {
                            throw new DrawerLoadMoveTrayToPositionOutTimeOutException();
                        }
                        else
                        {
                            return false;
                        }
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                     
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);

               
            };
            sLoadMoveTrayToPositionOutIng.OnExit += (sender, e) =>
            {
               // 視需要加上 Code
            };
        
            sLoadMoveTrayToPositionOutComplete.OnEntry += (sender, e) =>
            {    // sync
                var transition = tLoadMoveTrayToPositionOutComplete_LoadIdleForPutBoxOnTrayAtPositionOut;
                var triggerMember = new TriggerMember
                {
                    Guard = () => true,
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler =(state,ex)=> 
                    {
                        // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                    NotGuardException = null,
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
               
            };
            sLoadMoveTrayToPositionOutComplete.OnExit += (sender, e) =>
            {
                // 視需要加上 Code
            };
           
            sIdleForPutBoxOnTrayAtPositionOut.OnEntry += (sender, e) =>
              {//Sync
                  var transition = tIdleForPutBoxOnTrayAtPositionOut_NULL;
                  TriggerMember triggerMember = new TriggerMember
                  {
                      Guard = () => true,
                      Action = null,
                      ActionParameter = null,
                      ExceptionHandler =(state, ex)=> 
                      {
                          // TODO: do something
                      },
                      NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                      ThisStateExitEventArgs = new MacStateExitEventArgs(),
                      NotGuardException = null
                  };
                  transition.SetTriggerMembers(triggerMember);
                  Trigger(transition);
                 
              };
            sIdleForPutBoxOnTrayAtPositionOut.OnExit += (sender, e) =>
            {
                // Nothing

            };

            
            // 
            sLoadMoveTrayToPositionHomeStart.OnEntry += (sender,e) =>
            { // Sync
                var transition = tLoadMoveTrayToPositionHomeStart_LoadMoveTrayToPositionHomeIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { HalDrawer.CommandTrayMotionHome(); },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: DO Simething
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                    NotGuardException = null,
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
               
            };
            sLoadMoveTrayToPositionHomeStart.OnExit += (sender, e) =>
            {
                // 視需要加上 Code
            };

            sLoadMoveTrayToPositionHomeIng.OnEntry += (sender, e) =>
            { // Async
                var transition = tLoadMoveTrayToPositionInIng_LoadMoveTrayToPositionInComplete;
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                      {
                          // TODO: Do Something
                      },
                    Guard = (startTime) =>
                    {
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome) { return true; }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed) { throw new DrawerLoadMoveTrayToPositionHomeFailException(); }
                        else if (timeoutObj.IsTimeOut(startTime)) { throw new DrawerLoadMoveTrayToPositionHomeTimeOutException(); }
                        else { return false; }
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(EnumMacDrawerLoadToHomeCompleteSource.MoveTrayToPositionHome),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
               
            };
            sLoadMoveTrayToPositionHomeIng.OnExit += (sender, e) =>
              {
                  // 視需要加上 Code
              };
            
            sLoadMoveTrayToPositionHomeComplete.OnEntry += (sender, e) =>
            {  //Sync
                var eventArgs = (MacStateEntryEventArgs)e;
                var src = (EnumMacDrawerLoadToHomeCompleteSource)(eventArgs.Parameter);
                MacTransition transition = null;
                TriggerMember triggerMember = null;
                if (src == EnumMacDrawerLoadToHomeCompleteSource.MoveTrayToPositionHome)
                {   // 去檢查有没有盒子
                    transition = tLoadMoveTrayToPositionHomeComplete_LoadCheckBoxExistenceAtPositionHome;
                    triggerMember = new TriggerMember
                    {
                        Action =(parameter)=> { HalDrawer.CommandBoxDetection(); },
                        ActionParameter = null,
                        Guard = () =>true,
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        ThisStateExitEventArgs = new MacStateExitEventArgs(),
                        ExceptionHandler = (state, ex) => 
                        {
                            // TODO: do something
                        },
                        NotGuardException=null
                    };
                    
                }
                else if (src == EnumMacDrawerLoadToHomeCompleteSource.LoadCheckBoxExist)
                {   // 檢查後, 盒子在, 移到 In 
                    transition=tLoadMoveTrayToPositionHomeComplete_LoadMoveTrayToPositionInStart;
                    triggerMember = new TriggerMember
                    {
                        Action = null,
                        ActionParameter = null,
                        Guard = () => true,
                        ExceptionHandler = (state, ex) =>
                        {
                            // TODO: do something
                        },
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        NotGuardException = null,
                        ThisStateExitEventArgs = new MacStateExitEventArgs(),
                    };
                }
                else //if(src== EnumMacDrawerLoadToHomeCompleteSource.LoadCheckBoxNotExist)
                {   // 検查後, 盒子不在, 回退到 Position Out
                    transition = tLoadMoveTrayToPositionHomeComplete_LoadNoBoxRejectTrayToPositionOutFromPositionHomeStart;
                    triggerMember = new TriggerMember
                    {
                        Action = null,
                        ActionParameter = null,
                        Guard = () => true,
                        ExceptionHandler = (state, ex) =>
                        {
                            // TODO: Do Something
                        },
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        ThisStateExitEventArgs = new MacStateExitEventArgs(),
                        NotGuardException = null
                    };
                }
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);

           
            };
            sLoadMoveTrayToPositionHomeComplete.OnExit += (sender, e) =>
            {
                // 視需要加上 Code

            };

            
            // Load 時, 在 Home 點檢查有没有盒子
            sLoadCheckBoxExistenceAtPositionHome.OnEntry += (sender, e) =>
            {  //Async
              
                var transitionNoBox = tLoadCheckBoxExistenceAtPositionHome_LoadBoxNotExistAtPositionHome;
                var transitionHasBox = tLoadCheckBoxExistenceAtPositionHome_LoadBoxExistAtPositionHome;
                List<MacTransition> transitions = new List<MacTransition>();
                transitions.Add(transitionNoBox);
                transitions.Add(transitionHasBox);
                var triggerMemberHasBoxAsync = new TriggerMemberAsync
                {
                    Guard = (startTime) =>
                    {
                        if (DrawerWorkState.BoxExist == HalDrawer.CurrentWorkState) { return true; }
                        else if (timeoutObj.IsTimeOut(startTime))
                        {
                            throw new DrawerLoadCheckBoxExistanceAtPositionHomeTimeOutException();
                        }
                        else
                        {
                            return false;
                        }
                    },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: Do Something 
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transitionHasBox.SetTriggerMembers(triggerMemberHasBoxAsync);
                var triggerMemberHasNoBoxAsync = new TriggerMemberAsync
                {
                    Guard = (startTime) =>
                    {
                        if (DrawerWorkState.BoxNotExist == HalDrawer.CurrentWorkState) { return true; }
                        else if (timeoutObj.IsTimeOut(startTime))
                        {
                            throw new DrawerLoadCheckBoxExistanceAtPositionHomeTimeOutException();
                        }
                        else
                        {
                            return false;
                        }
                    },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: Do Something 
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transitionNoBox.SetTriggerMembers(triggerMemberHasNoBoxAsync);
                TriggerAsync(transitions);
            
            };
            sLoadCheckBoxExistenceAtPositionHome.OnExit += (sender, e) =>
            {
                // 視需要增加 Code
            };

            

            // Load 時在 HOME 點檢查有盒子
            sLoadBoxExistAtPositionHome.OnEntry += (sender, e) =>
            {  // Sync
                var transition = tLoadBoxExistAtPositionHome_LoadMoveTrayToPositionHomeComplete;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(EnumMacDrawerLoadToHomeCompleteSource.LoadCheckBoxExist),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                    ExceptionHandler = (state, ex) => 
                    {
                        //TODO: DO Something
                    },
                    Guard = () => true,
                    NotGuardException = null,
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
             
            };
            sLoadBoxExistAtPositionHome.OnExit += (sender, e) =>
            {
                // 視需要增加 Code 
            };

            // Load 時在 Home 點檢查没盒子
            sLoadBoxNotExistAtPositionHome.OnEntry += (sender, e) =>
            {     // Not Async
                var transition = tLoadBoxNotExistAtPositionHome_LoadMoveTrayToPositionHomeComplete;
                var triggerMember = new TriggerMember
                {
                    Guard = () => true,
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>  
                    {
                        // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(EnumMacDrawerLoadToHomeCompleteSource.LoadCheckBoxNotExist),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                    NotGuardException = null,
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
               
            };
            sLoadBoxNotExistAtPositionHome.OnExit += (sender, e) =>
            {
              // 視需要增加 Code
            };

          
            // Load 時, 在 Home 點檢查没有盒子, 將 Tray 回退到 Home, 開始
            sLoadNoBoxRejectTrayToPositionOutFromPositionHomeStart.OnEntry += (sender, e) =>
            {
                // Sync
                var transition = tLoadNoBoxRejectTrayToPositionOutFromPositionHomeStart_LoadNoBoxRejectTrayToPositionOutFromHomeIng;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => true,
                    Action = (parameter) => { HalDrawer.CommandTrayMotionOut(); },
                    ActionParameter = null,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                    ExceptionHandler = (state,ex)=>
                    {
                        // TODO: do something
                    },
                    NotGuardException = null,
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
               
                
            };
            sLoadNoBoxRejectTrayToPositionOutFromPositionHomeStart.OnExit += (sender, e) =>
            {
             // 視需要加上 Code
            };

            // Load 時, 在Home 點檢查時没有盒子,  將Tray 回退到 Out,Tray 移動中
            sLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng.OnEntry += (sender, e) =>
            {// Async
                MacTransition transition = tLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng_LoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete;
                TriggerMemberAsync triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (State, ex) =>
                    {
                        //TODO: Do something
                    },
                    Guard = (startTime) =>
                    {

                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionOut)
                        {
                            return true;
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            throw new DrawerLoadNoBoxRejectTrayToPositionOutFromPositionHomeFailException();
                        }
                        else if (timeoutObj.IsTimeOut(startTime))
                        {
                            throw new DrawerLoadNoBoxRejectTrayToPositionOutFromPositionHomeTimeOutException();
                        }
                        return false;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
               
              
            };
            sLoadNoBoxRejectTrayToPositionOutFromPositionHomeIng.OnExit += (sender, e) =>
            {
               // 視需要增加 Code
            };

            // Load 時, 在 Home 點檢查没有盒子, 將 Tray 回退到 Out, 完成
            sLoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete.OnEntry += (sender, e) =>
            {//Sync
                var transition = tLoadNoBoxRejectTrayToPositionOutFromPOsitionHomeComplete_IdleForPutBoxOnTrayAtPositionOut;
                TriggerMember triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: Do something 
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
                
             
            };
            sLoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete.OnExit += (sender, e) =>
            {
               // 視狀況增加 Code
            };

           
            // Load 時, 檢查 有盒子之後, 將Tray 移到  Position In, 動作開始
            sLoadMoveTrayToPositionInStart.OnEntry += (sender, e) =>
            {    // Sync
                var transition = tLoadMoveTrayToPositionInStart_LoadMoveTrayToPositionInIng;
                TriggerMember triggerMember = new TriggerMember
                {
                    Action = (parameter) => { HalDrawer.CommandTrayMotionIn(); },
                    Guard = () => true,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    NotGuardException = null,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
              

            };
            sLoadMoveTrayToPositionInStart.OnExit += (sender, e) =>
            {
              // 視需要增加 Code
            };

            // Load 時, 檢查 有盒子之後, 將Tray 移到  Position In, 動作中 
            sLoadMoveTrayToPositionInIng.OnEntry += (sender, e) =>
            {// Async
                var transition = tLoadMoveTrayToPositionInIng_LoadMoveTrayToPositionInComplete;
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
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionIn)
                        {
                            return true;
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            throw new DrawerLoadMoveTrayToPositionInFailException();
                        }
                        else if (timeoutObj.IsTimeOut(startTime))
                        {
                            throw new DrawerLoadMoveTrayToPositionInTimeOutException();
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
            sLoadMoveTrayToPositionInIng.OnExit += (sender, e) =>
            {
                // 視需要增加 Code
            };

            sLoadMoveTrayToPositionInComplete.OnEntry += (sender, e) =>
            { // Sync
                var transition = tLoadMoveTrayToPositionInComplete_IdleForGetBoxOnTrayAtPositionIn;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: to something
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
                
            };
            sLoadMoveTrayToPositionInComplete.OnExit += (sender, e) =>
            {
               // 視需要增 加 Code
            };
            sIdleForGetBoxOnTrayAtPositionIn.OnEntry += (sender, e) =>
            { // Sync
                var transition = tIdleForGetBoxOnTrayAtPositionIn_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => true,
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = null,
                    NextStateEntryEventArgs = null,
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                TriggerAsync(transition);
          
            };
            sIdleForGetBoxOnTrayAtPositionIn.OnExit += (sender, e) =>
            {
                // 視需增加 Code 
            };
          

            /**Uuload**/
            // 將Tray 移到 Position In 開始
            sUnloadMoveTrayToPositionInStart.OnEntry += (sender, e) =>
            {   // Sync
                var transition = tUnloadMoveTrayToPositionInStart_UnloadMoveTrayToPositionInIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter)=> HalDrawer.CommandTrayMotionIn(),
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                TriggerAsync(transition);
                
            };
            sUnloadMoveTrayToPositionInStart.OnExit += (sender, e) =>
            {
              // 按實況增加 Code
            };

            // 將Tray 移到Position In, 移動中
            sUnloadMoveTrayToPositionInIng.OnEntry += (sender, e) =>
            {   // Async
                var transition = tUnloadMoveTrayToPositionInIng_UnloadMoveTrayToPositionInComplete;
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something,
                    },
                    Guard = (startTime) =>
                    {
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionIn)
                        {
                            return true;
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            throw new DrawerUnloadMoveTrayToPositionInFailException();
                        }
                        else if (timeoutObj.IsTimeOut(startTime))
                        {
                            throw new DrawerUnloadMoveTrayToPositionInTimeOutException();
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
            sUnloadMoveTrayToPositionInIng.OnExit += (sender, e) =>
            {
                // 依實際狀況增加 Code
            };
            
            // UnLoad, Tray 到達 In
            sUnloadMoveTrayToPositionInComplete.OnEntry += (sender, e) =>
            {  // Sync
                var transition = tUnloadMoveTrayToPositionInComplete_IdleForPutBoxOnTrayAtPositionIn;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                      {
                          // TODO: do something
                      },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
               

            };
            
            sUnloadMoveTrayToPositionInComplete.OnExit += (sender, e) =>
            {

                // 依實際狀況增加 Code
            };

            //  等待將 Box 放到Tray 內
            sIdleForPutBoxOnTrayAtPositionIn.OnEntry += (sender, e) =>
            { // Sync
                var transition = tIdleForPutBoxOnTrayAtPositionIn_NULL;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something 
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);

          
            };
            sIdleForPutBoxOnTrayAtPositionIn.OnExit += (sender, e) =>
            {
              // 視狀況增加 Code
            };

      

            sUnloadMoveTrayToPositionHomeStart.OnEntry += (sender, e) =>
            {   // Sync
                var transition = tUnloadMoveTrayToPositionHomeStart_UnloadMoveTrayToPositionHomeIng;
                TriggerMember triggerMember = new TriggerMember
                {
                    Action = (parameter) => HalDrawer.CommandTrayMotionHome(),
                    ActionParameter = null,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: Do something,
                    },
                    Guard = () => true,
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()

                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
              
            };
            sUnloadMoveTrayToPositionHomeStart.OnExit += (sender, e) =>
            {
              
            };

            sUnloadMoveTrayToPositionHomeIng.OnEntry += (sender, e) =>
            {  // Async
                var transition= tUnloadMoveTrayToPositionHomeIng_UnloadMoveTrayToPositionHomeComplete;
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
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome)
                        {
                            return true;
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            throw new DrawerUnloadMoveTrayToPositionHomeFailException();
                        }
                        else if (timeoutObj.IsTimeOut(startTime))
                        {
                            throw new DrawerUnloadMoveTrayToPositionHomeTimeOutException();
                        }
                        else
                        {
                            return false;
                        }
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(EnumMacDrawerUnloadToHomeCompleteSource.MoveTrayToPositionHomeIng),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),                     
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
              
            };
             sUnloadMoveTrayToPositionHomeIng.OnExit += (sender, e) =>
             {
                 // 視實況增加 Code
                
              };

            sUnloadMoveTrayToPositionHomeComplete.OnEntry += (sender, e) =>
            {   // Sync
                EnumMacDrawerUnloadToHomeCompleteSource source  = (EnumMacDrawerUnloadToHomeCompleteSource)(((MacStateEntryEventArgs)e).Parameter);
                MacTransition transition = null;
                TriggerMember triggerMember = null;
                if(source == EnumMacDrawerUnloadToHomeCompleteSource.MoveTrayToPositionHomeIng)
                {
                    transition = tUnloadMoveTrayToHomeComplete_UnloadCheckBoxExistenceAtPositionHome;
                    triggerMember = new TriggerMember
                    {
                        Guard = () => true,
                        Action = (parameter)=> HalDrawer.CommandBoxDetection(),
                        ActionParameter = null,
                        ExceptionHandler = (state, ex) =>
                        {
                            // TODO: do something
                        },
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        NotGuardException = null,
                        ThisStateExitEventArgs = new MacStateExitEventArgs()
                    };
                }
                else if (source == EnumMacDrawerUnloadToHomeCompleteSource.UnloadCheckBoxExist)
                {
                    transition = tUnloadMoveTrayToPositionHomeComplete_UnloadMoveTrayToPositionOutFromPositionHomeStart;
                    triggerMember = new TriggerMember
                    {
                        Guard = ()=> true,
                        Action = null,
                        ActionParameter = null,
                        ExceptionHandler = (state, ex) =>
                        {
                            // TODO: do something
                        },
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        NotGuardException = null,
                        ThisStateExitEventArgs = new MacStateExitEventArgs()
                    };
                }
                else //if(source == EnumMacDrawerUnloadToHomeCompleteSource.UnloadCheckBoxNotExist)
                {
                    transition = tUnloadMoveTrayToPositionHomeComplete_UnloadNoBoxRejectTrayToPositionInFromPositionHomeStart;
                    triggerMember = new TriggerMember
                    {
                        Guard = () => true,
                        Action = null,
                        ActionParameter = null,
                        ExceptionHandler = (state, ex) =>
                        {
                            // TODO: do something
                        },
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        NotGuardException = null,
                        ThisStateExitEventArgs = new MacStateExitEventArgs()
                    };
                }
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
               
            };
            sUnloadMoveTrayToPositionHomeComplete.OnExit += (sender, e) =>
            {
               // 依狀況新增 Code 
               
            };

          
            sUnloadCheckBoxExistenceAtPositionHome.OnEntry += (sender, e) =>
            {   // Async
                var transitionHasBox = tUnloadCheckBoxExistenceAtPositionHome_UnloadBoxExistAtPositionHome;
                var transitionNoBox = tUnloadCheckBoxExistenceAtPositionHome_UnloadBoxNotExistAtPositionHome;
                List<MacTransition> transitions = new List<MacTransition>();
                transitions.Add(transitionHasBox);transitions.Add(transitionNoBox);
                var triggerMemberAsyncHasBox = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {

                    },
                    Guard = (startTime) =>
                    {
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.BoxExist)
                        {
                            return true;
                        }
                        else if (timeoutObj.IsTimeOut(startTime))
                        {
                            throw new DrawerUnloadCheckBoxExistanceAtPositionHomeTimeOutException();
                        }
                        else
                        {
                            return false;
                        }
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(EnumMacDrawerUnloadToHomeCompleteSource.UnloadCheckBoxExist),
                    ThisStateExitEventArgs = new MacStateExitEventArgs() 
                };
                var triggerMemberAsyncNoBox = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {

                    },
                    Guard = (startTime) =>
                    {
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.BoxNotExist)
                        {
                            return true;
                        }
                        else if (timeoutObj.IsTimeOut(startTime))
                        {
                            throw new DrawerUnloadCheckBoxExistanceAtPositionHomeTimeOutException();
                        }
                        else
                        {
                            return false;
                        }
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(EnumMacDrawerUnloadToHomeCompleteSource.UnloadCheckBoxNotExist),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transitionHasBox.SetTriggerMembers(triggerMemberAsyncHasBox);
                transitionNoBox.SetTriggerMembers(triggerMemberAsyncNoBox);
                TriggerAsync(transitions);

             
            };
            sUnloadCheckBoxExistenceAtPositionHome.OnExit += (sender, e) =>
            {
                // 視狀況增加 Code
             
            };

            sUnloadBoxExistAtPositionHome.OnEntry += (sender, e) =>
            {  //Sync
                var transition = tUnloadBoxExistAtPositionHome_UnloadMoveTrayToPositionHomeComplete;
                var triggerMember = new TriggerMember
                {
                    Guard = () => true,
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {

                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(EnumMacDrawerUnloadToHomeCompleteSource.UnloadCheckBoxExist),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
             
            };
            sUnloadBoxExistAtPositionHome.OnExit += (sender, e) =>
            {
                // 視狀況新增Code  
              
             };

            sUnloadBoxNotExistAtPositionHome.OnEntry += (sender, e) =>
            {  // Sync
                var transition = tUnloadBoxNotExistAtPositionHome_UnloadMoveTrayToPositionHomeComplete;
                TriggerMember triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: Do Something
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(EnumMacDrawerUnloadToHomeCompleteSource.UnloadCheckBoxNotExist),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                    NotGuardException = null,
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
               
            };
            sUnloadBoxNotExistAtPositionHome.OnExit += (sender, e) =>
            {
                // 視狀況增加 Code
               
             };


            sUnloadNoBoxRejectTrayToPositionInFromPositionHomeStart.OnEntry += (sender, e) =>
            {  // Unload, 檢查到没有盒子時要回將Tray回退到 In的位置
               // Sync
                var transition = tUnloadNoBoxRejectTrayToPositionInFromPositionHomeStart_UnloadNoBoxRejectTrayToPositionInFromPositionHomeIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => HalDrawer.CommandTrayMotionIn(),
                    Guard = () => true,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                    NotGuardException = null
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
              
            };
            sUnloadNoBoxRejectTrayToPositionInFromPositionHomeStart.OnExit += (sender, e) =>
            {
                // 視狀況增加 Code
              
            };

            sUnloadNoBoxRejectTrayToPositionInFromPositionHomeIng.OnEntry += (sender, e) =>
             {   // Async
                 var transition = tUnloadNoBoxRejectTrayToPositionInFromPositionHomeIng_UnloadNoBoxRejectTrayToPositionInFromPositionHomeComplete;
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
                         if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionIn)
                         {
                             return true;
                         }
                         else if(HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                         {
                             throw new DrawerUnloadNoBoxRejectTrayToPositionInFromPositionHomeFailException();
                         }
                         else if(timeoutObj.IsTimeOut(startTime))
                         {
                             throw new DrawerUnloadNoBoxRejectTrayToPositionInFromPositionHomeTimeOutException();
                         }
                         else
                         {
                             return false;
                         }
                     },
                     NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                     ThisStateExitEventArgs=new MacStateExitEventArgs(),
                 };
                 transition.SetTriggerMembers(triggerMemberAsync);
                 TriggerAsync(transition);
              
             };
            sUnloadNoBoxRejectTrayToPositionInFromPositionHomeIng.OnExit += (sender, e) =>
            {
                // 視狀況增加 Code 
               
            };

            sUnloadNoBoxRejectTrayToPositionInFromPositionHomeComplete.OnEntry += (sender, e) =>
            {   // Sync
                var transition = tUnloadNoBoxRejectTrayToPOsitionInFromPositionHomeComplete_IdleForPutBoxOnTrayAtPositionIn;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // Todo: do something
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()

                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
              
            };
            sUnloadNoBoxRejectTrayToPositionInFromPositionHomeComplete.OnExit += (sender, e) =>
            {
                // 視狀況增加 Code
               
            };

         
           sUnloadMoveTrayToPositionOutFromPositionHomeStart.OnEntry += (sender, e) => 
            {   // Sync 
                var transition = tUnloadMoveTrayToPositionOutFromPositionHomeStart_UnloadMoveTrayToPositionOutFromPositionHomeIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => HalDrawer.CommandTrayMotionIn(),
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                     ThisStateExitEventArgs=new MacStateExitEventArgs(),

                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
               
            };
            sUnloadMoveTrayToPositionOutFromPositionHomeStart.OnExit += (sender, e) =>
            {
                // 視狀況增加 Code
           
            };

            sUnloadMoveTrayToPositionOutFromPositionHomeIng.OnEntry += (sender, e) =>
            {   //Async
                var transition = tUnloadMoveTrayToPositionOutFromPositionHomeIng_UnloadMoveTrayToPositionOutFromPositionHomeComplete;
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // Todo: do something
                    },
                    Guard = (startTime) =>
                    {
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionOut)
                        {
                            return true;
                        }
                        else if(HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            throw new DrawerUnloadMoveTrayToPositionOutFailException();
                        }
                        else if(timeoutObj.IsTimeOut(startTime))
                        {
                            throw new DrawerUnloadMoveTrayToPositionOutTimeOutException();
                        }
                        else
                        {
                            return false;
                        }
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                     
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                Trigger(transition);
         
            };
            sUnloadMoveTrayToPositionOutFromPositionHomeIng.OnExit += (sender, e) =>
            {
                // 按狀況增加 COde
              
            };

           sUnloadMoveTrayToPositionOutFromPositionHomeComplete.OnEntry+=(sender,e)=>
            {   // Sync
                var transition = tUnloadMoveTrayToPositionOutComplete_IdleForGetBoxOnTrayAtPositionOut;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        //TODO: do something
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sUnloadMoveTrayToPositionOutFromPositionHomeComplete.OnExit += (sender, e) =>
            {
                // 視狀況增加 Code
            };

            sIdleForGetBoxOnTrayAtPositionOut.OnEntry += (sender, e) =>
            {
                var transition = tIdleForGetBoxOnTrayAtPositionOut_NULL;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // Todo: doi something
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);

            };
            sIdleForGetBoxOnTrayAtPositionOut.OnExit += (sender, e) =>
            {
                // 視狀況增加 Code
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
