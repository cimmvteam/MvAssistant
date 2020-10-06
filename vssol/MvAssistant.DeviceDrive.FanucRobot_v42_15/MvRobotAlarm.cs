using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.FanucRobot_v42_15
{

    public class MvRobotAlarm
    {

        public int Count = 0;
        public short AlarmID;
        public short AlarmNumber;
        public short CauseAlarmID;
        public short CauseAlarmNumber;
        public short Severity;
        public short Year;
        public short Month;
        public short Day;
        public short Hour;
        public short Minute;
        public short Second;
        public string AlarmMessage;
        public string CauseAlarmMessage;
        public string SeverityMessage;

    }
}
