using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.FanucRobot_v42_14
{
    public class MvRobotTCPData
    {
        public string DispName = "";
        public int Code = 0;
        public bool Enable = false;

        public override string ToString()
        {
            return "TCP: " + Code.ToString() + ", " + DispName;
        }
    }
}
