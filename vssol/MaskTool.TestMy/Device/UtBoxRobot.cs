using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.FanucRobot_v42_15;

namespace MvAssistant.Mac.TestMy.Device
{
    [TestClass]
    public class UtBoxRobot
    {
        /// <summary>box robot 點位存取測試</summary>
        [TestMethod]
        public void TestAccessPosReg()
        {
            // ldd: Logic Device Driver
            using (var ldd = new MvFanucRobotLdd())
            {
                // boxrobot 的IP
                ldd.RobotIp = "192.168.0.51";

                //是否連線
                if (ldd.ConnectIfNo() != 0)
                    Console.WriteLine("Connection Fail");

                // 讀取第1組暫停器內的資料robot 資訊
                var robotInfo = default(MvFanucRobotPosReg);
                robotInfo=ldd.ReadPosReg(1);
                // 將 x 軸數據累加1
                robotInfo.x += 1;
                robotInfo.e1 += 1;
                // 將各點位資料寫入第1組暫存器(機器不動)
                ldd.SetPosRegXyzWpr(1, robotInfo);

                // 讀取第1組暫存器內 robot 資料
                robotInfo = ldd.ReadPosReg(1);

                Console.WriteLine(robotInfo.x);
            }
        }


        /// <summary>Box robot 移動測試</summary>
        [TestMethod]
        public void TestMoveRobotPosition()
        {
            using (var ldd = new MvFanucRobotLdd())
            {

                ldd.RobotIp = "192.168.0.51";
                Assert.IsTrue(ldd.ConnectIfNo() == 0);

                Assert.IsTrue(ldd.AlarmReset());

                Assert.IsTrue(ldd.StopProgram() == 0);
                Assert.IsTrue(ldd.ExecutePNS("PNS0101"));


                var curr = ldd.ReadCurPosUf();

                curr.x += 1;
                ldd.Pns0101MoveStraightSync(curr.XyzwpreArrary, 0, 1, 0, 20);

                curr = ldd.ReadCurPosUf();
                Console.WriteLine(curr.x);

            }
        }
    }
}
