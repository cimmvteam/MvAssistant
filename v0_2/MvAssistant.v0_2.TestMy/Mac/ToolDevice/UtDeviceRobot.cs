using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_2.DeviceDrive.FanucRobot_v42_15;
using MvAssistant.v0_2.Mac.Hal.CompRobot;
using System;
using System.Linq;

namespace MvAssistant.v0_2.Mac.TestMy.ToolDevice
{
    [TestClass]
    public class UtDeviceRobot
    {
        [TestMethod]
        public void TestPosReg()
        {
            using (var ldd = new MvaFanucRobotLdd())
            {
                ldd.RobotIp = "192.168.0.51";

                if (ldd.ConnectTry() != 0)
                    Console.WriteLine("Connection Fail");


                var robotInfo = ldd.ReadPosReg(1);
                robotInfo.x += 1;
                ldd.SetPosRegXyzWpr(1, robotInfo);
                robotInfo = ldd.ReadPosReg(1);

                Console.WriteLine(robotInfo.x);
            }


        }

        [TestMethod]
        public void TestRobotPathSerialize()
        {

            var robotPath = new HalRobotPath()
            {
                Name = "Test",
                Remark = "Test",
            };

            var robotMotion = new HalRobotMotion()
            {
                X = 1.1f,
                Y = 2.2f,
                Z = 3.3f,
                W = 4.4f,
                P = 5.5f,
                R = 6.6f,
                E1 = 7.7f,
                UserFrame = 1,
                UserTool = 1,
                Speed = 60,
                MotionType = HalRobotEnumMotionType.Position,
            };
            robotPath.Motions.Add(robotMotion);

            robotMotion = robotMotion.Clone();
            robotMotion.X += 1;
            robotPath.Motions.Add(robotMotion);

            robotMotion = robotMotion.Clone();
            robotMotion.X += 1;
            robotPath.Motions.Add(robotMotion);






            MvaUtil.SaveXmlToFile(robotPath, robotPath.Name + ".xml");
            var loadPath = MvaUtil.LoadFromXmlFile<HalRobotPath>(robotPath.Name + ".xml");
            var loadMotion = loadPath.Motions.LastOrDefault();



            Assert.AreEqual(robotMotion.X, loadMotion.X);
            Assert.AreEqual(robotMotion.Z, loadMotion.Z);
            Assert.AreEqual(robotMotion.Y, loadMotion.Y);
            Assert.AreEqual(robotMotion.W, loadMotion.W);
            Assert.AreEqual(robotMotion.P, loadMotion.P);
            Assert.AreEqual(robotMotion.R, loadMotion.R);
            Assert.AreEqual(robotMotion.E1, loadMotion.E1);

            Assert.AreEqual(robotMotion.J1, loadMotion.J1);
            Assert.AreEqual(robotMotion.J2, loadMotion.J2);
            Assert.AreEqual(robotMotion.J3, loadMotion.J3);
            Assert.AreEqual(robotMotion.J4, loadMotion.J4);
            Assert.AreEqual(robotMotion.J5, loadMotion.J5);
            Assert.AreEqual(robotMotion.J6, loadMotion.J6);
            Assert.AreEqual(robotMotion.J7, loadMotion.J7);

            Assert.AreEqual(robotMotion.UserFrame, loadMotion.UserFrame);
            Assert.AreEqual(robotMotion.UserTool, loadMotion.UserTool);
            Assert.AreEqual(robotMotion.Speed, loadMotion.Speed);





        }



    }
}
