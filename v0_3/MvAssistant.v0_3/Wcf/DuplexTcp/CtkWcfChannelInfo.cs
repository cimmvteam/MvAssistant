using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace MvaCToolkitCs.v1_2.Wcf.DuplexTcp
{
    public class CtkWcfChannelInfo<T>
    {
        public OperationContext OpContext;
        public T Callback;
        public string SessionId;
        public IContextChannel Channel;
    }
}
