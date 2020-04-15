using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant;
using MvAssistant.MaskTool_v0_1.Plc;

namespace MaskTool.TestMy.MachineReal
{


    //[20200401] Merge
    [TestClass]
    public class UtPlc
    {
        public bool boolTestStop = false;
        [TestMethod]
        public void TestPlcConnect()
        {

            using (var plc = new MvPlcContext())
            {
                plc.Connect("192.168.0.200", 2);


                plc.StartAsyn();

                if (!SpinWait.SpinUntil(() => plc.IsConnected, 60 * 1000))
                    throw new MvException("PLC connection fail");

                Console.WriteLine("PLC connection success");


                //while (true) System.Threading.Thread.Sleep(1000);

            }

        }

        [TestMethod]
        public void TestPLCClosePort()//當發生PLC連線占用Port，嘗試關閉Port解決問題
        {
            using (var plc = new MvPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.ClosePort();
            }
        }

        [TestMethod]
        public void TestPublicArea()//測試OK
        {
            using (var plc = new MvPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.ResetAll();
                plc.SetSignalTower(true, false, false);
                plc.SetSignalTower(false, true, false);
                plc.SetSignalTower(false, false, true);
                plc.SetBuzzer(1);
                plc.SetBuzzer(2);
                plc.SetBuzzer(3);
                plc.SetBuzzer(4);
                plc.SetBuzzer(0);
                for (uint i = 1; i < 13; i++)
                {
                    plc.CoverFanCtrl(i, (600));
                }
                Console.WriteLine(plc.ReadCoverFanSpeed());
            }
        }

        [TestMethod]
        public void TestPlcBoxRobot() //測試 OK
        {
            using (var plc = new MvPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.BoxRobot.Clamp(1));
                Console.WriteLine(plc.BoxRobot.Unclamp());
                Console.WriteLine(plc.BoxRobot.Initial());
                plc.BoxRobot.SetSpeed(10); //Speed：1~100mm/s
                Console.WriteLine(plc.BoxRobot.ReadSpeedSetting());
                Console.WriteLine(plc.BoxRobot.ReadHandPos());
                Console.WriteLine(plc.BoxRobot.ReadBoxDetect());
                plc.BoxRobot.SetHandSpaceLimit(10, 20);
                Console.WriteLine(plc.BoxRobot.ReadHandSpaceLimitSetting());
                Console.WriteLine(plc.BoxRobot.ReadHandPosByLSR());
                plc.BoxRobot.SetClampToCabinetSpaceLimit(10);
                Console.WriteLine(plc.BoxRobot.ReadClampToCabinetSpaceLimitSetting());
                Console.WriteLine(plc.BoxRobot.ReadClampDistance());
                plc.BoxRobot.SetLevelSensorLimit(10, 15);
                Console.WriteLine(plc.BoxRobot.ReadLevelSensorLimitSetting());
                Console.WriteLine(plc.BoxRobot.ReadLevelSensor());
                Console.WriteLine(plc.BoxRobot.SetLevelReset());
                plc.BoxRobot.SetSixAxisSensorLimit(10, 20, 30, 10, 10, 10);
                Console.WriteLine(plc.BoxRobot.ReadSixAxisSensorLimitSetting());
                Console.WriteLine(plc.BoxRobot.ReadSixAxisSensor());
                Console.WriteLine(plc.BoxRobot.ReadHandVacuum());
                Console.WriteLine(plc.BoxRobot.ReadBTRobotStatus());
            }
        }

        [TestMethod]
        public void TestPlcCabinet()//測試OK
        {
            using (var plc = new MvPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.Cabinet.SetPressureDiffLimit(1, 2);
                Console.WriteLine(plc.Cabinet.ReadPressureDiffLimitSetting());
                Console.WriteLine(plc.Cabinet.ReadPressureDiff());
                plc.Cabinet.SetExhaustFlow(3, 4);
                Console.WriteLine(plc.Cabinet.ReadExhaustFlowSetting());
                Console.WriteLine(plc.Cabinet.ReadExhaustFlow());//硬體應有另一個實際值，待測
                Console.WriteLine(plc.Cabinet.ReadAreaSensor());
            }
        }

