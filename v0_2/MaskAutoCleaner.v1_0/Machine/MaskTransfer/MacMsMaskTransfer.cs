using MaskAutoCleaner.v1_0.Machine.CleanCh;
using MaskAutoCleaner.v1_0.Machine.InspectionCh;
using MaskAutoCleaner.v1_0.Machine.LoadPort;
using MaskAutoCleaner.v1_0.Machine.OpenStage;
using MaskAutoCleaner.v1_0.Machine.StateExceptions;
using MaskAutoCleaner.v1_0.Msg;
using MaskAutoCleaner.v1_0.Msg.SecsReport;
using MaskAutoCleaner.v1_0.StateMachineBeta;
using MaskAutoCleaner.v1_0.StateMachineExceptions.BoxTransferStateMachineException;
using MaskAutoCleaner.v1_0.StateMachineExceptions.CleanChStateMachineException;
using MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException;
using MaskAutoCleaner.v1_0.StateMachineExceptions.InspectionChStateMachineException;
using MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException;
using MaskAutoCleaner.v1_0.StateMachineExceptions.MaskTransferStateMachineException;
using MaskAutoCleaner.v1_0.StateMachineExceptions.OpenStageStateMachineException;
using MaskAutoCleaner.v1_0.StateMachineExceptions.UniversalStateMachineException;
using MvAssistant.v0_2.Mac;
using MvAssistant.v0_2.Mac.Hal.Assembly;
using MvAssistant.v0_2.Mac.JSon.RobotTransferFile;
using MvAssistant.v0_2.Mac.Manifest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.MaskTransfer
{

    /// <summary>
    /// MaskTransfer state machine
    /// </summary>
    [Guid("3C333536-8B09-43B0-9F56-957920050CFB")]
    public class MacMsMaskTransfer : MacMachineStateBase
    {
        public IMacHalMaskTransfer HalMaskTransfer { get { return (IMacHalMaskTransfer)this.halAssembly; } }

        public MacMsMaskTransfer() { LoadStateMachine(); }

        #region State Machine Command
        //d20201103 TriggerMember 在 Command 執行時產生
        //  好處是可以動態生成處理程式, 隨時置換要Trigger的內容
        //  若不存在會變動的程式就不需要這功能
        //  缺點是每次執行都會跑同一段代碼去生成TriggerMember
        //  若不在意微秒等級的時間延遲就沒關係
        //  目前保留此設計, 以確保未來彈性




        /// <summary> 狀態機啟動 </summary>
        public override void SystemBootup()
        {
            var transition = this.Transitions[EnumMacMaskTransferTransition.SystemBootup.ToString()];
            transition.StateFrom.ExecuteCommandAtEntry(new MacStateEntryEventArgs());
        }
        /// <summary> Mask Transfer初始化 </summary>
        public void Initial()
        {
            var transition = this.Transitions[EnumMacMaskTransferTransition.Initial.ToString()];
            transition.StateFrom.ExecuteCommandAtEntry(new MacStateEntryEventArgs());
        }
        /// <summary> 從 LP Home 到 Load Port A 夾取 Mask 並返回 LP Home </summary>
        public void LPHomeToLPAGetMaskReturnToLPHomeClamped()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToLoadPortA.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 LP Home 到 Load Port B 夾取 Mask 並返回 LP Home </summary>
        public void LPHomeToLPBGetMaskReturnToLPHomeClamped()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToLoadPortB.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 LP Home 到 Open Stage 夾取 Mask 並返回 LP Home </summary>
        public void LPHomeToOSGetMaskReturnToLPHomeClamped()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToOpenStage.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 LP Home 轉向到 IC Home(夾著Mask) </summary>
        public void LPHomeClampedToICHomeClamped()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToChangeDirectionToICHomeClampedFromLPHomeClamped.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 LP Home 轉向到 IC Home(不夾Mask) </summary>
        public void LPHomeToICHome()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToChangeDirectionToICHomeFromLPHome.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 IC Home 轉向到 LP Home(不夾Mask) </summary>
        public void ICHomeToLPHome()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToChangeDirectionToLPHomeFromICHome.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 IC Home 夾著 Mask 放入 Inspection Chamber(Pellicle面向上) </summary>
        public void ICHomeClampedToICPellicleReleaseReturnToICHome()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToInspectionChPellicleForRelease.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 IC Home 到 Inspection Chamber 取出 Mask(Pellicle面向上) </summary>
        public void ICHomeToICPellicleGetReturnToICClamped()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToInspectionChPellicle.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 IC Home 夾著 Mask 放入 Inspection Chamber(Glass面向上) </summary>
        public void ICHomeClampedToICGlassReleaseReturnToICHome()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToInspectionChGlassForRelease.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 IC Home 到 Inspection Chamber 取出 Mask(Glass面向上) </summary>
        public void ICHomeToICGlassGetReturnToICClamped()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToInspectionChGlass.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 將 IC Home 夾著 Mask 的狀態轉成 IC Home 夾著 Mask 並且兩面都完成檢測 </summary>
        public void ICHomeClampedToICHomeInspected()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToChangeMaskStateToInspected.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 在 IC Home 夾著 Mask 並且兩面都完成檢測後，需要清潔 Mask ，轉向到 CC Home </summary>
        public void ICHomeInspectedToCCHomeClamped()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToChangeDirectionToCCHomeClampedFromICHomeInspected.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 CC Home 夾著 Mask 進入 Clean Chamber(Pellicle面向下) </summary>
        public void CCHomeClampedToCCPellicle()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToCleanChPellicle.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 在 Clean Chamber 內夾著 Mask ，移動到 Air Gun 上方(Pellicle面向下) </summary>
        public void InCCPellicleMoveToClean()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToCleanPellicle.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 開始進行清理Pellicle的動作 </summary>
        public void CleanPellicle()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToCleanPellicle.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> Pellicle 清理完回到 Clean Chamber 內的起始點 </summary>
        public void CCPellicleCleanedReturnInCCPellicle()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToOriginAfterCleanedPellicle.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 在 Clean Chamber 內夾著 Mask ，移動到 Camera 上方(Pellicle面向下) </summary>
        public void InCCPellicleMoveToInspect()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToInspectPellicle.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 開始進行檢測Pellicle的動作 </summary>
        public void InspectPellicle()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToInspectPellicle.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> Pellicle 檢測完回到 Clean Chamber 內的起始點 </summary>
        public void CCPellicleInspectedReturnInCCPellicle()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToOriginAfterInspectedPellicle.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 Clean Cjamber 內(Pellecle面向下)，夾著 Mask 回到 CC Home </summary>
        public void InCCPellicleToCCHomeClamped()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToCCHomeClampedFromCleanChPellicle.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 CC Home 夾著 Mask 進入 Clean Chamber(Glass面向下) </summary>
        public void CCHomeClampedToCCGlass()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToCleanChGlass.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 在 Clean Chamber 內夾著 Mask ，移動到 Air Gun 上方(Glass面向下) </summary>
        public void InCCGlassMoveToClean()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToCleanGlass.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 開始進行清理Glass的動作 </summary>
        public void CleanGlass()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToCleanGlass.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> Glass 清理完回到 Clean Chamber 內的起始點 </summary>
        public void CCGlassCleanedReturnInCCGlass()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToOriginAfterCleanedGlass.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 在 Clean Chamber 內夾著 Mask ，移動到 Camera 上方(Glass面向下) </summary>
        public void InCCGlassMoveToInspect()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToInspectGlass.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 開始進行檢測Glass的動作 </summary>
        public void InspectGlass()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToInspectGlass.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> Glass 檢測完回到 Clean Chamber 內的起始點 </summary>
        public void CCGlassInspectedReturnInCCGlass()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToOriginAfterInspectedGlass.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 Clean Cjamber 內(Glass面向下)，夾著 Mask 回到 CC Home </summary>
        public void InCCGlassToCCHomeClamped()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToCCHomeClampedFromCleanChGlass.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 將 CC Home 夾著 Mask 的狀態轉成 CC Home 夾著 Mask 並且完成清潔 </summary>
        public void CCHomeClampedToCCHomeCleaned()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToChangeMaskStateToCleaned.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 在 IC Home 夾著 Mask 並且兩面都完成檢測，不用清潔直接轉向到 LP Home </summary>
        public void ICHomeInspectedToLPHomeInspected()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToChangeDirectionToLPHomeInspectedFromICHomeInspected.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 在 CC Home 夾著 Mask 並且完成清潔，轉向到 LP Home </summary>
        public void CCHomeCleanedToLPHomeCleaned()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToChangeDirectionToLPHomeCleanedFromCCHomeCleaned.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 LP Home 將未經過檢查的 Mask 放到 Open Stage，回到 LP Home </summary>
        public void LPHomeClampedToOSReleaseMaskReturnToLPHome()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToOpenStageForRelease.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 LP Home 將已經檢測過，不需清理的 Mask 放到 Load Port A </summary>
        public void LPHomeInspectedToLPARelease()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToLoadPortAInspectedForRelease.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 LP Home 將已經檢測過，不需清理的 Mask 放到 Load Port B </summary>
        public void LPHomeInspectedToLPBRelease()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToLoadPortBInspectedForRelease.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 LP Home 將已經檢測過，不需清理的 Mask 放到 Open Stage </summary>
        public void LPHomeInspectedToOSRelease()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToOpenStageInspectedForRelease.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 LP Home 將已經清理過的 Mask 放到 Load Port A </summary>
        public void LPHomeCleanedToLPARelease()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToLoadPortACleanedForRelease.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 LP Home 將已經清理過的 Mask 放到 Load Port B </summary>
        public void LPHomeCleanedToLPBRelease()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToLoadPortBCleanedForRelease.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 LP Home 將已經清理過的 Mask 放到 Open Stage </summary>
        public void LPHomeCleanedToOSRelease()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToOpenStageCleanedForRelease.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 LP Home 移動到 BarcodeReader </summary>
        public void LPHomeToBarcodeReader()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToBarcodeReaderClamped.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 BarcodeReader移動到 LP Home </summary>
        public void BarcodeReaderToLPHome()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToLPHomeClampedFromBarcodeReader.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從 IC Home 移動到變形檢測裝置 </summary>
        public void ICHomeToInspDeform()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToInspectDeformFromICHome.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 從變形檢測裝置移動到 IC Home </summary>
        public void InspDeformToICHome()
        {
            var transition = Transitions[EnumMacMaskTransferTransition.TriggerToMoveToICHomeFromInspectDeform.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }

        #endregion

        public override void LoadStateMachine()
        {
            //--- Declare State ---
            #region State
            MacState sStart = NewState(EnumMacMaskTransferState.Start);
            MacState sInitial = NewState(EnumMacMaskTransferState.Initial);

            // Position
            MacState sLPHome = NewState(EnumMacMaskTransferState.LPHome);
            MacState sICHome = NewState(EnumMacMaskTransferState.ICHome);
            MacState sLPHomeClamped = NewState(EnumMacMaskTransferState.LPHomeClamped);
            MacState sLPHomeInspected = NewState(EnumMacMaskTransferState.LPHomeInspected);
            MacState sLPHomeCleaned = NewState(EnumMacMaskTransferState.LPHomeCleaned);
            MacState sICHomeClamped = NewState(EnumMacMaskTransferState.ICHomeClamped);
            MacState sICHomeInspected = NewState(EnumMacMaskTransferState.ICHomeInspected);
            MacState sCCHomeClamped = NewState(EnumMacMaskTransferState.CCHomeClamped);
            MacState sCCHomeCleaned = NewState(EnumMacMaskTransferState.CCHomeCleaned);

            // Change Direction
            MacState sChangingDirectionToLPHome = NewState(EnumMacMaskTransferState.ChangingDirectionToLPHome);
            MacState sChangingDirectionToICHome = NewState(EnumMacMaskTransferState.ChangingDirectionToICHome);
            MacState sChangingDirectionToLPHomeClamped = NewState(EnumMacMaskTransferState.ChangingDirectionToLPHomeClamped);
            MacState sChangingDirectionToLPHomeInspected = NewState(EnumMacMaskTransferState.ChangingDirectionToLPHomeClamped);
            MacState sChangingDirectionToLPHomeCleaned = NewState(EnumMacMaskTransferState.ChangingDirectionToLPHomeClamped);
            MacState sChangingDirectionToICHomeClamped = NewState(EnumMacMaskTransferState.ChangingDirectionToICHomeClamped);
            MacState sChangingDirectionToCCHomeClamped = NewState(EnumMacMaskTransferState.ChangingDirectionToCCHomeClamped);

            //To Target Clamp - Move
            MacState sMovingToLoadPortA = NewState(EnumMacMaskTransferState.MovingToLoadPortA);
            MacState sMovingToLoadPortB = NewState(EnumMacMaskTransferState.MovingToLoadPortB);
            MacState sMovingToInspectionChPellicle = NewState(EnumMacMaskTransferState.MovingToInspectionChPellicle);
            MacState sMovingToInspectionChGlass = NewState(EnumMacMaskTransferState.MovingToInspectionChGlass);
            MacState sMovingToOpenStage = NewState(EnumMacMaskTransferState.MovingToOpenStage);

            //To Target Clamp - Calibration
            MacState sLoadPortAClamping = NewState(EnumMacMaskTransferState.LoadPortAClamping);
            MacState sLoadPortBClamping = NewState(EnumMacMaskTransferState.LoadPortBClamping);
            MacState sInspectionChPellicleClamping = NewState(EnumMacMaskTransferState.InspectionChPellicleClamping);
            MacState sInspectionChGlassClamping = NewState(EnumMacMaskTransferState.InspectionChGlassClamping);
            MacState sOpenStageClamping = NewState(EnumMacMaskTransferState.OpenStageClamping);

            //Clamped Back - Move
            MacState sMovingToLPHomeClampedFromLoadPortA = NewState(EnumMacMaskTransferState.MovingToLPHomeClampedFromLoadPortA);
            MacState sMovingToLPHomeClampedFromLoadPortB = NewState(EnumMacMaskTransferState.MovingToLPHomeClampedFromLoadPortB);
            MacState sMovingToICHomeClampedFromInspectionChPellicle = NewState(EnumMacMaskTransferState.MovingToICHomeClampedFromInspectionChPellicle);
            MacState sMovingToICHomeClampedFromInspectionChGlass = NewState(EnumMacMaskTransferState.MovingToICHomeClampedFromInspectionChGlass);
            MacState sMovingToLPHomeClampedFromOpenStage = NewState(EnumMacMaskTransferState.MovingToLPHomeClampedFromOpenStage);


            //Barcode Reader
            MacState sMovingToBarcodeReaderClamped = NewState(EnumMacMaskTransferState.MovingToBarcodeReaderClamped);
            MacState sReadingBarcode = NewState(EnumMacMaskTransferState.ReadingBarcode);
            MacState sMovingToLPHomeClampedFromBarcodeReader = NewState(EnumMacMaskTransferState.MovingToLPHomeClampedFromBarcodeReader);


            //Clean
            MacState sMovingToCleanChPellicle = NewState(EnumMacMaskTransferState.MovingToCleanChPellicle);//前往CleanCh
            MacState sClampedInCleanChAtOriginPellicle = NewState(EnumMacMaskTransferState.ClampedInCleanChAtOriginPellicle);//準備好Clean
            MacState sMovingToCleanPellicle = NewState(EnumMacMaskTransferState.MovingToCleanPellicle);
            MacState sPellicleOnAirGun = NewState(EnumMacMaskTransferState.PellicleOnAirGun);
            MacState sCleaningPellicle = NewState(EnumMacMaskTransferState.CleaningPellicle);
            MacState sCleanedPellicle = NewState(EnumMacMaskTransferState.CleanedPellicle);
            MacState sMovingToOriginAfterCleanedPellicle = NewState(EnumMacMaskTransferState.MovingToOriginAfterCleanedPellicle);
            MacState sMovingToInspectPellicle = NewState(EnumMacMaskTransferState.MovingToInspectPellicle);
            MacState sPellicleOnCamera = NewState(EnumMacMaskTransferState.PellicleOnCamera);
            MacState sInspectingPellicle = NewState(EnumMacMaskTransferState.InspectingPellicle);
            MacState sInspectedPellicle = NewState(EnumMacMaskTransferState.InspectedPellicle);
            MacState sMovingToOriginAfterInspectedPellicle = NewState(EnumMacMaskTransferState.MovingToOriginAfterInspectedPellicle);
            MacState sMovingToCCHomeClampedFromCleanChPellicle = NewState(EnumMacMaskTransferState.MovingToCCHomeClampedFromCleanChPellicle);//離開CleanCh

            MacState sMovingToCleanChGlass = NewState(EnumMacMaskTransferState.MovingToCleanChGlass);//前往CleanChGlass
            MacState sClampedInCleanChAtOriginGlass = NewState(EnumMacMaskTransferState.ClampedInCleanChAtOriginGlass);//準備好CleanGlass
            MacState sMovingToCleanGlass = NewState(EnumMacMaskTransferState.MovingToCleanGlass);
            MacState sGlassOnAirGun = NewState(EnumMacMaskTransferState.GlassOnAirGun);
            MacState sCleaningGlass = NewState(EnumMacMaskTransferState.CleaningGlass);
            MacState sCleanedGlass = NewState(EnumMacMaskTransferState.CleanedGlass);
            MacState sMovingToOriginAfterCleanedGlass = NewState(EnumMacMaskTransferState.MovingToOriginAfterCleanedGlass);
            MacState sMovingToInspectGlass = NewState(EnumMacMaskTransferState.MovingToInspectGlass);
            MacState sGlassOnCamera = NewState(EnumMacMaskTransferState.GlassOnCamera);
            MacState sInspectingGlass = NewState(EnumMacMaskTransferState.InspectingGlass);
            MacState sInspectedGlass = NewState(EnumMacMaskTransferState.InspectedGlass);
            MacState sMovingToOriginAfterInspectedGlass = NewState(EnumMacMaskTransferState.MovingToOriginAfterInspectedGlass);
            MacState sMovingToCCHomeClampedFromCleanChGlass = NewState(EnumMacMaskTransferState.MovingToCCHomeClampedFromCleanChGlass);//離開CleanChGlass

            //Inspect Deform
            MacState sMovingToInspectDeformFromICHome = NewState(EnumMacMaskTransferState.MovingToInspectDeform);
            MacState sInspectingClampDeform = NewState(EnumMacMaskTransferState.InspectingClampDeform);
            MacState sMovingToICHomeFromInspectDeform = NewState(EnumMacMaskTransferState.MovingToICHomeFromInspectDeform);

            //To Target
            MacState sMovingToLoadPortAForRelease = NewState(EnumMacMaskTransferState.MovingToLoadPortAForRelease);
            MacState sMovingToLoadPortBForRelease = NewState(EnumMacMaskTransferState.MovingToLoadPortBForRelease);
            MacState sMovingToInspectionChPellicleForRelease = NewState(EnumMacMaskTransferState.MovingToInspectionChPellicleForRelease);
            MacState sMovingToInspectionChGlassForRelease = NewState(EnumMacMaskTransferState.MovingToInspectionChGlassForRelease);
            MacState sMovingOpenStageForRelease = NewState(EnumMacMaskTransferState.MovingToOpenStageForRelease);

            MacState sLoadPortAReleasing = NewState(EnumMacMaskTransferState.LoadPortAReleasing);
            MacState sLoadPortBReleasing = NewState(EnumMacMaskTransferState.LoadPortBReleasing);
            MacState sInspectionChPellicleReleasing = NewState(EnumMacMaskTransferState.InspectionChPellicleReleasing);
            MacState sInspectionChGlassReleasing = NewState(EnumMacMaskTransferState.InspectionChGlassReleasing);
            MacState sOpenStageReleasing = NewState(EnumMacMaskTransferState.OpenStageReleasing);


            MacState sMovingToLPHomeFromLoadPortA = NewState(EnumMacMaskTransferState.MovingToLPHomeFromLoadPortA);
            MacState sMovingToLPHomeFromLoadPortB = NewState(EnumMacMaskTransferState.MovingToLPHomeFromLoadPortB);
            MacState sMovingToICHomeFromInspectionChPellicle = NewState(EnumMacMaskTransferState.MovingToICHomeFromInspectionChPellicle);
            MacState sMovingToICHomeFromInspectionChGlass = NewState(EnumMacMaskTransferState.MovingToICHomeFromInspectionChGlass);
            MacState sMovingToLPHomeFromOpenStage = NewState(EnumMacMaskTransferState.MovingToLPHomeFromOpenStage);

            #endregion State

            //--- Transition ---
            #region Transition
            MacTransition tStart_DeviceInitial = NewTransition(sStart, sInitial, EnumMacMaskTransferTransition.SystemBootup);
            MacTransition tDeviceInitial_LPHome = NewTransition(sInitial, sLPHome, EnumMacMaskTransferTransition.Initial);
            MacTransition tLPHome_NULL = NewTransition(sLPHome, null, EnumMacMaskTransferTransition.StandbyAtLPHome);
            MacTransition tLPHomeClamped_NULL = NewTransition(sLPHomeClamped, null, EnumMacMaskTransferTransition.StandbyAtLPHomeClamped);
            MacTransition tLPHomeInspected_NULL = NewTransition(sLPHomeInspected, null, EnumMacMaskTransferTransition.StandbyAtLPHomeInspected);
            MacTransition tLPHomeCleaned_NULL = NewTransition(sLPHomeCleaned, null, EnumMacMaskTransferTransition.StandbyAtLPHomeCleaned);
            MacTransition tICHome_NULL = NewTransition(sICHome, null, EnumMacMaskTransferTransition.StandbyAtICHome);
            MacTransition tICHomeClamped_NULL = NewTransition(sICHomeClamped, null, EnumMacMaskTransferTransition.StandbyAtICHomeClamped);
            MacTransition tICHomeClamped_ICHomeInspected = NewTransition(sICHomeClamped, sICHomeInspected, EnumMacMaskTransferTransition.TriggerToChangeMaskStateToInspected);
            MacTransition tICHomeInspected_NULL = NewTransition(sICHomeInspected, null, EnumMacMaskTransferTransition.StandbyAtICHomeInspected);
            //MacTransition tICHomeInspected_LPHomeInspected = NewTransition(sICHomeInspected, sLPHomeInspected, EnumMacMaskTransferTransition.TriggerToInspectedAtLPHomeClamped);
            MacTransition tCCHomeClamped_NULL = NewTransition(sCCHomeClamped, null, EnumMacMaskTransferTransition.StandbyAtCCHomeClamped);
            MacTransition tCCHomeClamped_CCHomeCleaned = NewTransition(sCCHomeClamped, sCCHomeCleaned, EnumMacMaskTransferTransition.TriggerToChangeMaskStateToCleaned);
            MacTransition tCCHomeCleaned_NULL = NewTransition(sCCHomeCleaned, null, EnumMacMaskTransferTransition.StandbyAtCCHomeCleaned);
            //MacTransition tCCHomeCleaned_LPHomeCleaned = NewTransition(sCCHomeCleaned, sLPHomeCleaned, EnumMacMaskTransferTransition.TriggerToCleanedAtLPHomeClamped);

            #region Change Direction
            MacTransition tLPHome_ChangingDirectionToICHome = NewTransition(sLPHome, sChangingDirectionToICHome, EnumMacMaskTransferTransition.TriggerToChangeDirectionToICHomeFromLPHome);
            MacTransition tICHome_ChangingDirectionToLPHome = NewTransition(sICHome, sChangingDirectionToLPHome, EnumMacMaskTransferTransition.TriggerToChangeDirectionToLPHomeFromICHome);
            MacTransition tLPHomeClamped_ChangingDirectionToICHomeClamped = NewTransition(sLPHomeClamped, sChangingDirectionToICHomeClamped, EnumMacMaskTransferTransition.TriggerToChangeDirectionToICHomeClampedFromLPHomeClamped);
            MacTransition tLPHomeClamped_ChangingDirectionToCCHomeClamped = NewTransition(sLPHomeClamped, sChangingDirectionToCCHomeClamped, EnumMacMaskTransferTransition.TriggerToChangeDirectionToCCHomeClampedFromLPHomeClamped);
            MacTransition tICHomeClamped_ChangingDirectionToLPHomeClamped = NewTransition(sICHomeClamped, sChangingDirectionToLPHomeClamped, EnumMacMaskTransferTransition.TriggerToChangeDirectionToLPHomeClampedFromICHomeClamped);
            MacTransition tICHomeInspected_ChangingDirectionToCCHomeClamped = NewTransition(sICHomeInspected, sChangingDirectionToCCHomeClamped, EnumMacMaskTransferTransition.TriggerToChangeDirectionToCCHomeClampedFromICHomeInspected);
            MacTransition tICHomeInspected_ChangingDirectionToLPHomeInspected = NewTransition(sICHomeInspected, sChangingDirectionToLPHomeInspected, EnumMacMaskTransferTransition.TriggerToChangeDirectionToLPHomeInspectedFromICHomeInspected);
            MacTransition tCCHomeClamped_ChangingDirectionToLPHomeClamped = NewTransition(sCCHomeClamped, sChangingDirectionToLPHomeClamped, EnumMacMaskTransferTransition.TriggerToChangeDirectionToLPHomeClampedFromCCHomeClamped);
            MacTransition tCCHomeClamped_ChangingDirectionToICHomeClamped = NewTransition(sCCHomeClamped, sChangingDirectionToICHomeClamped, EnumMacMaskTransferTransition.TriggerToChangeDirectionToICHomeClampedFromCCHomeClamped);
            MacTransition tCCHomeCleaned_ChangingDirectionToLPHomeCleaned = NewTransition(sCCHomeCleaned, sChangingDirectionToLPHomeCleaned, EnumMacMaskTransferTransition.TriggerToChangeDirectionToLPHomeCleanedFromCCHomeCleaned);
            MacTransition tChangingDirectionToICHome_ICHome = NewTransition(sChangingDirectionToICHome, sICHome, EnumMacMaskTransferTransition.FinishChangeDirectionToICHome);
            MacTransition tChangingDirectionToLPHome_LPHome = NewTransition(sChangingDirectionToLPHome, sLPHome, EnumMacMaskTransferTransition.FinishChangeDirectionToLPHome);
            MacTransition tChangingDirectionToICHomeClamped_ICHomeClamped = NewTransition(sChangingDirectionToICHomeClamped, sICHomeClamped, EnumMacMaskTransferTransition.FinishChangeDirectionToICHomeClamped);
            MacTransition tChangingDirectionToLPHomeClamped_LPHomeClamped = NewTransition(sChangingDirectionToLPHomeClamped, sLPHomeClamped, EnumMacMaskTransferTransition.FinishChangeDirectionToLPHomeClamped);
            MacTransition tChangingDirectionToLPHomeInspected_LPHomeInspected = NewTransition(sChangingDirectionToLPHomeInspected, sLPHomeInspected, EnumMacMaskTransferTransition.FinishChangeDirectionToLPHomeInspected);
            MacTransition tChangingDirectionToLPHomeCleaned_LPHomeCleaned = NewTransition(sChangingDirectionToLPHomeCleaned, sLPHomeCleaned, EnumMacMaskTransferTransition.FinishChangeDirectionToLPHomeCleaned);
            MacTransition tChangingDirectionToCCHomeClamped_CCHomeClamped = NewTransition(sChangingDirectionToCCHomeClamped, sCCHomeClamped, EnumMacMaskTransferTransition.FinishChangeDirectionToCCHomeClamped);
            #endregion Change Direction

            #region Load Port A
            MacTransition tLPHome_MovingToLoadPortA = NewTransition(sLPHome, sMovingToLoadPortA, EnumMacMaskTransferTransition.TriggerToMoveToLoadPortA);
            MacTransition tMovingToLoadPortA_LoadPortAClamping = NewTransition(sMovingToLoadPortA, sLoadPortAClamping, EnumMacMaskTransferTransition.MoveToLoadPortA);
            MacTransition tLoadPortAClamping_MovingToLPHomeClampedFromLoadPortA = NewTransition(sLoadPortAClamping, sMovingToLPHomeClampedFromLoadPortA, EnumMacMaskTransferTransition.ClampInLoadPortA);
            MacTransition tMovingToLPHomeClampedFromLoadPortA_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromLoadPortA, sLPHomeClamped, EnumMacMaskTransferTransition.MoveToLPHomeClampedFromLoadPortA);

            MacTransition tLPHomeInspected_MovingToLoadPortAForRelease = NewTransition(sLPHomeInspected, sMovingToLoadPortAForRelease, EnumMacMaskTransferTransition.TriggerToMoveToLoadPortAInspectedForRelease);
            MacTransition tLPHomeCleaned_MovingToLoadPortAForRelease = NewTransition(sLPHomeCleaned, sMovingToLoadPortAForRelease, EnumMacMaskTransferTransition.TriggerToMoveToLoadPortACleanedForRelease);
            MacTransition tMovingToLoadPortAForRelease_LoadPortAReleasing = NewTransition(sMovingToLoadPortAForRelease, sLoadPortAReleasing, EnumMacMaskTransferTransition.MoveToLoadPortAForRelease);
            MacTransition tLoadPortAReleasing_MovingToLPHomeFromLoadPortA = NewTransition(sLoadPortAReleasing, sMovingToLPHomeFromLoadPortA, EnumMacMaskTransferTransition.ReleaseInLoadPortA);
            MacTransition tMovingToLPHomeFromLoadPortA_LPHome = NewTransition(sMovingToLPHomeFromLoadPortA, sLPHome, EnumMacMaskTransferTransition.MoveToLPHomeFromLoadPortA);
            #endregion Load Port A

            #region Load Port B
            MacTransition tLPHome_MovingToLoadPortB = NewTransition(sLPHome, sMovingToLoadPortB, EnumMacMaskTransferTransition.TriggerToMoveToLoadPortB);
            MacTransition tMovingToLoadPortB_LoadPortBClamping = NewTransition(sMovingToLoadPortB, sLoadPortBClamping, EnumMacMaskTransferTransition.MoveToLoadPortB);
            MacTransition tLoadPortBClamping_MovingToLPHomeClampedFromLoadPortB = NewTransition(sLoadPortBClamping, sMovingToLPHomeClampedFromLoadPortB, EnumMacMaskTransferTransition.ClampInLoadPortB);
            MacTransition tMovingToLPHomeClampedFromLoadPortB_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromLoadPortB, sLPHomeClamped, EnumMacMaskTransferTransition.MoveToLPHomeClampedFromLoadPortB);

            MacTransition tLPHomeInspected_MovingToLoadPortBForRelease = NewTransition(sLPHomeInspected, sMovingToLoadPortBForRelease, EnumMacMaskTransferTransition.TriggerToMoveToLoadPortBInspectedForRelease);
            MacTransition tLPHomeCleaned_MovingToLoadPortBForRelease = NewTransition(sLPHomeCleaned, sMovingToLoadPortBForRelease, EnumMacMaskTransferTransition.TriggerToMoveToLoadPortBCleanedForRelease);
            MacTransition tMovingToLoadPortBForRelease_LoadPortBReleasing = NewTransition(sMovingToLoadPortBForRelease, sLoadPortBReleasing, EnumMacMaskTransferTransition.MoveToLoadPortBForRelease);
            MacTransition tLoadPortBReleasing_MovingToLPHomeFromLoadPortB = NewTransition(sLoadPortBReleasing, sMovingToLPHomeFromLoadPortB, EnumMacMaskTransferTransition.ReleaseInLoadPortB);
            MacTransition tMovingToLPHomeFromLoadPortB_LPHome = NewTransition(sMovingToLPHomeFromLoadPortB, sLPHome, EnumMacMaskTransferTransition.MoveToLPHomeFromLoadPortB);
            #endregion Load Port B

            #region Inspection Ch
            MacTransition tICHome_MovingToInspectionChPellicle = NewTransition(sICHome, sMovingToInspectionChPellicle, EnumMacMaskTransferTransition.TriggerToMoveToInspectionChPellicle);
            MacTransition tMovingToInspectionChPellicle_InspectionChPellicleClamping = NewTransition(sMovingToInspectionChPellicle, sInspectionChPellicleClamping, EnumMacMaskTransferTransition.MoveToInspectionChPellicle);
            MacTransition tInspectionChPellicleClamping_MovingToICHomeClampedFromInspectionChPellicle = NewTransition(sInspectionChPellicleClamping, sMovingToICHomeClampedFromInspectionChPellicle, EnumMacMaskTransferTransition.ClampInInspectionChPellicle);
            MacTransition tMovingToICHomeClampedFromInspectionChPellicle_ICHomeClamped = NewTransition(sMovingToICHomeClampedFromInspectionChPellicle, sICHomeClamped, EnumMacMaskTransferTransition.MoveToICHomeClampedFromInspectionChPellicle);
            MacTransition tICHomeClamped_MovingToInspectionChPellicleForRelease = NewTransition(sICHomeClamped, sMovingToInspectionChPellicleForRelease, EnumMacMaskTransferTransition.TriggerToMoveToInspectionChPellicleForRelease);
            MacTransition tMovingToInspectionChPellicleForRelease_InspectionChPellicleReleasing = NewTransition(sMovingToInspectionChPellicleForRelease, sInspectionChPellicleReleasing, EnumMacMaskTransferTransition.MoveToInspectionChPellicleForRelease);
            MacTransition tInspectionChPellicleReleasing_MovingToICHomeFromInspectionChPellicle = NewTransition(sInspectionChPellicleReleasing, sMovingToICHomeFromInspectionChPellicle, EnumMacMaskTransferTransition.ReleaseInInspectionChPellicle);
            MacTransition tMovingToICHomeFromInspectionChPellicle_ICHome = NewTransition(sMovingToICHomeFromInspectionChPellicle, sICHome, EnumMacMaskTransferTransition.MoveToICHomeFromInspectionChPellicle);

            MacTransition tICHome_MovingToInspectionChGlass = NewTransition(sICHome, sMovingToInspectionChGlass, EnumMacMaskTransferTransition.TriggerToMoveToInspectionChGlass);
            MacTransition tMovingToInspectionChGlass_InspectionChGlassClamping = NewTransition(sMovingToInspectionChGlass, sInspectionChGlassClamping, EnumMacMaskTransferTransition.MoveToInspectionChGlass);
            MacTransition tInspectionChGlassClamping_MovingToICHomeClampedFromInspectionChGlass = NewTransition(sInspectionChGlassClamping, sMovingToICHomeClampedFromInspectionChGlass, EnumMacMaskTransferTransition.ClampInInspectionChGlass);
            MacTransition tMovingToICHomeClampedFromInspectionChGlass_ICHomeClamped = NewTransition(sMovingToICHomeClampedFromInspectionChGlass, sICHomeClamped, EnumMacMaskTransferTransition.MoveToICHomeClampedFromInspectionChGlass);
            MacTransition tICHomeClamped_MovingToInspectionChGlassForRelease = NewTransition(sICHomeClamped, sMovingToInspectionChGlassForRelease, EnumMacMaskTransferTransition.TriggerToMoveToInspectionChGlassForRelease);
            MacTransition tMovingInspectionChGlassForRelease_InspectionChGlassReleasing = NewTransition(sMovingToInspectionChGlassForRelease, sInspectionChGlassReleasing, EnumMacMaskTransferTransition.MoveToInspectionChGlassForRelease);
            MacTransition tInspectionChGlassReleasing_MovingToICHomeFromInspectionChGlass = NewTransition(sInspectionChGlassReleasing, sMovingToICHomeFromInspectionChGlass, EnumMacMaskTransferTransition.ReleaseInInspectionChGlass);
            MacTransition tMovingToICHomeFromInspectionChGlass_ICHome = NewTransition(sMovingToICHomeFromInspectionChGlass, sICHome, EnumMacMaskTransferTransition.MoveToICHomeFromInspectionChGlass);
            #endregion Inspection Ch

            #region Clean Ch
            MacTransition tCCHomeClamped_MovingToCleanChPellicle = NewTransition(sCCHomeClamped, sMovingToCleanChPellicle, EnumMacMaskTransferTransition.TriggerToMoveToCleanChPellicle);
            MacTransition tMovingToCleanChPellicle_ClampedInCleanChAtOriginPellicle = NewTransition(sMovingToCleanChPellicle, sClampedInCleanChAtOriginPellicle, EnumMacMaskTransferTransition.WaitForMoveToCleanPellicle);
            MacTransition tClampedInCleanChAtOriginPellicle_NULL = NewTransition(sClampedInCleanChAtOriginPellicle, null, EnumMacMaskTransferTransition.StandbyClampedInCleanChAtOriginPellicle);
            MacTransition tClampedInCleanChAtOriginPellicle_MovingToCleanPellicle = NewTransition(sClampedInCleanChAtOriginPellicle, sMovingToCleanPellicle, EnumMacMaskTransferTransition.TriggerToMoveToCleanPellicle);
            MacTransition tMovingToCleanPellicle_PellicleOnAirGun = NewTransition(sMovingToCleanPellicle, sPellicleOnAirGun, EnumMacMaskTransferTransition.MoveToCleanPellicle);
            MacTransition tPellicleOnAirGun_NULL = NewTransition(sPellicleOnAirGun, null, EnumMacMaskTransferTransition.WaitFroCleanPellicle);
            MacTransition tPellicleOnAirGun_CleaningPellicle = NewTransition(sPellicleOnAirGun, sCleaningPellicle, EnumMacMaskTransferTransition.TriggerToCleanPellicle);
            MacTransition tCleaningPellicle_CleanedPellicle = NewTransition(sCleaningPellicle, sCleanedPellicle, EnumMacMaskTransferTransition.CleanPellicle);
            MacTransition tCleanedPellicle_NULL = NewTransition(sCleanedPellicle, null, EnumMacMaskTransferTransition.WaitForLeaveAfterCleanPellicle);
            MacTransition tCleanedPellicle_MovingToOriginAfterCleanedPellicle = NewTransition(sCleanedPellicle, sMovingToOriginAfterCleanedPellicle, EnumMacMaskTransferTransition.TriggerToMoveToOriginAfterCleanedPellicle);
            MacTransition tMovingToOriginAfterCleanedPellicle_ClampedInCleanChAtOriginPellicle = NewTransition(sMovingToOriginAfterCleanedPellicle, sClampedInCleanChAtOriginPellicle, EnumMacMaskTransferTransition.MoveToOriginAfterCleanedPellicle);
            MacTransition tClampedInCleanChAtOriginPellicle_MovingToInspectPellicle = NewTransition(sClampedInCleanChAtOriginPellicle, sMovingToInspectPellicle, EnumMacMaskTransferTransition.TriggerToMoveToInspectPellicle);
            MacTransition tMovingToInspectPellicle_PellicleOnCamera = NewTransition(sMovingToInspectPellicle, sPellicleOnCamera, EnumMacMaskTransferTransition.MoveToInspectPellicle);
            MacTransition tPellicleOnCamera_NULL = NewTransition(sPellicleOnCamera, null, EnumMacMaskTransferTransition.WaitForInspectPellicle);
            MacTransition tPellicleOnCamera_InspectingPellicle = NewTransition(sPellicleOnCamera, sInspectingPellicle, EnumMacMaskTransferTransition.TriggerToInspectPellicle);
            MacTransition tInspectingPellicle_InspectedPellicle = NewTransition(sInspectingPellicle, sInspectedPellicle, EnumMacMaskTransferTransition.InspectPellicle);
            MacTransition tInspectedPellicle_NULL = NewTransition(sInspectedPellicle, null, EnumMacMaskTransferTransition.WaitForLeaveAfterInspectedPellicle);
            MacTransition tInspectedPellicle_MovingToOriginAfterInspectedPellicle = NewTransition(sInspectedPellicle, sMovingToOriginAfterInspectedPellicle, EnumMacMaskTransferTransition.TriggerToMoveToOriginAfterInspectedPellicle);
            MacTransition tMovingToOriginAfterInspectedPellicle_ClampedInCleanChAtOriginPellicle = NewTransition(sMovingToOriginAfterInspectedPellicle, sClampedInCleanChAtOriginPellicle, EnumMacMaskTransferTransition.MoveToOriginAfterInspectedPellicle);
            MacTransition tClampedInCleanChAtOriginPellicle_MovingToCCHomeClampedFromCleanChPellicle = NewTransition(sClampedInCleanChAtOriginPellicle, sMovingToCCHomeClampedFromCleanChPellicle, EnumMacMaskTransferTransition.TriggerToMoveToCCHomeClampedFromCleanChPellicle);
            MacTransition tMovingToCCHomeClampedFromCleanChPellicle_CCHomeClamped = NewTransition(sMovingToCCHomeClampedFromCleanChPellicle, sCCHomeClamped, EnumMacMaskTransferTransition.MoveToCCHomeClampedFromCleanChPellicle);

            MacTransition tCCHomeClamped_MovingToCleanChGlass = NewTransition(sCCHomeClamped, sMovingToCleanChGlass, EnumMacMaskTransferTransition.TriggerToMoveToCleanChGlass);
            MacTransition tMovingToCleanChGlass_ClampedInCleanChAtOriginGlass = NewTransition(sMovingToCleanChGlass, sClampedInCleanChAtOriginGlass, EnumMacMaskTransferTransition.WaitForMoveToCleanGlass);
            MacTransition tClampedInCleanChAtOriginGlass_NULL = NewTransition(sClampedInCleanChAtOriginGlass, null, EnumMacMaskTransferTransition.StandbyClampedInCleanChAtOriginGlass);
            MacTransition tClampedInCleanChAtOriginGlass_MovingToCleanGlass = NewTransition(sClampedInCleanChAtOriginGlass, sMovingToCleanGlass, EnumMacMaskTransferTransition.TriggerToMoveToCleanGlass);
            MacTransition tMovingToCleanGlass_GlassOnAirGun = NewTransition(sMovingToCleanGlass, sGlassOnAirGun, EnumMacMaskTransferTransition.MoveToCleanGlass);
            MacTransition tGlassOnAirGun_NULL = NewTransition(sGlassOnAirGun, null, EnumMacMaskTransferTransition.WaitFroCleanGlass);
            MacTransition tGlassOnAirGun_CleaningGlass = NewTransition(sGlassOnAirGun, sCleaningGlass, EnumMacMaskTransferTransition.TriggerToCleanGlass);
            MacTransition tCleaningGlass_CleanedGlass = NewTransition(sCleaningGlass, sCleanedGlass, EnumMacMaskTransferTransition.CleanGlass);
            MacTransition tCleanedGlass_NULL = NewTransition(sCleanedGlass, null, EnumMacMaskTransferTransition.WaitForLeaveAfterCleanGlass);
            MacTransition tCleanedGlass_MovingToOriginAfterCleanedGlass = NewTransition(sCleanedGlass, sMovingToOriginAfterCleanedGlass, EnumMacMaskTransferTransition.TriggerToMoveToOriginAfterCleanedGlass);
            MacTransition tMovingToOriginAfterCleanedGlass_ClampedInCleanChAtOriginGlass = NewTransition(sMovingToOriginAfterCleanedGlass, sClampedInCleanChAtOriginGlass, EnumMacMaskTransferTransition.MoveToOriginAfterCleanedGlass);
            MacTransition tClampedInCleanChGlass_MovingToInspectGlass = NewTransition(sClampedInCleanChAtOriginGlass, sMovingToInspectGlass, EnumMacMaskTransferTransition.TriggerToMoveToInspectGlass);
            MacTransition tMovingToInspectGlass_GlassOnCamera = NewTransition(sMovingToInspectGlass, sGlassOnCamera, EnumMacMaskTransferTransition.MoveToInspectGlass);
            MacTransition tGlassOnCamera_NULL = NewTransition(sGlassOnCamera, null, EnumMacMaskTransferTransition.WaitForInspectGlass);
            MacTransition tGlassOnCamera_InspectingGlass = NewTransition(sGlassOnCamera, sInspectingGlass, EnumMacMaskTransferTransition.TriggerToInspectGlass);
            MacTransition tInspectingGlass_InspectedGlass = NewTransition(sInspectingGlass, sInspectedGlass, EnumMacMaskTransferTransition.InspectGlass);
            MacTransition tInspectedGlass_NULL = NewTransition(sInspectedGlass, null, EnumMacMaskTransferTransition.WaitForLeaveAfterInspectedGlass);
            MacTransition tInspectedGlass_MovingToOriginAfterInspectedGlass = NewTransition(sInspectedGlass, sMovingToOriginAfterInspectedGlass, EnumMacMaskTransferTransition.TriggerToMoveToOriginAfterInspectedGlass);
            MacTransition tMovingToOriginAfterInspectedGlass_ClampedInCleanChAtOriginGlass = NewTransition(sMovingToOriginAfterInspectedGlass, sClampedInCleanChAtOriginGlass, EnumMacMaskTransferTransition.MoveToOriginAfterInspectedGlass);
            MacTransition tClampedInCleanChAtOriginGlass_MovingToCCHomeClampedFromCleanChGlass = NewTransition(sClampedInCleanChAtOriginGlass, sMovingToCCHomeClampedFromCleanChGlass, EnumMacMaskTransferTransition.TriggerToMoveToCCHomeClampedFromCleanChGlass);
            MacTransition tMovingToCCHomeClampedFromCleanChGlass_CCHomeClamped = NewTransition(sMovingToCCHomeClampedFromCleanChGlass, sCCHomeClamped, EnumMacMaskTransferTransition.MoveToCCHomeClampedFromCleanChGlass);

            #endregion Clean Ch

            #region Open Stage
            MacTransition tLPHome_MovingToOpenStage = NewTransition(sLPHome, sMovingToOpenStage, EnumMacMaskTransferTransition.TriggerToMoveToOpenStage);
            MacTransition tMovingToOpenStage_OpenStageClamping = NewTransition(sMovingToOpenStage, sOpenStageClamping, EnumMacMaskTransferTransition.MoveToOpenStage);
            MacTransition tOpenStageClamping_MovingToLPHomeClampedFromOpenStage = NewTransition(sOpenStageClamping, sMovingToLPHomeClampedFromOpenStage, EnumMacMaskTransferTransition.ClampInOpenStage);
            MacTransition tMovingToLPHomeClampedFromOpenStage_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromOpenStage, sLPHomeClamped, EnumMacMaskTransferTransition.MoveToLPHomeClampedFromOpenStage);

            MacTransition tLPHomeClamped_MovingToOpenStageForRelease = NewTransition(sLPHomeClamped, sMovingOpenStageForRelease, EnumMacMaskTransferTransition.TriggerToMoveToOpenStageForRelease);
            MacTransition tLPHomeInspected_MovingToOpenStageForRelease = NewTransition(sLPHomeInspected, sMovingOpenStageForRelease, EnumMacMaskTransferTransition.TriggerToMoveToOpenStageInspectedForRelease);
            MacTransition tLPHomeCleaned_MovingToOpenStageForRelease = NewTransition(sLPHomeCleaned, sMovingOpenStageForRelease, EnumMacMaskTransferTransition.TriggerToMoveToOpenStageCleanedForRelease);
            MacTransition tMovingOpenStageForRelease_OpenStageReleasing = NewTransition(sMovingOpenStageForRelease, sOpenStageReleasing, EnumMacMaskTransferTransition.MoveToOpenStageForRelease);
            MacTransition tOpenStageReleasing_MovingToLPHomeFromOpenStage = NewTransition(sOpenStageReleasing, sMovingToLPHomeFromOpenStage, EnumMacMaskTransferTransition.ReleaseInOpenStage);
            MacTransition tMovingToLPHomeFromOpenStage_LPHome = NewTransition(sMovingToLPHomeFromOpenStage, sLPHome, EnumMacMaskTransferTransition.MoveToLPHomeFromOpenStage);
            #endregion Open Stage

            #region Barcode Reader
            MacTransition tLPHomeClamped_MovingToBarcodeReaderClamped = NewTransition(sLPHomeClamped, sMovingToBarcodeReaderClamped, EnumMacMaskTransferTransition.TriggerToMoveToBarcodeReaderClamped);
            MacTransition tMovingToBarcodeReaderClamped_ReadingBarcode = NewTransition(sMovingToBarcodeReaderClamped, sReadingBarcode, EnumMacMaskTransferTransition.MoveToBarcodeReader);
            MacTransition tReadingBarcode_NULL = NewTransition(sReadingBarcode, null, EnumMacMaskTransferTransition.WaitForReadBarcode);
            MacTransition tReadingBarcode_MovingToLPHomeClampedFromBarcodeReader = NewTransition(sReadingBarcode, sMovingToLPHomeClampedFromBarcodeReader, EnumMacMaskTransferTransition.TriggerToMoveToLPHomeClampedFromBarcodeReader);
            MacTransition tMovingToLPHomeClampedFromBarcodeReader_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromBarcodeReader, sLPHomeClamped, EnumMacMaskTransferTransition.MoveToLPHomeClampedFromBarcodeReader);
            #endregion Barcode Reader

            #region Inspect Deform
            MacTransition tICHome_MovingToInspectDeformFromICHome = NewTransition(sICHome, sMovingToInspectDeformFromICHome, EnumMacMaskTransferTransition.TriggerToMoveToInspectDeformFromICHome);
            MacTransition tMovingToInspectDeformFromICHome_InspectingClampDeform = NewTransition(sMovingToInspectDeformFromICHome, sInspectingClampDeform, EnumMacMaskTransferTransition.MoveToInspectDeformFromICHome);
            MacTransition tInspectingClampDeform_NULL = NewTransition(sInspectingClampDeform, null, EnumMacMaskTransferTransition.WaitForInspectDeform);
            MacTransition tInspectingClampDeform_MovingToICHomeFromInspectDeform = NewTransition(sInspectingClampDeform, sMovingToICHomeFromInspectDeform, EnumMacMaskTransferTransition.TriggerToMoveToICHomeFromInspectDeform);
            MacTransition tMovingToICHomeFromInspectDeform_ICHome = NewTransition(sMovingToICHomeFromInspectDeform, sICHome, EnumMacMaskTransferTransition.MoveToICHomeFromInspectDeform);
            #endregion Inspect Deform
            #endregion Transition

            //--- Exception Transition ---
            #region State Register OnEntry OnExit
            MaskrobotTransferPathFile fileObj = new MaskrobotTransferPathFile(@"D:\Positions\MTRobot\");
            sStart.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tStart_DeviceInitial;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sStart.OnExit += (sender, e) => { };

            sInitial.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                Mediater.ResetAllAlarm();

                var transition = tDeviceInitial_LPHome;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.Reset();
                            HalMaskTransfer.Initial();
                        }
                        catch (Exception ex) { throw new MaskTransferInitialFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInitial.OnExit += (sender, e) => { };

            sLPHome.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tLPHome_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLPHome.OnExit += (sender, e) => { };

            sLPHomeClamped.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tLPHomeClamped_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLPHomeClamped.OnExit += (sender, e) => { };

            sLPHomeInspected.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tLPHomeInspected_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLPHomeInspected.OnExit += (sender, e) => { };

            sLPHomeCleaned.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tLPHomeCleaned_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLPHomeCleaned.OnExit += (sender, e) => { };

            sICHome.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tICHome_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sICHome.OnExit += (sender, e) => { };

            sICHomeClamped.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tICHomeClamped_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sICHomeClamped.OnExit += (sender, e) => { };

            sICHomeInspected.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tICHomeInspected_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sICHomeInspected.OnExit += (sender, e) => { };

            sCCHomeClamped.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tCCHomeClamped_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sCCHomeClamped.OnExit += (sender, e) => { };

            sCCHomeCleaned.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tCCHomeCleaned_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sCCHomeCleaned.OnExit += (sender, e) => { };

            #region Change Direction
            sChangingDirectionToLPHome.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tChangingDirectionToLPHome_LPHome;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ChangeDirection(fileObj.LoadPortHomePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sChangingDirectionToLPHome.OnExit += (sender, e) => { };

            sChangingDirectionToLPHomeClamped.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tChangingDirectionToLPHomeClamped_LPHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ChangeDirection(fileObj.LoadPortHomePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sChangingDirectionToLPHomeClamped.OnExit += (sender, e) => { };

            sChangingDirectionToLPHomeInspected.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tChangingDirectionToLPHomeInspected_LPHomeInspected;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ChangeDirection(fileObj.LoadPortHomePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sChangingDirectionToLPHomeInspected.OnExit += (sender, e) => { };

            sChangingDirectionToLPHomeCleaned.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tChangingDirectionToLPHomeCleaned_LPHomeCleaned;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ChangeDirection(fileObj.LoadPortHomePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sChangingDirectionToLPHomeCleaned.OnExit += (sender, e) => { };

            sChangingDirectionToICHome.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tChangingDirectionToICHome_ICHome;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ChangeDirection(fileObj.InspChHomePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sChangingDirectionToICHome.OnExit += (sender, e) => { };

            sChangingDirectionToICHomeClamped.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tChangingDirectionToICHomeClamped_ICHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ChangeDirection(fileObj.InspChHomePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sChangingDirectionToICHomeClamped.OnExit += (sender, e) => { };

            sChangingDirectionToCCHomeClamped.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tChangingDirectionToCCHomeClamped_CCHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ChangeDirection(fileObj.CleanChHomePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sChangingDirectionToCCHomeClamped.OnExit += (sender, e) => { };
            #endregion Change Direction

            #region Load PortA
            sMovingToLoadPortA.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToLoadPortA_LoadPortAClamping;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromLPHomeToLP1PathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(EnumMacMaskType.DontCare),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToLoadPortA.OnExit += (sender, e) => { };

            sLoadPortAClamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tLoadPortAClamping_MovingToLPHomeClampedFromLoadPortA;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.Clamp((uint)parameter);
                        }
                        catch (Exception ex) { throw new MaskTransferPLCExecuteFailException(ex.Message); }
                    },
                    ActionParameter = e.Parameter,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLoadPortAClamping.OnExit += (sender, e) => { };

            sMovingToLPHomeClampedFromLoadPortA.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToLPHomeClampedFromLoadPortA_LPHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromLP1ToLPHomePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToLPHomeClampedFromLoadPortA.OnExit += (sender, e) => { };

            sMovingToLoadPortAForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToLoadPortAForRelease_LoadPortAReleasing;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromLPHomeToLP1PathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToLoadPortAForRelease.OnExit += (sender, e) => { };

            sLoadPortAReleasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tLoadPortAReleasing_MovingToLPHomeFromLoadPortA;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.Unclamp();
                        }
                        catch (Exception ex) { throw new MaskTransferPLCExecuteFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLoadPortAReleasing.OnExit += (sender, e) => { };

            sMovingToLPHomeFromLoadPortA.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToLPHomeFromLoadPortA_LPHome;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromLP1ToLPHomePathFile());
                            HalMaskTransfer.RobotMoving(true);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToLPHomeFromLoadPortA.OnExit += (sender, e) => { };
            #endregion Load PortA

            #region Load PortB
            sMovingToLoadPortB.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToLoadPortB_LoadPortBClamping;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromLPHomeToLP2PathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(EnumMacMaskType.DontCare),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToLoadPortB.OnExit += (sender, e) => { };

            sLoadPortBClamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tLoadPortBClamping_MovingToLPHomeClampedFromLoadPortB;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.Clamp((uint)parameter);
                        }
                        catch (Exception ex) { throw new MaskTransferPLCExecuteFailException(ex.Message); }
                    },
                    ActionParameter = e.Parameter,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLoadPortBClamping.OnExit += (sender, e) => { };

            sMovingToLPHomeClampedFromLoadPortB.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToLPHomeClampedFromLoadPortB_LPHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromLP2ToLPHomePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToLPHomeClampedFromLoadPortB.OnExit += (sender, e) => { };

            sMovingToLoadPortBForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToLoadPortBForRelease_LoadPortBReleasing;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromLPHomeToLP2PathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToLoadPortBForRelease.OnExit += (sender, e) => { };

            sLoadPortBReleasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tLoadPortBReleasing_MovingToLPHomeFromLoadPortB;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.Unclamp();
                        }
                        catch (Exception ex) { throw new MaskTransferPLCExecuteFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLoadPortBReleasing.OnExit += (sender, e) => { };

            sMovingToLPHomeFromLoadPortB.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToLPHomeFromLoadPortB_LPHome;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromLP2ToLPHomePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToLPHomeFromLoadPortB.OnExit += (sender, e) => { };
            #endregion Load PortB

            #region Inspection Ch
            sMovingToInspectionChPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToInspectionChPellicle_InspectionChPellicleClamping;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        if (!Mediater.RobotIntrudeInspCh(true))
                            throw new MaskTransferPathMoveFailException("Inspection Chamber not allowed to intrude !");
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromICHomeToICBackSidePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(EnumMacMaskType.DontCare),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToInspectionChPellicle.OnExit += (sender, e) => { };

            sInspectionChPellicleClamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tInspectionChPellicleClamping_MovingToICHomeClampedFromInspectionChPellicle;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromICBackSideToICStagePathFile());
                            HalMaskTransfer.RobotMoving(false);
                            HalMaskTransfer.Clamp((uint)parameter);
                        }
                        catch (Exception ex) { throw new MaskTransferPLCExecuteFailException(ex.Message); }
                    },
                    ActionParameter = e.Parameter,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInspectionChPellicleClamping.OnExit += (sender, e) => { };

            sMovingToICHomeClampedFromInspectionChPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToICHomeClampedFromInspectionChPellicle_ICHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromICStageToICBackSidePathFile());
                            HalMaskTransfer.ExePathMove(fileObj.FromICBackSideToICHomePathFile());
                            HalMaskTransfer.RobotMoving(false);
                            Mediater.RobotIntrudeInspCh(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToICHomeClampedFromInspectionChPellicle.OnExit += (sender, e) => { };

            sMovingToInspectionChPellicleForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToInspectionChPellicleForRelease_InspectionChPellicleReleasing;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        if (!Mediater.RobotIntrudeInspCh(true))
                            throw new MaskTransferPathMoveFailException("Inspection Chamber not allowed to intrude !");
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromICHomeToICBackSidePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToInspectionChPellicleForRelease.OnExit += (sender, e) => { };

            sInspectionChPellicleReleasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tInspectionChPellicleReleasing_MovingToICHomeFromInspectionChPellicle;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromICBackSideToICStagePathFile());
                            HalMaskTransfer.RobotMoving(false);
                            HalMaskTransfer.Unclamp();
                        }
                        catch (Exception ex) { throw new MaskTransferPLCExecuteFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInspectionChPellicleReleasing.OnExit += (sender, e) => { };

            sMovingToICHomeFromInspectionChPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToICHomeFromInspectionChPellicle_ICHome;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromICStageToICBackSidePathFile());
                            HalMaskTransfer.ExePathMove(fileObj.FromICBackSideToICHomePathFile());
                            HalMaskTransfer.RobotMoving(false);
                            Mediater.RobotIntrudeInspCh(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToICHomeFromInspectionChPellicle.OnExit += (sender, e) => { };



            sMovingToInspectionChGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToInspectionChGlass_InspectionChGlassClamping;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        if (!Mediater.RobotIntrudeInspCh(true))
                            throw new MaskTransferPathMoveFailException("Inspection Chamber not allowed to intrude !");
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromICHomeToICFrontSidePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(EnumMacMaskType.DontCare),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToInspectionChGlass.OnExit += (sender, e) => { };

            sInspectionChGlassClamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tInspectionChGlassClamping_MovingToICHomeClampedFromInspectionChGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromICFrontSideToICStagePathFile());
                            HalMaskTransfer.RobotMoving(false);
                            HalMaskTransfer.Clamp((uint)parameter);
                        }
                        catch (Exception ex) { throw new MaskTransferPLCExecuteFailException(ex.Message); }
                    },
                    ActionParameter = e.Parameter,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInspectionChGlassClamping.OnExit += (sender, e) => { };

            sMovingToICHomeClampedFromInspectionChGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToICHomeClampedFromInspectionChGlass_ICHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromICStageToICFrontSidePathFile());
                            HalMaskTransfer.ExePathMove(fileObj.FromICFrontSideToICHomePathFile());
                            HalMaskTransfer.RobotMoving(false);
                            Mediater.RobotIntrudeInspCh(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToICHomeClampedFromInspectionChGlass.OnExit += (sender, e) => { };

            sMovingToInspectionChGlassForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingInspectionChGlassForRelease_InspectionChGlassReleasing;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        if (!Mediater.RobotIntrudeInspCh(true))
                            throw new MaskTransferPathMoveFailException("Inspection Chamber not allowed to intrude !");
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {

                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromICHomeToICFrontSidePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToInspectionChGlassForRelease.OnExit += (sender, e) => { };

            sInspectionChGlassReleasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tInspectionChGlassReleasing_MovingToICHomeFromInspectionChGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromICFrontSideToICStagePathFile());
                            HalMaskTransfer.RobotMoving(false);
                            HalMaskTransfer.Unclamp();
                        }
                        catch (Exception ex) { throw new MaskTransferPLCExecuteFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInspectionChGlassReleasing.OnExit += (sender, e) => { };

            sMovingToICHomeFromInspectionChGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToICHomeFromInspectionChGlass_ICHome;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromICStageToICFrontSidePathFile());
                            HalMaskTransfer.ExePathMove(fileObj.FromICFrontSideToICHomePathFile());
                            HalMaskTransfer.RobotMoving(false);
                            Mediater.RobotIntrudeInspCh(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToICHomeFromInspectionChGlass.OnExit += (sender, e) => { };
            #endregion Inspection Ch

            #region Clean Ch
            sMovingToCleanChPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToCleanChPellicle_ClampedInCleanChAtOriginPellicle;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromCCHomeToCCFrontSidePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToCleanChPellicle.OnExit += (sender, e) => { };

            sClampedInCleanChAtOriginPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tClampedInCleanChAtOriginPellicle_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sClampedInCleanChAtOriginPellicle.OnExit += (sender, e) => { };

            sMovingToCleanPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToCleanPellicle_PellicleOnAirGun;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromCCFrontSideToCleanPathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToCleanPellicle.OnExit += (sender, e) => { };

            sPellicleOnAirGun.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tPellicleOnAirGun_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sPellicleOnAirGun.OnExit += (sender, e) => { };

            sCleaningPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tCleaningPellicle_CleanedPellicle;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {

                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sCleaningPellicle.OnExit += (sender, e) => { };

            sCleanedPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tCleanedPellicle_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sCleanedPellicle.OnExit += (sender, e) => { };

            sMovingToOriginAfterCleanedPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToOriginAfterCleanedPellicle_ClampedInCleanChAtOriginPellicle;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromFrontSideCleanFinishToCCPathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToOriginAfterCleanedPellicle.OnExit += (sender, e) => { };

            sMovingToInspectPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToInspectPellicle_PellicleOnCamera;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromCCFrontSideToCapturePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToInspectPellicle.OnExit += (sender, e) => { };

            sPellicleOnCamera.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tPellicleOnCamera_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sPellicleOnCamera.OnExit += (sender, e) => { };

            sInspectingPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tInspectingPellicle_InspectedPellicle;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {

                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInspectingPellicle.OnExit += (sender, e) => { };

            sInspectedPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tInspectedPellicle_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInspectedPellicle.OnExit += (sender, e) => { };

            sMovingToOriginAfterInspectedPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToOriginAfterInspectedPellicle_ClampedInCleanChAtOriginPellicle;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromFrontSideCaptureFinishToCCPathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToOriginAfterInspectedPellicle.OnExit += (sender, e) => { };

            sMovingToCCHomeClampedFromCleanChPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToCCHomeClampedFromCleanChPellicle_CCHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromCCFrontSideToCCHomePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToCCHomeClampedFromCleanChPellicle.OnExit += (sender, e) => { };



            sMovingToCleanChGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToCleanChGlass_ClampedInCleanChAtOriginGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromCCHomeToCCBackSidePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToCleanChGlass.OnExit += (sender, e) => { };

            sClampedInCleanChAtOriginGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tClampedInCleanChAtOriginGlass_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sClampedInCleanChAtOriginGlass.OnExit += (sender, e) => { };

            sMovingToCleanGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToCleanGlass_GlassOnAirGun;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromCCBackSideToCleanPathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToCleanGlass.OnExit += (sender, e) => { };

            sGlassOnAirGun.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tGlassOnAirGun_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sGlassOnAirGun.OnExit += (sender, e) => { };

            sCleaningGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tCleaningGlass_CleanedGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {

                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sCleaningGlass.OnExit += (sender, e) => { };

            sCleanedGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tCleanedGlass_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sCleanedGlass.OnExit += (sender, e) => { };

            sMovingToOriginAfterCleanedGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToOriginAfterCleanedGlass_ClampedInCleanChAtOriginGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromBackSideCleanFinishToCCPathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToOriginAfterCleanedGlass.OnExit += (sender, e) => { };

            sMovingToInspectGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToInspectGlass_GlassOnCamera;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromCCBackSideToCapturePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToInspectGlass.OnExit += (sender, e) => { };

            sGlassOnCamera.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tGlassOnCamera_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sGlassOnCamera.OnExit += (sender, e) => { };

            sInspectingGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tInspectingGlass_InspectedGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {

                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInspectingGlass.OnExit += (sender, e) => { };

            sInspectedGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tInspectedGlass_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInspectedGlass.OnExit += (sender, e) => { };

            sMovingToOriginAfterInspectedGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToOriginAfterInspectedGlass_ClampedInCleanChAtOriginGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromBackSideCaptureFinishToCCPathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToOriginAfterInspectedGlass.OnExit += (sender, e) => { };

            sMovingToCCHomeClampedFromCleanChGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToCCHomeClampedFromCleanChGlass_CCHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromCCBackSideToCCHomePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToCCHomeClampedFromCleanChGlass.OnExit += (sender, e) => { };
            #endregion Clean Ch

            #region OpenStage
            sMovingToOpenStage.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToOpenStage_OpenStageClamping;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            Mediater.RobotIntrudeOpenStage(null, true);
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromLPHomeToOSPathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(EnumMacMaskType.DontCare),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToOpenStage.OnExit += (sender, e) => { };

            sOpenStageClamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tOpenStageClamping_MovingToLPHomeClampedFromOpenStage;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromOSToIronBoxPathFile());
                            HalMaskTransfer.RobotMoving(false);
                            HalMaskTransfer.Clamp((uint)parameter);
                        }
                        catch (Exception ex) { throw new MaskTransferPLCExecuteFailException(ex.Message); }
                    },
                    ActionParameter = e.Parameter,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sOpenStageClamping.OnExit += (sender, e) => { };

            sMovingToLPHomeClampedFromOpenStage.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToLPHomeClampedFromOpenStage_LPHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromIronBoxToOSPathFile());
                            HalMaskTransfer.ExePathMove(fileObj.FromOSToLPHomePathFile());
                            HalMaskTransfer.RobotMoving(false);
                            Mediater.RobotIntrudeOpenStage(null, false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToLPHomeClampedFromOpenStage.OnExit += (sender, e) => { };

            sMovingOpenStageForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingOpenStageForRelease_OpenStageReleasing;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            Mediater.RobotIntrudeOpenStage(null, true);
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromLPHomeToOSPathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingOpenStageForRelease.OnExit += (sender, e) => { };

            sOpenStageReleasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tOpenStageReleasing_MovingToLPHomeFromOpenStage;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromOSToIronBoxPathFile());
                            HalMaskTransfer.RobotMoving(false);
                            HalMaskTransfer.Unclamp();
                        }
                        catch (Exception ex) { throw new MaskTransferPLCExecuteFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sOpenStageReleasing.OnExit += (sender, e) => { };

            sMovingToLPHomeFromOpenStage.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToLPHomeFromOpenStage_LPHome;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromIronBoxToOSPathFile());
                            HalMaskTransfer.ExePathMove(fileObj.FromOSToLPHomePathFile());
                            HalMaskTransfer.RobotMoving(false);
                            Mediater.RobotIntrudeOpenStage(null, false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToLPHomeFromOpenStage.OnExit += (sender, e) => { };
            #endregion OpenStage

            #region Barcode Reader
            sMovingToBarcodeReaderClamped.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToBarcodeReaderClamped_ReadingBarcode;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        if (!HalMaskTransfer.CheckPosition(fileObj.LoadPortHomePathFile()))
                            throw new MaskTransferPathMoveFailException("Robot position was not at Load Port Home,could not move to Barcode Reader !");
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromLPHomeToBarcodeReaderPathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToBarcodeReaderClamped.OnExit += (sender, e) => { };

            sReadingBarcode.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tReadingBarcode_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sReadingBarcode.OnExit += (sender, e) => { };

            sMovingToLPHomeClampedFromBarcodeReader.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToLPHomeClampedFromBarcodeReader_LPHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromBarcodeReaderToLPHomePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToLPHomeClampedFromBarcodeReader.OnExit += (sender, e) => { };
            #endregion

            #region Inspect Deform
            sMovingToInspectDeformFromICHome.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToInspectDeformFromICHome_InspectingClampDeform;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        if (!HalMaskTransfer.CheckPosition(fileObj.InspChHomePathFile()))
                            throw new MaskTransferPathMoveFailException("Robot position was not at Inspection Chamber Home,could not move to Inspect Deform !");
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromICHomeToInspDeformPathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToInspectDeformFromICHome.OnExit += (sender, e) => { };

            sInspectingClampDeform.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tInspectingClampDeform_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInspectingClampDeform.OnExit += (sender, e) => { };

            sMovingToICHomeFromInspectDeform.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tMovingToICHomeFromInspectDeform_ICHome;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(fileObj.FromInspDeformToICHomePathFile());
                            HalMaskTransfer.RobotMoving(false);
                        }
                        catch (Exception ex) { throw new MaskTransferPathMoveFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToICHomeFromInspectDeform.OnExit += (sender, e) => { };
            #endregion Inspect Deform

            #endregion State Register OnEntry OnExit
        }

        private bool CheckEquipmentStatus()
        {
            string Result = null;
            if (Mediater.ReadPowerON() == false) Result += "Equipment is power off now, ";
            if (Mediater.ReadBCP_Maintenance()) Result += "Key lock in the electric control box is turn to maintenance, ";
            if (Mediater.ReadCB_Maintenance()) Result += "Outside key lock between cabinet_1 and cabinet_2 is turn to maintenance, ";
            if (Mediater.ReadBCP_EMO().Item1) Result += "EMO_1 has been trigger, ";
            if (Mediater.ReadBCP_EMO().Item2) Result += "EMO_2 has been trigger, ";
            if (Mediater.ReadBCP_EMO().Item3) Result += "EMO_3 has been trigger, ";
            if (Mediater.ReadBCP_EMO().Item4) Result += "EMO_4 has been trigger, ";
            if (Mediater.ReadBCP_EMO().Item5) Result += "EMO_5 has been trigger, ";
            if (Mediater.ReadCB_EMO().Item1) Result += "EMO_6 has been trigger, ";
            if (Mediater.ReadCB_EMO().Item2) Result += "EMO_7 has been trigger, ";
            if (Mediater.ReadCB_EMO().Item3) Result += "EMO_8 has been trigger, ";
            if (Mediater.ReadLP1_EMO()) Result += "Load Port_1 EMO has been trigger, ";
            if (Mediater.ReadLP2_EMO()) Result += "Load Port_2 EMO has been trigger, ";
            if (Mediater.ReadBCP_Door()) Result += "The door of electric control box has been open, ";
            if (Mediater.ReadLP1_Door()) Result += "The door of Load Port_1 has been open, ";
            if (Mediater.ReadLP2_Door()) Result += "The door of Load Pord_2 has been open, ";
            if (Mediater.ReadBCP_Smoke()) Result += "Smoke detected in the electric control box, ";

            if (Result == null)
                return true;
            else
                throw new UniversalEquipmentException(Result);
        }

        private bool CheckAssemblyAlarmSignal()
        {
            //var CB_Alarm = HalUniversal.ReadAlarm_Cabinet();
            //var CC_Alarm = HalUniversal.ReadAlarm_CleanCh();
            //var CF_Alarm = HalUniversal.ReadAlarm_CoverFan();
            //var BT_Alarm = HalUniversal.ReadAlarm_BTRobot();
            var MTClampInsp_Alarm = Mediater.ReadAlarm_MTClampInsp();
            var MT_Alarm = Mediater.ReadAlarm_MTRobot();
            //var IC_Alarm = HalUniversal.ReadAlarm_InspCh();
            //var LP_Alarm = HalUniversal.ReadAlarm_LoadPort();
            //var OS_Alarm = HalUniversal.ReadAlarm_OpenStage();

            //if (CB_Alarm != "") throw new CabinetPLCAlarmException(CB_Alarm);
            //if (CC_Alarm != "") throw new CleanChPLCAlarmException(CC_Alarm);
            //if (CF_Alarm != "") throw new UniversalCoverFanPLCAlarmException(CF_Alarm);
            //if (BT_Alarm != "") throw new BoxTransferPLCAlarmException(BT_Alarm);
            if (MTClampInsp_Alarm != "") throw new MTClampInspectDeformPLCAlarmException(MTClampInsp_Alarm);
            if (MT_Alarm != "") throw new MaskTransferPLCAlarmException(MT_Alarm);
            //if (IC_Alarm != "") throw new InspectionChPLCAlarmException(IC_Alarm);
            //if (LP_Alarm != "") throw new LoadportPLCAlarmException(LP_Alarm);
            //if (OS_Alarm != "") throw new OpenStagePLCAlarmException(OS_Alarm);

            return true;
        }

        private bool CheckAssemblyWarningSignal()
        {
            //var CB_Warning = HalUniversal.ReadWarning_Cabinet();
            //var CC_Warning = HalUniversal.ReadWarning_CleanCh();
            //var CF_Warning = HalUniversal.ReadWarning_CoverFan();
            //var BT_Warning = HalUniversal.ReadWarning_BTRobot();
            var MTClampInsp_Warning = Mediater.ReadWarning_MTClampInsp();
            var MT_Warning = Mediater.ReadWarning_MTRobot();
            //var IC_Warning = HalUniversal.ReadWarning_InspCh();
            //var LP_Warning = HalUniversal.ReadWarning_LoadPort();
            //var OS_Warning = HalUniversal.ReadWarning_OpenStage();

            //if (CB_Warning != "") throw new CabinetPLCWarningException(CB_Warning);
            //if (CC_Warning != "") throw new CleanChPLCWarningException(CC_Warning);
            //if (CF_Warning != "") throw new UniversalCoverFanPLCWarningException(CF_Warning);
            //if (BT_Warning != "") throw new BoxTransferPLCWarningException(BT_Warning);
            if (MTClampInsp_Warning != "") throw new MTClampInspectDeformPLCWarningException(MTClampInsp_Warning);
            if (MT_Warning != "") throw new MaskTransferPLCWarningException(MT_Warning);
            //if (IC_Warning != "") throw new InspectionChPLCWarningException(IC_Warning);
            //if (LP_Warning != "") throw new LoadportPLCWarningException(LP_Warning);
            //if (OS_Warning != "") throw new OpenStagePLCWarningException(OS_Warning);

            return true;
        }
    }
}