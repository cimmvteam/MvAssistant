using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_2;
using MvAssistant.v0_2.Mac.Hal.CompPlc;
using MvAssistant.v0_2.Mac.Xml;

namespace MvAssistant.v0_2.Mac.TestMy.ToolPlc
{
    [TestClass]
    public class UtPlc
    {
        public bool boolTestStop = false;

        [TestMethod]
        public void TestXmlEdit()
        {
            try
            {
                var doc = new XmlHelper();
                doc.CreatXml("D://project/github/MsakPosition.xml");
                doc.Insert("D://project/github/MsakPosition.xml", "58", "Position", "111.111", "122.222", "133.333", "144.444", "155.555", "166.666", "177.777");
                doc.Update("D://project/github/MsakPosition.xml", "58", "Joint", "111", "222", "333", "444", "555", "666", "777");
                doc.Delete("D://project/github/MsakPosition.xml", "58");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [TestMethod]
        public void TestPlcConnect()
        {

            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);


                plc.StartAsyn();

                if (!SpinWait.SpinUntil(() => plc.IsConnectedByHandShake, 60 * 1000))
                    throw new MvaException("PLC connection fail");

                Console.WriteLine("PLC connection success");


                //while (true) System.Threading.Thread.Sleep(1000);

            }

        }

        [TestMethod]
        public void TestPLCClosePort()//當發生PLC連線占用Port，嘗試關閉Port解決問題
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.ClosePort();
            }
        }

        [TestMethod]
        public void TestPublicArea()// OK
        {
            try
            {

                using (var plc = new MacHalPlcContext())
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
                    plc.EMSAlarm(true, false, false, false);// OK
                    plc.EMSAlarm(false, true, false, false);// OK
                    plc.EMSAlarm(false, false, true, false);// OK
                    plc.EMSAlarm(false, false, false, true);// OK
                    plc.EMSAlarm(false, false, false, false);// OK

                    Console.WriteLine(plc.ReadPowerON());// OK
                    Console.WriteLine(plc.ReadBCP_Maintenance());// BCP盤鑰匙鎖(主控盤)
                    Console.WriteLine(plc.ReadCB_Maintenance());// Cabinet鑰匙鎖
                    Console.WriteLine(plc.ReadBCP_EMO());// OK
                    Console.WriteLine(plc.ReadCB_EMO());// OK
                    Console.WriteLine(plc.ReadLP1_EMO());// OK
                    Console.WriteLine(plc.ReadLP2_EMO());// OK
                    Console.WriteLine(plc.ReadBCP_Door());// OK
                    Console.WriteLine(plc.ReadLP1_Door());// OK
                    Console.WriteLine(plc.ReadLP2_Door());// OK
                    Console.WriteLine(plc.ReadBCP_Smoke());// OK
                    Console.WriteLine(plc.ReadLP_Light_Curtain());// OK

                    plc.Eqp.ReadAlarm_General();
                    plc.Eqp.ReadAlarm_Cabinet();
                    plc.Eqp.ReadAlarm_CleanCh();
                    plc.Eqp.ReadAlarm_BTRobot();
                    plc.Eqp.ReadAlarm_MTRobot();
                    plc.Eqp.ReadAlarm_OpenStage();
                    plc.Eqp.ReadAlarm_InspCh();
                    plc.Eqp.ReadAlarm_LoadPort();
                    plc.Eqp.ReadAlarm_CoverFan();
                    plc.Eqp.ReadAlarm_MTClampInsp();

                    plc.Eqp.ReadWarning_General();
                    plc.Eqp.ReadWarning_Cabinet();
                    plc.Eqp.ReadWarning_CleanCh();
                    plc.Eqp.ReadWarning_BTRobot();
                    plc.Eqp.ReadWarning_MTRobot();
                    plc.Eqp.ReadWarning_OpenStage();
                    plc.Eqp.ReadWarning_InspCh();
                    plc.Eqp.ReadWarning_LoadPort();
                    plc.Eqp.ReadWarning_CoverFan();
                    plc.Eqp.ReadWarning_MTClampInsp();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void TestPlcBoxRobot()// OK
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.BoxTransfer.Clamp(1));
                Console.WriteLine(plc.BoxTransfer.Unclamp());
                Console.WriteLine(plc.BoxTransfer.Initial());
                plc.BoxTransfer.SetSpeedVar(10); //Speed：1~100mm/s
                Console.WriteLine(plc.BoxTransfer.ReadSpeedVar());
                Console.WriteLine(plc.BoxTransfer.ReadHandPos());
                Console.WriteLine(plc.BoxTransfer.ReadBoxDetect());
                plc.BoxTransfer.SetHandSpaceLimit(10, 20);
                Console.WriteLine(plc.BoxTransfer.ReadHandSpaceLimitSetting());
                Console.WriteLine(plc.BoxTransfer.ReadHandPosByLSR());
                plc.BoxTransfer.SetClampToCabinetSpaceLimit(10);
                Console.WriteLine(plc.BoxTransfer.ReadClampToCabinetSpaceLimitSetting());
                Console.WriteLine(plc.BoxTransfer.ReadClampDistance());
                plc.BoxTransfer.SetLevelSensorLimit(10, 15);
                Console.WriteLine(plc.BoxTransfer.ReadLevelSensorLimitSetting());
                Console.WriteLine(plc.BoxTransfer.ReadLevelSensor());
                Console.WriteLine(plc.BoxTransfer.LevelReset());
                plc.BoxTransfer.SetSixAxisSensorUpperLimit(10, 20, 30, 10, 10, 10);
                Console.WriteLine(plc.BoxTransfer.ReadSixAxisSensorUpperLimit());
                Console.WriteLine(plc.BoxTransfer.ReadSixAxisSensor());
                Console.WriteLine(plc.BoxTransfer.ReadHandVacuum());
                Console.WriteLine(plc.BoxTransfer.ReadBTStatus());
                plc.BoxTransfer.RobotMoving(false);// OK
            }
        }

        [TestMethod]
        public void TestPlcCabinet()//測試 OK
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.Cabinet.SetPressureDiffLimit(1, 2);
                Console.WriteLine(plc.Cabinet.ReadPressureDiffLimitSetting());
                Console.WriteLine(plc.Cabinet.ReadPressureDiff());
                plc.Cabinet.SetExhaustFlow(3, 4);
                Console.WriteLine(plc.Cabinet.ReadExhaustFlowSetting());
                Console.WriteLine(plc.Cabinet.ReadLightCurtain());
            }
        }

        [TestMethod]
        public void TestPlcCleanCh()//測試 OK
        {
            using (var plc = new MacHalPlcContext())
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
                Console.WriteLine(plc.CleanCh.ReadLightCurtain());
            }
        }

        [TestMethod]
        public void TestPlcInspCh()//測試 OK
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                //bool[] AlarmArray = new bool[256];

                Console.WriteLine(plc.InspectionCh.XYPosition(20, 10));//X:300~-10,Y:250~-10
                Console.WriteLine(plc.InspectionCh.ZPosition(-10));//1~-85
                Console.WriteLine(plc.InspectionCh.WPosition(20));//0~359
                Console.WriteLine(plc.InspectionCh.Initial());
                plc.InspectionCh.SetSpeedVar(10, 10, 10);
                Console.WriteLine(plc.InspectionCh.ReadSpeedVar());
                Console.WriteLine(plc.InspectionCh.SetRobotIntrude(true));
                Console.WriteLine(plc.InspectionCh.ReadXYPosition());
                Console.WriteLine(plc.InspectionCh.ReadZPosition());
                Console.WriteLine(plc.InspectionCh.ReadWPosition());
                plc.InspectionCh.SetRobotPosLeftRightLimit(-10, 10);
                Console.WriteLine(plc.InspectionCh.ReadRobotPosLeftRightLimit());
                Console.WriteLine(plc.InspectionCh.ReadRobotPosLeftRight());
                plc.InspectionCh.SetRobotPosUpDownLimit(10, 0);
                Console.WriteLine(plc.InspectionCh.ReadRobotPosUpDownLimit());
                Console.WriteLine(plc.InspectionCh.ReadRobotPosUpDown());
                Console.WriteLine(plc.InspectionCh.ReadICStatus());

                //AlarmArray = plc.InspCh.ReadAlarmArray();
                //Console.WriteLine(plc.InspCh.ReadAlarmArray());
            }
        }

        [TestMethod]
        public void TestPlcLoadPort()//測試 OK
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);

                plc.LoadPort.SetPressureDiffLimit(1, 2);
                Console.WriteLine(plc.LoadPort.ReadChamberPressureDiffLimit());
                Console.WriteLine(plc.LoadPort.ReadPressureDiff());
            }
        }

        [TestMethod]
        public void TestPlcMaskRobot()// OK
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                try
                {
                    Console.WriteLine(plc.MaskTransfer.Clamp(1));
                    Console.WriteLine(plc.MaskTransfer.Unclamp());
                    Console.WriteLine(plc.MaskTransfer.Initial());
                    plc.MaskTransfer.SetSpeedVar(6, 8);
                    Console.WriteLine(plc.MaskTransfer.ReadSpeedVar());
                    Console.WriteLine(plc.MaskTransfer.ReadClampGripPos());
                    plc.MaskTransfer.CCDSpin(1000);  // 待測  已拆除(討論要移除)
                    Console.WriteLine(plc.MaskTransfer.ReadCCDSpinDegree());
                    plc.MaskTransfer.SetSixAxisSensorUpperLimit(10, 20, 30, 10, 10, 10);
                    Console.WriteLine(plc.MaskTransfer.ReadSixAxisSensorUpperLimit());
                    Console.WriteLine(plc.MaskTransfer.ReadSixAxisSensor());
                    plc.MaskTransfer.SetClampTactileLim(15, 10);// OK
                    Console.WriteLine(plc.MaskTransfer.ReadClampTactileLimit());// OK
                    Console.WriteLine(plc.MaskTransfer.ReadClampTactile_FrontSide());// OK
                    Console.WriteLine(plc.MaskTransfer.ReadClampTactile_BehindSide());// OK
                    Console.WriteLine(plc.MaskTransfer.ReadClampTactile_LeftSide());// OK
                    Console.WriteLine(plc.MaskTransfer.ReadClampTactile_RightSide());// OK
                    plc.MaskTransfer.SetLevelLimit(15, 10, 5);// OK
                    Console.WriteLine(plc.MaskTransfer.ReadLevelLimit());// OK
                    Console.WriteLine(plc.MaskTransfer.ReadLevel());// OK
                    plc.MaskTransfer.SetStaticElecLimit(20, 10);
                    Console.WriteLine(plc.MaskTransfer.ReadStaticElecLimit());
                    Console.WriteLine(plc.MaskTransfer.ReadStaticElec());
                    Console.WriteLine(plc.MaskTransfer.ReadMTStatus());
                    Console.WriteLine(plc.MaskTransfer.ReadHandInspection());
                    plc.MaskTransfer.RobotMoving(false);//OK
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        [TestMethod]
        public void TestPlcOpenStage()//OK
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                if (plc.IsConnected)
                    try
                    {
                        Console.WriteLine(plc.OpenStage.Open());
                        Console.WriteLine(plc.OpenStage.Close());
                        Console.WriteLine(plc.OpenStage.Clamp());
                        Console.WriteLine(plc.OpenStage.Unclamp());
                        Console.WriteLine(plc.OpenStage.SortClamp());
                        Console.WriteLine(plc.OpenStage.SortUnclamp());
                        Console.WriteLine(plc.OpenStage.Lock());
                        Console.WriteLine(plc.OpenStage.Vacuum(false));//OK
                        Console.WriteLine(plc.OpenStage.Initial());
                        plc.OpenStage.SetBoxTypeVar(1);
                        Console.WriteLine(plc.OpenStage.ReadBoxTypeVar());
                        plc.OpenStage.SetSpeedVar(50);//OK
                        Console.WriteLine(plc.OpenStage.ReadSpeedVar()); //OK
                        Console.WriteLine(plc.OpenStage.SetRobotIntrude(false, false));
                        Console.WriteLine(plc.OpenStage.ReadClampStatus());
                        Console.WriteLine(plc.OpenStage.ReadSortClampPosition());
                        Console.WriteLine(plc.OpenStage.ReadSliderPosition());
                        Console.WriteLine(plc.OpenStage.ReadCoverPos());
                        Console.WriteLine(plc.OpenStage.ReadCoverSensor());
                        Console.WriteLine(plc.OpenStage.ReadBoxDeform());
                        Console.WriteLine(plc.OpenStage.ReadWeightOnStage());
                        Console.WriteLine(plc.OpenStage.ReadBoxExist());
                        Console.WriteLine(plc.OpenStage.ReadOSStatus());
                        Console.WriteLine(plc.OpenStage.ReadRobotIntruded());
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                else
                    throw new Exception("Can not connect to PLC device !!");
            }
        }

        [TestMethod]
        public void TestPlcOpenStageFlow()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                boolTestStop = false;
                plc.OpenStage.SetBoxTypeVar(1);//鐵盒：1，水晶盒：2
                Console.WriteLine(plc.OpenStage.SetRobotIntrude(false, false));
                Console.WriteLine(plc.OpenStage.Initial());
                for (int i = 0; i < 1; i++)
                //while (boolTestStop == false)
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
                    Console.WriteLine(plc.OpenStage.SetRobotIntrude(true, false));
                    Console.WriteLine(plc.OpenStage.SetRobotIntrude(false, false));
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

        [TestMethod]
        public void TestPlcInspChFlow()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                //bool[] AlarmArray = new bool[256];

                Console.WriteLine(plc.InspectionCh.SetRobotIntrude(false));
                Console.WriteLine(plc.InspectionCh.Initial());
                plc.InspectionCh.SetSpeedVar(100, 50, 500);
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(plc.InspectionCh.XYPosition(200, 10));//X:300~-10,Y:250~-10  左下
                    Console.WriteLine(plc.InspectionCh.WPosition(52));//0~359
                    Console.WriteLine(plc.InspectionCh.XYPosition(10, 10));//X:300~-10,Y:250~-10  右下
                    Console.WriteLine(plc.InspectionCh.XYPosition(10, 150));//X:300~-10,Y:250~-10  右上

                    Console.WriteLine(plc.InspectionCh.XYPosition(200, 150));//X:300~-10,Y:250~-10  左上
                    Console.WriteLine(plc.InspectionCh.ZPosition(-10));//1~-85
                    Console.WriteLine(plc.InspectionCh.ZPosition(-50));//1~-85
                }
            }
        }
    }
}
