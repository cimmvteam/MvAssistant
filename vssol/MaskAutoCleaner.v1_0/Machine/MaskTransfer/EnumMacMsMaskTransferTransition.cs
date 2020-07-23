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

        //Load Port
        ReadyToMoveToLoadPortA,
        ReadyToClampInLoadPortA,
        ReadyToReleaseInLoadPortA,
        ReadyToMoveToLPHomeFromLoadPortA,
        ReadyToStandbyAtLPHome,

        ReadyToMoveToLoadPortB,
        ReadyToClampInLoadPortB,
        ReadyToReleaseInLoadPortB,
        ReadyToMoveToLPHomeFromLoadPortB,
        



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
