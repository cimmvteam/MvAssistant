using MvAssistant.Mac.v1_0.Hal.Component;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.ComponentFake.Ionizer
{
    [GuidAttribute("976E56DF-7331-4BB2-AD1E-015288B3F2F3")]
    public class HalIonizerFake : HalFakeBase, IHalIonizer
    {
        public bool TurnOn()
        {
            return true;
        }

        public bool TurnOff()
        {
            return true;
        }
    }
}
