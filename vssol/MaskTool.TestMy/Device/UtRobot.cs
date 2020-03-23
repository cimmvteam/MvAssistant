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

                robot.StopProgram();
                robot.SystemRecoverAuto();
                robot.AlarmReset();
                robot.ExecutePNS("PNS0101");

                var robotInfo = robot.GetCurrRobotInfo();

                var  target = new float[] { robotInfo.x, robotInfo.y, robotInfo.z + 10, robotInfo.w, robotInfo.p, robotInfo.r };

                robot.ExecuteMove(target);


                //robot.MoveStraightSync(target, 0, 0, 0, 20);



            }



        }
    }
}
