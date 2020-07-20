using MvAssistant.Mac.v1_0.Hal.CompLoadPort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.LoadPort
{
    [Guid("B9C477A8-FA09-4B83-9885-8B2F5F201406")]
    public class MacMsLoadPort : MacMachineStateBase
    {
        public IMacHalLoadPortUnit HalMaskTransfer { get { return this.halAssembly as IMacHalLoadPortUnit; } }

        public override void LoadStateMachine()
        {
            throw new NotImplementedException();
        }
    }
}
