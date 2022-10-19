using MvaCToolkitCs.v1_2.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace MvaCToolkitCs.v1_2.Wcf.DuplexTcp
{
    public class CtkWcfDuplexEventArgs : CtkProtocolEventArgs
    {
        public Object WcfChannel;
        public CtkWcfMessage WcfMsg;
        public CtkWcfMessage WcfReturnMsg;
        public bool IsWcfNeedReturnMsg;

    }
}
