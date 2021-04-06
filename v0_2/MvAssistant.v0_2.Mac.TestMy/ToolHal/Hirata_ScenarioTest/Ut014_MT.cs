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
    public class Ut014_MT
    {
        MaskrobotTransferPathFile pathFileObj;//= new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
        public Ut014_MT()
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
                    halContext.MvaCfInit();
                    halContext.MvaCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();

                    //1. Mask Robot (無夾持光罩) 從Home點移動至Load Port B
                    mt.RobotMoving(true);
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
                    mt.ExePathMove(pathFileObj.FromLPHomeToLP2PathFile());
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
                    halContext.MvaCfInit();
                    halContext.MvaCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var ic = halContext.HalDevices[MacEnumDevice.inspectionch_assembly.ToString()] as MacHalInspectionCh;
                    var lpb = halContext.HalDevices[MacEnumDevice.loadportB_assembly.ToString()] as MacHalLoadPort;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    lpb.HalConnect();
                    ic.HalConnect();

                    ic.Initial();

                    //1. 光罩放置於Inspection Chamber Stage上

                    //2. Mask Robot從Home點移動至Inspection Chamber
                    ic.SetRobotIntrude(true);
                    mt.RobotMoving(true);
                    mt.ChangeDirection(pathFileObj.InspChHomePathFile());
                    mt.ExePathMove(pathFileObj.FromICHomeToICFrontSidePathFile());
                    mt.ExePathMove(pathFileObj.FromICFrontSideToICStagePathFile());
                    mt.RobotMoving(false);

                    //3. Mask Robot在Inspection Chamber 內進行光罩夾取
                    mt.Clamp(1);

                    //4. Mask Robot將光罩從Inspection Chamber移動至Load Port B處 (不放置)
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromICStageToICFrontSidePathFile());
                    mt.ExePathMove(pathFileObj.FromICFrontSideToICHomePathFile());
                    ic.SetRobotIntrude(false);
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
                    mt.ExePathMove(pathFileObj.FromLPHomeToLP2PathFile());
                    mt.RobotMoving(false);

                    //5. Mask Robot將光罩從Load Port B, 移回Inspection Chamber
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLP2ToLPHomePathFile());
                    mt.ChangeDirection(pathFileObj.InspChHomePathFile());
                    ic.SetRobotIntrude(true);
                    mt.ExePathMove(pathFileObj.FromICHomeToICFrontSidePathFile());
                    mt.ExePathMove(pathFileObj.FromICFrontSideToICStagePathFile());
                    mt.RobotMoving(false);

                    //6. Mask Robot將光罩放置於Inspection Chamber Stage上
                    mt.Unclamp();

                    //7. Mask Robot (無夾持光罩) 從Inspection Chamber移回Home點
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
                    halContext.MvaCfInit();
                    halContext.MvaCfLoad();

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
                    os.SetRobotIntrude(false, true);
                    mt.RobotMoving(true);
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
                    os.SetRobotIntrude(false, true);
                    mt.ExePathMove(pathFileObj.FromLPHomeToOSPathFile());
                    if (BoxType == 1)
                        mt.ExePathMove(pathFileObj.FromOSToIronBoxPathFile());
                    else if (BoxType == 2)
                        mt.ExePathMove(pathFileObj.FromOSToCrystalBoxPathFile());
                    mt.RobotMoving(false);

                    //3. Mask Robot在Open Stage進行光罩夾取
                    mt.Clamp(1);

                    //4. Mask Robot將光罩從Open Stage移動至Load Port B處 (不放置)
                    mt.RobotMoving(true);
                    if (BoxType == 1)
                        mt.ExePathMove(pathFileObj.FromIronBoxToOSPathFile());
                    else if (BoxType == 2)
                        mt.ExePathMove(pathFileObj.FromCrystalBoxToOSPathFile());
                    mt.ExePathMove(pathFileObj.FromOSToLPHomePathFile());
                    os.SetRobotIntrude(false, false);
                    mt.ExePathMove(pathFileObj.FromLPHomeToLP2PathFile());
                    mt.RobotMoving(false);

                    //5. Mask Robot將光罩從Load Port B, 移回Open Stage
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLP2ToLPHomePathFile());
                    os.SetRobotIntrude(false, true);
                    mt.ExePathMove(pathFileObj.FromLPHomeToOSPathFile());
                    if (BoxType == 1)
                        mt.ExePathMove(pathFileObj.FromOSToIronBoxPathFile());
                    else if (BoxType == 2)
                        mt.ExePathMove(pathFileObj.FromOSToCrystalBoxPathFile());
                    mt.RobotMoving(false);

                    //6. Mask Robot將光罩放置於Open Stage上
                    mt.Unclamp();

                    //7. Mask Robot (無夾持光罩) 從Open Stage移回Home點
                    mt.RobotMoving(true);
                    if (BoxType == 1)
                        mt.ExePathMove(pathFileObj.FromIronBoxToOSPathFile());
                    else if (BoxType == 2)
                        mt.ExePathMove(pathFileObj.FromCrystalBoxToOSPathFile());
                    mt.ExePathMove(pathFileObj.FromOSToLPHomePathFile());
                    mt.RobotMoving(false);
                    os.SetRobotIntrude(false, false);

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
                    os.SetRobotIntrude(false, true);
                    mt.RobotMoving(true);
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
                    os.SetRobotIntrude(false, true);
                    mt.ExePathMove(pathFileObj.FromLPHomeToOSPathFile());
                    if (BoxType == 1)
                        mt.ExePathMove(pathFileObj.FromOSToIronBoxPathFile());
                    else if (BoxType == 2)
                        mt.ExePathMove(pathFileObj.FromOSToCrystalBoxPathFile());
                    mt.RobotMoving(false);

                    //3. Mask Robot在Open Stage進行光罩夾取
                    mt.Clamp(1);

                    //4. Mask Robot將光罩從Open Stage移動至Load Port B處 (不放置)
                    mt.RobotMoving(true);
                    if (BoxType == 1)
                        mt.ExePathMove(pathFileObj.FromIronBoxToOSPathFile());
                    else if (BoxType == 2)
                        mt.ExePathMove(pathFileObj.FromCrystalBoxToOSPathFile());
                    mt.ExePathMove(pathFileObj.FromOSToLPHomePathFile());
                    os.SetRobotIntrude(false, false);
                    mt.ExePathMove(pathFileObj.FromLPHomeToLP2PathFile());
                    mt.RobotMoving(false);

                    //5. Mask Robot將光罩從Load Port B, 移回Open Stage
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLP2ToLPHomePathFile());
                    os.SetRobotIntrude(false, true);
                    mt.ExePathMove(pathFileObj.FromLPHomeToOSPathFile());
                    if (BoxType == 1)
                        mt.ExePathMove(pathFileObj.FromOSToIronBoxPathFile());
                    else if (BoxType == 2)
                        mt.ExePathMove(pathFileObj.FromOSToCrystalBoxPathFile());
                    mt.RobotMoving(false);

                    //6. Mask Robot將光罩放置於Open Stage上
                    mt.Unclamp();

                    //7. Mask Robot (無夾持光罩) 從Open Stage移回Home點
                    mt.RobotMoving(true);
                    if (BoxType == 1)
                        mt.ExePathMove(pathFileObj.FromIronBoxToOSPathFile());
                    else if (BoxType == 2)
                        mt.ExePathMove(pathFileObj.FromCrystalBoxToOSPathFile());
                    mt.ExePathMove(pathFileObj.FromOSToLPHomePathFile());
                    mt.RobotMoving(false);
                    os.SetRobotIntrude(false, false);


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
