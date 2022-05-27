using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Hal.CompE84
{
    [Guid("C9B53EE3-A59F-4293-B095-0F81E84A791E")]
    public class MacHalE84Aml : MacHalComponentBase, IMacHalE84
    {


        public override int HalClose()
        {
            return 0;
        }

        public override int HalConnect()
        {
            return 0;
        }
    }
}
