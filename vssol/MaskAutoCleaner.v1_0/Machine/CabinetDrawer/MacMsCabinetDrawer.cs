using MaskAutoCleaner.v1_0.StateMachineBeta;
using MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.CompDrawer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.CabinetDrawer
{
    [Guid("71653EBD-4BF3-46B5-9541-02CFFD14A3EA")]
    public class MacMsCabinetDrawer : MacMachineStateBase
    {
        public IMacHalCabinet HalCabinetUniversal { get { return this.halAssembly as IMacHalCabinet; } }


        protected override void SetCurrentState(MacState state)
        {
            base.SetCurrentState(state);
            /**
            try
            {
                if (state == this.States[EnumMacCabinetDrawerState.WaitingLoadInstruction.ToString()])
                {  //WaitingLoadInstruction: 等待, Load 命令: 亮綠燈
                    this.HalDrawer.CommandBrightLEDGreenOn();
                }
                else if (state == this.States[EnumMacCabinetDrawerState.WaitingUnloadInstruction.ToString()])
                {  // WaitingUnloadInstruction: 等待Unload命令: 亮紅燈
                    this.HalDrawer.CommandBrightLEDRedOn();
                } 
                else if (state.StateException != null)
                {  // 發生例外
                    this.HalDrawer.CommandBrightLEDAllOn();
                }
                else
                {  // LED 全滅
                    this.HalDrawer.CommandBrightLEDAllOff();
                }
            }
            catch  {    }
            */
        }
        public IMacHalDrawer HalDrawer
        {
            get
            {


                return HalCabinetUniversal.MacHalDrawer;
            }
        }

        public string DeviceIndex { get { return HalDrawer.DeviceIndex; } }

        /// <summary>
        /// Fake Test 時直接設定為某狀態
        /// </summary>
        /// <param name="enumState"></param>
        public void SetCurrentStateForFakeTest(EnumMacCabinetDrawerState enumState)
        {
            MacState state = States[enumState.ToString()];
            SetCurrentState(state);
        }
        /// <summary>目前的狀態是否可以接受 Load 指令?</summary>
        /// <returns></returns>
        public bool CanLoad()
        {
            if (CurrentState == this.States[EnumMacCabinetDrawerState.WaitingLoadInstruction.ToString()])
            { return true; }
            else
            { return false; }
        }

        public MacMsCabinetDrawer()
        {

            TimeoutObject = new MacMsTimeOutController(20);

            LoadStateMachine();
        }

        #region State Machine Command

        /// <summary>系統啟動</summary>
        /// <remarks>
        /// 情境: 系統重啟之後, 各個 Drawer 需進入這個狀態
        /// </remarks>
        public override void SystemBootup()
        {

            this.States[EnumMacCabinetDrawerState.SystemBootup.ToString()].ExecuteCommand(new MacStateEntryEventArgs());
        }

        /// <summary>系統啟動之後 Initial</summary>
        /// <remarks>
        /// <para>
        /// 狀態會更新到 sWaitingLoadInstruction (等待 Load 命令狀態)
        /// </para>
        /// <para>
        /// 情境: 系統重啟執行第一個作動之前需執行這個命令
        /// </para>
        /// </remarks>
        public void SystemBootupInitial()
        {
            this.States[EnumMacCabinetDrawerState.SystemBootupInitialStart.ToString()].ExecuteCommand(new MacStateEntryEventArgs());
        }

        /// <summary>Load, 將 Tray 移到 Out</summary>
        /// <remarks>
        /// <para>
        /// 情境: 
        /// <para>1. Op 點選 UI 的 Load(含數量) 的命令後, 會自動尋找 狀態 為 sWaitingLoadInstruction 的 Drawer;符合條件Drawer的 Tray 退到 Out</para>
        /// <para>2. Tray 退到 Out 之後, Drawer 的狀態轉為 sLoadWaitingPutBoxOnTray ~ 等待放入 Box 到 Tray 上 </para>
        /// </para>
        /// </remarks>
        public void Load_MoveTrayToOut()
        {
            this.States[EnumMacCabinetDrawerState.LoadMoveTrayToOutStart.ToString()].ExecuteCommand(new MacStateEntryEventArgs());
        }

        /// <summary>Load, 將 Tray 移到 Home</summary>
        /// <remarks>
        /// <para>
        /// 情境: Op 將盒子放到 Tray 後, 按下Drawer 的 按鈕, 將 Tray 送到 Home, 檢查盒子在不在
        /// <para>
        /// 1. 没有盒子: Tray 回退到Out, 狀態重回 sLoadWaitingPutBoxOnTray(等待 Box 放到 Tray)
        /// </para>
        /// <para>
        /// 2. 有盒子: 狀態更新到  sLoadWaitingMoveTrayToIn (等待命令將 Tray 移到 In)
        /// </para>
        /// <para>
        /// 當Drawer 的Btton 被按下, 啟動 
        /// </para>
        /// </para>
        /// </remarks>
        public void Load_MoveTrayToHome()
        {

            this.States[EnumMacCabinetDrawerState.LoadMoveTrayToHomeStart.ToString()].ExecuteCommand(new MacStateEntryEventArgs());
        }



        /// <summary>Load,  Tray  移到 In</summary>
        /// <remarks>
        /// <para>
        /// 情境: 
        /// <para>1. BoxRobot 要夾取盒子到 Open Stage 先下本命令將 Tray 移到 In </para>
        /// <para>2. Tray 移到定位時,  狀態更新為 sLoadWaitingGetBoxOnTray (等待盒子被取走)</para>
        /// </para>
        /// </remarks>
        public void Load_MoveTrayToIn()
        {
            this.States[EnumMacCabinetDrawerState.LoadMoveTrayToInStart.ToString()].ExecuteCommand(new MacStateEntryEventArgs());
        }


        /// <summary> 盒子被取走後, 將 Tray 移到 Home, 等待 Unload 命令</summary>
        /// <remarks>
        /// <para>
        /// 情境:盒子被 BoxRobot取走之後, 下本指令將 Tray 移到 Home , 狀態更新至 sWaitingUnloadInstruction(等待接收 Unload 指令)
        /// </para>
        /// </remarks>
        public void MoveTrayToHomeWaitingUnloadInstruction()
        {
            this.States[EnumMacCabinetDrawerState.MoveTrayToHomeWaitingUnloadInstructionStart.ToString()].ExecuteCommand(new MacStateEntryEventArgs());
        }


        /// <summary>Unload, 將 Tray 移到 In</summary>
        /// <remarks>
        /// <para>
        /// 情境: Robot 要將盒子送回時,針對Drawer 下本命令, 將指定Drawer 的Tray 移到 In; Tray 到定位後, Drawer 狀態會更新到sUnloadWaitingPutBoxOnTray(等待盒子放到 Tray 上)
        /// </para>
        /// </remarks>
        public void Unload_MoveTrayToIn()
        {
            this.States[EnumMacCabinetDrawerState.UnloadMoveTrayToInStart.ToString()].ExecuteCommand(new MacStateEntryEventArgs());
        }



        /// <summary>Unload, 盒子放到 Tray 後, 將 Tray 移到 Home</summary>
        /// <remarks>
        /// <para>
        /// Tray 移到 Home 之後檢查 Tray 上有没有盒子
        /// <para>
        /// 1. 有盒子:自動將 Tray 送到 Out, 狀態會更新到 UnloadWaitingGetBoxOnTray(等待將 盒子拿走)
        /// </para>
        /// <para>
        /// 2. 没盒子:將狀態自動更新至 WaitingUnloadInstruction(等待 Unload 指令)
        /// </para>
        /// </para>
        /// <para>
        /// 情境: BoxRobot 將要 Unload 的盒子放到指定 Drawer 的 Tray 後, 下令將 Tray 送到 Home, Tray 到達定位時會檢查 Tray 上有没有盒子
        /// <para>1. 有盒子: 將 Tray 再移動到 Out, Tray 移到定位之後 Drawer 狀態更新為 sUnloadWaitingGetBoxOnTray(等待將 盒子拿走)</para>
        /// <para>2. 没盒子: 將 狀態變更為sWaitingUnloadInstruction(等待接收 Unload 指令)</para>
        /// </para>
        /// </remarks>
        public void Unload_MoveTrayToHome()
        {
            this.States[EnumMacCabinetDrawerState.UnloadMoveTrayToHomeStart.ToString()].ExecuteCommand(new MacStateEntryEventArgs());
        }


        /// <summary>將 Tray 移到 Home, 等待 Load 命令</summary>
        /// <remarks>
        /// <para>
        /// 情境: OP 取走盒子後, 按鈕 或透過 UI 操作, 將 Tray 送到 Home, 狀態更新到 sWaitingLoadInstruction(等待 Load 命令)
        /// </para>
        /// </remarks>
        public void MoveTrayToHomeWaitingLoadInstruction()
        {
            this.States[EnumMacCabinetDrawerState.MoveTrayToHomeWaitingLoadInstructionStart.ToString()].ExecuteCommand(new MacStateEntryEventArgs());
        }

        #endregion Command 

        /// <summary>下達Load 指令將 Tray 移到 Out 時動作完成的狀態 (到達Out 等待 將 Box 放到Tray 時的狀態) </summary>
        public MacState StateLoadWaitingPutBoxOnTray { get { return this.States[EnumMacCabinetDrawerState.LoadWaitingPutBoxOnTray.ToString()]; } }
        /// <summary>下達 Initial指令, 完成動作的狀態</summary>
        public MacState StateWaitingLoadInstruction { get { return this.States[EnumMacCabinetDrawerState.WaitingLoadInstruction.ToString()]; } }
        /// <summary>系統啟動後的狀態</summary>
        public MacState StateSystemBootup { get { return this.States[EnumMacCabinetDrawerState.SystemBootup.ToString()]; } }
        public override void LoadStateMachine()
        {
            try   // TODO: Remove Try catch 
            {
                if (HalCabinetUniversal.MacHalDrawer.PressButtonToLoad == null)
                {
                    HalCabinetUniversal.MacHalDrawer.PressButtonToLoad = this.Load_MoveTrayToHome;
                }
            }
            catch
            {

            }

            #region  state
            // 系統啟動
            MacState sSystemBootup = NewState(EnumMacCabinetDrawerState.SystemBootup);

            // 系統啟動後 Initial 
            MacState sSystemBootupInitialStart = NewState(EnumMacCabinetDrawerState.SystemBootupInitialStart);
            MacState sSystemBootupInitialIng = NewState(EnumMacCabinetDrawerState.SystemBootupInitialIng);
            MacState sSystemBootupInitialComplete = NewState(EnumMacCabinetDrawerState.SystemBootupInitialComplete);

            // 等待 Load 命令
            MacState sWaitingLoadInstruction = NewState(EnumMacCabinetDrawerState.WaitingLoadInstruction);

            // Load, 將 Tray 移到 Out
            MacState sLoadMoveTrayToOutStart = NewState(EnumMacCabinetDrawerState.LoadMoveTrayToOutStart);
            MacState sLoadMoveTrayToOutIng = NewState(EnumMacCabinetDrawerState.LoadMoveTrayToOutIng);
            MacState sLoadMoveTrayToOutComplete = NewState(EnumMacCabinetDrawerState.LoadMoveTrayToOutComplete);

            // Load, 等待 Box 放到 Tray
            MacState sLoadWaitingPutBoxOnTray = NewState(EnumMacCabinetDrawerState.LoadWaitingPutBoxOnTray);

            //--
            MacState sLoadMoveTrayToHomeStart = NewState(EnumMacCabinetDrawerState.LoadMoveTrayToHomeStart);
            MacState sLoadMoveTrayToHomeIng = NewState(EnumMacCabinetDrawerState.LoadMoveTrayToHomeIng);
            MacState sLoadMoveTrayToHomeComplete = NewState(EnumMacCabinetDrawerState.LoadMoveTrayToHomeComplete);
            MacState sLoadCheckBoxExistenceStart = NewState(EnumMacCabinetDrawerState.LoadCheckBoxExistenceStart);
            MacState sLoadCheckBoxExistenceIng = NewState(EnumMacCabinetDrawerState.LoadCheckBoxExistenceIng);
            MacState sLoadCheckBoxExistenceComplete = NewState(EnumMacCabinetDrawerState.LoadCheckBoxExistenceComplete);

            MacState sLoadWaitingMoveTrayToIn = NewState(EnumMacCabinetDrawerState.LoadWaitingMoveTrayToIn);

            MacState sLoadRejectTrayToOutStart = NewState(EnumMacCabinetDrawerState.LoadRejectTrayToOutStart);
            MacState sLoadRejectTrayToOutIng = NewState(EnumMacCabinetDrawerState.LoadRejectTrayToOutIng);
            MacState sLoadRejectTrayToOutComplete = NewState(EnumMacCabinetDrawerState.LoadRejectTrayToOutComplete);

            MacState sLoadMoveTrayToInStart = NewState(EnumMacCabinetDrawerState.LoadMoveTrayToInStart);
            MacState sLoadMoveTrayToInIng = NewState(EnumMacCabinetDrawerState.LoadMoveTrayToInIng);
            MacState sLoadMoveTrayToInComplete = NewState(EnumMacCabinetDrawerState.LoadMoveTrayToInComplete);
            MacState sLoadWaitingGetBoxOnTray = NewState(EnumMacCabinetDrawerState.LoadWaitingGetBoxOnTray);

            MacState sMoveTrayToHomeWaitingUnloadInstructionStart = NewState(EnumMacCabinetDrawerState.MoveTrayToHomeWaitingUnloadInstructionStart);
            MacState sMoveTrayToHomeWaitingUnloadInstructionIng = NewState(EnumMacCabinetDrawerState.MoveTrayToHomeWaitingUnloadInstructionIng);
            MacState sMoveTrayToHomeWaitingUnloadInstructionComplete = NewState(EnumMacCabinetDrawerState.MoveTrayToHomeWaitingUnloadInstructionComplete);

            MacState sWaitingUnloadInstruction = NewState(EnumMacCabinetDrawerState.WaitingUnloadInstruction);

            MacState sUnloadMoveTrayToInStart = NewState(EnumMacCabinetDrawerState.UnloadMoveTrayToInStart);
            MacState sUnloadMoveTrayToInIng = NewState(EnumMacCabinetDrawerState.UnloadMoveTrayToInIng);
            MacState sUnloadMoveTrayToInComplete = NewState(EnumMacCabinetDrawerState.UnloadMoveTrayToInComplete);

            MacState sUnloadWaitingPutBoxOnTray = NewState(EnumMacCabinetDrawerState.UnloadWaitingPutBoxOnTray);

            MacState sUnloadMoveTrayToHomeStart = NewState(EnumMacCabinetDrawerState.UnloadMoveTrayToHomeStart);
            MacState sUnloadMoveTrayToHomeIng = NewState(EnumMacCabinetDrawerState.UnloadMoveTrayToHomeIng);
            MacState sUnloadMoveTrayToHomeComplete = NewState(EnumMacCabinetDrawerState.UnloadMoveTrayToHomeComplete);

            MacState sUnloadCheckBoxExistenceStart = NewState(EnumMacCabinetDrawerState.UnloadCheckBoxExistenceStart);
            MacState sUnloadCheckBoxExistenceIng = NewState(EnumMacCabinetDrawerState.UnloadCheckBoxExistenceIng);
            MacState sUnloadCheckBoxExistenceComplete = NewState(EnumMacCabinetDrawerState.UnloadCheckBoxExistenceComplete);

            MacState sUnloadMoveTrayToOutStart = NewState(EnumMacCabinetDrawerState.UnloadMoveTrayToOutStart);
            MacState sUnloadMoveTrayToOutIng = NewState(EnumMacCabinetDrawerState.UnloadMoveTrayToOutIng);
            MacState sUnloadMoveTrayToOutComplete = NewState(EnumMacCabinetDrawerState.UnloadMoveTrayToOutComplete);

            MacState sUnloadWaitingGetBoxOnTray = NewState(EnumMacCabinetDrawerState.UnloadWaitingGetBoxOnTray);

            MacState sMoveTrayToHomeWaitingLoadInstructionStart = NewState(EnumMacCabinetDrawerState.MoveTrayToHomeWaitingLoadInstructionStart);
            MacState sMoveTrayToHomeWaitingLoadInstructionIng = NewState(EnumMacCabinetDrawerState.MoveTrayToHomeWaitingLoadInstructionIng);
            MacState sMoveTrayToHomeWaitingLoadInstructionComplete = NewState(EnumMacCabinetDrawerState.MoveTrayToHomeWaitingLoadInstructionComplete);
            #endregion state

            #region transition
            MacTransition tSystemBootup_SystemBootupInitialStart = NewTransition(sSystemBootup, sSystemBootupInitialStart, EnumMacCabinetDrawerTransition.SystemBootup_SystemBootupInitialStart);
            //--
            MacTransition tSystemBootupInitialStart_SystemBootupInitialIng = NewTransition(sSystemBootupInitialStart, sSystemBootupInitialIng, EnumMacCabinetDrawerTransition.SystemBootupInitialStart_SystemBootupInitialIng);
            MacTransition tSystemBootupInitialIng_SystemBootupInitialComplete = NewTransition(sSystemBootupInitialIng, sSystemBootupInitialComplete, EnumMacCabinetDrawerTransition.SystemBootupInitialIng_SystemBootupInitialComplete);
            MacTransition tSystemBootupInitialComplete_WaitingLoadInstruction = NewTransition(sSystemBootupInitialComplete, sWaitingLoadInstruction, EnumMacCabinetDrawerTransition.SystemBootupInitialComplete_WaitingLoadInstruction);
            MacTransition tWaitingLoadInstruction_NULL = NewTransition(sWaitingLoadInstruction, null, EnumMacCabinetDrawerTransition.WaitingLoadInstruction_NULL);
            //--
            MacTransition tLoadMoveTrayToOutStart_LoadMoveTrayToOutIng = NewTransition(sLoadMoveTrayToOutStart, sLoadMoveTrayToOutIng, EnumMacCabinetDrawerTransition.LoadMoveTrayToOutStart_LoadMoveTrayToOutIng);
            MacTransition tLoadMoveTrayToOutIng_LoadMoveTrayToOutComplete = NewTransition(sLoadMoveTrayToOutIng, sLoadMoveTrayToOutComplete, EnumMacCabinetDrawerTransition.LoadMoveTrayToOutIng_LoadMoveTrayToOutComplete);
            MacTransition tLoadMoveTrayToOutComplete_LoadWaitingPutBoxOnTray = NewTransition(sLoadMoveTrayToOutComplete, sLoadWaitingPutBoxOnTray, EnumMacCabinetDrawerTransition.LoadMoveTrayToOutComplete_LoadWaitingPutBoxOnTray);
            MacTransition tLoadWaitingPutBoxOnTray_NULL = NewTransition(sLoadWaitingPutBoxOnTray, null, EnumMacCabinetDrawerTransition.LoadWaitingPutBoxOnTray_NULL);
            //--
            MacTransition tLoadMoveTrayToHomeStart_LoadMoveTrayToHomeIng = NewTransition(sLoadMoveTrayToHomeStart, sLoadMoveTrayToHomeIng, EnumMacCabinetDrawerTransition.LoadMoveTrayToHomeStart_LoadMoveTrayToHomeIng);
            MacTransition tLoadMoveTrayToHomeIng_LoadMoveTrayToHomeComplete = NewTransition(sLoadMoveTrayToHomeIng, sLoadMoveTrayToHomeComplete, EnumMacCabinetDrawerTransition.LoadMoveTrayToHomeIng_LoadMoveTrayToHomeComplete);
            MacTransition tLoadMoveTrayToHomeComplete_LoadCheckBoxExistenceStart = NewTransition(sLoadMoveTrayToHomeComplete, sLoadCheckBoxExistenceStart, EnumMacCabinetDrawerTransition.LoadMoveTrayToHomeComplete_LoadCheckBoxExistenceStart);
            MacTransition tLoadCheckBoxExistenceStart_LoadCheckBoxExistenceIng = NewTransition(sLoadCheckBoxExistenceStart, sLoadCheckBoxExistenceIng, EnumMacCabinetDrawerTransition.LoadCheckBoxExistenceStart_LoadCheckBoxExistenceIng);
            MacTransition tLoadCheckBoxExistenceIng_LoadCheckBoxExistenceComplete = NewTransition(sLoadCheckBoxExistenceIng, sLoadCheckBoxExistenceComplete, EnumMacCabinetDrawerTransition.LoadCheckBoxExistenceIng_LoadCheckBoxExistenceComplete);

            MacTransition tLoadCheckBoxExistenceComplete_LoadWaitingMoveTrayToIn = NewTransition(sLoadCheckBoxExistenceComplete, sLoadWaitingMoveTrayToIn, EnumMacCabinetDrawerTransition.LoadCheckBoxExistenceComplete_LoadWaitingMoveTrayToIn);
            MacTransition tLoadWaitingMoveTrayToIn_NULL = NewTransition(sLoadWaitingMoveTrayToIn, null, EnumMacCabinetDrawerTransition.LoadWaitingMoveTrayToIn_NULL);

            MacTransition tLoadCheckBoxExistenceComplete_LoadRejectToOutStart = NewTransition(sLoadCheckBoxExistenceComplete, sLoadRejectTrayToOutStart, EnumMacCabinetDrawerTransition.LoadCheckBoxExistenceComplete_LoadRejectToOutStart);
            MacTransition tLoadRejectTrayToOutStart_LoadRejectTrayToOutIng = NewTransition(sLoadRejectTrayToOutStart, sLoadRejectTrayToOutIng, EnumMacCabinetDrawerTransition.LoadRejectToOutStart_LoadRejectToOutIng);
            MacTransition tLoadRejectTrayToOutIng_LoadRejectTrayToOutComplete = NewTransition(sLoadRejectTrayToOutIng, sLoadRejectTrayToOutComplete, EnumMacCabinetDrawerTransition.LoadRejectToOutIng_LoadRejectToOutComplete);
            MacTransition tLoadRejectTrayToOutComplete_LoadWaitingPutBoxOnTray = NewTransition(sLoadRejectTrayToOutComplete, sLoadWaitingPutBoxOnTray, EnumMacCabinetDrawerTransition.LoadRejectToOutComplete_LoadWaitingPutBoxOnTray);

            MacTransition tLoadMoveTrayToInStart_LoadMoveTrayToInIng = NewTransition(sLoadMoveTrayToInStart, sLoadMoveTrayToInIng, EnumMacCabinetDrawerTransition.LoadMoveTrayToInStart_LoadMoveTrayToInIng);
            MacTransition tLoadMoveTrayToInIng_LoadMoveTrayToInComplete = NewTransition(sLoadMoveTrayToInIng, sLoadMoveTrayToInComplete, EnumMacCabinetDrawerTransition.LoadMoveTrayToInIng_LoadMoveTrayToInComplete);
            MacTransition tLoadMoveTrayToInComplete_LoadWaitingGetBoxOnTray = NewTransition(sLoadMoveTrayToInComplete, sLoadWaitingGetBoxOnTray, EnumMacCabinetDrawerTransition.LoadMoveTrayToInComplete_LoadWaitingGetBoxOnTray);
            MacTransition tLoadWaitingGetBoxOnTray_NULL = NewTransition(sLoadWaitingGetBoxOnTray, null, EnumMacCabinetDrawerTransition.LoadWaitingGetBoxOnTray_NULL);

            MacTransition tMoveTrayToHomeWaitingUnloadInstructionStart_MoveTrayToHomeWaitingUnloadInstructionIng = NewTransition(sMoveTrayToHomeWaitingUnloadInstructionStart, sMoveTrayToHomeWaitingUnloadInstructionIng,
                                                                                                        EnumMacCabinetDrawerTransition.MoveTrayToHomeWaitingUnloadInstructionStart_MoveTrayToHomeWaitingUnloadInstructionIng);
            MacTransition tMoveTrayToHomeWaitingUnloadInstructionIng_MoveTrayToHomeWaitingUnloadInstructionComplete = NewTransition(sMoveTrayToHomeWaitingUnloadInstructionIng, sMoveTrayToHomeWaitingUnloadInstructionComplete,
                                                                                                       EnumMacCabinetDrawerTransition.MoveTrayToHomeWaitingUnloadInstructionIng_MoveTrayToHomeWaitingUnloadInstructionComplete);
            MacTransition tMoveTrayToHomeWaitingUnloadInstructionComplete_WaitingUnloadInstruction = NewTransition(sMoveTrayToHomeWaitingUnloadInstructionComplete, sWaitingUnloadInstruction,
                                                                                                       EnumMacCabinetDrawerTransition.MoveTrayToHomeWaitingUnloadInstructionComplete_WaitingUnloadInstruction);
            MacTransition tWaitingUnloadInstruction_NULL = NewTransition(sWaitingUnloadInstruction, null, EnumMacCabinetDrawerTransition.WaitingUnloadInstruction_NULL);

            MacTransition tUnloadMoveTrayToInStart_UnloadMoveTrayToInIng = NewTransition(sUnloadMoveTrayToInStart, sUnloadMoveTrayToInIng, EnumMacCabinetDrawerTransition.UnloadMoveTrayToInStart_UnloadMoveTrayToInIng);
            MacTransition tUnloadMoveTrayToInIng_UnloadMoveTrayToInComplete = NewTransition(sUnloadMoveTrayToInIng, sUnloadMoveTrayToInComplete, EnumMacCabinetDrawerTransition.UnloadMoveTrayToInIng_UnloadMoveTrayToInComplete);
            MacTransition tUnloadMoveTrayToInComplete_UnloadWaitingPutBoxOnTray = NewTransition(sUnloadMoveTrayToInComplete, sUnloadWaitingPutBoxOnTray, EnumMacCabinetDrawerTransition.UnloadMoveTrayToInComplete_UnloadWaitingPutBoxOnTray);
            MacTransition tUnloadWaitingPutBoxOnTray_NULL = NewTransition(sUnloadWaitingPutBoxOnTray, null, EnumMacCabinetDrawerTransition.UnloadWaitingPutBoxOnTray_NULL);

            MacTransition tUnloadMoveTrayToHomeStart_UnloadMoveTrayToHomeIng = NewTransition(sUnloadMoveTrayToHomeStart, sUnloadMoveTrayToHomeIng,
                                                                                  EnumMacCabinetDrawerTransition.UnloadMoveTrayToHomeStart_UnloadMoveTrayToHomeIng);
            MacTransition tUnloadMoveTrayToHomeIng_UnloadMoveTrayToHomeComplete = NewTransition(sUnloadMoveTrayToHomeIng, sUnloadMoveTrayToHomeComplete,
                                                                                  EnumMacCabinetDrawerTransition.UnloadMoveTrayToHomeIng_UnloadMoveTrayToHomeComplete);
            MacTransition tUnloadMoveTrayToHomeComplete_UnloadCheckBoxExistenceStart = NewTransition(sUnloadMoveTrayToHomeComplete, sUnloadCheckBoxExistenceStart,
                                                                                  EnumMacCabinetDrawerTransition.UnloadMoveTrayToHomeComplete_UnloadCheckBoxExistenceStart);
            MacTransition tUnloadCheckBoxExistenceStart_UnloadCheckBoxExistenceIng = NewTransition(sUnloadCheckBoxExistenceStart, sUnloadCheckBoxExistenceIng,
                                                                                 EnumMacCabinetDrawerTransition.UnloadCheckBoxExistenceStart_UnloadCheckBoxExistenceIng);
            MacTransition tUnloadCheckBoxExistenceIng_UnloadCheckBoxExistenceComplete = NewTransition(sUnloadCheckBoxExistenceIng, sUnloadCheckBoxExistenceComplete,
                                                                                 EnumMacCabinetDrawerTransition.UnloadCheckBoxExistenceIng_UnloadCheckBoxExistenceComplete);
            MacTransition tUnloadCheckBoxExistenceComplete_WaitingUnloadInstruction = NewTransition(sUnloadCheckBoxExistenceComplete, sWaitingUnloadInstruction,
                                                                                 EnumMacCabinetDrawerTransition.UnloadCheckBoxExistenceComplete_UnloadMoveTrayToOutStart);
            MacTransition tUnloadCheckBoxExistenceComplete_UnloadMoveTrayToOutStart = NewTransition(sUnloadCheckBoxExistenceComplete, sUnloadMoveTrayToOutStart,
                                                                                 EnumMacCabinetDrawerTransition.UnloadCheckBoxExistenceComplete_UnloadMoveTrayToOutStart);
            MacTransition tUnloadMoveTrayToOutStart_UnloadMoveTrayToOutIng = NewTransition(sUnloadMoveTrayToOutStart, sUnloadMoveTrayToOutIng,
                                                                                EnumMacCabinetDrawerTransition.UnloadMoveTrayToOutStart_UnloadMoveTrayToOutIng);
            MacTransition tUnloadMoveTrayToOutIng_UnloadMoveTrayToOutComplete = NewTransition(sUnloadMoveTrayToOutIng, sUnloadMoveTrayToOutComplete,
                                                                                EnumMacCabinetDrawerTransition.UnloadMoveTrayToOutIng_UnloadMoveTrayToOutComplete);
            MacTransition tUnloadMoveTrayToOutComplete_UnloadWaitingGetBoxOnTray = NewTransition(sUnloadMoveTrayToOutComplete, sUnloadWaitingGetBoxOnTray,
                                                                               EnumMacCabinetDrawerTransition.UnloadMoveTrayToOutComplete_UnloadWaitingGetBoxOnTray);
            MacTransition tUnloadWaitingGetBoxOnTray_NULL = NewTransition(sUnloadWaitingGetBoxOnTray, null,
                                                                               EnumMacCabinetDrawerTransition.UnloadWaitingGetBoxOnTray_NULL);


            MacTransition tMoveTrayToHomeWaitingLoadInstructionStart_MoveTrayToHomeWaitingLoadInstructionIng = NewTransition(sMoveTrayToHomeWaitingLoadInstructionStart, sMoveTrayToHomeWaitingLoadInstructionIng,
                                                                                                        EnumMacCabinetDrawerTransition.MoveTrayToHomeWaitingLoadInstructionStart_MoveTrayToHomeWaitingLoadInstructionIng);
            MacTransition tMoveTrayToHomeWaitingLoadInstructionIng_MoveTrayToHomeWaitingLoadInstructionComplete = NewTransition(sMoveTrayToHomeWaitingLoadInstructionIng, sMoveTrayToHomeWaitingLoadInstructionComplete,
                                                                                                       EnumMacCabinetDrawerTransition.MoveTrayToHomeWaitingLoadInstructionIng_MoveTrayToHomeWaitingLoadInstructionComplete);
            MacTransition tMoveTrayToHomeWaitingLoadInstructionComplete_WaitingLoadInstruction = NewTransition(sMoveTrayToHomeWaitingLoadInstructionComplete, sWaitingLoadInstruction,
                                                                                                       EnumMacCabinetDrawerTransition.MoveTrayToHomeWaitingLoadInstructionComplete_WaitingLoadInstruction);

            //

            #endregion transition

            #region event
            sSystemBootup.OnEntry += (sender, e) =>
            {  // Sync
                Debug.WriteLine("SystemBootup.OnEntry; Device Index=" + this.DeviceIndex);
                this.SetCurrentState((MacState)sender);
                var transition = tSystemBootup_SystemBootupInitialStart;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something   
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sSystemBootup.OnExit += (sender, e) =>
            {
                Debug.WriteLine("SystemBootup.OnExit; Device Index=" + this.DeviceIndex);
            };

            sSystemBootupInitialStart.OnEntry += (sender, e) =>
             { // Sync
                 Debug.WriteLine("SystemBootupInitialStart.OnEntry; Device Index=" + this.DeviceIndex);
                 this.SetCurrentState((MacState)sender);
                 var transition = tSystemBootupInitialStart_SystemBootupInitialIng;
                 var triggerMember = new TriggerMember
                 {
                     Action = (parameter) =>
                     { this.HalDrawer.CommandINI(); },
                     ActionParameter = null,
                     ExceptionHandler = (state, ex) =>
                     {
                         // do something
                     },
                     Guard = () => { return true; },
                     NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                     NotGuardException = null,
                     ThisStateExitEventArgs = new MacStateExitEventArgs()
                 };
                 transition.SetTriggerMembers(triggerMember);
                 this.Trigger(transition);
             };
            sSystemBootupInitialStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("SystemBootupInitialStart.OnExit; Device Index=" + this.DeviceIndex);
            };

            sSystemBootupInitialIng.OnEntry += (sender, e) =>
            {  // Async
                Debug.WriteLine("SystemBootupInitialIng.OnEntry; Device Index=" + this.DeviceIndex);
                this.SetCurrentState((MacState)sender);
                var transition = tSystemBootupInitialIng_SystemBootupInitialComplete;
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = (startTime) =>
                    {
                        var rtnV = false;
                        if (this.HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionHome)
                        {
                            rtnV = true;
                        }
                        else if (this.HalDrawer.CurrentWorkState == DrawerWorkState.InitialFailed)
                        {
                            throw new DrawerInitialFailException();
                        }
                        else if (TimeoutObject.IsTimeOut(startTime))
                        {
                            throw new DrawerInitialTimeOutException();
                        }
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                this.TriggerAsync(transition);
            };
            sSystemBootupInitialIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("SystemBootupInitialIng.OnExit; Device Index=" + this.DeviceIndex);
            };

            sSystemBootupInitialComplete.OnEntry += (sender, e) =>
            { // Sync
                Debug.WriteLine("SystemBootupInitialComplete.OnEntry; Device Index=" + this.DeviceIndex);
                this.SetCurrentState((MacState)sender);
                var transition = tSystemBootupInitialComplete_WaitingLoadInstruction;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        //  do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                this.Trigger(transition);
            };
            sSystemBootupInitialComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("SystemBootupInitialComplete.OnExit; Device Index=" + this.DeviceIndex);
            };

            sWaitingLoadInstruction.OnEntry += (sender, e) =>
              { //Sync
                  Debug.WriteLine("WaitingLoadInstruction.OnEntry; Device Index=" + this.DeviceIndex);
                  this.SetCurrentState((MacState)sender);
                  var transition = tWaitingLoadInstruction_NULL;
                  var triggerMember = new TriggerMember
                  {
                      Action = null,
                      ActionParameter = null,
                      ExceptionHandler = (state, ex) =>
                      {
                          // do something
                      },
                      Guard = () => { return true; },
                      NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                      NotGuardException = null,
                      ThisStateExitEventArgs = new MacStateExitEventArgs()
                  };
                  transition.SetTriggerMembers(triggerMember);
                  this.Trigger(transition);
              };
            sWaitingLoadInstruction.OnExit += (sender, e) =>
            {
                Debug.WriteLine("WaitingLoadInstruction.OnExit; Device Index=" + this.DeviceIndex);
            };

            sLoadMoveTrayToOutStart.OnEntry += (sender, e) =>
              {   // Sync
                Debug.WriteLine("LoadMoveTrayToOutStart.OnEntry; Device Index=" + this.DeviceIndex);
                  var transition = tLoadMoveTrayToOutStart_LoadMoveTrayToOutIng;
                  this.SetCurrentState((MacState)sender);
                  var triggerMember = new TriggerMember
                  {
                      Action = (parameter) => { this.HalDrawer.CommandTrayMotionOut(); },
                      ActionParameter = null,
                      ExceptionHandler = (state, ex) =>
                      {
                        // do something
                    },
                      Guard = () => { return true; },
                      NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                      NotGuardException = null,
                      ThisStateExitEventArgs = new MacStateExitEventArgs(),
                  };
                  transition.SetTriggerMembers(triggerMember);
                  Trigger(transition);
              };
            sLoadMoveTrayToOutStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadMoveTrayToOutStart.OnExit; Device Index=" + this.DeviceIndex);
            };

            sLoadMoveTrayToOutIng.OnEntry += (sender, e) =>
            {// Async
                Debug.WriteLine("LoadMoveTrayToOutIng.OnEntry; Device Index=" + this.DeviceIndex);
                var transition = tLoadMoveTrayToOutIng_LoadMoveTrayToOutComplete;
                this.SetCurrentState((MacState)sender);

                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something  
                    },
                    Guard = (startTime) =>
                    {
                        bool rtnV = false;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionOut)
                        {
                            rtnV = true;
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            throw new DrawerLoadMoveTrayToPositionOutFailException();
                        }
                        else if (TimeoutObject.IsTimeOut(startTime))
                        {
                            throw new DrawerLoadMoveTrayToPositionOutTimeOutException();
                        }
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sLoadMoveTrayToOutIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadMoveTrayToOutIng.OnExit; Device Index=" + this.DeviceIndex);
            };


            sLoadMoveTrayToOutComplete.OnEntry += (sender, e) =>
              {// Sync
                Debug.WriteLine("LoadMoveTrayToOutComplete.OnEntry; Device Index=" + this.DeviceIndex);
                  var transition = tLoadMoveTrayToOutComplete_LoadWaitingPutBoxOnTray;
                  SetCurrentState((MacState)sender);
                  var triggerMember = new TriggerMember
                  {
                      Action = null,
                      ActionParameter = null,
                      ExceptionHandler = (state, ex) =>
                        {
                          // do something
                      },
                      Guard = () => { return true; },
                      NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                      NotGuardException = null,
                      ThisStateExitEventArgs = new MacStateExitEventArgs(),
                  };
                  transition.SetTriggerMembers(triggerMember);
                  Trigger(transition);
              };
            sLoadMoveTrayToOutComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadMoveTrayToOutComplete.OnExit; Device Index=" + this.DeviceIndex);
            };

            sLoadWaitingPutBoxOnTray.OnEntry += (sender, e) =>
            {  // Sync
                Debug.WriteLine("LoadWaitingPutBoxOnTray.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tLoadWaitingPutBoxOnTray_NULL;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLoadWaitingPutBoxOnTray.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadWaitingPutBoxOnTray.OnExit; Device Index=" + this.DeviceIndex);
            };

            sLoadMoveTrayToHomeStart.OnEntry += (sender, e) =>
            { // Sync
                Debug.WriteLine("LoadMoveTrayToHomeStart.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tLoadMoveTrayToHomeStart_LoadMoveTrayToHomeIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { this.HalDrawer.CommandTrayMotionHome(); },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLoadMoveTrayToHomeStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadMoveTrayToHomeStart.OnExit; Device Index=" + this.DeviceIndex);
            };

            sLoadMoveTrayToHomeIng.OnEntry += (sender, e) =>
            {
                //Async
                Debug.WriteLine("LoadMoveTrayToHomeIng.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tLoadMoveTrayToHomeIng_LoadMoveTrayToHomeComplete;

                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = (startTime) =>
                    {
                        var rtnV = false;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionHome)
                        {
                            rtnV = true;
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            throw new DrawerLoadMoveTrayToPositionHomeFailException();
                        }
                        else if (TimeoutObject.IsTimeOut(startTime))
                        {
                            throw new DrawerLoadMoveTrayToPositionHomeTimeOutException();
                        }
                        return rtnV;
                    },
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sLoadMoveTrayToHomeIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadMoveTrayToHomeIng.OnExit; Device Index=" + this.DeviceIndex);
            };

            sLoadMoveTrayToHomeComplete.OnEntry += (sender, e) =>
            {  // Sync
                Debug.WriteLine("LoadMoveTrayToHomeComplete.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tLoadMoveTrayToHomeComplete_LoadCheckBoxExistenceStart;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLoadMoveTrayToHomeComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadMoveTrayToHomeComplete.OnExit; Device Index=" + this.DeviceIndex);
            };

            sLoadCheckBoxExistenceStart.OnEntry += (sender, e) =>
            {  // Sync
                Debug.WriteLine("LoadCheckBoxExistenceStart.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tLoadCheckBoxExistenceStart_LoadCheckBoxExistenceIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { HalDrawer.CommandBoxDetection(); },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLoadCheckBoxExistenceStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadCheckBoxExistenceStart.OnExit; Device Index=" + this.DeviceIndex);
            };

            sLoadCheckBoxExistenceIng.OnEntry += (sender, e) =>
            {  // Async
                Debug.WriteLine("LoadCheckBoxExistenceIng.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tLoadCheckBoxExistenceIng_LoadCheckBoxExistenceComplete;

                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = (startTime) =>
                    {
                        var rtnV = false;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.BoxExist || HalDrawer.CurrentWorkState == DrawerWorkState.BoxNotExist)
                        {
                            rtnV = true;
                        }
                        else if (TimeoutObject.IsTimeOut(startTime))
                        {
                            throw new DrawerLoadCheckBoxExistanceAtPositionHomeTimeOutException();
                        }
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sLoadCheckBoxExistenceIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadCheckBoxExistenceIng.OnExit; Device Index=" + this.DeviceIndex);
            };

            sLoadCheckBoxExistenceComplete.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("LoadCheckBoxExistenceComplete.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                MacTransition transition = null;
                TriggerMember triggerMember = null;
                if (HalDrawer.CurrentWorkState == DrawerWorkState.BoxExist)
                {
                    transition = tLoadCheckBoxExistenceComplete_LoadWaitingMoveTrayToIn;
                    triggerMember = new TriggerMember
                    {
                        Action = null,
                        ActionParameter = null,
                        ExceptionHandler = (state, ex) =>
                        {
                            // do something
                        },
                        Guard = () => { return true; },
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                        NotGuardException = null,
                        ThisStateExitEventArgs = new MacStateExitEventArgs()
                    };
                }
                else //if(HalDrawer.CurrentWorkState == DrawerWorkState.BoxNotExist)
                {
                    transition = tLoadCheckBoxExistenceComplete_LoadRejectToOutStart;
                    triggerMember = new TriggerMember
                    {
                        Action = null,
                        ActionParameter = null,
                        ExceptionHandler = (state, ex) =>
                        {
                            // do something
                        },
                        Guard = () => { return true; },
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                        NotGuardException = null,
                        ThisStateExitEventArgs = new MacStateExitEventArgs()
                    };
                }
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLoadCheckBoxExistenceComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadCheckBoxExistenceComplete.OnExit; Device Index=" + this.DeviceIndex);
            };
            sLoadWaitingMoveTrayToIn.OnEntry += (sender, ex) =>
               {    // Sync 
                Debug.WriteLine("LoadWaitingMoveTrayToIn.OnEntry; Device Index=" + this.DeviceIndex);
                   SetCurrentState((MacState)sender);
                   var transition = tLoadWaitingMoveTrayToIn_NULL;
                   var triggerMember = new TriggerMember
                   {
                       Action = null,
                       ActionParameter = null,
                       ExceptionHandler = (state, e) =>
                       {
                        // do something
                    },
                       Guard = () => { return true; },
                       NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                       NotGuardException = null,
                       ThisStateExitEventArgs = new MacStateExitEventArgs()
                   };
                   transition.SetTriggerMembers(triggerMember);
                   Trigger(transition);
               };
            sLoadWaitingMoveTrayToIn.OnExit += (sender, ex) =>
            {
                Debug.WriteLine("LoadWaitingMoveTrayToIn.OnExit; Device Index=" + this.DeviceIndex);
            };

            sLoadRejectTrayToOutStart.OnEntry += (sender, e) =>
            {  // Sync
                Debug.WriteLine("LoadRejectTrayToOutStart.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tLoadRejectTrayToOutStart_LoadRejectTrayToOutIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { HalDrawer.CommandTrayMotionOut(); },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // to something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLoadRejectTrayToOutStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadRejectTrayToOutStart.OnExit; Device Index=" + this.DeviceIndex);
            };

            sLoadRejectTrayToOutIng.OnEntry += (sender, e) =>
            { //Async
                Debug.WriteLine("LoadRejectTrayToOutIng.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tLoadRejectTrayToOutIng_LoadRejectTrayToOutComplete;

                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = (startTime) =>
                    {
                        var rtnV = false;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionOut)
                        {
                            rtnV = true;
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            throw new DrawerLoadNoBoxRejectTrayToPositionOutFromPositionHomeFailException();
                        }
                        else if (TimeoutObject.IsTimeOut(startTime))
                        {
                            throw new DrawerLoadNoBoxRejectTrayToPositionOutFromPositionHomeTimeOutException();
                        }
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sLoadRejectTrayToOutIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadRejectTrayToOutIng.OnExit; Device Index=" + this.DeviceIndex);
            };

            sLoadRejectTrayToOutComplete.OnEntry += (sender, e) =>
            {  // Sync
                Debug.WriteLine("LoadRejectTrayToOutComplete.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tLoadRejectTrayToOutComplete_LoadWaitingPutBoxOnTray;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLoadRejectTrayToOutComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadRejectTrayToOutComplete.OnExit; Device Index=" + this.DeviceIndex);
            };

            sLoadMoveTrayToInStart.OnEntry += (sender, e) =>
            { // Sync
                Debug.WriteLine("LoadMoveTrayToInStart.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tLoadMoveTrayToInStart_LoadMoveTrayToInIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { this.HalDrawer.CommandTrayMotionIn(); },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLoadMoveTrayToInStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadMoveTrayToInStart.OnExit; Device Index=" + this.DeviceIndex);
            };

            sLoadMoveTrayToInIng.OnEntry += (sender, e) =>
            { // Async
                Debug.WriteLine("LoadMoveTrayToInIng.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tLoadMoveTrayToInIng_LoadMoveTrayToInComplete;

                TriggerMemberAsync triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = (startTime) =>
                    {
                        var rtnV = false;
                        if (DrawerWorkState.TrayArriveAtPositionIn == HalDrawer.CurrentWorkState)
                        {
                            rtnV = true;
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            throw new DrawerLoadMoveTrayToPositionInFailException();
                        }
                        else if (TimeoutObject.IsTimeOut(startTime))
                        {
                            throw new DrawerLoadMoveTrayToPositionInTimeOutException();
                        }
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sLoadMoveTrayToInIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadMoveTrayToInIng.OnExit; Device Index=" + this.DeviceIndex);
            };

            sLoadMoveTrayToInComplete.OnEntry += (sender, e) =>
            {
                // Sync
                Debug.WriteLine("LoadMoveTrayToInComplete.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tLoadMoveTrayToInComplete_LoadWaitingGetBoxOnTray;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLoadMoveTrayToInComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadMoveTrayToInComplete.OnExit; Device Index=" + this.DeviceIndex);
            };

            sLoadWaitingGetBoxOnTray.OnEntry += (sender, e) =>
            { // Sync
                Debug.WriteLine("LoadWaitingGetBoxOnTray.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tLoadWaitingGetBoxOnTray_NULL;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {

                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLoadWaitingGetBoxOnTray.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadWaitingGetBoxOnTray.OnExit; Device Index=" + this.DeviceIndex);
            };

            sMoveTrayToHomeWaitingUnloadInstructionStart.OnEntry += (sender, e) =>
            { //Sync
                Debug.WriteLine("MoveTrayToHomeWaitingUnloadInstructionStart.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tMoveTrayToHomeWaitingUnloadInstructionStart_MoveTrayToHomeWaitingUnloadInstructionIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { HalDrawer.CommandTrayMotionHome(); },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMoveTrayToHomeWaitingUnloadInstructionStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("MoveTrayToHomeWaitingUnloadInstructionStart.OnExit; Device Index=" + this.DeviceIndex);
            };

            sMoveTrayToHomeWaitingUnloadInstructionIng.OnEntry += (sender, e) =>
            { // Async
                Debug.WriteLine("MoveTrayToHomeWaitingUnloadInstructionIng.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tMoveTrayToHomeWaitingUnloadInstructionIng_MoveTrayToHomeWaitingUnloadInstructionComplete;

                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = (startTime) =>
                    {
                        var rtnV = false;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionHome)
                        {
                            rtnV = true;
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            throw new DrawerMoveTrayToHomeWaitingUnloadInstructionFailException();
                        }
                        else if (TimeoutObject.IsTimeOut(startTime))
                        {
                            throw new DrawerMoveTrayToHomeWaitingUnloadInstructionTimeOutException();
                        }
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sMoveTrayToHomeWaitingUnloadInstructionIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("MoveTrayToHomeWaitingUnloadInstructionIng.OnExit; Device Index=" + this.DeviceIndex);
            };

            sMoveTrayToHomeWaitingUnloadInstructionComplete.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("MoveTrayToHomeWaitingUnloadInstructionComplete.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tMoveTrayToHomeWaitingUnloadInstructionComplete_WaitingUnloadInstruction;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        //do something 
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMoveTrayToHomeWaitingUnloadInstructionComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("MoveTrayToHomeWaitingUnloadInstructionComplete.OnExit; Device Index=" + this.DeviceIndex);
            };

            sWaitingUnloadInstruction.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("WaitingUnloadInstruction.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tWaitingUnloadInstruction_NULL;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // to something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sWaitingUnloadInstruction.OnExit += (sender, e) =>
            {
                Debug.WriteLine("WaitingUnloadInstruction.OnExit; Device Index=" + this.DeviceIndex);
            };

            sUnloadMoveTrayToInStart.OnEntry += (sender, e) =>
            { // Sync
                Debug.WriteLine("UnloadMoveTrayToInStart.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tUnloadMoveTrayToInStart_UnloadMoveTrayToInIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { this.HalDrawer.CommandTrayMotionIn(); },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something;
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sUnloadMoveTrayToInStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("UnloadMoveTrayToInStart.OnExit; Device Index=" + this.DeviceIndex);
            };

            sUnloadMoveTrayToInIng.OnEntry += (sender, e) =>
            {  // Async
                Debug.WriteLine("UnloadMoveTrayToInIng.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tUnloadMoveTrayToInIng_UnloadMoveTrayToInComplete;

                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = (startTime) =>
                    {
                        var rtnV = false;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionIn)
                        {
                            rtnV = true;
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            throw new DrawerUnloadMoveTrayToPositionInFailException();
                        }
                        else if (TimeoutObject.IsTimeOut(startTime))
                        {
                            throw new DrawerUnloadMoveTrayToPositionInTimeOutException();
                        }
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sUnloadMoveTrayToInIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("UnloadMoveTrayToInIng.OnExit; Device Index=" + this.DeviceIndex);
            };

            sUnloadMoveTrayToInComplete.OnEntry += (sender, e) =>
            {  // Sync
                SetCurrentState((MacState)sender);
                Debug.WriteLine("UnloadMoveTrayToInComplete.OnEntry; Device Index=" + this.DeviceIndex);
                var transition = tUnloadMoveTrayToInComplete_UnloadWaitingPutBoxOnTray;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);

            };
            sUnloadMoveTrayToInComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("UnloadMoveTrayToInComplete.OnExit; Device Index=" + this.DeviceIndex);
            };

            /**unload, 等待將Box 放在 Tray 上 
             */
            sUnloadWaitingPutBoxOnTray.OnEntry += (sender, e) =>
              {// sync
                  Debug.WriteLine("UnloadWaitingPutBoxOnTray.OnEntry; Device Index=" + this.DeviceIndex);
                  SetCurrentState((MacState)sender);
                  var transition = tUnloadWaitingPutBoxOnTray_NULL;
                  var triggerMember = new TriggerMember
                  {
                      Action = null,
                      ActionParameter = null,
                      ExceptionHandler = (state, ex) =>
                      {
                          // do somethingf
                      },
                      Guard = () => { return true; },
                      NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                      NotGuardException = null,
                      ThisStateExitEventArgs = new MacStateExitEventArgs()
                  };
                  transition.SetTriggerMembers(triggerMember);
                  Trigger(transition);
              };
            sUnloadWaitingPutBoxOnTray.OnExit += (sender, e) =>
            {
                Debug.WriteLine("UnloadWaitingPutBoxOnTray.OnExit; Device Index=" + this.DeviceIndex);
            };

            #region Unload,將 Tray 送到 Home
            sUnloadMoveTrayToHomeStart.OnEntry += (sender, e) =>
            {// Sync
                Debug.WriteLine("UnloadMoveTrayToHomeStart.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tUnloadMoveTrayToHomeStart_UnloadMoveTrayToHomeIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { HalDrawer.CommandTrayMotionHome(); },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something,
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sUnloadMoveTrayToHomeStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("UnloadMoveTrayToHomeStart.OnExit; Device Index=" + this.DeviceIndex);
            };

            sUnloadMoveTrayToHomeIng.OnEntry += (sender, e) =>
            {   // Async
                Debug.WriteLine("UnloadMoveTrayToHomeIng.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tUnloadMoveTrayToHomeIng_UnloadMoveTrayToHomeComplete;
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {

                    },
                    Guard = (startTime) =>
                    {
                        var rtnV = false;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionHome)
                        {
                            rtnV = true;
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            throw new DrawerUnloadMoveTrayToPositionHomeFailException();
                        }
                        else if (TimeoutObject.IsTimeOut(startTime))
                        {
                            throw new DrawerUnloadMoveTrayToPositionHomeTimeOutException();
                        }
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sUnloadMoveTrayToHomeIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("UnloadMoveTrayToHomeIng.OnExit; Device Index=" + this.DeviceIndex);
            };

            sUnloadMoveTrayToHomeComplete.OnEntry += (sender, e) =>
            {   // Sync
                Debug.WriteLine("UnloadMoveTrayToHomeComplete.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tUnloadMoveTrayToHomeComplete_UnloadCheckBoxExistenceStart;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sUnloadMoveTrayToHomeComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("UnloadMoveTrayToHomeComplete.OnExit; Device Index=" + this.DeviceIndex);
            };
            #endregion 將 Tray 送到 Home

            #region Unload, 檢查有没有盒子
            sUnloadCheckBoxExistenceStart.OnEntry += (sender, e) =>
            {  // sync
                Debug.WriteLine("UnloadCheckBoxExistenceStart.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tUnloadCheckBoxExistenceStart_UnloadCheckBoxExistenceIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { HalDrawer.CommandBoxDetection(); },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sUnloadCheckBoxExistenceStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("UnloadCheckBoxExistenceStart.OnExit; Device Index=" + this.DeviceIndex);
            };

            sUnloadCheckBoxExistenceIng.OnEntry += (sender, e) =>
            {   // Async
                Debug.WriteLine("UnloadCheckBoxExistenceIng.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tUnloadCheckBoxExistenceIng_UnloadCheckBoxExistenceComplete;
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = (startTime) =>
                    {
                        var rtnV = false;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.BoxExist || HalDrawer.CurrentWorkState == DrawerWorkState.BoxNotExist)
                        {
                            rtnV = true;
                        }
                        else if (TimeoutObject.IsTimeOut(startTime))
                        {
                            throw new DrawerLoadCheckBoxExistanceAtPositionHomeTimeOutException();
                        }
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sUnloadCheckBoxExistenceIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("UnloadCheckBoxExistenceIng.OnExit; Device Index=" + this.DeviceIndex);
            };

            sUnloadCheckBoxExistenceComplete.OnEntry += (sender, e) =>
            {  // Sync
                Debug.WriteLine("UnloadCheckBoxExistenceComplete.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                MacTransition transition = null;
                TriggerMember triggerMember = null;
                triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                if (HalDrawer.CurrentWorkState == DrawerWorkState.BoxExist)
                {    // Tray 上有 Box, 回到 Out
                    transition = tUnloadCheckBoxExistenceComplete_UnloadMoveTrayToOutStart;
                }
                else //if(HalDrawer.CurrentWorkState == DrawerWorkState.BoxNotExist)
                {    // Tray 上没有 BOX, 回復到WaitingUnloadInstruction 狀態
                    transition = tUnloadCheckBoxExistenceComplete_WaitingUnloadInstruction;
                }
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sUnloadCheckBoxExistenceComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("UnloadCheckBoxExistenceComplete.OnExit; Device Index=" + this.DeviceIndex);
            };

            #endregion Unload, 檢查有没有盒子
            #region Unload, 將Box 移至 Out
            sUnloadMoveTrayToOutStart.OnEntry += (sender, e) =>
            {  // Sync
                Debug.WriteLine("UnloadMoveTrayToOutStart.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tUnloadMoveTrayToOutStart_UnloadMoveTrayToOutIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { HalDrawer.CommandTrayMotionOut(); },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sUnloadMoveTrayToOutStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("UnloadMoveTrayToOutStart.OnExit; Device Index=" + this.DeviceIndex);
            };

            sUnloadMoveTrayToOutIng.OnEntry += (sender, e) =>
            {  // Async
                Debug.WriteLine("UnloadMoveTrayToOutIng.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tUnloadMoveTrayToOutIng_UnloadMoveTrayToOutComplete;
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = (startTime) =>
                    {
                        var rtnV = false;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionOut)
                        {
                            rtnV = true;
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            throw new DrawerUnloadMoveTrayToPositionOutFailException();
                        }
                        else if (TimeoutObject.IsTimeOut(startTime))
                        {
                            throw new DrawerUnloadMoveTrayToPositionOutTimeOutException();
                        }
                        return rtnV;
                    },
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sUnloadMoveTrayToOutIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("UnloadMoveTrayToOutIng.OnExit; Device Index=" + this.DeviceIndex);
            };

            sUnloadMoveTrayToOutComplete.OnEntry += (sender, e) =>
            {  // Sync
                Debug.WriteLine("UnloadMoveTrayToOutComplete.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                // Unload,已將Tary移到 Out, 等待 將Box 取走 
                var transition = tUnloadMoveTrayToOutComplete_UnloadWaitingGetBoxOnTray;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // to something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);

            };
            sUnloadMoveTrayToOutComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("UnloadMoveTrayToOutComplete.OnExit; Device Index=" + this.DeviceIndex);
            };

            sUnloadWaitingGetBoxOnTray.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("UnloadWaitingGetBoxOnTray.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tUnloadWaitingGetBoxOnTray_NULL;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {

                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);

            };
            sUnloadWaitingGetBoxOnTray.OnExit += (sender, e) =>
            {
                Debug.WriteLine("UnloadWaitingGetBoxOnTray.OnExit; Device Index=" + this.DeviceIndex);
            };
            #endregion Unload, 將Box 移至 Out

            #region 將Tray 移到 Home, 準備接 Load指令 
            sMoveTrayToHomeWaitingLoadInstructionStart.OnEntry += (sender, e) =>
            {  // Sync
                Debug.WriteLine("MoveTrayToHomeWaitingLoadInstructionStart.OnEntry; Device Index=" + this.DeviceIndex);
                var transition = tMoveTrayToHomeWaitingLoadInstructionStart_MoveTrayToHomeWaitingLoadInstructionIng;
                SetCurrentState((MacState)sender);
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { HalDrawer.CommandTrayMotionHome(); },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something  
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMoveTrayToHomeWaitingLoadInstructionStart.OnExit += (sender, e) =>
             {
                 Debug.WriteLine("MoveTrayToHomeWaitingLoadInstructionStart.OnExit; Device Index=" + this.DeviceIndex);
             };

            sMoveTrayToHomeWaitingLoadInstructionIng.OnEntry += (sender, e) =>
            {  // Async
                Debug.WriteLine("MoveTrayToHomeWaitingLoadInstructionIng.OnEntry; Device Index=" + this.DeviceIndex);
                var transition = tMoveTrayToHomeWaitingLoadInstructionIng_MoveTrayToHomeWaitingLoadInstructionComplete;
                SetCurrentState((MacState)sender);
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                      {
                          // do something
                      },
                    Guard = (startTime) =>
                    {
                        var rtnV = false;
                        if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionHome)
                        {
                            rtnV = true;
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            throw new DrawerMoveTrayToHomeWaitingLoadInstructionFailException();
                        }
                        else if (TimeoutObject.IsTimeOut(startTime))
                        {
                            throw new DrawerMoveTrayToHomeWaitingLoadInstructionTimeOutException();
                        }
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sMoveTrayToHomeWaitingLoadInstructionIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("MoveTrayToHomeWaitingLoadInstructionIng.OnExit; Device Index=" + this.DeviceIndex);
            };

            sMoveTrayToHomeWaitingLoadInstructionComplete.OnEntry += (sender, e) =>
            {  // Sync
                Debug.WriteLine("MoveTrayToHomeWaitingLoadInstructionComplete.OnEntry; Device Index=" + this.DeviceIndex);
                SetCurrentState((MacState)sender);
                var transition = tMoveTrayToHomeWaitingLoadInstructionComplete_WaitingLoadInstruction;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMoveTrayToHomeWaitingLoadInstructionComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("MoveTrayToHomeWaitingLoadInstructionComplete.OnExit; Device Index=" + this.DeviceIndex);
            };


            #endregion 將Tray 移到 Home, 準備接 Load指令


            #endregion event

        }






    }
    /**
    public class CabinetDrawerLEDController
    {
        private enum LedCommand
        {
            AllOff,
            GreenOn,
            GreenFlash,
            RedOn,
            RedFlash,
            AllOn,
            AllFlash,
        }
        public IMacHalDrawer HalDrawer = null;
        private LedCommand LightFlag = LedCommand.AllOff;
        private int FlashmSec = 500;
        private CabinetDrawerLEDController()
        {

        }
        public  CabinetDrawerLEDController(IMacHalDrawer halDrawer):this()
        {
            HalDrawer = halDrawer;
        }
        public void AllOff()
        {
            LightFlag = LedCommand.AllOff;
        }
        public void GreenOn()
        {
            LightFlag = LedCommand.GreenOn;
        }
        public void GreenFlash()
        {
            LightFlag = LedCommand.GreenFlash;
        }
        public void RedOn()
        {
            LightFlag = LedCommand.RedOn;
        }

        public void RedFlash()
        {
            LightFlag = LedCommand.RedFlash;
        }

        public void AllOn()
        {
            LightFlag = LedCommand.AllOn;
        }
        public void ALLFlash()
        {
            LightFlag = LedCommand.AllFlash;
        }


        private void SetLedState()
        {
            bool? toggle = false;
            Action led = () =>
            {
                while (toggle.HasValue)
                {
                    try
                    {
                        if (((bool)toggle))
                        {
                            

                            toggle = false;
                        }
                        else
                        {

                            toggle = true;
                        }
                      
                    }
                    catch(Exception ex)
                    {

                    }
                    Sleep(FlashmSec);          
                }
            };
            
            var task = new Task(led);
            task.Start();

        }

        private void Sleep(int flashmSec)
        {
            throw new NotImplementedException();
        }
    }
    */
}
