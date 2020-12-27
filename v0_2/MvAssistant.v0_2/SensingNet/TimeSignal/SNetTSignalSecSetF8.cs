using CToolkit.v1_1.Numeric;
using CToolkit.v1_1.Timing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SensingNet.v0_2.TimeSignal
{
    [Serializable]
    public class SNetTSignalSecSetF8 : ISNetTdTSignalSet<CtkTimeSecond, double>
    {
        //1 Ticks是100奈秒, 0 tick={0001/1/1 上午 12:00:00}
        //請勿使用Datetime, 避免有人誤解 比對只進行 年月日時分秒, 事實會比較到tick
        /// <summary>
        /// 存取務必注意多執行緒
        /// </summary>
        SortedDictionary<CtkTimeSecond, List<double>> Signals = new SortedDictionary<CtkTimeSecond, List<double>>();
        public List<KeyValuePair<CtkTimeSecond, List<double>>> SignalsShot { get { return this.ToShotList(); } }


        ~SNetTSignalSecSetF8()
        {
            lock (this) this.Signals.Clear();
        }

        public SNetTSignalSecSetF8() { }
        public SNetTSignalSecSetF8(CtkTimeSecond time, IEnumerable<double> signals) { this.AddRange(time, signals); }


        public List<double> this[CtkTimeSecond key] { get { lock (this) return this.Signals[key]; } set { lock (this) this.Signals[key] = value; } }

        public SNetTSignalSecF8 Get(CtkTimeSecond key)
        {
            //存取務必注意多執行緒
            lock (this)
            {
                var signals = this[key];
                return new SNetTSignalSecF8(key, signals);
            }
        }
        public SNetTSignalSecF8 GetFirstOrDefault()
        {
            //存取務必注意多執行緒
            lock (this)
            {
                if (this.Signals.Count == 0) return null;
                return this.Signals.First();
            }
        }
        public SNetTSignalSecF8 GetLastOrDefault()
        {
            //存取務必注意多執行緒
            lock (this)
            {
                if (this.Signals.Count == 0) return null;
                return this.Signals.Last();
            }
        }
        public KeyValuePair<CtkTimeSecond, List<double>>? GetLastOrDefaultPair()
        {

            //存取務必注意多執行緒
            lock (this)
            {
                if (this.Signals.Count == 0) return null;
                return this.Signals.Last();
            }
        }
        public void Interpolation(int dataSize)
        {
            //存取務必注意多執行緒
            lock (this)
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

        }
        public void RemoveFirst()
        {
            //存取務必注意多執行緒
            lock (this)
            {
                var key = this.Signals.First().Key;
                this.Signals.Remove(key);
            }
        }
        public void RemoveByCount(int count)
        {
            //存取務必注意多執行緒
            lock (this)
            {
                while (this.Signals.Count > count)
                    this.Signals.Remove(this.Signals.First().Key);//不能呼叫其它方法, 會deadlock
            }
        }
        public void RemoveByTime(CtkTimeSecond time)
        {
            //存取務必注意多執行緒
            lock (this)
            {
                var query = this.Signals.Where(x => x.Key < time).ToList();
                foreach (var row in query)
                    this.Signals.Remove(row.Key);
            }

        }
        public void RemoveByTime(CtkTimeSecond start, CtkTimeSecond end)
        {
            //存取務必注意多執行緒
            lock (this)
            {
                var query = (from row in this.Signals
                             where row.Key < start || row.Key > end
                             select row).ToList();
                foreach (var row in query)
                    this.Signals.Remove(row.Key);
            }
        }

        public List<KeyValuePair<CtkTimeSecond, List<double>>> ToShotList()
        {
            var list = new List<KeyValuePair<CtkTimeSecond, List<double>>>();
            lock (this)
            {
                foreach (var kv in this.Signals)
                    list.Add(new KeyValuePair<CtkTimeSecond, List<double>>(kv.Key, kv.Value));
            }
            return list;

        }

        public double Max() { lock (this) { return this.Signals.Max(x => x.Value.Max()); } }
        public double Min() { lock (this) { return this.Signals.Min(x => x.Value.Min()); } }
        public double Count() { lock (this) { return this.Signals.Count; } }


        #region ISNetTdTSignalSet

        public void AddRange(CtkTimeSecond key, IEnumerable<double> signals)
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
            rs.Signals[val.Time.Value] = val.SignalsShot;
            return rs;
        }

        #endregion

    }
}
