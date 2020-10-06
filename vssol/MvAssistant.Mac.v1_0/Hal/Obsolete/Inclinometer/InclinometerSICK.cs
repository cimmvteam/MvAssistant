using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvAssistant.Mac.v1_0.Hal.Component;
using System.Runtime.InteropServices;
using MvAssistant.Mac.v1_0.Hal.Component.Inclinometer;

namespace MvAssistant.Mac.v1_0.Hal.Component.Inclinometer
{
    [GuidAttribute("6D330C85-23B3-4534-A3FF-6578DDA1ABA9")]
    public class InclinometerSICK : MacHalComponentBase, IHalInclinometer
    {
        public object GetAngle()
        {
            throw new NotImplementedException();
        }

        public void HalZeroCalibration()
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
