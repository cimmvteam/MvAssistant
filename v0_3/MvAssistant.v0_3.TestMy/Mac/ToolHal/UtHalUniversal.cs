﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_3.Mac.Hal;
using MvAssistant.v0_3.Mac.Hal.Assembly;
using MvAssistant.v0_3.Mac.Manifest;
using System.Threading;

namespace MvAssistant.v0_3.Mac.TestMy.ToolHal
{
    [TestClass]
    public class UtHalUniversal
    {
       
        [TestMethod]
        public void TestFlow()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvaCfLoad();

                    var unv = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                    var os = halContext.HalDevices[EnumMacDeviceId.openstage_assembly.ToString()] as MacHalOpenStage;
                    var mt = halContext.HalDevices[EnumMacDeviceId.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var bt = halContext.HalDevices[EnumMacDeviceId.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;

                    unv.HalConnect();
                    os.HalConnect();
                    mt.HalConnect();

                    os.Initial();
                    os.SetBoxType(1);
                    os.SortClamp();
                    os.Vacuum(true);
                    os.SortUnclamp();
                    os.Lock();
                    if (os.SetRobotIntrude(true, false).Item1 == true)
                        bt.RobotMoving(true);
                    else
                        throw new Exception("Open Stage not allow Box Transfer intrude.");
                    //BT開鎖
                    bt.RobotMoving(false);
                    SpinWait.SpinUntil(() => (os.ReadRobotIntruded().Item1 == false && os.ReadRobotIntruded().Item2 == false));
                    os.Close();
                    os.Clamp();
                    os.Open();
                    if (os.SetRobotIntrude(false, true).Item2 == true)
                        mt.RobotMoving(true);
                    else
                        throw new Exception("Open Stage not allow Mask Transfer intrude.");
                    //MT取mask
                    mt.RobotMoving(false);
                    SpinWait.SpinUntil(() => (os.ReadRobotIntruded().Item1 == false && os.ReadRobotIntruded().Item2 == false));
                    os.Close();
                    os.Unclamp();
                    os.Lock();
                    if (os.SetRobotIntrude(true, false).Item1 == true)
                        bt.RobotMoving(true);
                    else
                        throw new Exception("Open Stage not allow Box Transfer intrude.");
                    //BT開鎖
                    bt.RobotMoving(false);
                    SpinWait.SpinUntil(() => (os.ReadRobotIntruded().Item1 == false && os.ReadRobotIntruded().Item2 == false));
                    os.Vacuum(false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void TestSetParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvaCfLoad();

                var unv = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;


            }
        }

        [TestMethod]
        public void TestReadParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvaCfLoad();

                var unv = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;


            }
        }

        [TestMethod]
        public void TestReadComponentValue()
        {
            try
            {

                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvaCfLoad();

                    var unv = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;

                    if (unv.HalConnect() != 0)
                    {
                        System.Diagnostics.Debug.WriteLine("Connect Fail");
                    }

                    unv.ReadCoverFanSpeed();
                    unv.ReadPowerON();
                    unv.ReadBCP_Maintenance();
                    unv.ReadCB_Maintenance();
                    unv.ReadBCP_EMO();
                    unv.ReadCB_EMO();
                    unv.ReadLP1_EMO();
                    unv.ReadLP2_EMO();
                    unv.ReadBCP_Door();
                    unv.ReadLP1_Door();
                    unv.ReadLP2_Door();
                    unv.ReadBCP_Smoke();

                    unv.ReadAlarm_General();
                    unv.ReadAlarm_Cabinet();
                    unv.ReadAlarm_CleanCh();
                    unv.ReadAlarm_BTRobot();
                    unv.ReadAlarm_MTRobot();
                    unv.ReadAlarm_OpenStage();
                    unv.ReadAlarm_InspCh();
                    unv.ReadAlarm_LoadPort();
                    unv.ReadAlarm_CoverFan();
                    unv.ReadAlarm_MTClampInsp();

                    unv.ReadWarning_General();
                    unv.ReadWarning_Cabinet();
                    unv.ReadWarning_CleanCh();
                    unv.ReadWarning_BTRobot();
                    unv.ReadWarning_MTRobot();
                    unv.ReadWarning_OpenStage();
                    unv.ReadWarning_InspCh();
                    unv.ReadWarning_LoadPort();
                    unv.ReadWarning_CoverFan();
                    unv.ReadWarning_MTClampInsp();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [TestMethod]
        public void TestAssemblyWork()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvaCfLoad();

                var unv = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;

                unv.ResetAllAlarm();
                unv.SetSignalTower(true, false, false);
                unv.SetBuzzer(1);
                unv.CoverFanCtrl(1, 150);
                unv.EMSAlarm(true, false, false, false);

            }
        }
    }
}
