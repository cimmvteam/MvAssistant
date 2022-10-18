using System;
using System.Collections.Generic;

namespace MvAssistant.v0_3.Mac.Hal.CompRobot
{
    [Serializable]
    public class MacHalRobotPath
    {
        public string Name;
        public string Remark;
        public List<MacHalRobotMotion> Motions = new List<MacHalRobotMotion>();
    }
}
