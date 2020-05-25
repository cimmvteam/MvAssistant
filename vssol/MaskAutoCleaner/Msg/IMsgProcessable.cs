using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaskAutoCleaner.Msg
{
    public interface IMsgProcessable
    {
        int RequestProcMsg(MsgBase msg);

    }
}
