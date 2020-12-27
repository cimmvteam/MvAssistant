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
        public SNetTSignalSecSetF8 TSignal = new SNetTSignalSecSetF8();


        ~SNetTdnFft() { this.Dispose(false); }

        protected override void Purge()
        {
            if (this.PurgeCounts < 0) return;
            var now = DateTime.Now;
            //var oldKey = new CtkTimeSecond(now.AddSeconds(-this.PurgeSeconds));
            this.TSignal.RemoveByCount(this.PurgeCounts);
        }

        public void Input(object sender, SNetTdEventArg e)
        {
            if (!this.IsEnalbed) return;
            var ea = e as SNetTdSignalSecSetF8EventArg;
            if (ea == null) throw new SNetException("尚未無法處理此類資料: " + e.GetType().FullName);




            if (!ea.PrevTime.HasValue) return;
            if (ea.Time == ea.PrevTime.Value) return;
            var t = ea.PrevTime.Value;

            //取得時間變更前的時間資料
            IList<double> signalData = ea.TSignalSource.GetOrCreate(t);
            signalData = CtkNumUtil.InterpolationForce(signalData, this.SampleRate);

            var ctkNumContext = CtkNumContext.GetOrCreate();
            var comp = ctkNumContext.FftForward(signalData);

            var fftData = new double[comp.Length];
            this.TSignal.Set(t, fftData.ToList());

            Parallel.For(0, comp.Length, (idx) =>
            {
                fftData[idx] = comp[idx].Magnitude;
            });


            this.ProcAndPushData(this.TSignal, new SNetTSignalSecF8(t, signalData));
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
