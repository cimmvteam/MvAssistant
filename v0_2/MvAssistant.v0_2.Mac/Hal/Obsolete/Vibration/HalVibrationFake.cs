using MvAssistant.Mac.v1_0.Hal.Component;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.ComponentFake.Vibration
{
    [GuidAttribute("F96453A3-2F9F-46BF-AE9B-737BCB8E2E45")]
    public class HalVibrationFake : HalFakeBase, IHalVibration
    {
        public float GetVibrationValue()
        {
            throw new NotImplementedException();
        }
    }
}
