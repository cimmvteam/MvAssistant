using CToolkit.v1_1.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;

namespace SensingNet.v0_2.TimeSignal
{
    [Serializable]
    public class SNetTSignalSecSetC : ISNetTdTSignalSet<CtkTimeSecond, Complex>
    {
        //1 Ticks是100奈秒, 0 tick={0001/1/1 上午 12:00:00}
        //請勿使用Datetime, 避免有人誤解 比對只進行 年月日時分秒, 事實會比較到tick
        SortedDictionary<CtkTimeSecond, List<Complex>> Signals = new SortedDictionary<CtkTimeSecond, List<Complex>>();
        public List<KeyValuePair<CtkTimeSecond, List<Complex>>> SignalsShot { get { return this.ToShotList(); } }

        public List<Complex> this[CtkTimeSecond key] { get { lock (this) return this.Signals[key]; } set { lock (this) this.Signals[key] = value; } }

        public KeyValuePair<CtkTimeSecond, List<Complex>>? GetLastOrDefault()
        {
            lock (this)
            {
                if (this.Signals.Count == 0) return null;
                return this.Signals.Last();
            }
        }

        public List<KeyValuePair<CtkTimeSecond, List<Complex>>> ToShotList()
        {
            var list = new List<KeyValuePair<CtkTimeSecond, List<Complex>>>();
            lock (this)
            {
                foreach (var kv in this.Signals)
                    list.Add(new KeyValuePair<CtkTimeSecond, List<Complex>>(kv.Key, kv.Value));
            }
            return list;

        }
        public double Max() { lock (this) { return this.Signals.Max(x => x.Value.Max(r => r.Magnitude)); } }
        public double Min() { lock (this) { return this.Signals.Min(x => x.Value.Min(r => r.Magnitude)); } }
        public double Count() { lock (this) { return this.Signals.Count; } }




        #region ISNetDspTimeSignalSet

        public void AddRange(CtkTimeSecond key, IEnumerable<Complex> signals)
        {
            var list = this.GetOrCreate(key);
            lock (this) list.AddRange(signals);
        }
        public bool ContainKey(CtkTimeSecond key) { lock (this) return this.Signals.ContainsKey((CtkTimeSecond)key); }
        public List<Complex> GetOrCreate(CtkTimeSecond key)
        {
            var k = (CtkTimeSecond)key;
            lock (this)
            {
                if (!this.Signals.ContainsKey(k)) this.Signals[k] = new List<Complex>();
                return this.Signals[k];
            }
        }


        public bool GetOrCreate(CtkTimeSecond key, ref List<Complex> data)
        {
            var k = (CtkTimeSecond)key;
            lock (this)
            {
                if (!this.Signals.ContainsKey(k))
                {
                    data = new List<Complex>();
                    this.Signals[k] = data;
                    return true;
                }
                data = this.Signals[k];
            }
            return false;
        }
        public void Set(CtkTimeSecond key, List<Complex> signals)
        {
            var k = (CtkTimeSecond)key;
            lock (this) this.Signals[k] = signals;
        }
        public void Set(CtkTimeSecond key, Complex signal)
        {
            var list = this.GetOrCreate(key);
            lock (this)
            {
                list.Clear();
                list.Add(signal);
            }
        }




        #endregion

    }
}
