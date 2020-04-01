using System;
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

        [TestMethod]
        public void TestPublicArea()
        {
            using (var plc = new MvPlcContext())
            {
                plc.SetSignalTower(true, false, false);
                plc.SetBuzzer(1);
            }
        }

        [TestMethod]
        public void TestPlcBoxRobot()
        {
            using (var plc = new MvPlcContext())
            {
                Console.WriteLine(plc.BoxRobot.Clamp(0));
                Console.WriteLine(plc.BoxRobot.Unclamp());
                Console.WriteLine(plc.BoxRobot.Initial());
                //Console.WriteLine(plc.BoxRobot.SetCommand());
                Console.WriteLine(plc.BoxRobot.ReadHandPos());
                Console.WriteLine(plc.BoxRobot.ReadBoxDetect());
                plc.BoxRobot.SetHandSpaceLimit(10, 20);
                Console.WriteLine(plc.BoxRobot.ReadHandSpaceLimitSetting());
                Console.WriteLine(plc.BoxRobot.ReadHandPosByLSR());
                plc.BoxRobot.SetClampToCabinetSpaceLimit(10);
                Console.WriteLine(plc.BoxRobot.ReadClampToCabinetSpaceLimitSetting());
                Console.WriteLine(plc.BoxRobot.ReadClampDistance());
                plc.BoxRobot.SetLevelSensorLimit(0, 0);
                Console.WriteLine(plc.BoxRobot.ReadLevelSensorLimitSetting());
                Console.WriteLine(plc.BoxRobot.ReadLevelSensor());
                plc.BoxRobot.SetSixAxisSensorLimit(10, 20, 30, 10, 10, 10);
                Console.WriteLine(plc.BoxRobot.ReadSixAxisSensorLimitSetting());
                Console.WriteLine(plc.BoxRobot.ReadSixAxisSensor());
                Console.WriteLine(plc.BoxRobot.ReadHandVacuum());
            }
        }

        [TestMethod]
        public void TestPlcCabinet()
        {
            using (var plc = new MvPlcContext())
            {
                plc.Cabinet.SetPressureDiffLimit(1, 2);
                Console.WriteLine(plc.Cabinet.ReadPressureDiffLimitSetting());
                Console.WriteLine(plc.Cabinet.ReadPressureDiff());
                plc.Cabinet.SetExhaustFlow(3, 4);
                Console.WriteLine(plc.Cabinet.ReadExhaustFlowSetting());
                Console.WriteLine(plc.Cabinet.ReadExhaustFlow());
                Console.WriteLine(plc.Cabinet.ReadAreaSensor());
            }
        }

        [TestMethod]
        public void TestPlcCleanCh()
        {
            using (var plc = new MvPlcContext())
            {
                plc.CleanCh.SetParticleCntLimit(1, 2, 3);
                Console.WriteLine(plc.CleanCh.ReadParticleCntLimitSetting());
                Console.WriteLine(plc.CleanCh.ReadParticleCount());
                Console.WriteLine(plc.CleanCh.ReadMaskLevel());
                plc.CleanCh.SetRobotAboutLimit(10, -10);
                Console.WriteLine(plc.CleanCh.ReadRobotAboutLimitSetting());
                Console.WriteLine(plc.CleanCh.ReadRobotPosAbout());
                plc.CleanCh.SetRobotUpDownLimit(10, -10);
                Console.WriteLine(plc.CleanCh.ReadRobotUpDownLimitSetting());
                Console.WriteLine(plc.CleanCh.ReadRobotPosUpDown());
                plc.CleanCh.SetPressureDiffLimit(2);
                Console.WriteLine(plc.CleanCh.ReadPressureDiffLimitSetting());
                Console.WriteLine(plc.CleanCh.ReadPressureDiff());
                Console.WriteLine(plc.CleanCh.GasValveBlow(3));
                plc.CleanCh.SetPressureCtrl(10);
                Console.WriteLine(plc.CleanCh.ReadPressureCtrlSetting());
                Console.WriteLine(plc.CleanCh.ReadBlowPressure());
                Console.WriteLine(plc.CleanCh.ReadPressure());
                Console.WriteLine(plc.CleanCh.ReadAreaSensor());
            }
        }

        [TestMethod]
        public void TestPlcInspCh()
        {
            using (var plc = new MvPlcContext())
            {
                Console.WriteLine(plc.InspCh.XYPosition(20, 10));//X:300~-10,Y:250~-10
                Console.WriteLine(plc.InspCh.ZPosition(-10));//1~-85
                Console.WriteLine(plc.InspCh.WPosition(20));//0~359
                Console.WriteLine(plc.InspCh.Initial());
                //Console.WriteLine(plc.InspCh.SetCommand());
                Console.WriteLine(plc.InspCh.ReadRobotIntrude());
                Console.WriteLine(plc.InspCh.ReadXYPosition());
                Console.WriteLine(plc.InspCh.ReadZPosition());
                Console.WriteLine(plc.InspCh.ReadWPosition());
                plc.InspCh.SetRobotAboutLimit(-10, 10);
                Console.WriteLine(plc.InspCh.ReadRobotAboutLimitSetting());
                Console.WriteLine(plc.InspCh.ReadRobotPosAbout());
                plc.InspCh.SetRobotUpDownLimit(10, 0);
                Console.WriteLine(plc.InspCh.ReadRobotUpDownLimitSetting());
                Console.WriteLine(plc.InspCh.ReadRobotPosUpDown());
            }
        }

        [TestMethod]
        public void TestPlcLoadPort()
        {
            using (var plc = new MvPlcContext())
            {
                plc.LoadPort.SetPressureDiffLimit(1, 2);
                Console.WriteLine(plc.LoadPort.ReadPressureDiffLimitSrtting());
                Console.WriteLine(plc.LoadPort.ReadPressureDiff());
            }
        }

        [TestMethod]
        public void TestPlcMaskRobot()
        {
            using (var plc = new MvPlcContext())
            {
                Console.WriteLine(plc.MaskRobot.Initial());
                Console.WriteLine(plc.MaskRobot.ReadHandInspection());
            }
        }

        [TestMethod]
        public void TestPlcOpenStage()//Function各別測試OK
        {
            using (var plc = new MvPlcContext())
            {
                Console.WriteLine(plc.OpenStage.Open());
                Console.WriteLine(plc.OpenStage.Lock());
                Console.WriteLine(plc.OpenStage.Close());
                Console.WriteLine(plc.OpenStage.Clamp());
                Console.WriteLine(plc.OpenStage.Unclamp());
                Console.WriteLine(plc.OpenStage.SortClamp());
                Console.WriteLine(plc.OpenStage.SortUnclamp());
                Console.WriteLine(plc.OpenStage.Initial());
                //Console.WriteLine(plc.OpenStage.SetCommand());
                Console.WriteLine(plc.OpenStage.ReadRobotIntrude(true, false));
                Console.WriteLine(plc.OpenStage.ReadClampStatus());
                Console.WriteLine(plc.OpenStage.ReadSortClampPosition());
                Console.WriteLine(plc.OpenStage.ReadSliderPosition());
                Console.WriteLine(plc.OpenStage.ReadCoverPos());
                Console.WriteLine(plc.OpenStage.ReadCoverSensor());
                Console.WriteLine(plc.OpenStage.ReadBoxExist());
            }
        }

        [TestMethod]
        public void TestPlcOpenStageFlow()//Test OK
        {
            using (var plc = new MvPlcContext())
            {
                Console.WriteLine(plc.OpenStage.Initial());
                for (int i = 0; i < 1; i++)
                //while (true)
                {
                    Console.WriteLine(plc.OpenStage.SortClamp());
                    Console.WriteLine(plc.OpenStage.SortUnclamp());
                    Console.WriteLine(plc.OpenStage.Close());
                    Console.WriteLine(plc.OpenStage.Clamp());
                    Console.WriteLine(plc.OpenStage.Open());
                    Console.WriteLine(plc.OpenStage.ReadRobotIntrude(true, false));//Mask Robot入侵將MTIntrude訊號改為False
                    Console.WriteLine(plc.OpenStage.ReadRobotIntrude(true, true));//沒有Robot入侵時，將訊號改為True
                    Console.WriteLine(plc.OpenStage.Close());
                    Console.WriteLine(plc.OpenStage.Unclamp());
                    Console.WriteLine(plc.OpenStage.Lock());
                }
                //Console.WriteLine(plc.OpenStage.CheckClampStatus());
                //Console.WriteLine(plc.OpenStage.CheckSortClampPosition());
                //Console.WriteLine(plc.OpenStage.CheckSliderPosition());
                //Console.WriteLine(plc.OpenStage.CheckCoverPos());
                //Console.WriteLine(plc.OpenStage.CheckCoverSensor());
                //Console.WriteLine(plc.OpenStage.CheckBoxExist());
            }
        }
    }
}
