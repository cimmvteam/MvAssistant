using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut009_IC
    {
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                using (var halContext = new MacHalContext("UserData/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfInit();
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    ic.HalConnect();

                    ic.Initial();

                    //1. 將光罩放置於Inspection Chamber Stage上

                    //2. 將光罩分以4 x 4, 16宮格方式, 移動Stage X &Y方向, 讓編號4 - CCD可以拍攝到每個宮格的影像


                    //3. 每個宮格內, 執行以下程序: 開啟線光源->拍照(FOV正確, 可看到particle)->關閉線光源
                    ic.LightForSideBarDfsSetValue(999);//bar 0~999
                    ic.LightForSideBarInspSetValue(999);//bar 0~999
                    ic.Camera_TopInsp_CapToSave("D:/Image/IC/TopInsp", "jpg");
                    ic.LightForSideBarDfsSetValue(0);
                    ic.LightForSideBarInspSetValue(0);

                    //4. 每個宮格內, 執行以下程序: 開啟環形光源->拍照(FOV正確, 可看到光罩pattern)->關閉環形光源
                    ic.LightForTopCrlInspSetValue(255);
                    ic.Camera_TopInsp_CapToSave("D:/Image/IC/TopInsp", "jpg");
                    ic.LightForTopCrlInspSetValue(0);
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
