using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.InspectionCh
{
    public enum EnumMacInspectionChTransition
    {
        SystemBootUp,
        Initial,
        StandbyAtIdle,
        
        TriggerToInspectPellicle,
        PellicleOnStage,
        DefensePellicle,
        InspectPellicle,
        InspectedPellicleOnStage,
        WaitForReleasePellicle,
        TriggerToIdleAfterReleasePellicle,
        
        TriggerToInspectGlass,
        GlassOnStage,
        DefenseGlass,
        InspectGlass,
        InspectedGlassOnStage,
        WaitForReleaseGlass,
        TriggerToIdleAfterReleaseGlass,
    }
}
