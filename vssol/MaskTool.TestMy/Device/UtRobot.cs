using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.FanucRobot;

namespace MaskTool.TestMy.Device
{
    [TestClass]
    public class UtRobot
    {
        [TestMethod]
        public void TestMethod1()
        {

            using (var robot = new MvFanucRobotLdd())
            {
                robot.RobotIp = "192.168.0.50";
                robot.ConnectIfNo();
                robot.SystemRecoverAuto();
                robot.AlarmReset();
                robot.ExecutePNS("PNS0101");



                Array target = new float[] { 0, 0, 5, 0, 0, 0 };


                robot.MoveStraightSync(target, 0, 0, 1, 20);



            }



        }
    }
}
