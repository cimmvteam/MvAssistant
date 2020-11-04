using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.MaskTransfer
{
    public enum EnumMacMaskTransferTransition
    {
        PowerON,
        Initial,
        StandbyAtLPHome,
        StandbyAtLPHomeClamped,
        StandbyAtLPHomeInspected,
        //TriggerToInspectedAtLPHomeClamped,
        //TriggerToCleanedAtLPHomeClamped,
        StandbyAtLPHomeCleaned,
        StandbyAtICHome,
        StandbyAtICHomeClamped,
        TriggerToChangeMaskStateToInspected,
        StandbyAtICHomeInspected,
        StandbyAtCCHomeClamped,
        TriggerToChangeMaskStateToCleaned,
        StandbyAtCCHomeCleaned,

        //Change Direction
        TriggerToChangeDirectionToLPHomeFromICHome,
        TriggerToChangeDirectionToICHomeFromLPHome,
        TriggerToChangeDirectionToLPHomeClampedFromICHomeClamped,
        TriggerToChangeDirectionToLPHomeClampedFromCCHomeClamped,
        TriggerToChangeDirectionToICHomeClampedFromLPHomeClamped,
        TriggerToChangeDirectionToICHomeClampedFromCCHomeClamped,
        TriggerToChangeDirectionToLPHomeCleanedFromCCHomeCleaned,
        TriggerToChangeDirectionToCCHomeClampedFromLPHomeClamped,
        TriggerToChangeDirectionToCCHomeClampedFromICHomeInspected,
        TriggerToChangeDirectionToLPHomeInspectedFromICHomeInspected,
        FinishChangeDirectionToLPHome,
        FinishChangeDirectionToICHome,
        FinishChangeDirectionToLPHomeClamped,
        FinishChangeDirectionToLPHomeInspected,
        FinishChangeDirectionToLPHomeCleaned,
        FinishChangeDirectionToICHomeClamped,
        FinishChangeDirectionToCCHomeClamped,

        //Load Port
        TriggerToMoveToLoadPortA,
        ClampInLoadPortA,
        MoveToLPHomeClampedFromLoadPortA,
        TriggerToMoveToLoadPortAInspectedForRelease,
        TriggerToMoveToLoadPortACleanedForRelease,
        ReleaseInLoadPortA,
        MoveToLPHomeFromLoadPortA,
        StandbyAtLPHomeFromLoadPortA,
        StandbyAtLPHomeClampedFromLoadPortA,

        TriggerToMoveToLoadPortB,
        ToClampInLoadPortB,
        MoveToLPHomeClampedFromLoadPortB,
        TriggerToMoveToLoadPortBInspectedForRelease,
        TriggerToMoveToLoadPortBCleanedForRelease,
        ReleaseInLoadPortB,
        MoveToLPHomeFromLoadPortB,
        StandbyAtLPHomeFromLoadPortB,
        StandbyAtLPHomeClampedFromLoadPortB,

        //Inspection Chamber
        TriggerToMoveToInspectionCh,
        TriggerToMoveToInspectionChGlass,
        ClampInInspectionCh,
        ClampInInspectionChGlass,
        MoveToICHomeClampedFromInspectionCh,
        MoveToICHomeClampedFromInspectionChGlass,
        TriggerToMoveToInspectionChForRelease,
        TriggerToMoveToInspectionChGlassForRelease,
        ReleaseInInspectionCh,
        ReleaseInInspectionChGlass,
        MoveToICHomeFromInspectionCh,
        MoveToICHomeFromInspectionChGlass,
        StandbyAtICHomeFromInspectionCh,
        StandbyAtICHomeClampedFromInspectionCh,
        StandbyAtICHomeFromInspectionChGlass,
        StandbyAtICHomeClampedFromInspectionChGlass,

        //Open Stage
        TriggerToMoveToOpenStage,
        ClampInOpenStage,
        MoveToLPHomeClampedFromOpenStage,
        TriggerToMoveToOpenStageForRelease,
        TriggerToMoveToOpenStageInspectedForRelease,
        TriggerToMoveToOpenStageCleanedForRelease,
        ReleaseInOpenStage,
        MoveToLPHomeFromOpenStage,
        StandbyAtLPHomeFromOpenStage,
        StandbyAtLPHomeClampedFromOpenStage,

        //Barcode Reader
        TriggerToMoveToBarcodeReaderClamped,
        WaitForBarcodeReader,
        StandbyAtBarcodeReader,
        TriggerToMoveToLPHomeClampedFromBarcodeReader,
        StandbyAtLPHomeClampedFromBarcodeReader,

        //Inspect Deform
        TriggerToMoveToInspectDeformFromICHome,
        WaitForInspectDeform,
        StandbyAtInspectDeform,
        TriggerToMoveToICHomeFromInspectDeform,
        StandbyAtICHomeFromInspectDeform,


        ReceiveTransferMask,
        CompleteCalibration,
        CompleteClamped,
        CompleteReleased,
        ReceiveAckHome,


        //Clean
        TriggerToMoveToCleanChPellicle,
        WaitForMoveToClean,
        StandbyClampedInCleanCh,
        TriggerToMoveToCleanPellicle,
        WaitFroClean,
        TriggerToCleanPellicle,
        TriggerToMoveAfterCleanedPellicle,
        WaitForMoveToInspect,
        StandbyAtClean,
        TriggerToMoveToInspectPellicle,
        WaitForInspect,
        TriggerToInspectPellicle,
        TriggerToMoveAfterInspectedPellicle,
        WaitForLeaveCleanCh,
        StandbyAtInspect,
        TriggerToMoveToCCHomeClampedFromCleanCh,
        StandbyAtCCHomeClampedFromCleanCh,

        TriggerToMoveToCleanChGlass,
        WaitForMoveToCleanGlass,
        StandbyClampedInCleanChGlass,
        TriggerToMoveToCleanGlass,
        WaitFroCleanGlass,
        TriggerToCleanGlass,
        StandbyAtCleanGlass,
        TriggerToMoveAfterCleanedGlass,
        WaitForMoveToInspectGlass,
        TriggerToMoveToInspectGlass,
        WaitForInspectGlass,
        TriggerToInspectGlass,
        StandbyAtInspectGlass,
        TriggerToMoveAfterInspectedGlass,
        WaitForLeaveCleanChGlass,
        TriggerToMoveToCCHomeClampedFromCleanChGlass,
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
