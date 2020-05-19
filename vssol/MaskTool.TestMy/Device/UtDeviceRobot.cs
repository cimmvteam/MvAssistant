using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.FanucRobot_v42_15;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;

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
                ldd.RobotIp = "192.168.0.50";

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
        public void TestRobotInfoSerialize()
        {

            var robotInfo = new HalRobotMotion()
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
            };

            MvUtil.SaveToXmlFile(robotInfo, "pos.xml");
            var loadObj = MvUtil.LoadFromXmlFile<HalRobotMotion>("pos.xml");


            Assert.AreEqual(robotInfo.X, loadObj.X);
            Assert.AreEqual(robotInfo.Z, loadObj.Z);
            Assert.AreEqual(robotInfo.Y, loadObj.Y);
            Assert.AreEqual(robotInfo.W, loadObj.W);
            Assert.AreEqual(robotInfo.P, loadObj.P);
            Assert.AreEqual(robotInfo.R, loadObj.R);
            Assert.AreEqual(robotInfo.E1, loadObj.E1);

            Assert.AreEqual(robotInfo.J1, loadObj.J1);
            Assert.AreEqual(robotInfo.J2, loadObj.J2);
            Assert.AreEqual(robotInfo.J3, loadObj.J3);
            Assert.AreEqual(robotInfo.J4, loadObj.J4);
            Assert.AreEqual(robotInfo.J5, loadObj.J5);
            Assert.AreEqual(robotInfo.J6, loadObj.J6);
            Assert.AreEqual(robotInfo.J7, loadObj.J7);

            Assert.AreEqual(robotInfo.UserFrame, loadObj.UserFrame);
            Assert.AreEqual(robotInfo.UserTool, loadObj.UserTool);
            Assert.AreEqual(robotInfo.Speed, loadObj.Speed);





        }



    }
}
