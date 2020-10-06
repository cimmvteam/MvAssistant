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
    [Guid("F0AEF882-8298-4DAE-9D7C-99AFEDEAF7F3")]
    public class InclinometerOmronPlc : IHalInclinometer
    {
        public object GetAngle()
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

        public int HalStop()
        {
            throw new NotImplementedException();
        }

        public void HalZeroCalibration()
        {
            throw new NotImplementedException();
        }
    }
}
