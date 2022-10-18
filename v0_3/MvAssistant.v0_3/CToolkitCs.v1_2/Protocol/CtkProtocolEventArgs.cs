using System;

namespace CToolkitCs.v1_2.Protocol
{
    public class CtkProtocolEventArgs : EventArgs
    {
        public Exception Exception;
        public string Message;
        public object Sender;
        public CtkProtocolTrxMessage TrxMessage;

    }
}
