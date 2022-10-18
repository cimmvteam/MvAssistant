using CToolkit.v1_1.Timing;
using SensingNet.v0_2.TdBase;
using SensingNet.v0_2.TimeSignal;
using System;
using System.Linq;

namespace SensingNet.v0_2.TdSignalProc
{

    /// <summary>
    /// 用在Double(F8)序列資料節點
    /// </summary>
    [Serializable]
    public class SNetTdNodeF8 : SNetTdNode
    {
        public CtkTimeSecond? PrevTime;

        /// <summary>
        /// -1 = unlimit
        /// 0 = no storage
        /// >0 = remain count
        /// </summary>
        public int PurgeCounts = 60;
        //public int PurgeSeconds = 60;

        ~SNetTdNodeF8() { this.Dispose(false); }

        /// <summary>
        /// 簡易處理方式, 多段時間同時輸入時, 請自行分段輸入.
        /// </summary>
        /// <param name="tSignal">原始訊號來源</param>
        /// <param name="newSignals">本次要新增的訊號</param>
        protected virtual void ProcAndPushData(SNetTSignalSecSetF8 tSignal, SNetTSignalSecF8 newSignals)
        {
            if (!this.IsEnalbed) return;
            this.Purge();//先Purge, 避免Exception造成沒有Purge


            var ea = new SNetTdSignalSecSetF8EventArg();
            ea.Sender = this;
            var time = newSignals.Time.HasValue ? newSignals.Time.Value : DateTime.Now;
            ea.Time = time;
            ea.TSignalSource = tSignal;
            ea.PrevTime = this.PrevTime;

            ea.TSignalNew.AddRange(time, newSignals.SignalsShot);
            tSignal.AddRange(time, newSignals.SignalsShot);
            this.OnDataChange(ea);

            this.PrevTime = time;
            this.Purge();
        }

        protected virtual void Purge()
        {
            throw new NotImplementedException();
        }



        #region Event

        public event EventHandler<SNetTdSignalEventArg> EhDataChange;
        protected void OnDataChange(SNetTdSignalEventArg ea)
        {
            if (this.EhDataChange == null) return;
            this.EhDataChange(this, ea);
        }

        #endregion



        #region IDisposable

        protected override void DisposeSelf()
        {
            base.DisposeSelf();
        }

        #endregion




    }
}
