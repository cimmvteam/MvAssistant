using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut014_MT
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
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();

                    //1. Mask Robot (無夾持光罩) 從Home點移動至Load Port B
                    mt.RobotMoving(true);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
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
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    var lpb = halContext.HalDevices[MacEnumDevice.loadportB_assembly.ToString()] as MacHalLoadPort;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    lpb.HalConnect();
                    ic.HalConnect();

                    ic.Initial();

                    //1. 光罩放置於Inspection Chamber Stage上

                    //2. Mask Robot從Home點移動至Inspection Chamber
                    ic.ReadRobotIntrude(true);
                    mt.RobotMoving(true);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeToICFrontSide.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICStage.json");
                    mt.RobotMoving(false);

                    //3. Mask Robot在Inspection Chamber 內進行光罩夾取
                    mt.Clamp(1);

                    //4. Mask Robot將光罩從Inspection Chamber移動至Load Port B處 (不放置)
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICStageToICFrontSide.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                    ic.ReadRobotIntrude(false);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
                    mt.RobotMoving(false);

                    //5. Mask Robot將光罩從Load Port B, 移回Inspection Chamber
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    ic.ReadRobotIntrude(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeToICFrontSide.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICStage.json");
                    mt.RobotMoving(false);

                    //6. Mask Robot將光罩放置於Inspection Chamber Stage上
                    mt.Unclamp();

                    //7. Mask Robot (無夾持光罩) 從Inspection Chamber移回Home點
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
                    var lpa = halContext.HalDevices[MacEnumDevice.loadportA_assembly.ToString()] as MacHalLoadPort;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    lpa.HalConnect();
                    os.HalConnect();

                    uint BoxType = 1;//1：鐵盒 , 2：水晶盒

                    os.Initial();

                    //1. 光罩放置於Open Stage上的鐵盒內
                    os.SetBoxType(BoxType);
                    os.SortClamp();
                    os.Vacuum(true);
                    os.SortUnclamp();
                    os.Lock();
                    var BoxWeight = os.ReadWeightOnStage();
                    if (os.ReadCoverSensor().Item2 == false)
                        throw new Exception("Box status was not closed");
                    os.Close();
                    os.Clamp();
                    os.Open();
                    if (os.ReadCoverSensor().Item1 == false)
                        throw new Exception("Box status was not opened");


                    //2. Mask Robot從Home點移動至Open Stage上方
                    os.ReadRobotIntrude(false, true);
                    mt.RobotMoving(true);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                    if (BoxType == 1)
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToIronBox.json");
                    else if (BoxType == 2)
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToCrystalBox.json");
                    mt.RobotMoving(false);

                    //3. Mask Robot在Open Stage進行光罩夾取
                    mt.Clamp(1);

                    //4. Mask Robot將光罩從Open Stage移動至Load Port B處 (不放置)
                    mt.RobotMoving(true);
                    if (BoxType == 1)
                        mt.ExePathMove(@"D:\Positions\MTRobot\IronBoxToOS.json");
                    else if (BoxType == 2)
                        mt.ExePathMove(@"D:\Positions\MTRobot\CrystalBoxToOS.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                    os.ReadRobotIntrude(false, false);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
                    mt.RobotMoving(false);

                    //5. Mask Robot將光罩從Load Port A, 移回Open Stage
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
                    os.ReadRobotIntrude(false, true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                    if (BoxType == 1)
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToIronBox.json");
                    else if (BoxType == 2)
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToCrystalBox.json");
                    mt.RobotMoving(false);

                    //6. Mask Robot將光罩放置於Open Stage上
                    mt.Unclamp();

                    //7. Mask Robot (無夾持光罩) 從Open Stage移回Home點
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

                    BoxType = 2;
                    //8. 重複1~7, 執行Open Stage上水晶盒的傳送測試
                    //1. 光罩放置於Open Stage上的水晶盒內
                    os.SetBoxType(BoxType);
                    os.SortClamp();
                    os.Vacuum(true);
                    os.SortUnclamp();
                    os.Lock();
                    BoxWeight = os.ReadWeightOnStage();
                    if (os.ReadCoverSensor().Item2 == false)
                        throw new Exception("Box status was not closed");
                    os.Close();
                    os.Clamp();
                    os.Open();
                    if (os.ReadCoverSensor().Item1 == false)
                        throw new Exception("Box status was not opened");


                    //2. Mask Robot從Home點移動至Open Stage上方
                    os.ReadRobotIntrude(false, true);
                    mt.RobotMoving(true);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                    if (BoxType == 1)
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToIronBox.json");
                    else if (BoxType == 2)
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToCrystalBox.json");
                    mt.RobotMoving(false);

                    //3. Mask Robot在Open Stage進行光罩夾取
                    mt.Clamp(1);

                    //4. Mask Robot將光罩從Open Stage移動至Load Port B處 (不放置)
                    mt.RobotMoving(true);
                    if (BoxType == 1)
                        mt.ExePathMove(@"D:\Positions\MTRobot\IronBoxToOS.json");
                    else if (BoxType == 2)
                        mt.ExePathMove(@"D:\Positions\MTRobot\CrystalBoxToOS.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                    os.ReadRobotIntrude(false, false);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
                    mt.RobotMoving(false);

                    //5. Mask Robot將光罩從Load Port B, 移回Open Stage
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
                    os.ReadRobotIntrude(false, true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                    if (BoxType == 1)
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToIronBox.json");
                    else if (BoxType == 2)
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToCrystalBox.json");
                    mt.RobotMoving(false);

                    //6. Mask Robot將光罩放置於Open Stage上
                    mt.Unclamp();

                    //7. Mask Robot (無夾持光罩) 從Open Stage移回Home點
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
