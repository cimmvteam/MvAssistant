using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Alarm
{
    public class MacAlarmRobotMoveErrorException : MacAlarmException
    {

        public MacAlarmRobotMoveErrorException() { this.AlarmId = Msg.EnumAlarmId.MT_RobotMoveError; }
        public MacAlarmRobotMoveErrorException(string message) : base(message) { this.AlarmId = Msg.EnumAlarmId.MT_RobotMoveError; }


    }
}
