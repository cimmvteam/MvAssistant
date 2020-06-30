﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Machine.SmMaskTransfer
{
    public enum EnumMacSmMaskTransferTransition
    {
        PowerOn,
        CompleteInitial,
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