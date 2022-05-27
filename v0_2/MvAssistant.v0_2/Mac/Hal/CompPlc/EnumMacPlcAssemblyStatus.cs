using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Hal.CompPlc
{
    public enum EnumMacPlcAssemblyStatus
    {
        None = 0,
        Unknow = -1,

        Idle = 1,
        Busy = 2,
        Alarm = 3,
        Maintenance = 4

    }
}
