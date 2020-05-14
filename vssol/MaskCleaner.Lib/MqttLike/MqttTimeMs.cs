using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.MqttLike
{
    public struct MqttTimeMs : IComparable<MqttTimeMs>
    {
        public long TotalMilliSeconds;
        public MqttTimeMs(long t) { this.TotalMilliSeconds = t; }

        public MqttTimeMs(DateTime t) { this.TotalMilliSeconds = (long)(t - new DateTime()).TotalMilliseconds; }

        public DateTime DateTime { get { return new DateTime(this.TotalMilliSeconds * TimeSpan.TicksPerMillisecond); } }
        public static implicit operator MqttTimeMs(long total) { return new MqttTimeMs(total); }
        public static implicit operator MqttTimeMs(DateTime total) { return new MqttTimeMs(total); }

        public static bool operator <=(MqttTimeMs a, MqttTimeMs b) { return a.CompareTo(b) <= 0; }
        public static bool operator >(MqttTimeMs a, MqttTimeMs b) { return a.CompareTo(b) > 0; }
        public static bool operator <(MqttTimeMs a, MqttTimeMs b) { return a.CompareTo(b) < 0; }
        public static bool operator >=(MqttTimeMs a, MqttTimeMs b) { return a.CompareTo(b) >= 0; }
        public int CompareTo(MqttTimeMs other) { return this.TotalMilliSeconds.CompareTo(other.TotalMilliSeconds); }
    }
}

