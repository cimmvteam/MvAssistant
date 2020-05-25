using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{

    [TestClass]
    public class UtHalBoxTransfer
    {
        [TestMethod]
        public void TestPathMove()
        {
            // BackHome
            // ChangeDirectionFaceDrawer1
            // HomeToDrawer1 #1~#20
            // Drawer1 To Home
            // ChangeDirectionFaceDrawer2 
            // HomeToDrawer2 #1~#15
            // Drawer2ToHome 
            // ChangeDirectionFaceOpenStage
            // HomeToOpenStage
            // OpenStageToHome
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.Load();


                    var mt = halContext.HalDevices[MacEnumDevice.boxtransfer_plc.ToString()] as MacHalBoxTransfer;

                    if (mt.HalConnect() != 0)
                    {
                        System.Diagnostics.Debug.WriteLine("Connect Fail");
                    }

                    /*
                    mt.RobotMove(mt.HomeToOpenStage());
                    mt.RobotMove(mt.OpenStageToHome());
                    mt.ChangeDirection(mt.PosToInspCh());
                    mt.RobotMove(mt.FrontSideIntoInspCh());
                    mt.RobotMove(mt.FrontSideLeaveInspCh());
                    mt.RobotMove(mt.BackSideIntoInspCh());
                    mt.RobotMove(mt.BackSideLeaveInspCh());
                    mt.ChangeDirection(mt.PosToCleanCh());
                    mt.RobotMove(mt.BackSideClean());
                    mt.RobotMove(mt.FrontSideClean());
                    mt.RobotMove(mt.FrontSideCCDTakeImage());
                    mt.RobotMove(mt.BackSideCCDTakeImage());
                    mt.ChangeDirection(mt.PosHome());
                    */
                }
            }
            catch (Exception ex) { throw ex; }

        }
    

        [TestMethod]
        public void TestSetParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var bt = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalBoxTransfer;

                bt.SetSpeed(20);
                bt.SetHandSpaceLimit(10, 20);
                bt.SetClampToCabinetSpaceLimit(50);
                bt.SetLevelSensorLimit(5, 6);
                bt.SetSixAxisSensorLimit(1, 2, 3, 4, 5, 6);
            }
        }

        [TestMethod]
        public void TestReadParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var bt = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalBoxTransfer;

                bt.ReadSpeedSetting();
                bt.ReadHandSpaceLimitSetting();
                bt.ReadClampToCabinetSpaceLimitSetting();
                bt.ReadLevelSensorLimitSetting();
                bt.ReadSixAxisSensorLimitSetting();
            }
        }

        [TestMethod]
        public void TestReadComponentValue()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var bt = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalBoxTransfer;

                bt.ReadHandPos();
                bt.ReadBoxDetect();
                bt.ReadHandPosByLSR();
                bt.ReadClampDistance();
                bt.ReadLevelSensor();
                bt.ReadSixAxisSensor();
                bt.ReadHandVacuum();
            }
        }

        [TestMethod]
        public void TestAssemblyWork()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var bt = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalBoxTransfer;

                bt.Clamp(1);
                bt.Unclamp();
                bt.LevelReset();
                bt.ReadBTRobotStatus();
                bt.RobotMoving(false);
                bt.Initial();
            }
        }
    }
}
