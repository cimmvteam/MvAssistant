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

        EnumMacMsMaskTransferState CurrentWorkState { get; set; }

        TimeOutController timeoutObj = new TimeOutController();

        public void Initial()
        {
            this.States[EnumMacMsMaskTransferState.Initial.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }

        public override void LoadStateMachine()
        {
            //--- Declare State ---
            #region State
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
            #endregion

            //--- Transition ---
            #region Transition
            MacTransition tStart_DeviceInitial = NewTransition(sStart, sDeviceInitial, EnumMacMsMaskTransferTransition.PowerON);
            MacTransition tDeviceInitial_LPHome = NewTransition(sDeviceInitial, sLPHome, EnumMacMsMaskTransferTransition.Initial);
            MacTransition tLPHome_LPHome = NewTransition(sLPHome, sLPHome, EnumMacMsMaskTransferTransition.ReadyToReceiveTriggerAtLPHome);
            MacTransition tLPHomeClamped_LPHomeClamped = NewTransition(sLPHomeClamped, sLPHomeClamped, EnumMacMsMaskTransferTransition.ReadyToReceiveTriggerAtLPHomeClamped);
            MacTransition tWaitingForBarcodeReader_WaitingForBarcodeReader = NewTransition(sWaitingForBarcodeReader, sWaitingForBarcodeReader, EnumMacMsMaskTransferTransition.ReadyToReceiveTriggerAtBarcodeReader);

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
            #endregion

            #region State Register OnEntry OnExit
            //Normal Entry
            #region Entry
            sStart.OnEntry += sStart_OnEntry;


            sDeviceInitial.OnEntry += (sender, e) =>
                {
                    var thisState = (MacState)sender;
                    MacTransition transition = null;
                    DateTime thisTime = DateTime.Now;
                    Action guard = () =>
                    {
                        while (true)
                        {
                            if (CurrentWorkState == EnumMacMsMaskTransferState.Initial)
                            {
                                HalMaskTransfer.Initial();
                                transition = tDeviceInitial_LPHome;
                                break;
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
            sDeviceInitial.OnExit += (sender,e)=>
            {
                var args = (MacMaskTransferCommonExitEventArgs)e;
                var nextState = args.Transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sLPHome.OnEntry += (sender, e) =>
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
                        if (timeoutObj.IsTimeOut(thisTime))
                        {
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                            break;
                        }
                        // TODO: Other Components State Check
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sLPHome.OnExit += sLPHome_OnExit;

            sLPHomeClamped.OnEntry += sLPHomeClamped_OnEntry;
            sLPHomeClamped.OnExit += sLPHomeClamped_OnExit;

            sICHome.OnEntry += sICHome_OnEntry;
            sICHome.OnExit += sICHome_OnExit;

            sICHomeClamped.OnEntry += sICHomeClamped_OnEntry;
            sICHomeClamped.OnExit += sICHomeClamped_OnExit;

            #region Load PortA
            sMovingToLoadPortA.OnEntry += sMovingToLoadPortA_OnEntry;
            sMovingToLoadPortA.OnExit += sMovingToLoadPortA_OnExit;

            sLoadPortAClamping.OnEntry += sLoadPortAClamping_OnEntry;
            sLoadPortAClamping.OnExit += sLoadPortAClamping_OnExit;

            sMovingToLPHomeClampedFromLoadPortA.OnEntry += sMovingToLPHomeClampedFromLoadPortA_OnEntry;
            sMovingToLPHomeClampedFromLoadPortA.OnExit += sMovingToLPHomeClampedFromLoadPortA_OnExit;

            sMovingToLoadPortAForRelease.OnEntry += sMovingToLoadPortAForRelease_OnEntry;
            sMovingToLoadPortAForRelease.OnExit += sMovingToLoadPortAForRelease_OnExit;

            sLoadPortAReleasing.OnEntry += sLoadPortAReleasing_OnEntry;
            sLoadPortAReleasing.OnExit += sLoadPortAReleasing_OnExit;

            sMovingToLPHomeFromLoadPortA.OnEntry += sMovingToLPHomeFromLoadPortA_OnEntry;
            sMovingToLPHomeFromLoadPortA.OnExit += sMovingToLPHomeFromLoadPortA_OnExit;
            #endregion

            #region Load PortB
            sMovingToLoadPortB.OnEntry += sMovingToLoadPortB_OnEntry;
            sMovingToLoadPortB.OnExit += sMovingToLoadPortB_OnExit;

            sLoadPortBClamping.OnEntry += sLoadPortBClamping_OnEntry;
            sLoadPortBClamping.OnExit += sLoadPortBClamping_OnExit;

            sMovingToLPHomeClampedFromLoadPortB.OnEntry += sMovingToLPHomeClampedFromLoadPortB_OnEntry;
            sMovingToLPHomeClampedFromLoadPortB.OnExit += sMovingToLPHomeClampedFromLoadPortB_OnExit;

            sMovingToLoadPortBForRelease.OnEntry += sMovingToLoadPortBForRelease_OnEntry;
            sMovingToLoadPortBForRelease.OnExit += sMovingToLoadPortBForRelease_OnExit;

            sLoadPortBReleasing.OnEntry += sLoadPortBReleasing_OnEntry;
            sLoadPortBReleasing.OnExit += sLoadPortBReleasing_OnExit;

            sMovingToLPHomeFromLoadPortB.OnEntry += sMovingToLPHomeFromLoadPortB_OnEntry;
            sMovingToLPHomeFromLoadPortB.OnExit += sMovingToLPHomeFromLoadPortB_OnExit;
            #endregion

            #region Inspection Ch
            sMovingToInspectionCh.OnEntry += sMovingToInspectionCh_OnEntry;
            sMovingToInspectionCh.OnExit += sMovingToInspectionCh_OnExit;

            sInspectionChClamping.OnEntry += sInspectionChClamping_OnEntry;
            sInspectionChClamping.OnExit += sInspectionChClamping_OnExit;

            sMovingToICHomeClampedFromInspectionCh.OnEntry += sMovingToICHomeClampedFromInspectionCh_OnEntry;
            sMovingToICHomeClampedFromInspectionCh.OnExit += sMovingToICHomeClampedFromInspectionCh_OnExit;

            sMovingInspectionChForRelease.OnEntry += sMovingInspectionChForRelease_OnEntry;
            sMovingInspectionChForRelease.OnExit += sMovingInspectionChForRelease_OnExit;

            sInspectionChReleasing.OnEntry += sInspectionChReleasing_OnEntry;
            sInspectionChReleasing.OnExit += sInspectionChReleasing_OnExit;

            sMovingToICHomeFromInspectionCh.OnEntry += sMovingToICHomeFromInspectionCh_OnEntry;
            sMovingToICHomeFromInspectionCh.OnExit += sMovingToICHomeFromInspectionCh_OnExit;



            sMovingToInspectionChGlass.OnEntry += sMovingToInspectionChGlass_OnEntry;
            sMovingToInspectionChGlass.OnExit += sMovingToInspectionChGlass_OnExit;

            sInspectionChGlassClamping.OnEntry += sInspectionChGlassClamping_OnEntry;
            sInspectionChGlassClamping.OnExit += sInspectionChGlassClamping_OnExit;

            sMovingToICHomeClampedFromInspectionChGlass.OnEntry += sMovingToICHomeClampedFromInspectionChGlass_OnEntry;
            sMovingToICHomeClampedFromInspectionChGlass.OnExit += sMovingToICHomeClampedFromInspectionChGlas_OnExit;

            sMovingInspectionChGlassForRelease.OnEntry += sMovingInspectionChGlassForRelease_OnEntry;
            sMovingInspectionChGlassForRelease.OnExit += sMovingInspectionChGlassForRelease_OnExit;

            sInspectionChGlassReleasing.OnEntry += sInspectionChGlassReleasing_OnEntry;
            sInspectionChGlassReleasing.OnExit += sInspectionChGlassReleasing_OnExit;

            sMovingToICHomeFromInspectionChGlass.OnEntry += sMovingToICHomeFromInspectionChGlass_OnEntry;
            sMovingToICHomeFromInspectionChGlass.OnExit += sMovingToICHomeFromInspectionChGlass_OnExit;
            #endregion

            #region Clean Ch
            sCleanReady.OnEntry += sCleanReady_OnEntry;
            sCleanReady.OnExit += sCleanReady_OnExit;

            sCleanMovingStart.OnEntry += sCleanMovingStart_OnEntry;
            sCleanMovingStart.OnExit += sCleanMovingStart_OnExit;

            sCleanMovingReturn.OnEntry += sCleanMovingReturn_OnEntry;
            sCleanMovingReturn.OnExit += sCleanMovingReturn_OnExit;

            sCleanChMoving.OnEntry += sCleanChMoving_OnEntry;
            sCleanChMoving.OnExit += sCleanChMoving_OnExit;

            sCleanChWaitAckMove.OnEntry += sCleanChWaitAckMove_OnEntry;
            sCleanChWaitAckMove.OnExit += sCleanChWaitAckMove_OnExit;
            #endregion

            #region OpenStage
            sMovingToOpenStage.OnEntry += sMovingToOpenStage_OnEntry;
            sMovingToOpenStage.OnExit += sMovingToOpenStage_OnExit;

            sOpenStageClamping.OnEntry += sOpenStageClamping_OnEntry;
            sOpenStageClamping.OnExit += sOpenStageClamping_OnExit;

            sMovingToLPHomeClampedFromOpenStage.OnEntry += sMovingToLPHomeClampedFromOpenStage_OnEntry;
            sMovingToLPHomeClampedFromOpenStage.OnExit += sMovingToLPHomeClampedFromOpenStage_OnExit;

            sMovingOpenStageForRelease.OnEntry += sMovingOpenStageForRelease_OnEntry;
            sMovingOpenStageForRelease.OnExit += sMovingOpenStageForRelease_OnExit;

            sOpenStageReleasing.OnEntry += sOpenStagReleasing_OnEntry;
            sOpenStageReleasing.OnExit += sOpenStageReleasing_OnExit;

            sMovingToLPHomeFromOpenStage.OnEntry += sMovingToLPHomeFromOpenStage_OnEntry;
            sMovingToLPHomeFromOpenStage.OnExit += sMovingToHomeFromOpenStage_OnExit;
            #endregion

            #region Barcode Reader
            sMovingToBarcodeReaderClamped.OnEntry += sMovingToBarcodeReaderClamped_OnEntry;
            sMovingToBarcodeReaderClamped.OnExit += sMovingToBarcodeReader_OnExit;

            sWaitingForBarcodeReader.OnEntry += sWaitingForBarcodeReader_OnEntry;
            sWaitingForBarcodeReader.OnExit += sWaitingForBarcodeReader_OnExit;

            sMovingToLPHomeClampedFromBarcodeReader.OnEntry += sMovingToLPHomeClampedFromBarcodeReader_OnEntry;
            sMovingToLPHomeClampedFromBarcodeReader.OnExit += sMovingToLPHomeClampedFromBarcodeReader_OnExit;
            #endregion

            #endregion
            
            #endregion State Register OnEntry OnExit


            

            //--- Exception Transition ---

        }





        #region State OnEntry
        private void sStart_OnEntry(object sender, MacStateEntryEventArgs e)
        {
        }

        private void sLPHome_OnEntry(object sender, MacStateEntryEventArgs e)
        {

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
                    if (timeoutObj.IsTimeOut(thisTime))
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLoadPortA)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            if (!HalMaskTransfer.CheckPosition(@"D:\Positions\MTRobot\LoadPortHome.json"))
                                HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.LoadPortAClamping)
                    {
                        try
                        {
                            var MaskType = (uint)e.Parameter;
                            HalMaskTransfer.Clamp(MaskType);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeClampedFromLoadPortA)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLoadPortAForRelease)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            if (!HalMaskTransfer.CheckPosition(@"D:\Positions\MTRobot\LoadPortHome.json"))
                                HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.LoadPortAReleasing)
                    {
                        try
                        {
                            HalMaskTransfer.Unclamp();
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeFromLoadPortA)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                            HalMaskTransfer.RobotMoving(true);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLoadPortB)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            if (!HalMaskTransfer.CheckPosition(@"D:\Positions\MTRobot\LoadPortHome.json"))
                                HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.LoadPortBClamping)
                    {
                        try
                        {
                            var MaskType = (uint)e.Parameter;
                            HalMaskTransfer.Clamp(MaskType);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeClampedFromLoadPortB)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLoadPortBForRelease)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            if (!HalMaskTransfer.CheckPosition(@"D:\Positions\MTRobot\LoadPortHome.json"))
                                HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.LoadPortBReleasing)
                    {
                        try
                        {
                            HalMaskTransfer.Unclamp();
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeFromLoadPortB)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            if (!HalMaskTransfer.CheckPosition(@"D:\Positions\MTRobot\InspChHome.json"))
                                HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICHomeBackSideToIC.json");
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.InspectionChClamping)
                    {
                        try
                        {
                            var MaskType = (uint)e.Parameter;
                            HalMaskTransfer.Clamp(MaskType);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToICHomeClampedFromInspectionCh)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICBackSideToICHome.json");
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingInspectionChForRelease)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            if (!HalMaskTransfer.CheckPosition(@"D:\Positions\MTRobot\InspChHome.json"))
                                HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICHomeBackSideToIC.json");
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.InspectionChReleasing)
                    {
                        try
                        {
                            HalMaskTransfer.Unclamp();
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToICHomeFromInspectionCh)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICBackSideToICHome.json");
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToInspectionChGlass)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            if (!HalMaskTransfer.CheckPosition(@"D:\Positions\MTRobot\InspChHome.json"))
                                HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.InspectionChGlassClamping)
                    {
                        try
                        {
                            var MaskType = (uint)e.Parameter;
                            HalMaskTransfer.Clamp(MaskType);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToICHomeClampedFromInspectionChGlass)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            if (!HalMaskTransfer.CheckPosition(@"D:\Positions\MTRobot\InspChHome.json"))
                                HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingInspectionChGlassForRelease)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            if (!HalMaskTransfer.CheckPosition(@"D:\Positions\MTRobot\InspChHome.json"))
                                HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                        try
                        {
                            HalMaskTransfer.Unclamp();
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToICHomeFromInspectionChGlass)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            if (!HalMaskTransfer.CheckPosition(@"D:\Positions\MTRobot\InspChHome.json"))
                                HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToOpenStage)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            if (!HalMaskTransfer.CheckPosition(@"D:\Positions\MTRobot\LoadPortHome.json"))
                                HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.OpenStageClamping)
                    {
                        try
                        {
                            var MaskType = (uint)e.Parameter;
                            HalMaskTransfer.Clamp(MaskType);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeClampedFromOpenStage)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingOpenStageForRelease)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            if (!HalMaskTransfer.CheckPosition(@"D:\Positions\MTRobot\LoadPortHome.json"))
                                HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.OpenStageReleasing)
                    {
                        try
                        {
                            HalMaskTransfer.Unclamp();
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeFromOpenStage)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime, 60))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToBarcodeReaderClamped)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            if (!HalMaskTransfer.CheckPosition(@"D:\Positions\MTRobot\LoadPortHome.json"))
                                HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                            // TODO: Robot move path
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
                    }
                    Thread.Sleep(10);
                }
            };
            new Task(guard).Start();
        }

        private void sWaitingForBarcodeReader_OnEntry(Object sender, MacStateEntryEventArgs e)
        {
            var thisState = (MacState)sender;
            DateTime thisTime = DateTime.Now;
            Action guard = () =>
            {
                while (true)
                {
                    if (CurrentWorkState == EnumMacMsMaskTransferState.WaitingForBarcodeReader)
                    {
                        if (false)// TODO: 判斷Barcode Reader已經讀取完等待移走
                        {
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        else
                        {
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Wait));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
                    if (CurrentWorkState == EnumMacMsMaskTransferState.MovingToLPHomeClampedFromBarcodeReader)
                    {
                        try
                        {
                            HalMaskTransfer.RobotMoving(true);
                            // TODO: Robot move path
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Complete));
                            break;
                        }
                        catch (Exception)
                        {
                            HalMaskTransfer.RobotMoving(false);
                            thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.Fail));
                            break;
                        }
                    }
                    if (timeoutObj.IsTimeOut(thisTime))
                    {
                        thisState.DoExit(new MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult.TimeOut));
                        break;
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
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToClampInLoadPortA.ToString()];
                nextState = transition.StateTo;
                // TODO: How to get MaskType
                nextState.DoEntry(new MacStateEntryEventArgs(0));
            }
            else if (args.Result == MacMaskTransferCommonResult.Fail)
            {
                // TODO
                nextState = transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            }
            else if (args.Result == MacMaskTransferCommonResult.TimeOut)
            {
                // TODO
                nextState = transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            }
        }
        private void sMovingToLoadPortB_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToClampInLoadPortB.ToString()];
                nextState = transition.StateTo;
                // TODO: How to get MaskType
                nextState.DoEntry(new MacStateEntryEventArgs(0));
            }
            else if (args.Result == MacMaskTransferCommonResult.Fail)
            {
                // TODO
                nextState = transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            }
            else if (args.Result == MacMaskTransferCommonResult.TimeOut)
            {
                // TODO
                nextState = transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            }
        }
        private void sLoadPortAClamping_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToLPHomeClampedFromLoadPortA.ToString()];
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
        private void sLoadPortBClamping_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToLPHomeClampedFromLoadPortB.ToString()];
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
        private void sMovingToLPHomeClampedFromLoadPortA_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToStandbyAtLPHomeClampedFromLoadPortA.ToString()];
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
        private void sMovingToLPHomeClampedFromLoadPortB_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToStandbyAtLPHomeClampedFromLoadPortB.ToString()];
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
        private void sMovingToLoadPortAForRelease_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToReleaseInLoadPortA.ToString()];
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
        private void sMovingToLoadPortBForRelease_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToReleaseInLoadPortB.ToString()];
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
        private void sLoadPortAReleasing_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToLPHomeFromLoadPortA.ToString()];
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
        private void sLoadPortBReleasing_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToLPHomeFromLoadPortB.ToString()];
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
        private void sMovingToLPHomeFromLoadPortA_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToStandbyAtLPHomeFromLoadPortA.ToString()];
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
        private void sMovingToLPHomeFromLoadPortB_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToStandbyAtLPHomeFromLoadPortB.ToString()];
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
        #endregion

        #region Inspection Chamber
        private void sMovingToInspectionCh_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToClampInInspectionCh.ToString()];
                nextState = transition.StateTo;
                // TODO: How to get MaskType
                nextState.DoEntry(new MacStateEntryEventArgs(0));
            }
            else if (args.Result == MacMaskTransferCommonResult.Fail)
            {
                // TODO
                nextState = transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            }
            else if (args.Result == MacMaskTransferCommonResult.TimeOut)
            {
                // TODO
                nextState = transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            }
        }
        private void sMovingToInspectionChGlass_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToClampInInspectionChGlass.ToString()];
                nextState = transition.StateTo;
                // TODO: How to get MaskType
                nextState.DoEntry(new MacStateEntryEventArgs(0));
            }
            else if (args.Result == MacMaskTransferCommonResult.Fail)
            {
                // TODO
                nextState = transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            }
            else if (args.Result == MacMaskTransferCommonResult.TimeOut)
            {
                // TODO
                nextState = transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            }
        }
        private void sInspectionChClamping_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToICHomeClampedFromInspectionCh.ToString()];
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
        private void sInspectionChGlassClamping_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToICHomeClampedFromInspectionChGlass.ToString()];
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
        private void sMovingToICHomeClampedFromInspectionCh_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToStandbyAtICHomeClampedFromInspectionCh.ToString()];
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
        private void sMovingToICHomeClampedFromInspectionChGlas_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToStandbyAtICHomeClampedFromInspectionChGlass.ToString()];
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
        private void sMovingInspectionChForRelease_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToReleaseInInspectionCh.ToString()];
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
        private void sMovingInspectionChGlassForRelease_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToReleaseInInspectionChGlass.ToString()];
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
        private void sInspectionChReleasing_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToICHomeFromInspectionCh.ToString()];
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
        private void sInspectionChGlassReleasing_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToICHomeFromInspectionChGlass.ToString()];
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
        private void sMovingToICHomeFromInspectionCh_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToStandbyAtICHomeFromInspectionCh.ToString()];
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
        private void sMovingToICHomeFromInspectionChGlass_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToStandbyAtICHomeFromInspectionChGlass.ToString()];
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
        private void sMovingToBarcodeReader_OnExit(Object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToWaitForBarcodeReader.ToString()];
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
        private void sWaitingForBarcodeReader_OnExit(Object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToLPHomeClampedFromBarcodeReader.ToString()];
            }
            else if (args.Result == MacMaskTransferCommonResult.Wait)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToReceiveTriggerAtBarcodeReader.ToString()];
            }
            else if (args.Result == MacMaskTransferCommonResult.TimeOut)
            {
                // TODO
            }
            nextState = transition.StateTo;
            nextState.DoEntry(new MacStateEntryEventArgs(null));
        }
        private void sMovingToLPHomeClampedFromBarcodeReader_OnExit(Object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.Complete)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToStandbyAtLPHomeClampedFromBarcodeReader.ToString()];
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
        #endregion
        
        private void sLPHome_OnExit(object sender, MacStateExitEventArgs e)
        {
            var args = (MacMaskTransferCommonExitEventArgs)e;
            MacTransition transition = null;
            MacState nextState = null;
            if (args.Result == MacMaskTransferCommonResult.ReadyToMoveToLoadPortA)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToLoadPortA.ToString()];
            }
            else if (args.Result == MacMaskTransferCommonResult.ReadyToMoveToLoadPortB)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToLoadPortB.ToString()];
            }
            else if (args.Result == MacMaskTransferCommonResult.ReadyToMoveToInspectionCh)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToInspectionCh.ToString()];
            }
            else if (args.Result == MacMaskTransferCommonResult.ReadyToMoveToOpenStage)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToMoveToOpenStage.ToString()];
            }
            else if (args.Result == MacMaskTransferCommonResult.Wait)
            {
                transition = this.Transitions[EnumMacMsMaskTransferTransition.ReadyToReceiveTriggerAtLPHome.ToString()];
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