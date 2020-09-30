using CToolkit.v1_1;
using CToolkit.v1_1.Timing;
using SensingNet.v0_2.TdBase;
using SensingNet.v0_2.TimeSignal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensingNet.v0_2.TdSignalProc
{
    public class SNetTdnStatistics : SNetTdNodeF8
    {

        public SNetTSignalSecSetF8 TSignalAvg = new SNetTSignalSecSetF8();
        public SNetTSignalSecSetF8 TSignalMax = new SNetTSignalSecSetF8();
        public SNetTSignalSecSetF8 TSignalMin = new SNetTSignalSecSetF8();


        ~SNetTdnStatistics() { this.Dispose(false); }



        protected override void Purge()
        {
            if (this.PurgeSeconds <= 0) return;
            var now = DateTime.Now;
            var oldKey = new CtkTimeSecond(now.AddSeconds(-this.PurgeSeconds));

            PurgeSignalByTime(this.TSignalAvg, oldKey);
            PurgeSignalByTime(this.TSignalMax, oldKey);
            PurgeSignalByTime(this.TSignalMin, oldKey);
        }


        public void Input(object sender, SNetTdSignalEventArg e)
        {
            if (!this.IsEnalbed) return;
            var ea = e as SNetTdSignalSecSetF8EventArg;
            if (ea == null) throw new SNetException("尚未無法處理此類資料: " + e.GetType().FullName);


            var key = ea.Time;
            var list = ea.TSignalSource.GetOrCreate(key.Value);
            this.TSignalAvg.Set(ea.Time.Value, list.Average());
            this.TSignalMax.Set(ea.Time.Value, list.Max());
            this.TSignalMin.Set(ea.Time.Value, list.Min());


            this.Purge();

            this.OnDataChange(ea);

            ea.InvokeResult = this.disposed ? SNetTdEnumInvokeResult.IsDisposed : SNetTdEnumInvokeResult.None;
        }


        #region IDisposable

        protected override void DisposeSelf()
        {
            base.DisposeSelf();
        }

        #endregion
    }
}
