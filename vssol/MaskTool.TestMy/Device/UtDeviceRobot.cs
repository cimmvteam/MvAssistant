using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.FanucRobot_v42_15;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using MvAssistant.Mac.v1_0.Hal.CompRobot;

namespace MvAssistant.Mac.TestMy.Device
{
    [TestClass]
    public class UtDeviceRobot
    {
        [TestMethod]
        public void TestPosReg()
        {
            using (var ldd = new MvFanucRobotLdd())
            {
                ldd.RobotIp = "192.168.0.51";

                if (ldd.ConnectIfNo() != 0)
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






            MvUtil.SaveToXmlFile(robotPath, robotPath.Name + ".xml");
            var loadPath = MvUtil.LoadFromXmlFile<HalRobotPath>(robotPath.Name + ".xml");
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
