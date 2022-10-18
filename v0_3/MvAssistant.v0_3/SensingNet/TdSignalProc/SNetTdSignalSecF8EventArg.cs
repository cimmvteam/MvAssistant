using CToolkit.v1_1.Timing;
using SensingNet.v0_2.TimeSignal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensingNet.v0_2.TdSignalProc
{
    public class SNetTdSignalSecF8EventArg : SNetTdSignalEventArg
    {
        public SNetTSignalSecF8 TSignal;

        public static implicit operator SNetTdSignalSecF8EventArg(SNetTSignalSecF8 val)
        {
            var rs = new SNetTdSignalSecF8EventArg();
            rs.TSignal = val;
            return rs;
        }

        public static implicit operator SNetTdSignalSecF8EventArg(KeyValuePair<CtkTimeSecond, List<double>> val)
        {
            var rs = new SNetTdSignalSecF8EventArg();
            rs.TSignal = val;
            return rs;
        }

    }
}
