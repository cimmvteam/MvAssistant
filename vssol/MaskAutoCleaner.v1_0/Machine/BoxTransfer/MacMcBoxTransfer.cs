using MaskAutoCleaner.v1_0.Msg;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.BoxTransfer
{
    [Guid("024E0148-043D-4D07-9661-6A4BCD40B316")]
    public class MacMcBoxTransfer : MacMachineCtrlBase
    {
        public IMacHalBoxTransfer HalBoxTransfer { get { return this.HalAssembly as IMacHalBoxTransfer; } }
        /// <summary>
        /// 使用固定的State Machine,
        /// 若有其它版的狀態機, 一般也會用不同的控制機
        /// </summary>
        public MacMsBoxTransfer StateMachine { get { return this.MsAssembly as MacMsBoxTransfer; } set { this.MsAssembly = value; } }

        public MacMcBoxTransfer()
        {
            this.MsAssembly = new MacMsBoxTransfer();
        }

        public override int RequestProcMsg(IMacMsg msg)
        {
            throw new NotImplementedException();
        }
    }
}