        [TestMethod]
        public void TestPlcCleanCh()//Test OK
        {
            using (var plc = new MvPlcContext())
            {
                plc.Connect("192.168.0.200", 2);

                //plc.CleanCh.SetParticleCntLimit(1, 2, 3);
                //Console.WriteLine(plc.CleanCh.ReadParticleCntLimitSetting());
                //Console.WriteLine(plc.CleanCh.ReadParticleCount());
                //Console.WriteLine(plc.CleanCh.ReadMaskLevel());
                //plc.CleanCh.SetRobotAboutLimit(10, -10);
                //Console.WriteLine(plc.CleanCh.ReadRobotAboutLimitSetting());
                //Console.WriteLine(plc.CleanCh.ReadRobotPosAbout());
                //plc.CleanCh.SetRobotUpDownLimit(10, -10);
                //Console.WriteLine(plc.CleanCh.ReadRobotUpDownLimitSetting());
                //Console.WriteLine(plc.CleanCh.ReadRobotPosUpDown());
                //plc.CleanCh.SetPressureDiffLimit(2);
                //Console.WriteLine(plc.CleanCh.ReadPressureDiffLimitSetting());
                //Console.WriteLine(plc.CleanCh.ReadPressureDiff());
                Console.WriteLine(plc.CleanCh.GasValveBlow(20));
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
                plc.Connect("192.168.0.200", 2);
                //bool[] AlarmArray = new bool[256];

                Console.WriteLine(plc.InspCh.XYPosition(20, 10));//X:300~-10,Y:250~-10
                Console.WriteLine(plc.InspCh.ZPosition(-10));//1~-85
                Console.WriteLine(plc.InspCh.WPosition(20));//0~359
                Console.WriteLine(plc.InspCh.Initial());
                plc.InspCh.SetSpeed(10, 10, 10);
                Console.WriteLine(plc.InspCh.ReadSpeedSetting());
                Console.WriteLine(plc.InspCh.ReadRobotIntrude(true));
                Console.WriteLine(plc.InspCh.ReadXYPosition());
                Console.WriteLine(plc.InspCh.ReadZPosition());
                Console.WriteLine(plc.InspCh.ReadWPosition());
                plc.InspCh.SetRobotAboutLimit(-10, 10);
                Console.WriteLine(plc.InspCh.ReadRobotAboutLimitSetting());
                Console.WriteLine(plc.InspCh.ReadRobotPosAbout());
                plc.InspCh.SetRobotUpDownLimit(10, 0);
                Console.WriteLine(plc.InspCh.ReadRobotUpDownLimitSetting());
                Console.WriteLine(plc.InspCh.ReadRobotPosUpDown());
                Console.WriteLine(plc.InspCh.ReadInspChStatus());

                //AlarmArray = plc.InspCh.ReadAlarmArray();
                //Console.WriteLine(plc.InspCh.ReadAlarmArray());
            }
        }

        [TestMethod]
        public void TestPlcLoadPort()//Test OK
        {
            using (var plc = new MvPlcContext())
            {
                plc.Connect("192.168.0.200", 2);

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
                plc.Connect("192.168.0.200", 2);

                //Console.WriteLine(plc.MaskRobot.Clamp(0));
                //Console.WriteLine(plc.MaskRobot.Unclamp());
                //Console.WriteLine(plc.MaskRobot.Initial());
                //plc.MaskRobot.SetSpeed(10);
                //Console.WriteLine(plc.MaskRobot.ReadSpeedSetting());
                //plc.MaskRobot.SetStaticElecLimit(10, 20);
                //Console.WriteLine(plc.MaskRobot.ReadStaticElecLimitSetting());
                //Console.WriteLine(plc.MaskRobot.ReadStaticElec());
                //Console.WriteLine(plc.MaskRobot.ReadMTRobotStatus());
                Console.WriteLine(plc.MaskRobot.ReadHandInspection());//OK
            }
        }

