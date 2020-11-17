using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalLight
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var halContext = new MacHalContext("UserData/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfInit();
                halContext.MvCfLoad();

                var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;
                var lpa = halContext.HalDevices[MacEnumDevice.loadportA_assembly.ToString()] as MacHalLoadPort;
                var lpb = halContext.HalDevices[MacEnumDevice.loadportB_assembly.ToString()] as MacHalLoadPort;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                uni.HalConnect();
                ic.HalConnect();
                lpa.HalConnect();
                lpb.HalConnect();
                cc.HalConnect();
                os.HalConnect();
                bt.HalConnect();

                lpa.LightForLoadPortA(200);//bar 0~255
                lpb.LightForLoadPortB(200);//bar 0~255
                lpa.LightForBarcodeReader(200);//bar 0~255
                cc.LightForInspSetValue(200);//bar 0~999
                os.LightForFrontBarDfsSetValue(200);//bar 0~255
                os.LightForSideBarDfsSetValue(200);//bar 0~255
                os.LightForTopBarDfsSetValue(200);//bar 0~255
                bt.LightForGripper(200);//bar 0~255

                ic.LightForSideBarDfsSetValue(200);//bar 0~999
                ic.LightForSideBarInspSetValue(200);//bar 0~999
                ic.LightForTopCrlDfsSetValue(200);//crl 0~255
                ic.LightForTopCrlInspSetValue(200);//crl 0~255
                ic.LightForLeftSpotInspSetValue(200);//spot 0~255
                ic.LightForRightSpotInspSetValue(200);//spot 0~255




                lpa.LightForLoadPortA(0);
                lpb.LightForLoadPortB(0);
                lpa.LightForBarcodeReader(0);
                cc.LightForInspSetValue(0);
                os.LightForFrontBarDfsSetValue(0);
                os.LightForSideBarDfsSetValue(0);
                os.LightForTopBarDfsSetValue(0);
                bt.LightForGripper(0);

                ic.LightForSideBarDfsSetValue(0);
                ic.LightForSideBarInspSetValue(0);
                ic.LightForTopCrlDfsSetValue(0);
                ic.LightForTopCrlInspSetValue(0);
                ic.LightForLeftSpotInspSetValue(0);
                ic.LightForRightSpotInspSetValue(0);
            }
        }
    }
}
