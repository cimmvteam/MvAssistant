using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.FanucRobot_v42_14;

namespace MvAssistant.Mac.TestMy.MachineRealDevice
{
    [TestClass]
    public class UtRobot
    {
        [TestMethod]
        public void TestPosReg()
        {

            using(var ldd = new MvFanucRobotLdd())
            {
                ldd.RobotIp = "192.168.0.50";
                ldd.ConnectIfNo();
                //ldd.WritePosReg()
            }

        }
    }
}
