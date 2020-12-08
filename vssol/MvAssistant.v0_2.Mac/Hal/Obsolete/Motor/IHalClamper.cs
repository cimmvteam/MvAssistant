using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Motor
{
    [GuidAttribute("D6B8D055-BDB2-4925-8CAD-BFEDEB3A5C38")]
    public interface IHalClamper : IMacHalComponent
    {
        bool HalIsShrinked();
        void HalShrinked();
        void HalReleased();
    }
}
