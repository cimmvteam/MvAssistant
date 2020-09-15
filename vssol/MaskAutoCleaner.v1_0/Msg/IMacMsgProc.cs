using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaskAutoCleaner.v1_0.Msg
{
    public interface IMacMsgProc
    {
        int RequestProcMsg(MacMsgBase msg);

    }
}
