using MaskAutoCleaner.v1_0.Machine.CleanCh;
using MaskAutoCleaner.v1_0.Machine.InspectionCh;
using MaskAutoCleaner.v1_0.Machine.OpenStage;
using MaskAutoCleaner.v1_0.Machine.StateExceptions;
using MaskAutoCleaner.v1_0.Msg;
using MaskAutoCleaner.v1_0.Msg.PrescribedSecs;
using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.Mac.v1_0.Hal.Assembly;
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
        private IMacHalMaskTransfer HalMaskTransfer { get { return this.halAssembly as IMacHalMaskTransfer; } }

        public MacMsMaskTransfer() { LoadStateMachine(); }

        EnumMacMsMaskTransferState CurrentWorkState { get; set; }

        MacMaskTransferUnitStateTimeOutController timeoutObj = new MacMaskTransferUnitStateTimeOutController();

        public void Initial()
        {
            this.States[EnumMacMsMaskTransferState.Initial.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }

        public override void LoadStateMachine()
        {
            //--- Declare State ---
            #region State
            MacState sStart = NewState(EnumMacMsMaskTransferState.Start);
            MacState sInitial = NewState(EnumMacMsMaskTransferState.Initial);

            // Position
            MacState sLPHome = NewState(EnumMacMsMaskTransferState.LPHome);
            MacState sICHome = NewState(EnumMacMsMaskTransferState.ICHome);
            MacState sLPHomeClamped = NewState(EnumMacMsMaskTransferState.LPHomeClamped);
            MacState sICHomeClamped = NewState(EnumMacMsMaskTransferState.ICHomeClamped);
            MacState sCCHomeClamped = NewState(EnumMacMsMaskTransferState.CCHomeClamped);

            // Change Direction
            MacState sChangingDirectionToLPHome = NewState(EnumMacMsMaskTransferState.ChangingDirectionToLPHome);
            MacState sChangingDirectionToICHome = NewState(EnumMacMsMaskTransferState.ChangingDirectionToICHome);
            MacState sChangingDirectionToLPHomeClamped = NewState(EnumMacMsMaskTransferState.ChangingDirectionToLPHomeClamped);
            MacState sChangingDirectionToICHomeClamped = NewState(EnumMacMsMaskTransferState.ChangingDirectionToICHomeClamped);
            MacState sChangingDirectionToCCHomeClamped = NewState(EnumMacMsMaskTransferState.ChangingDirectionToCCHomeClamped);

            //To Target Clamp - Move
            MacState sMovingToLoadPortA = NewState(EnumMacMsMaskTransferState.MovingToLoadPortA);
            MacState sMovingToLoadPortB = NewState(EnumMacMsMaskTransferState.MovingToLoadPortB);
            MacState sMovingToInspectionCh = NewState(EnumMacMsMaskTransferState.MovingToInspectionCh);
            MacState sMovingToInspectionChGlass = NewState(EnumMacMsMaskTransferState.MovingToInspectionChGlass);
            MacState sMovingToOpenStage = NewState(EnumMacMsMaskTransferState.MovingToOpenStage);

            //To Target Clamp - Calibration
            MacState sLoadPortAClamping = NewState(EnumMacMsMaskTransferState.LoadPortAClamping);
            MacState sLoadPortBClamping = NewState(EnumMacMsMaskTransferState.LoadPortBClamping);
            MacState sInspectionChClamping = NewState(EnumMacMsMaskTransferState.InspectionChClamping);
            MacState sInspectionChGlassClamping = NewState(EnumMacMsMaskTransferState.InspectionChGlassClamping);
            MacState sOpenStageClamping = NewState(EnumMacMsMaskTransferState.OpenStageClamping);

            //Clamped Back - Move
            MacState sMovingToLPHomeClampedFromLoadPortA = NewState(EnumMacMsMaskTransferState.MovingToLPHomeClampedFromLoadPortA);
            MacState sMovingToLPHomeClampedFromLoadPortB = NewState(EnumMacMsMaskTransferState.MovingToLPHomeClampedFromLoadPortB);
            MacState sMovingToICHomeClampedFromInspectionCh = NewState(EnumMacMsMaskTransferState.MovingToICHomeClampedFromInspectionCh);
            MacState sMovingToICHomeClampedFromInspectionChGlass = NewState(EnumMacMsMaskTransferState.MovingToICHomeClampedFromInspectionChGlass);
            MacState sMovingToLPHomeClampedFromOpenStage = NewState(EnumMacMsMaskTransferState.MovingToLPHomeClampedFromOpenStage);


            //Barcode Reader
            MacState sMovingToBarcodeReaderClamped = NewState(EnumMacMsMaskTransferState.MovingToBarcodeReaderClamped);
            MacState sWaitingForBarcodeReader = NewState(EnumMacMsMaskTransferState.WaitingForBarcodeReader);
            MacState sMovingToLPHomeClampedFromBarcodeReader = NewState(EnumMacMsMaskTransferState.MovingToLPHomeClampedFromBarcodeReader);


            //Clean
            MacState sCleanMovingStart = NewState(EnumMacMsMaskTransferState.CleanMovingStart);//前往CleanCh
            MacState sCleanReady = NewState(EnumMacMsMaskTransferState.CleanReady);//準備好Clean
            MacState sCleanMovingReturn = NewState(EnumMacMsMaskTransferState.CleanMovingReturn);//離開CleanCh
            MacState sCleanChMoving = NewState(EnumMacMsMaskTransferState.CleanChMoving);
            MacState sCleanChWaitAckMove = NewState(EnumMacMsMaskTransferState.CleanChWaitAckMove);


            //To Target
            MacState sMovingToLoadPortAForRelease = NewState(EnumMacMsMaskTransferState.MovingToLoadPortAForRelease);
            MacState sMovingToLoadPortBForRelease = NewState(EnumMacMsMaskTransferState.MovingToLoadPortBForRelease);
            MacState sMovingToInspectionChForRelease = NewState(EnumMacMsMaskTransferState.MovingInspectionChForRelease);
            MacState sMovingToInspectionChGlassForRelease = NewState(EnumMacMsMaskTransferState.MovingInspectionChGlassForRelease);
            MacState sMovingOpenStageForRelease = NewState(EnumMacMsMaskTransferState.MovingOpenStageForRelease);

            MacState sLoadPortAReleasing = NewState(EnumMacMsMaskTransferState.LoadPortAReleasing);
            MacState sLoadPortBReleasing = NewState(EnumMacMsMaskTransferState.LoadPortBReleasing);
            MacState sInspectionChReleasing = NewState(EnumMacMsMaskTransferState.InspectionChReleasing);
            MacState sInspectionChGlassReleasing = NewState(EnumMacMsMaskTransferState.InspectionChGlassReleasing);
            MacState sOpenStageReleasing = NewState(EnumMacMsMaskTransferState.OpenStageReleasing);


            MacState sMovingToLPHomeFromLoadPortA = NewState(EnumMacMsMaskTransferState.MovingToLPHomeFromLoadPortA);
            MacState sMovingToLPHomeFromLoadPortB = NewState(EnumMacMsMaskTransferState.MovingToLPHomeFromLoadPortB);
            MacState sMovingToICHomeFromInspectionCh = NewState(EnumMacMsMaskTransferState.MovingToICHomeFromInspectionCh);
            MacState sMovingToICHomeFromInspectionChGlass = NewState(EnumMacMsMaskTransferState.MovingToICHomeFromInspectionChGlass);
            MacState sMovingToLPHomeFromOpenStage = NewState(EnumMacMsMaskTransferState.MovingToLPHomeFromOpenStage);

            //MacState sWaitAckHome = NewState(EnumMacMsMaskTransferState.WaitAckHome);
            #endregion State

            //--- Transition ---
            #region Transition
            MacTransition tStart_DeviceInitial = NewTransition(sStart, sInitial, EnumMacMsMaskTransferTransition.PowerON);
            MacTransition tDeviceInitial_LPHome = NewTransition(sInitial, sLPHome, EnumMacMsMaskTransferTransition.Initial);
            MacTransition tLPHome_LPHome = NewTransition(sLPHome, sLPHome, EnumMacMsMaskTransferTransition.ReceiveTriggerAtLPHome);
            MacTransition tLPHomeClamped_LPHomeClamped = NewTransition(sLPHomeClamped, sLPHomeClamped, EnumMacMsMaskTransferTransition.ReceiveTriggerAtLPHomeClamped);
            MacTransition tICHome_ICHome = NewTransition(sICHome, sICHome, EnumMacMsMaskTransferTransition.ReceiveTriggerAtICHome);
            MacTransition tICHomeClamped_ICHomeClamped = NewTransition(sICHomeClamped, sICHomeClamped, EnumMacMsMaskTransferTransition.ReceiveTriggerAtICHomeClamped);
            MacTransition tWaitingForBarcodeReader_WaitingForBarcodeReader = NewTransition(sWaitingForBarcodeReader, sWaitingForBarcodeReader, EnumMacMsMaskTransferTransition.ReceiveTriggerAtBarcodeReader);

            #region Change Direction
            MacTransition tLPHome_ChangingDirectionToICHome = NewTransition(sLPHome, sChangingDirectionToICHome, EnumMacMsMaskTransferTransition.ChangeDirectionToICHomeFromLPHome);
            MacTransition tICHome_ChangingDirectionToLPHome = NewTransition(sICHome, sChangingDirectionToLPHome, EnumMacMsMaskTransferTransition.ChangeDirectionToLPHomeFromICHome);
            MacTransition tLPHomeClamped_ChangingDirectionToICHomeClamped = NewTransition(sLPHomeClamped, sChangingDirectionToICHomeClamped, EnumMacMsMaskTransferTransition.ChangeDirectionToICHomeClampedFromLPHomeClamped);
            MacTransition tLPHomeClamped_ChangingDirectionToCCHomeClamped = NewTransition(sLPHomeClamped, sChangingDirectionToCCHomeClamped, EnumMacMsMaskTransferTransition.ChangeDirectionToCCHomeClampedFromLPHomeClamped);
            MacTransition tICHomeClamped_ChangingDirectionToLPHomeClamped = NewTransition(sICHomeClamped, sChangingDirectionToLPHomeClamped, EnumMacMsMaskTransferTransition.ChangeDirectionToLPHomeClampedFromICHomeClamped);
            MacTransition tICHomeClamped_ChangingDirectionToCCHomeClamped = NewTransition(sICHomeClamped, sChangingDirectionToCCHomeClamped, EnumMacMsMaskTransferTransition.ChangeDirectionToCCHomeClampedFromICHomeClamped);
            MacTransition tCCHomeClamped_ChangingDirectionToLPHomeClamped = NewTransition(sCCHomeClamped, sChangingDirectionToLPHomeClamped, EnumMacMsMaskTransferTransition.ChangeDirectionToLPHomeClampedFromCCHomeClamped);
            MacTransition tCCHomeClamped_ChangingDirectionToICHomeClamped = NewTransition(sCCHomeClamped, sChangingDirectionToICHomeClamped, EnumMacMsMaskTransferTransition.ChangeDirectionToICHomeClampedFromCCHomeClamped);
            MacTransition tChangingDirectionToICHome_ICHome = NewTransition(sChangingDirectionToICHome, sICHome, EnumMacMsMaskTransferTransition.FinishChangeDirectionToICHome);
            MacTransition tChangingDirectionToLPHome_LPHome = NewTransition(sChangingDirectionToLPHome, sLPHome, EnumMacMsMaskTransferTransition.FinishChangeDirectionToLPHome);
            MacTransition tChangingDirectionToICHomeClamped_ICHomeClamped = NewTransition(sChangingDirectionToICHomeClamped, sICHomeClamped, EnumMacMsMaskTransferTransition.FinishChangeDirectionToICHomeClamped);
            MacTransition tChangingDirectionToLPHomeClamped_LPHomeClamped = NewTransition(sChangingDirectionToLPHomeClamped, sLPHomeClamped, EnumMacMsMaskTransferTransition.FinishChangeDirectionToLPHomeClamped);
            MacTransition tChangingDirectionToCCHomeClamped_CCHomeClamped = NewTransition(sChangingDirectionToCCHomeClamped, sCCHomeClamped, EnumMacMsMaskTransferTransition.FinishChangeDirectionToCCHomeClamped);
            #endregion Change Direction

            #region Load Port A
            MacTransition tLPHome_MovingToLoadPortA = NewTransition(sLPHome, sMovingToLoadPortA, EnumMacMsMaskTransferTransition.MoveToLoadPortA);
            MacTransition tMovingToLoadPortA_LoadPortAClamping = NewTransition(sMovingToLoadPortA, sLoadPortAClamping, EnumMacMsMaskTransferTransition.ClampInLoadPortA);
            MacTransition tLoadPortAClamping_MovingToLPHomeClampedFromLoadPortA = NewTransition(sLoadPortAClamping, sMovingToLPHomeClampedFromLoadPortA, EnumMacMsMaskTransferTransition.MoveToLPHomeClampedFromLoadPortA);
            MacTransition tMovingToLPHomeClampedFromLoadPortA_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromLoadPortA, sLPHomeClamped, EnumMacMsMaskTransferTransition.StandbyAtLPHomeClampedFromLoadPortA);
            MacTransition tLPHomeClamped_MovingToLoadPortAForRelease = NewTransition(sLPHomeClamped, sMovingToLoadPortAForRelease, EnumMacMsMaskTransferTransition.MoveToLoadPortAForRelease);
            MacTransition tMovingToLoadPortAForRelease_LoadPortAReleasing = NewTransition(sMovingToLoadPortAForRelease, sLoadPortAReleasing, EnumMacMsMaskTransferTransition.ReleaseInLoadPortA);
            MacTransition tLoadPortAReleasing_MovingToLPHomeFromLoadPortA = NewTransition(sLoadPortAReleasing, sMovingToLPHomeFromLoadPortA, EnumMacMsMaskTransferTransition.MoveToLPHomeFromLoadPortA);
            MacTransition tMovingToLPHomeFromLoadPortA_LPHome = NewTransition(sMovingToLPHomeFromLoadPortA, sLPHome, EnumMacMsMaskTransferTransition.StandbyAtLPHomeFromLoadPortA);
            #endregion Load Port A

            #region Load Port B
            MacTransition tLPHome_MovingToLoadPortB = NewTransition(sLPHome, sMovingToLoadPortB, EnumMacMsMaskTransferTransition.MoveToLoadPortB);
            MacTransition tMovingToLoadPortB_LoadPortBClamping = NewTransition(sMovingToLoadPortB, sLoadPortBClamping, EnumMacMsMaskTransferTransition.ToClampInLoadPortB);
            MacTransition tLoadPortBClamping_MovingToLPHomeClampedFromLoadPortB = NewTransition(sLoadPortBClamping, sMovingToLPHomeClampedFromLoadPortB, EnumMacMsMaskTransferTransition.MoveToLPHomeClampedFromLoadPortB);
            MacTransition tMovingToLPHomeClampedFromLoadPortB_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromLoadPortB, sLPHomeClamped, EnumMacMsMaskTransferTransition.StandbyAtLPHomeClampedFromLoadPortB);
            MacTransition tLPHomeClamped_MovingToLoadPortBForRelease = NewTransition(sLPHomeClamped, sMovingToLoadPortBForRelease, EnumMacMsMaskTransferTransition.MoveToLoadPortBForRelease);
            MacTransition tMovingToLoadPortBForRelease_LoadPortBReleasing = NewTransition(sMovingToLoadPortBForRelease, sLoadPortBReleasing, EnumMacMsMaskTransferTransition.ReleaseInLoadPortB);
            MacTransition tLoadPortBReleasing_MovingToLPHomeFromLoadPortB = NewTransition(sLoadPortBReleasing, sMovingToLPHomeFromLoadPortB, EnumMacMsMaskTransferTransition.MoveToLPHomeFromLoadPortB);
            MacTransition tMovingToLPHomeFromLoadPortB_LPHome = NewTransition(sMovingToLPHomeFromLoadPortB, sLPHome, EnumMacMsMaskTransferTransition.StandbyAtLPHomeFromLoadPortB);
            #endregion Load Port B

            #region Inspection Ch
            MacTransition tICHome_MovingToInspectionCh = NewTransition(sICHome, sMovingToInspectionCh, EnumMacMsMaskTransferTransition.MoveToInspectionCh);
            MacTransition tMovingToInspectionCh_InspectionChClamping = NewTransition(sMovingToInspectionCh, sInspectionChClamping, EnumMacMsMaskTransferTransition.ClampInInspectionCh);
            MacTransition tInspectionChClamping_MovingToICHomeClampedFromInspectionCh = NewTransition(sInspectionChClamping, sMovingToICHomeClampedFromInspectionCh, EnumMacMsMaskTransferTransition.MoveToICHomeClampedFromInspectionCh);
            MacTransition tMovingToICHomeClampedFromInspectionCh_ICHomeClamped = NewTransition(sMovingToICHomeClampedFromInspectionCh, sICHomeClamped, EnumMacMsMaskTransferTransition.StandbyAtICHomeClampedFromInspectionCh);
            MacTransition tICHomeClamped_MovingToInspectionChForRelease = NewTransition(sICHomeClamped, sMovingToInspectionChForRelease, EnumMacMsMaskTransferTransition.MoveToInspectionChForRelease);
            MacTransition tMovingInspectionChForRelease_InspectionChReleasing = NewTransition(sMovingToInspectionChForRelease, sInspectionChReleasing, EnumMacMsMaskTransferTransition.CleanMoveComplete);
            MacTransition tInspectionChReleasing_MovingToICHomeFromInspectionCh = NewTransition(sInspectionChReleasing, sMovingToICHomeFromInspectionCh, EnumMacMsMaskTransferTransition.CompleteReleased);
            MacTransition tMovingToICHomeFromInspectionCh_ICHome = NewTransition(sMovingToICHomeFromInspectionCh, sICHome, EnumMacMsMaskTransferTransition.StandbyAtICHomeFromInspectionCh);

            MacTransition tICHome_MovingToInspectionChGlass = NewTransition(sICHome, sMovingToInspectionChGlass, EnumMacMsMaskTransferTransition.MoveToInspectionChGlass);
            MacTransition tMovingToInspectionChGlass_InspectionChGlassClamping = NewTransition(sMovingToInspectionChGlass, sInspectionChGlassClamping, EnumMacMsMaskTransferTransition.ClampInInspectionChGlass);
            MacTransition tInspectionChGlassClamping_MovingToICHomeClampedFromInspectionChGlass = NewTransition(sInspectionChGlassClamping, sMovingToICHomeClampedFromInspectionChGlass, EnumMacMsMaskTransferTransition.MoveToICHomeClampedFromInspectionChGlass);
            MacTransition tMovingToICHomeClampedFromInspectionChGlass_ICHomeClamped = NewTransition(sMovingToICHomeClampedFromInspectionChGlass, sICHomeClamped, EnumMacMsMaskTransferTransition.StandbyAtICHomeClampedFromInspectionChGlass);
            MacTransition tICHomeClamped_MovingToInspectionChGlassForRelease = NewTransition(sICHomeClamped, sMovingToInspectionChGlassForRelease, EnumMacMsMaskTransferTransition.MoveToInspectionChGlassForRelease);
            MacTransition tMovingInspectionChGlassForRelease_InspectionChGlassReleasing = NewTransition(sMovingToInspectionChGlassForRelease, sInspectionChGlassReleasing, EnumMacMsMaskTransferTransition.CleanMoveComplete);
            MacTransition tInspectionChGlassReleasing_MovingToICHomeFromInspectionChGlass = NewTransition(sInspectionChGlassReleasing, sMovingToICHomeFromInspectionChGlass, EnumMacMsMaskTransferTransition.CompleteReleased);
            MacTransition tMovingToICHomeFromInspectionChGlass_ICHome = NewTransition(sMovingToICHomeFromInspectionChGlass, sICHome, EnumMacMsMaskTransferTransition.StandbyAtICHomeFromInspectionChGlass);
            #endregion Inspection Ch

            #region Open Stage
            MacTransition tLPHome_MovingToOpenStage = NewTransition(sLPHome, sMovingToOpenStage, EnumMacMsMaskTransferTransition.MoveToOpenStage);
            MacTransition tMovingToOpenStage_OpenStageClamping = NewTransition(sMovingToOpenStage, sOpenStageClamping, EnumMacMsMaskTransferTransition.ClampInOpenStage);
            MacTransition tOpenStageClamping_MovingToLPHomeClampedFromOpenStage = NewTransition(sOpenStageClamping, sMovingToLPHomeClampedFromOpenStage, EnumMacMsMaskTransferTransition.MoveToLPHomeClampedFromOpenStage);
            MacTransition tMovingToLPHomeClampedFromOpenStage_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromOpenStage, sLPHomeClamped, EnumMacMsMaskTransferTransition.StandbyAtLPHomeClampedFromOpenStage);
            MacTransition tLPHomeClamped_MovingToOpenStageForRelease = NewTransition(sLPHomeClamped, sMovingOpenStageForRelease, EnumMacMsMaskTransferTransition.MoveToOpenStageForRelease);
            MacTransition tMovingOpenStageForRelease_OpenStageReleasing = NewTransition(sMovingOpenStageForRelease, sOpenStageReleasing, EnumMacMsMaskTransferTransition.CleanMoveComplete);
            MacTransition tOpenStageReleasing_MovingToLPHomeFromOpenStage = NewTransition(sOpenStageReleasing, sMovingToLPHomeFromOpenStage, EnumMacMsMaskTransferTransition.CompleteReleased);
            MacTransition tMovingToLPHomeFromOpenStage_LPHome = NewTransition(sMovingToLPHomeFromOpenStage, sLPHome, EnumMacMsMaskTransferTransition.StandbyAtLPHomeFromOpenStage);
            #endregion Open Stage

            #region Barcode Reader
            MacTransition tLPHomeClamped_MovingToBarcodeReaderClamped = NewTransition(sLPHomeClamped, sMovingToBarcodeReaderClamped, EnumMacMsMaskTransferTransition.MoveToBarcodeReaderClamped);
            MacTransition tMovingToBarcodeReaderClamped_WaitingForBarcodeReader = NewTransition(sMovingToBarcodeReaderClamped, sWaitingForBarcodeReader, EnumMacMsMaskTransferTransition.WaitForBarcodeReader);
            MacTransition tWaitingForBarcodeReader_MovingToLPHomeClampedFromBarcodeReader = NewTransition(sWaitingForBarcodeReader, sMovingToLPHomeClampedFromBarcodeReader, EnumMacMsMaskTransferTransition.MoveToLPHomeClampedFromBarcodeReader);
            MacTransition tMovingToLPHomeClampedFromBarcodeReader_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromBarcodeReader, sLPHomeClamped, EnumMacMsMaskTransferTransition.StandbyAtLPHomeClampedFromBarcodeReader);
            #endregion Barcode Reader


            //Is Ready to Release
            //MacTransition tLPHomeClamped_ReadyToRelease = NewTransition(sLPHomeClamped, sReadyToRelease, EnumMacMsMaskTransferTransition.IsReady);


            //--- Clean Start
            //MacTransition tReadyToRelease_CleanMovingStart = NewTransition(sReadyToRelease, sCleanMovingStart, EnumMacMsMaskTransferTransition.CleanStart);
            MacTransition tCleanMovingStart_CleanReady = NewTransition(sCleanMovingStart, sCleanReady, EnumMacMsMaskTransferTransition.CleanMoveComplete);
            MacTransition tCleanReady_CleanMovingReturn = NewTransition(sCleanReady, sCleanMovingReturn, EnumMacMsMaskTransferTransition.CleanProcessComplete);//觸發回Home點:ReadyToRelease應讓Recipe控制, 避免其它Device衝突
            //MacTransition tCleanMovingReturn_ReadyToRelease = NewTransition(sCleanMovingReturn, sReadyToRelease, EnumMacMsMaskTransferTransition.CleanMoveComplete);
            MacTransition tCleanReady_CleanChMoving = NewTransition(sCleanReady, sCleanChMoving, EnumMacMsMaskTransferTransition.CleanReceiveMove);
            MacTransition tCleanChMoving_CleanChWaitAckMove = NewTransition(sCleanChMoving, sCleanChWaitAckMove, EnumMacMsMaskTransferTransition.CleanMoveComplete);
            MacTransition tCleanChWaitAckMove_CleanReady = NewTransition(sCleanChWaitAckMove, sCleanReady, EnumMacMsMaskTransferTransition.CleanMoveAck);


            //Complete or No Clean Job
            //MacTransition tReadyToRelease_MovingToLoadPortForRelease = NewTransition(sReadyToRelease, sMovingToLoadPortForRelease, EnumMacMsMaskTransferTransition.NoCleanJob);
            //MacTransition tReadyToRelease_MovingInspectionChForRelease = NewTransition(sReadyToRelease, sMovingInspectionChForRelease, EnumMacMsMaskTransferTransition.NoCleanJob);
            //MacTransition tReadyToRelease_MovingInspectionChGlassForRelease = NewTransition(sReadyToRelease, sMovingInspectionChGlassForRelease, EnumMacMsMaskTransferTransition.NoCleanJob);
            //MacTransition tReadyToRelease_MovingOpenStageForRelease = NewTransition(sReadyToRelease, sMovingOpenStageForRelease, EnumMacMsMaskTransferTransition.NoCleanJob);


            //Complete Move
            //MacTransition tMovingToLPHomeFromLoadPort_WaitAckHome = NewTransition(sMovingToLPHomeFromLoadPort, sWaitAckHome, EnumMacMsMaskTransferTransition.CleanMoveComplete);
            //MacTransition tMovingToICHomeFromInspectionCh_WaitAckHome = NewTransition(sMovingToICHomeFromInspectionCh, sWaitAckHome, EnumMacMsMaskTransferTransition.CleanMoveComplete);
            //MacTransition tMovingToICHomeFromInspectionChGlass_WaitAckHome = NewTransition(sMovingToICHomeFromInspectionChGlass, sWaitAckHome, EnumMacMsMaskTransferTransition.CleanMoveComplete);
            //MacTransition tMovingToLPHomeFromOpenStage_WaitAckHome = NewTransition(sMovingToLPHomeFromOpenStage, sWaitAckHome, EnumMacMsMaskTransferTransition.CleanMoveComplete);

            //MacTransition tWaitAckHome_LPHome = NewTransition(sWaitAckHome, sLPHome, EnumMacMsMaskTransferTransition.ReceiveAckHome);
            #endregion Transition

            #region State Register OnEntry OnExit

            sStart.OnEntry += (sender, e) =>
            {
                Func<StateGuardRtns> guard = () =>
                {
                    StateGuardRtns rtn = null;
                    try
                    {
                        rtn = new StateGuardRtns
                        {
                            Transition = tStart_DeviceInitial,
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        };
                    }
                    catch (Exception)
                    { throw new StateFailException(); }
                    return rtn;
                };
                Action<Exception> exceptionHandler = (ex) =>
                { throw ex; };
                Action<object> action = null;
                object actionParameter = null;
                Trigger(guard, action, actionParameter, exceptionHandler);
            };
            sStart.OnExit += (sender, e) =>
            { };

            sInitial.OnEntry += (sender, e) =>
            {
                Func<StateGuardRtns> guard = () =>
                {
                    StateGuardRtns rtn = null;
                    try
                    {
                        rtn = new StateGuardRtns
                        {
                            Transition = tDeviceInitial_LPHome,
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        };
                    }
                    catch (Exception)
                    { throw new StateFailException(); }
                    return rtn;
                };
                Action<Exception> exceptionHandler = (ex) =>
                { throw ex; };
                Action<object> action = (parameter) => { HalMaskTransfer.Initial(); };
                object actionParameter = null;
                Trigger(guard, action, actionParameter, exceptionHandler);
            };
            sInitial.OnExit += (sender, e) =>
            { };

            sLPHome.OnEntry += (sender, e) =>
            {
                Func<StateGuardRtns> guard = null;
                Action<object> action = null;
                Action<object> actionMoveToICHome = null;
                Action<object> actionMoveToOS = null;
                Action<object> actionWait = null;
                object actionParameter = null;
                object actionParameterMoveToICHome = null;
                object actionParameterMoveToOS = null;
                object actionParameterWait = null;
                Action<Exception> exceptionHandler = null;
                Action<Exception> exceptionHandlerMoveToICHome = (ex) => { throw ex; };
                Action<Exception> exceptionHandlerMoveToOS = (ex) => { throw ex; };
                Action<Exception> exceptionHandlerWait = (ex) => { throw ex; };
                Func<StateGuardRtns> guardMoveToICHome = () =>//需要轉向去IC夾取
                {
                    StateGuardRtns rtn = null;
                    try
                    {
                        rtn = new StateGuardRtns
                        {
                            Transition = tLPHome_ChangingDirectionToICHome,
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        };
                    }
                    catch (Exception)
                    { throw new StateFailException(); }
                    return rtn;
                };
                Func<StateGuardRtns> guardMoveToOS = () =>//移動到Open Stage夾取
                {
                    StateGuardRtns rtn = null;
                    try
                    {
                        rtn = new StateGuardRtns
                        {
                            Transition = tLPHome_MovingToOpenStage,
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        };
                    }
                    catch (Exception)
                    { throw new StateFailException(); }
                    return rtn;
                };
                Func<StateGuardRtns> guardWait = () =>//保持原狀態
                {
                    StateGuardRtns rtn = null;
                    try
                    {
                        rtn = new StateGuardRtns
                        {
                            Transition = tLPHome_LPHome,
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        };
                    }
                    catch (Exception)
                    { throw new StateFailException(); }
                    return rtn;
                };

                // TODO: Get Load Port A/B State
                var ICState = new MacMsInspectionCh();//暫時先New，需要找到已存在的Component State， TODO: Get InspectionCh State
                var CCState = new MacMsCleanCh();
                var OSState = new MacMsOpenStage();
                // TODO: Get Other Components State
                if (ICState.CurrentWorkState == EnumMacMsInspectionChState.WaitingForReleaseMask)
                {
                    guard = guardMoveToICHome;
                    action = actionMoveToICHome;
                    actionParameter = actionParameterMoveToICHome;
                    exceptionHandler = exceptionHandlerMoveToICHome;
                }
                else if (ICState.CurrentWorkState == EnumMacMsInspectionChState.WaitingForReleaseGlass)
                {
                    guard = guardMoveToICHome;
                    action = actionMoveToICHome;
                    actionParameter = actionParameterMoveToICHome;
                    exceptionHandler = exceptionHandlerMoveToICHome;
                }
                else if (OSState.CurrentWorkState == EnumMacMsOpenStageState.WaitingForReleaseMask)
                {
                    guard = guardMoveToOS;
                    action = actionMoveToOS;
                    actionParameter = actionParameterMoveToOS;
                    exceptionHandler = exceptionHandlerMoveToOS;
                }
                // TODO: Other Components State Check
                else
                {
                    guard = guardWait;
                    action = actionWait;
                    actionParameter = actionParameterWait;
                    exceptionHandler = exceptionHandlerWait;
                }
                Trigger(guard, action, actionParameter, exceptionHandler);
            };
            sLPHome.OnExit += (sender, e) =>
            { };

            sLPHomeClamped.OnEntry += (sender, e) =>
            {
                Func<StateGuardRtns> guard = null;
                Action<object> action = null;
                Action<object> actionMoveToICHomeClamped = null;
                Action<object> actionMoveToOSForRelease = null;
                Action<object> actionWait = null;
                object actionParameter = null;
                object actionParameterMoveToICHomeClamped = null;
                object actionParameterMoveToOSForRelease = null;
                object actionParameterWait = null;
                Action<Exception> exceptionHandler = null;
                Action<Exception> exceptionHandlerMoveToICHomeClamped = (ex) => { throw ex; };
                Action<Exception> exceptionHandlerMoveToOSForRelease = (ex) => { throw ex; };
                Action<Exception> exceptionHandlerWait = (ex) => { throw ex; };
                Func<StateGuardRtns> guardMoveToICHomeClamped = () =>//需要轉向去IC放置
                {
                    StateGuardRtns rtn = null;
                    try
                    {
                        rtn = new StateGuardRtns
                        {
                            Transition = tLPHomeClamped_ChangingDirectionToICHomeClamped,
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        };
                    }
                    catch (Exception)
                    { throw new StateFailException(); }
                    return rtn;
                };
                Func<StateGuardRtns> guardMoveToOSForRelease = () =>//移動到Open Stage放置
                {
                    StateGuardRtns rtn = null;
                    try
                    {
                        rtn = new StateGuardRtns
                        {
                            Transition = tLPHomeClamped_MovingToOpenStageForRelease,
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        };
                    }
                    catch (Exception)
                    { throw new StateFailException(); }
                    return rtn;
                };
                Func<StateGuardRtns> guardWait = () =>//保持原狀態
                {
                    StateGuardRtns rtn = null;
                    try
                    {
                        rtn = new StateGuardRtns
                        {
                            Transition = tLPHomeClamped_LPHomeClamped,
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        };
                    }
                    catch (Exception)
                    { throw new StateFailException(); }
                    return rtn;
                };

                // TODO: Get Load Port A/B State
                var ICState = new MacMsInspectionCh();//暫時先New，需要找到已存在的Component State， TODO: Get InspectionCh State
                var CCState = new MacMsCleanCh();
                var OSState = new MacMsOpenStage();
                // TODO: Get Other Components State
                if (ICState.CurrentWorkState == EnumMacMsInspectionChState.WaitingForInputMask)
                {
                    guard = guardMoveToICHomeClamped;
                    action = actionMoveToICHomeClamped;
                    actionParameter = actionParameterMoveToICHomeClamped;
                    exceptionHandler = exceptionHandlerMoveToICHomeClamped;
                }
                else if (ICState.CurrentWorkState == EnumMacMsInspectionChState.WaitingForInputGlass)
                {
                    guard = guardMoveToICHomeClamped;
                    action = actionMoveToICHomeClamped;
                    actionParameter = actionParameterMoveToICHomeClamped;
                    exceptionHandler = exceptionHandlerMoveToICHomeClamped;
                }
                else if (OSState.CurrentWorkState == EnumMacMsOpenStageState.WaitingForInputMask)
                {
                    guard = guardMoveToOSForRelease;
                    action = actionMoveToOSForRelease;
                    actionParameter = actionParameterMoveToOSForRelease;
                    exceptionHandler = exceptionHandlerMoveToOSForRelease;
                }
                // TODO: Other Components State Check
                else
                {
                    guard = guardWait;
                    action = actionWait;
                    actionParameter = actionParameterWait;
                    exceptionHandler = exceptionHandlerWait;
                }
            };
            sLPHomeClamped.OnExit += (sender, e) =>
            { };

            sICHome.OnEntry += (sender, e) =>
            {
                Func<StateGuardRtns> guard = null;
                Action<object> action = null;
                Action<object> actionMoveToLPHome = null;
                Action<object> actionMoveToICGetMask = null;
                Action<object> actionMoveToICGetGlass = null;
                Action<object> actionWait = null;
                object actionParameter = null;
                object actionParameterMoveToLPHome = null;
                object actionParameterMoveToICGetMask = null;
                object actionParameterMoveToICGetGlass = null;
                object actionParameterWait = null;
                Action<Exception> exceptionHandler = null;
                Action<Exception> exceptionHandlerMoveToLPHome = (ex) => { throw ex; };
                Action<Exception> exceptionHandlerMoveToICGetMask = (ex) => { throw ex; };
                Action<Exception> exceptionHandlerMoveToICGetGlass = (ex) => { throw ex; };
                Action<Exception> exceptionHandlerWait = (ex) => { throw ex; };
                Func<StateGuardRtns> guardMoveToLPHome = () =>//需要轉向去LP夾取
                {
                    StateGuardRtns rtn = null;
                    try
                    {
                        rtn = new StateGuardRtns
                        {
                            Transition = tICHome_ChangingDirectionToLPHome,
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        };
                    }
                    catch (Exception)
                    { throw new StateFailException(); }
                    return rtn;
                };
                Func<StateGuardRtns> guardMoveToICGetMask = () =>//移動到IC夾取Mask(在IC裡面Pellicle朝上)
                {
                    StateGuardRtns rtn = null;
                    try
                    {
                        rtn = new StateGuardRtns
                        {
                            Transition = tICHome_MovingToInspectionCh,
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        };
                    }
                    catch (Exception)
                    { throw new StateFailException(); }
                    return rtn;
                };
                Func<StateGuardRtns> guardMoveToICGetGlass = () =>//移動到IC夾取Mask(在IC裡面Glass朝上)
                {
                    StateGuardRtns rtn = null;
                    try
                    {
                        rtn = new StateGuardRtns
                        {
                            Transition = tICHome_MovingToInspectionChGlass,
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        };
                    }
                    catch (Exception)
                    { throw new StateFailException(); }
                    return rtn;
                };
                Func<StateGuardRtns> guardWait = () =>//保持原狀態
                {
                    StateGuardRtns rtn = null;
                    try
                    {
                        rtn = new StateGuardRtns
                        {
                            Transition = tICHome_ICHome,
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        };
                    }
                    catch (Exception)
                    { throw new StateFailException(); }
                    return rtn;
                };

                // TODO: Get Load Port A/B State
                var ICState = new MacMsInspectionCh();//暫時先New，需要找到已存在的Component State， TODO: Get InspectionCh State
                var CCState = new MacMsCleanCh();
                var OSState = new MacMsOpenStage();
                // TODO: Get Other Components State
                if (ICState.CurrentWorkState == EnumMacMsInspectionChState.WaitingForReleaseMask)
                {
                    guard = guardMoveToICGetMask;
                    action = actionMoveToICGetMask;
                    actionParameter = actionParameterMoveToICGetMask;
                    exceptionHandler = exceptionHandlerMoveToICGetMask;
                }
                else if (ICState.CurrentWorkState == EnumMacMsInspectionChState.WaitingForReleaseGlass)
                {
                    guard = guardMoveToICGetGlass;
                    action = actionMoveToICGetGlass;
                    actionParameter = actionParameterMoveToICGetGlass;
                    exceptionHandler = exceptionHandlerMoveToICGetGlass;
                }
                else if (OSState.CurrentWorkState == EnumMacMsOpenStageState.WaitingForReleaseMask)
                {
                    guard = guardMoveToLPHome;
                    action = actionMoveToLPHome;
                    actionParameter = actionParameterMoveToLPHome;
                    exceptionHandler = exceptionHandlerMoveToLPHome;
                }
                // TODO: Other Components State Check
                else
                {
                    guard = guardWait;
                    action = actionWait;
                    actionParameter = actionParameterWait;
                    exceptionHandler = exceptionHandlerWait;
                }
            };
            sICHome.OnExit += (sender, e) =>
            { };

            sICHomeClamped.OnEntry += (sender, e) =>
            {
                Func<StateGuardRtns> guard = null;
                Action<object> action = null;
                Action<object> actionMoveToLPHome = null;
                Action<object> actionMoveToICForReleaseMask = null;
                Action<object> actionMoveToICForReleaseGlass = null;
                Action<object> actionWait = null;
                object actionParameter = null;
                object actionParameterMoveToLPHome = null;
                object actionParameterMoveToICForReleaseMask = null;
                object actionParameterMoveToICForReleaseGlass = null;
                object actionParameterWait = null;
                Action<Exception> exceptionHandler = null;
                Action<Exception> exceptionHandlerMoveToLPHome = (ex) => { throw ex; };
                Action<Exception> exceptionHandlerMoveToICForReleaseMask = (ex) => { throw ex; };
                Action<Exception> exceptionHandlerMoveToICForReleaseGlass = (ex) => { throw ex; };
                Action<Exception> exceptionHandlerWait = (ex) => { throw ex; };
                Func<StateGuardRtns> guardMoveToLPHome = () =>//需要轉向去LP放置
                {
                    StateGuardRtns rtn = null;
                    try
                    {
                        rtn = new StateGuardRtns
                        {
                            Transition = tICHomeClamped_ChangingDirectionToLPHomeClamped,
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        };
                    }
                    catch (Exception)
                    { throw new StateFailException(); }
                    return rtn;
                };
                Func<StateGuardRtns> guardMoveToICForReleaseMask = () =>//移動到IC放置Mask(在IC裡面Pellicle朝上)
                {
                    StateGuardRtns rtn = null;
                    try
                    {
                        rtn = new StateGuardRtns
                        {
                            Transition = tICHomeClamped_MovingToInspectionChForRelease,
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        };
                    }
                    catch (Exception)
                    { throw new StateFailException(); }
                    return rtn;
                };
                Func<StateGuardRtns> guardMoveToICForReleaseGlass = () =>//移動到IC放置Mask(在IC裡面Glass朝上)
                {
                    StateGuardRtns rtn = null;
                    try
                    {
                        rtn = new StateGuardRtns
                        {
                            Transition = tICHomeClamped_MovingToInspectionChGlassForRelease,
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        };
                    }
                    catch (Exception)
                    { throw new StateFailException(); }
                    return rtn;
                };
                Func<StateGuardRtns> guardWait = () =>//保持原狀態
                {
                    StateGuardRtns rtn = null;
                    try
                    {
                        rtn = new StateGuardRtns
                        {
                            Transition = tICHomeClamped_ICHomeClamped,
                            ThisStateExitEventArgs = new MacStateExitEventArgs(),
                            NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        };
                    }
                    catch (Exception)
                    { throw new StateFailException(); }
                    return rtn;
                };

                // TODO: Get Load Port A/B State
                var ICState = new MacMsInspectionCh();//暫時先New，需要找到已存在的Component State， TODO: Get InspectionCh State
                var CCState = new MacMsCleanCh();
                var OSState = new MacMsOpenStage();
                // TODO: Get Other Components State
                if (ICState.CurrentWorkState == EnumMacMsInspectionChState.WaitingForInputMask)
                {
                    guard = guardMoveToICForReleaseMask;
                    action = actionMoveToICForReleaseMask;
                    actionParameter = actionParameterMoveToICForReleaseMask;
                    exceptionHandler = exceptionHandlerMoveToICForReleaseMask;
                }
                else if (ICState.CurrentWorkState == EnumMacMsInspectionChState.WaitingForInputGlass)
                {
                    guard = guardMoveToICForReleaseGlass;
                    action = actionMoveToICForReleaseGlass;
                    actionParameter = actionParameterMoveToICForReleaseGlass;
                    exceptionHandler = exceptionHandlerMoveToICForReleaseGlass;
                }
                else if (OSState.CurrentWorkState == EnumMacMsOpenStageState.WaitingForInputMask)
                {
                    guard = guardMoveToLPHome;
                    action = actionMoveToLPHome;
                    actionParameter = actionParameterMoveToLPHome;
                    exceptionHandler = exceptionHandlerMoveToLPHome;
                }
                // TODO: Other Components State Check
                else
                {
                    guard = guardWait;
                    action = actionWait;
                    actionParameter = actionParameterWait;
                    exceptionHandler = exceptionHandlerWait;
                }
            };
            sICHomeClamped.OnExit += (sender, e) =>
            { };

            sCCHomeClamped.OnEntry += (sender, e) =>
            { };
            sCCHomeClamped.OnExit += (sender, e) =>
            { };

            #region Change Direction
            sChangingDirectionToLPHome.OnEntry += (sender, e) =>
            {
                Func<StateGuardRtns> guard = () =>
                {
                    var rtn = new StateGuardRtns
                    {
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        ThisStateExitEventArgs = new MacStateExitEventArgs(),
                        Transition = tChangingDirectionToLPHome_LPHome,
                    };
                    return rtn;
                };
                Action<object> action = (parameter) =>
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    HalMaskTransfer.RobotMoving(false);
                };
                object actionParameter = null;
                Action<Exception> exceptionHandler = (ex) =>
                { throw ex; };
                Trigger(guard, action, actionParameter, exceptionHandler);
            };
            sChangingDirectionToLPHome.OnExit += (sender, e) =>
            { };

            sChangingDirectionToLPHomeClamped.OnEntry += (sender, e) =>
            {
                Func<StateGuardRtns> guard = () =>
                {
                    var rtn = new StateGuardRtns
                    {
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        ThisStateExitEventArgs = new MacStateExitEventArgs(),
                        Transition = tChangingDirectionToLPHomeClamped_LPHomeClamped,
                    };
                    return rtn;
                };
                Action<object> action = (parameter) =>
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    HalMaskTransfer.RobotMoving(false);
                };
                object actionParameter = null;
                Action<Exception> exceptionHandler = (ex) =>
                { throw ex; };
                Trigger(guard, action, actionParameter, exceptionHandler);
            };
            sChangingDirectionToLPHomeClamped.OnExit += (sender, e) =>
            { };

            sChangingDirectionToICHome.OnEntry += (sender, e) =>
            {
                Func<StateGuardRtns> guard = () =>
                {
                    var rtn = new StateGuardRtns
                    {
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        ThisStateExitEventArgs = new MacStateExitEventArgs(),
                        Transition = tChangingDirectionToICHome_ICHome,
                    };
                    return rtn;
                };
                Action<object> action = (parameter) =>
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    HalMaskTransfer.RobotMoving(false);
                };
                object actionParameter = null;
                Action<Exception> exceptionHandler = (ex) =>
                { throw ex; };
                Trigger(guard, action, actionParameter, exceptionHandler);
            };
            sChangingDirectionToICHome.OnExit += (sender, e) =>
            { };

            sChangingDirectionToICHomeClamped.OnEntry += (sender, e) =>
            {
                Func<StateGuardRtns> guard = () =>
                {
                    var rtn = new StateGuardRtns
                    {
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        ThisStateExitEventArgs = new MacStateExitEventArgs(),
                        Transition = tChangingDirectionToICHomeClamped_ICHomeClamped,
                    };
                    return rtn;
                };
                Action<object> action = (parameter) =>
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    HalMaskTransfer.RobotMoving(false);
                };
                object actionParameter = null;
                Action<Exception> exceptionHandler = (ex) =>
                { throw ex; };
                Trigger(guard, action, actionParameter, exceptionHandler);
            };
            sChangingDirectionToICHomeClamped.OnExit += (sender, e) =>
            { };

            sChangingDirectionToCCHomeClamped.OnEntry += (sender, e) =>
            {
                Func<StateGuardRtns> guard = () =>
                {
                    var rtn = new StateGuardRtns
                    {
                        NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                        ThisStateExitEventArgs = new MacStateExitEventArgs(),
                        Transition = tChangingDirectionToCCHomeClamped_CCHomeClamped,
                    };
                    return rtn;
                };
                Action<object> action = (parameter) =>
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\CleanChHome.json");
                    HalMaskTransfer.RobotMoving(false);
                };
                object actionParameter = null;
                Action<Exception> exceptionHandler = (ex) =>
                { throw ex; };
                Trigger(guard, action, actionParameter, exceptionHandler);
            };
            sChangingDirectionToCCHomeClamped.OnExit += (sender, e) =>
            { };
            #endregion Change Direction

            #region Load PortA
            sMovingToLoadPortA.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLoadPortA)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingToLoadPortA_LoadPortAClamping;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 60))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToLoadPortA.OnExit += (sender, e) =>
            { };

            sLoadPortAClamping.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.LoadPortAClamping)
                        {
                            try
                            {
                                var MaskType = (uint)e.Parameter;
                                HalMaskTransfer.Clamp(MaskType);
                                transition = tLoadPortAClamping_MovingToLPHomeClampedFromLoadPortA;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sLoadPortAClamping.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeClampedFromLoadPortA.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeClampedFromLoadPortA)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingToLPHomeClampedFromLoadPortA_LPHomeClamped;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 60))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToLPHomeClampedFromLoadPortA.OnExit += (sender, e) =>
            { };

            sMovingToLoadPortAForRelease.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLoadPortAForRelease)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingToLoadPortAForRelease_LoadPortAReleasing;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 60))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToLoadPortAForRelease.OnExit += (sender, e) =>
            { };

            sLoadPortAReleasing.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.LoadPortAReleasing)
                        {
                            try
                            {
                                HalMaskTransfer.Unclamp();
                                transition = tLoadPortAReleasing_MovingToLPHomeFromLoadPortA;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sLoadPortAReleasing.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeFromLoadPortA.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeFromLoadPortA)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                                HalMaskTransfer.RobotMoving(true);
                                transition = tMovingToLPHomeFromLoadPortA_LPHome;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 60))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToLPHomeFromLoadPortA.OnExit += (sender, e) =>
            { };
            #endregion Load PortA

            #region Load PortB
            sMovingToLoadPortB.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLoadPortB)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingToLoadPortB_LoadPortBClamping;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 60))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToLoadPortB.OnExit += (sender, e) =>
            { };

            sLoadPortBClamping.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.LoadPortBClamping)
                        {
                            try
                            {
                                var MaskType = (uint)e.Parameter;
                                HalMaskTransfer.Clamp(MaskType);
                                transition = tLoadPortBClamping_MovingToLPHomeClampedFromLoadPortB;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sLoadPortBClamping.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeClampedFromLoadPortB.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeClampedFromLoadPortB)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingToLPHomeClampedFromLoadPortB_LPHomeClamped;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 60))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToLPHomeClampedFromLoadPortB.OnExit += (sender, e) =>
            { };

            sMovingToLoadPortBForRelease.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLoadPortBForRelease)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingToLoadPortBForRelease_LoadPortBReleasing;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 60))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToLoadPortBForRelease.OnExit += (sender, e) =>
            { };

            sLoadPortBReleasing.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.LoadPortBReleasing)
                        {
                            try
                            {
                                HalMaskTransfer.Unclamp();
                                transition = tLoadPortBReleasing_MovingToLPHomeFromLoadPortB;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sLoadPortBReleasing.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeFromLoadPortB.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeFromLoadPortB)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingToLPHomeFromLoadPortB_LPHome;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 60))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToLPHomeFromLoadPortB.OnExit += (sender, e) =>
            { };
            #endregion Load PortB

            #region Inspection Ch
            sMovingToInspectionCh.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingToInspectionCh;
                        if (isGuard)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICHomeBackSideToIC.json");
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingToInspectionCh_InspectionChClamping;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToInspectionCh.OnExit += (sender, e) =>
            { };

            sInspectionChClamping.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.InspectionChClamping)
                        {
                            try
                            {
                                var MaskType = (uint)e.Parameter;
                                HalMaskTransfer.Clamp(MaskType);
                                transition = tInspectionChClamping_MovingToICHomeClampedFromInspectionCh;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sInspectionChClamping.OnExit += (sender, e) =>
            { };

            sMovingToICHomeClampedFromInspectionCh.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToICHomeClampedFromInspectionCh)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICBackSideToICHome.json");
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingToICHomeClampedFromInspectionCh_ICHomeClamped;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToICHomeClampedFromInspectionCh.OnExit += (sender, e) =>
            { };

            sMovingToInspectionChForRelease.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingInspectionChForRelease)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICHomeBackSideToIC.json");
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingInspectionChForRelease_InspectionChReleasing;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToInspectionChForRelease.OnExit += (sender, e) =>
            { };

            sInspectionChReleasing.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.InspectionChReleasing)
                        {
                            try
                            {
                                HalMaskTransfer.Unclamp();
                                transition = tInspectionChReleasing_MovingToICHomeFromInspectionCh;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sInspectionChReleasing.OnExit += (sender, e) =>
            { };

            sMovingToICHomeFromInspectionCh.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToICHomeFromInspectionCh)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICBackSideToICHome.json");
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingToICHomeFromInspectionCh_ICHome;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToICHomeFromInspectionCh.OnExit += (sender, e) =>
            { };



            sMovingToInspectionChGlass.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToInspectionChGlass)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingToInspectionChGlass_InspectionChGlassClamping;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToInspectionChGlass.OnExit += (sender, e) =>
            { };

            sInspectionChGlassClamping.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.InspectionChGlassClamping)
                        {
                            try
                            {
                                var MaskType = (uint)e.Parameter;
                                HalMaskTransfer.Clamp(MaskType);
                                transition = tInspectionChGlassClamping_MovingToICHomeClampedFromInspectionChGlass;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sInspectionChGlassClamping.OnExit += (sender, e) =>
            { };

            sMovingToICHomeClampedFromInspectionChGlass.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToICHomeClampedFromInspectionChGlass)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingToICHomeClampedFromInspectionChGlass_ICHomeClamped;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToICHomeClampedFromInspectionChGlass.OnExit += (sender, e) =>
            { };

            sMovingToInspectionChGlassForRelease.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingInspectionChGlassForRelease)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingInspectionChGlassForRelease_InspectionChGlassReleasing;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToInspectionChGlassForRelease.OnExit += (sender, e) =>
            { };

            sInspectionChGlassReleasing.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.Initial;
                        if (isGuard)
                        {
                            try
                            {
                                HalMaskTransfer.Unclamp();
                                transition = tInspectionChGlassReleasing_MovingToICHomeFromInspectionChGlass;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sInspectionChGlassReleasing.OnExit += (sender, e) =>
            { };

            sMovingToICHomeFromInspectionChGlass.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToICHomeFromInspectionChGlass)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingToICHomeFromInspectionChGlass_ICHome;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToICHomeFromInspectionChGlass.OnExit += (sender, e) =>
            { };
            #endregion

            #region Clean Ch
            sCleanReady.OnEntry += (sender, e) =>
            { };
            sCleanReady.OnExit += (sender, e) =>
            { };

            sCleanMovingStart.OnEntry += (sender, e) =>
            { };
            sCleanMovingStart.OnExit += (sender, e) =>
            { };

            sCleanMovingReturn.OnEntry += (sender, e) =>
            { };
            sCleanMovingReturn.OnExit += (sender, e) =>
            { };

            sCleanChMoving.OnEntry += (sender, e) =>
            { };
            sCleanChMoving.OnExit += (sender, e) =>
            { };

            sCleanChWaitAckMove.OnEntry += (sender, e) =>
            { };
            sCleanChWaitAckMove.OnExit += (sender, e) =>
            { };
            #endregion

            #region OpenStage
            sMovingToOpenStage.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToOpenStage)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingToOpenStage_OpenStageClamping;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 60))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToOpenStage.OnExit += (sender, e) =>
            { };

            sOpenStageClamping.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.OpenStageClamping)
                        {
                            try
                            {
                                var MaskType = (uint)e.Parameter;
                                HalMaskTransfer.Clamp(MaskType);
                                transition = tOpenStageClamping_MovingToLPHomeClampedFromOpenStage;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 60))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sOpenStageClamping.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeClampedFromOpenStage.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeClampedFromOpenStage)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingToLPHomeClampedFromOpenStage_LPHomeClamped;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 60))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToLPHomeClampedFromOpenStage.OnExit += (sender, e) =>
            { };

            sMovingOpenStageForRelease.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingOpenStageForRelease)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingOpenStageForRelease_OpenStageReleasing;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 60))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingOpenStageForRelease.OnExit += (sender, e) =>
            { };

            sOpenStageReleasing.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.OpenStageReleasing)
                        {
                            try
                            {
                                HalMaskTransfer.Unclamp();
                                transition = tOpenStageReleasing_MovingToLPHomeFromOpenStage;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sOpenStageReleasing.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeFromOpenStage.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeFromOpenStage)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingToLPHomeFromOpenStage_LPHome;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 60))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToLPHomeFromOpenStage.OnExit += (sender, e) =>
            { };
            #endregion

            #region Barcode Reader
            sMovingToBarcodeReaderClamped.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToBarcodeReaderClamped)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                if (!HalMaskTransfer.CheckPosition(@"D:\Positions\MTRobot\LoadPortHome.json"))
                                    HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                                // TODO: Robot move path
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingToBarcodeReaderClamped_WaitingForBarcodeReader;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToBarcodeReaderClamped.OnExit += (sender, e) =>
            { };

            sWaitingForBarcodeReader.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.WaitingForBarcodeReader)
                        {
                            if (false)// TODO: 判斷Barcode Reader已經讀取完等待移走
                            {
                                transition = tWaitingForBarcodeReader_MovingToLPHomeClampedFromBarcodeReader;
                                break;
                            }
                            else
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sWaitingForBarcodeReader.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeClampedFromBarcodeReader.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeClampedFromBarcodeReader)
                        {
                            try
                            {
                                HalMaskTransfer.RobotMoving(true);
                                // TODO: Robot move path
                                HalMaskTransfer.RobotMoving(false);
                                transition = tMovingToLPHomeClampedFromBarcodeReader_LPHomeClamped;
                                break;
                            }
                            catch (Exception)
                            {
                                HalMaskTransfer.RobotMoving(false);
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMovingToLPHomeClampedFromBarcodeReader.OnExit += (sender, e) =>
            { };
            #endregion


            #endregion State Register OnEntry OnExit




            //--- Exception Transition ---

        }


        public class MacMaskTransferUnitStateTimeOutController
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
}