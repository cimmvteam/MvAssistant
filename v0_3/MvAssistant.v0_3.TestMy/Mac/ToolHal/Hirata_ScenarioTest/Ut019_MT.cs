﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_3.Mac.Hal;
using MvAssistant.v0_3.Mac.Hal.Assembly;
using MvAssistant.v0_3.Mac.JSon;
using MvAssistant.v0_3.Mac.JSon.RobotTransferFile;
using MvAssistant.v0_3.Mac.Manifest;

namespace MvAssistant.v0_3.Mac.TestMy.ToolHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut019_MT
    {
        MaskrobotTransferPathFile pathFileObj;//= new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
        public Ut019_MT()
        {
            PositionInstance.Load(); // 在這裏載入所有(Boxtransfer 及 Masktransfer)的路徑點位資料
            pathFileObj = new MaskrobotTransferPathFile(PositionInstance.MTR_Path);
        }
        
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
                    var mt = halContext.HalDevices[EnumMacDeviceId.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var lpb = halContext.HalDevices[EnumMacDeviceId.loadportB_assembly.ToString()] as MacHalLoadPort;
                    var lpa = halContext.HalDevices[EnumMacDeviceId.loadportA_assembly.ToString()] as MacHalLoadPort;
                    var ic = halContext.HalDevices[EnumMacDeviceId.inspectionch_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    lpb.HalConnect();
                    lpa.HalConnect();
                    ic.HalConnect();

                    //1. 光罩放置於Load Port B POD內

                    //2~5  Load Poat B Dock
                    lpb.Dock();

                    //6. (編號7-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源
                    lpb.LightForLoadPortBSetValue(255);
                    lpb.Camera_LoadPortB_CapToSave("D:/Image/LP/LPB/Insp", "jpg");
                    lpb.LightForLoadPortBSetValue(0);

                    //7.Mask Robot從Home點移動至Load Port B
                    mt.RobotMoving(true);
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
                    mt.ExePathMove(pathFileObj.FromLPHomeToLP2PathFile());
                    mt.RobotMoving(false);

                    //8. Mask Robot上的傾斜角度傳感器可報值 & 位於水平狀態
                    var Level = mt.ReadLevel();

                    //9. Mask Robot在Load Port B 進行光罩夾取
                    mt.Clamp(1);

                    //10. Mask Robot將光罩從Load Port B移動至Recognizer Chamber處
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLP2ToLPHomePathFile());
                    mt.ExePathMove(pathFileObj.FromLPHomeToBarcodeReaderPathFile());
                    mt.RobotMoving(false);

                    //11. (編號8 - Barcode CCD)拍攝mask barcode, 並能成功辨識ID
                    lpa.LightForBarcodeReaderSetValue(2);
                    lpa.Camera_Barcode_CapToSave("D:/Image/LP/LPA/Barcode", "jpg");
                    lpa.LightForBarcodeReaderSetValue(0);

                    //12. Mask Robot將光罩從Recognizer移動至Inspection Chamber Entry處
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromBarcodeReaderToLPHomePathFile());
                    mt.ChangeDirection(pathFileObj.InspChHomePathFile());
                    ic.SetRobotIntrude(true);
                    mt.ExePathMove(pathFileObj.FromICHomeToICBackSidePathFile());
                    mt.ExePathMove(pathFileObj.FromICBackSideToICStagePathFile());
                    mt.RobotMoving(false);

                    //13. Mask Robot將光罩從Inspection Chamber Entry處, 移回Load Port B
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromICStageToICBackSidePathFile());
                    mt.ExePathMove(pathFileObj.FromICBackSideToICHomePathFile());
                    ic.SetRobotIntrude(false);
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
                    mt.ExePathMove(pathFileObj.FromLPHomeToLP2PathFile());
                    mt.RobotMoving(false);

                    //14. Mask Robot將光罩放置於Load Port B POD內
                    mt.Unclamp();

                    //15. Mask Robot (無夾持光罩) 從Load Port B移回Home點
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLP2ToLPHomePathFile());
                    mt.RobotMoving(false);

                    //16. (編號7-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源
                    lpb.LightForLoadPortBSetValue(255);
                    lpb.Camera_LoadPortB_CapToSave("D:/Image/LP/LPB/Insp", "jpg");
                    lpb.LightForLoadPortBSetValue(0);

                    //17~20  Load Port B Undock
                    lpb.Undock();
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
