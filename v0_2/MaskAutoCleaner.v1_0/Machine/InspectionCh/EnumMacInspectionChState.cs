using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.InspectionCh
{
    public enum EnumMacInspectionChState
    {
        Start,
        Initial,

        Idle,
        PellicleOnStage,
        DefensingPellicle,
        InspectingPellicle,
        PellicleOnStageInspected,
        WaitingForReleasePellicle,
        
        GlassOnStage,
        DefensingGlass,
        InspectingGlass,
        GlassOnStageInspected,
        WaitingForReleaseGlass,
    }
}
