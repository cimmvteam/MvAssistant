using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut015_MT
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
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    var lpa = halContext.HalDevices[MacEnumDevice.loadportA_assembly.ToString()] as MacHalLoadPort;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    lpa.HalConnect();
                    ic.HalConnect();

                    ic.Initial();

                    //1. Mask Robot (無夾持光罩) 從Home點移動至Inspection Chamber
                    ic.ReadRobotIntrude(true);
                    mt.RobotMoving(true);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeToICFrontSide.json");
                    mt.RobotMoving(false);

                    //2. 雷射sensor可以偵測到Mask Robot進入Inspection Chamber & 報值
                    var about = ic.ReadRobotPosAbout();
                    var UpDown = ic.ReadRobotPosUpDown();


                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                    ic.ReadRobotIntrude(false);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
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
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;

                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    ic.HalConnect();

                    ic.Initial();

                    //1. 光罩放置於Load Port A的Pod上

                    //2. Mask Robot從Home點移動至Load Port A
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                    mt.RobotMoving(false);

                    //3.Mask Robot在Load Port A的Pod 內進行光罩夾取
                    mt.Clamp(1);

                    //4. Mask Robot將光罩從Load Port A移動至Inspection Chamber內 (不放置)
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    ic.ReadRobotIntrude(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeToICFrontSide.json");
                    mt.RobotMoving(false);

                    //5. 雷射sensor可以偵測到Mask Robot進入Inspection Chamber & 報值
                    var About = ic.ReadRobotPosAbout();
                    var UpDown = ic.ReadRobotPosUpDown();

                    //6. Mask Robot將光罩從Inspection Chamber, 移回Load Port A
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                    ic.ReadRobotIntrude(false);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                    mt.RobotMoving(false);

                    //7. Mask Robot將光罩放置於Load Port A Pod上
                    mt.Unclamp();

                    //8. Mask Robot (無夾持光罩) 從Load Port A移回Home點
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                    mt.RobotMoving(false);


                    About = 0;
                    UpDown = 0;
                    //9 重複1~8, 執行Load Port B->Inspection Chamber光罩傳送
                    //1. 光罩放置於Load Port B的Pod上

                    //2. Mask Robot從Home點移動至Load Port B
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
                    mt.RobotMoving(false);

                    //3.Mask Robot在Load Port B的Pod 內進行光罩夾取
                    mt.Clamp(1);

                    //4. Mask Robot將光罩從Load Port B移動至Inspection Chamber內 (不放置)
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    ic.ReadRobotIntrude(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeToICFrontSide.json");
                    mt.RobotMoving(false);

                    //5. 雷射sensor可以偵測到Mask Robot進入Inspection Chamber & 報值
                    About = ic.ReadRobotPosAbout();
                    UpDown = ic.ReadRobotPosUpDown();

                    //6. Mask Robot將光罩從Inspection Chamber, 移回Load Port B
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                    ic.ReadRobotIntrude(false);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
                    mt.RobotMoving(false);

                    //7. Mask Robot將光罩放置於Load Port B Pod上
                    mt.Unclamp();

                    //8. Mask Robot (無夾持光罩) 從Load Port B移回Home點
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
                    mt.RobotMoving(false);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestMethod3()//OK
        {
            try
            {
                using (var halContext = new MacHalContext("UserData/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfInit();
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    os.HalConnect();
                    ic.HalConnect();

                    uint BoxType = 1;//1：鐵盒 , 2：水晶盒

                    os.Initial();

                    //1. 光罩放置於Open Stage上的鐵盒內
                    os.SetBoxType(BoxType);
                    os.SortClamp();
                    os.Vacuum(true);
                    os.SortUnclamp();
                    os.Lock();
                    if (os.ReadCoverSensor().Item2 == false)
                        throw new Exception("Box status was not closed");
                    os.Close();
                    os.Clamp();
                    os.Open();
                    if (os.ReadCoverSensor().Item1 == false)
                        throw new Exception("Box status was not opened");


                    //2. Mask Robot從Home點移動至Open Stage上方
                    mt.RobotMoving(true);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    os.ReadRobotIntrude(false, true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                    if (BoxType == 1)
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToIronBox.json");
                    else if (BoxType == 2)
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToCrystalBox.json");
                    mt.RobotMoving(false);

                    //3. Mask Robot在Open Stage進行光罩夾取
                    mt.Clamp(1);

                    //4.Mask Robot將光罩從Open Stage移動至Inspection Chamber處(不放置)
                    mt.RobotMoving(true);
                    if (BoxType == 1)
                        mt.ExePathMove(@"D:\Positions\MTRobot\IronBoxToOS.json");
                    else if (BoxType == 2)
                        mt.ExePathMove(@"D:\Positions\MTRobot\CrystalBoxToOS.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                    os.ReadRobotIntrude(false, false);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    ic.ReadRobotIntrude(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeToICFrontSide.json");
                    mt.RobotMoving(false);

                    //5. 雷射sensor可以偵測到Mask Robot進入Inspection Chamber & 報值
                    var About = ic.ReadRobotPosAbout();
                    var UpDown = ic.ReadRobotPosUpDown();

                    //6. Mask Robot將光罩從Inspection Chamber, 移回Open Stage
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                    ic.ReadRobotIntrude(false);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    os.ReadRobotIntrude(false, true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                    if (BoxType == 1)
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToIronBox.json");
                    else if (BoxType == 2)
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToCrystalBox.json");
                    mt.RobotMoving(false);

                    //7.Mask Robot將光罩放置於Open Stage上
                    mt.Unclamp();

                    //8. Mask Robot (無夾持光罩) 從Open Stage移回Home點
                    mt.RobotMoving(true);
                    if (BoxType == 1)
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToIronBox.json");
                    else if (BoxType == 2)
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToCrystalBox.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                    mt.RobotMoving(false);
                    os.ReadRobotIntrude(false, false);

                    os.Close();
                    os.Unclamp();
                    os.Vacuum(false);
                    os.Lock();

                    About = 0;
                    UpDown = 0;

                    BoxType = 2;
                    //8. 重複1~7, 執行Open Stage上水晶盒的傳送測試
                    //1. 光罩放置於Open Stage上的水晶盒內
                    os.SetBoxType(BoxType);
                    os.SortClamp();
                    os.Vacuum(true);
                    os.SortUnclamp();
                    os.Lock();
                    if (os.ReadCoverSensor().Item2 == false)
                        throw new Exception("Box status was not closed");
                    os.Close();
                    os.Clamp();
                    os.Open();
                    if (os.ReadCoverSensor().Item1 == false)
                        throw new Exception("Box status was not opened");


                    //2. Mask Robot從Home點移動至Open Stage上方
                    mt.RobotMoving(true);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    os.ReadRobotIntrude(false, true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                    if (BoxType == 1)
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToIronBox.json");
                    else if (BoxType == 2)
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToCrystalBox.json");
                    mt.RobotMoving(false);

                    //3. Mask Robot在Open Stage進行光罩夾取
                    mt.Clamp(1);

                    //4.Mask Robot將光罩從Open Stage移動至Inspection Chamber處(不放置)
                    mt.RobotMoving(true);
                    if (BoxType == 1)
                        mt.ExePathMove(@"D:\Positions\MTRobot\IronBoxToOS.json");
                    else if (BoxType == 2)
                        mt.ExePathMove(@"D:\Positions\MTRobot\CrystalBoxToOS.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                    os.ReadRobotIntrude(false, false);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    ic.ReadRobotIntrude(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeToICFrontSide.json");
                    mt.RobotMoving(false);

                    //5. 雷射sensor可以偵測到Mask Robot進入Inspection Chamber & 報值
                    About = ic.ReadRobotPosAbout();
                    UpDown = ic.ReadRobotPosUpDown();

                    //6. Mask Robot將光罩從Inspection Chamber, 移回Open Stage
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                    ic.ReadRobotIntrude(false);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    os.ReadRobotIntrude(false, true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                    if (BoxType == 1)
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToIronBox.json");
                    else if (BoxType == 2)
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToCrystalBox.json");
                    mt.RobotMoving(false);

                    //7.Mask Robot將光罩放置於Open Stage上
                    mt.Unclamp();

                    //8. Mask Robot (無夾持光罩) 從Open Stage移回Home點
                    mt.RobotMoving(true);
                    if (BoxType == 1)
                        mt.ExePathMove(@"D:\Positions\MTRobot\IronBoxToOS.json");
                    else if (BoxType == 2)
                        mt.ExePathMove(@"D:\Positions\MTRobot\CrystalBoxToOS.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                    mt.RobotMoving(false);
                    os.ReadRobotIntrude(false, false);

                    os.Close();
                    os.Unclamp();
                    os.Vacuum(false);
                    os.Lock();
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
