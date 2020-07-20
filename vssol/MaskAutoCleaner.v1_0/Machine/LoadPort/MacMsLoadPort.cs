using MvAssistant.Mac.v1_0.Hal.CompLoadPort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.LoadPort
{
    [Guid("B6CCEC0B-9042-4B88-A306-E29B87B6469C")]
    public class MacMsLoadPort : MacMachineStateBase
    {
        public IMacHalLoadPortUnit HalMaskTransfer { get { return this.halAssembly as IMacHalLoadPortUnit; } }

        public override void LoadStateMachine()
        {
            throw new NotImplementedException();
        }
    }
}
