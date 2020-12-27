using MvAssistant.v0_2.Mac.Hal.CompPlc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistantMacVerifyEqp
{
    public class ProgramMgr
    {

        public MacHalPlcContext Plc;


        public void Initial()
        {
            this.Plc = new MacHalPlcContext();

        }

    }
}
