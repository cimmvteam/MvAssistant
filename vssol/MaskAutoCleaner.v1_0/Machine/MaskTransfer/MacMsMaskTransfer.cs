using MaskAutoCleaner.v1_0.Machine.CleanCh;
using MaskAutoCleaner.v1_0.Machine.InspectionCh;
using MaskAutoCleaner.v1_0.Machine.LoadPort;
using MaskAutoCleaner.v1_0.Machine.OpenStage;
using MaskAutoCleaner.v1_0.Machine.StateExceptions;
using MaskAutoCleaner.v1_0.Msg;
using MaskAutoCleaner.v1_0.Msg.PrescribedSecs;
using MaskAutoCleaner.v1_0.StateMachineBeta;
using MaskAutoCleaner.v1_0.StateMachineExceptions.MaskTransferStateMachineException;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;
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
        private IMacHalMaskTransfer HalMaskTransfer { get { return (IMacHalMaskTransfer)this.halAssembly; } }
        private IMacHalInspectionCh HalInspectionCh { get { return this.halAssembly as IMacHalInspectionCh; } }

        public MacMsMaskTransfer() { LoadStateMachine(); }

        EnumMacMsMaskTransferState CurrentWorkState { get; set; }

        MacMaskTransferUnitStateTimeOutController timeoutObj = new MacMaskTransferUnitStateTimeOutController();

        public void Initial()
        {
            this.States[EnumMacMsMaskTransferState.Start.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }

        public void MoveToLoadPortAGetMask()
        {
            var transition = Transitions[EnumMacMsMaskTransferTransition.MoveToLoadPortA.ToString()];
            TriggerMember triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                {   // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }

        public void ReleaseToInspectionCh()
        {
            MacTransition transition = null;
            transition = Transitions[EnumMacMsMaskTransferTransition.ChangeDirectionToICHomeClampedFromLPHomeClamped.ToString()];
            TriggerMember triggerMember = null;
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                {   // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);

            transition = Transitions[EnumMacMsMaskTransferTransition.MoveToInspectionChForRelease.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                {   // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);

            transition = Transitions[EnumMacMsMaskTransferTransition.MoveToInspectionCh.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                {  // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);

            transition = Transitions[EnumMacMsMaskTransferTransition.MoveToInspectionChGlassForRelease.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                { // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);

            transition = Transitions[EnumMacMsMaskTransferTransition.MoveToInspectionChGlass.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                { // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);

            transition = Transitions[EnumMacMsMaskTransferTransition.InspectedAtICHomeClamped.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                { // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }
        public void ReleaseToLoadPortA()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsMaskTransferTransition.InspectedAtLPHomeClamped.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                { // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);

            transition = Transitions[EnumMacMsMaskTransferTransition.MoveToLoadPortAInspectedForRelease.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                { // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
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
            MacState sLPHomeInspected = NewState(EnumMacMsMaskTransferState.LPHomeInspected);
            MacState sLPHomeCleaned = NewState(EnumMacMsMaskTransferState.LPHomeCleaned);
            MacState sICHomeClamped = NewState(EnumMacMsMaskTransferState.ICHomeClamped);
            MacState sICHomeInspected = NewState(EnumMacMsMaskTransferState.ICHomeInspected);
            MacState sCCHomeClamped = NewState(EnumMacMsMaskTransferState.CCHomeClamped);
            MacState sCCHomeCleaned = NewState(EnumMacMsMaskTransferState.CCHomeCleaned);

            // Change Direction
            MacState sChangingDirectionToLPHome = NewState(EnumMacMsMaskTransferState.ChangingDirectionToLPHome);
            MacState sChangingDirectionToICHome = NewState(EnumMacMsMaskTransferState.ChangingDirectionToICHome);
            MacState sChangingDirectionToLPHomeClamped = NewState(EnumMacMsMaskTransferState.ChangingDirectionToLPHomeClamped);
            MacState sChangingDirectionToLPHomeInspected = NewState(EnumMacMsMaskTransferState.ChangingDirectionToLPHomeClamped);
            MacState sChangingDirectionToLPHomeCleaned = NewState(EnumMacMsMaskTransferState.ChangingDirectionToLPHomeClamped);
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
            MacState sMovingToCleanCh = NewState(EnumMacMsMaskTransferState.MovingToCleanCh);//前往CleanCh
            MacState sWaitingForMoveToClean = NewState(EnumMacMsMaskTransferState.WaitingForMoveToClean);//準備好Clean
            MacState sMovingToClean = NewState(EnumMacMsMaskTransferState.MovingToClean);
            MacState sWaitingForClean = NewState(EnumMacMsMaskTransferState.WaitingForClean);
            MacState sMovingAfterCleaned = NewState(EnumMacMsMaskTransferState.MovingAfterCleaned);
            MacState sWaitingForMoveToCapture = NewState(EnumMacMsMaskTransferState.WaitingForMoveToCapture);//準備好Capture
            MacState sMovingToCapture = NewState(EnumMacMsMaskTransferState.MovingToCapture);
            MacState sWaitingForCapture = NewState(EnumMacMsMaskTransferState.WaitingForCapture);
            MacState sMovingAfterCaptured = NewState(EnumMacMsMaskTransferState.MovingAfterCaptured);
            MacState sWaitingForLeaveCleanCh = NewState(EnumMacMsMaskTransferState.WaitingForLeaveCleanCh);
            MacState sMovingToCCHomeClampedFromCleanCh = NewState(EnumMacMsMaskTransferState.MovingToCCHomeClampedFromCleanCh);//離開CleanCh

            MacState sMovingToCleanChGlass = NewState(EnumMacMsMaskTransferState.MovingToCleanChGlass);//前往CleanChGlass
            MacState sWaitingForMoveToCleanGlass = NewState(EnumMacMsMaskTransferState.WaitingForMoveToCleanGlass);//準備好CleanGlass
            MacState sMovingToCleanGlass = NewState(EnumMacMsMaskTransferState.MovingToCleanGlass);
            MacState sWaitingForCleanGlass = NewState(EnumMacMsMaskTransferState.WaitingForCleanGlass);
            MacState sMovingAfterCleanedGlass = NewState(EnumMacMsMaskTransferState.MovingAfterCleanedGlass);
            MacState sWaitingForMoveToCaptureGlass = NewState(EnumMacMsMaskTransferState.WaitingForMoveToCaptureGlass);//準備好CaptureGlass
            MacState sMovingToCaptureGlass = NewState(EnumMacMsMaskTransferState.MovingToCaptureGlass);
            MacState sWaitingForCaptureGlass = NewState(EnumMacMsMaskTransferState.WaitingForCaptureGlass);
            MacState sMovingAfterCapturedGlass = NewState(EnumMacMsMaskTransferState.MovingAfterCapturedGlass);
            MacState sWaitingForLeaveCleanChGlass = NewState(EnumMacMsMaskTransferState.WaitingForLeaveCleanChGlass);
            MacState sMovingToCCHomeClampedFromCleanChGlass = NewState(EnumMacMsMaskTransferState.MovingToCCHomeClampedFromCleanChGlass);//離開CleanChGlass

            //Inspect Deform
            MacState sMovingToInspectDeformFromICHome = NewState(EnumMacMsMaskTransferState.MovingToInspectDeform);
            MacState sWaitingForInspectDeform = NewState(EnumMacMsMaskTransferState.WaitingForInspectDeform);
            MacState sMovingToICHomeFromInspectDeform = NewState(EnumMacMsMaskTransferState.MovingToICHomeFromInspectDeform);

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

            #endregion State

            //--- Transition ---
            #region Transition
            MacTransition tStart_DeviceInitial = NewTransition(sStart, sInitial, EnumMacMsMaskTransferTransition.PowerON);
            MacTransition tDeviceInitial_LPHome = NewTransition(sInitial, sLPHome, EnumMacMsMaskTransferTransition.Initial);
            MacTransition tLPHome_NULL = NewTransition(sLPHome, null, EnumMacMsMaskTransferTransition.ReceiveTriggerAtLPHome);
            MacTransition tLPHomeClamped_NULL = NewTransition(sLPHomeClamped, null, EnumMacMsMaskTransferTransition.ReceiveTriggerAtLPHomeClamped);
            MacTransition tLPHomeInspected_NULL = NewTransition(sLPHomeInspected, null, EnumMacMsMaskTransferTransition.ReceiveTriggerAtLPHomeInspected);
            MacTransition tLPHomeCleaned_NULL = NewTransition(sLPHomeInspected, null, EnumMacMsMaskTransferTransition.ReceiveTriggerAtLPHomeCleaned);
            MacTransition tICHome_NULL = NewTransition(sICHome, null, EnumMacMsMaskTransferTransition.ReceiveTriggerAtICHome);
            MacTransition tICHomeClamped_NULL = NewTransition(sICHomeClamped, null, EnumMacMsMaskTransferTransition.ReceiveTriggerAtICHomeClamped);
            MacTransition tICHomeClamped_ICHomeInspected = NewTransition(sICHomeClamped, sICHomeInspected, EnumMacMsMaskTransferTransition.InspectedAtICHomeClamped);
            MacTransition tICHomeInspected_NULL = NewTransition(sICHomeInspected, null, EnumMacMsMaskTransferTransition.ReceiveTriggerAtICHomeInspected);
            MacTransition tICHomeInspected_LPHomeInspected = NewTransition(sICHomeInspected, sLPHomeInspected, EnumMacMsMaskTransferTransition.InspectedAtLPHomeClamped);
            MacTransition tCCHomeClamped_NULL = NewTransition(sCCHomeClamped, null, EnumMacMsMaskTransferTransition.ReceiveTriggerAtCCHomeClamped);
            MacTransition tCCHomeClamped_CCHomeCleaned = NewTransition(sCCHomeClamped, sCCHomeCleaned, EnumMacMsMaskTransferTransition.CleanedAtCCHomeClamped);
            MacTransition tCCHomeCleaned_LPHomeCleaned = NewTransition(sCCHomeCleaned, sLPHomeCleaned, EnumMacMsMaskTransferTransition.CleanedAtLPHomeClamped);

            #region Change Direction
            MacTransition tLPHome_ChangingDirectionToICHome = NewTransition(sLPHome, sChangingDirectionToICHome, EnumMacMsMaskTransferTransition.ChangeDirectionToICHomeFromLPHome);
            MacTransition tICHome_ChangingDirectionToLPHome = NewTransition(sICHome, sChangingDirectionToLPHome, EnumMacMsMaskTransferTransition.ChangeDirectionToLPHomeFromICHome);
            MacTransition tLPHomeClamped_ChangingDirectionToICHomeClamped = NewTransition(sLPHomeClamped, sChangingDirectionToICHomeClamped, EnumMacMsMaskTransferTransition.ChangeDirectionToICHomeClampedFromLPHomeClamped);
            //MacTransition tLPHomeClamped_ChangingDirectionToCCHomeClamped = NewTransition(sLPHomeClamped, sChangingDirectionToCCHomeClamped, EnumMacMsMaskTransferTransition.ChangeDirectionToCCHomeClampedFromLPHomeClamped);
            MacTransition tICHomeClamped_ChangingDirectionToLPHomeClamped = NewTransition(sICHomeClamped, sChangingDirectionToLPHomeClamped, EnumMacMsMaskTransferTransition.ChangeDirectionToLPHomeClampedFromICHomeClamped);
            MacTransition tICHomeInspected_ChangingDirectionToCCHomeClamped = NewTransition(sICHomeInspected, sChangingDirectionToCCHomeClamped, EnumMacMsMaskTransferTransition.ChangeDirectionToCCHomeClampedFromICHomeInspected);
            MacTransition tICHomeInspected_ChangingDirectionToLPHomeInspected = NewTransition(sICHomeInspected, sChangingDirectionToLPHomeInspected, EnumMacMsMaskTransferTransition.ChangeDirectionToLPHomeInspectedFromICHomeInspected);
            MacTransition tCCHomeClamped_ChangingDirectionToLPHomeClamped = NewTransition(sCCHomeClamped, sChangingDirectionToLPHomeClamped, EnumMacMsMaskTransferTransition.ChangeDirectionToLPHomeClampedFromCCHomeClamped);
            MacTransition tCCHomeClamped_ChangingDirectionToICHomeClamped = NewTransition(sCCHomeClamped, sChangingDirectionToICHomeClamped, EnumMacMsMaskTransferTransition.ChangeDirectionToICHomeClampedFromCCHomeClamped);
            MacTransition tCCHomeCleaned_ChangingDirectionToLPHomeCleaned = NewTransition(sCCHomeCleaned, sChangingDirectionToLPHomeCleaned, EnumMacMsMaskTransferTransition.ChangeDirectionToLPHomeCleanedFromCCHomeCleaned);
            MacTransition tChangingDirectionToICHome_ICHome = NewTransition(sChangingDirectionToICHome, sICHome, EnumMacMsMaskTransferTransition.FinishChangeDirectionToICHome);
            MacTransition tChangingDirectionToLPHome_LPHome = NewTransition(sChangingDirectionToLPHome, sLPHome, EnumMacMsMaskTransferTransition.FinishChangeDirectionToLPHome);
            MacTransition tChangingDirectionToICHomeClamped_ICHomeClamped = NewTransition(sChangingDirectionToICHomeClamped, sICHomeClamped, EnumMacMsMaskTransferTransition.FinishChangeDirectionToICHomeClamped);
            MacTransition tChangingDirectionToLPHomeClamped_LPHomeClamped = NewTransition(sChangingDirectionToLPHomeClamped, sLPHomeClamped, EnumMacMsMaskTransferTransition.FinishChangeDirectionToLPHomeClamped);
            MacTransition tChangingDirectionToLPHomeInspected_LPHomeInspected = NewTransition(sChangingDirectionToLPHomeInspected, sLPHomeInspected, EnumMacMsMaskTransferTransition.FinishChangeDirectionToLPHomeInspected);
            MacTransition tChangingDirectionToLPHomeCleaned_LPHomeCleaned = NewTransition(sChangingDirectionToLPHomeCleaned, sLPHomeCleaned, EnumMacMsMaskTransferTransition.FinishChangeDirectionToLPHomeCleaned);
            MacTransition tChangingDirectionToCCHomeClamped_CCHomeClamped = NewTransition(sChangingDirectionToCCHomeClamped, sCCHomeClamped, EnumMacMsMaskTransferTransition.FinishChangeDirectionToCCHomeClamped);
            #endregion Change Direction

            #region Load Port A
            MacTransition tLPHome_MovingToLoadPortA = NewTransition(sLPHome, sMovingToLoadPortA, EnumMacMsMaskTransferTransition.MoveToLoadPortA);
            MacTransition tMovingToLoadPortA_LoadPortAClamping = NewTransition(sMovingToLoadPortA, sLoadPortAClamping, EnumMacMsMaskTransferTransition.ClampInLoadPortA);
            MacTransition tLoadPortAClamping_MovingToLPHomeClampedFromLoadPortA = NewTransition(sLoadPortAClamping, sMovingToLPHomeClampedFromLoadPortA, EnumMacMsMaskTransferTransition.MoveToLPHomeClampedFromLoadPortA);
            MacTransition tMovingToLPHomeClampedFromLoadPortA_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromLoadPortA, sLPHomeClamped, EnumMacMsMaskTransferTransition.StandbyAtLPHomeClampedFromLoadPortA);

            MacTransition tLPHomeInspected_MovingToLoadPortAForRelease = NewTransition(sLPHomeInspected, sMovingToLoadPortAForRelease, EnumMacMsMaskTransferTransition.MoveToLoadPortAInspectedForRelease);
            MacTransition tLPHomeCleaned_MovingToLoadPortAForRelease = NewTransition(sLPHomeCleaned, sMovingToLoadPortAForRelease, EnumMacMsMaskTransferTransition.MoveToLoadPortACleanedForRelease);
            MacTransition tMovingToLoadPortAForRelease_LoadPortAReleasing = NewTransition(sMovingToLoadPortAForRelease, sLoadPortAReleasing, EnumMacMsMaskTransferTransition.ReleaseInLoadPortA);
            MacTransition tLoadPortAReleasing_MovingToLPHomeFromLoadPortA = NewTransition(sLoadPortAReleasing, sMovingToLPHomeFromLoadPortA, EnumMacMsMaskTransferTransition.MoveToLPHomeFromLoadPortA);
            MacTransition tMovingToLPHomeFromLoadPortA_LPHome = NewTransition(sMovingToLPHomeFromLoadPortA, sLPHome, EnumMacMsMaskTransferTransition.StandbyAtLPHomeFromLoadPortA);
            #endregion Load Port A

            #region Load Port B
            MacTransition tLPHome_MovingToLoadPortB = NewTransition(sLPHome, sMovingToLoadPortB, EnumMacMsMaskTransferTransition.MoveToLoadPortB);
            MacTransition tMovingToLoadPortB_LoadPortBClamping = NewTransition(sMovingToLoadPortB, sLoadPortBClamping, EnumMacMsMaskTransferTransition.ToClampInLoadPortB);
            MacTransition tLoadPortBClamping_MovingToLPHomeClampedFromLoadPortB = NewTransition(sLoadPortBClamping, sMovingToLPHomeClampedFromLoadPortB, EnumMacMsMaskTransferTransition.MoveToLPHomeClampedFromLoadPortB);
            MacTransition tMovingToLPHomeClampedFromLoadPortB_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromLoadPortB, sLPHomeClamped, EnumMacMsMaskTransferTransition.StandbyAtLPHomeClampedFromLoadPortB);

            MacTransition tLPHomeInspected_MovingToLoadPortBForRelease = NewTransition(sLPHomeInspected, sMovingToLoadPortBForRelease, EnumMacMsMaskTransferTransition.MoveToLoadPortBInspectedForRelease);
            MacTransition tLPHomeCleaned_MovingToLoadPortBForRelease = NewTransition(sLPHomeCleaned, sMovingToLoadPortBForRelease, EnumMacMsMaskTransferTransition.MoveToLoadPortBCleanedForRelease);
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
            MacTransition tMovingInspectionChForRelease_InspectionChReleasing = NewTransition(sMovingToInspectionChForRelease, sInspectionChReleasing, EnumMacMsMaskTransferTransition.ReleaseInInspectionCh);
            MacTransition tInspectionChReleasing_MovingToICHomeFromInspectionCh = NewTransition(sInspectionChReleasing, sMovingToICHomeFromInspectionCh, EnumMacMsMaskTransferTransition.MoveToICHomeFromInspectionCh);
            MacTransition tMovingToICHomeFromInspectionCh_ICHome = NewTransition(sMovingToICHomeFromInspectionCh, sICHome, EnumMacMsMaskTransferTransition.StandbyAtICHomeFromInspectionCh);

            MacTransition tICHome_MovingToInspectionChGlass = NewTransition(sICHome, sMovingToInspectionChGlass, EnumMacMsMaskTransferTransition.MoveToInspectionChGlass);
            MacTransition tMovingToInspectionChGlass_InspectionChGlassClamping = NewTransition(sMovingToInspectionChGlass, sInspectionChGlassClamping, EnumMacMsMaskTransferTransition.ClampInInspectionChGlass);
            MacTransition tInspectionChGlassClamping_MovingToICHomeClampedFromInspectionChGlass = NewTransition(sInspectionChGlassClamping, sMovingToICHomeClampedFromInspectionChGlass, EnumMacMsMaskTransferTransition.MoveToICHomeClampedFromInspectionChGlass);
            MacTransition tMovingToICHomeClampedFromInspectionChGlass_ICHomeClamped = NewTransition(sMovingToICHomeClampedFromInspectionChGlass, sICHomeClamped, EnumMacMsMaskTransferTransition.StandbyAtICHomeClampedFromInspectionChGlass);
            MacTransition tICHomeClamped_MovingToInspectionChGlassForRelease = NewTransition(sICHomeClamped, sMovingToInspectionChGlassForRelease, EnumMacMsMaskTransferTransition.MoveToInspectionChGlassForRelease);
            MacTransition tMovingInspectionChGlassForRelease_InspectionChGlassReleasing = NewTransition(sMovingToInspectionChGlassForRelease, sInspectionChGlassReleasing, EnumMacMsMaskTransferTransition.ReleaseInInspectionChGlass);
            MacTransition tInspectionChGlassReleasing_MovingToICHomeFromInspectionChGlass = NewTransition(sInspectionChGlassReleasing, sMovingToICHomeFromInspectionChGlass, EnumMacMsMaskTransferTransition.MoveToICHomeFromInspectionChGlass);
            MacTransition tMovingToICHomeFromInspectionChGlass_ICHome = NewTransition(sMovingToICHomeFromInspectionChGlass, sICHome, EnumMacMsMaskTransferTransition.StandbyAtICHomeFromInspectionChGlass);
            #endregion Inspection Ch

            #region Clean Ch
            MacTransition tCCHomeClamped_MovingToCleanCh = NewTransition(sCCHomeClamped, sMovingToCleanCh, EnumMacMsMaskTransferTransition.MoveToCleanCh);
            MacTransition tMovingToCleanCh_WaitingForMoveToClean = NewTransition(sMovingToCleanCh, sWaitingForMoveToClean, EnumMacMsMaskTransferTransition.WaitForMoveToClean);
            MacTransition tWaitingForMoveToClean_MovingToClean = NewTransition(sWaitingForMoveToClean, sMovingToClean, EnumMacMsMaskTransferTransition.MoveToClean);
            MacTransition tMovingToClean_WaitingForClean = NewTransition(sMovingToClean, sWaitingForClean, EnumMacMsMaskTransferTransition.WaitFroClean);
            MacTransition tWaitingForClean_NULL = NewTransition(sWaitingForClean, null, EnumMacMsMaskTransferTransition.ReceiveTriggerAtClean);
            MacTransition tWaitingForClean_MovingAfterCleaned = NewTransition(sWaitingForClean, sMovingAfterCleaned, EnumMacMsMaskTransferTransition.MoveAferCleaned);
            MacTransition tMovingAfterCleaned_WaitingForMoveToCapture = NewTransition(sMovingAfterCleaned, sWaitingForMoveToCapture, EnumMacMsMaskTransferTransition.WaitForMoveToCapture);
            MacTransition tWaitingForMoveToCapture_MovingToCapture = NewTransition(sWaitingForMoveToCapture, sMovingToCapture, EnumMacMsMaskTransferTransition.MoveToCapture);
            MacTransition tMovingToCapture_WaitingForCapture = NewTransition(sMovingToCapture, sWaitingForCapture, EnumMacMsMaskTransferTransition.WaitForCapture);
            MacTransition tWaitingForCapture_NULL = NewTransition(sWaitingForCapture, null, EnumMacMsMaskTransferTransition.ReceiveTriggerAtCapture);
            MacTransition tWaitingForCapture_MovingAfterCaptured = NewTransition(sWaitingForCapture, sMovingAfterCaptured, EnumMacMsMaskTransferTransition.MoveAfterCapture);
            MacTransition tMovingAfterCaptured_WaitingForLeaveCleanCh = NewTransition(sMovingAfterCaptured, sWaitingForLeaveCleanCh, EnumMacMsMaskTransferTransition.WaitForLeaveCleanCh);
            MacTransition tWaitingForLeaveCleanCh_MovingToCCHomeClampedFromCleanCh = NewTransition(sWaitingForLeaveCleanCh, sMovingToCCHomeClampedFromCleanCh, EnumMacMsMaskTransferTransition.MoveToCCHomeClampedFromCleanCh);
            MacTransition tMovingToCCHomeClampedFromCleanCh_CCHomeClamped = NewTransition(sMovingToCCHomeClampedFromCleanCh, sCCHomeClamped, EnumMacMsMaskTransferTransition.StandbyAtCCHomeClampedFromCleanCh);

            MacTransition tCCHomeClamped_MovingToCleanChGlass = NewTransition(sCCHomeClamped, sMovingToCleanChGlass, EnumMacMsMaskTransferTransition.MoveToCleanChGlass);
            MacTransition tMovingToCleanChGlass_WaitingForMoveToCleanGlass = NewTransition(sMovingToCleanChGlass, sWaitingForMoveToCleanGlass, EnumMacMsMaskTransferTransition.WaitForMoveToCleanGlass);
            MacTransition tWaitingForMoveToCleanGlass_MovingToCleanGlass = NewTransition(sWaitingForMoveToCleanGlass, sMovingToCleanGlass, EnumMacMsMaskTransferTransition.MoveToCleanGlass);
            MacTransition tMovingToCleanGlass_WaitingForCleanGlass = NewTransition(sMovingToCleanGlass, sWaitingForCleanGlass, EnumMacMsMaskTransferTransition.WaitFroCleanGlass);
            MacTransition tWaitingForCleanGlass_NULL = NewTransition(sWaitingForCleanGlass, null, EnumMacMsMaskTransferTransition.ReceiveTriggerAtCleanGlass);
            MacTransition tWaitingForCleanGlass_MovingAfterCleanedGlass = NewTransition(sWaitingForCleanGlass, sMovingAfterCleanedGlass, EnumMacMsMaskTransferTransition.MoveAferCleanedGlass);
            MacTransition tMovingAfterCleanedGlass_WaitingForMoveToCaptureGlass = NewTransition(sMovingAfterCleanedGlass, sWaitingForMoveToCaptureGlass, EnumMacMsMaskTransferTransition.WaitForMoveToCaptureGlass);
            MacTransition tWaitingForMoveToCaptureGlass_MovingToCaptureGlass = NewTransition(sWaitingForMoveToCaptureGlass, sMovingToCaptureGlass, EnumMacMsMaskTransferTransition.MoveToCaptureGlass);
            MacTransition tMovingToCaptureGlass_WaitingForCaptureGlass = NewTransition(sMovingToCaptureGlass, sWaitingForCaptureGlass, EnumMacMsMaskTransferTransition.WaitForCaptureGlass);
            MacTransition tWaitingForCaptureGlass_NULL = NewTransition(sWaitingForCaptureGlass, null, EnumMacMsMaskTransferTransition.ReceiveTriggerAtCaptureGlass);
            MacTransition tWaitingForCaptureGlass_MovingAfterCapturedGlass = NewTransition(sWaitingForCaptureGlass, sMovingAfterCapturedGlass, EnumMacMsMaskTransferTransition.MoveAfterCapturedGlass);
            MacTransition tMovingAfterCapturedGlass_WaitingForLeaveCleanChGlass = NewTransition(sMovingAfterCapturedGlass, sWaitingForLeaveCleanChGlass, EnumMacMsMaskTransferTransition.WaitForLeaveCleanChGlass);
            MacTransition tWaitingForLeaveCleanChGlass_MovingToCCHomeClampedFromCleanChGlass = NewTransition(sWaitingForLeaveCleanChGlass, sMovingToCCHomeClampedFromCleanChGlass, EnumMacMsMaskTransferTransition.MoveToCCHomeClampedFromCleanChGlass);
            MacTransition tMovingToCCHomeClampedFromCleanChGlass_CCHomeClamped = NewTransition(sMovingToCCHomeClampedFromCleanChGlass, sCCHomeClamped, EnumMacMsMaskTransferTransition.StandbyAtCCHomeClampedFromCleanChGlass);

            #endregion Clean Ch

            #region Open Stage
            MacTransition tLPHome_MovingToOpenStage = NewTransition(sLPHome, sMovingToOpenStage, EnumMacMsMaskTransferTransition.MoveToOpenStage);
            MacTransition tMovingToOpenStage_OpenStageClamping = NewTransition(sMovingToOpenStage, sOpenStageClamping, EnumMacMsMaskTransferTransition.ClampInOpenStage);
            MacTransition tOpenStageClamping_MovingToLPHomeClampedFromOpenStage = NewTransition(sOpenStageClamping, sMovingToLPHomeClampedFromOpenStage, EnumMacMsMaskTransferTransition.MoveToLPHomeClampedFromOpenStage);
            MacTransition tMovingToLPHomeClampedFromOpenStage_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromOpenStage, sLPHomeClamped, EnumMacMsMaskTransferTransition.StandbyAtLPHomeClampedFromOpenStage);

            MacTransition tLPHomeInspected_MovingToOpenStageForRelease = NewTransition(sLPHomeInspected, sMovingOpenStageForRelease, EnumMacMsMaskTransferTransition.MoveToOpenStageForRelease);
            MacTransition tLPHomeCleaned_MovingToOpenStageForRelease = NewTransition(sLPHomeCleaned, sMovingOpenStageForRelease, EnumMacMsMaskTransferTransition.MoveToOpenStageForRelease);
            MacTransition tMovingOpenStageForRelease_OpenStageReleasing = NewTransition(sMovingOpenStageForRelease, sOpenStageReleasing, EnumMacMsMaskTransferTransition.ReleaseInOpenStage);
            MacTransition tOpenStageReleasing_MovingToLPHomeFromOpenStage = NewTransition(sOpenStageReleasing, sMovingToLPHomeFromOpenStage, EnumMacMsMaskTransferTransition.CompleteReleased);
            MacTransition tMovingToLPHomeFromOpenStage_LPHome = NewTransition(sMovingToLPHomeFromOpenStage, sLPHome, EnumMacMsMaskTransferTransition.StandbyAtLPHomeFromOpenStage);
            #endregion Open Stage

            #region Barcode Reader
            MacTransition tLPHomeClamped_MovingToBarcodeReaderClamped = NewTransition(sLPHomeClamped, sMovingToBarcodeReaderClamped, EnumMacMsMaskTransferTransition.MoveToBarcodeReaderClamped);
            MacTransition tMovingToBarcodeReaderClamped_WaitingForBarcodeReader = NewTransition(sMovingToBarcodeReaderClamped, sWaitingForBarcodeReader, EnumMacMsMaskTransferTransition.WaitForBarcodeReader);
            MacTransition tWaitingForBarcodeReader_NULL = NewTransition(sWaitingForBarcodeReader, null, EnumMacMsMaskTransferTransition.ReceiveTriggerAtBarcodeReader);
            MacTransition tWaitingForBarcodeReader_MovingToLPHomeClampedFromBarcodeReader = NewTransition(sWaitingForBarcodeReader, sMovingToLPHomeClampedFromBarcodeReader, EnumMacMsMaskTransferTransition.MoveToLPHomeClampedFromBarcodeReader);
            MacTransition tMovingToLPHomeClampedFromBarcodeReader_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromBarcodeReader, sLPHomeClamped, EnumMacMsMaskTransferTransition.StandbyAtLPHomeClampedFromBarcodeReader);
            #endregion Barcode Reader

            #region Inspect Deform
            MacTransition tICHome_MovingToInspectDeformFromICHome = NewTransition(sICHome, sMovingToInspectDeformFromICHome, EnumMacMsMaskTransferTransition.MoveToInspectDeformFromICHome);
            MacTransition tMovingToInspectDeformFromICHome_WaitingForInspectDeform = NewTransition(sMovingToInspectDeformFromICHome, sWaitingForInspectDeform, EnumMacMsMaskTransferTransition.WaitForInspectDeform);
            MacTransition tWaitingForInspectDeform_NULL = NewTransition(sWaitingForInspectDeform, null, EnumMacMsMaskTransferTransition.ReceiveTriggerAtInspectDeform);
            MacTransition tWaitingForInspectDeform_MovingToICHomeFromInspectDeform = NewTransition(sWaitingForInspectDeform, sMovingToICHomeFromInspectDeform, EnumMacMsMaskTransferTransition.MoveToICHomeFromInspectDeform);
            MacTransition tMovingToICHomeFromInspectDeform_ICHome = NewTransition(sMovingToICHomeFromInspectDeform, sICHome, EnumMacMsMaskTransferTransition.StandbyAtICHomeFromInspectDeform);
            #endregion Inspect Deform

            //Is Ready to Release
            //MacTransition tLPHomeClamped_ReadyToRelease = NewTransition(sLPHomeClamped, sReadyToRelease, EnumMacMsMaskTransferTransition.IsReady);


            //--- Clean Start



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
                var transition = tStart_DeviceInitial;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sStart.OnExit += (sender, e) =>
            { };

            sInitial.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.Initial();
                }
                catch (Exception ex)
                {
                    throw new MaskTransferInitialFailException(ex.Message);
                }

                var transition = tDeviceInitial_LPHome;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sInitial.OnExit += (sender, e) =>
            { };

            sLPHome.OnEntry += (sender, e) =>
            {
                var transition = tLPHome_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sLPHome.OnExit += (sender, e) =>
            { };

            sLPHomeClamped.OnEntry += (sender, e) =>
            {
                var transition = tLPHomeClamped_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sLPHomeClamped.OnExit += (sender, e) =>
            { };

            sLPHomeInspected.OnEntry += (sender, e) =>
            {
                var transition = tLPHomeInspected_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sLPHomeInspected.OnExit += (sender, e) =>
            { };

            sLPHomeCleaned.OnEntry += (sender, e) =>
            {
                var transition = tLPHomeCleaned_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sLPHomeCleaned.OnExit += (sender, e) =>
            { };

            sICHome.OnEntry += (sender, e) =>
            {
                var transition = tICHome_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sICHome.OnExit += (sender, e) =>
            { };

            sICHomeClamped.OnEntry += (sender, e) =>
            {
                var transition = tICHomeClamped_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sICHomeClamped.OnExit += (sender, e) =>
            { };

            sICHomeInspected.OnEntry += (sender, e) =>
            {
                var transition = tICHomeInspected_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sICHomeInspected.OnExit += (sender, e) =>
            { };

            sCCHomeClamped.OnEntry += (sender, e) =>
            {
                var transition = tCCHomeClamped_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sCCHomeClamped.OnExit += (sender, e) =>
            { };

            #region Change Direction
            sChangingDirectionToLPHome.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tChangingDirectionToLPHome_LPHome;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sChangingDirectionToLPHome.OnExit += (sender, e) =>
            { };

            sChangingDirectionToLPHomeClamped.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tChangingDirectionToLPHomeClamped_LPHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sChangingDirectionToLPHomeClamped.OnExit += (sender, e) =>
            { };

            sChangingDirectionToLPHomeInspected.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tChangingDirectionToLPHomeInspected_LPHomeInspected;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sChangingDirectionToLPHomeInspected.OnExit += (sender, e) =>
            { };

            sChangingDirectionToLPHomeCleaned.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tChangingDirectionToLPHomeCleaned_LPHomeCleaned;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sChangingDirectionToLPHomeCleaned.OnExit += (sender, e) =>
            { };

            sChangingDirectionToICHome.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tChangingDirectionToICHome_ICHome;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sChangingDirectionToICHome.OnExit += (sender, e) =>
            { };

            sChangingDirectionToICHomeClamped.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tChangingDirectionToICHomeClamped_ICHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sChangingDirectionToICHomeClamped.OnExit += (sender, e) =>
            { };

            sChangingDirectionToCCHomeClamped.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ChangeDirection(@"D:\Positions\MTRobot\CleanChHome.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tChangingDirectionToCCHomeClamped_CCHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sChangingDirectionToCCHomeClamped.OnExit += (sender, e) =>
            { };
            #endregion Change Direction

            #region Load PortA
            sMovingToLoadPortA.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLoadPortA_LoadPortAClamping;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToLoadPortA.OnExit += (sender, e) =>
            { };

            sLoadPortAClamping.OnEntry += (sender, e) =>
            {
                try
                {
                    var MaskType = (uint)e.Parameter;
                    HalMaskTransfer.Clamp(MaskType);
                    HalMaskTransfer.Clamp(MaskType);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tLoadPortAClamping_MovingToLPHomeClampedFromLoadPortA;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sLoadPortAClamping.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeClampedFromLoadPortA.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLPHomeClampedFromLoadPortA_LPHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToLPHomeClampedFromLoadPortA.OnExit += (sender, e) =>
            { };

            sMovingToLoadPortAForRelease.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLoadPortAForRelease_LoadPortAReleasing;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToLoadPortAForRelease.OnExit += (sender, e) =>
            { };

            sLoadPortAReleasing.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tLoadPortAReleasing_MovingToLPHomeFromLoadPortA;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sLoadPortAReleasing.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeFromLoadPortA.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                    HalMaskTransfer.RobotMoving(true);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLPHomeFromLoadPortA_LPHome;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToLPHomeFromLoadPortA.OnExit += (sender, e) =>
            { };
            #endregion Load PortA

            #region Load PortB
            sMovingToLoadPortB.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLoadPortB_LoadPortBClamping;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToLoadPortB.OnExit += (sender, e) =>
            { };

            sLoadPortBClamping.OnEntry += (sender, e) =>
            {
                try
                {
                    var MaskType = (uint)e.Parameter;
                    HalMaskTransfer.Clamp(MaskType);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tLoadPortBClamping_MovingToLPHomeClampedFromLoadPortB;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sLoadPortBClamping.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeClampedFromLoadPortB.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLPHomeClampedFromLoadPortB_LPHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToLPHomeClampedFromLoadPortB.OnExit += (sender, e) =>
            { };

            sMovingToLoadPortBForRelease.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLoadPortBForRelease_LoadPortBReleasing;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToLoadPortBForRelease.OnExit += (sender, e) =>
            { };

            sLoadPortBReleasing.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tLoadPortBReleasing_MovingToLPHomeFromLoadPortB;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sLoadPortBReleasing.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeFromLoadPortB.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLPHomeFromLoadPortB_LPHome;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToLPHomeFromLoadPortB.OnExit += (sender, e) =>
            { };
            #endregion Load PortB

            #region Inspection Ch
            sMovingToInspectionCh.OnEntry += (sender, e) =>
            {
                try
                {
                    if (!HalInspectionCh.ReadRobotIntrude(true))
                        throw new MaskTransferPathMoveFailException("Inspection Chamber not allowed to intrude !");
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICHomeBackSideToIC.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToInspectionCh_InspectionChClamping;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToInspectionCh.OnExit += (sender, e) =>
            { };

            sInspectionChClamping.OnEntry += (sender, e) =>
            {
                try
                {
                    var MaskType = (uint)e.Parameter;
                    HalMaskTransfer.Clamp(MaskType);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tInspectionChClamping_MovingToICHomeClampedFromInspectionCh;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sInspectionChClamping.OnExit += (sender, e) =>
            { };

            sMovingToICHomeClampedFromInspectionCh.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICBackSideToICHome.json");
                    HalMaskTransfer.RobotMoving(false);
                    HalInspectionCh.ReadRobotIntrude(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToICHomeClampedFromInspectionCh_ICHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToICHomeClampedFromInspectionCh.OnExit += (sender, e) =>
            { };

            sMovingToInspectionChForRelease.OnEntry += (sender, e) =>
            {
                try
                {
                    if (!HalInspectionCh.ReadRobotIntrude(true))
                        throw new MaskTransferPathMoveFailException("Inspection Chamber not allowed to intrude !");
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICHomeBackSideToIC.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingInspectionChForRelease_InspectionChReleasing;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToInspectionChForRelease.OnExit += (sender, e) =>
            { };

            sInspectionChReleasing.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tInspectionChReleasing_MovingToICHomeFromInspectionCh;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sInspectionChReleasing.OnExit += (sender, e) =>
            { };

            sMovingToICHomeFromInspectionCh.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICBackSideToICHome.json");
                    HalMaskTransfer.RobotMoving(false);
                    HalInspectionCh.ReadRobotIntrude(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToICHomeFromInspectionCh_ICHome;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToICHomeFromInspectionCh.OnExit += (sender, e) =>
            { };



            sMovingToInspectionChGlass.OnEntry += (sender, e) =>
            {
                try
                {
                    if (!HalInspectionCh.ReadRobotIntrude(true))
                        throw new MaskTransferPathMoveFailException("Inspection Chamber not allowed to intrude !");
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToInspectionChGlass_InspectionChGlassClamping;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToInspectionChGlass.OnExit += (sender, e) =>
            { };

            sInspectionChGlassClamping.OnEntry += (sender, e) =>
            {
                try
                {
                    var MaskType = (uint)e.Parameter;
                    HalMaskTransfer.Clamp(MaskType);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tInspectionChGlassClamping_MovingToICHomeClampedFromInspectionChGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sInspectionChGlassClamping.OnExit += (sender, e) =>
            { };

            sMovingToICHomeClampedFromInspectionChGlass.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                    HalMaskTransfer.RobotMoving(false);
                    HalInspectionCh.ReadRobotIntrude(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToICHomeClampedFromInspectionChGlass_ICHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToICHomeClampedFromInspectionChGlass.OnExit += (sender, e) =>
            { };

            sMovingToInspectionChGlassForRelease.OnEntry += (sender, e) =>
            {
                try
                {
                    if (!HalInspectionCh.ReadRobotIntrude(true))
                        throw new MaskTransferPathMoveFailException("Inspection Chamber not allowed to intrude !");
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingInspectionChGlassForRelease_InspectionChGlassReleasing;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToInspectionChGlassForRelease.OnExit += (sender, e) =>
            { };

            sInspectionChGlassReleasing.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tInspectionChGlassReleasing_MovingToICHomeFromInspectionChGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sInspectionChGlassReleasing.OnExit += (sender, e) =>
            { };

            sMovingToICHomeFromInspectionChGlass.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                    HalMaskTransfer.RobotMoving(false);
                    HalInspectionCh.ReadRobotIntrude(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToICHomeFromInspectionChGlass_ICHome;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToICHomeFromInspectionChGlass.OnExit += (sender, e) =>
            { };
            #endregion Inspection Ch

            #region Clean Ch
            sMovingToCleanCh.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\CCHomeFrontSideToCC.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCleanCh_WaitingForMoveToClean;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToCleanCh.OnExit += (sender, e) =>
            { };

            sWaitingForMoveToClean.OnEntry += (sender, e) =>
            {
                try
                {

                }
                catch (Exception ex)
                {
                    throw new MaskTransferException(ex.Message);
                }

                var transition = tWaitingForMoveToClean_MovingToClean;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sWaitingForMoveToClean.OnExit += (sender, e) =>
            { };

            sMovingToClean.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\CCFrontSideToClean.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToClean_WaitingForClean;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToClean.OnExit += (sender, e) =>
            { };

            sWaitingForClean.OnEntry += (sender, e) =>
            {
                try
                {

                }
                catch (Exception ex)
                {
                    throw new MaskTransferException(ex.Message);
                }

                var transition = tWaitingForClean_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sWaitingForClean.OnExit += (sender, e) =>
            { };

            sMovingAfterCleaned.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\FrontSideCleanFinishToCC.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingAfterCleaned_WaitingForMoveToCapture;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingAfterCleaned.OnExit += (sender, e) =>
            { };

            sWaitingForMoveToCapture.OnEntry += (sender, e) =>
            {
                try
                {

                }
                catch (Exception ex)
                {
                    throw new MaskTransferException(ex.Message);
                }

                var transition = tWaitingForMoveToCapture_MovingToCapture;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sWaitingForMoveToCapture.OnExit += (sender, e) =>
            { };

            sMovingToCapture.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\CCFrontSideToCapture.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCapture_WaitingForCapture;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToCapture.OnExit += (sender, e) =>
            { };

            sWaitingForCapture.OnEntry += (sender, e) =>
            {
                try
                {

                }
                catch (Exception ex)
                {
                    throw new MaskTransferException(ex.Message);
                }

                var transition = tWaitingForCapture_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sWaitingForCapture.OnExit += (sender, e) =>
            { };

            sMovingAfterCaptured.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\FrontSideCaptureFinishToCC.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingAfterCaptured_WaitingForLeaveCleanCh;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingAfterCaptured.OnExit += (sender, e) =>
            { };

            sWaitingForLeaveCleanCh.OnEntry += (sender, e) =>
            {
                try
                {

                }
                catch (Exception ex)
                {
                    throw new MaskTransferException(ex.Message);
                }

                var transition = tWaitingForLeaveCleanCh_MovingToCCHomeClampedFromCleanCh;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sWaitingForLeaveCleanCh.OnExit += (sender, e) =>
            { };

            sMovingToCCHomeClampedFromCleanCh.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\CCFrontSideToCCHome.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCCHomeClampedFromCleanCh_CCHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToCCHomeClampedFromCleanCh.OnExit += (sender, e) =>
            { };



            sMovingToCleanChGlass.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\CCHomeBackSideToCC.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCleanChGlass_WaitingForMoveToCleanGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToCleanChGlass.OnExit += (sender, e) =>
            { };

            sWaitingForMoveToCleanGlass.OnEntry += (sender, e) =>
            {
                try
                {

                }
                catch (Exception ex)
                {
                    throw new MaskTransferException(ex.Message);
                }

                var transition = tWaitingForMoveToCleanGlass_MovingToCleanGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sWaitingForMoveToCleanGlass.OnExit += (sender, e) =>
            { };

            sMovingToCleanGlass.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\CCBackSideToClean.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCleanGlass_WaitingForCleanGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToCleanGlass.OnExit += (sender, e) =>
            { };

            sWaitingForCleanGlass.OnEntry += (sender, e) =>
            {
                try
                {

                }
                catch (Exception ex)
                {
                    throw new MaskTransferException(ex.Message);
                }

                var transition = tWaitingForCleanGlass_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sWaitingForCleanGlass.OnExit += (sender, e) =>
            { };

            sMovingAfterCleanedGlass.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\BackSideCleanFinishToCC.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingAfterCleanedGlass_WaitingForMoveToCaptureGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingAfterCleanedGlass.OnExit += (sender, e) =>
            { };

            sWaitingForMoveToCaptureGlass.OnEntry += (sender, e) =>
            {
                try
                {

                }
                catch (Exception ex)
                {
                    throw new MaskTransferException(ex.Message);
                }

                var transition = tWaitingForMoveToCaptureGlass_MovingToCaptureGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sWaitingForMoveToCaptureGlass.OnExit += (sender, e) =>
            { };

            sMovingToCaptureGlass.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\CCBackSideToCapture.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCaptureGlass_WaitingForCaptureGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToCaptureGlass.OnExit += (sender, e) =>
            { };

            sWaitingForCaptureGlass.OnEntry += (sender, e) =>
            {
                try
                {

                }
                catch (Exception ex)
                {
                    throw new MaskTransferException(ex.Message);
                }

                var transition = tWaitingForCaptureGlass_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sWaitingForCaptureGlass.OnExit += (sender, e) =>
            { };

            sMovingAfterCapturedGlass.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\BackSideCaptureFinishToCC.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingAfterCapturedGlass_WaitingForLeaveCleanChGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingAfterCapturedGlass.OnExit += (sender, e) =>
            { };

            sWaitingForLeaveCleanChGlass.OnEntry += (sender, e) =>
            {
                try
                {

                }
                catch (Exception ex)
                {
                    throw new MaskTransferException(ex.Message);
                }

                var transition = tWaitingForLeaveCleanChGlass_MovingToCCHomeClampedFromCleanChGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sWaitingForLeaveCleanChGlass.OnExit += (sender, e) =>
            { };

            sMovingToCCHomeClampedFromCleanChGlass.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\FrontSideCaptureFinishToCC.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCCHomeClampedFromCleanChGlass_CCHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToCCHomeClampedFromCleanChGlass.OnExit += (sender, e) =>
            { };
            #endregion Clean Ch

            #region OpenStage
            sMovingToOpenStage.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToOpenStage_OpenStageClamping;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToOpenStage.OnExit += (sender, e) =>
            { };

            sOpenStageClamping.OnEntry += (sender, e) =>
            {
                try
                {
                    var MaskType = (uint)e.Parameter;
                    HalMaskTransfer.Clamp(MaskType);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tOpenStageClamping_MovingToLPHomeClampedFromOpenStage;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sOpenStageClamping.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeClampedFromOpenStage.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLPHomeClampedFromOpenStage_LPHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToLPHomeClampedFromOpenStage.OnExit += (sender, e) =>
            { };

            sMovingOpenStageForRelease.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingOpenStageForRelease_OpenStageReleasing;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingOpenStageForRelease.OnExit += (sender, e) =>
            { };

            sOpenStageReleasing.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tOpenStageReleasing_MovingToLPHomeFromOpenStage;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sOpenStageReleasing.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeFromOpenStage.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLPHomeFromOpenStage_LPHome;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToLPHomeFromOpenStage.OnExit += (sender, e) =>
            { };
            #endregion

            #region Barcode Reader
            sMovingToBarcodeReaderClamped.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    if (!HalMaskTransfer.CheckPosition(@"D:\Positions\MTRobot\LoadPortHome.json"))
                        throw new MaskTransferPathMoveFailException("Robot position was not at Load Port Home,could not move to Barcode Reader !");
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToBarcodeReader.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToBarcodeReaderClamped_WaitingForBarcodeReader;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToBarcodeReaderClamped.OnExit += (sender, e) =>
            { };

            sWaitingForBarcodeReader.OnEntry += (sender, e) =>
            {
                try// TODO: 判斷Barcode Reader已經讀取完等待移走
                {

                }
                catch (Exception ex)
                {
                    throw new MaskTransferException(ex.Message);
                }

                var transition = tWaitingForBarcodeReader_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sWaitingForBarcodeReader.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeClampedFromBarcodeReader.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\BarcodeReaderToLPHome.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLPHomeClampedFromBarcodeReader_LPHomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToLPHomeClampedFromBarcodeReader.OnExit += (sender, e) =>
            { };
            #endregion

            #region Inspect Deform
            sMovingToInspectDeformFromICHome.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    if (!HalMaskTransfer.CheckPosition(@"D:\Positions\MTRobot\InspChHome.json"))
                        throw new MaskTransferPathMoveFailException("Robot position was not at Inspection Chamber Home,could not move to Inspect Deform !");
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\LPHomeToBarcodeReader.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToInspectDeformFromICHome_WaitingForInspectDeform;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToInspectDeformFromICHome.OnExit += (sender, e) =>
            { };

            sWaitingForInspectDeform.OnEntry += (sender, e) =>
            {
                try// TODO: 判斷Inspect Deform已經檢查完等待移走
                {

                }
                catch (Exception ex)
                {
                    throw new MaskTransferException(ex.Message);
                }

                var transition = tWaitingForInspectDeform_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sWaitingForInspectDeform.OnExit += (sender, e) =>
            { };

            sMovingToICHomeFromInspectDeform.OnEntry += (sender, e) =>
            {
                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(@"D:\Positions\MTRobot\InspDeformToICHome.json");
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToICHomeFromInspectDeform_ICHome;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
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
            sMovingToICHomeFromInspectDeform.OnExit += (sender, e) =>
            { };
            #endregion Inspect Deform

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