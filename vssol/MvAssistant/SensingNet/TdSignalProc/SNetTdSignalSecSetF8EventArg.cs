using CToolkit.v1_1.Timing;
using SensingNet.v0_2.TimeSignal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensingNet.v0_2.TdSignalProc
{
    public class SNetTdSignalSecSetF8EventArg : SNetTdSignalEventArg
    {

        public CtkTimeSecond? Time;//當次時間
        public CtkTimeSecond? PrevTime;//前一次時間
        public SNetTSignalSecSetF8 TSignalSource;//完整訊號來源

        public SNetTSignalSecSetF8 TSignalNew = new SNetTSignalSecSetF8();//此次新增訊號

        public SNetTSignalSecF8 GetThisOrLast()
        {
            if (this.Time.HasValue) return this.TSignalSource.Get(this.Time.Value);
            return this.TSignalSource.GetLastOrDefault();
        }

    }
}
