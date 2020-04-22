using MaskAutoCleaner.Hal.Intf.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Imp.Component.Vibration
{

    [GuidAttribute("F0D438F5-27D7-4ABF-ABA8-E3C73647B0A5")]
    public class HalVibrationCwTech : HalComponentBase, IHalVibration
    {




        public float GetVibrationValue()
        {
            throw new NotImplementedException();
        }


        public int HalConnect()
        {
            throw new NotImplementedException();
        }

        public int HalClose()
        {
            throw new NotImplementedException();
        }

        public bool HalIsConnected()
        {
            throw new NotImplementedException();
        }
    }
}
