using MvAssistant.Mac.v1_0.Hal.Component.AirPressure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.AirPressure
{
    [GuidAttribute("CF9AB756-4759-4A7F-A7DC-87429818ECA5")]
    public class HalDiffPressure :MacHalComponentBase, IHalDiffPressure
    {
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
