﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.MaskTransfer
{
    public enum EnumMacMsMaskTransferTransition
    {
        PowerON,
        Initial,
        ReceiveTriggerAtLPHome,
        ReceiveTriggerAtLPHomeClamped,
        ReceiveTriggerAtLPHomeInspected,
        InspectedAtLPHomeClamped,
        CleanedAtLPHomeClamped,
        ReceiveTriggerAtLPHomeCleaned,
        ReceiveTriggerAtICHome,
        ReceiveTriggerAtICHomeClamped,
        InspectedAtICHomeClamped,
        ReceiveTriggerAtICHomeInspected,
        ReceiveTriggerAtCCHomeClamped,
        CleanedAtCCHomeClamped,

        //Change Direction
        ChangeDirectionToLPHomeFromICHome,
        ChangeDirectionToICHomeFromLPHome,
        ChangeDirectionToLPHomeClampedFromICHomeClamped,
        ChangeDirectionToLPHomeClampedFromCCHomeClamped,
        ChangeDirectionToICHomeClampedFromLPHomeClamped,
        ChangeDirectionToICHomeClampedFromCCHomeClamped,
        ChangeDirectionToLPHomeCleanedFromCCHomeCleaned,
        ChangeDirectionToCCHomeClampedFromLPHomeClamped,
        ChangeDirectionToCCHomeClampedFromICHomeInspected,
        ChangeDirectionToLPHomeInspectedFromICHomeInspected,
        FinishChangeDirectionToLPHome,
        FinishChangeDirectionToICHome,
        FinishChangeDirectionToLPHomeClamped,
        FinishChangeDirectionToLPHomeInspected,
        FinishChangeDirectionToLPHomeCleaned,
        FinishChangeDirectionToICHomeClamped,
        FinishChangeDirectionToCCHomeClamped,

        //Load Port
        MoveToLoadPortA,
        ClampInLoadPortA,
        MoveToLPHomeClampedFromLoadPortA,
        MoveToLoadPortAInspectedForRelease,
        MoveToLoadPortACleanedForRelease,
        ReleaseInLoadPortA,
        MoveToLPHomeFromLoadPortA,
        StandbyAtLPHomeFromLoadPortA,
        StandbyAtLPHomeClampedFromLoadPortA,

        MoveToLoadPortB,
        ToClampInLoadPortB,
        MoveToLPHomeClampedFromLoadPortB,
        MoveToLoadPortBInspectedForRelease,
        MoveToLoadPortBCleanedForRelease,
        ReleaseInLoadPortB,
        MoveToLPHomeFromLoadPortB,
        StandbyAtLPHomeFromLoadPortB,
        StandbyAtLPHomeClampedFromLoadPortB,

        //Inspection Chamber
        MoveToInspectionCh,
        MoveToInspectionChGlass,
        ClampInInspectionCh,
        ClampInInspectionChGlass,
        MoveToICHomeClampedFromInspectionCh,
        MoveToICHomeClampedFromInspectionChGlass,
        MoveToInspectionChForRelease,
        MoveToInspectionChGlassForRelease,
        ReleaseInInspectionCh,
        ReleaseInInspectionChGlass,
        MoveToICHomeFromInspectionCh,
        MoveToICHomeFromInspectionChGlass,
        StandbyAtICHomeFromInspectionCh,
        StandbyAtICHomeClampedFromInspectionCh,
        StandbyAtICHomeFromInspectionChGlass,
        StandbyAtICHomeClampedFromInspectionChGlass,

        //Open Stage
        MoveToOpenStage,
        ClampInOpenStage,
        MoveToLPHomeClampedFromOpenStage,
        MoveToOpenStageForRelease,
        ReleaseInOpenStage,
        MoveToLPHomeFromOpenStage,
        StandbyAtLPHomeFromOpenStage,
        StandbyAtLPHomeClampedFromOpenStage,

        //Barcode Reader
        MoveToBarcodeReaderClamped,
        WaitForBarcodeReader,
        ReceiveTriggerAtBarcodeReader,
        MoveToLPHomeClampedFromBarcodeReader,
        StandbyAtLPHomeClampedFromBarcodeReader,

        //Inspect Deform
        MoveToInspectDeformFromICHome,
        WaitForInspectDeform,
        ReceiveTriggerAtInspectDeform,
        MoveToICHomeFromInspectDeform,
        StandbyAtICHomeFromInspectDeform,


        ReceiveTransferMask,
        CompleteCalibration,
        CompleteClamped,
        CompleteReleased,
        ReceiveAckHome,


        //Clean
        MoveToCleanCh,
        WaitForMoveToClean,
        MoveToClean,
        WaitFroClean,
        MoveAferCleaned,
        WaitForMoveToCapture,
        ReceiveTriggerAtClean,
        MoveToCapture,
        WaitForCapture,
        MoveAfterCapture,
        WaitForLeaveCleanCh,
        ReceiveTriggerAtCapture,
        MoveToCCHomeClampedFromCleanCh,
        StandbyAtCCHomeClampedFromCleanCh,

        MoveToCleanChGlass,
        WaitForMoveToCleanGlass,
        MoveToCleanGlass,
        WaitFroCleanGlass,
        ReceiveTriggerAtCleanGlass,
        MoveAferCleanedGlass,
        WaitForMoveToCaptureGlass,
        MoveToCaptureGlass,
        WaitForCaptureGlass,
        ReceiveTriggerAtCaptureGlass,
        MoveAfterCapturedGlass,
        WaitForLeaveCleanChGlass,
        MoveToCCHomeClampedFromCleanChGlass,
        StandbyAtCCHomeClampedFromCleanChGlass,

        //Barcode Reader
        IsReady,
        CompleteRead,



        OverPositioningError,
        FailCalibration,
        OverEPotential,
        OverTactile,
        OverForce

    }
}
