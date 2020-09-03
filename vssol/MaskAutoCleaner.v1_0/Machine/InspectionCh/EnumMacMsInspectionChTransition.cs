using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.InspectionCh
{
    public enum EnumMacMsInspectionChTransition
    {
        PowerON,
        Initial,
        StandbyAtIdle,

        ReceiveTriggerToWaitForInputPellicle,
        StandbyAtWaitForInputPellicle,
        ReceiveTriggerToInspectPellicle,
        DefensePellicle,
        InspectPellicle,
        StandbyAtStageWithPellicleInspected,
        WaitForReleasePellicle,
        StandbyAtWaitForReleasePellicle,
        ReceiveTriggerToIdleAfterReleasePellicle,

        ReceiveTriggerToWaitForInputGlass,
        StandbyAtWaitForInputGlass,
        ReceiveTriggerToInspectGlass,
        DefenseGlass,
        InspectGlass,
        StandbyAtStageWithGlassInspected,
        WaitForReleaseGlass,
        StandbyAtWaitForReleaseGlass,
        ReceiveTriggerToIdleAfterReleaseGlass,
    }
}
