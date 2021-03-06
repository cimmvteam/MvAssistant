﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_2.Mac.Hal;
using MvAssistant.v0_2.Mac.Hal.Assembly;
using MvAssistant.v0_2.Mac.Manifest;

namespace MvAssistant.v0_2.Mac.TestMy.ToolHal
{
    [TestClass]
    public class UtHalLight
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var halContext = new MacHalContext("UserData/Manifest/Manifest.xml.real"))
            {
                halContext.MvaCfInit();
                halContext.MvaCfLoad();

                var ic = halContext.HalDevices[MacEnumDevice.inspectionch_assembly.ToString()] as MacHalInspectionCh;
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

                ic.LightForBackLineSetValue(200);//bar 0~255(小線光-後)
                ic.LightForLeftLineSetValue(200);//bar 0~255(小線光-左)
                ic.LightForTopCrlDfsSetValue(200);//crl 0~255(小環光)
                ic.LightForTopCrlInspSetValue(200);//crl 0~255(大環光)
                ic.LightForLeftBarSetValue(200);//spot 0~999(大線光-左)
                ic.LightForRightBarSetValue(200);//spot 0~999(大線光-右)




                lpa.LightForLoadPortA(0);
                lpb.LightForLoadPortB(0);
                lpa.LightForBarcodeReader(0);
                cc.LightForInspSetValue(0);
                os.LightForFrontBarDfsSetValue(0);
                os.LightForSideBarDfsSetValue(0);
                os.LightForTopBarDfsSetValue(0);
                bt.LightForGripper(0);

                ic.LightForBackLineSetValue(0);
                ic.LightForLeftLineSetValue(0);
                ic.LightForTopCrlDfsSetValue(0);
                ic.LightForTopCrlInspSetValue(0);
                ic.LightForLeftBarSetValue(0);
                ic.LightForRightBarSetValue(0);
            }
        }
    }
}
