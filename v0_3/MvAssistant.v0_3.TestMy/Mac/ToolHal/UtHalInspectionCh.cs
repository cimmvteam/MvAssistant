﻿using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_3.Mac.Hal;
using MvAssistant.v0_3.Mac.Hal.Assembly;
using MvAssistant.v0_3.Mac.Manifest;

namespace MvAssistant.v0_3.Mac.TestMy.ToolHal
{
    [TestClass]
    public class UtHalInspectionCh
    {
        [TestMethod]
        public void TestCamera()
        {

            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvaCfLoad();


                var ic = halContext.HalDevices[EnumMacDeviceId.inspectionch_assembly.ToString()] as MacHalInspectionCh;
                var uni = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                uni.HalConnect();
                ic.HalConnect();

                for (double i = -43; i > -46; i -= 0.01)
                {
                    ic.ZPosition(i);
                    Thread.Sleep(1000);
                    ic.Camera_TopInsp_CapToSave("D:/Image/IC/TopInsp", "bmp");
                    Thread.Sleep(2000);
                }
                //ic.Camera_SideDfs_CapToSave("D:/Image/IC/SideDfs", "jpg");
                //ic.Camera_SideInsp_CapToSave("D:/Image/IC/SideInsp", "jpg");
                //ic.Camera_TopDfs_CapToSave("D:/Image/IC/TopDfs", "jpg");
            }
        }

        [TestMethod]
        public void TestSetParameter()
        {

            using (var halContext = new MacHalContext("UserData/Manifest/Manifest.xml.real"))
            {
                halContext.MvaCfBootup();
                halContext.MvaCfLoad();

                var ic = halContext.HalDevices[EnumMacDeviceId.inspectionch_assembly.ToString()] as MacHalInspectionCh;
                var uni = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                uni.HalConnect();
                ic.HalConnect();

                ic.SetSpeedVar(200, 100, 50);
                ic.SetRobotPosLeftRightLimit(10, 100);
                ic.SetRobotPosUpDownLimit(10, -20);
                ic.SetParticleCntLimit(15,25,35);
                ic.SetPressureDiffLimit(20);
            }
        }

        [TestMethod]
        public void TestReadParameter()
        {

            using (var halContext = new MacHalContext("UserData/Manifest/Manifest.xml.real"))
            {
                halContext.MvaCfBootup();
                halContext.MvaCfLoad();

                var ic = halContext.HalDevices[EnumMacDeviceId.inspectionch_assembly.ToString()] as MacHalInspectionCh;
                var uni = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                uni.HalConnect();
                ic.HalConnect();

                ic.ReadSpeedVar();
                ic.ReadRobotPosLeftRightLimit();
                ic.ReadRobotPosUpDownLimit();
                ic.ReadParticleCntLimit();
                ic.ReadChamberPressureDiffLimit();
            }
        }

        [TestMethod]
        public void TestReadComponentValue()
        {

            using (var halContext = new MacHalContext("UserData/Manifest/Manifest.xml.real"))
            {
                halContext.MvaCfBootup();
                halContext.MvaCfLoad();

                var ic = halContext.HalDevices[EnumMacDeviceId.inspectionch_assembly.ToString()] as MacHalInspectionCh;
                var uni = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                uni.HalConnect();
                ic.HalConnect();

                ic.SetRobotIntrude(false);
                ic.ReadXYPosition();
                ic.ReadZPosition();
                ic.ReadWPosition();
                ic.ReadRobotPosLeftRight();
                ic.ReadRobotPosUpDown();
                ic.ReadParticleCount();
                ic.ReadChamberPressureDiff();
            }
        }

        [TestMethod]
        public void TestAssemblyWork()
        {

            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvaCfLoad();

                var ic = halContext.HalDevices[EnumMacDeviceId.inspectionch_assembly.ToString()] as MacHalInspectionCh;
                var uni = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                uni.HalConnect();
                ic.HalConnect();

                ic.SetRobotIntrude(false);
                ic.Initial();
                ic.XYPosition(200, 100);
                ic.ZPosition(-30);
                ic.WPosition(51);
            }
        }

        [TestMethod]
        public void TestWork_Inspection()
        {

            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvaCfLoad();

                var ic = halContext.HalDevices[EnumMacDeviceId.inspectionch_assembly.ToString()] as MacHalInspectionCh;
                var uni = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                uni.HalConnect();
                ic.HalConnect();

                ic.HalConnect();
                //ic.Initial();

                ic.InspectionSide();




            }
        }


    }
}
