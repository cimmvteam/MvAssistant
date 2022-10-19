using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace MvaCToolkitCs.v1_2.Wcf.NetTcp
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SampleCtkWcfNetTcpInst02 : SampleICtkWcfNetTcp0201, SampleICtkWcfNetTcp0202
    {
        public int Multiple(int a, int b)
        {
            return a * b;
        }

        public int Divide(int a, int b)
        {
            return a / b;
        }
    }
}
