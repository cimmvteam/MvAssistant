using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.CleanCh
{
    public enum EnumMacMsCleanChTransition
    {
        PowerON,
        Initial,

        Idle,
        CleanMask,
        ReturnToIdleWithMaskCleaned,
        InspectMask,
        ReturnToIdleWithMaskInspected,

        CleanGlass,
        ReturnToIdleWithGlassCleaned,
        InspectGlass,
        ReturnToIdleWithGlassInspected,
    }
}
