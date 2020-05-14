using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.FanucRobot_v42_15;

namespace MvAssistant.Mac.TestMy.Device
{
    [TestClass]
    public class UtDeviceRobot
    {
        [TestMethod]
        public void TestPosReg()
        {
            using(var ldd= new MvFanucRobotLdd())
            {
                ldd.RobotIp = "192.168.0.50";

                if (ldd.ConnectIfNo() != 0)
                    Console.WriteLine("Connection Fail");


                var robotInfo = ldd.ReadPosReg(1);
                robotInfo.x += 1;
                ldd.WritePosRegXyzWpr(1, robotInfo);
                robotInfo = ldd.ReadPosReg(1);

                Console.WriteLine(robotInfo.x);
            }


        }
    }
}
