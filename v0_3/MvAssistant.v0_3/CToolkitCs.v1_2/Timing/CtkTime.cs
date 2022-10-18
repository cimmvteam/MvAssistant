using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Timing
{
    public struct CtkTime: IComparable<CtkTime>
    {
        public long TotalMilliSecond;
        public decimal DecimalOfMilliSecond;
        public DateTime DateTime { get { return new DateTime((long)(TotalMilliSecond + DecimalOfMilliSecond) * TimeSpan.TicksPerMillisecond); } }


        public CtkTime(DateTime dt)
        {
            var span = dt - new DateTime();
            this.TotalMilliSecond = (long)span.TotalMilliseconds;
            this.DecimalOfMilliSecond = (decimal)span.TotalMilliseconds - this.TotalMilliSecond;
        }

        public CtkTime(long totalMs = 0, decimal decimalOfMs = 0.0m) { this.TotalMilliSecond = totalMs; this.DecimalOfMilliSecond = decimalOfMs; }

        public int CompareTo(CtkTime other)
        {
            var compare =  this.TotalMilliSecond.CompareTo(other.TotalMilliSecond);
            if (compare != 0) return compare;
            return this.DecimalOfMilliSecond.CompareTo(other.DecimalOfMilliSecond);
        }
    }
}
