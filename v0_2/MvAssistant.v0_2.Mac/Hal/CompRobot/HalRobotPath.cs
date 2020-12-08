using System;
using System.Collections.Generic;

namespace MvAssistant.v0_2.Mac.Hal.CompRobot
{
    [Serializable]
    public class HalRobotPath
    {
        public string Name;
        public string Remark;
        public List<HalRobotMotion> Motions = new List<HalRobotMotion>();
    }
}
