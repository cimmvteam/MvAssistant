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
    public class Ut020_MT
    {
        MaskrobotTransferPathFile pathFileObj;//= new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
        public Ut020_MT()
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
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    ic.HalConnect();

                    ic.Initial();

                    //1. 光罩放置於Inspection Chamber Stage上

                    //2. (編號2-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源
                    ic.LightForBackLineSetValue(120);
                    ic.Camera_SideDfs_CapToSave("D:/Image/IC/SigeDfs", "jpg");
                    ic.LightForBackLineSetValue(0);

                    //3. (編號3-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源
                    ic.LightForLeftLineSetValue(255);
                    ic.LightForBackLineSetValue(255);
                    ic.LightForTopCrlDfsSetValue(39);
                    ic.Camera_TopDfs_CapToSave("D:/Image/IC/TopDfs", "jpg");
                    ic.LightForLeftLineSetValue(0);
                    ic.LightForBackLineSetValue(0);
                    ic.LightForTopCrlDfsSetValue(0);

                    //4. Mask Robot從Home點移動至Inspection Chamber
                    mt.RobotMoving(true);
                    mt.ChangeDirection(pathFileObj.InspChHomePathFile());
                    ic.ReadRobotIntrude(true);
                    mt.ExePathMove(pathFileObj.FromICHomeToICFrontSidePathFile());
                    mt.ExePathMove(pathFileObj.FromICFrontSideToICStagePathFile());
                    mt.RobotMoving(false);

                    //5. 雷射sensor可以偵測到Mask Robot進入Inspection Chamber &報值
                    var About = ic.ReadRobotPosAbout();
                    var UpDown = ic.ReadRobotPosUpDown();

                    //6. Mask Robot在Inspection Chamber 內進行光罩夾取
                    mt.Clamp(1);

                    //7. Mask Robot將光罩從Inspection Chamber移動至Load Port A/ B Entry處
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromICStageToICFrontSidePathFile());
                    mt.ExePathMove(pathFileObj.FromICFrontSideToICHomePathFile());
                    ic.ReadRobotIntrude(false);
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
                    mt.ExePathMove(pathFileObj.FromLPHomeToLP1PathFile());
                    mt.ExePathMove(pathFileObj.FromLP1ToLPHomePathFile());
                    mt.ExePathMove(pathFileObj.FromLPHomeToLP2PathFile());
                    mt.RobotMoving(false);

                    //8. Mask Robot將光罩從Load Port A / B Entry處, 移回Inspection Chamber
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLP2ToLPHomePathFile());
                    mt.ChangeDirection(pathFileObj.InspChHomePathFile());
                    ic.ReadRobotIntrude(true);
                    mt.ExePathMove(pathFileObj.FromICHomeToICFrontSidePathFile());
                    mt.ExePathMove(pathFileObj.FromICFrontSideToICStagePathFile());
                    mt.RobotMoving(false);

                    //9. 雷射sensor可以偵測到Mask Robot進入Inspection Chamber &報值
                    About = ic.ReadRobotPosAbout();
                    UpDown = ic.ReadRobotPosUpDown();

                    //10. Mask Robot將光罩放置於Inspection Chamber Stage上
                    mt.Unclamp();

                    //11. (編號2-CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    ic.LightForBackLineSetValue(120);
                    ic.Camera_SideDfs_CapToSave("D:/Image/IC/SigeDfs", "jpg");
                    ic.LightForBackLineSetValue(0);

                    //12. (編號3-CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    ic.LightForLeftLineSetValue(255);
                    ic.LightForBackLineSetValue(255);
                    ic.LightForTopCrlDfsSetValue(39);
                    ic.Camera_TopDfs_CapToSave("D:/Image/IC/TopDfs", "jpg");
                    ic.LightForLeftLineSetValue(0);
                    ic.LightForBackLineSetValue(0);
                    ic.LightForTopCrlDfsSetValue(0);

                    //13. Mask Robot(無夾持光罩) 從Inspection Chamber移回Home點
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromICStageToICFrontSidePathFile());
                    mt.ExePathMove(pathFileObj.FromICFrontSideToICHomePathFile());
                    ic.ReadRobotIntrude(false);
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
                    mt.RobotMoving(false);
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
