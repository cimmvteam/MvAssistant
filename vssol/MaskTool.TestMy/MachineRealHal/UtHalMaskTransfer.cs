using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalMaskTransfer
    {
        [TestMethod]
        public void TestPathMove()
        {

            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.Load();


                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;

                    if (mt.HalConnect() != 0)
                    {
                        System.Diagnostics.Debug.WriteLine("Connect Fail");
                    }

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

                var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;

                mt.SetClampTactileLim(50, 10);
                mt.SetLevelLimit(10, 11, 12);
                mt.SetSixAxisSensorLimit(1, 2, 3, 4, 5, 6);
                mt.SetSpeed(50, 60);
                mt.SetStaticElecLimit(50, 20);
            }
        }

        [TestMethod]
        public void TestReadParameter()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.Load();

                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    uni.HalConnect();
                    mt.HalConnect();

                    mt.ReadClampTactileLimSetting();
                    mt.ReadLevelLimitSetting();
                    mt.ReadSixAxisSensorLimitSetting();
                    mt.ReadSpeedSetting();
                    mt.ReadStaticElecLimitSetting();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void TestReadComponentValue()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;

                mt.ReadCCDSpinDegree();
                mt.ReadClampGripPos();
                mt.ReadClampTactile_FrontSide();
                mt.ReadClampTactile_BehindSide();
                mt.ReadClampTactile_LeftSide();
                mt.ReadClampTactile_RightSide();
                mt.ReadHandInspection();
                mt.ReadLevel();
                mt.ReadSixAxisSensor();
                mt.ReadStaticElec();
            }
        }

        [TestMethod]
        public void TestAssemblyWork()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;

                mt.Clamp(0);
                mt.Unclamp();
                //mt.CCDSpin(50);
                mt.Initial();
                mt.ReadMTRobotStatus();
                mt.RobotMoving(false);
            }
        }
    }
}
