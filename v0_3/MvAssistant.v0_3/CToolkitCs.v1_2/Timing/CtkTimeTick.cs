using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Timing
{
    /// <summary>
    /// 此結構其實等同於 DateTime
    /// 因此可以直接使用DateTime取代
    /// 這邊僅留存提示, 提
    /// </summary>
    public struct CtkTimeTick : IComparable<CtkTimeTick>
    {
        public long TotalTicks;
        public CtkTimeTick(DateTime dt)
        {
            var span = dt - new DateTime();
            this.TotalTicks = (long)span.Ticks;
        }

        public CtkTimeTick(long total = 0) { this.TotalTicks = total; }

        public int CompareTo(CtkTimeTick other) { return this.TotalTicks.CompareTo(other.TotalTicks); }

        public override bool Equals(object obj)
        {
            if (!(obj is CtkTimeTick))
            {
                return false;
            }

            var tick = (CtkTimeTick)obj;
            return TotalTicks == tick.TotalTicks;
        }

        public override int GetHashCode()
        {
            var hashCode = -295345707;
            hashCode = hashCode * -1521134295 + TotalTicks.GetHashCode();
            return hashCode;
        }

        public DateTime ToDateTime() { return new DateTime(this.TotalTicks); }



        #region Opeartor

        public static implicit operator CtkTimeTick(long d) { return new CtkTimeTick(d); }
        public static implicit operator CtkTimeTick(DateTime dt) { return new CtkTimeTick(dt); }

        public static bool operator !=(CtkTimeTick a, CtkTimeTick b) { return a.CompareTo(b) != 0; }
        public static bool operator <(CtkTimeTick a, CtkTimeTick b) { return a.CompareTo(b) < 0; }
        public static bool operator <=(CtkTimeTick a, CtkTimeTick b) { return a.CompareTo(b) <= 0; }
        public static bool operator ==(CtkTimeTick a, CtkTimeTick b) { return a.CompareTo(b) == 0; }
        public static bool operator >(CtkTimeTick a, CtkTimeTick b) { return a.CompareTo(b) > 0; }
        public static bool operator >=(CtkTimeTick a, CtkTimeTick b) { return a.CompareTo(b) >= 0; }

        #endregion

    }
}
