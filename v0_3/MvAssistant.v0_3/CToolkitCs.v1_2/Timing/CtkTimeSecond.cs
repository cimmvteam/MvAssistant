using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Timing
{
    public struct CtkTimeSecond : IComparable<CtkTimeSecond>
    {
        public long TotalSecond;
        public CtkTimeSecond(long total = 0) { this.TotalSecond = total; }

        public CtkTimeSecond(DateTime dt)
        {
            var span = dt - new DateTime();
            this.TotalSecond = (long)span.TotalSeconds;
        }

        public DateTime DateTime { get { return new DateTime(TotalSecond * TimeSpan.TicksPerSecond); } }
        public static implicit operator CtkTimeSecond(long d) { return new CtkTimeSecond(d); }
        public static implicit operator CtkTimeSecond(DateTime dt) { return new CtkTimeSecond(dt); }

        public static bool operator !=(CtkTimeSecond a, CtkTimeSecond b) { return a.CompareTo(b) != 0; }

        public static bool operator <(CtkTimeSecond a, CtkTimeSecond b) { return a.CompareTo(b) < 0; }

        public static bool operator <=(CtkTimeSecond a, CtkTimeSecond b) { return a.CompareTo(b) <= 0; }

        public static bool operator ==(CtkTimeSecond a, CtkTimeSecond b) { return a.CompareTo(b) == 0; }

        public static bool operator >(CtkTimeSecond a, CtkTimeSecond b) { return a.CompareTo(b) > 0; }

        public static bool operator >=(CtkTimeSecond a, CtkTimeSecond b) { return a.CompareTo(b) >= 0; }
        public int CompareTo(CtkTimeSecond other) { return this.TotalSecond.CompareTo(other.TotalSecond); }

        public override bool Equals(object obj) { return base.Equals(obj); }
        public override int GetHashCode() { return base.GetHashCode(); }
    }
}
