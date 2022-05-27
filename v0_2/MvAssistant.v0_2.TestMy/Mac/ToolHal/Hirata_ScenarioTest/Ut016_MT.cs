using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_2.Mac.Hal;
using MvAssistant.v0_2.Mac.Hal.Assembly;
using MvAssistant.v0_2.Mac.JSon;
using MvAssistant.v0_2.Mac.JSon.RobotTransferFile;
using MvAssistant.v0_2.Mac.Manifest;

namespace MvAssistant.v0_2.Mac.TestMy.ToolHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut016_MT
    {
        MaskrobotTransferPathFile pathFileObj;//= new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
        public Ut016_MT()
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
                    halContext.MvaCfBookup();
                    halContext.MvaCfLoad();

                    var unv = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                    var mt = halContext.HalDevices[EnumMacDeviceId.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var cc = halContext.HalDevices[EnumMacDeviceId.clean_assembly.ToString()] as MacHalCleanCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    cc.HalConnect();

                    //1. 光罩放置於Load Port A的Pod上

                    //2.Mask Robot從Home點移動至Load Port A
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLPHomeToLP1PathFile());
                    mt.RobotMoving(false);

                    //3. Mask Robot在Load Port A的Pod 內進行光罩夾取
                    mt.Clamp(1);

                    //4. Mask Robot將光罩從Load Port A移動至Clean Chamber內
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLP1ToLPHomePathFile());
                    mt.ChangeDirection(pathFileObj.CleanChHomePathFile());
                    mt.ExePathMove(pathFileObj.FromCCHomeToCCFrontSidePathFile());
                    mt.RobotMoving(false);

                    //5. 雷射sensor可以偵測到Mask Robot進入Clean Chamber & 報值
                    var About = cc.ReadRobotPosLeftRight();
                    var UpDown = cc.ReadRobotPosUpDown();

                    //6. Mask Robot模擬吹除轉動Mask行為
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromCCFrontSideToCleanPathFile());
                    mt.ExePathMove(pathFileObj.FromFrontSideCleanFinishToCCPathFile());
                    mt.ExePathMove(pathFileObj.FromCCFrontSideToCapturePathFile());
                    mt.ExePathMove(pathFileObj.FromFrontSideCaptureFinishToCCPathFile());
                    mt.RobotMoving(false);

                    //7. 將光罩移動至安全光柵附近, 觸發光柵報值(由人員手動遮斷)
                    var LightCurtain = cc.ReadLightCurtain();

                    //8. Mask Robot將光罩從Clean Chamber, 移回Load Port A
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromCCFrontSideToCCHomePathFile());
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
                    mt.ExePathMove(pathFileObj.FromLPHomeToLP1PathFile());
                    mt.RobotMoving(false);

                    //9. Mask Robot將光罩放置於Load Port A Pod上
                    mt.Unclamp();

                    //10. Mask Robot (無夾持光罩) 從Load Port A移回Home點
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLP1ToLPHomePathFile());
                    mt.RobotMoving(false);


                    About = 0;
                    UpDown = 0;

                    //11. 重複1~10, 執行Load Port B -> Clean Chamber光罩傳送
                    //1. 光罩放置於Load Port B的Pod上

                    //2.Mask Robot從Home點移動至Load Port B
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLPHomeToLP2PathFile());
                    mt.RobotMoving(false);

                    //3. Mask Robot在Load Port B的Pod 內進行光罩夾取
                    mt.Clamp(1);

                    //4. Mask Robot將光罩從Load Port B移動至Clean Chamber內
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLP2ToLPHomePathFile());
                    mt.ChangeDirection(pathFileObj.CleanChHomePathFile());
                    mt.ExePathMove(pathFileObj.FromCCHomeToCCFrontSidePathFile());
                    mt.RobotMoving(false);

                    //5. 雷射sensor可以偵測到Mask Robot進入Clean Chamber & 報值
                    About = cc.ReadRobotPosLeftRight();
                    UpDown = cc.ReadRobotPosUpDown();

                    //6. Mask Robot模擬吹除轉動Mask行為
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromCCFrontSideToCleanPathFile());
                    mt.ExePathMove(pathFileObj.FromFrontSideCleanFinishToCCPathFile());
                    mt.ExePathMove(pathFileObj.FromCCFrontSideToCapturePathFile());
                    mt.ExePathMove(pathFileObj.FromFrontSideCaptureFinishToCCPathFile());
                    mt.RobotMoving(false);

                    //7. 將光罩移動至安全光柵附近, 觸發光柵報值(由人員手動遮斷)
                    LightCurtain = cc.ReadLightCurtain();

                    //8. Mask Robot將光罩從Clean Chamber, 移回Load Port B
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromCCFrontSideToCCHomePathFile());
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
                    mt.ExePathMove(pathFileObj.FromLPHomeToLP2PathFile());
                    mt.RobotMoving(false);

                    //9. Mask Robot將光罩放置於Load Port B Pod上
                    mt.Unclamp();

                    //10. Mask Robot (無夾持光罩) 從Load Port B移回Home點
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLP2ToLPHomePathFile());
                    mt.RobotMoving(false);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestMethod2()//OK
        {
            try
            {
                using (var halContext = new MacHalContext("UserData/Manifest/Manifest.xml.real"))
                {
                    halContext.MvaCfBookup();
                    halContext.MvaCfLoad();

                    var unv = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                    var mt = halContext.HalDevices[EnumMacDeviceId.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var cc = halContext.HalDevices[EnumMacDeviceId.clean_assembly.ToString()] as MacHalCleanCh;
                    var ic = halContext.HalDevices[EnumMacDeviceId.inspectionch_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    cc.HalConnect();
                    ic.HalConnect();

                    //1. 光罩放置於Inspection Chamber Stage上

                    //2. Mask Robot從Home點移動至Inspection Chamber
                    mt.RobotMoving(true);
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
                    mt.ChangeDirection(pathFileObj.InspChHomePathFile());
                    ic.SetRobotIntrude(true);
                    mt.ExePathMove(pathFileObj.FromICHomeToICFrontSidePathFile());
                    mt.ExePathMove(pathFileObj.FromICFrontSideToICStagePathFile());
                    mt.RobotMoving(false);

                    //3. Mask Robot在Inspection Chamber 內進行光罩夾取
                    mt.Clamp(1);

                    //4. Mask Robot將光罩從Inspection Chamber移動至Clean Chamber內
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromICStageToICFrontSidePathFile());
                    mt.ExePathMove(pathFileObj.FromICFrontSideToICHomePathFile());
                    ic.SetRobotIntrude(false);
                    mt.ChangeDirection(pathFileObj.CleanChHomePathFile());
                    mt.ExePathMove(pathFileObj.FromCCHomeToCCBackSidePathFile());
                    mt.RobotMoving(false);

                    //5. 雷射sensor可以偵測到Mask Robot進入Clean Chamber & 報值
                    var About = cc.ReadRobotPosLeftRight();
                    var UpDown = cc.ReadRobotPosUpDown();

                    //6. Mask Robot模擬吹除轉動Mask行為
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromCCBackSideToCleanPathFile());
                    mt.ExePathMove(pathFileObj.FromBackSideCleanFinishToCCPathFile());
                    mt.ExePathMove(pathFileObj.FromCCBackSideToCapturePathFile());
                    mt.ExePathMove(pathFileObj.FromBackSideCaptureFinishToCCPathFile());
                    mt.RobotMoving(false);

                    //7. 將光罩移動至安全光柵附近, 可觸發光柵報值(由人員手動遮斷)
                    var LightCurtain = cc.ReadLightCurtain();

                    //8. Mask Robot將光罩從Clean Chamber, 移回Inspection Chamber
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromCCBackSideToCCHomePathFile());
                    mt.ChangeDirection(pathFileObj.InspChHomePathFile());
                    ic.SetRobotIntrude(true);
                    mt.ExePathMove(pathFileObj.FromICHomeToICFrontSidePathFile());
                    mt.ExePathMove(pathFileObj.FromICFrontSideToICStagePathFile());
                    mt.RobotMoving(false);

                    //9. Mask Robot將光罩放置於Inspection Chamber Stage上
                    mt.Unclamp();

                    //10.Mask Robot(無夾持光罩) 從Inspection Chamber移回Home點
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromICStageToICFrontSidePathFile());
                    mt.ExePathMove(pathFileObj.FromICFrontSideToICHomePathFile());
                    ic.SetRobotIntrude(false);
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
                    mt.RobotMoving(false);

                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