        [TestMethod]
        public void TestPlcOpenStage()//Function各別測試OK
        {
            using (var plc = new MvPlcContext())
            {
                plc.Connect("192.168.0.200", 2);

                //Console.WriteLine(plc.OpenStage.Open());
                //Console.WriteLine(plc.OpenStage.Close());
                //Console.WriteLine(plc.OpenStage.Clamp());
                //Console.WriteLine(plc.OpenStage.Unclamp());
                //Console.WriteLine(plc.OpenStage.SortClamp());
                //Console.WriteLine(plc.OpenStage.SortUnclamp());
                //Console.WriteLine(plc.OpenStage.Lock());
                //Console.WriteLine(plc.OpenStage.Initial());
                plc.OpenStage.SetBoxType(0);
                Console.WriteLine(plc.OpenStage.ReadBoxTypeSetting());
                Console.WriteLine(plc.OpenStage.ReadRobotIntrude(true, true));
                Console.WriteLine(plc.OpenStage.ReadClampStatus());
                Console.WriteLine(plc.OpenStage.ReadSortClampPosition());
                Console.WriteLine(plc.OpenStage.ReadSliderPosition());
                Console.WriteLine(plc.OpenStage.ReadCoverPos());
                Console.WriteLine(plc.OpenStage.ReadCoverSensor());
                Console.WriteLine(plc.OpenStage.ReadBoxDeform());
                Console.WriteLine(plc.OpenStage.ReadWeightOnStage());
                Console.WriteLine(plc.OpenStage.ReadBoxExist());
                Console.WriteLine(plc.OpenStage.ReadOpenStageStatus());
            }
        }

        [TestMethod]
        public void TestPlcOpenStageFlow()//測試 OK
        {
            using (var plc = new MvPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                boolTestStop = false;
                plc.OpenStage.SetBoxType(1);//鐵盒：1，水晶盒：2
                Console.WriteLine(plc.OpenStage.Initial());
                //for (int i = 0; i < 1; i++)
                while (boolTestStop == false)
                {
                    Console.WriteLine(plc.OpenStage.SortClamp());
                    if (boolTestStop) break;
                    Console.WriteLine(plc.OpenStage.SortUnclamp());
                    if (boolTestStop) break;
                    Console.WriteLine(plc.OpenStage.Close());
                    if (boolTestStop) break;
                    Console.WriteLine(plc.OpenStage.Clamp());
                    if (boolTestStop) break;
                    Console.WriteLine(plc.OpenStage.Open());
                    if (boolTestStop) break;
                    Console.WriteLine(plc.OpenStage.ReadRobotIntrude(true, false));//Mask Robot入侵將MTIntrude訊號改為False
                    Console.WriteLine(plc.OpenStage.ReadRobotIntrude(true, true));//沒有Robot入侵時，將訊號改為True
                    if (boolTestStop) break;
                    Console.WriteLine(plc.OpenStage.Close());
                    if (boolTestStop) break;
                    Console.WriteLine(plc.OpenStage.Unclamp());
                    if (boolTestStop) break;
                    Console.WriteLine(plc.OpenStage.Lock());
                }
                //Console.WriteLine(plc.OpenStage.ReadClampStatus());
                //Console.WriteLine(plc.OpenStage.ReadSortClampPosition());
                //Console.WriteLine(plc.OpenStage.ReadSliderPosition());
                //Console.WriteLine(plc.OpenStage.ReadCoverPos());
                //Console.WriteLine(plc.OpenStage.ReadCoverSensor());
                //Console.WriteLine(plc.OpenStage.ReadBoxExist());
            }
        }
    }
}
