using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.InspectionCh
{
    public enum EnumMacInspectionChTransition
    {
        PowerON,
        Initial,
        StandbyAtIdle,
        
        TriggerToInspectPellicle,
        DefensePellicle,
        InspectPellicle,
        StandbyAtStageWithPellicleInspected,
        WaitForReleasePellicle,
        StandbyAtWaitForReleasePellicle,
        TriggerToIdleAfterReleasePellicle,
        
        TriggerToInspectGlass,
        DefenseGlass,
        InspectGlass,
        StandbyAtStageWithGlassInspected,
        WaitForReleaseGlass,
        StandbyAtWaitForReleaseGlass,
        TriggerToIdleAfterReleaseGlass,
    }
}
