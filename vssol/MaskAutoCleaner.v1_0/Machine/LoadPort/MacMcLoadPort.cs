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
        public MacMsLoadPort StateMachine { get { return this.MsAssembly as MacMsLoadPort; } set { this.MsAssembly = value; } }
        public MacMcLoadPort()
        {
            this.MsAssembly = new MacMsLoadPort();
        }
        public override int RequestProcMsg(IMacMsg msg)
        {
            return 0;
        }
    }
}
