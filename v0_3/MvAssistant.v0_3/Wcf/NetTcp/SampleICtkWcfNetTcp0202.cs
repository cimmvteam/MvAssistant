using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace MvaCToolkitCs.v1_2.Wcf.NetTcp
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface SampleICtkWcfNetTcp0202
    {
        [OperationContract()]
        int Divide(int a, int b);
    }
}
