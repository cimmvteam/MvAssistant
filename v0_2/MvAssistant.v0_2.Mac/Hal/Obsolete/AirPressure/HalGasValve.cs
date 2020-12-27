using MvAssistant.Mac.v1_0.Hal.Component.AirPressure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.AirPressure
{
    [GuidAttribute("0E393D33-83EA-4123-A813-EB406E375B7F")]
    public class HalGasValve : MacHalComponentBase, IHalGasValve
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

        public int HalConnect()
        {
            return 1;
        }

        public int HalClose()
        {
            return 1;
        }

        public bool HalIsConnected()
        {
            return true;
        }

        public string ID
        {
            get;
            set;
        }

        public string DeviceConnStr
        {
            get;
            set;
        }
    }
}
