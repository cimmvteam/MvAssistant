using MvAssistant.Mac.v1_0.Hal.CompPlc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTransferTest
{
  public   class ProgramMgr
    {

        public MacHalPlcContext Plc;


        public void Initial()
        {
            this.Plc = new MacHalPlcContext();

        }

    }
}
