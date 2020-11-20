using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut010_IC
    {
        [TestMethod]
        public void TestMethod1()//OK
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

                    //2. Rotate stage, 讓編號1-CCD可以拍攝到光罩4個側邊的影像
                    ic.XYPosition(30, 90);

                    //3. 在每個側邊, 執行以下程序: 開啟Light Bar光源 -> 拍照(FOV正確) -> 關閉Light Bar光源
                    ic.LightForTopCrlInspSetValue(10);//crl 0~255
                    ic.LightForLeftBarSetValue(100);//spot 0~255
                    for (int i = 0; i < 360; i+=90)
                    {
                        ic.Camera_TopInsp_CapToSave("D:/Image/IC/TopInsp", "jpg");
                    }
                    ic.LightForTopCrlInspSetValue(0);
                    ic.LightForLeftBarSetValue(0);

                    ic.XYPosition(0,158);
                    ic.WPosition(0);
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
