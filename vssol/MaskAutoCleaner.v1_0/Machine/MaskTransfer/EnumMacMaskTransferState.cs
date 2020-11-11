using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.MaskTransfer
{
    public enum EnumMacMaskTransferState
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
        MovingToInspectionChPellicle,
        MovingToInspectionChGlass,
        MovingToOpenStage,

        MovingToLPHomeFromLoadPortA,
        MovingToLPHomeFromLoadPortB,
        MovingToICHomeFromInspectionChPellicle,
        MovingToICHomeFromInspectionChGlass,
        MovingToLPHomeFromOpenStage,

        //夾著 Mask 到 Assembly
        MovingToLoadPortAForRelease,
        MovingToLoadPortBForRelease,
        MovingToInspectionChPellicleForRelease,
        MovingToInspectionChGlassForRelease,
        MovingToOpenStageForRelease,

        //夾著 Mask 回 LPHome 或 ICHome
        MovingToLPHomeClampedFromLoadPortA,
        MovingToLPHomeClampedFromLoadPortB,
        MovingToICHomeClampedFromInspectionChPellicle,
        MovingToICHomeClampedFromInspectionChGlass,
        MovingToLPHomeClampedFromOpenStage,
        #endregion

        #region Gripper action
        LoadPortAClamping,
        LoadPortBClamping,
        InspectionChPellicleClamping,
        InspectionChGlassClamping,
        OpenStageClamping,

        //ReadyToRelease,

        LoadPortAReleasing,
        LoadPortBReleasing,
        InspectionChPellicleReleasing,
        InspectionChGlassReleasing,
        OpenStageReleasing,
        #endregion


        //Clean
        MovingToCleanChPellicle,
        ClampedInCleanChAtOriginPellicle,//WaitingForMoveToClean,
        MovingToCleanPellicle,
        PellicleOnAirGun,
        CleaningPellicle,
        CleanedPellicle,
        MovingToOriginAfterCleanedPellicle,
        //WaitingForMoveToCapture,
        MovingToInspectPellicle,
        PellicleOnCamera,
        InspectingPellicle,
        InspectedPellicle,
        MovingToOriginAfterInspectedPellicle,
        //WaitingForLeaveCleanCh,
        MovingToCCHomeClampedFromCleanChPellicle,

        MovingToCleanChGlass,
        ClampedInCleanChAtOriginGlass,//WaitingForMoveToCleanGlass,
        MovingToCleanGlass,
        GlassOnAirGun,
        CleaningGlass,
        CleanedGlass,
        MovingToOriginAfterCleanedGlass,
        //WaitingForMoveToCaptureGlass,
        MovingToInspectGlass,
        GlassOnCamera,
        InspectingGlass,
        InspectedGlass,
        MovingToOriginAfterInspectedGlass,
        //WaitingForLeaveCleanChGlass,
        MovingToCCHomeClampedFromCleanChGlass,

        //Barcode
        MovingToBarcodeReaderClamped,
        ReadingBarcode,
        MovingToLPHomeClampedFromBarcodeReader,

        //Inspect Deform
        MovingToInspectDeform,
        InspectingClampDeform,
        MovingToICHomeFromInspectDeform,
    }
}
