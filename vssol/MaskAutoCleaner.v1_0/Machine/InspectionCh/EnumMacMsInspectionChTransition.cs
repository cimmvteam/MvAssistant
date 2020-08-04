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

        WaitForInputMask,
        StandbyAtStageWithMask,
        DefenseMask,
        InspectMask,
        StandbyAtStageWithMaskInspected,
        WaitForReleaseMask,
        ReturnToIdleFromReleaseMask,

        WaitForInputGlass,
        StandbyAtStageWithGlass,
        DefenseGlass,
        InspectGlass,
        StandbyAtStageWithGlassInspected,
        WaitForReleaseGlass,
        ReturnToIdleFromReleaseGlass,
    }
}
