using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Sensor.Proto
{
    public struct MvaSsProtoCmdSvidCfg
    {
        public UInt32 Svid;
        public double CalibrateSysOffset;
        public double CalibrateSysScale;
        public double CalibrateUserOffset;
        public double CalibrateUserScale;


    }
}
