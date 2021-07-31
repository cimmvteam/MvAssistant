using CToolkit.v1_1;
using CToolkit.v1_1.Numeric;
using CToolkit.v1_1.Timing;
using MathNet.Filtering.FIR;
using SensingNet.v0_2.TdBase;
using SensingNet.v0_2.TimeSignal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensingNet.v0_2.TdSignalProc
{
    [Serializable]
    public class SNetTdnFft : SNetTdNodeF8
    {
        public int SampleRate = 1024;
        /// <summary>
        /// MathNet FFT 選 Matlab -> 算出來的結果可以加總後取平均, 仍是頻域圖
        /// </summary>
        public SNetTSignalSecSetF8 TSignalSet = new SNetTSignalSecSetF8();


        ~SNetTdnFft() { this.Dispose(false); }

        protected override void Purge()
        {
            if (this.PurgeCounts < 0) return;
            var now = DateTime.Now;
            //var oldKey = new CtkTimeSecond(now.AddSeconds(-this.PurgeSeconds));
            this.TSignalSet.RemoveByCount(this.PurgeCounts);
        }

        public void Input(object sender, SNetTdEventArg e)
        {
            if (!this.IsEnalbed) return;
            var myea = e as SNetTdSignalSecSetF8EventArg;
            if (myea == null) throw new SNetException("尚未無法處理此類資料: " + e.GetType().FullName);




            if (!myea.PrevTime.HasValue) return;
            if (myea.Time == myea.PrevTime.Value) return;
            var t = myea.PrevTime.Value;
            this.Purge();//先Purge, 避免Exception造成沒有Purge


            //取得時間變更前的時間資料
            IList<double> signalData = myea.TSignalSource.GetOrCreate(t);
            signalData = CtkNumUtil.InterpolationForce(signalData, this.SampleRate);

            var ctkNumContext = CtkNumContext.GetOrCreate();
            var comp = ctkNumContext.FftForward(signalData);

            var fftData = new double[comp.Length];
            Parallel.For(0, comp.Length, (idx) =>
            {
                fftData[idx] = comp[idx].Magnitude;
            });
            this.TSignalSet.Set(t, fftData.ToList());




            var ea = new SNetTdSignalSecSetF8EventArg();
            ea.Sender = this;
            ea.PrevTime = this.PrevTime;
            ea.Time = t;
            ea.TSignalSource = this.TSignalSet;
            ea.TSignalNew = new SNetTSignalSecSetF8(t, fftData);
            this.OnDataChange(ea);

            myea.InvokeResult = this.disposed ? SNetTdEnumInvokeResult.IsDisposed : SNetTdEnumInvokeResult.None;
        }


        #region IDisposable

        protected override void DisposeSelf()
        {
            base.DisposeSelf();
        }

        #endregion


    }
}
