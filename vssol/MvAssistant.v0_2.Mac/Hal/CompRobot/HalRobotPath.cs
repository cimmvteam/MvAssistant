using System;
using System.Collections.Generic;

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
