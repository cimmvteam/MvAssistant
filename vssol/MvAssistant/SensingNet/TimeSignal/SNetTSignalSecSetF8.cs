using CToolkit.v1_1.Numeric;
using CToolkit.v1_1.Timing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensingNet.v0_2.TimeSignal
{
    public class SNetTSignalSecSetF8 : ISNetTdTSignalSet<CtkTimeSecond, double>
    {
        //1 Ticks是100奈秒, 0 tick={0001/1/1 上午 12:00:00}
        //請勿使用Datetime, 避免有人誤解 比對只進行 年月日時分秒, 事實會比較到tick
        public SortedDictionary<CtkTimeSecond, List<double>> Signals = new SortedDictionary<CtkTimeSecond, List<double>>();

        ~SNetTSignalSecSetF8()
        {
            this.Signals.Clear();
        }

        public SNetTSignalSecSetF8() { }
        public SNetTSignalSecSetF8(CtkTimeSecond time, IEnumerable<double> signals) { this.Add(time, signals); }


        public List<double> this[CtkTimeSecond key] { get { return this.Signals[key]; } set { this.Signals[key] = value; } }

        public SNetTSignalSecF8 Get(CtkTimeSecond key)
        {
            var signals = this[key];
            return new SNetTSignalSecF8(key, signals);
        }
        public SNetTSignalSecF8 GetFirstOrDefault()
        {
            if (this.Signals.Count == 0) return null;
            return this.Signals.First();
        }
        public SNetTSignalSecF8 GetLastOrDefault()
        {
            if (this.Signals.Count == 0) return null;
            return this.Signals.Last();
        }
        public KeyValuePair<CtkTimeSecond, List<double>>? GetLastOrDefaultPair()
        {
            if (this.Signals.Count == 0) return null;
            return this.Signals.Last();
        }
        public void Interpolation(int dataSize)
        {

            foreach (var kv in this.Signals)
            {
                var ss = kv.Value;
                if (dataSize == ss.Count) continue;
                var list = CtkNumUtil.InterpolationCanOneOrZero(ss, dataSize);
                kv.Value.Clear();
                kv.Value.AddRange(list);
            }
        }
        public void RemoveFirst()
        {
            var key = this.Signals.First().Key;
            this.Signals.Remove(key);
        }



        #region ISNetTdTSignalSet

        public void Add(CtkTimeSecond key, IEnumerable<double> signals)
        {
            var list = this.GetOrCreate(key);
            list.AddRange(signals);
        }
        public void Add(CtkTimeSecond key, double signal)
        {
            var list = this.GetOrCreate(key);
            list.Add(signal);
        }
        public bool ContainKey(CtkTimeSecond key) { return this.Signals.ContainsKey((CtkTimeSecond)key); }
        public List<double> GetOrCreate(CtkTimeSecond key)
        {
            var k = (CtkTimeSecond)key;
            if (!this.Signals.ContainsKey(k))
            {
                lock (this)
                    this.Signals[k] = new List<double>();
            }
            return this.Signals[k];
        }
        public bool GetOrCreate(CtkTimeSecond key, ref List<double> data)
        {
            var k = (CtkTimeSecond)key;
            if (!this.Signals.ContainsKey(k))
            {
                data = new List<double>();
                lock (this)
                    this.Signals[k] = data;

                return true;
            }

            data = this.Signals[k];
            return false;
        }
        public void Set(CtkTimeSecond key, List<double> signals)
        {
            var k = (CtkTimeSecond)key;
            lock (this)
                this.Signals[k] = signals;
        }
        public void Set(CtkTimeSecond key, double signal)
        {
            var list = this.GetOrCreate(key);
            list.Clear();
            list.Add(signal);
        }

        #endregion



        #region Static Operator

        public static implicit operator SNetTSignalSecSetF8(SNetTSignalSecF8 val)
        {
            var rs = new SNetTSignalSecSetF8();
            if (!val.Time.HasValue) throw new ArgumentException("Time can not be null");
            rs.Signals[val.Time.Value] = val.Signals;
            return rs;
        }

        #endregion

    }
}
