using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MaskAutoCleaner.v1_0.Msg;
using MvAssistant.Mac.v1_0.Hal.Assembly;

namespace MaskAutoCleaner.v1_0.Machine.Universal
{
    [Guid("AFEFFEA4-B491-4A75-9E31-BDDBB5131262")]
    public class MacMcUniversal : MacMachineCtrlBase
    {
        public IMacHalUniversal HalMaskTransfer { get { return this.halAssembly as IMacHalUniversal; } }
        /// <summary>
        /// 使用固定的State Machine,
        /// 若有其它版的狀態機, 一般也會用不同的控制機
        /// </summary>
        public MacMsUniversal StateMachine { get { return this.msAssembly as MacMsUniversal; } set { this.msAssembly = value; } }

        public MacMcUniversal()
        {
            this.msAssembly = new MacMsUniversal();
        }
        public override int RequestProcMsg(MsgBase msg)
        {
            return 0;
        }
    }
}
