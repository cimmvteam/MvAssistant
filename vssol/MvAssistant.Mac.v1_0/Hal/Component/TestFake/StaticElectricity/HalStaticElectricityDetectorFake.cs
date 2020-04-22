using MaskAutoCleaner.Hal.Intf.Component;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.ImpFake.Component.StaticElectricity
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
