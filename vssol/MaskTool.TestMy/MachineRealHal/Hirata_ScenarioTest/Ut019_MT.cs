using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.JSon;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest
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
                    halContext.MvCfInit();
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var lpb = halContext.HalDevices[MacEnumDevice.loadportB_assembly.ToString()] as MacHalLoadPort;
                    var lpa = halContext.HalDevices[MacEnumDevice.loadportA_assembly.ToString()] as MacHalLoadPort;
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    lpb.HalConnect();
                    lpa.HalConnect();
                    ic.HalConnect();

                    //1. 光罩放置於Load Port B POD內

                    //2~5  Load Poat B Dock
                    lpb.Dock();

                    //6. (編號7-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源
                    lpb.LightForLoadPortB(200);
                    lpb.Camera_LoadPortB_CapToSave("D:/Image/LP/LPB/Insp", "jpg");
                    lpb.LightForLoadPortB(0);

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
                    mt.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToBarcodeReader.json");
                    mt.RobotMoving(false);

                    //11. (編號8 - Barcode CCD)拍攝mask barcode, 並能成功辨識ID
                    lpa.Camera_Barcode_CapToSave("D:/Image/LP/LPA/Barcode", "jpg");

                    //12. Mask Robot將光罩從Recognizer移動至Inspection Chamber Entry處
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\BarcodeReaderToLPHome.json");
                    mt.ChangeDirection(pathFileObj.InspChHomePathFile());
                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    ic.ReadRobotIntrude(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeToICBackSide.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICBackSideToICStage.json");
                    mt.RobotMoving(false);

                    //13. Mask Robot將光罩從Inspection Chamber Entry處, 移回Load Port B
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICStageToICBackSide.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICBackSideToICHome.json");
                    ic.ReadRobotIntrude(false);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
                    mt.RobotMoving(false);

                    //14. Mask Robot將光罩放置於Load Port B POD內
                    mt.Unclamp();

                    //15. Mask Robot (無夾持光罩) 從Load Port B移回Home點
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
                    mt.RobotMoving(false);

                    //16. (編號7-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源
                    lpb.LightForLoadPortB(200);
                    lpb.Camera_LoadPortB_CapToSave("D:/Image/LP/LPB/Insp", "jpg");
                    lpb.LightForLoadPortB(0);

                    //17~20  Load Port B Undock
                    lpb.Undock();
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
