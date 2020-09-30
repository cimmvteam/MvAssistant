using CToolkit.v1_1.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensingNet.v0_2.TdBase
{
    public class SNetTdEventArg : EventArgs
    {
        public SNetTdEnumInvokeResult InvokeResult = SNetTdEnumInvokeResult.None;



        public static implicit operator SNetTdEventArg(SNetTdEnumInvokeResult result) { return new SNetTdEventArg() { InvokeResult = result }; }
    }
}
