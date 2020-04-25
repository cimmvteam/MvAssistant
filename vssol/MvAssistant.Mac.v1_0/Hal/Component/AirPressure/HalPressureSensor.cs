using MvAssistant.Mac.v1_0.Hal.Component.AirPressure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.AirPressure
{
    [GuidAttribute("E99805FA-2669-4A7D-94CC-F7F0AEBF9920")]
    public class HalPressureSensor : MacHalComponentBase, IHalPressureSensor
    {
        public void SetPressureValue(float value)
        {
            throw new NotImplementedException();
        }

        public float GetPressureValue()
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
