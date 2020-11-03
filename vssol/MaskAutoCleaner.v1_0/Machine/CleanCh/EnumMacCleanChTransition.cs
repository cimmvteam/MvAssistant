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
        ReceiveTriggerToCleanPellicle,
        CleaningPellicle,
        ReceiveTriggerToReturnToIdleAfterCleanPellicle,
        ReceiveTriggerToInspectPellicle,
        InspectingPellicle,
        ReceiveTriggerToReturnToIdleAfterInspectPellicle,

        ReceiveTriggerToCleanGlass,
        CleaningGlass,
        ReceiveTriggerToReturnToIdleAfterCleanGlass,
        ReceiveTriggerToInspectGlass,
        InspectingGlass,
        ReceiveTriggerToReturnToIdleAfterInspectGlass,
    }
}
