using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_3.Mac.Hal;
using MvAssistant.v0_3.Mac.Hal.Assembly;
using MvAssistant.v0_3.Mac.Manifest;

namespace MvAssistant.v0_3.Mac.TestMy.ToolHal.Hirata_ScenarioTest
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
                    halContext.MvaCfBootup();
                    halContext.MvaCfLoad();

                    var unv = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                    var ic = halContext.HalDevices[EnumMacDeviceId.inspectionch_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    ic.HalConnect();

                    ic.Initial();

                    //1. 將光罩放置於Inspection Chamber Stage上

                    //2. Rotate stage, 讓編號1-CCD可以拍攝到光罩4個側邊的影像
                    ic.XYPosition(30, 90);

                    //3. 在每個側邊, 執行以下程序: 開啟Light Bar光源 -> 拍照(FOV正確) -> 關閉Light Bar光源
                    ic.LightForTopCrlDfsSetValue(10);//crl 0~255
                    ic.LightForLeftLineSetValue(100);//spot 0~255
                    for (int i = 0; i < 360; i+=90)
                    {
                        ic.Camera_SideInsp_CapToSave("D:/Image/IC/SideInsp", "jpg");
                    }
                    ic.LightForTopCrlDfsSetValue(0);
                    ic.LightForLeftLineSetValue(0);

                    ic.XYPosition(0,158);
                    ic.WPosition(0);
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
