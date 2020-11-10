using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.CleanCh
{
    public enum EnumMacCleanChTransition
    {
        SystemBootUp,
        Initial,
        StandbyAtIdle,

        Idle,
        TriggerToCleanPellicle,
        CleaningPellicle,
        CleanedPellicle,
        TriggerToReturnToIdleAfterCleanPellicle,
        TriggerToInspectPellicle,
        InspectingPellicle,
        InspectedPellicle,
        TriggerToReturnToIdleAfterInspectPellicle,

        TriggerToCleanGlass,
        CleaningGlass,
        CleanedGlass,
        TriggerToReturnToIdleAfterCleanGlass,
        TriggerToInspectGlass,
        InspectingGlass,
        InspectedGlass,
        TriggerToReturnToIdleAfterInspectGlass,
    }
}
