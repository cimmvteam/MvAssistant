using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Alarm
{
    public class MacAlarmTimeoutException : MacAlarmException
    {
        public MacAlarmTimeoutException() { this.AlarmId = Msg.EnumAlarmId.Timoueout; }
        public MacAlarmTimeoutException(string message) : base(message) { this.AlarmId = Msg.EnumAlarmId.Timoueout; }


    }
}
