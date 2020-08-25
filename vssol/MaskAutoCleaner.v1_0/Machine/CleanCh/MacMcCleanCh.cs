using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MaskAutoCleaner.v1_0.Msg;
using MvAssistant.Mac.v1_0.Hal.Assembly;

namespace MaskAutoCleaner.v1_0.Machine.CleanCh
{
    [Guid("34546ED9-4B29-443A-9A96-66ACB3AA61F8")]
    public class MacMcCleanCh : MacMachineCtrlBase
    {
        public IMacHalCleanCh HalMaskTransfer { get { return this.halAssembly as IMacHalCleanCh; } }
        /// <summary>
        /// 使用固定的State Machine,
        /// 若有其它版的狀態機, 一般也會用不同的控制機
        /// </summary>
        public MacMsCleanCh StateMachine { get { return this.msAssembly as MacMsCleanCh; } set { this.msAssembly = value; } }

        public MacMcCleanCh()
        {
            this.msAssembly = new MacMsCleanCh();
        }
        public override int RequestProcMsg(MsgBase msg)
        {
            return 0;
        }
    }
}
