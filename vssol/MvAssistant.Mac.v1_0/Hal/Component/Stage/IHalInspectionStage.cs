using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Stage
{
    [GuidAttribute("187B5942-DB33-4730-8D6C-97EC59F6385B")]
    public interface IHalInspectionStage : IHalComponent
    {
        bool HalMoveAbs(HalStageMotion p);
        bool HalMoveRel(HalStageMotion p);
        bool HalMoveIsComplete();
    }
}
