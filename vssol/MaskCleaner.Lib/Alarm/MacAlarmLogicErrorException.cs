using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Alarm
{
    public class MacAlarmLogicErrorException : MacAlarmException
    {
        public MacAlarmLogicErrorException() { this.AlarmId = Msg.EnumAlarmId.Timoueout; }
        public MacAlarmLogicErrorException(string message) : base(message) { this.AlarmId = Msg.EnumAlarmId.DotNetException; }


    }
}
