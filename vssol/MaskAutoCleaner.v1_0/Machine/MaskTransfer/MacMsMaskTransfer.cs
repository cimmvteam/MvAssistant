using MaskAutoCleaner.v1_0.Msg;
using MaskAutoCleaner.v1_0.Msg.PrescribedSecs;
using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace MaskAutoCleaner.v1_0.Machine.MaskTransfer
{

    /// <summary>
    /// MaskTransfer state machine
    /// </summary>
    [Guid("3C333536-8B09-43B0-9F56-957920050CFB")]
    public class MacMsMaskTransfer : MacMachineStateBase
    {
        public IMacHalMaskTransfer HalMaskTransfer { get { return this.halAssembly as IMacHalMaskTransfer; } }

        public MacMsMaskTransfer() { LoadStateMachine(); }

        public void Initial()
        {
            this.States[EnumMacMsMaskTransferState.Initial.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }

        public override void LoadStateMachine()
        {
            //--- Declare State ---

            MacState sStart = NewState(EnumMacMsMaskTransferState.Start);
            MacState sDeviceInitial = NewState(EnumMacMsMaskTransferState.Initial);
            MacState sLPHome = NewState(EnumMacMsMaskTransferState.LPHome);
            MacState sICHome = NewState(EnumMacMsMaskTransferState.ICHome);

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

            //Clamped Back - Home
            MacState sLPHomeClamped = NewState(EnumMacMsMaskTransferState.LPHomeClamped);
            MacState sICHomeClamped = NewState(EnumMacMsMaskTransferState.ICHomeClamped);
            //MacState sReadyToRelease = NewState(EnumMacMsMaskTransferState.ReadyToRelease);

            //Barcode Reader
            MacState sMovingToBarcodeReaderClamped = NewState(EnumMacMsMaskTransferState.MovingToBarcodeReader);
            MacState sBarcodeReader = NewState(EnumMacMsMaskTransferState.BarcodeReader);
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
            MacState sMovingInspectionChForRelease = NewState(EnumMacMsMaskTransferState.MovingInspectionChForRelease);
            MacState sMovingInspectionChGlassForRelease = NewState(EnumMacMsMaskTransferState.MovingInspectionChGlassForRelease);
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



            #region State Register OnEntry OnExit

            //Normal Entry
            sStart.OnEntry += sStart_OnEntry;
            sDeviceInitial.OnEntry += sDeviceInitial_OnEntry;
            sLPHome.OnEntry += sLPHome_OnEntry;
            sICHome.OnEntry += sICHome_OnEntry;
            sMovingToLoadPortA.OnEntry += sMovingToLoadPortA_OnEntry;
            sMovingToLoadPortB.OnEntry += sMovingToLoadPortB_OnEntry;
            sMovingToInspectionCh.OnEntry += sMovingToInspectionCh_OnEntry;
            sMovingToInspectionChGlass.OnEntry += sMovingToInspectionChGlass_OnEntry;
            sMovingToOpenStage.OnEntry += sMovingToOpenStage_OnEntry;
            sLoadPortAClamping.OnEntry += sLoadPortAClamping_OnEntry;
            sLoadPortBClamping.OnEntry += sLoadPortBClamping_OnEntry;
            sInspectionChClamping.OnEntry += sInspectionChClamping_OnEntry;
            sInspectionChGlassClamping.OnEntry += sInspectionChGlassClamping_OnEntry;
            sOpenStageClamping.OnEntry += sOpenStageClamping_OnEntry;
            sMovingToLPHomeClampedFromLoadPortA.OnEntry += sMovingToLPHomeClampedFromLoadPortA_OnEntry;
            sMovingToLPHomeClampedFromLoadPortB.OnEntry += sMovingToLPHomeClampedFromLoadPortB_OnEntry;
            sMovingToICHomeClampedFromInspectionCh.OnEntry += sMovingToICHomeClampedFromInspectionCh_OnEntry;
            sMovingToICHomeClampedFromInspectionChGlass.OnEntry += sMovingToICHomeClampedFromInspectionChGlass_OnEntry;
            sMovingToLPHomeClampedFromOpenStage.OnEntry += sMovingToLPHomeClampedFromOpenStage_OnEntry;
            sLPHomeClamped.OnEntry += sLPHomeClamped_OnEntry;
            sICHomeClamped.OnEntry += sICHomeClamped_OnEntry;
            //sReadyToRelease.OnEntry += sReadyToRelease_OnEntry;
            sCleanReady.OnEntry += sCleanReady_OnEntry;
            sCleanMovingStart.OnEntry += sCleanMovingStart_OnEntry;
            sCleanMovingReturn.OnEntry += sCleanMovingReturn_OnEntry;
            sCleanChMoving.OnEntry += sCleanChMoving_OnEntry;
            sCleanChWaitAckMove.OnEntry += sCleanChWaitAckMove_OnEntry;
            sMovingToLoadPortAForRelease.OnEntry += sMovingToLoadPortAForRelease_OnEntry;
            sMovingToLoadPortBForRelease.OnEntry += sMovingToLoadPortBForRelease_OnEntry;
            sMovingToBarcodeReaderClamped.OnEntry += sMovingToBarcodeReaderClamped_OnEntry;
            sBarcodeReader.OnEntry += sBarcodeReader_OnEntry;
            sMovingToLPHomeClampedFromBarcodeReader.OnEntry += sMovingToLPHomeClampedFromBarcodeReader_OnEntry;
            sMovingInspectionChForRelease.OnEntry += sMovingInspectionChForRelease_OnEntry;
            sMovingInspectionChGlassForRelease.OnEntry += sMovingInspectionChGlassForRelease_OnEntry;
            sMovingOpenStageForRelease.OnEntry += sMovingOpenStageForRelease_OnEntry;
            sLoadPortAReleasing.OnEntry += sLoadPortAReleasing_OnEntry;
            sLoadPortBReleasing.OnEntry += sLoadPortBReleasing_OnEntry;
            sInspectionChReleasing.OnEntry += sInspectionChReleasing_OnEntry;
            sInspectionChGlassReleasing.OnEntry += sInspectionChGlassReleasing_OnEntry;
            sOpenStageReleasing.OnEntry += sOpenStagReleasing_OnEntry;
            sMovingToLPHomeFromLoadPortA.OnEntry += sMovingToLPHomeFromLoadPortA_OnEntry;
            sMovingToLPHomeFromLoadPortB.OnEntry += sMovingToLPHomeFromLoadPortB_OnEntry;
            sMovingToICHomeFromInspectionCh.OnEntry += sMovingToICHomeFromInspectionCh_OnEntry;
            sMovingToICHomeFromInspectionChGlass.OnEntry += sMovingToICHomeFromInspectionChGlass_OnEntry;
            sMovingToLPHomeFromOpenStage.OnEntry += sMovingToLPHomeFromOpenStage_OnEntry;
            //sWaitAckHome.OnEntry += sWaitAckHome_OnEntry;





            //Normal Exit
            sStart.OnExit += sStart_OnExit;
            sDeviceInitial.OnExit += sDeviceInitial_OnExit;
            sLPHome.OnExit += sLPHome_OnExit;
            sICHome.OnExit += sICHome_OnExit;
            sMovingToLoadPortA.OnExit += sMovingToLoadPortA_OnExit;
            sMovingToLoadPortB.OnExit += sMovingToLoadPortB_OnExit;
            sMovingToInspectionCh.OnExit += sMovingToInspectionCh_OnExit;
            sMovingToInspectionChGlass.OnExit += sMovingToInspectionChGlass_OnExit;
            sMovingToOpenStage.OnExit += sMovingToOpenStage_OnExit;
            sLoadPortAClamping.OnExit += sLoadPortAClamping_OnExit;
            sLoadPortBClamping.OnExit += sLoadPortBClamping_OnExit;
            sInspectionChClamping.OnExit += sInspectionChCalibration_OnExit;
            sInspectionChGlassClamping.OnExit += sInspectionChGlassCalibration_OnExit;
            sOpenStageClamping.OnExit += sOpenStageCalibration_OnExit;
            sMovingToLPHomeClampedFromLoadPortA.OnExit += sMovingToLPHomeClampedFromLoadPortA_OnExit;
            sMovingToLPHomeClampedFromLoadPortB.OnExit += sMovingToLPHomeClampedFromLoadPortB_OnExit;
            sMovingToICHomeClampedFromInspectionCh.OnExit += sMovingToICHomeClampedFromInspectionCh_OnExit;
            sMovingToICHomeClampedFromInspectionChGlass.OnExit += sMovingToICHomeClampedFromInspectionChGlas_OnExit;
            sMovingToLPHomeClampedFromOpenStage.OnExit += sMovingToLPHomeClampedFromOpenStage_OnExit;
            sLPHomeClamped.OnExit += sLPHomeClamped_OnExit;
            sICHomeClamped.OnExit += sICHomeClamped_OnExit;
            //sReadyToRelease.OnExit += sReadyToRelease_OnExit;
            sCleanReady.OnExit += sCleanReady_OnExit;
            sCleanMovingStart.OnExit += sCleanMovingStart_OnExit;
            sCleanMovingReturn.OnExit += sCleanMovingReturn_OnExit;
            sCleanChMoving.OnExit += sCleanChMoving_OnExit;
            sCleanChWaitAckMove.OnExit += sCleanChWaitAckMove_OnExit;
            sMovingToBarcodeReaderClamped.OnExit += sMovingToBarcodeReader_OnExit;
            sBarcodeReader.OnExit += sBarcodeReader_OnExit;
            sMovingToLPHomeClampedFromBarcodeReader.OnExit += sMovingToLPHomeClampedFromBarcodeReader_OnExit;
            sMovingToLoadPortAForRelease.OnExit += sMovingToLoadPortAForRelease_OnExit;
            sMovingToLoadPortBForRelease.OnExit += sMovingToLoadPortBForRelease_OnExit;
            sMovingInspectionChForRelease.OnExit += sMovingInspectionChForRelease_OnExit;
            sMovingInspectionChGlassForRelease.OnExit += sMovingInspectionChGlassForRelease_OnExit;
            sMovingOpenStageForRelease.OnExit += sMovingOpenStageForRelease_OnExit;
            sLoadPortAReleasing.OnExit += sLoadPortAReleasing_OnExit;
            sLoadPortBReleasing.OnExit += sLoadPortBReleasing_OnExit;
            sInspectionChReleasing.OnExit += sInspectionChCalibrationForRelease_OnExit;
            sInspectionChGlassReleasing.OnExit += sInspectionChGlassCalibrationForRelease_OnExit;
            sOpenStageReleasing.OnExit += sOpenStageCalibrationForRelease_OnExit;
            sMovingToLPHomeFromLoadPortA.OnExit += sMovingToLPHomeFromLoadPortA_OnExit;
            sMovingToLPHomeFromLoadPortB.OnExit += sMovingToLPHomeFromLoadPortB_OnExit;
            sMovingToICHomeFromInspectionCh.OnExit += sMovingToICHomeFromInspectionCh_OnExit;
            sMovingToICHomeFromInspectionChGlass.OnExit += sMovingToICHomeFromInspectionChGlass_OnExit;
            sMovingToLPHomeFromOpenStage.OnExit += sMovingToHomeFromOpenStage_OnExit;
            //sWaitAckHome.OnExit += sWaitAckHome_OnExit;


            #endregion State Register OnEntry OnExit

            //--- Transition ---

            MacTransition tStart_DeviceInitial = NewTransition(sStart, sDeviceInitial, EnumMacMsMaskTransferTransition.PowerOn);
            MacTransition tDeviceInitial_LPHome = NewTransition(sDeviceInitial, sLPHome, EnumMacMsMaskTransferTransition.CompleteInitial);

            //Receive Transfer From Home
            MacTransition tLPHome_MovingToLoadPortA = NewTransition(sLPHome, sMovingToLoadPortA, EnumMacMsMaskTransferTransition.ReadyToMoveToLoadPortA);
            MacTransition tLPHome_MovingToLoadPortB = NewTransition(sLPHome, sMovingToLoadPortB, EnumMacMsMaskTransferTransition.ReadyToMoveToLoadPortB);
            MacTransition tICHome_MovingToInspectionCh = NewTransition(sICHome, sMovingToInspectionCh, EnumMacMsMaskTransferTransition.ReceiveTransferMask);
            MacTransition tICHome_MovingToInspectionChGlass = NewTransition(sICHome, sMovingToInspectionChGlass, EnumMacMsMaskTransferTransition.ReceiveTransferMask);
            MacTransition tLPHome_MovingToOpenStage = NewTransition(sLPHome, sMovingToOpenStage, EnumMacMsMaskTransferTransition.ReceiveTransferMask);

            //Complete Move
            MacTransition tMovingToLoadPortA_LoadPortAClamping = NewTransition(sMovingToLoadPortA, sLoadPortAClamping, EnumMacMsMaskTransferTransition.ReadyToClampInLoadPortA);
            MacTransition tMovingToLoadPortB_LoadPortBClamping = NewTransition(sMovingToLoadPortB, sLoadPortBClamping, EnumMacMsMaskTransferTransition.ReadyToClampInLoadPortB);
            MacTransition tMovingToInspectionCh_InspectionChClamping = NewTransition(sMovingToInspectionCh, sInspectionChClamping, EnumMacMsMaskTransferTransition.CleanMoveComplete);
            MacTransition tMovingToInspectionChGlass_InspectionChGlassClamping = NewTransition(sMovingToInspectionChGlass, sInspectionChGlassClamping, EnumMacMsMaskTransferTransition.CleanMoveComplete);
            MacTransition tMovingToOpenStage_OpenStageClamping = NewTransition(sMovingToOpenStage, sOpenStageClamping, EnumMacMsMaskTransferTransition.CleanMoveComplete);

            //Compelte Clamped -> 準備移回Home_Clamped
            MacTransition tLoadPortAClamping_MovingToLPHomeClampedFromLoadPortA = NewTransition(sLoadPortAClamping, sMovingToLPHomeClampedFromLoadPortA, EnumMacMsMaskTransferTransition.ReadyToMoveToLPHomeFromLoadPortA);
            MacTransition tLoadPortBClamping_MovingToLPHomeClampedFromLoadPortB = NewTransition(sLoadPortBClamping, sMovingToLPHomeClampedFromLoadPortB, EnumMacMsMaskTransferTransition.ReadyToMoveToLPHomeFromLoadPortB);
            MacTransition tInspectionChClamping_MovingToICHomeClampedFromInspectionCh = NewTransition(sInspectionChClamping, sMovingToICHomeClampedFromInspectionCh, EnumMacMsMaskTransferTransition.CompleteClamped);
            MacTransition tInspectionChGlassClamping_MovingToICHomeClampedFromInspectionChGlass = NewTransition(sInspectionChGlassClamping, sMovingToICHomeClampedFromInspectionChGlass, EnumMacMsMaskTransferTransition.CompleteClamped);
            MacTransition tOpenStageClamping_MovingToLPHomeClampedFromOpenStage = NewTransition(sOpenStageClamping, sMovingToLPHomeClampedFromOpenStage, EnumMacMsMaskTransferTransition.CompleteClamped);

            //Complete Move
            MacTransition tMovingToLPHomeClampedFromLoadPortA_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromLoadPortA, sLPHomeClamped, EnumMacMsMaskTransferTransition.ReadyToStandbyAtLPHome);
            MacTransition tMovingToLPHomeClampedFromLoadPortB_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromLoadPortB, sLPHomeClamped, EnumMacMsMaskTransferTransition.ReadyToStandbyAtLPHome);
            MacTransition tMovingToICHomeClampedFromInspectionCh_ICHomeClamped = NewTransition(sMovingToICHomeClampedFromInspectionCh, sICHomeClamped, EnumMacMsMaskTransferTransition.CleanMoveComplete);
            MacTransition tMovingToICHomeClampedFromInspectionChGlass_ICHomeClamped = NewTransition(sMovingToICHomeClampedFromInspectionChGlass, sICHomeClamped, EnumMacMsMaskTransferTransition.CleanMoveComplete);
            MacTransition tMovingToLPHomeClampedFromOpenStage_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromOpenStage, sLPHomeClamped, EnumMacMsMaskTransferTransition.CleanMoveComplete);


            //Is Ready to Release
            //MacTransition tLPHomeClamped_ReadyToRelease = NewTransition(sLPHomeClamped, sReadyToRelease, EnumMacMsMaskTransferTransition.IsReady);
            MacTransition tLPHomeClamped_MovingToBarcodeReader = NewTransition(sLPHomeClamped, sMovingToBarcodeReaderClamped, EnumMacMsMaskTransferTransition.IsReady);

            //Barcode Reader
            MacTransition tMovingToBarcodeReader_BarcodeReader = NewTransition(sMovingToBarcodeReaderClamped, sBarcodeReader, EnumMacMsMaskTransferTransition.CleanMoveComplete);
            MacTransition tBarcodeReader_MovingToLPHomeClampedFromBarcodeReader = NewTransition(sBarcodeReader, sMovingToLPHomeClampedFromBarcodeReader, EnumMacMsMaskTransferTransition.CompleteRead);
            MacTransition tMovingToLPHomeClampedFromBarcodeReader_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromBarcodeReader, sLPHomeClamped, EnumMacMsMaskTransferTransition.CleanMoveComplete);


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
            MacTransition tMovingToLoadPortAForRelease_LoadPortAReleasing = NewTransition(sMovingToLoadPortAForRelease, sLoadPortAReleasing, EnumMacMsMaskTransferTransition.ReadyToReleaseInLoadPortA);
            MacTransition tMovingToLoadPortBForRelease_LoadPortBReleasing = NewTransition(sMovingToLoadPortBForRelease, sLoadPortBReleasing, EnumMacMsMaskTransferTransition.ReadyToReleaseInLoadPortB);
            MacTransition tMovingInspectionChForRelease_InspectionChReleasing = NewTransition(sMovingInspectionChForRelease, sInspectionChReleasing, EnumMacMsMaskTransferTransition.CleanMoveComplete);
            MacTransition tMovingInspectionChGlassForRelease_InspectionChGlassReleasing = NewTransition(sMovingInspectionChGlassForRelease, sInspectionChGlassReleasing, EnumMacMsMaskTransferTransition.CleanMoveComplete);
            MacTransition tMovingOpenStageForRelease_OpenStageReleasing = NewTransition(sMovingOpenStageForRelease, sOpenStageReleasing, EnumMacMsMaskTransferTransition.CleanMoveComplete);


            //Complete Clamp
            MacTransition tLoadPortAReleasing_MovingToLPHomeFromLoadPortA = NewTransition(sLoadPortAReleasing, sMovingToLPHomeFromLoadPortA, EnumMacMsMaskTransferTransition.ReadyToMoveToLPHomeFromLoadPortA);
            MacTransition tLoadPortBReleasing_MovingToLPHomeFromLoadPortB = NewTransition(sLoadPortBReleasing, sMovingToLPHomeFromLoadPortB, EnumMacMsMaskTransferTransition.ReadyToMoveToLPHomeFromLoadPortB);
            MacTransition tInspectionChReleasing_MovingToICHomeFromInspectionCh = NewTransition(sInspectionChReleasing, sMovingToICHomeFromInspectionCh, EnumMacMsMaskTransferTransition.CompleteReleased);
            MacTransition tInspectionChGlassReleasing_MovingToICHomeFromInspectionChGlass = NewTransition(sInspectionChGlassReleasing, sMovingToICHomeFromInspectionChGlass, EnumMacMsMaskTransferTransition.CompleteReleased);
            MacTransition tOpenStageReleasing_MovingToLPHomeFromOpenStage = NewTransition(sOpenStageReleasing, sMovingToLPHomeFromOpenStage, EnumMacMsMaskTransferTransition.CompleteReleased);


            //Complete Move
            //MacTransition tMovingToLPHomeFromLoadPort_WaitAckHome = NewTransition(sMovingToLPHomeFromLoadPort, sWaitAckHome, EnumMacMsMaskTransferTransition.CleanMoveComplete);
            //MacTransition tMovingToICHomeFromInspectionCh_WaitAckHome = NewTransition(sMovingToICHomeFromInspectionCh, sWaitAckHome, EnumMacMsMaskTransferTransition.CleanMoveComplete);
            //MacTransition tMovingToICHomeFromInspectionChGlass_WaitAckHome = NewTransition(sMovingToICHomeFromInspectionChGlass, sWaitAckHome, EnumMacMsMaskTransferTransition.CleanMoveComplete);
            //MacTransition tMovingToLPHomeFromOpenStage_WaitAckHome = NewTransition(sMovingToLPHomeFromOpenStage, sWaitAckHome, EnumMacMsMaskTransferTransition.CleanMoveComplete);

            //MacTransition tWaitAckHome_LPHome = NewTransition(sWaitAckHome, sLPHome, EnumMacMsMaskTransferTransition.ReceiveAckHome);

            //--- Exception Transition ---

        }





        #region State OnEntry
        private void sStart_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sDeviceInitial_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.Initial();
        }

        private void sLPHome_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sLPHomeClamped_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sICHome_OnEntry(object sender, MacStateEntryEventArgs e)
        {

        }

        private void sICHomeClamped_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        #region Load Port A
        private void sMovingToLoadPortA_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);
            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
            HalMaskTransfer.RobotMoving(false);
        }

        private void sLoadPortAClamping_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var MaskType = (uint)e.Parameter;
            HalMaskTransfer.Clamp(MaskType);
        }

        private void sMovingToLPHomeClampedFromLoadPortA_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);
            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
            HalMaskTransfer.RobotMoving(false);
        }

        private void sMovingToLoadPortAForRelease_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);
            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
            HalMaskTransfer.RobotMoving(false);
        }

        private void sLoadPortAReleasing_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.Unclamp();
        }

        private void sMovingToLPHomeFromLoadPortA_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);
            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
            HalMaskTransfer.RobotMoving(true);
        }
        #endregion

        #region Load Port B
        private void sMovingToLoadPortB_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);
            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
            HalMaskTransfer.RobotMoving(false);
        }

        private void sLoadPortBClamping_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var MaskType = (uint)e.Parameter;
            HalMaskTransfer.Clamp(MaskType);
        }

        private void sMovingToLPHomeClampedFromLoadPortB_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);
            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
            HalMaskTransfer.RobotMoving(false);
        }

        private void sMovingToLoadPortBForRelease_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);
            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
            HalMaskTransfer.RobotMoving(false);
        }

        private void sLoadPortBReleasing_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.Unclamp();
        }

        private void sMovingToLPHomeFromLoadPortB_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);
            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
            HalMaskTransfer.RobotMoving(false);
        }
        #endregion

        #region Inspection Chamber
        private void sMovingToInspectionCh_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);
            
            HalMaskTransfer.RobotMoving(false);
        }
        
        private void sInspectionChClamping_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingToICHomeClampedFromInspectionCh_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);

            HalMaskTransfer.RobotMoving(false);
        }

        private void sMovingInspectionChForRelease_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);

            HalMaskTransfer.RobotMoving(false);
        }

        private void sInspectionChReleasing_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingToICHomeFromInspectionCh_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);

            HalMaskTransfer.RobotMoving(false);
        }



        private void sMovingToInspectionChGlass_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);

            HalMaskTransfer.RobotMoving(false);
        }

        private void sInspectionChGlassClamping_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingToICHomeClampedFromInspectionChGlass_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);

            HalMaskTransfer.RobotMoving(false);
        }

        private void sMovingInspectionChGlassForRelease_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);

            HalMaskTransfer.RobotMoving(false);
        }

        private void sInspectionChGlassReleasing_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }
        
        private void sMovingToICHomeFromInspectionChGlass_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);

            HalMaskTransfer.RobotMoving(false);
        }
        #endregion

        #region Clean Chamber
        private void sCleanChMoving_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sCleanMovingStart_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sCleanMovingReturn_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sCleanReady_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }
        #endregion

        #region Open Stage
        private void sMovingToOpenStage_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);
            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
            HalMaskTransfer.RobotMoving(false);
        }

        private void sOpenStageClamping_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var MaskType = (uint)e.Parameter;
            HalMaskTransfer.Clamp(MaskType);
        }

        private void sMovingToLPHomeClampedFromOpenStage_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);
            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
            HalMaskTransfer.RobotMoving(false);
        }

        private void sMovingOpenStageForRelease_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);
            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
            HalMaskTransfer.RobotMoving(false);
        }

        private void sOpenStagReleasing_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.Unclamp();
        }

        private void sMovingToLPHomeFromOpenStage_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);
            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
            HalMaskTransfer.RobotMoving(false);
        }
        #endregion

        #region Barcode Reader
        private void sMovingToBarcodeReaderClamped_OnEntry(Object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);
            
            HalMaskTransfer.RobotMoving(false);
        }

        private void sBarcodeReader_OnEntry(Object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingToLPHomeClampedFromBarcodeReader_OnEntry(Object sender, MacStateEntryEventArgs e)
        {
            HalMaskTransfer.RobotMoving(true);

            HalMaskTransfer.RobotMoving(false);
        }
        #endregion

        private void sCleanChWaitAckMove_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }
        
        private void sReadyToRelease_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }
        
        private void sWaitAckHome_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }
        #endregion State OnEntry

        #region State Exit


        private void esExpCalibrationFail_OnExit(object sender, MacStateExitEventArgs e) { }
        private void esExpCalibrationReleaseFail_OnExit(object sender, MacStateExitEventArgs e) { }
        private void esExpForceInClamped_OnExit(object sender, MacStateExitEventArgs e) { }
        private void esExpForceInClamping_OnExit(object sender, MacStateExitEventArgs e) { }
        private void esExpForceInReleased_OnExit(object sender, MacStateExitEventArgs e) { }
        private void esExpForceInReleasing_OnExit(object sender, MacStateExitEventArgs e) { }
        private void esExpMayEsdDamage_OnExit(object sender, MacStateExitEventArgs e) { }
        private void esExpMayEsdDamageInRelease_OnExit(object sender, MacStateExitEventArgs e) { }
        private void esExpRobotPositioningError_OnExit(object sender, MacStateExitEventArgs e) { }
        private void esExpTactileInClamped_OnExit(object sender, MacStateExitEventArgs e) { }
        private void esExpTactileInReleased_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sBarcodeReader_OnExit(Object sender, MacStateExitEventArgs e) { }
        private void sCleanChMoving_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sCleanMovingStart_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sCleanMovingReturn_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sCleanReady_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sCleanChWaitAckMove_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sDeviceInitial_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sLPHome_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sICHome_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sLPHomeClamped_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sICHomeClamped_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sInspectionChCalibration_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sInspectionChCalibrationForRelease_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sInspectionChGlassCalibration_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sInspectionChGlassCalibrationForRelease_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sLoadPortAClamping_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sLoadPortAReleasing_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sLoadPortBClamping_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sLoadPortBReleasing_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingInspectionChForRelease_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingInspectionChGlassForRelease_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingOpenStageForRelease_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToBarcodeReader_OnExit(Object sender, MacStateExitEventArgs e) { }
        private void sMovingToLPHomeClampedFromBarcodeReader_OnExit(Object sender, MacStateExitEventArgs e) { }
        private void sMovingToICHomeClampedFromInspectionCh_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToICHomeClampedFromInspectionChGlas_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToLPHomeClampedFromLoadPortA_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToLPHomeClampedFromLoadPortB_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToLPHomeClampedFromOpenStage_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToICHomeFromInspectionCh_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToICHomeFromInspectionChGlass_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToLPHomeFromLoadPortA_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToLPHomeFromLoadPortB_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToHomeFromOpenStage_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToInspectionCh_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToInspectionChGlass_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToLoadPortA_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToLoadPortAForRelease_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToLoadPortB_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToLoadPortBForRelease_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToOpenStage_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sOpenStageCalibration_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sOpenStageCalibrationForRelease_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sReadyToRelease_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sStart_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sWaitAckHome_OnExit(object sender, MacStateExitEventArgs e) { }
        #endregion State Exit





    }
}