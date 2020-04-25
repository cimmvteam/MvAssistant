using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.E84
{
    [GuidAttribute("9970145F-71F8-48C3-A290-FB98E331005C")]
    public interface IHalE84 : IMacHalComponent
    {
        string HalGetOhtStatus();
        bool HalGetOhtAligned();
    }
}
