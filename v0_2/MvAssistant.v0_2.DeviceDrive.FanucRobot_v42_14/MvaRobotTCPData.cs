﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.DeviceDrive.FanucRobot_v42_14
{
    public class MvaRobotTCPData
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
