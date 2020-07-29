using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.MaskTransfer
{
    public enum EnumMacMsMaskTransferTransition
    {
        PowerOn,
        CompleteInitial,
        ReadyToReceiveTriggerAtLPHome,
        ReadyToReceiveTriggerAtLPHomeClamped,

        //Load Port
        ReadyToMoveToLoadPortA,
        ReadyToClampInLoadPortA,
        ReadyToMoveToLPHomeClampedFromLoadPortA,
        ReadyToMoveToLoadPortAForRelease,
        ReadyToReleaseInLoadPortA,
        ReadyToMoveToLPHomeFromLoadPortA,
        ReadyToStandbyAtLPHomeFromLoadPortA,
        ReadyToStandbyAtLPHomeClampedFromLoadPortA,

        ReadyToMoveToLoadPortB,
        ReadyToClampInLoadPortB,
        ReadyToMoveToLPHomeClampedFromLoadPortB,
        ReadyToMoveToLoadPortBForRelease,
        ReadyToReleaseInLoadPortB,
        ReadyToMoveToLPHomeFromLoadPortB,
        ReadyToStandbyAtLPHomeFromLoadPortB,
        ReadyToStandbyAtLPHomeClampedFromLoadPortB,

        //Inspection Chamber
        ReadyToMoveToInspectionCh,
        ReadyToMoveToInspectionChGlass,
        ReadyToClampInInspectionCh,
        ReadyToClampInInspectionChGlass,
        ReadyToMoveToICHomeClampedFromInspectionCh,
        ReadyToMoveToICHomeClampedFromInspectionChGlass,
        ReadyToMoveToInspectionChForRelease,
        ReadyToMoveToInspectionChGlassForRelease,
        ReadyToReleaseInInspectionCh,
        ReadyToReleaseInInspectionChGlass,
        ReadyToMoveToICHomeFromInspectionCh,
        ReadyToMoveToICHomeFromInspectionChGlass,
        ReadyToStandbyAtICHomeFromInspectionCh,
        ReadyToStandbyAtICHomeClampedFromInspectionCh,
        ReadyToStandbyAtICHomeFromInspectionChGlass,
        ReadyToStandbyAtICHomeClampedFromInspectionChGlass,

        //Open Stage
        ReadyToMoveToOpenStage,
        ReadyToClampInOpenStage,
        ReadyToMoveToLPHomeClampedFromOpenStage,
        ReadyToMoveToOpenStageForRelease,
        ReadyToReleaseInOpenStage,
        ReadyToMoveToLPHomeFromOpenStage,
        ReadyToStandbyAtLPHomeFromOpenStage,
        ReadyToStandbyAtLPHomeClampedFromOpenStage,

        //Barcode Reader
        ReadyToMoveToBarcodeReaderClamped,
        ReadyToWaitForBarcodeReader,
        ReadyToMoveToLPHomeClampedFromBarcodeReader,
        ReadyToStandbyAtLPHomeClampedFromBarcodeReader,



        ReceiveTransferMask,
        CompleteCalibration,
        CompleteClamped,
        CompleteReleased,
        ReceiveAckHome,


        //Clean
        CleanStart,
        CleanMoveComplete,
        CleanProcessComplete,
        CleanReceiveMove,
        CleanMoveAck,

        NoCleanJob,


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
