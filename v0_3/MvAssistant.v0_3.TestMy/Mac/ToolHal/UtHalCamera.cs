﻿using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_3.Mac.Hal;
using MvAssistant.v0_3.Mac.Hal.Assembly;
using MvAssistant.v0_3.Mac.Manifest;

namespace MvAssistant.v0_3.Mac.TestMy.ToolHal
{
    [TestClass]
    public class UtHalCamera
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var halContext = new MacHalContext("UserData/Manifest/Manifest.xml.real"))
            {
                halContext.MvaCfBootup();
                halContext.MvaCfLoad();

                var uni = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                var ic = halContext.HalDevices[EnumMacDeviceId.inspectionch_assembly.ToString()] as MacHalInspectionCh;
                var cc = halContext.HalDevices[EnumMacDeviceId.clean_assembly.ToString()] as MacHalCleanCh;
                var lpa = halContext.HalDevices[EnumMacDeviceId.loadportA_assembly.ToString()] as MacHalLoadPort;
                var lpb = halContext.HalDevices[EnumMacDeviceId.loadportB_assembly.ToString()] as MacHalLoadPort;
                var os = halContext.HalDevices[EnumMacDeviceId.openstage_assembly.ToString()] as MacHalOpenStage;
                var bt = halContext.HalDevices[EnumMacDeviceId.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                
                uni.HalConnect();
                ic.HalConnect();
                cc.HalConnect();
                lpa.HalConnect();
                lpb.HalConnect();
                os.HalConnect();
                bt.HalConnect();

                try
                {
                    for (int i = 0; i < 2000; i++)
                    {
                        //ic.Camera_SideDfs_CapToSave("D:/Image/IC/SigeDfs", "jpg");
                        //ic.Camera_TopDfs_CapToSave("D:/Image/IC/TopDfs", "jpg");
                        //ic.Camera_SideInsp_CapToSave("D:/Image/IC/SideInsp", "jpg");
                        ic.Camera_TopInsp_CapToSave("D:/Image/IC/TopInsp", "jpg");//需要有介面卡的主機才能執行此動作
                        //cc.Camera_Insp_CapToSave("D:/Image/CC/Insp", "jpg");
                        //lpa.Camera_LoadPortA_CapToSave("D:/Image/LP/LPA/Insp", "jpg");
                        //lpa.Camera_Barcode_CapToSave("D:/Image/LP/LPA/Barcode", "jpg");
                        //lpb.Camera_LoadPortB_CapToSave("D:/Image/LP/LPB/Insp", "jpg");
                        //os.Camera_Top_CapToSave("D:/Image/OS/Top", "jpg");
                        //os.Camera_Side_CapToSave("D:/Image/OS/Side", "jpg");
                        //os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                        //os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                        //bt.Camera_CapToSave("D:/Image/BT/Gripper", "jpg");
                        Thread.Sleep(100);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
        }
    }
}
