using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace MvaCToolkitCs.v1_2.Wcf.NetTcp
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SampleCtkWcfNetTcpInst01 : SampleICtkWcfNetTcp0101, SampleICtkWcfNetTcp0102
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Minus(int a, int b)
        {
            return a - b;
        }
    }
}
