using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut016_MT
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
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    cc.HalConnect();

                    //1. 光罩放置於Load Port A的Pod上

                    //2.Mask Robot從Home點移動至Load Port A
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                    mt.RobotMoving(false);

                    //3. Mask Robot在Load Port A的Pod 內進行光罩夾取
                    mt.Clamp(1);

                    //4. Mask Robot將光罩從Load Port A移動至Clean Chamber內
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                    mt.ChangeDirection(@"D:\Positions\MTRobot\CleanChHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\CCHomeToCCFrontSide.json");
                    mt.RobotMoving(false);

                    //5. 雷射sensor可以偵測到Mask Robot進入Clean Chamber & 報值
                    var About = cc.ReadRobotPosAbout();
                    var UpDown = cc.ReadRobotPosUpDown();

                    //6. Mask Robot模擬吹除轉動Mask行為
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\CCFrontSideToClean.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\FrontSideCleanFinishToCC.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\CCFrontSideToCapture.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\FrontSideCaptureFinishToCC.json");
                    mt.RobotMoving(false);

                    //7. 將光罩移動至安全光柵附近, 觸發光柵報值(由人員手動遮斷)
                    var LightCurtain = cc.ReadLightCurtain();

                    //8. Mask Robot將光罩從Clean Chamber, 移回Load Port A
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\CCFrontSideToCCHome.json");
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                    mt.RobotMoving(false);

                    //9. Mask Robot將光罩放置於Load Port A Pod上
                    mt.Unclamp();

                    //10. Mask Robot (無夾持光罩) 從Load Port A移回Home點
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                    mt.RobotMoving(false);


                    About = 0;
                    UpDown = 0;

                    //11. 重複1~10, 執行Load Port B -> Clean Chamber光罩傳送
                    //1. 光罩放置於Load Port B的Pod上

                    //2.Mask Robot從Home點移動至Load Port B
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
                    mt.RobotMoving(false);

                    //3. Mask Robot在Load Port B的Pod 內進行光罩夾取
                    mt.Clamp(1);

                    //4. Mask Robot將光罩從Load Port B移動至Clean Chamber內
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
                    mt.ChangeDirection(@"D:\Positions\MTRobot\CleanChHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\CCHomeToCCFrontSide.json");
                    mt.RobotMoving(false);

                    //5. 雷射sensor可以偵測到Mask Robot進入Clean Chamber & 報值
                    About = cc.ReadRobotPosAbout();
                    UpDown = cc.ReadRobotPosUpDown();

                    //6. Mask Robot模擬吹除轉動Mask行為
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\CCFrontSideToClean.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\FrontSideCleanFinishToCC.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\CCFrontSideToCapture.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\FrontSideCaptureFinishToCC.json");
                    mt.RobotMoving(false);

                    //7. 將光罩移動至安全光柵附近, 觸發光柵報值(由人員手動遮斷)
                    LightCurtain = cc.ReadLightCurtain();

                    //8. Mask Robot將光罩從Clean Chamber, 移回Load Port B
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\CCFrontSideToCCHome.json");
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
                    mt.RobotMoving(false);

                    //9. Mask Robot將光罩放置於Load Port B Pod上
                    mt.Unclamp();

                    //10. Mask Robot (無夾持光罩) 從Load Port B移回Home點
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
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
                    halContext.MvCfInit();
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    cc.HalConnect();
                    ic.HalConnect();

                    //1. 光罩放置於Inspection Chamber Stage上

                    //2. Mask Robot從Home點移動至Inspection Chamber
                    mt.RobotMoving(true);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    ic.ReadRobotIntrude(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeToICFrontSide.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICStage.json");
                    mt.RobotMoving(false);

                    //3. Mask Robot在Inspection Chamber 內進行光罩夾取
                    mt.Clamp(1);

                    //4. Mask Robot將光罩從Inspection Chamber移動至Clean Chamber內
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICStageToICFrontSide.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                    ic.ReadRobotIntrude(false);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\CleanChHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\CCHomeToCCBackSide.json");
                    mt.RobotMoving(false);

                    //5. 雷射sensor可以偵測到Mask Robot進入Clean Chamber & 報值
                    var About = cc.ReadRobotPosAbout();
                    var UpDown = cc.ReadRobotPosUpDown();

                    //6. Mask Robot模擬吹除轉動Mask行為
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\CCBackSideToClean.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\BackSideCleanFinishToCC.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\CCBackSideToCapture.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\BackSideCaptureFinishToCC.json");
                    mt.RobotMoving(false);

                    //7. 將光罩移動至安全光柵附近, 可觸發光柵報值

                    //8. Mask Robot將光罩從Clean Chamber, 移回Inspection Chamber
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\CCBackSideToCCHome.json");
                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    ic.ReadRobotIntrude(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeToICFrontSide.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICStage.json");
                    mt.RobotMoving(false);

                    //9. Mask Robot將光罩放置於Inspection Chamber Stage上
                    mt.Unclamp();

                    //10.Mask Robot(無夾持光罩) 從Inspection Chamber移回Home點
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICStageToICFrontSide.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                    ic.ReadRobotIntrude(false);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.RobotMoving(false);

                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
