using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.MaskTransfer
{
    public enum EnumMacMsMaskTransferState
    {
        Start,
        Initial,

        #region Position
        /// <summary>
        /// Load Port Home(Idle)
        /// </summary>
        LPHome,
        LPHomeClamped,
        LPHomeInspected,
        LPHomeCleaned,
        /// <summary>
        /// Inspection  Chamber Home(Idle)
        /// </summary>
        ICHome,
        ICHomeClamped,
        ICHomeInspected,
        CCHomeClamped,
        CCHomeCleaned,
        #endregion

        #region Moving path

        ChangingDirectionToLPHome,
        ChangingDirectionToLPHomeClamped,
        ChangingDirectionToICHome,
        ChangingDirectionToICHomeClamped,
        ChangingDirectionToCCHomeClamped,

        MovingToLoadPortA,
        MovingToLoadPortB,
        MovingToInspectionCh,
        MovingToInspectionChGlass,
        MovingToOpenStage,

        MovingToLPHomeFromLoadPortA,
        MovingToLPHomeFromLoadPortB,
        MovingToICHomeFromInspectionCh,
        MovingToICHomeFromInspectionChGlass,
        MovingToLPHomeFromOpenStage,

        //夾著 Mask 到 Assembly
        MovingToLoadPortAForRelease,
        MovingToLoadPortBForRelease,
        MovingInspectionChForRelease,
        MovingInspectionChGlassForRelease,
        MovingOpenStageForRelease,

        //夾著 Mask 回 LPHome 或 ICHome
        MovingToLPHomeClampedFromLoadPortA,
        MovingToLPHomeClampedFromLoadPortB,
        MovingToICHomeClampedFromInspectionCh,
        MovingToICHomeClampedFromInspectionChGlass,
        MovingToLPHomeClampedFromOpenStage,
        #endregion

        #region Gripper action
        LoadPortAClamping,
        LoadPortBClamping,
        InspectionChClamping,
        InspectionChGlassClamping,
        OpenStageClamping,
        
        //ReadyToRelease,

        LoadPortAReleasing,
        LoadPortBReleasing,
        InspectionChReleasing,
        InspectionChGlassReleasing,
        OpenStageReleasing,
        #endregion


        //Clean
        MovingToCleanCh,
        WaitingForMoveToClean,
        MovingToClean,
        CleaningPellicle,
        MovingAfterCleaned,
        WaitingForMoveToCapture,
        MovingToCapture,
        CapturingPellicle,
        MovingAfterCaptured,
        WaitingForLeaveCleanCh,
        MovingToCCHomeClampedFromCleanCh,

        MovingToCleanChGlass,
        WaitingForMoveToCleanGlass,
        MovingToCleanGlass,
        CleaningGlass,
        MovingAfterCleanedGlass,
        WaitingForMoveToCaptureGlass,
        MovingToCaptureGlass,
        CapturingGlass,
        MovingAfterCapturedGlass,
        WaitingForLeaveCleanChGlass,
        MovingToCCHomeClampedFromCleanChGlass,

        //Barcode
        MovingToBarcodeReaderClamped,
        ReadingBarcode,
        MovingToLPHomeClampedFromBarcodeReader,

        //Inspect Deform
        MovingToInspectDeform,
        InspectingClampDeform,
        MovingToICHomeFromInspectDeform,

        //WaitAckHome,

        ExpRobotPositioningError,
        ExpCalibrationFail,
        ExpCalibrationReleaseFail,
        ExpMayEspDamage,
        ExpMayEspDamageInRelease,

        ExpTactileInReleased,
        ExpTactileInClamped,
        ExpForceInReleased,
        ExpForceInClamped,
        ExpForceInReleasing,
        ExpForceInClamping,

    }
}
