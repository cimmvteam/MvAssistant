using CToolkit.v1_1.Numeric;
using CToolkit.v1_1.Timing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensingNet.v0_2.TimeSignal
{
    /// <summary>
    /// signal list at specified time
    /// </summary>
    [Serializable]
    public class SNetTSignalSecF8 : ISNetTdTSignal<CtkTimeSecond, double>
    {
        //1 Ticks是100奈秒, 0 tick={0001/1/1 上午 12:00:00}
        //請勿使用Datetime, 避免有人誤解 比對只進行 年月日時分秒, 事實會比較到tick
        public CtkTimeSecond? Time;
        List<double> Signals = new List<double>();
        public List<double> SignalsShot { get { return this.ToShotList(); } }


        public SNetTSignalSecF8() { }
        public SNetTSignalSecF8(CtkTimeSecond? time, IEnumerable<double> signals)
        {
            this.Time = time;
            this.Signals.AddRange(signals);
        }

        public List<double> ToShotList()
        {
            var list = new List<double>();
            lock (this)
            {
                list.AddRange(this.Signals);
            }
            return list;
        }
        public double Max() { lock (this) { return this.Signals.Max(); } }
        public double Min() { lock (this) { return this.Signals.Min(); } }
        public double Count() { lock (this) { return this.Signals.Count; } }




        #region Static Operator

        public static implicit operator SNetTSignalSecF8(KeyValuePair<CtkTimeSecond, List<double>> val) { return new SNetTSignalSecF8(val.Key, val.Value); }

        #endregion

    }
}
