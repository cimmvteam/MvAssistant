using MaskAutoCleaner.Hal.Intf.Component.AirPressure;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.ImpFake.Component.AirPressure
{
    [GuidAttribute("ED5BD81C-DEB5-46D4-B2A2-6E996D79E043")]
    public class HalGasValveFake : HalFakeBase, IHalGasValve
    {
        public bool TurnOn()
        {
            return true;
        }

        public bool TurnOff()
        {
            return true;
        }

        public void HalZeroCalibration()
        {
            return;
        }
    }
}
