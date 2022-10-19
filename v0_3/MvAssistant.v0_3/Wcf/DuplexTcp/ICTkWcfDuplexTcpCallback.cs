using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace MvaCToolkitCs.v1_2.Wcf.DuplexTcp
{
    public interface ICTkWcfDuplexTcpCallback 
    {
        //[OperationContract()]
        event EventHandler<CtkWcfDuplexEventArgs> EhReceiveMsg;

        [OperationContract(IsOneWay = true)]
        void CtkSend(CtkWcfMessage msg);

        [OperationContract()]
        CtkWcfMessage CtkSendReply(CtkWcfMessage msg);


    }
}
