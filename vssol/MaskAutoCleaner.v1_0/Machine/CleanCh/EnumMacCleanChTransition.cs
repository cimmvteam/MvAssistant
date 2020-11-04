using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.CleanCh
{
    public enum EnumMacCleanChTransition
    {
        PowerON,
        Initial,
        StandbyAtIdle,

        Idle,
        TriggerToCleanPellicle,
        CleaningPellicle,
        TriggerToReturnToIdleAfterCleanPellicle,
        TriggerToInspectPellicle,
        InspectingPellicle,
        TriggerToReturnToIdleAfterInspectPellicle,

        TriggerToCleanGlass,
        CleaningGlass,
        TriggerToReturnToIdleAfterCleanGlass,
        TriggerToInspectGlass,
        InspectingGlass,
        TriggerToReturnToIdleAfterInspectGlass,
    }
}
