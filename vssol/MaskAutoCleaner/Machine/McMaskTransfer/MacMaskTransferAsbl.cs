using MaskAutoCleaner.Msg;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MaskAutoCleaner.Machine.MaskTransfer
{

    public class MacMaskTransferAsbl : MacMachineBase
    {
        public IMacHalMaskTransfer halMaskTransfer { get { return this.halAssembly as IMacHalMaskTransfer; } }





        public override int RequestProcMsg(MsgBase msg)
        {



            return 0;
        }
    }
}

