using MaskAutoCleaner.Msg;
using MaskAutoCleaner.Msg.PrescribedSecs;
using MaskAutoCleaner.StateMachine_v1_1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace MaskAutoCleaner.Machine.SmMaskTransfer
{

    /// <summary>
    /// MaskTransfer state machine
    /// </summary>
    public class MacSmMaskTransfer : MacMachineSmBase
    {

        public void LoadCurrentState()
        {
        }
        public void LoadStateMachine()
        {
            //--- Declare State ---

            MacState sStart = NewState(EnumMacSmMaskTransferState.Start);
            MacState sDeviceInitial = NewState(EnumMacSmMaskTransferState.Initial);
            MacState sHome = NewState(EnumMacSmMaskTransferState.Home);

            //To Target Clamp - Move
            MacState sMovingToLoadPort = NewState(EnumMacSmMaskTransferState.MovingToLoadPort);
            MacState sMovingToInspectionCh = NewState(EnumMacSmMaskTransferState.MovingToInspectionCh);
            MacState sMovingToInspectionChGlass = NewState(EnumMacSmMaskTransferState.MovingToInspectionChGlass);
            MacState sMovingToOpenStage = NewState(EnumMacSmMaskTransferState.MovingToOpenStage);
            //To Target Clamp - Calibration
            MacState sLoadPortClamping = NewState(EnumMacSmMaskTransferState.LoadPortClamping);
            MacState sInspectionChClamping = NewState(EnumMacSmMaskTransferState.InspectionChClamping);
            MacState sInspectionChGlassClamping = NewState(EnumMacSmMaskTransferState.InspectionChGlassClamping);
            MacState sOpenStageClamping = NewState(EnumMacSmMaskTransferState.OpenStageClamping);

            //Clamped Back - Move
            MacState sMovingToHomeClampedFromLoadPort = NewState(EnumMacSmMaskTransferState.MovingToHomeClampedFromLoadPort);
            MacState sMovingToHomeClampedFromInspectionCh = NewState(EnumMacSmMaskTransferState.MovingToHomeClampedFromInspectionCh);
            MacState sMovingToHomeClampedFromInspectionChGlass = NewState(EnumMacSmMaskTransferState.MovingToHomeClampedFromInspectionChGlass);
            MacState sMovingToHomeClampedFromOpenStage = NewState(EnumMacSmMaskTransferState.MovingToHomeClampedFromOpenStage);

            //Clamped Back - Home
            MacState sHomeClamped = NewState(EnumMacSmMaskTransferState.HomeClamped);
            MacState sReadyToRelease = NewState(EnumMacSmMaskTransferState.ReadyToRelease);

            //Barcode Reader
            MacState sMovingToBarcodeReader = NewState(EnumMacSmMaskTransferState.MovingToBarcodeReader);
            MacState sBarcodeReader = NewState(EnumMacSmMaskTransferState.BarcodeReader);
            MacState sMovingToHomeClampedFromBarcodeReader = NewState(EnumMacSmMaskTransferState.MovingToHomeClampedFromBarcodeReader);



            //Clean
            MacState sCleanMovingStart = NewState(EnumMacSmMaskTransferState.CleanMovingStart);//前往CleanCh
            MacState sCleanReady = NewState(EnumMacSmMaskTransferState.CleanReady);//準備好Clean
            MacState sCleanMovingReturn = NewState(EnumMacSmMaskTransferState.CleanMovingReturn);//離開CleanCh
            MacState sCleanChMoving = NewState(EnumMacSmMaskTransferState.CleanChMoving);
            MacState sCleanChWaitAckMove = NewState(EnumMacSmMaskTransferState.CleanChWaitAckMove);



            //To Target
            MacState sMovingToLoadPortForRelease = NewState(EnumMacSmMaskTransferState.MovingToLoadPortForRelease);
            MacState sMovingInspectionChForRelease = NewState(EnumMacSmMaskTransferState.MovingInspectionChForRelease);
            MacState sMovingInspectionChGlassForRelease = NewState(EnumMacSmMaskTransferState.MovingInspectionChGlassForRelease);
            MacState sMovingOpenStageForRelease = NewState(EnumMacSmMaskTransferState.MovingOpenStageForRelease);

            MacState sLoadPortReleasing = NewState(EnumMacSmMaskTransferState.LoadPortReleasing);
            MacState sInspectionChReleasing = NewState(EnumMacSmMaskTransferState.InspectionChReleasing);
            MacState sInspectionChGlassReleasing = NewState(EnumMacSmMaskTransferState.InspectionChGlassReleasing);
            MacState sOpenStageReleasing = NewState(EnumMacSmMaskTransferState.OpenStageReleasing);


            MacState sMovingToHomeFromLoadPort = NewState(EnumMacSmMaskTransferState.MovingToHomeFromLoadPort);
            MacState sMovingToHomeFromInspectionCh = NewState(EnumMacSmMaskTransferState.MovingToHomeFromInspectionCh);
            MacState sMovingToHomeFromInspectionChGlass = NewState(EnumMacSmMaskTransferState.MovingToHomeFromInspectionChGlass);
            MacState sMovingToHomeFromOpenStage = NewState(EnumMacSmMaskTransferState.MovingToHomeFromOpenStage);

            MacState sWaitAckHome = NewState(EnumMacSmMaskTransferState.WaitAckHome);



            #region State Register OnEntry OnExit

            //Normal Entry
            sStart.OnEntry += sStart_OnEntry;
            sDeviceInitial.OnEntry += sDeviceInitial_OnEntry;
            sHome.OnEntry += sHome_OnEntry;
            sMovingToLoadPort.OnEntry += sMovingToLoadPort_OnEntry;
            sMovingToInspectionCh.OnEntry += sMovingToInspectionCh_OnEntry;
            sMovingToInspectionChGlass.OnEntry += sMovingToInspectionChGlass_OnEntry;
            sMovingToOpenStage.OnEntry += sMovingToOpenStage_OnEntry;
            sLoadPortClamping.OnEntry += sLoadPortCalibration_OnEntry;
            sInspectionChClamping.OnEntry += sInspectionChCalibration_OnEntry;
            sInspectionChGlassClamping.OnEntry += sInspectionChGlassCalibration_OnEntry;
            sOpenStageClamping.OnEntry += sOpenStageCalibration_OnEntry;
            sMovingToHomeClampedFromLoadPort.OnEntry += sMovingToHomeClampedFromLoadPort_OnEntry;
            sMovingToHomeClampedFromInspectionCh.OnEntry += sMovingToHomeClampedFromInspectionCh_OnEntry;
            sMovingToHomeClampedFromInspectionChGlass.OnEntry += sMovingToHomeClampedFromInspectionChGlass_OnEntry;
            sMovingToHomeClampedFromOpenStage.OnEntry += sMovingToHomeClampedFromOpenStage_OnEntry;
            sHomeClamped.OnEntry += sHomeClamped_OnEntry;
            sReadyToRelease.OnEntry += sReadyToRelease_OnEntry;
            sCleanReady.OnEntry += sCleanReady_OnEntry;
            sCleanMovingStart.OnEntry += sCleanMovingStart_OnEntry;
            sCleanMovingReturn.OnEntry += sCleanMovingReturn_OnEntry;
            sCleanChMoving.OnEntry += sCleanChMoving_OnEntry;
            sCleanChWaitAckMove.OnEntry += sCleanChWaitAckMove_OnEntry;
            sMovingToLoadPortForRelease.OnEntry += sMovingToLoadPortForRelease_OnEntry;
            sMovingToBarcodeReader.OnEntry += sMovingToBarcodeReader_OnEntry;
            sBarcodeReader.OnEntry += sBarcodeReader_OnEntry;
            sMovingToHomeClampedFromBarcodeReader.OnEntry += sMovingToHomeClampedFromBarcodeReader_OnEntry;
            sMovingInspectionChForRelease.OnEntry += sMovingInspectionChForRelease_OnEntry;
            sMovingInspectionChGlassForRelease.OnEntry += sMovingInspectionChGlassForRelease_OnEntry;
            sMovingOpenStageForRelease.OnEntry += sMovingOpenStageForRelease_OnEntry;
            sLoadPortReleasing.OnEntry += sLoadPortCalibrationForRelease_OnEntry;
            sInspectionChReleasing.OnEntry += sInspectionChCalibrationForRelease_OnEntry;
            sInspectionChGlassReleasing.OnEntry += sInspectionChGlassCalibrationForRelease_OnEntry;
            sOpenStageReleasing.OnEntry += sOpenStageCalibrationForRelease_OnEntry;
            sMovingToHomeFromLoadPort.OnEntry += sMovingToHomeFromLoadPort_OnEntry;
            sMovingToHomeFromInspectionCh.OnEntry += sMovingToHomeFromInspectionCh_OnEntry;
            sMovingToHomeFromInspectionChGlass.OnEntry += sMovingToHomeFromInspectionChGlass_OnEntry;
            sMovingToHomeFromOpenStage.OnEntry += sMovingToHomeFromOpenStage_OnEntry;
            sWaitAckHome.OnEntry += sWaitAckHome_OnEntry;





            //Normal Exit
            sStart.OnExit += sStart_OnExit;
            sDeviceInitial.OnExit += sDeviceInitial_OnExit;
            sHome.OnExit += sHome_OnExit;
            sMovingToLoadPort.OnExit += sMovingToLoadPortA_OnExit;
            sMovingToInspectionCh.OnExit += sMovingToInspectionCh_OnExit;
            sMovingToInspectionChGlass.OnExit += sMovingToInspectionChGlass_OnExit;
            sMovingToOpenStage.OnExit += sMovingToOpenStage_OnExit;
            sLoadPortClamping.OnExit += sLoadPortACalibration_OnExit;
            sInspectionChClamping.OnExit += sInspectionChCalibration_OnExit;
            sInspectionChGlassClamping.OnExit += sInspectionChGlassCalibration_OnExit;
            sOpenStageClamping.OnExit += sOpenStageCalibration_OnExit;
            sMovingToHomeClampedFromLoadPort.OnExit += sMovingToHomeClampedFromLoadPortA_OnExit;
            sMovingToHomeClampedFromInspectionCh.OnExit += sMovingToHomeClampedFromInspectionCh_OnExit;
            sMovingToHomeClampedFromInspectionChGlass.OnExit += sMovingToHomeClampedFromInspectionChGlas_OnExit;
            sMovingToHomeClampedFromOpenStage.OnExit += sMovingToHomeClampedFromOpenStage_OnExit;
            sHomeClamped.OnExit += sHomeClamped_OnExit;
            sReadyToRelease.OnExit += sReadyToRelease_OnExit;
            sCleanReady.OnExit += sCleanReady_OnExit;
            sCleanMovingStart.OnExit += sCleanMovingStart_OnExit;
            sCleanMovingReturn.OnExit += sCleanMovingReturn_OnExit;
            sCleanChMoving.OnExit += sCleanChMoving_OnExit;
            sCleanChWaitAckMove.OnExit += sCleanChWaitAckMove_OnExit;
            sMovingToBarcodeReader.OnExit += sMovingToBarcodeReader_OnExit;
            sBarcodeReader.OnExit += sBarcodeReader_OnExit;
            sMovingToHomeClampedFromBarcodeReader.OnExit += sMovingToHomeClampedFromBarcodeReader_OnExit;
            sMovingToLoadPortForRelease.OnExit += sMovingToLoadPortAForRelease_OnExit;
            sMovingInspectionChForRelease.OnExit += sMovingInspectionChForRelease_OnExit;
            sMovingInspectionChGlassForRelease.OnExit += sMovingInspectionChGlassForRelease_OnExit;
            sMovingOpenStageForRelease.OnExit += sMovingOpenStageForRelease_OnExit;
            sLoadPortReleasing.OnExit += sLoadPortACalibrationForRelease_OnExit;
            sInspectionChReleasing.OnExit += sInspectionChCalibrationForRelease_OnExit;
            sInspectionChGlassReleasing.OnExit += sInspectionChGlassCalibrationForRelease_OnExit;
            sOpenStageReleasing.OnExit += sOpenStageCalibrationForRelease_OnExit;
            sMovingToHomeFromLoadPort.OnExit += sMovingToHomeFromLoadPortA_OnExit;
            sMovingToHomeFromInspectionCh.OnExit += sMovingToHomeFromInspectionCh_OnExit;
            sMovingToHomeFromInspectionChGlass.OnExit += sMovingToHomeFromInspectionChGlass_OnExit;
            sMovingToHomeFromOpenStage.OnExit += sMovingToHomeFromOpenStage_OnExit;
            sWaitAckHome.OnExit += sWaitAckHome_OnExit;


            #endregion State Register OnEntry OnExit

            //--- Transition ---

            MacTransition tStart_DeviceInitial = NewTransition(sStart, sDeviceInitial, EnumMacSmMaskTransferTransition.PowerOn);
            MacTransition tDeviceInitial_Home = NewTransition(sDeviceInitial, sHome, EnumMacSmMaskTransferTransition.CompleteInitial);

            //Receive Transfer From Home
            MacTransition tHome_MovingToLoadPort = NewTransition(sHome, sMovingToLoadPort, EnumMacSmMaskTransferTransition.ReceiveTransferMask);
            MacTransition tHome_MovingToInspectionCh = NewTransition(sHome, sMovingToInspectionCh, EnumMacSmMaskTransferTransition.ReceiveTransferMask);
            MacTransition tHome_MovingToInspectionChGlass = NewTransition(sHome, sMovingToInspectionChGlass, EnumMacSmMaskTransferTransition.ReceiveTransferMask);
            MacTransition tHome_MovingToOpenStage = NewTransition(sHome, sMovingToOpenStage, EnumMacSmMaskTransferTransition.ReceiveTransferMask);

            //Complete Move
            MacTransition tMovingToLoadPort_LoadPortClamping = NewTransition(sMovingToLoadPort, sLoadPortClamping, EnumMacSmMaskTransferTransition.CleanMoveComplete);
            MacTransition tMovingToInspectionCh_InspectionChClamping = NewTransition(sMovingToInspectionCh, sInspectionChClamping, EnumMacSmMaskTransferTransition.CleanMoveComplete);
            MacTransition tMovingToInspectionChGlass_InspectionChGlassClamping = NewTransition(sMovingToInspectionChGlass, sInspectionChGlassClamping, EnumMacSmMaskTransferTransition.CleanMoveComplete);
            MacTransition tMovingToOpenStage_OpenStageClamping = NewTransition(sMovingToOpenStage, sOpenStageClamping, EnumMacSmMaskTransferTransition.CleanMoveComplete);

            //Compelte Clamped -> 準備移回Home_Clamped
            MacTransition tLoadPortClamping_MovingToHomeClampedFromLoadPort = NewTransition(sLoadPortClamping, sMovingToHomeClampedFromLoadPort, EnumMacSmMaskTransferTransition.CompleteClamped);
            MacTransition tInspectionChClamping_MovingToHomeClampedFromInspectionCh = NewTransition(sInspectionChClamping, sMovingToHomeClampedFromInspectionCh, EnumMacSmMaskTransferTransition.CompleteClamped);
            MacTransition tInspectionChGlassClamping_MovingToHomeClampedFromInspectionChGlass = NewTransition(sInspectionChGlassClamping, sMovingToHomeClampedFromInspectionChGlass, EnumMacSmMaskTransferTransition.CompleteClamped);
            MacTransition tOpenStageClamping_MovingToHomeClampedFromOpenStage = NewTransition(sOpenStageClamping, sMovingToHomeClampedFromOpenStage, EnumMacSmMaskTransferTransition.CompleteClamped);

            //Complete Move
            MacTransition tMovingToHomeClampedFromLoadPortA_HomeClamped = NewTransition(sMovingToHomeClampedFromLoadPort, sHomeClamped, EnumMacSmMaskTransferTransition.CleanMoveComplete);
            MacTransition tMovingToHomeClampedFromInspectionCh_HomeClamped = NewTransition(sMovingToHomeClampedFromInspectionCh, sHomeClamped, EnumMacSmMaskTransferTransition.CleanMoveComplete);
            MacTransition tMovingToHomeClampedFromInspectionChGlass_HomeClamped = NewTransition(sMovingToHomeClampedFromInspectionChGlass, sHomeClamped, EnumMacSmMaskTransferTransition.CleanMoveComplete);
            MacTransition tMovingToHomeClampedFromOpenStage_HomeClamped = NewTransition(sMovingToHomeClampedFromOpenStage, sHomeClamped, EnumMacSmMaskTransferTransition.CleanMoveComplete);


            //Is Ready to Release
            MacTransition tHomeClamped_ReadyToRelease = NewTransition(sHomeClamped, sReadyToRelease, EnumMacSmMaskTransferTransition.IsReady);
            MacTransition tHomeClamped_MovingToBarcodeReader = NewTransition(sHomeClamped, sMovingToBarcodeReader, EnumMacSmMaskTransferTransition.IsReady);

            //Barcode Reader
            MacTransition tMovingToBarcodeReader_BarcodeReader = NewTransition(sMovingToBarcodeReader, sBarcodeReader, EnumMacSmMaskTransferTransition.CleanMoveComplete);
            MacTransition tBarcodeReader_MovingToHomeClampedFromBarcodeReader = NewTransition(sBarcodeReader, sMovingToHomeClampedFromBarcodeReader, EnumMacSmMaskTransferTransition.CompleteRead);
            MacTransition tMovingToHomeClampedFromBarcodeReader_HomeClamped = NewTransition(sMovingToHomeClampedFromBarcodeReader, sHomeClamped, EnumMacSmMaskTransferTransition.CleanMoveComplete);


            //--- Clean Start
            MacTransition tReadyToRelease_CleanMovingStart = NewTransition(sReadyToRelease, sCleanMovingStart, EnumMacSmMaskTransferTransition.CleanStart);
            MacTransition tCleanMovingStart_CleanReady = NewTransition(sCleanMovingStart, sCleanReady, EnumMacSmMaskTransferTransition.CleanMoveComplete);
            MacTransition tCleanReady_CleanMovingReturn = NewTransition(sCleanReady, sCleanMovingReturn, EnumMacSmMaskTransferTransition.CleanProcessComplete);//觸發回Home點:ReadyToRelease應讓Recipe控制, 避免其它Device衝突
            MacTransition tCleanMovingReturn_ReadyToRelease = NewTransition(sCleanMovingReturn, sReadyToRelease, EnumMacSmMaskTransferTransition.CleanMoveComplete);
            MacTransition tCleanReady_CleanChMoving = NewTransition(sCleanReady, sCleanChMoving, EnumMacSmMaskTransferTransition.CleanReceiveMove);
            MacTransition tCleanChMoving_CleanChWaitAckMove = NewTransition(sCleanChMoving, sCleanChWaitAckMove, EnumMacSmMaskTransferTransition.CleanMoveComplete);
            MacTransition tCleanChWaitAckMove_CleanReady = NewTransition(sCleanChWaitAckMove, sCleanReady, EnumMacSmMaskTransferTransition.CleanMoveAck);


            //Complete or No Clean Job
            MacTransition tReadyToRelease_MovingToLoadPortForRelease = NewTransition(sReadyToRelease, sMovingToLoadPortForRelease, EnumMacSmMaskTransferTransition.NoCleanJob);
            MacTransition tReadyToRelease_MovingInspectionChForRelease = NewTransition(sReadyToRelease, sMovingInspectionChForRelease, EnumMacSmMaskTransferTransition.NoCleanJob);
            MacTransition tReadyToRelease_MovingInspectionChGlassForRelease = NewTransition(sReadyToRelease, sMovingInspectionChGlassForRelease, EnumMacSmMaskTransferTransition.NoCleanJob);
            MacTransition tReadyToRelease_MovingOpenStageForRelease = NewTransition(sReadyToRelease, sMovingOpenStageForRelease, EnumMacSmMaskTransferTransition.NoCleanJob);


            //Complete Move
            MacTransition tMovingToLoadPortForRelease_LoadPortReleasing = NewTransition(sMovingToLoadPortForRelease, sLoadPortReleasing, EnumMacSmMaskTransferTransition.CleanMoveComplete);
            MacTransition tMovingInspectionChForRelease_InspectionChReleasing = NewTransition(sMovingInspectionChForRelease, sInspectionChReleasing, EnumMacSmMaskTransferTransition.CleanMoveComplete);
            MacTransition tMovingInspectionChGlassForRelease_InspectionChGlassReleasing = NewTransition(sMovingInspectionChGlassForRelease, sInspectionChGlassReleasing, EnumMacSmMaskTransferTransition.CleanMoveComplete);
            MacTransition tMovingOpenStageForRelease_OpenStageReleasing = NewTransition(sMovingOpenStageForRelease, sOpenStageReleasing, EnumMacSmMaskTransferTransition.CleanMoveComplete);


            //Complete Clamp
            MacTransition tLoadPortReleasing_MovingToHomeFromLoadPort = NewTransition(sLoadPortReleasing, sMovingToHomeFromLoadPort, EnumMacSmMaskTransferTransition.CompleteReleased);
            MacTransition tInspectionChReleasing_MovingToHomeFromInspectionCh = NewTransition(sInspectionChReleasing, sMovingToHomeFromInspectionCh, EnumMacSmMaskTransferTransition.CompleteReleased);
            MacTransition tInspectionChGlassReleasing_MovingToHomeFromInspectionChGlass = NewTransition(sInspectionChGlassReleasing, sMovingToHomeFromInspectionChGlass, EnumMacSmMaskTransferTransition.CompleteReleased);
            MacTransition tOpenStageReleasing_MovingToHomeFromOpenStage = NewTransition(sOpenStageReleasing, sMovingToHomeFromOpenStage, EnumMacSmMaskTransferTransition.CompleteReleased);


            //Complete Move
            MacTransition tMovingToHomeFromLoadPort_WaitAckHome = NewTransition(sMovingToHomeFromLoadPort, sWaitAckHome, EnumMacSmMaskTransferTransition.CleanMoveComplete);
            MacTransition tMovingToHomeFromInspectionCh_WaitAckHome = NewTransition(sMovingToHomeFromInspectionCh, sWaitAckHome, EnumMacSmMaskTransferTransition.CleanMoveComplete);
            MacTransition tMovingToHomeFromInspectionChGlass_WaitAckHome = NewTransition(sMovingToHomeFromInspectionChGlass, sWaitAckHome, EnumMacSmMaskTransferTransition.CleanMoveComplete);
            MacTransition tMovingToHomeFromOpenStage_WaitAckHome = NewTransition(sMovingToHomeFromOpenStage, sWaitAckHome, EnumMacSmMaskTransferTransition.CleanMoveComplete);

            MacTransition tWaitAckHome_Home = NewTransition(sWaitAckHome, sHome, EnumMacSmMaskTransferTransition.ReceiveAckHome);

            //--- Exception Transition ---

        }





        #region State OnEntry



        private void sBarcodeReader_OnEntry(Object sender, MacStateEntryEventArgs e)
        {
        }

        private void sCleanChMoving_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        void sCleanMovingStart_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sCleanMovingReturn_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sCleanReady_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sCleanChWaitAckMove_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sDeviceInitial_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sHome_OnEntry(object sender, MacStateEntryEventArgs e)
        {

        }

        private void sHomeClamped_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sInspectionChCalibration_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sInspectionChCalibrationForRelease_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sInspectionChGlassCalibration_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sInspectionChGlassCalibrationForRelease_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sLoadPortCalibration_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sLoadPortCalibrationForRelease_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingInspectionChForRelease_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingInspectionChGlassForRelease_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingOpenStageForRelease_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingToBarcodeReader_OnEntry(Object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingToHomeClampedFromBarcodeReader_OnEntry(Object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingToHomeClampedFromInspectionCh_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingToHomeClampedFromInspectionChGlass_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingToHomeClampedFromLoadPort_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingToHomeClampedFromOpenStage_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingToHomeFromInspectionCh_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingToHomeFromInspectionChGlass_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingToHomeFromLoadPort_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingToHomeFromOpenStage_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingToInspectionCh_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingToInspectionChGlass_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingToLoadPort_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingToLoadPortForRelease_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sMovingToOpenStage_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sOpenStageCalibration_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sOpenStageCalibrationForRelease_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        void sReadyToRelease_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }
        private void sStart_OnEntry(object sender, MacStateEntryEventArgs e)
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
        private void sHome_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sHomeClamped_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sInspectionChCalibration_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sInspectionChCalibrationForRelease_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sInspectionChGlassCalibration_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sInspectionChGlassCalibrationForRelease_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sLoadPortACalibration_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sLoadPortACalibrationForRelease_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sLoadPortBCalibration_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sLoadPortBCalibrationForRelease_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingInspectionChForRelease_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingInspectionChGlassForRelease_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingOpenStageForRelease_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToBarcodeReader_OnExit(Object sender, MacStateExitEventArgs e) { }
        private void sMovingToHomeClampedFromBarcodeReader_OnExit(Object sender, MacStateExitEventArgs e) { }
        private void sMovingToHomeClampedFromInspectionCh_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToHomeClampedFromInspectionChGlas_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToHomeClampedFromLoadPortA_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToHomeClampedFromLoadPortB_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToHomeClampedFromOpenStage_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToHomeFromInspectionCh_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToHomeFromInspectionChGlass_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToHomeFromLoadPortA_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToHomeFromLoadPortB_OnExit(object sender, MacStateExitEventArgs e) { }
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