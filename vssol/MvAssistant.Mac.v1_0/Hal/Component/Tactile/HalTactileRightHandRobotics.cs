using MvAssistant.Mac.v1_0.Hal.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Tactile
{
    [GuidAttribute("CD93DFDB-C8D3-443A-A176-0749D1261B52")]
    public class HalTactileRightHandRobotics : MacHalComponentBase, IHalStaticElectricityDetector
    {
    

        public float GetCurrent()
        {
            throw new NotImplementedException();
        }

        public float GetPressValue()
        {
            throw new NotImplementedException();
        }

        public int HalClose()
        {
            throw new NotImplementedException();
        }

        public int HalConnect()
        {
            throw new NotImplementedException();
        }

        public bool HalIsConnected()
        {
            throw new NotImplementedException();
        }

        public void HalZeroCalibration()
        {
            throw new NotImplementedException();
        }

        public float Get()
        {
            throw new NotImplementedException();
        }
    }
}
