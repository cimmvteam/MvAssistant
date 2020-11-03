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
        InspectedAtLPHomeClamped,
        CleanedAtLPHomeClamped,
        StandbyAtLPHomeCleaned,
        StandbyAtICHome,
        StandbyAtICHomeClamped,
        InspectedAtICHomeClamped,
        StandbyAtICHomeInspected,
        StandbyAtCCHomeClamped,
        CleanedAtCCHomeClamped,
        StandbyAtCCHomeCleaned,

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
        MoveToOpenStageInspectedForRelease,
        MoveToOpenStageCleanedForRelease,
        ReleaseInOpenStage,
        MoveToLPHomeFromOpenStage,
        StandbyAtLPHomeFromOpenStage,
        StandbyAtLPHomeClampedFromOpenStage,

        //Barcode Reader
        MoveToBarcodeReaderClamped,
        WaitForBarcodeReader,
        StandbyAtBarcodeReader,
        MoveToLPHomeClampedFromBarcodeReader,
        StandbyAtLPHomeClampedFromBarcodeReader,

        //Inspect Deform
        MoveToInspectDeformFromICHome,
        WaitForInspectDeform,
        StandbyAtInspectDeform,
        MoveToICHomeFromInspectDeform,
        StandbyAtICHomeFromInspectDeform,


        ReceiveTransferMask,
        CompleteCalibration,
        CompleteClamped,
        CompleteReleased,
        ReceiveAckHome,


        //Clean
        MoveToCleanChPellicle,
        WaitForMoveToClean,
        StandbyClampedInCleanCh,
        MoveToCleanPellicle,
        WaitFroClean,
        CleanPellicle,
        MoveAfterCleanedPellicle,
        WaitForMoveToInspect,
        StandbyAtClean,
        MoveToInspectPellicle,
        WaitForInspect,
        InspectPellicle,
        MoveAfterInspectedPellicle,
        WaitForLeaveCleanCh,
        StandbyAtInspect,
        MoveToCCHomeClampedFromCleanCh,
        StandbyAtCCHomeClampedFromCleanCh,

        MoveToCleanChGlass,
        WaitForMoveToCleanGlass,
        StandbyClampedInCleanChGlass,
        MoveToCleanGlass,
        WaitFroCleanGlass,
        CleanGlass,
        StandbyAtCleanGlass,
        MoveAfterCleanedGlass,
        WaitForMoveToInspectGlass,
        MoveToInspectGlass,
        WaitForInspectGlass,
        InspectGlass,
        StandbyAtInspectGlass,
        MoveAfterInspectedGlass,
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
