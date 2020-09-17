using MaskAutoCleaner.v1_0.Msg;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.BoxTransfer
{
    public class MacMcBoxTransfer : MacMachineCtrlBase
    {
        public IMacHalBoxTransfer HalBoxTransfer { get { return this.halAssembly as IMacHalBoxTransfer; } }
        /// <summary>
        /// 使用固定的State Machine,
        /// 若有其它版的狀態機, 一般也會用不同的控制機
        /// </summary>
        public MacMsBoxTransfer StateMachine { get { return this.msAssembly as MacMsBoxTransfer; } set { this.msAssembly = value; } }

        public MacMcBoxTransfer()
        {
            this.msAssembly = new MacMsBoxTransfer();
        }



        public override int RequestProcMsg(MsgBase msg)
        {
            return 0;
        }
    }
}
