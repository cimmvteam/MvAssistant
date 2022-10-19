using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace MvaCToolkitCs.v1_2.Wcf.NetTcp
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface SampleICtkWcfNetTcp0102
    {
        [OperationContract()]
        int Minus(int a, int b);
    }
}
