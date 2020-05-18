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

            var robotInfo = new MvFanucRobotInfo()
            {
                x = 1.1f,
                y = 2.2f,
                z = 3.3f,
                w = 4.4f,
                p = 5.5f,
                r = 6.6f,
                e1 = 7.7f,
                UserFrame = 1,
                UserTool = 1,
                Speed = 60,
            };

            MvUtil.SaveToXmlFile(robotInfo, "pos.xml");
            var loadObj = MvUtil.LoadFromXmlFile<MvFanucRobotInfo>("pos.xml");


            Assert.AreEqual(robotInfo.x, loadObj.x);
            Assert.AreEqual(robotInfo.y, loadObj.y);
            Assert.AreEqual(robotInfo.z, loadObj.z);
            Assert.AreEqual(robotInfo.w, loadObj.w);
            Assert.AreEqual(robotInfo.p, loadObj.p);
            Assert.AreEqual(robotInfo.r, loadObj.r);
            Assert.AreEqual(robotInfo.e1, loadObj.e1);
            Assert.AreEqual(robotInfo.e2, loadObj.e2);
            Assert.AreEqual(robotInfo.e3, loadObj.e3);

            Assert.AreEqual(robotInfo.j1, loadObj.j1);
            Assert.AreEqual(robotInfo.j2, loadObj.j2);
            Assert.AreEqual(robotInfo.j3, loadObj.j3);
            Assert.AreEqual(robotInfo.j4, loadObj.j4);
            Assert.AreEqual(robotInfo.j5, loadObj.j5);
            Assert.AreEqual(robotInfo.j6, loadObj.j6);
            Assert.AreEqual(robotInfo.j7, loadObj.j7);
            Assert.AreEqual(robotInfo.j8, loadObj.j8);
            Assert.AreEqual(robotInfo.j9, loadObj.j9);

            Assert.AreEqual(robotInfo.UserFrame, loadObj.UserFrame);
            Assert.AreEqual(robotInfo.UserTool, loadObj.UserTool);
            Assert.AreEqual(robotInfo.Speed, loadObj.Speed);





        }



    }
}
