using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MaskAutoCleaner.v1_0.Msg;

namespace MaskAutoCleaner.v1_0.Machine.LoadPort
{
    [Guid("34944ABD-CDB9-4ABA-BAB0-384E2FA7134D")]
    public class MacMcLoadPort : MacMachineCtrlBase
    {
        public MacMsLoadPort StateMachine { get { return this.msAssembly as MacMsLoadPort; } set { this.msAssembly = value; } }
        public MacMcLoadPort()
        {
            this.msAssembly = new MacMsLoadPort();
        }
        public override int RequestProcMsg(MacMsgBase msg)
        {
            return 0;
        }
    }
}
