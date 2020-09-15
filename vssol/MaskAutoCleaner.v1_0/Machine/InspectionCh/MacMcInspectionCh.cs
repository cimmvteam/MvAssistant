using MaskAutoCleaner.v1_0.Msg;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.InspectionCh
{
    [Guid("85BE70B6-10A6-4403-B4E3-3224CD847B48")]
    public class MacMcInspectionCh : MacMachineCtrlBase
    {
        public IMacHalInspectionCh HalMaskTransfer { get { return this.halAssembly as IMacHalInspectionCh; } }
        /// <summary>
        /// 使用固定的State Machine,
        /// 若有其它版的狀態機, 一般也會用不同的控制機
        /// </summary>
        public MacMsInspectionCh StateMachine { get { return this.msAssembly as MacMsInspectionCh; } set { this.msAssembly = value; } }

        public MacMcInspectionCh()
        {
            this.msAssembly = new MacMsInspectionCh();
        }



        public override int RequestProcMsg(MacMsgBase msg)
        {
            return 0;
        }
    }
}
