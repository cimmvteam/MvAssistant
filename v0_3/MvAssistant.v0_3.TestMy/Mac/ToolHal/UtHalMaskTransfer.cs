﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_3.Mac.Hal;
using MvAssistant.v0_3.Mac.Hal.Assembly;
using MvAssistant.v0_3.Mac.Manifest;
using System;

namespace MvAssistant.v0_3.Mac.TestMy.ToolHal
{
    [TestClass]
    public class UtHalMaskTransfer
    {
        #region PLC HAL
        [TestMethod]
        public void TestSetParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvaCfLoad();

                var mt = halContext.HalDevices[EnumMacDeviceId.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                var uni = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                uni.HalConnect();
                mt.HalConnect();

                mt.SetClampTactileLim(50, 10);
                mt.SetLevelLimit(10, 11, 12);
                mt.SetSixAxisSensorUpperLimit(10.1, 20.2, 30.3, 40.4, 50.5, 60.6);
                mt.SetSixAxisSensorLowerLimit(1.1, 2.2, 3.3, 4.4, 5.5, 6.6);
                mt.SetSpeed(5, 1000);
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
                    halContext.MvaCfLoad();

                    var mt = halContext.HalDevices[EnumMacDeviceId.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var uni = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                    uni.HalConnect();
                    mt.HalConnect();

                    mt.ReadClampTactileLimit();
                    mt.ReadLevelLimit();
                    mt.ReadSixAxisSensorUpperLimit();
                    mt.ReadSixAxisSensorLowerLimit();
                    mt.ReadSpeedVar();
                    mt.ReadStaticElecLimit();
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
                halContext.MvaCfLoad();

                var mt = halContext.HalDevices[EnumMacDeviceId.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                var uni = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                uni.HalConnect();
                mt.HalConnect();

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
                halContext.MvaCfLoad();

                var mt = halContext.HalDevices[EnumMacDeviceId.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                var uni = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                uni.HalConnect();
                mt.HalConnect();

                mt.Clamp(0);
                mt.Unclamp();
                mt.CCDSpin(2000);
                mt.Initial();
                mt.ReadMTStatus();
                mt.RobotMoving(false);
            }
        }
        #endregion
    }
}
