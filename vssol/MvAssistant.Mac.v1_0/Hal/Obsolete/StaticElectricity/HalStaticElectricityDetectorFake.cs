using MvAssistant.Mac.v1_0.Hal.Component;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.ComponentFake.StaticElectricity
{
    [GuidAttribute("12D2BE92-61D0-415E-8710-54A35429E888")]
    public class HalStaticElectricityDetectorFake : HalFakeBase, IHalStaticElectricityDetector
    {
        public float Get()
        {
            throw new NotImplementedException();
        }
    }
}
