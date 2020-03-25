﻿using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant;
using MvAssistant.MaskTool_v0_1.Plc;

namespace MaskTool.TestMy.MachineReal
{
    [TestClass]
    public class UtPlc
    {
        [TestMethod]
        public void TestPlcConnect()
        {

            using (var plc = new MvPlcContext())
            {
                plc.StartAsyn();

                if (!SpinWait.SpinUntil(() => plc.IsConnected, 60 * 1000))
                    throw new MvException("PLC connection fail");

                Console.WriteLine("PLC connection success");


                //while (true) System.Threading.Thread.Sleep(1000);

            }

        }

        public void TestPlcBoxRobot()
        {
            using (var plc = new MvPlcContext())
            {

                plc.StartAsyn();
                if (!SpinWait.SpinUntil(() => plc.IsConnected, 60 * 1000))
                    throw new MvException("PLC connection fail");

                Console.WriteLine(plc.BoxRobot.Clamp(0));// BoxType
                Console.WriteLine(plc.BoxRobot.Unclamp());
                Console.WriteLine(plc.BoxRobot.Initial());
                Console.WriteLine(plc.BoxRobot.CheckHandPos());
                Console.WriteLine(plc.BoxRobot.CheckBox());
                Console.WriteLine(plc.BoxRobot.CheckHandPosByLSR(1.1, 2.2));//  double*2
                Console.WriteLine(plc.BoxRobot.CheckClampLength(20.2));//  double*1
                Console.WriteLine(plc.BoxRobot.CheckLevelSensor(3.3, 4.4));//  double*2
                Console.WriteLine(plc.BoxRobot.CheckSixAxisSensor(1, 2, 3, 4, 5, 6));//  uinr*6
                Console.WriteLine(plc.BoxRobot.CheakHandVacuum());
            }
        }

        public void TestPlcOpenStage()
        {
            using (var plc = new MvPlcContext())
            {

                plc.StartAsyn();
                if (!SpinWait.SpinUntil(() => plc.IsConnected, 60 * 1000))
                    throw new MvException("PLC connection fail");

                Console.WriteLine(plc.OpenStage.Open());
                Console.WriteLine(plc.OpenStage.Close());
                Console.WriteLine(plc.OpenStage.Clamp());
                Console.WriteLine(plc.OpenStage.Unclamp());
                Console.WriteLine(plc.OpenStage.SortClamp());
                Console.WriteLine(plc.OpenStage.SortUnclamp());
                Console.WriteLine(plc.OpenStage.Lock(true));// T/F
                Console.WriteLine(plc.OpenStage.Initial());
                Console.WriteLine(plc.OpenStage.CheckRobotIntrude());
                Console.WriteLine(plc.OpenStage.CheckClampStatus());
                Console.WriteLine(plc.OpenStage.CheckSortClampPosition());
                Console.WriteLine(plc.OpenStage.CheckSliderPosition());
                Console.WriteLine(plc.OpenStage.CheckCoverPos());
                Console.WriteLine(plc.OpenStage.CheckCoverSensor());
                Console.WriteLine(plc.OpenStage.CheckBoxExist());
            }
        }

        [TestMethod]
        public void TestPlcInspCh()
        {
            using (var plc = new MvPlcContext())
            {
                plc.StartAsyn();
                if (!SpinWait.SpinUntil(() => plc.IsConnected, 60 * 1000))
                    throw new MvException("PLC connection fail");

                Console.WriteLine(plc.InspCh.XYPosition(20, 10));//X:300~-10,Y:250~-10
                Console.WriteLine(plc.InspCh.ZPosition(-10));//1~-85
                Console.WriteLine(plc.InspCh.WPosition(20));//0~359
                Console.WriteLine(plc.InspCh.Initial());
                Console.WriteLine(plc.InspCh.CheckRobotIntrude());
                Console.WriteLine(plc.InspCh.CheckXYPosition());
                Console.WriteLine(plc.InspCh.CheckZPosition());
                Console.WriteLine(plc.InspCh.CheckWPosition());
                Console.WriteLine(plc.InspCh.CheckRobotAbout(-10,10));
                Console.WriteLine(plc.InspCh.CheckRobotUpDown(10,0));
            }
        }
    }
}
