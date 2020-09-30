using CToolkit.v1_1.Timing;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace SensingNet.v0_2.TimeSignal
{
    public class SNetTSignalSecSetC : ISNetTdTSignalSet<CtkTimeSecond, Complex>
    {
        //1 Ticks是100奈秒, 0 tick={0001/1/1 上午 12:00:00}
        //請勿使用Datetime, 避免有人誤解 比對只進行 年月日時分秒, 事實會比較到tick
        public SortedDictionary<CtkTimeSecond, List<Complex>> Signals = new SortedDictionary<CtkTimeSecond, List<Complex>>();
        public List<Complex> this[CtkTimeSecond key] { get { return this.Signals[key]; } set { this.Signals[key] = value; } }

        public KeyValuePair<CtkTimeSecond, List<Complex>>? GetLastOrDefault()
        {
            if (this.Signals.Count == 0) return null;
            return this.Signals.Last();
        }



        #region ISNetDspTimeSignalSet

        public void Add(CtkTimeSecond key, IEnumerable<Complex> signals)
        {
            var list = this.GetOrCreate(key);
            list.AddRange(signals);
        }
        public bool ContainKey(CtkTimeSecond key) { return this.Signals.ContainsKey((CtkTimeSecond)key); }
        public List<Complex> GetOrCreate(CtkTimeSecond key)
        {
            var k = (CtkTimeSecond)key;
            if (!this.Signals.ContainsKey(k)) this.Signals[k] = new List<Complex>();
            return this.Signals[k];


        }


        public bool GetOrCreate(CtkTimeSecond key, ref List<Complex> data)
        {
            var k = (CtkTimeSecond)key;
            if (!this.Signals.ContainsKey(k))
            {
                data = new List<Complex>();
                this.Signals[k] = data;

                return true;
            }

            data = this.Signals[k];
            return false;
        }
        public void Set(CtkTimeSecond key, List<Complex> signals)
        {
            var k = (CtkTimeSecond)key;
            this.Signals[k] = signals;
        }
        public void Set(CtkTimeSecond key, Complex signal)
        {
            var list = this.GetOrCreate(key);
            list.Clear();
            list.Add(signal);
        }




        #endregion

    }
}
