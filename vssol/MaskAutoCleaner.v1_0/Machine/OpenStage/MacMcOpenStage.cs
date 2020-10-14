using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MaskAutoCleaner.v1_0.Msg;
using MvAssistant.Mac.v1_0.Hal.Assembly;

namespace MaskAutoCleaner.v1_0.Machine.OpenStage
{
    [Guid("C5CA0CD4-C6A1-4F65-B4FA-9EED941864A0")]
    public class MacMcOpenStage : MacMachineCtrlBase
    {
        public IMacHalOpenStage HalOpenStage { get { return this.halAssembly as IMacHalOpenStage; } }
        /// <summary>
        /// 使用固定的State Machine,
        /// 若有其它版的狀態機, 一般也會用不同的控制機
        /// </summary>
        public MacMsOpenStage StateMachine { get { return this.msAssembly as MacMsOpenStage; } set { this.msAssembly = value; } }

        public MacMcOpenStage()
        {
            this.msAssembly = new MacMsOpenStage();
        }
        public override int RequestProcMsg(MacMsgBase msg)
        {
            return 0;
        }
    }
}
