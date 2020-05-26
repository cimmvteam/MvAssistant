using MaskAutoCleaner.Msg;
using MaskAutoCleaner.Msg.PrescribedSecs;
using MaskCleanTool.StateMachine_v1_1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace MaskAutoCleaner.Machine.MaskTransfer
{

    /// <summary>
    /// MaskTransfer state machine
    /// </summary>
    public class MaskTransferAsblSm : MachineAsblSmBase
    {

        public void LoadCurrentState()
        {
            CurrentState = states[EnumMaskTransferState.Start.ToString()];
            this.SmTriggerByName(EnumMaskTransferTransition.PowerOn);
        }
        public void LoadStateMachine()
        {
            //--- Declare State ---

            State sStart = NewState(EnumMaskTransferState.Start);
            State sDeviceInitial = NewState(EnumMaskTransferState.Initial);
            State sHome = NewState(EnumMaskTransferState.Home);

            //To Target Clamp - Move
            State sMovingToLoadPort = NewState(EnumMaskTransferState.MovingToLoadPort);
            State sMovingToInspectionCh = NewState(EnumMaskTransferState.MovingToInspectionCh);
            State sMovingToInspectionChGlass = NewState(EnumMaskTransferState.MovingToInspectionChGlass);
            State sMovingToOpenStage = NewState(EnumMaskTransferState.MovingToOpenStage);
            //To Target Clamp - Calibration
            State sLoadPortClamping = NewState(EnumMaskTransferState.LoadPortClamping);
            State sInspectionChClamping = NewState(EnumMaskTransferState.InspectionChClamping);
            State sInspectionChGlassClamping = NewState(EnumMaskTransferState.InspectionChGlassClamping);
            State sOpenStageClamping = NewState(EnumMaskTransferState.OpenStageClamping);

            //Clamped Back - Move
            State sMovingToHomeClampedFromLoadPort = NewState(EnumMaskTransferState.MovingToHomeClampedFromLoadPort);
            State sMovingToHomeClampedFromInspectionCh = NewState(EnumMaskTransferState.MovingToHomeClampedFromInspectionCh);
            State sMovingToHomeClampedFromInspectionChGlass = NewState(EnumMaskTransferState.MovingToHomeClampedFromInspectionChGlass);
            State sMovingToHomeClampedFromOpenStage = NewState(EnumMaskTransferState.MovingToHomeClampedFromOpenStage);

            //Clamped Back - Home
            State sHomeClamped = NewState(EnumMaskTransferState.HomeClamped);
            State sReadyToRelease = NewState(EnumMaskTransferState.ReadyToRelease);

            //Barcode Reader
            State sMovingToBarcodeReader = NewState(EnumMaskTransferState.MovingToBarcodeReader.ToString());
            State sBarcodeReader = NewState(EnumMaskTransferState.BarcodeReader.ToString());
            State sMovingToHomeClampedFromBarcodeReader = NewState(EnumMaskTransferState.MovingToHomeClampedFromBarcodeReader);



            //Clean
            State sCleanMovingStart = NewState(EnumMaskTransferState.CleanMovingStart);//前往CleanCh
            State sCleanReady = NewState(EnumMaskTransferState.CleanReady);//準備好Clean
            State sCleanMovingReturn = NewState(EnumMaskTransferState.CleanMovingReturn);//離開CleanCh
            State sCleanChMoving = NewState(EnumMaskTransferState.CleanChMoving);
            State sCleanChWaitAckMove = NewState(EnumMaskTransferState.CleanChWaitAckMove);



            //To Target
            State sMovingToLoadPortForRelease = NewState(EnumMaskTransferState.MovingToLoadPortForRelease);
            State sMovingInspectionChForRelease = NewState(EnumMaskTransferState.MovingInspectionChForRelease);
            State sMovingInspectionChGlassForRelease = NewState(EnumMaskTransferState.MovingInspectionChGlassForRelease);
            State sMovingOpenStageForRelease = NewState(EnumMaskTransferState.MovingOpenStageForRelease);

            State sLoadPortReleasing = NewState(EnumMaskTransferState.LoadPortReleasing);
            State sInspectionChReleasing = NewState(EnumMaskTransferState.InspectionChReleasing);
            State sInspectionChGlassReleasing = NewState(EnumMaskTransferState.InspectionChGlassReleasing);
            State sOpenStageReleasing = NewState(EnumMaskTransferState.OpenStageReleasing);


            State sMovingToHomeFromLoadPort = NewState(EnumMaskTransferState.MovingToHomeFromLoadPort.ToString());
            State sMovingToHomeFromInspectionCh = NewState(EnumMaskTransferState.MovingToHomeFromInspectionCh);
            State sMovingToHomeFromInspectionChGlass = NewState(EnumMaskTransferState.MovingToHomeFromInspectionChGlass);
            State sMovingToHomeFromOpenStage = NewState(EnumMaskTransferState.MovingToHomeFromOpenStage);

            State sWaitAckHome = NewState(EnumMaskTransferState.WaitAckHome.ToString());



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



            //W934 mht for state process three seconds function
            foreach (KeyValuePair<string, State> everyState in this.states)
            {
                if (!everyState.Value.GetType().ToString().Contains("Exception"))
                    everyState.Value.OnEntry += sOnEvtWaitThreeSec;
            }
            #endregion State Register OnEntry OnExit

            //--- Transition ---

            Transition tStart_DeviceInitial = NewTransition(sStart, sDeviceInitial, Guard_Start_DeviceInitial, Action_Start_DeviceInitial, EnumMaskTransferTransition.PowerOn.ToString());
            Transition tDeviceInitial_Home = NewTransition(sDeviceInitial, sHome, Guard_DeviceInitial_Home, Action_DeviceInitial_Home, EnumMaskTransferTransition.CompleteInitial.ToString());

            //Receive Transfer From Home
            Transition tHome_MovingToLoadPort = NewTransition(sHome, sMovingToLoadPort, Guard_Home_MovingToLoadPort, ActionHome_MovingToLoadPort, EnumMaskTransferTransition.ReceiveTransferMask.ToString());
            Transition tHome_MovingToInspectionCh = NewTransition(sHome, sMovingToInspectionCh, Guard_Home_MovingToInspectionCh, ActionHome_MovingToInspectionCh, EnumMaskTransferTransition.ReceiveTransferMask.ToString());
            Transition tHome_MovingToInspectionChGlass = NewTransition(sHome, sMovingToInspectionChGlass, Guard_Home_MovingToInspectionChGlass, ActionHome_MovingToInspectionChGlass, EnumMaskTransferTransition.ReceiveTransferMask.ToString());
            Transition tHome_MovingToOpenStage = NewTransition(sHome, sMovingToOpenStage, Guard_Home_MovingToOpenStage, ActionHome_MovingToOpenStage, EnumMaskTransferTransition.ReceiveTransferMask.ToString());

            //Complete Move
            Transition tMovingToLoadPort_LoadPortClamping = NewTransition(sMovingToLoadPort, sLoadPortClamping, Guard_MovingToLoadPort_LoadPortClamping, Action_MovingToLoadPort_LoadPortClamping, EnumMaskTransferTransition.CleanMoveComplete.ToString());
            Transition tMovingToInspectionCh_InspectionChClamping = NewTransition(sMovingToInspectionCh, sInspectionChClamping, Guard_MovingToInspectionCh_InspectionChClamping, Action_MovingToInspectionCh_InspectionChClamping, EnumMaskTransferTransition.CleanMoveComplete.ToString());
            Transition tMovingToInspectionChGlass_InspectionChGlassClamping = NewTransition(sMovingToInspectionChGlass, sInspectionChGlassClamping, Guard_MovingToInspectionChGlass_InspectionChGlassClamping, Action_MovingToInspectionChGlass_InspectionChGlassClamping, EnumMaskTransferTransition.CleanMoveComplete.ToString());
            Transition tMovingToOpenStage_OpenStageClamping = NewTransition(sMovingToOpenStage, sOpenStageClamping, Guard_MovingToOpenStage_OpenStageClamping, Action_MovingToOpenStage_OpenStageClamping, EnumMaskTransferTransition.CleanMoveComplete.ToString());

            //Compelte Clamped -> 準備移回Home_Clamped
            Transition tLoadPortClamping_MovingToHomeClampedFromLoadPort = NewTransition(sLoadPortClamping, sMovingToHomeClampedFromLoadPort, Guard_LoadPortClamping_MovingToHomeClampedFromLoadPort, Action_LoadPortClamping_MovingToHomeClampedFromLoadPort, EnumMaskTransferTransition.CompleteClamped.ToString());
            Transition tInspectionChClamping_MovingToHomeClampedFromInspectionCh = NewTransition(sInspectionChClamping, sMovingToHomeClampedFromInspectionCh, Guard_InspectionChClamping_MovingToHomeClampedFromInspectionCh, Action_InspectionChClamping_MovingToHomeClampedFromInspectionCh, EnumMaskTransferTransition.CompleteClamped.ToString());
            Transition tInspectionChGlassClamping_MovingToHomeClampedFromInspectionChGlass = NewTransition(sInspectionChGlassClamping, sMovingToHomeClampedFromInspectionChGlass, Guard_InspectionChGlassClamping_MovingToHomeClampedFromInspectionChGlass, Action_InspectionChGlassClamping_MovingToHomeClampedFromInspectionChGlass, EnumMaskTransferTransition.CompleteClamped.ToString());
            Transition tOpenStageClamping_MovingToHomeClampedFromOpenStage = NewTransition(sOpenStageClamping, sMovingToHomeClampedFromOpenStage, Guard_OpenStageClamping_MovingToHomeClampedFromOpenStage, Action_OpenStageClamping_MovingToHomeClampedFromOpenStage, EnumMaskTransferTransition.CompleteClamped.ToString());

            //Complete Move
            Transition tMovingToHomeClampedFromLoadPortA_HomeClamped = NewTransition(sMovingToHomeClampedFromLoadPort, sHomeClamped, Guard_MovingToHomeClampedFromLoadPort_HomeClamped, ActionMovingToHomeClampedFromLoadPort_HomeClamped, EnumMaskTransferTransition.CleanMoveComplete.ToString());
            Transition tMovingToHomeClampedFromInspectionCh_HomeClamped = NewTransition(sMovingToHomeClampedFromInspectionCh, sHomeClamped, Guard_MovingToHomeClampedFromInspectionCh_HomeClamped, ActionMovingToHomeClampedFromInspectionCh_HomeClamped, EnumMaskTransferTransition.CleanMoveComplete.ToString());
            Transition tMovingToHomeClampedFromInspectionChGlass_HomeClamped = NewTransition(sMovingToHomeClampedFromInspectionChGlass, sHomeClamped, Guard_MovingToHomeClampedFromInspectionChGlass_HomeClamped, ActionMovingToHomeClampedFromInspectionChGlass_HomeClamped, EnumMaskTransferTransition.CleanMoveComplete.ToString());
            Transition tMovingToHomeClampedFromOpenStage_HomeClamped = NewTransition(sMovingToHomeClampedFromOpenStage, sHomeClamped, Guard_MovingToHomeClampedFromOpenStage_HomeClamped, ActionMovingToHomeClampedFromOpenStage_HomeClamped, EnumMaskTransferTransition.CleanMoveComplete.ToString());


            //Is Ready to Release
            Transition tHomeClamped_ReadyToRelease = NewTransition(sHomeClamped, sReadyToRelease, Guard_HomeClamped_ReadyToRelease, Action_HomeClamped_ReadyToRelease, EnumMaskTransferTransition.IsReady.ToString());
            Transition tHomeClamped_MovingToBarcodeReader = NewTransition(sHomeClamped, sMovingToBarcodeReader, Guard_HomeClamped_MovingToBarcodeReader, Action_HomeClamped_MovingToBarcodeReader, EnumMaskTransferTransition.IsReady.ToString());

            //Barcode Reader
            Transition tMovingToBarcodeReader_BarcodeReader = NewTransition(sMovingToBarcodeReader, sBarcodeReader, Guard_MovingToBarcodeReader_BarcodeReader, Action_MovingToBarcodeReader_BarcodeReader, EnumMaskTransferTransition.CleanMoveComplete.ToString());
            Transition tBarcodeReader_MovingToHomeClampedFromBarcodeReader = NewTransition(sBarcodeReader, sMovingToHomeClampedFromBarcodeReader, Guard_BarcodeReader_MovingToHomeClampedFromBarcodeReader, Action_BarcodeReader_MovingToHomeClampedFromBarcodeReader, EnumMaskTransferTransition.CompleteRead.ToString());
            Transition tMovingToHomeClampedFromBarcodeReader_HomeClamped = NewTransition(sMovingToHomeClampedFromBarcodeReader, sHomeClamped, Guard_MovingToHomeClampedFromBarcodeReader_HomeClamped, Action_MovingToHomeClampedFromBarcodeReader_HomeClamped, EnumMaskTransferTransition.CleanMoveComplete.ToString());


            //--- Clean Start
            Transition tReadyToRelease_CleanMovingStart = NewTransition(sReadyToRelease, sCleanMovingStart, Guard_ReadyToRelease_CleanMovingStart, Action_ReadyToRelease_CleanMovingStart, EnumMaskTransferTransition.CleanStart.ToString());
            Transition tCleanMovingStart_CleanReady = NewTransition(sCleanMovingStart, sCleanReady, Guard_CleanMovingStart_CleanReady, Action_CleanMovingStart_CleanReady, EnumMaskTransferTransition.CleanMoveComplete.ToString());
            Transition tCleanReady_CleanMovingReturn = NewTransition(sCleanReady, sCleanMovingReturn, Guard_CleanReady_CleanMovingReturn, Action_CleanReady_CleanMovingReturn, EnumMaskTransferTransition.CleanProcessComplete.ToString());//觸發回Home點:ReadyToRelease應讓Recipe控制, 避免其它Device衝突
            Transition tCleanMovingReturn_ReadyToRelease = NewTransition(sCleanMovingReturn, sReadyToRelease, Guard_CleanMovingReturn_ReadyToRelease, Action_CleanMovingReturn_ReadyToRelease, EnumMaskTransferTransition.CleanMoveComplete.ToString());
            Transition tCleanReady_CleanChMoving = NewTransition(sCleanReady, sCleanChMoving, Guard_CleanReady_CleanChMoving, Action_CleanReady_CleanChMoving, EnumMaskTransferTransition.CleanReceiveMove.ToString());
            Transition tCleanChMoving_CleanChWaitAckMove = NewTransition(sCleanChMoving, sCleanChWaitAckMove, Guard_CleanChMoving_CleanChWaitAckMove, Action_CleanChMoving_CleanChWaitAckMove, EnumMaskTransferTransition.CleanMoveComplete.ToString());
            Transition tCleanChWaitAckMove_CleanReady = NewTransition(sCleanChWaitAckMove, sCleanReady, Guard_CleanChWaitAckMove_CleanReady, ActionCleanChWaitAckMove_CleanReady, EnumMaskTransferTransition.CleanMoveAck.ToString());


            //Complete or No Clean Job
            Transition tReadyToRelease_MovingToLoadPortForRelease = NewTransition(sReadyToRelease, sMovingToLoadPortForRelease, Guard_ReadyToRelease_MovingToLoadPortForRelease, Action_ReadyToRelease_MovingToLoadPortForRelease, EnumMaskTransferTransition.NoCleanJob.ToString());
            Transition tReadyToRelease_MovingInspectionChForRelease = NewTransition(sReadyToRelease, sMovingInspectionChForRelease, Guard_ReadyToRelease_MovingInspectionChForRelease, Action_ReadyToRelease_MovingInspectionChForRelease, EnumMaskTransferTransition.NoCleanJob.ToString());
            Transition tReadyToRelease_MovingInspectionChGlassForRelease = NewTransition(sReadyToRelease, sMovingInspectionChGlassForRelease, Guard_ReadyToRelease_MovingInspectionChGlassForRelease, Action_ReadyToRelease_MovingInspectionChGlassForRelease, EnumMaskTransferTransition.NoCleanJob.ToString());
            Transition tReadyToRelease_MovingOpenStageForRelease = NewTransition(sReadyToRelease, sMovingOpenStageForRelease, Guard_ReadyToRelease_MovingOpenStageForRelease, Action_ReadyToRelease_MovingOpenStageForRelease, EnumMaskTransferTransition.NoCleanJob.ToString());


            //Complete Move
            Transition tMovingToLoadPortForRelease_LoadPortReleasing = NewTransition(sMovingToLoadPortForRelease, sLoadPortReleasing, GuardMovingToLoadPortForRelease_LoadPortReleasing, ActionMovingToLoadPortForRelease_LoadPortReleasing, EnumMaskTransferTransition.CleanMoveComplete.ToString());
            Transition tMovingInspectionChForRelease_InspectionChReleasing = NewTransition(sMovingInspectionChForRelease, sInspectionChReleasing, Guard_MovingInspectionChForRelease_InspectionChReleasing, ActionMovingInspectionChForRelease_InspectionChReleasing, EnumMaskTransferTransition.CleanMoveComplete.ToString());
            Transition tMovingInspectionChGlassForRelease_InspectionChGlassReleasing = NewTransition(sMovingInspectionChGlassForRelease, sInspectionChGlassReleasing, Guard_MovingInspectionChGlassForRelease_InspectionChGlassReleasing, Action_MovingInspectionChGlassForRelease_InspectionChGlassReleasing, EnumMaskTransferTransition.CleanMoveComplete.ToString());
            Transition tMovingOpenStageForRelease_OpenStageReleasing = NewTransition(sMovingOpenStageForRelease, sOpenStageReleasing, Guard_MovingOpenStageForRelease_OpenStageReleasing, Action_MovingOpenStage_OpenStageReleasing, EnumMaskTransferTransition.CleanMoveComplete.ToString());


            //Complete Clamp
            Transition tLoadPortReleasing_MovingToHomeFromLoadPort = NewTransition(sLoadPortReleasing, sMovingToHomeFromLoadPort, Guard_LoadPortReleasing_MovingToHomeFromLoadPort, Action_LoadPortReleasing_MovingToHomeFromLoadPort, EnumMaskTransferTransition.CompleteReleased.ToString());
            Transition tInspectionChReleasing_MovingToHomeFromInspectionCh = NewTransition(sInspectionChReleasing, sMovingToHomeFromInspectionCh, Guard_InspectionChReleasing_MovingToHomeFromInspectionCh, Action_InspectionChReleasing_MovingToHomeFromInspectionCh, EnumMaskTransferTransition.CompleteReleased.ToString());
            Transition tInspectionChGlassReleasing_MovingToHomeFromInspectionChGlass = NewTransition(sInspectionChGlassReleasing, sMovingToHomeFromInspectionChGlass, Guard_InspectionChGlassReleasing_MovingToHomeFromInspectionChGlass, Action_InspectionChGlassReleasing_MovingToHomeFromInspectionChGlass, EnumMaskTransferTransition.CompleteReleased.ToString());
            Transition tOpenStageReleasing_MovingToHomeFromOpenStage = NewTransition(sOpenStageReleasing, sMovingToHomeFromOpenStage, Guard_OpenStageReleasing_MovingToHomeFromOpenStage, Action_OpenStageReleasing_MovingToHomeFromOpenStage, EnumMaskTransferTransition.CompleteReleased.ToString());


            //Complete Move
            Transition tMovingToHomeFromLoadPort_WaitAckHome = NewTransition(sMovingToHomeFromLoadPort, sWaitAckHome, Guard_MovingToHomeFromLoadPort_WaitAckHome, ActionMovingToHomeFromLoadPort_WaitAckHome, EnumMaskTransferTransition.CleanMoveComplete.ToString());
            Transition tMovingToHomeFromInspectionCh_WaitAckHome = NewTransition(sMovingToHomeFromInspectionCh, sWaitAckHome, Guard_MovingToHomeFromInspectionCh_WaitAckHome, ActionMovingToHomeFromInspectionCh_WaitAckHome, EnumMaskTransferTransition.CleanMoveComplete.ToString());
            Transition tMovingToHomeFromInspectionChGlass_WaitAckHome = NewTransition(sMovingToHomeFromInspectionChGlass, sWaitAckHome, Guard_MovingToHomeFromInspectionChGlass_WaitAckHome, ActionMovingToHomeFromInspectionChGlass_WaitAckHome, EnumMaskTransferTransition.CleanMoveComplete.ToString());
            Transition tMovingToHomeFromOpenStage_WaitAckHome = NewTransition(sMovingToHomeFromOpenStage, sWaitAckHome, Guard_MovingToHomeFromOpenStage_WaitAckHome, ActionMovingToHomeFromOpenStage_WaitAckHome, EnumMaskTransferTransition.CleanMoveComplete.ToString());

            Transition tWaitAckHome_Home = NewTransition(sWaitAckHome, sHome, Guard_WaitAckHome_Home, Action_WaitAckHome_Home, EnumMaskTransferTransition.ReceiveAckHome.ToString());

            //--- Exception Transition ---

        }








        #region State OnEntry



        private void sBarcodeReader_OnEntry(Object sender, StateEntryEventArgs e)
        {
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {

                while (string.IsNullOrEmpty(this.ActiveMask.MaskBarCode))
                {
                    var img = this.halMaskTransfer.CameraBarcodeReader.Shot();

                    //TODO: image process to read barcode
                    if (img != null)
                    {
                        var fakeBarcode = this.halMaskTransfer.CameraBarcodeReader as MaskAutoCleaner.Hal.ImpFake.Component.Camera.HalCameraFake; //so trash
                        if (fakeBarcode != null)
                        {
                            this.ActiveMask.MaskBarCode = "So fag";
                        }
                        if (this.ActiveMask.MaskBarCode == null)
                        {
                            throw new MacExcpetion("Mask transfer BarcodeReader fail");
                        }
                    }


                    SpinWait.SpinUntil(() => string.IsNullOrEmpty(this.ActiveMask.MaskBarCode) == false, 100);
                }

                this.SmTriggerToNext();//Next Trigger
                return true;
            }));
            this._stateJobHdl.RunInStateEntry();
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }

        private void sCleanChMoving_OnEntry(object sender, StateEntryEventArgs e)
        {
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                this.ActiveMask.MaskMoveState = Mask.EnumMaskMoveState.RobotMoving;
                this.BroadcastJobNotify(EnumJobNotify.MT_CleanMove);

                //TODO: 移動Robot
                this.SmTriggerToNext();
                return true;
            }));
            this._stateJobHdl.RunInStateEntry();
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }

        void sCleanMovingStart_OnEntry(object sender, StateEntryEventArgs e)
        {
            this.SmTriggerByName(EnumMaskTransferTransition.CleanMoveComplete);
        }

        private void sCleanMovingReturn_OnEntry(object sender, StateEntryEventArgs e)
        {
            this.BroadcastJobNotify(EnumJobNotify.MT_CleanEnd);
            this.SmTriggerByName(EnumMaskTransferTransition.CleanMoveComplete);
        }

        private void sCleanReady_OnEntry(object sender, StateEntryEventArgs e)
        {
            MvSpinWait.SpinUntil(() => this.ActiveMask.MaskMoveState == Mask.EnumMaskMoveState.WaitRobot || this.ActiveMask.MaskMoveState == Mask.EnumMaskMoveState.CompleteClean);

            if (this.ActiveMask.MaskMoveState != Mask.EnumMaskMoveState.CompleteClean)
                this.SmTriggerToNext(EnumMaskTransferTransition.CleanReceiveMove);
        }

        private void sCleanChWaitAckMove_OnEntry(object sender, StateEntryEventArgs e)
        {
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                this.ActiveMask.MaskMoveState = Mask.EnumMaskMoveState.FinishMove;
                this.BroadcastJobNotify(EnumJobNotify.MT_CleanCompleteMove);

                MvSpinWait.SpinUntil(() => this.ActiveMask.MaskMoveState != Mask.EnumMaskMoveState.FinishMove);
                this.SmTriggerToNext();
                return true;
            }));
            this._stateJobHdl.RunInStateEntry();
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }

        private void sDeviceInitial_OnEntry(object sender, StateEntryEventArgs e)
        {
            var di = new DirectoryInfo(this.MachineMediater.MachineCfg.RecipePathMaskTransfer);
            foreach (var fi in di.GetFiles())
                this.DeviceCmdDict[fi.Name] = DeviceCmdSet.LoadXml(fi.FullName);


            this.ConnecRcs();

            var robot = this.halMaskTransfer.Robot;
            {
                robot.HalStopProgram();
                var status = robot.HalReset();
                status = robot.HalStartProgram("PNS0101");
                if (status != 0) throw new MacExcpetion("MT Robot 無法連線");
            }



            {
                var gripper01 = this.halMaskTransfer.Gripper01;
                var gripper02 = this.halMaskTransfer.Gripper02;



                //等待完成連線
                while (!gripper01.HalIsConnected()) Thread.Sleep(1000);
                while (!gripper02.HalIsConnected()) Thread.Sleep(1000);





                //TODO: 這段移動要移除
                // 之後有光學尺和觸覺感測, 要判斷位置及壓力, 避免光罩掉落
                //移動 -100 count
                var cmd = new HalGripperCmd()
                {
                    Enable = true,
                    Direction = HalEnumGripperDirection.Counterclockwise,
                    Offset = -100,
                    SpeedLevel = 1,
                };
                gripper01.HalMove(cmd);
                gripper02.HalMove(cmd);

                //等待完成移動
                SpinWait.SpinUntil(() => this.halMaskTransfer.Gripper01.HalIsCompleted(), 10 * 1000);
                SpinWait.SpinUntil(() => this.halMaskTransfer.Gripper02.HalIsCompleted(), 10 * 1000);

                //零點重置
                gripper01.HalZeroReset();
                gripper02.HalZeroReset();


            }



            this.SmTriggerToSingleNext();
        }

        private void sHome_OnEntry(object sender, StateEntryEventArgs e)
        {

        }

        private void sHomeClamped_OnEntry(object sender, StateEntryEventArgs e)
        {
            this.SmTriggerToNext(EnumMaskTransferTransition.IsReady);
        }

        private void sInspectionChCalibration_OnEntry(object sender, StateEntryEventArgs e)
        {
            this.SmTriggerToNext();//Next Trigger
        }

        private void sInspectionChCalibrationForRelease_OnEntry(object sender, StateEntryEventArgs e)
        {
            this.SmTriggerToNext();//Next Trigger
        }

        private void sInspectionChGlassCalibration_OnEntry(object sender, StateEntryEventArgs e)
        {
            this.SmTriggerToNext();//Next Trigger
        }

        private void sInspectionChGlassCalibrationForRelease_OnEntry(object sender, StateEntryEventArgs e)
        {
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                this.SmTriggerToNext();//Next Trigger
                return true;
            }));
            this._stateJobHdl.RunInStateEntry();
            this._stateJobHdl.Clear();//清除自己的工作&訂閱

        }

        private void sLoadPortCalibration_OnEntry(object sender, StateEntryEventArgs e)
        {
            this.SmTriggerToNext();//Next Trigger
        }

        private void sLoadPortCalibrationForRelease_OnEntry(object sender, StateEntryEventArgs e)
        {
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                this.SmTriggerToNext();//Next Trigger
                return true;
            }));
            this._stateJobHdl.RunInStateEntry();
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }

        private void sMovingInspectionChForRelease_OnEntry(object sender, StateEntryEventArgs e)
        {
            //--- Initial ---
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)


            //--- Attach Pre Jobs/Check ---
            this._stateJobHdl.PreJobs.Add(new StateJobBasic((sjh) => { return true; }));


            //--- Attach Major Jobs ---
            this._stateJobHdl.MajorJobs.AddRange(this.AttachStateJobFromDeviceCmd(EnumMaskTransferCmdSet.HomeTo_InspectionCh01));
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                this.ReportSecsCeid(EnumCeid.S6F11_MT_RobotMoveToInspectionCh, S6F11.Create());
                this.SmTriggerToNext();//Next Trigger
                return true;
            }));


            //--- Execute Entry ---
            this._stateJobHdl.RunInStateEntry();


            //--- Final ---
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }

        private void sMovingInspectionChGlassForRelease_OnEntry(object sender, StateEntryEventArgs e)
        {
            //--- Initial ---
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)


            //--- Attach Pre Jobs/Check ---
            this._stateJobHdl.PreJobs.Add(new StateJobBasic((sjh) => { return true; }));


            //--- Attach Major Jobs ---
            this._stateJobHdl.MajorJobs.AddRange(this.AttachStateJobFromDeviceCmd(EnumMaskTransferCmdSet.HomeTo_InspectionCh01Glass));
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                this.ReportSecsCeid(EnumCeid.S6F11_MT_RobotMoveToInspectionChGlass, S6F11.Create());
                this.SmTriggerToNext();//Next Trigger
                return true;
            }));


            //--- Execute Entry ---
            this._stateJobHdl.RunInStateEntry();


            //--- Final ---
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }

        private void sMovingOpenStageForRelease_OnEntry(object sender, StateEntryEventArgs e)
        {
            this.SmTriggerToNext();//Next Trigger
        }

        private void sMovingToBarcodeReader_OnEntry(Object sender, StateEntryEventArgs e)
        {
            //--- Initial ---
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)


            //--- Attach Pre Jobs/Check ---
            this._stateJobHdl.PreJobs.Add(new StateJobBasic((sjh) => { return true; }));


            //--- Attach Major Jobs ---
            this._stateJobHdl.MajorJobs.AddRange(this.AttachStateJobFromDeviceCmd(EnumMaskTransferCmdSet.HomeTo_BarcodeReader01));
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                this.ReportSecsCeid(EnumCeid.S6F11_MT_RobotMoveToBarcodeReader, S6F11.Create());
                this.SmTriggerToNext();//Next Trigger
                return true;
            }));


            //--- Execute Entry ---
            this._stateJobHdl.RunInStateEntry();


            //--- Final ---
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }

        private void sMovingToHomeClampedFromBarcodeReader_OnEntry(Object sender, StateEntryEventArgs e)
        {
            //--- Initial ---
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)


            //--- Attach Pre Jobs/Check ---
            this._stateJobHdl.PreJobs.Add(new StateJobBasic((sjh) => { return true; }));


            //--- Attach Major Jobs ---
            this._stateJobHdl.MajorJobs.AddRange(this.AttachStateJobFromDeviceCmd(EnumMaskTransferCmdSet.ToHome_BarcodeReader01));
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                this.ReportSecsCeid(EnumCeid.S6F11_MT_RobotMoveToHome, S6F11.Create());
                this.SmTriggerToNext();//Next Trigger
                return true;
            }));


            //--- Execute Entry ---
            this._stateJobHdl.RunInStateEntry();


            //--- Final ---
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }

        private void sMovingToHomeClampedFromInspectionCh_OnEntry(object sender, StateEntryEventArgs e)
        {
            //--- Initial ---
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)


            //--- Attach Pre Jobs/Check ---
            this._stateJobHdl.PreJobs.Add(new StateJobBasic((sjh) => { return true; }));


            //--- Attach Major Jobs ---
            this._stateJobHdl.MajorJobs.AddRange(this.AttachStateJobFromDeviceCmd(EnumMaskTransferCmdSet.ToHome_InspectionCh01));
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                this.ReportSecsCeid(EnumCeid.S6F11_MT_RobotMoveToHome, S6F11.Create());
                this.SmTriggerToNext();//Next Trigger
                return true;
            }));


            //--- Execute Entry ---
            this._stateJobHdl.RunInStateEntry();


            //--- Final ---
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }

        private void sMovingToHomeClampedFromInspectionChGlass_OnEntry(object sender, StateEntryEventArgs e)
        {
            //--- Initial ---
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)


            //--- Attach Pre Jobs/Check ---
            this._stateJobHdl.PreJobs.Add(new StateJobBasic((sjh) => { return true; }));


            //--- Attach Major Jobs ---
            this._stateJobHdl.MajorJobs.AddRange(this.AttachStateJobFromDeviceCmd(EnumMaskTransferCmdSet.ToHome_InspectionCh01Glass));
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                this.ReportSecsCeid(EnumCeid.S6F11_MT_RobotMoveToHome, S6F11.Create());
                this.SmTriggerToNext();//Next Trigger
                return true;
            }));


            //--- Execute Entry ---
            this._stateJobHdl.RunInStateEntry();


            //--- Final ---
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }

        private void sMovingToHomeClampedFromLoadPort_OnEntry(object sender, StateEntryEventArgs e)
        {
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)


            //--- Attach Pre Jobs/Check ---
            this._stateJobHdl.PreJobs.Add(new StateJobBasic((sjh) => { return true; }));


            //--- Attach Major Jobs ---
            var positionId = this.MachineMediater.GetPositionId(this.ActiveMask.LocCurrent);
            var cmdset = EnumMaskTransferCmdSet.None;
            switch (positionId)
            {
                case EnumPositionId.LoadPort01: cmdset = EnumMaskTransferCmdSet.ToHome_LoadPort01; break;
                case EnumPositionId.LoadPort02: cmdset = EnumMaskTransferCmdSet.ToHome_LoadPort02; break;
            }

            this._stateJobHdl.MajorJobs.AddRange(this.AttachStateJobFromDeviceCmd(cmdset));
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                this.ReportSecsCeid(EnumCeid.S6F11_MT_RobotMoveToHome, S6F11.Create());
                this.SmTriggerToNext();//Next Trigger
                return true;
            }));


            //--- Execute Entry ---
            this._stateJobHdl.RunInStateEntry();


            //--- Final ---
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }

        private void sMovingToHomeClampedFromOpenStage_OnEntry(object sender, StateEntryEventArgs e)
        {
            this.SmTriggerToNext();//Next Trigger
        }

        private void sMovingToHomeFromInspectionCh_OnEntry(object sender, StateEntryEventArgs e)
        {
            //--- Initial ---
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)


            //--- Attach Pre Jobs/Check ---
            this._stateJobHdl.PreJobs.Add(new StateJobBasic((sjh) => { return true; }));


            //--- Attach Major Jobs ---
            this._stateJobHdl.MajorJobs.AddRange(this.AttachStateJobFromDeviceCmd(EnumMaskTransferCmdSet.ToHome_InspectionCh01));
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                this.ReportSecsCeid(EnumCeid.S6F11_MT_RobotMoveToHome, S6F11.Create());
                this.SmTriggerToNext();//Next Trigger
                return true;
            }));


            //--- Execute Entry ---
            this._stateJobHdl.RunInStateEntry();


            //--- Final ---
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }

        private void sMovingToHomeFromInspectionChGlass_OnEntry(object sender, StateEntryEventArgs e)
        {
            //--- Initial ---
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)


            //--- Attach Pre Jobs/Check ---
            this._stateJobHdl.PreJobs.Add(new StateJobBasic((sjh) => { return true; }));


            //--- Attach Major Jobs ---
            this._stateJobHdl.MajorJobs.AddRange(this.AttachStateJobFromDeviceCmd(EnumMaskTransferCmdSet.ToHome_InspectionCh01Glass));
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                this.ReportSecsCeid(EnumCeid.S6F11_MT_RobotMoveToHome, S6F11.Create());
                this.SmTriggerToNext();//Next Trigger
                return true;
            }));


            //--- Execute Entry ---
            this._stateJobHdl.RunInStateEntry();


            //--- Final ---
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }

        private void sMovingToHomeFromLoadPort_OnEntry(object sender, StateEntryEventArgs e)
        {
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)


            //--- Attach Pre Jobs/Check ---
            this._stateJobHdl.PreJobs.Add(new StateJobBasic((sjh) => { return true; }));


            //--- Attach Major Jobs ---
            var positionId = this.MachineMediater.GetPositionId(this.ActiveMask.LocCurrent);
            var cmdset = EnumMaskTransferCmdSet.None;
            switch (positionId)
            {
                case EnumPositionId.LoadPort01: cmdset = EnumMaskTransferCmdSet.ToHome_LoadPort01; break;
                case EnumPositionId.LoadPort02: cmdset = EnumMaskTransferCmdSet.ToHome_LoadPort02; break;
            }

            this._stateJobHdl.MajorJobs.AddRange(this.AttachStateJobFromDeviceCmd(cmdset));
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                this.ReportSecsCeid(EnumCeid.S6F11_MT_RobotMoveToHome, S6F11.Create());
                this.SmTriggerToNext();//Next Trigger
                return true;
            }));


            //--- Execute Entry ---
            this._stateJobHdl.RunInStateEntry();


            //--- Final ---
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }

        private void sMovingToHomeFromOpenStage_OnEntry(object sender, StateEntryEventArgs e)
        {
            this.SmTriggerToNext();//Next Trigger
        }

        private void sMovingToInspectionCh_OnEntry(object sender, StateEntryEventArgs e)
        {
            //--- Initial ---
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)



            //--- Attach Pre Jobs/Check ---
            this._stateJobHdl.PreJobs.Add(new StateJobBasic((sjh) => { return true; }));


            //--- Attach Major Jobs ---
            this._stateJobHdl.MajorJobs.AddRange(this.AttachStateJobFromDeviceCmd(EnumMaskTransferCmdSet.HomeTo_InspectionCh01));
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                //完成移動就Report
                this.ReportSecsCeid(EnumCeid.S6F11_MT_RobotMoveToInspectionCh, S6F11.Create());
                this.SmTriggerToNext();//Next Trigger
                return true;
            }));


            //--- Execute Entry ---
            this._stateJobHdl.RunInStateEntry();


            //--- Final ---
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }

        private void sMovingToInspectionChGlass_OnEntry(object sender, StateEntryEventArgs e)
        {
            //--- Initial ---
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)


            //--- Attach Pre Jobs/Check ---
            this._stateJobHdl.PreJobs.Add(new StateJobBasic((sjh) => { return true; }));


            //--- Attach Major Jobs ---
            this._stateJobHdl.MajorJobs.AddRange(this.AttachStateJobFromDeviceCmd(EnumMaskTransferCmdSet.HomeTo_InspectionCh01Glass));
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                //完成移動就Report
                this.ReportSecsCeid(EnumCeid.S6F11_MT_RobotMoveToInspectionChGlass, S6F11.Create());
                this.SmTriggerToNext();//Next Trigger
                return true;
            }));


            //--- Execute Entry ---
            this._stateJobHdl.RunInStateEntry();


            //--- Final ---
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }

        private void sMovingToLoadPort_OnEntry(object sender, StateEntryEventArgs e)
        {
            //--- Initial ---
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)


            // StateJobHandler & Subscribe Topic
            this._stateJobHdl.Subscribe(EnumMqttTopicId.MT_Tactile1_Raw, (info) => { this.ReportAlarmIfOoc(this.spec.TactilePressureInReleased, info.NewSignal, EnumAlarmId.MT_TactileOverSpec1); });
            this._stateJobHdl.Subscribe(EnumMqttTopicId.MT_Tactile2_Raw, (info) => { this.ReportAlarmIfOoc(this.spec.TactilePressureInReleased, info.NewSignal, EnumAlarmId.MT_TactileOverSpec2); });
            this._stateJobHdl.Subscribe(EnumMqttTopicId.MT_Tactile3_Raw, (info) => { this.ReportAlarmIfOoc(this.spec.TactilePressureInReleased, info.NewSignal, EnumAlarmId.MT_TactileOverSpec3); });
            this._stateJobHdl.Subscribe(EnumMqttTopicId.MT_Tactile4_Raw, (info) => { this.ReportAlarmIfOoc(this.spec.TactilePressureInReleased, info.NewSignal, EnumAlarmId.MT_TactileOverSpec4); });
            this._stateJobHdl.Subscribe(EnumMqttTopicId.MT_Tactile1_Avg, (info) => { this.ReportAlarmIfOoc(this.spec.RobotForceInReleased, info.NewSignal, EnumAlarmId.MT_RobotForceOverSpec); });



            //--- Attach Pre Jobs/Check ---
            this._stateJobHdl.PreJobs.Add(new StateJobBasic((sjh) => { return true; }));


            //--- Attach Major Jobs ---
            var positionId = this.MachineMediater.GetPositionId(this.ActiveMask.LocCurrent);
            var cmdset = EnumMaskTransferCmdSet.None;
            switch (positionId)
            {
                case EnumPositionId.LoadPort01: cmdset = EnumMaskTransferCmdSet.HomeTo_LoadPort01; break;
                case EnumPositionId.LoadPort02: cmdset = EnumMaskTransferCmdSet.HomeTo_LoadPort02; break;
            }

            this._stateJobHdl.MajorJobs.AddRange(this.AttachStateJobFromDeviceCmd(cmdset));
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                this.ReportSecsCeid(EnumCeid.S6F11_MT_RobotMoveToLoadPort, S6F11.Create());
                this.SmTriggerToNext();//Next Trigger
                return true;
            }));


            //--- Execute Entry ---
            this._stateJobHdl.RunInStateEntry();


            //--- Final ---
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }

        private void sMovingToLoadPortForRelease_OnEntry(object sender, StateEntryEventArgs e)
        {
            //--- Initial ---
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)


            // StateJobHandler & Subscribe Topic




            //--- Attach Pre Jobs/Check ---
            this._stateJobHdl.PreJobs.Add(new StateJobBasic((sjh) => { return true; }));


            //--- Attach Major Jobs ---
            var positionId = this.MachineMediater.GetPositionId(this.ActiveMask.LocNext);
            var cmdset = EnumMaskTransferCmdSet.None;
            switch (positionId)
            {
                case EnumPositionId.LoadPort01: cmdset = EnumMaskTransferCmdSet.HomeTo_LoadPort01; break;
                case EnumPositionId.LoadPort02: cmdset = EnumMaskTransferCmdSet.HomeTo_LoadPort02; break;
            }

            this._stateJobHdl.MajorJobs.AddRange(this.AttachStateJobFromDeviceCmd(cmdset));
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                this.ReportSecsCeid(EnumCeid.S6F11_MT_RobotMoveToLoadPort, S6F11.Create());
                this.SmTriggerToNext();//Next Trigger
                return true;
            }));


            //--- Execute Entry ---
            this._stateJobHdl.RunInStateEntry();


            //--- Final ---
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }

        private void sMovingToOpenStage_OnEntry(object sender, StateEntryEventArgs e)
        {
            this.SmTriggerToNext();//Next Trigger
        }

        private void sOpenStageCalibration_OnEntry(object sender, StateEntryEventArgs e)
        {
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                this.SmTriggerToNext();//Next Trigger
                return true;
            }));
            this._stateJobHdl.RunInStateEntry();
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }

        private void sOpenStageCalibrationForRelease_OnEntry(object sender, StateEntryEventArgs e)
        {
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                this.SmTriggerToNext();//Next Trigger
                return true;
            }));
            this._stateJobHdl.RunInStateEntry();
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }

        void sReadyToRelease_OnEntry(object sender, StateEntryEventArgs e)
        {
            if (this.ActiveMask.IsNeedClean())
                this.SmTriggerToNext(EnumMaskTransferTransition.CleanStart);
            //if no need clean, wait NoCleanJob transition
        }
        private void sStart_OnEntry(object sender, StateEntryEventArgs e)
        {
            this._stateJobHdl.Clear();//清除上一個的工作&訂閱(通常不應該有殘留)
            this._stateJobHdl.MajorJobs.Add(new StateJobBasic((sjh) =>
            {
                this.SmTriggerToNext();//Next Trigger
                return true;
            }));
            this._stateJobHdl.RunInStateEntry();
            this._stateJobHdl.Clear();//清除自己的工作&訂閱
        }
        private void sWaitAckHome_OnEntry(object sender, StateEntryEventArgs e)
        {
            var start = DateTime.Now;
            this.BroadcastJobNotify(EnumJobNotify.MT_CompleteRelease);
            MvSpinWait.SpinUntil(() => (DateTime.Now - start).TotalSeconds > 3);
        }
        #endregion State OnEntry

        #region State Exit


        private void esExpCalibrationFail_OnExit(object sender, EventArgs e)
        {
        }

        private void esExpCalibrationReleaseFail_OnExit(object sender, EventArgs e)
        {
        }

        private void esExpForceInClamped_OnExit(object sender, EventArgs e)
        {
        }

        private void esExpForceInClamping_OnExit(object sender, EventArgs e)
        {
        }

        private void esExpForceInReleased_OnExit(object sender, EventArgs e)
        {
        }

        private void esExpForceInReleasing_OnExit(object sender, EventArgs e)
        {
        }

        private void esExpMayEsdDamage_OnExit(object sender, EventArgs e)
        {
        }

        private void esExpMayEsdDamageInRelease_OnExit(object sender, EventArgs e)
        {
        }

        private void esExpRobotPositioningError_OnExit(object sender, EventArgs e)
        {
        }

        private void esExpTactileInClamped_OnExit(object sender, EventArgs e)
        {
        }

        private void esExpTactileInReleased_OnExit(object sender, EventArgs e)
        {
        }

        private void sBarcodeReader_OnExit(Object sender, StateExitEventArgs e) { }

        private void sCleanChMoving_OnExit(object sender, EventArgs e)
        {
        }

        void sCleanMovingStart_OnExit(object sender, StateExitEventArgs e)
        {

        }

        private void sCleanMovingReturn_OnExit(object sender, EventArgs e)
        {
        }

        private void sCleanReady_OnExit(object sender, EventArgs e)
        {
        }

        private void sCleanChWaitAckMove_OnExit(object sender, EventArgs e)
        {
        }

        private void sDeviceInitial_OnExit(object sender, EventArgs e)
        {
        }

        private void sHome_OnExit(object sender, EventArgs e)
        {
        }

        private void sHomeClamped_OnExit(object sender, EventArgs e)
        {
        }

        private void sInspectionChCalibration_OnExit(object sender, EventArgs e)
        {
        }

        private void sInspectionChCalibrationForRelease_OnExit(object sender, EventArgs e)
        {
        }

        private void sInspectionChGlassCalibration_OnExit(object sender, EventArgs e)
        {
        }

        private void sInspectionChGlassCalibrationForRelease_OnExit(object sender, EventArgs e)
        {
        }

        private void sLoadPortACalibration_OnExit(object sender, EventArgs e)
        {
        }

        private void sLoadPortACalibrationForRelease_OnExit(object sender, EventArgs e)
        {
        }

        private void sLoadPortBCalibration_OnExit(object sender, EventArgs e)
        {
        }

        private void sLoadPortBCalibrationForRelease_OnExit(object sender, EventArgs e)
        {
        }

        private void sMovingInspectionChForRelease_OnExit(object sender, EventArgs e)
        {
        }

        private void sMovingInspectionChGlassForRelease_OnExit(object sender, EventArgs e)
        {
        }

        private void sMovingOpenStageForRelease_OnExit(object sender, EventArgs e)
        {
        }

        private void sMovingToBarcodeReader_OnExit(Object sender, StateExitEventArgs e) { }

        private void sMovingToHomeClampedFromBarcodeReader_OnExit(Object sender, StateExitEventArgs e) { }

        private void sMovingToHomeClampedFromInspectionCh_OnExit(object sender, EventArgs e)
        {
        }

        private void sMovingToHomeClampedFromInspectionChGlas_OnExit(object sender, EventArgs e)
        {
        }

        private void sMovingToHomeClampedFromLoadPortA_OnExit(object sender, EventArgs e)
        {
        }

        private void sMovingToHomeClampedFromLoadPortB_OnExit(object sender, EventArgs e)
        {
        }

        private void sMovingToHomeClampedFromOpenStage_OnExit(object sender, EventArgs e)
        {
        }

        private void sMovingToHomeFromInspectionCh_OnExit(object sender, EventArgs e)
        {
        }

        private void sMovingToHomeFromInspectionChGlass_OnExit(object sender, EventArgs e)
        {
        }

        private void sMovingToHomeFromLoadPortA_OnExit(object sender, EventArgs e)
        {
        }

        private void sMovingToHomeFromLoadPortB_OnExit(object sender, EventArgs e)
        {
        }

        private void sMovingToHomeFromOpenStage_OnExit(object sender, EventArgs e)
        {
        }

        private void sMovingToInspectionCh_OnExit(object sender, EventArgs e)
        {
        }

        private void sMovingToInspectionChGlass_OnExit(object sender, EventArgs e)
        {
        }

        private void sMovingToLoadPortA_OnExit(object sender, EventArgs e)
        {
            this._stateJobHdl.StopInStateExit();
        }

        private void sMovingToLoadPortAForRelease_OnExit(object sender, EventArgs e)
        {
        }

        private void sMovingToLoadPortB_OnExit(object sender, EventArgs e)
        {
        }

        private void sMovingToLoadPortBForRelease_OnExit(object sender, EventArgs e)
        {
        }

        private void sMovingToOpenStage_OnExit(object sender, EventArgs e)
        {
        }

        private void sOpenStageCalibration_OnExit(object sender, EventArgs e)
        {
        }

        private void sOpenStageCalibrationForRelease_OnExit(object sender, EventArgs e)
        {
        }

        void sReadyToRelease_OnExit(object sender, StateExitEventArgs e)
        {
        }
        private void sStart_OnExit(object sender, EventArgs e)
        {
        }
        private void sWaitAckHome_OnExit(object sender, EventArgs e)
        {
        }
        #endregion State Exit

        #region Transition Guard


        private bool Guard_BarcodeReader_MovingToHomeClampedFromBarcodeReader() { return string.IsNullOrEmpty(this.ActiveMask.MaskBarCode) == false; }


        private bool Guard_CleanMovingReturn_ReadyToRelease() { return true; }

        private bool Guard_CleanReady_CleanChMoving() { return true; }

        private bool Guard_CleanReady_CleanMovingReturn() { return true; }

        private bool Guard_CleanChWaitAckMove_CleanReady() { return true; }

        private bool Guard_DeviceInitial_Home() { return true; }

        private bool Guard_Home_MovingToInspectionCh()
        {
            if (this.ActiveMask == null) return false;
            var type = this.GetAssemblyTypeById(this.ActiveMask.LocCurrent);
            return type == typeof(InspectionChAsbSm) && this.ActiveMask.MaskCurrentUpSide == EnumMaskUpSide.Pellicle;
        }

        private bool Guard_Home_MovingToInspectionChGlass()
        {
            if (this.ActiveMask == null) return false;
            var type = this.GetAssemblyTypeById(this.ActiveMask.LocCurrent);
            return type == typeof(InspectionChAsbSm) && this.ActiveMask.MaskCurrentUpSide == EnumMaskUpSide.Glass;
        }

        private bool Guard_Home_MovingToLoadPort()
        {
            if (this.ActiveMask == null) return false;
            var maskLocCurrType = this.GetAssemblyTypeById(this.ActiveMask.LocCurrent);
            return maskLocCurrType == typeof(LoadPortAsbSm);
        }

        private bool Guard_Home_MovingToOpenStage()
        {
            if (this.ActiveMask == null) return false;
            var type = this.GetAssemblyTypeById(this.ActiveMask.LocCurrent);
            return type == typeof(OpenStageAsbSm);
        }

        private bool Guard_HomeClamped_MovingToBarcodeReader() { return string.IsNullOrEmpty(this.ActiveMask.MaskBarCode); }

        private bool Guard_HomeClamped_ReadyToRelease() { return !string.IsNullOrEmpty(this.ActiveMask.MaskBarCode); }




        private bool Guard_InspectionChCalibration_ExpCalibrationFail() { return true; }

        private bool Guard_InspectionChCalibration_ExpForceInReleased() { return true; }

        private bool Guard_InspectionChCalibration_ExpMayEsdDamage() { return true; }

        private bool Guard_InspectionChCalibrationForRelease_ExpCalibrationFailInRelease() { return true; }

        private bool Guard_InspectionChCalibrationForRelease_ExpForceInClamped() { return true; }

        private bool Guard_InspectionChCalibrationForRelease_ExpMayEsdDamageInRelease() { return true; }

        private bool Guard_InspectionChClamping_MovingToHomeClampedFromInspectionCh()
        {
            if (this.ActiveMask == null) return false;
            var type = this.GetAssemblyTypeById(this.ActiveMask.LocCurrent);
            return type == typeof(InspectionChAsbSm) && this.ActiveMask.MaskCurrentUpSide == EnumMaskUpSide.Pellicle;
        }

        private bool Guard_InspectionChGlassCalibration_ExpCalibrationFail() { return true; }

        private bool Guard_InspectionChGlassCalibration_ExpForceInReleased() { return true; }

        private bool Guard_InspectionChGlassCalibration_ExpMayEsdDamage() { return true; }

        private bool Guard_InspectionChGlassCalibrationForRelease_ExpCalibrationFailInRelease() { return true; }

        private bool Guard_InspectionChGlassCalibrationForRelease_ExpForceInClamped() { return true; }

        private bool Guard_InspectionChGlassCalibrationForRelease_ExpForceInReleasing() { return true; }

        private bool Guard_InspectionChGlassCalibrationForRelease_ExpMayEsdDamageInRelease() { return true; }

        private bool Guard_InspectionChGlassClamping_MovingToHomeClampedFromInspectionChGlass()
        {
            if (this.ActiveMask == null) return false;
            var type = this.GetAssemblyTypeById(this.ActiveMask.LocCurrent);
            return type == typeof(InspectionChAsbSm) && this.ActiveMask.MaskCurrentUpSide == EnumMaskUpSide.Glass;
        }

        private bool Guard_InspectionChGlassReleasing_MovingToHomeFromInspectionChGlass()
        {
            if (this.ActiveMask == null) return false;
            var type = this.GetAssemblyTypeById(this.ActiveMask.LocNext);
            return type == typeof(InspectionChAsbSm) && this.ActiveMask.MaskNextUpSide == EnumMaskUpSide.Glass;
        }

        private bool Guard_InspectionChReleasing_MovingToHomeFromInspectionCh()
        {
            if (this.ActiveMask == null) return false;
            var type = this.GetAssemblyTypeById(this.ActiveMask.LocNext);
            return type == typeof(InspectionChAsbSm) && this.ActiveMask.MaskNextUpSide == EnumMaskUpSide.Pellicle;
        }

        private bool Guard_LoadPortCalibration_ExpCalibrationFail() { return true; }

        private bool Guard_LoadPortCalibration_ExpForceInReleased() { return true; }

        private bool Guard_LoadPortCalibration_ExpMayEsdDamage() { return true; }

        private bool Guard_LoadPortCalibrationForRelease_ExpCalibrationFailInRelease() { return true; }

        private bool Guard_LoadPortCalibrationForRelease_ExpForceInClamped() { return true; }

        private bool Guard_LoadPortCalibrationForRelease_ExpMayEsdDamageInRelease() { return true; }

        private bool Guard_LoadPortClamping_MovingToHomeClampedFromLoadPort()
        {
            if (this.ActiveMask == null) return false;
            var type = this.GetAssemblyTypeById(this.ActiveMask.LocCurrent);
            return type == typeof(LoadPortAsbSm);
        }

        private bool Guard_LoadPortReleasing_MovingToHomeFromLoadPort()
        {
            if (this.ActiveMask == null) return false;
            var type = this.GetAssemblyTypeById(this.ActiveMask.LocNext);
            return type == typeof(LoadPortAsbSm);
        }

        private bool Guard_MovingInspectionChForRelease_ExpForceInClamped() { return true; }

        private bool Guard_MovingInspectionChForRelease_ExpTactileInClamped() { return true; }

        private bool Guard_MovingInspectionChForRelease_InspectionChReleasing()
        { return true; }

        private bool Guard_MovingInspectionChGlassForRelease_ExpForceInClamped() { return true; }

        private bool Guard_MovingInspectionChGlassForRelease_ExpTactileInClamped()
        {
            return true;
        }

        private bool Guard_MovingInspectionChGlassForRelease_InspectionChGlassReleasing()
        { return true; }

        private bool Guard_MovingOpenStageForRelease_ExpForceInClamped()
        {
            return true;
        }

        private bool Guard_MovingOpenStageForRelease_ExpTactileInClamped()
        {
            return true;
        }

        // return MvAop.Define.Try().Return<bool>(() => this.RobotIn_InspectionCh_Laser.Read() < this.spec.RobotIn_InspectionCh && this.MT__Robot.hal.HalMoveIsComplete()); }
        private bool Guard_MovingOpenStageForRelease_OpenStageReleasing()
        { return true; }

        private bool Guard_MovingToBarcodeReader_BarcodeReader() { return true; }

        private bool Guard_MovingToHomeClampedFromBarcodeReader_HomeClamped() { return true; }

        private bool Guard_MovingToHomeClampedFromInspectionCh_ExpForceInClamped()
        {
            return true;
        }

        private bool Guard_MovingToHomeClampedFromInspectionCh_ExpTactileInClamped()
        {
            return true;
        }

        private bool Guard_MovingToHomeClampedFromInspectionCh_HomeClamped()
        {
            if (this.ActiveMask == null) return false;
            var type = this.GetAssemblyTypeById(this.ActiveMask.LocCurrent);
            return type == typeof(InspectionChAsbSm) && this.ActiveMask.MaskCurrentUpSide == EnumMaskUpSide.Pellicle;
        }

        private bool Guard_MovingToHomeClampedFromInspectionChGlass_ExpForceInClamped()
        {
            return true;
        }

        private bool Guard_MovingToHomeClampedFromInspectionChGlass_ExpTactileInClamped()
        {
            return true;
        }

        private bool Guard_MovingToHomeClampedFromInspectionChGlass_HomeClamped()
        {
            if (this.ActiveMask == null) return false;
            var type = this.GetAssemblyTypeById(this.ActiveMask.LocCurrent);
            return type == typeof(InspectionChAsbSm) && this.ActiveMask.MaskCurrentUpSide == EnumMaskUpSide.Glass;
        }

        private bool Guard_MovingToHomeClampedFromLoadPort_ExpForceInClamped()
        {
            return true;
        }

        private bool Guard_MovingToHomeClampedFromLoadPort_ExpTactileInClamped()
        {
            return true;
        }

        private bool Guard_MovingToHomeClampedFromLoadPort_HomeClamped()
        {
            if (this.ActiveMask == null) return false;
            var type = this.GetAssemblyTypeById(this.ActiveMask.LocCurrent);
            return type == typeof(LoadPortAsbSm);
        }

        private bool Guard_MovingToHomeClampedFromOpenStage_ExpForceInClamped()
        {
            return true;
        }

        private bool Guard_MovingToHomeClampedFromOpenStage_ExpTactileInClamped()
        {
            return true;
        }

        private bool Guard_MovingToHomeClampedFromOpenStage_HomeClamped()
        {
            if (this.ActiveMask == null) return false;
            var type = this.GetAssemblyTypeById(this.ActiveMask.LocCurrent);
            return type == typeof(OpenStageAsbSm);
        }

        private bool Guard_MovingToHomeFromInspectionCh_ExpForceInReleased()
        {
            return true;
        }

        private bool Guard_MovingToHomeFromInspectionCh_ExpTactileInReleased()
        {
            return true;
        }

        private bool Guard_MovingToHomeFromInspectionCh_WaitAckHome()
        {
            return true;
        }

        private bool Guard_MovingToHomeFromInspectionChGlass_ExpForceInReleased()
        {
            return true;
        }

        private bool Guard_MovingToHomeFromInspectionChGlass_ExpTactileInReleased()
        {
            return true;
        }

        private bool Guard_MovingToHomeFromInspectionChGlass_WaitAckHome()
        {
            return true;
        }

        private bool Guard_MovingToHomeFromLoadPort_ExpForceInReleased()
        {
            return true;
        }

        private bool Guard_MovingToHomeFromLoadPort_ExpTactileInReleased()
        {
            return true;
        }

        private bool Guard_MovingToHomeFromLoadPort_WaitAckHome()
        {
            return true;
        }

        private bool Guard_MovingToHomeFromOpenStage_ExpForceInReleased()
        {
            return true;
        }

        private bool Guard_MovingToHomeFromOpenStage_ExpTactileInReleased()
        {
            return true;
        }

        private bool Guard_MovingToHomeFromOpenStage_WaitAckHome()
        {
            return true;
        }

        private bool Guard_MovingToInspectionCh_ExpForceInReleased()
        {
            return true;
        }

        private bool Guard_MovingToInspectionCh_ExpTactileInReleased()
        {
            return true;
        }

        private bool Guard_MovingToInspectionCh_InspectionChClamping()
        { return true; }

        private bool Guard_MovingToInspectionChGlass_ExpForceInReleased()
        {
            return true;
        }

        private bool Guard_MovingToInspectionChGlass_ExpTactileInReleased()
        {
            return true;
        }

        private bool Guard_MovingToInspectionChGlass_InspectionChGlassClamping()
        { return true; }

        private bool Guard_MovingToLoadPort_ExpForceInReleased()
        {
            return true;
        }

        private bool Guard_MovingToLoadPort_ExpTactileInReleased()
        {
            return true;
        }

        private bool Guard_MovingToLoadPort_LoadPortClamping()
        { return true; }

        private bool Guard_MovingToLoadPortForRelease_ExpForceInClamped()
        {
            return true;
        }

        private bool Guard_MovingToLoadPortForRelease_ExpTactileInClamped()
        {
            return true;
        }

        private bool Guard_MovingToOpenStage_ExpForceInReleased()
        {
            return true;
        }

        private bool Guard_MovingToOpenStage_ExpTactileInReleased()
        {
            return true;
        }

        private bool Guard_MovingToOpenStage_OpenStageClamping()
        { return true; }

        private bool Guard_OpenStageCalibration_ExpCalibrationFail()
        {
            return true;
        }

        private bool Guard_OpenStageCalibration_ExpForceInReleased()
        {
            return true;
        }

        private bool Guard_OpenStageCalibration_ExpMayEsdDamage()
        {
            return true;
        }

        private bool Guard_OpenStageCalibrationForRelease_ExpCalibrationFailInRelease()
        {
            return true;
        }

        private bool Guard_OpenStageCalibrationForRelease_ExpForceInClamped()
        {
            return true;
        }

        private bool Guard_OpenStageCalibrationForRelease_ExpForceInClamping()
        {
            return true;
        }

        private bool Guard_OpenStageCalibrationForRelease_ExpMayEsdDamageInRelease()
        {
            return true;
        }

        private bool Guard_OpenStageClamping_MovingToHomeClampedFromOpenStage()
        {
            if (this.ActiveMask == null) return false;
            var type = this.GetAssemblyTypeById(this.ActiveMask.LocCurrent);
            return type == typeof(OpenStageAsbSm);
        }

        private bool Guard_OpenStageReleasing_MovingToHomeFromOpenStage()
        {
            if (this.ActiveMask == null) return false;
            var type = this.GetAssemblyTypeById(this.ActiveMask.LocNext);
            return type == typeof(OpenStageAsbSm);
        }

        private bool Guard_ReadyToRelease_CleanMovingStart() { return true; }

        private bool Guard_ReadyToRelease_MovingInspectionChForRelease()
        {
            if (this.ActiveMask == null) return false;
            var type = this.GetAssemblyTypeById(this.ActiveMask.LocNext);
            return type == typeof(InspectionChAsbSm) && this.ActiveMask.MaskNextUpSide == EnumMaskUpSide.Pellicle;
        }


        private bool Guard_CleanMovingStart_CleanReady() { return true; }

        private bool Guard_ReadyToRelease_MovingInspectionChGlassForRelease()
        {
            if (this.ActiveMask == null) return false;
            var type = this.GetAssemblyTypeById(this.ActiveMask.LocNext);
            return type == typeof(InspectionChAsbSm) && this.ActiveMask.MaskNextUpSide == EnumMaskUpSide.Glass;
        }

        private bool Guard_ReadyToRelease_MovingOpenStageForRelease()
        {
            if (this.ActiveMask == null) return false;
            var type = this.GetAssemblyTypeById(this.ActiveMask.LocNext);
            return type == typeof(OpenStageAsbSm);
        }
        private bool Guard_ReadyToRelease_MovingToLoadPortForRelease()
        {
            if (this.ActiveMask == null) return false;
            var type = this.GetAssemblyTypeById(this.ActiveMask.LocNext);
            return type == typeof(LoadPortAsbSm);
        }
        private bool Guard_Start_DeviceInitial()
        {
            return true;
        }
        private bool Guard_WaitAckHome_Home()
        {
            return true;
        }

        private bool Guard_CleanChMoving_CleanChWaitAckMove() { return true; }
        // return MvAop.Define.Try().Return<bool>(() => this.RobotIn_LoadPortA_Laser.Read() < this.spec.RobotIn_LoadPortA && this.MT__Robot.hal.HalMoveIsComplete()); }

        //MvAop.Define.Try().Return<bool>(() => this.RobotIn_InspectionCh_Laser.Read() < this.spec.RobotIn_InspectionCh && this.MT__Robot.hal.HalMoveIsComplete()); }

        //MvAop.Define.Try().Return<bool>(() => this.RobotIn_InspectionCh_Laser.Read() < this.spec.RobotIn_CleanCh && this.MT__Robot.hal.HalMoveIsComplete()); }

        // MvAop.Define.Try().Return<bool>(() => this.MT__Robot.hal.HalGetPose().IntersectRange(this.spec.HomePoseStart, this.spec.HomePoseEnd)); }
        private bool GuardMovingToLoadPortForRelease_LoadPortReleasing()
        { return true; }
        // return MvAop.Define.Try().Return<bool>(() => this.MT__Gripper.CurrentState == this.MT__Gripper.States["Clamped"]); }

        // MvAop.Define.Try().Return<bool>(() => this.MT__Gripper.CurrentState == this.MT__Gripper.States["Clamped"]); }

        // MvAop.Define.Try().Return<bool>(() => this.MT__Gripper.CurrentState == this.MT__Gripper.States["Clamped"]); }

        // MvAop.Define.Try().Return<bool>(() => this.MT__Gripper.CurrentState == this.MT__Gripper.States["Clamped"]); }

        // return MvAop.Define.Try().Return<bool>(() => this.MT__Robot.hal.HalGetPose().IntersectRange(this.spec.HomePoseStart, this.spec.HomePoseEnd)); }

        // MvAop.Define.Try().Return<bool>(() => this.MT__Robot.hal.HalGetPose().IntersectRange(this.spec.HomePoseStart, this.spec.HomePoseEnd)); }

        // MvAop.Define.Try().Return<bool>(() => this.MT__Robot.hal.HalGetPose().IntersectRange(this.spec.HomePoseStart, this.spec.HomePoseEnd)); }
        // return MvAop.Define.Try().Return<bool>(() => this.MT__Gripper.CurrentState == this.MT__Gripper.States["Released"]); }

        // MvAop.Define.Try().Return<bool>(() => this.MT__Gripper.CurrentState == this.MT__Gripper.States["Released"]); }

        // MvAop.Define.Try().Return<bool>(() => this.MT__Gripper.CurrentState == this.MT__Gripper.States["Released"]); }

        // MvAop.Define.Try().Return<bool>(() => this.MT__Gripper.CurrentState == this.MT__Gripper.States["Released"]); }

        // return MvAop.Define.Try().Return<bool>(() => this.MT__Robot.hal.HalMoveIsComplete()); }

        // MvAop.Define.Try().Return<bool>(() => this.MT__Robot.hal.HalMoveIsComplete()); }

        //MvAop.Define.Try().Return<bool>(() => this.MT__Robot.hal.HalMoveIsComplete()); }

        //MvAop.Define.Try().Return<bool>(() => this.MT__Robot.hal.HalMoveIsComplete()); }

        // MvAop.Define.Try().Return<bool>(() => true); }


        #endregion Transition Guard

        #region Transition Trigger


        private void Action_BarcodeReader_MovingToHomeClampedFromBarcodeReader() { }


        private void Action_CleanMovingReturn_ReadyToRelease()
        {
        }

        private void Action_CleanReady_CleanMovingReturn() { }


        private void Action_CleanMovingStart_CleanReady() { }

        private void Action_DeviceInitial_Home()
        {
        }

        private void Action_HomeClamped_MovingToBarcodeReader() { }

        void Action_HomeClamped_ReadyToRelease() { }
        private void Action_InspectionChCalibration_ExpCalibrationFail()
        {
        }
        private void Action_InspectionChCalibration_ExpForceInReleased()
        {
        }
        private void Action_InspectionChCalibration_ExpMayEsdDamage()
        {
        }
        private void Action_InspectionChCalibrationForRelease_ExpCalibrationFailInRelease()
        {
        }
        private void Action_InspectionChCalibrationForRelease_ExpForceInClamped()
        {
        }
        private void Action_InspectionChCalibrationForRelease_ExpMayEsdDamageInRelease()
        {
        }
        private void Action_InspectionChClamping_MovingToHomeClampedFromInspectionCh()
        {
        }
        private void Action_InspectionChGlassCalibration_ExpCalibrationFail()
        {
        }
        private void Action_InspectionChGlassCalibration_ExpForceInReleased()
        {
        }
        private void Action_InspectionChGlassCalibration_ExpMayEsdDamage()
        {
        }
        private void Action_InspectionChGlassCalibrationForRelease_ExpCalibrationFailInRelease()
        {
        }
        private void Action_InspectionChGlassCalibrationForRelease_ExpForceInClamped()
        {
        }
        private void Action_InspectionChGlassCalibrationForRelease_ExpForceInReleasing()
        {
        }
        private void Action_InspectionChGlassCalibrationForRelease_ExpMayEsdDamageInRelease()
        {
        }
        private void Action_InspectionChGlassClamping_MovingToHomeClampedFromInspectionChGlass()
        {
        }
        private void Action_InspectionChGlassReleasing_MovingToHomeFromInspectionChGlass()
        {
        }
        private void Action_InspectionChReleasing_MovingToHomeFromInspectionCh()
        {
        }



        //--- Excception Transition Action ---
        private void Action_LoadPortCalibration_ExpCalibrationFail() { }

        private void Action_LoadPortCalibration_ExpForceInReleased()
        {
        }

        private void Action_LoadPortCalibration_ExpMayEsdDamage()
        {
        }

        private void Action_LoadPortCalibrationForRelease_ExpCalibrationFailInRelease()
        {
        }

        private void Action_LoadPortCalibrationForRelease_ExpForceInClamped()
        {
        }

        private void Action_LoadPortCalibrationForRelease_ExpMayEsdDamageInRelease()
        {
        }

        private void Action_LoadPortClamping_MovingToHomeClampedFromLoadPort()
        {
        }

        private void Action_LoadPortReleasing_MovingToHomeFromLoadPort()
        {
        }

        private void Action_MovingInspectionChForRelease_ExpForceInClamped()
        {
        }

        private void Action_MovingInspectionChForRelease_ExpTactileInClamped()
        {
        }

        private void Action_MovingInspectionChGlassForRelease_ExpForceInClamped()
        {
        }

        private void Action_MovingInspectionChGlassForRelease_ExpTactileInClamped()
        {
        }

        private void Action_MovingInspectionChGlassForRelease_InspectionChGlassReleasing()
        {
        }

        private void Action_MovingOpenStage_OpenStageReleasing()
        {
        }

        private void Action_MovingOpenStageForRelease_ExpForceInClamped()
        {
        }

        private void Action_MovingOpenStageForRelease_ExpTactileInClamped()
        {
        }

        private void Action_MovingToBarcodeReader_BarcodeReader() { }
        private void Action_MovingToHomeClampedFromBarcodeReader_HomeClamped() { }

        private void Action_MovingToHomeClampedFromInspectionCh_ExpForceInClamped()
        {
        }

        private void Action_MovingToHomeClampedFromInspectionCh_ExpTactileInClamped()
        {
        }

        private void Action_MovingToHomeClampedFromInspectionChGlass_ExpForceInClamped()
        {
        }

        private void Action_MovingToHomeClampedFromInspectionChGlass_ExpTactileInClamped()
        {
        }

        private void Action_MovingToHomeClampedFromLoadPort_ExpForceInClamped()
        {
        }

        private void Action_MovingToHomeClampedFromLoadPort_ExpTactileInClamped()
        {
        }

        private void Action_MovingToHomeClampedFromOpenStage_ExpForceInClamped()
        {
        }

        private void Action_MovingToHomeClampedFromOpenStage_ExpTactileInClamped()
        {
        }

        private void Action_MovingToHomeFromInspectionCh_ExpForceInReleased()
        {
        }

        private void Action_MovingToHomeFromInspectionCh_ExpTactileInReleased()
        {
        }

        private void Action_MovingToHomeFromInspectionChGlass_ExpForceInReleased()
        {
        }

        private void Action_MovingToHomeFromInspectionChGlass_ExpTactileInReleased()
        {
        }

        private void Action_MovingToHomeFromLoadPort_ExpForceInReleased()
        {
        }

        private void Action_MovingToHomeFromLoadPort_ExpTactileInReleased()
        {
        }

        private void Action_MovingToHomeFromOpenStage_ExpForceInReleased()
        {
        }

        private void Action_MovingToHomeFromOpenStage_ExpTactileInReleased()
        {
        }

        private void Action_MovingToInspectionCh_ExpForceInReleased()
        {
        }

        private void Action_MovingToInspectionCh_ExpTactileInReleased()
        {
        }

        private void Action_MovingToInspectionCh_InspectionChClamping()
        {
        }

        private void Action_MovingToInspectionChGlass_ExpForceInReleased()
        {
        }

        private void Action_MovingToInspectionChGlass_ExpTactileInReleased()
        {
        }

        private void Action_MovingToInspectionChGlass_InspectionChGlassClamping()
        {
        }

        private void Action_MovingToLoadPort_ExpForceInReleased()
        {
        }

        private void Action_MovingToLoadPort_ExpTactileInReleased()
        {
        }

        private void Action_MovingToLoadPort_LoadPortClamping()
        {
        }

        private void Action_MovingToLoadPortForRelease_ExpForceInClamped()
        {
        }

        private void Action_MovingToLoadPortForRelease_ExpTactileInClamped()
        {
        }

        private void Action_MovingToOpenStage_ExpForceInReleased()
        {
        }

        private void Action_MovingToOpenStage_ExpTactileInReleased()
        {
        }

        private void Action_MovingToOpenStage_OpenStageClamping()
        {
        }

        private void Action_OpenStageCalibration_ExpCalibrationFail()
        {
        }

        private void Action_OpenStageCalibration_ExpForceInReleased()
        {
        }

        private void Action_OpenStageCalibration_ExpMayEsdDamage()
        {
        }

        private void Action_OpenStageCalibrationForRelease_ExpCalibrationFailInRelease()
        {
        }

        private void Action_OpenStageCalibrationForRelease_ExpForceInClamped()
        {
        }

        private void Action_OpenStageCalibrationForRelease_ExpForceInClamping()
        {
        }

        private void Action_OpenStageCalibrationForRelease_ExpMayEsdDamageInRelease()
        {
        }

        private void Action_OpenStageClamping_MovingToHomeClampedFromOpenStage()
        {
        }

        private void Action_OpenStageReleasing_MovingToHomeFromOpenStage()
        {
        }

        private void Action_ReadyToRelease_CleanMovingStart()
        {
            this.ReportSecsCeid(EnumCeid.S6F11_MT_RobotMoveToCleanCh, S6F11.Create());
        }

        private void Action_ReadyToRelease_MovingInspectionChForRelease()
        {
        }

        private void Action_ReadyToRelease_MovingInspectionChGlassForRelease()
        {
        }

        private void Action_ReadyToRelease_MovingOpenStageForRelease()
        {
        }

        private void Action_ReadyToRelease_MovingToLoadPortForRelease()
        {
        }

        private void Action_Start_DeviceInitial()
        {
        }
        private void Action_WaitAckHome_Home()
        {
        }

        private void Action_CleanChMoving_CleanChWaitAckMove()
        {
        }
        private void Action_CleanReady_CleanChMoving()
        {
        }
        private void ActionCleanChWaitAckMove_CleanReady()
        {
        }
        private void ActionHome_MovingToInspectionCh()
        {
        }

        private void ActionHome_MovingToInspectionChGlass()
        {
        }

        private void ActionHome_MovingToLoadPort()
        {
        }
        private void ActionHome_MovingToOpenStage()
        {
        }
        private void ActionMovingInspectionChForRelease_InspectionChReleasing()
        {
        }
        private void ActionMovingToHomeClampedFromInspectionCh_HomeClamped()
        {
            this.BroadcastJobNotify(EnumJobNotify.MT_CompleteClamp);
        }

        private void ActionMovingToHomeClampedFromInspectionChGlass_HomeClamped()
        {
            this.BroadcastJobNotify(EnumJobNotify.MT_CompleteClamp);

        }

        private void ActionMovingToHomeClampedFromLoadPort_HomeClamped()
        {
            this.BroadcastJobNotify(EnumJobNotify.MT_CompleteClamp);

        }

        private void ActionMovingToHomeClampedFromOpenStage_HomeClamped()
        {
            this.BroadcastJobNotify(EnumJobNotify.MT_CompleteClamp);

        }

        private void ActionMovingToHomeFromInspectionCh_WaitAckHome()
        {
        }

        private void ActionMovingToHomeFromInspectionChGlass_WaitAckHome()
        {
        }

        private void ActionMovingToHomeFromLoadPort_WaitAckHome()
        {
        }

        private void ActionMovingToHomeFromOpenStage_WaitAckHome()
        {
        }
        private void ActionMovingToLoadPortForRelease_LoadPortReleasing()
        {
        }


        #endregion Transition Trigger

    }
}