using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Timing
{
    public struct CtkTimeMilliSecond : IComparable<CtkTimeMilliSecond>
    {
        public long TotalMilliSecond;
        public CtkTimeMilliSecond(DateTime dt)
        {
            var span = dt - new DateTime();
            this.TotalMilliSecond = (long)span.TotalMilliseconds;
        }

        public CtkTimeMilliSecond(long total = 0) { this.TotalMilliSecond = total; }

        public int CompareTo(CtkTimeMilliSecond other) { return this.TotalMilliSecond.CompareTo(other.TotalMilliSecond); }

        public override bool Equals(object obj)
        {
            if (!(obj is CtkTimeMilliSecond))
            {
                return false;
            }

            var second = (CtkTimeMilliSecond)obj;
            return TotalMilliSecond == second.TotalMilliSecond;
        }

        public override int GetHashCode()
        {
            return -143325390 + TotalMilliSecond.GetHashCode();
        }

        public DateTime ToDateTime() { return new DateTime(TotalMilliSecond * TimeSpan.TicksPerMillisecond); }
        #region Operator

        public static implicit operator CtkTimeMilliSecond(long d) { return new CtkTimeMilliSecond(d); }
        public static implicit operator CtkTimeMilliSecond(DateTime dt) { return new CtkTimeMilliSecond(dt); }

        public static bool operator !=(CtkTimeMilliSecond a, CtkTimeMilliSecond b) { return a.CompareTo(b) != 0; }
        public static bool operator <(CtkTimeMilliSecond a, CtkTimeMilliSecond b) { return a.CompareTo(b) < 0; }
        public static bool operator <=(CtkTimeMilliSecond a, CtkTimeMilliSecond b) { return a.CompareTo(b) <= 0; }
        public static bool operator ==(CtkTimeMilliSecond a, CtkTimeMilliSecond b) { return a.CompareTo(b) == 0; }
        public static bool operator >(CtkTimeMilliSecond a, CtkTimeMilliSecond b) { return a.CompareTo(b) > 0; }
        public static bool operator >=(CtkTimeMilliSecond a, CtkTimeMilliSecond b) { return a.CompareTo(b) >= 0; }

        #endregion

    }
}
