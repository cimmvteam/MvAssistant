using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompRobot
{
    [Serializable]
    public class HalRobotPath
    {
        public string Name;
        public string Remark;
        public List<HalRobotMotion> Motions = new List<HalRobotMotion>();
    }
}
