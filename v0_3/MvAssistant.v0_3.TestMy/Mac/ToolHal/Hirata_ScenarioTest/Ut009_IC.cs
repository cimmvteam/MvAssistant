﻿using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_3.Mac.Hal;
using MvAssistant.v0_3.Mac.Hal.Assembly;
using MvAssistant.v0_3.Mac.Manifest;

namespace MvAssistant.v0_3.Mac.TestMy.ToolHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut009_IC
    {
        [TestMethod]
        public void TestMethod1()//OK
        {
            try
            {
                using (var halContext = new MacHalContext("UserData/Manifest/Manifest.xml.real"))
                {
                    halContext.MvaCfBootup();
                    halContext.MvaCfLoad();

                    var unv = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                    var ic = halContext.HalDevices[EnumMacDeviceId.inspectionch_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    ic.HalConnect();

                    ic.Initial();

                    //1. 將光罩放置於Inspection Chamber Stage上

                    //2. 將光罩分以4 x 4, 16宮格方式, 移動Stage X &Y方向, 讓編號4 - CCD可以拍攝到每個宮格的影像
                    
                    //3. 每個宮格內, 執行以下程序: 開啟線光源->拍照(FOV正確, 可看到particle)->關閉線光源
                    ic.LightForLeftBarSetValue(999);//bar 0~999
                    ic.LightForRightBarSetValue(999);//bar 0~999
                    for (int i = 158; i <= 296; i += 23)
                    {
                        for (int j = 123; j <= 261; j += 23)
                        {
                            ic.XYPosition(i, j);
                            ic.Camera_TopInsp_CapToSave("D:/Image/IC/TopInsp", "jpg");
                            Thread.Sleep(500);
                        }
                    }
                    ic.LightForLeftBarSetValue(0);
                    ic.LightForRightBarSetValue(0);

                    //4. 每個宮格內, 執行以下程序: 開啟環形光源->拍照(FOV正確, 可看到光罩pattern)->關閉環形光源
                    ic.LightForTopCrlInspSetValue(255);
                    for (int i = 158; i <= 296; i += 23)
                    {
                        for (int j = 123; j <= 261; j += 23)
                        {
                            ic.XYPosition(i, j);
                            ic.Camera_TopInsp_CapToSave("D:/Image/IC/TopInsp", "jpg");
                            Thread.Sleep(500);
                        }
                    }
                    ic.LightForTopCrlInspSetValue(0);
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
