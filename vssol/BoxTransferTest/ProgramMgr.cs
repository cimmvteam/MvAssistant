using MvAssistant.MaskTool_v0_1.Plc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTransferTest
{
  public   class ProgramMgr
    {

        public MvPlcContext Plc;


        public void Initial()
        {
            this.Plc = new MvPlcContext();

        }

    }
}
