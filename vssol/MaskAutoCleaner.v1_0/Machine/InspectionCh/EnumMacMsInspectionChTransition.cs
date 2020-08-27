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
        ReceiveTriggerAtIdle,

        WaitForInputPellicle,
        ReceiveTriggerAtInputPellicle,
        StandbyAtStageWithPellicle,
        DefensePellicle,
        InspectPellicle,
        StandbyAtStageWithPellicleInspected,
        WaitForReleasePellicle,
        ReceiveTriggerAtReleasePellicle,
        ReturnToIdleFromReleasePellicle,

        WaitForInputGlass,
        ReceiveTriggerAtInputGlass,
        StandbyAtStageWithGlass,
        DefenseGlass,
        InspectGlass,
        StandbyAtStageWithGlassInspected,
        WaitForReleaseGlass,
        ReceiveTriggerAtReleaseGlass,
        ReturnToIdleFromReleaseGlass,
    }
}
