using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.MaskTransfer
{
    public enum EnumMacMaskTransferTransition
    {
        SystemBootup,
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
        MoveToLoadPortA,
        ClampInLoadPortA,
        MoveToLPHomeClampedFromLoadPortA,
        TriggerToMoveToLoadPortAInspectedForRelease,
        TriggerToMoveToLoadPortACleanedForRelease,
        MoveToLoadPortAForRelease,
        ReleaseInLoadPortA,
        MoveToLPHomeFromLoadPortA,

        TriggerToMoveToLoadPortB,
        MoveToLoadPortB,
        ClampInLoadPortB,
        MoveToLPHomeClampedFromLoadPortB,
        TriggerToMoveToLoadPortBInspectedForRelease,
        TriggerToMoveToLoadPortBCleanedForRelease,
        MoveToLoadPortBForRelease,
        ReleaseInLoadPortB,
        MoveToLPHomeFromLoadPortB,

        //Inspection Chamber
        TriggerToMoveToInspectionChPellicle,
        MoveToInspectionChPellicle,
        TriggerToMoveToInspectionChGlass,
        MoveToInspectionChGlass,
        ClampInInspectionChPellicle,
        ClampInInspectionChGlass,
        MoveToICHomeClampedFromInspectionChPellicle,
        MoveToICHomeClampedFromInspectionChGlass,
        TriggerToMoveToInspectionChPellicleForRelease,
        MoveToInspectionChPellicleForRelease,
        TriggerToMoveToInspectionChGlassForRelease,
        MoveToInspectionChGlassForRelease,
        ReleaseInInspectionChPellicle,
        ReleaseInInspectionChGlass,
        MoveToICHomeFromInspectionChPellicle,
        MoveToICHomeFromInspectionChGlass,

        //Open Stage
        TriggerToMoveToOpenStage,
        MoveToOpenStage,
        ClampInOpenStage,
        MoveToLPHomeClampedFromOpenStage,
        TriggerToMoveToOpenStageForRelease,
        TriggerToMoveToOpenStageInspectedForRelease,
        TriggerToMoveToOpenStageCleanedForRelease,
        MoveToOpenStageForRelease,
        ReleaseInOpenStage,
        MoveToLPHomeFromOpenStage,

        //Barcode Reader
        TriggerToMoveToBarcodeReaderClamped,
        MoveToBarcodeReader,
        WaitForReadBarcode,
        TriggerToMoveToLPHomeClampedFromBarcodeReader,
        MoveToLPHomeClampedFromBarcodeReader,

        //Inspect Deform
        TriggerToMoveToInspectDeformFromICHome,
        MoveToInspectDeformFromICHome,
        WaitForInspectDeform,
        TriggerToMoveToICHomeFromInspectDeform,
        MoveToICHomeFromInspectDeform,


        ReceiveTransferMask,
        CompleteCalibration,
        CompleteClamped,
        CompleteReleased,
        ReceiveAckHome,


        //Clean
        TriggerToMoveToCleanChPellicle,
        WaitForMoveToCleanPellicle,
        StandbyClampedInCleanChAtOriginPellicle,
        TriggerToMoveToCleanPellicle,
        MoveToCleanPellicle,
        WaitFroCleanPellicle,
        TriggerToCleanPellicle,
        CleanPellicle,
        TriggerToMoveToOriginAfterCleanedPellicle,
        MoveToOriginAfterCleanedPellicle,
        WaitForLeaveAfterCleanPellicle,
        TriggerToMoveToInspectPellicle,
        MoveToInspectPellicle,
        WaitForInspectPellicle,
        TriggerToInspectPellicle,
        InspectPellicle,
        TriggerToMoveToOriginAfterInspectedPellicle,
        MoveToOriginAfterInspectedPellicle,
        WaitForLeaveAfterInspectedPellicle,
        TriggerToMoveToCCHomeClampedFromCleanChPellicle,
        MoveToCCHomeClampedFromCleanChPellicle,

        TriggerToMoveToCleanChGlass,
        WaitForMoveToCleanGlass,
        StandbyClampedInCleanChAtOriginGlass,
        TriggerToMoveToCleanGlass,
        MoveToCleanGlass,
        WaitFroCleanGlass,
        TriggerToCleanGlass,
        CleanGlass,
        WaitForLeaveAfterCleanGlass,
        TriggerToMoveToOriginAfterCleanedGlass,
        MoveToOriginAfterCleanedGlass,
        TriggerToMoveToInspectGlass,
        MoveToInspectGlass,
        WaitForInspectGlass,
        TriggerToInspectGlass,
        InspectGlass,
        WaitForLeaveAfterInspectedGlass,
        TriggerToMoveToOriginAfterInspectedGlass,
        MoveToOriginAfterInspectedGlass,
        TriggerToMoveToCCHomeClampedFromCleanChGlass,
        MoveToCCHomeClampedFromCleanChGlass,

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
