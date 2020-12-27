using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.DeviceDrive.FanucRobot_v42_14
{

    public class MvRobotAlarmInfo
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
