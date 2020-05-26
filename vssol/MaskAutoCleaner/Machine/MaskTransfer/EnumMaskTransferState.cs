using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Machine.MaskTransfer
{
    public enum EnumMaskTransferState
    {
        Start,
        Initial,
        Home,

        MovingToLoadPort,
        MovingToInspectionCh,
        MovingToInspectionChGlass,
        MovingToOpenStage,

        LoadPortClamping,
        InspectionChClamping,
        InspectionChGlassClamping,
        OpenStageClamping,

        GripperClamp,

        MovingToHomeClampedFromLoadPort,
        MovingToHomeClampedFromInspectionCh,
        MovingToHomeClampedFromInspectionChGlass,
        MovingToHomeClampedFromOpenStage,

        HomeClamped,
        ReadyToRelease,

        //Clean
        CleanReady,
        CleanMovingStart,
        CleanMovingReturn,
        CleanChMoving,
        CleanChWaitAckMove,

        //Barcode
        MovingToBarcodeReader,
        BarcodeReader,
        MovingToHomeClampedFromBarcodeReader,

        MovingToLoadPortForRelease,
        MovingInspectionChForRelease,
        MovingInspectionChGlassForRelease,
        MovingOpenStageForRelease,

        LoadPortReleasing,
        InspectionChReleasing,
        InspectionChGlassReleasing,
        OpenStageReleasing,

        GripperRelease,

        MovingToHomeFromLoadPort,
        MovingToHomeFromInspectionCh,
        MovingToHomeFromInspectionChGlass,
        MovingToHomeFromOpenStage,









        WaitAckHome,



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
