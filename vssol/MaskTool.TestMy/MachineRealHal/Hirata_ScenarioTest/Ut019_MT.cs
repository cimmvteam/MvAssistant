using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut019_MT
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
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var lpb = halContext.HalDevices[MacEnumDevice.loadportB_assembly.ToString()] as MacHalLoadPort;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    lpb.HalConnect();

                    //1. 光罩放置於Load Port B POD內

                    //2~5  Load Poat A Dock
                    lpb.Dock();

                    //6. Mask Robot從Home點移動至Load Port B
                    mt.RobotMoving(true);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
                    mt.RobotMoving(false);

                    //7. Mask Robot在Load Port B 進行光罩夾取
                    mt.Clamp(1);

                    //8. Mask Robot將光罩從Load Port B移動至Inspection Chamber Entry處
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeToICFrontSide.json");
                    mt.RobotMoving(false);

                    //9. Mask Robot將光罩從Inspection Chamber Entry處, 移回Load Port B
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
                    mt.RobotMoving(false);

                    //10. Mask Robot將光罩放置於Load Port B POD內
                    mt.Unclamp();

                    //11.Mask Robot(無夾持光罩) 從Load Port B移回Home點
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
                    mt.RobotMoving(false);

                    //12~15  Load Port B Undock
                    lpb.Undock();
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
