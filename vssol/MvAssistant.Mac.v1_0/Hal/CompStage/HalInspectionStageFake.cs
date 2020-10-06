using MvAssistant.Mac.v1_0.Hal.Component.Stage;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.ComponentFake.Stage
{
    [GuidAttribute("D29D1764-B386-4219-BCBE-8EE89AF956B7")]
    public class HalInspectionStageFake : HalFakeBase, IHalInspectionStage
    {


        public bool HalMoveAbs(HalStageMotion p)
        {
            return true;
        }

        public bool HalMoveRel(HalStageMotion p)
        {
            return true;
        }

        public bool HalMoveIsComplete()
        {
            return true;
        }
    }
}
