using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.JSon.RobotTransferFile
{
    public enum MaskrobotTransferLocation
    {
        Dontcare = 0,
        LoadPortHome,
        LPHome,
        InspChHome,
        CleanChHome,
        LP1,
        LP2,
        OS,
        IronBox,
        CrystalBox,
        IC,
        ICFrontSide,
        ICHome,
        ICBackSide,
        ICStage,
        Clean,
        Camera,
        Capture,
        CC,
        CCFrontSide,
        CCHome,
        CCBackSide,
        DeformInsp,
        FrontSideCleanFinish,
        FrontSideCaptureFinish,
        BackSideCleanFinish,
        BackSideCapture,
        BarcodeReader,
        InspDeform,
    }
    public static class MaskrobotTransferLocationExtends
    {
        public static string ToDefaultText(this MaskrobotTransferLocation inst)
        {
            return default(string);

        }
        public static string ToText(this MaskrobotTransferLocation inst)
        {
            var rtnV = inst.ToDefaultText();
            if (inst != MaskrobotTransferLocation.Dontcare)
            {
                rtnV = inst.ToString();
            }
            return rtnV;
        }
    }
}
