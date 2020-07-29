using MaskAutoCleaner.v1_0.Machine.InspectionCh;
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

        private void SetDrawerWorkState(EnumMacMsMaskTransferState state)
        { CurrentWorkState = state; }

        private void ResetCurrentWorkState()
        { CurrentWorkState = EnumMacMsMaskTransferState.Initial; }

        EnumMacMsMaskTransferState CurrentWorkState { get; set; }

        TimeOutController timeoutObj = new TimeOutController();

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
            sWaitingForBarcodeReader.OnEntry += sBarcodeReader_OnEntry;
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
            sLPHome.OnExit += (sender, e) =>
            {
                var args = (MacMaskTransferCommonExitEventArgs)e;
                MacTransition transition = null;
                MacState nextState = null;
                if (args.Result == MacMaskTransferCommonResult.ReadyToMoveToLoadPortA)
                {
                    transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToLoadPortA.ToString()];
                    nextState = transition.StateTo;
                    nextState.DoEntry(new MacStateEntryEventArgs(0));
                }
                else if (args.Result == MacMaskTransferCommonResult.ReadyToMoveToLoadPortB)
                {
                    transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToLoadPortB.ToString()];
                    nextState = transition.StateTo;
                    nextState.DoEntry(new MacStateEntryEventArgs(0));
                }
                else if (args.Result == MacMaskTransferCommonResult.ReadyToMoveToInspectionCh)
                {
                    transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToInspectionCh.ToString()];
                    nextState = transition.StateTo;
                    nextState.DoEntry(new MacStateEntryEventArgs(0));
                }
                else if (args.Result == MacMaskTransferCommonResult.ReadyToMoveToOpenStage)
                {
                    transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToOpenStage.ToString()];
                    nextState = transition.StateTo;
                    nextState.DoEntry(new MacStateEntryEventArgs(0));
                }
                else if (args.Result == MacMaskTransferCommonResult.Wait)
                {
                    transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToReceiveTriggerAtLPHome.ToString()];
                    nextState = transition.StateTo;
                    nextState.DoEntry(new MacStateEntryEventArgs(0));
                }
                else if (args.Result == MacMaskTransferCommonResult.Fail)
                {
                    // TODO
                }
                else if (args.Result == MacMaskTransferCommonResult.TimeOut)
                {
                    // TODO
                }
            };
            sICHome.OnExit += sICHome_OnExit;
            sMovingToLoadPortA.OnExit += sMovingToLoadPortA_OnExit;
            sMovingToLoadPortB.OnExit += sMovingToLoadPortB_OnExit;
            sMovingToInspectionCh.OnExit += sMovingToInspectionCh_OnExit;
            sMovingToInspectionChGlass.OnExit += sMovingToInspectionChGlass_OnExit;
            sMovingToOpenStage.OnExit += sMovingToOpenStage_OnExit;
            sLoadPortAClamping.OnExit += sLoadPortAClamping_OnExit;
            sLoadPortBClamping.OnExit += sLoadPortBClamping_OnExit;
            sInspectionChClamping.OnExit += sInspectionChClamping_OnExit;
            sInspectionChGlassClamping.OnExit += sInspectionChGlassClamping_OnExit;
            sOpenStageClamping.OnExit += sOpenStageClamping_OnExit;
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
            sWaitingForBarcodeReader.OnExit += sWaitingForBarcodeReader_OnExit;
            sMovingToLPHomeClampedFromBarcodeReader.OnExit += sMovingToLPHomeClampedFromBarcodeReader_OnExit;
            sMovingToLoadPortAForRelease.OnExit += sMovingToLoadPortAForRelease_OnExit;
            sMovingToLoadPortBForRelease.OnExit += sMovingToLoadPortBForRelease_OnExit;
            sMovingInspectionChForRelease.OnExit += sMovingInspectionChForRelease_OnExit;
            sMovingInspectionChGlassForRelease.OnExit += sMovingInspectionChGlassForRelease_OnExit;
            sMovingOpenStageForRelease.OnExit += sMovingOpenStageForRelease_OnExit;
            sLoadPortAReleasing.OnExit += sLoadPortAReleasing_OnExit;
            sLoadPortBReleasing.OnExit += sLoadPortBReleasing_OnExit;
            sInspectionChReleasing.OnExit += sInspectionChReleasing_OnExit;
            sInspectionChGlassReleasing.OnExit += sInspectionChGlassReleasing_OnExit;
            sOpenStageReleasing.OnExit += sOpenStageReleasing_OnExit;
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
            MacTransition tLPHome_LPHome = NewTransition(sLPHome,sLPHome, EnumMacMsMaskTransferTransition.ReadyToReceiveTriggerAtLPHome);
            MacTransition tLPHomeClamped_LPHomeClamped = NewTransition(sLPHomeClamped, sLPHomeClamped, EnumMacMsMaskTransferTransition.ReadyToReceiveTriggerAtLPHomeClamped);

            //Receive Transfer From Home
            MacTransition tLPHome_MovingToLoadPortA = NewTransition(sLPHome, sMovingToLoadPortA, EnumMacMsMaskTransferTransition.ReadyToMoveToLoadPortA);
            MacTransition tLPHome_MovingToLoadPortB = NewTransition(sLPHome, sMovingToLoadPortB, EnumMacMsMaskTransferTransition.ReadyToMoveToLoadPortB);
            MacTransition tICHome_MovingToInspectionCh = NewTransition(sICHome, sMovingToInspectionCh, EnumMacMsMaskTransferTransition.ReadyToMoveToInspectionCh);
            MacTransition tICHome_MovingToInspectionChGlass = NewTransition(sICHome, sMovingToInspectionChGlass, EnumMacMsMaskTransferTransition.ReadyToMoveToInspectionChGlass);
            MacTransition tLPHome_MovingToOpenStage = NewTransition(sLPHome, sMovingToOpenStage, EnumMacMsMaskTransferTransition.ReadyToMoveToOpenStage);

            //Complete Move
            MacTransition tMovingToLoadPortA_LoadPortAClamping = NewTransition(sMovingToLoadPortA, sLoadPortAClamping, EnumMacMsMaskTransferTransition.ReadyToClampInLoadPortA);
            MacTransition tMovingToLoadPortB_LoadPortBClamping = NewTransition(sMovingToLoadPortB, sLoadPortBClamping, EnumMacMsMaskTransferTransition.ReadyToClampInLoadPortB);
            MacTransition tMovingToInspectionCh_InspectionChClamping = NewTransition(sMovingToInspectionCh, sInspectionChClamping, EnumMacMsMaskTransferTransition.ReadyToClampInInspectionCh);
            MacTransition tMovingToInspectionChGlass_InspectionChGlassClamping = NewTransition(sMovingToInspectionChGlass, sInspectionChGlassClamping, EnumMacMsMaskTransferTransition.ReadyToClampInInspectionChGlass);
            MacTransition tMovingToOpenStage_OpenStageClamping = NewTransition(sMovingToOpenStage, sOpenStageClamping, EnumMacMsMaskTransferTransition.ReadyToClampInOpenStage);

            //Compelte Clamped -> 準備移回Home_Clamped
            MacTransition tLoadPortAClamping_MovingToLPHomeClampedFromLoadPortA = NewTransition(sLoadPortAClamping, sMovingToLPHomeClampedFromLoadPortA, EnumMacMsMaskTransferTransition.ReadyToMoveToLPHomeClampedFromLoadPortA);
            MacTransition tLoadPortBClamping_MovingToLPHomeClampedFromLoadPortB = NewTransition(sLoadPortBClamping, sMovingToLPHomeClampedFromLoadPortB, EnumMacMsMaskTransferTransition.ReadyToMoveToLPHomeClampedFromLoadPortB);
            MacTransition tInspectionChClamping_MovingToICHomeClampedFromInspectionCh = NewTransition(sInspectionChClamping, sMovingToICHomeClampedFromInspectionCh, EnumMacMsMaskTransferTransition.ReadyToMoveToICHomeClampedFromInspectionCh);
            MacTransition tInspectionChGlassClamping_MovingToICHomeClampedFromInspectionChGlass = NewTransition(sInspectionChGlassClamping, sMovingToICHomeClampedFromInspectionChGlass, EnumMacMsMaskTransferTransition.ReadyToMoveToICHomeClampedFromInspectionChGlass);
            MacTransition tOpenStageClamping_MovingToLPHomeClampedFromOpenStage = NewTransition(sOpenStageClamping, sMovingToLPHomeClampedFromOpenStage, EnumMacMsMaskTransferTransition.ReadyToMoveToLPHomeClampedFromOpenStage);

            //Complete Move
            MacTransition tMovingToLPHomeClampedFromLoadPortA_sLPHomeClamped = NewTransition(sMovingToLPHomeClampedFromLoadPortA, sLPHomeClamped, EnumMacMsMaskTransferTransition.ReadyToMoveToLPHomeClampedFromLoadPortA);
            MacTransition tMovingToLPHomeClampedFromLoadPortB_sLPHomeClamped = NewTransition(sMovingToLPHomeClampedFromLoadPortB, sLPHomeClamped, EnumMacMsMaskTransferTransition.ReadyToMoveToLPHomeClampedFromLoadPortB);
            MacTransition tMovingToICHomeClampedFromInspectionCh_sICHomeClamped = NewTransition(sMovingToICHomeClampedFromInspectionCh, sICHomeClamped, EnumMacMsMaskTransferTransition.ReadyToMoveToICHomeClampedFromInspectionCh);
            MacTransition tMovingToICHomeClampedFromInspectionChGlass_sICHomeClamped = NewTransition(sMovingToICHomeClampedFromInspectionChGlass, sICHomeClamped, EnumMacMsMaskTransferTransition.ReadyToMoveToICHomeClampedFromInspectionChGlass);
            MacTransition tMovingToLPHomeClampedFromOpenStage_sICHomeClamped = NewTransition(sMovingToLPHomeClampedFromOpenStage, sLPHomeClamped, EnumMacMsMaskTransferTransition.ReadyToMoveToLPHomeClampedFromOpenStage);

            //Barcode Reader
            MacTransition tLPHomeClamped_MovingToBarcodeReaderClamped = NewTransition(sLPHomeClamped, sMovingToBarcodeReaderClamped, EnumMacMsMaskTransferTransition.ReadyToMoveToBarcodeReaderClamped);
            MacTransition tMovingToBarcodeReaderClamped_WaitingForBarcodeReader = NewTransition(sMovingToBarcodeReaderClamped, sWaitingForBarcodeReader, EnumMacMsMaskTransferTransition.ReadyToWaitForBarcodeReader);
            MacTransition tWaitingForBarcodeReader_MovingToLPHomeClampedFromBarcodeReader = NewTransition(sWaitingForBarcodeReader, sMovingToLPHomeClampedFromBarcodeReader, EnumMacMsMaskTransferTransition.ReadyToMoveToLPHomeClampedFromBarcodeReader);
            MacTransition tMovingToLPHomeClampedFromBarcodeReader_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromBarcodeReader, sLPHomeClamped, EnumMacMsMaskTransferTransition.ReadyToStandbyAtLPHomeClampedFromBarcodeReader);


            //Is Ready to Release
            //MacTransition tLPHomeClamped_ReadyToRelease = NewTransition(sLPHomeClamped, sReadyToRelease, EnumMacMsMaskTransferTransition.IsReady);
            MacTransition tLPHomeClamped_MovingToBarcodeReader = NewTransition(sLPHomeClamped, sMovingToBarcodeReaderClamped, EnumMacMsMaskTransferTransition.IsReady);


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
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    if (CurrentWorkState == EnumMacMsMaskTransferState.Initial)
                    {
                        HalMaskTransfer.Initial();
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                        break;
                    }
                    else if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sLPHome_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var ICState = new MacMsInspectionCh();// TODO: Get InspectionCh State
                    // TODO: Get Other Components State
                    if (ICState.CurrentWorkState == EnumMacMsInspectionChState.WaitingForReleaseMask)
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.ReadyToMoveToInspectionCh));
                        break;
                    }
                    else if (CurrentWorkState == EnumMacMsMaskTransferState.LPHome)
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Wait));
                        break;
                    }
                    else if(timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
                    }
                    // TODO: Other Components State Check
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sLPHomeClamped_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var ICState = new MacMsInspectionCh();// TODO: Get InspectionCh State
                    // TODO: Get Other Components State
                    if (ICState.CurrentWorkState == EnumMacMsInspectionChState.WaitingForPutIntoMask)
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.ReadyToMoveToInspectionChForRelease));
                        break;
                    }
                    else if (CurrentWorkState == EnumMacMsMaskTransferState.LPHomeClamped)
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Wait));
                        break;
                    }
                    else if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
                    }
                    // TODO: Other Components State Check
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
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
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingToLoadPortA;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sLoadPortAClamping_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.LoadPortAClamping;
                    if (isGuard)
                    {
                        var MaskType = (uint)e.Parameter;
                        HalMaskTransfer.Clamp(MaskType);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sMovingToLPHomeClampedFromLoadPortA_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeClampedFromLoadPortA;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sMovingToLoadPortAForRelease_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingToLoadPortAForRelease;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sLoadPortAReleasing_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.LoadPortAReleasing;
                    if (isGuard)
                    {
                        HalMaskTransfer.Unclamp();
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sMovingToLPHomeFromLoadPortA_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeFromLoadPortA;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                        HalMaskTransfer.RobotMoving(true);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }
        #endregion

        #region Load Port B
        private void sMovingToLoadPortB_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingToLoadPortB;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sLoadPortBClamping_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.LoadPortBClamping;
                    if (isGuard)
                    {
                        var MaskType = (uint)e.Parameter;
                        HalMaskTransfer.Clamp(MaskType);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sMovingToLPHomeClampedFromLoadPortB_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeClampedFromLoadPortB;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sMovingToLoadPortBForRelease_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingToLoadPortBForRelease;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sLoadPortBReleasing_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.LoadPortBReleasing;
                    if (isGuard)
                    {
                        HalMaskTransfer.Unclamp();
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sMovingToLPHomeFromLoadPortB_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeFromLoadPortB;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }
        #endregion

        #region Inspection Chamber
        private void sMovingToInspectionCh_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingToInspectionCh;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        // TODO: Robot move path
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sInspectionChClamping_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.InspectionChClamping;
                    if (isGuard)
                    {
                        var MaskType = (uint)e.Parameter;
                        HalMaskTransfer.Clamp(MaskType);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sMovingToICHomeClampedFromInspectionCh_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingToICHomeClampedFromInspectionCh;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        // TODO: Robot move path
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sMovingInspectionChForRelease_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingInspectionChForRelease;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        // TODO: Robot move path
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sInspectionChReleasing_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.InspectionChReleasing;
                    if (isGuard)
                    {
                        HalMaskTransfer.Unclamp();
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sMovingToICHomeFromInspectionCh_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingToICHomeFromInspectionCh;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        // TODO: Robot move path
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }



        private void sMovingToInspectionChGlass_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingToInspectionChGlass;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        // TODO: Robot move path
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sInspectionChGlassClamping_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.InspectionChGlassClamping;
                    if (isGuard)
                    {
                        var MaskType = (uint)e.Parameter;
                        HalMaskTransfer.Clamp(MaskType);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sMovingToICHomeClampedFromInspectionChGlass_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingToICHomeClampedFromInspectionChGlass;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        // TODO: Robot move path
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sMovingInspectionChGlassForRelease_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingInspectionChGlassForRelease;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        // TODO: Robot move path
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sInspectionChGlassReleasing_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.Initial;
                    if (isGuard)
                    {
                        HalMaskTransfer.Unclamp();
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sMovingToICHomeFromInspectionChGlass_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingToICHomeFromInspectionChGlass;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        // TODO: Robot move path
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
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
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingToOpenStage;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sOpenStageClamping_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.OpenStageClamping;
                    if (isGuard)
                    {
                        var MaskType = (uint)e.Parameter;
                        HalMaskTransfer.Clamp(MaskType);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sMovingToLPHomeClampedFromOpenStage_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeClampedFromOpenStage;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sMovingOpenStageForRelease_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingOpenStageForRelease;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sOpenStagReleasing_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.OpenStageReleasing;
                    if (isGuard)
                    {
                        HalMaskTransfer.Unclamp();
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sMovingToLPHomeFromOpenStage_OnEntry(object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeFromOpenStage;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }
        #endregion

        #region Barcode Reader
        private void sMovingToBarcodeReaderClamped_OnEntry(Object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingToBarcodeReaderClamped;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        // TODO: Robot move path
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sBarcodeReader_OnEntry(Object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.WaitingForBarcodeReader;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        // TODO: Robot move path
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sMovingToLPHomeClampedFromBarcodeReader_OnEntry(Object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    var isGuard = CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeClampedFromBarcodeReader;
                    if (isGuard)
                    {
                        HalMaskTransfer.RobotMoving(true);
                        // TODO: Robot move path
                        HalMaskTransfer.RobotMoving(false);
                        thisState.DoExit(new MacStateExitEventArgs());
                        break;
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        // TODO: to throw a time out Exception 
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
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

        #region Load Port
        private void sMovingToLoadPortA_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToClampInLoadPortA.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(0));
        }
        private void sMovingToLoadPortB_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToClampInLoadPortB.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(0));
        }
        private void sLoadPortAClamping_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToLPHomeClampedFromLoadPortA.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        private void sLoadPortBClamping_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToLPHomeClampedFromLoadPortB.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        private void sMovingToLPHomeClampedFromLoadPortA_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToStandbyAtLPHomeClampedFromLoadPortA.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        private void sMovingToLPHomeClampedFromLoadPortB_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToStandbyAtLPHomeClampedFromLoadPortB.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        private void sMovingToLoadPortAForRelease_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToReleaseInLoadPortA.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        private void sMovingToLoadPortBForRelease_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToReleaseInLoadPortB.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        private void sLoadPortAReleasing_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToLPHomeFromLoadPortA.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        private void sLoadPortBReleasing_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToLPHomeFromLoadPortB.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        private void sMovingToLPHomeFromLoadPortA_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToStandbyAtLPHomeFromLoadPortA.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        private void sMovingToLPHomeFromLoadPortB_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToStandbyAtLPHomeFromLoadPortB.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        #endregion

        #region Inspection Chamber
        private void sMovingToInspectionCh_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToClampInInspectionCh.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(0));
        }
        private void sMovingToInspectionChGlass_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToClampInInspectionChGlass.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(0));
        }
        private void sInspectionChClamping_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToICHomeClampedFromInspectionCh.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(0));
        }
        private void sInspectionChGlassClamping_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToICHomeClampedFromInspectionChGlass.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(0));
        }
        private void sMovingToICHomeClampedFromInspectionCh_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToStandbyAtICHomeClampedFromInspectionCh.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(0));
        }
        private void sMovingToICHomeClampedFromInspectionChGlas_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToStandbyAtICHomeClampedFromInspectionChGlass.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(0));
        }
        private void sMovingInspectionChForRelease_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToReleaseInInspectionCh.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(0));
        }
        private void sMovingInspectionChGlassForRelease_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToReleaseInInspectionChGlass.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(0));
        }
        private void sInspectionChReleasing_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToICHomeFromInspectionCh.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(0));
        }
        private void sInspectionChGlassReleasing_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToICHomeFromInspectionChGlass.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(0));
        }
        private void sMovingToICHomeFromInspectionCh_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToStandbyAtICHomeFromInspectionCh.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(0));
        }
        private void sMovingToICHomeFromInspectionChGlass_OnExit(object sender, MacStateExitEventArgs e)
        {
            var thisState = (MacState)sender;
            var thisTransition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToStandbyAtICHomeFromInspectionChGlass.ToString()];
            var nextState = thisTransition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(0));
        }

        #endregion

        #region Clean Chamber
        private void sCleanChMoving_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sCleanMovingStart_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sCleanMovingReturn_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sCleanReady_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sCleanChWaitAckMove_OnExit(object sender, MacStateExitEventArgs e) { }
        #endregion

        #region Open Stage
        private void sMovingToOpenStage_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sOpenStageClamping_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToLPHomeClampedFromOpenStage_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingOpenStageForRelease_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sOpenStageReleasing_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sMovingToHomeFromOpenStage_OnExit(object sender, MacStateExitEventArgs e) { }
        #endregion

        #region Barcode Reader
        private void sMovingToBarcodeReader_OnExit(Object sender, MacStateExitEventArgs e) { }
        private void sWaitingForBarcodeReader_OnExit(Object sender, MacStateExitEventArgs e) { }
        private void sMovingToLPHomeClampedFromBarcodeReader_OnExit(Object sender, MacStateExitEventArgs e) { }
        #endregion

        private void sDeviceInitial_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sLPHome_OnExit(object sender, MacStateExitEventArgs e)
        {
            
        }
        private void sICHome_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sLPHomeClamped_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.ReadyToMoveToLoadPortAForRelease)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToLoadPortAForRelease.ToString()];
            }
            else if (args.Result == MacMaskTransferCommonResult.ReadyToMoveToLoadPortBForRelease)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToLoadPortBForRelease.ToString()];
            }
            else if (args.Result == MacMaskTransferCommonResult.ReadyToMoveToInspectionChForRelease)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToInspectionChForRelease.ToString()];
            }
            else if (args.Result == MacMaskTransferCommonResult.ReadyToMoveToOpenStageForRelease)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToOpenStageForRelease.ToString()];
            }
            else if (args.Result == MacMaskTransferCommonResult.ReadyToMoveToBarcodeReaderClamped)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToBarcodeReaderClamped.ToString()];
            }
            else if (args.Result == MacMaskTransferCommonResult.Wait)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToReceiveTriggerAtLPHomeClamped.ToString()];
            }
            else if (args.Result == MacMaskTransferCommonResult.Fail)
            {
                // TODO
            }
            else if (args.Result == MacMaskTransferCommonResult.TimeOut)
            {
                // TODO
            }
            nextState = transition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        private void sICHomeClamped_OnExit(object sender, MacStateExitEventArgs e) { }
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


        private void sReadyToRelease_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sStart_OnExit(object sender, MacStateExitEventArgs e) { }
        private void sWaitAckHome_OnExit(object sender, MacStateExitEventArgs e) { }
        #endregion State Exit

        public class TimeOutController
        {
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
                return IsTimeOut(startTime, 20);
            }
        }



    }
}