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
    public class Ut017_MT
    {
        MaskrobotTransferPathFile pathFileObj;//= new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
        public Ut017_MT()
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
                    var os = halContext.HalDevices[EnumMacDeviceId.openstage_assembly.ToString()] as MacHalOpenStage;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    os.HalConnect();

                    uint BoxType = 1;//1：鐵盒 , 2：水晶盒

                    os.Initial();

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

                    //1. Mask Robot (無夾持光罩) 從Home點移動至Open Stage
                    mt.RobotMoving(true);
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
                    os.SetRobotIntrude(null, true);
                    mt.ExePathMove(pathFileObj.FromLPHomeToOSPathFile());
                    if (BoxType == 1)
                        mt.ExePathMove(pathFileObj.FromOSToIronBoxPathFile());
                    else if (BoxType == 2)
                        mt.ExePathMove(pathFileObj.FromOSToCrystalBoxPathFile());

                    if (BoxType == 1)
                        mt.ExePathMove(pathFileObj.FromIronBoxToOSPathFile());
                    else if (BoxType == 2)
                        mt.ExePathMove(pathFileObj.FromCrystalBoxToOSPathFile());
                    mt.ExePathMove(pathFileObj.FromOSToLPHomePathFile());
                    os.SetRobotIntrude(null, false);
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
                    halContext.MvaCfBootup();
                    halContext.MvaCfLoad();

                    var unv = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                    var mt = halContext.HalDevices[EnumMacDeviceId.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var ic = halContext.HalDevices[EnumMacDeviceId.inspectionch_assembly.ToString()] as MacHalInspectionCh;
                    var os = halContext.HalDevices[EnumMacDeviceId.openstage_assembly.ToString()] as MacHalOpenStage;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    os.HalConnect();
                    ic.HalConnect();

                    uint BoxType = 1;//1：鐵盒 , 2：水晶盒

                    ic.Initial();

                    //1. 光罩放置於Inspection Chamber Stage上

                    //2. Mask Robot從Home點移動至Inspection Chamber
                    mt.RobotMoving(true);
                    mt.ChangeDirection(pathFileObj.InspChHomePathFile());
                    ic.SetRobotIntrude(true);
                    mt.ExePathMove(pathFileObj.FromICHomeToICFrontSidePathFile());
                    mt.ExePathMove(pathFileObj.FromICFrontSideToICStagePathFile());
                    mt.RobotMoving(false);

                    //3. Mask Robot在Inspection Chamber 內進行光罩夾取
                    mt.Clamp(1);

                    //4. Mask Robot將光罩從Inspection Chamber移動至Open Stage處 (不放置)
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromICStageToICFrontSidePathFile());
                    mt.ExePathMove(pathFileObj.FromICFrontSideToICHomePathFile());
                    ic.SetRobotIntrude(false);
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
                    os.SetRobotIntrude(false, true);
                    mt.ExePathMove(pathFileObj.FromLPHomeToOSPathFile());
                    if (BoxType == 1)
                        mt.ExePathMove(pathFileObj.FromOSToIronBoxPathFile());
                    else if (BoxType == 2)
                        mt.ExePathMove(pathFileObj.FromOSToCrystalBoxPathFile());
                    mt.RobotMoving(false);

                    //5. Mask Robot將光罩從Open Stage, 移回Inspection Chamber
                    mt.RobotMoving(true);
                    if (BoxType == 1)
                        mt.ExePathMove(pathFileObj.FromIronBoxToOSPathFile());
                    else if (BoxType == 2)
                        mt.ExePathMove(pathFileObj.FromCrystalBoxToOSPathFile());
                    mt.ExePathMove(pathFileObj.FromOSToLPHomePathFile());
                    os.SetRobotIntrude(false, false);
                    mt.ChangeDirection(pathFileObj.InspChHomePathFile());
                    ic.SetRobotIntrude(true);
                    mt.ExePathMove(pathFileObj.FromICHomeToICFrontSidePathFile());
                    mt.ExePathMove(pathFileObj.FromICFrontSideToICStagePathFile());
                    mt.RobotMoving(false);

                    //6. Mask Robot將光罩放置於Inspection Chamber Stage上
                    mt.Unclamp();

                    //7.Mask Robot(無夾持光罩) 從Inspection Chamber移回Home點
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

        [TestMethod]
        public void TestMethod3()//OK
        {
            try
            {
                using (var halContext = new MacHalContext("UserData/Manifest/Manifest.xml.real"))
                {
                    halContext.MvaCfBootup();
                    halContext.MvaCfLoad();

                    var unv = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                    var mt = halContext.HalDevices[EnumMacDeviceId.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var lpa = halContext.HalDevices[EnumMacDeviceId.loadportA_assembly.ToString()] as MacHalLoadPort;
                    var lpb = halContext.HalDevices[EnumMacDeviceId.loadportA_assembly.ToString()] as MacHalLoadPort;
                    var os = halContext.HalDevices[EnumMacDeviceId.openstage_assembly.ToString()] as MacHalOpenStage;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    os.HalConnect();
                    lpa.HalConnect();
                    lpb.HalConnect();

                    uint BoxType = 1;//1：鐵盒 , 2：水晶盒

                    //1.光罩放置於Load Port A的Pod內

                    //2. Mask Robot從Home點移動至Load Port A上方
                    mt.RobotMoving(true);
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
                    mt.ExePathMove(pathFileObj.FromLPHomeToLP1PathFile());
                    mt.RobotMoving(false);

                    //3. Mask Robot在Load Port A進行光罩夾取
                    mt.Clamp(1);

                    //4. Mask Robot將光罩從Load Port A移動至Open Stage處 (不放置)
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLP1ToLPHomePathFile());
                    os.SetRobotIntrude(false, true);
                    mt.ExePathMove(pathFileObj.FromLPHomeToOSPathFile());
                    if (BoxType == 1)
                        mt.ExePathMove(pathFileObj.FromOSToIronBoxPathFile());
                    else if (BoxType == 2)
                        mt.ExePathMove(pathFileObj.FromOSToCrystalBoxPathFile());
                    mt.RobotMoving(false);

                    //5. Mask Robot將光罩從Open Stage, 移回Load Port A
                    mt.RobotMoving(true);
                    if (BoxType == 1)
                        mt.ExePathMove(pathFileObj.FromIronBoxToOSPathFile());
                    else if (BoxType == 2)
                        mt.ExePathMove(pathFileObj.FromCrystalBoxToOSPathFile());
                    mt.ExePathMove(pathFileObj.FromOSToLPHomePathFile());
                    os.SetRobotIntrude(false, false);
                    mt.ExePathMove(pathFileObj.FromLPHomeToLP1PathFile());
                    mt.RobotMoving(false);

                    //6. Mask Robot將光罩放置於Load Port A的Pod內
                    mt.Unclamp();

                    //7. Mask Robot (無夾持光罩) 從Load Port A移回Home點
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLP1ToLPHomePathFile());
                    mt.RobotMoving(false);


                    //8. 重複1~7, 執行Load Port B至Open Stage的傳送測試
                    //1.光罩放置於Load Port B的Pod內

                    //2. Mask Robot從Home點移動至Load Port B上方
                    mt.RobotMoving(true);
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
                    mt.ExePathMove(pathFileObj.FromLPHomeToLP2PathFile());
                    mt.RobotMoving(false);

                    //3. Mask Robot在Load Port B進行光罩夾取
                    mt.Clamp(1);

                    //4. Mask Robot將光罩從Load Port B移動至Open Stage處 (不放置)
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLP2ToLPHomePathFile());
                    os.SetRobotIntrude(false, true);
                    mt.ExePathMove(pathFileObj.FromLPHomeToOSPathFile());
                    if (BoxType == 1)
                        mt.ExePathMove(pathFileObj.FromOSToIronBoxPathFile());
                    else if (BoxType == 2)
                        mt.ExePathMove(pathFileObj.FromOSToCrystalBoxPathFile());
                    mt.RobotMoving(false);

                    //5. Mask Robot將光罩從Open Stage, 移回Load Port B
                    mt.RobotMoving(true);
                    if (BoxType == 1)
                        mt.ExePathMove(pathFileObj.FromIronBoxToOSPathFile());
                    else if (BoxType == 2)
                        mt.ExePathMove(pathFileObj.FromCrystalBoxToOSPathFile());
                    mt.ExePathMove(pathFileObj.FromOSToLPHomePathFile());
                    os.SetRobotIntrude(false, false);
                    mt.ExePathMove(pathFileObj.FromLPHomeToLP2PathFile());
                    mt.RobotMoving(false);

                    //6. Mask Robot將光罩放置於Load Port B的Pod內
                    mt.Unclamp();

                    //7. Mask Robot (無夾持光罩) 從Load Port B移回Home點
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLP2ToLPHomePathFile());
                    mt.RobotMoving(false);
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
