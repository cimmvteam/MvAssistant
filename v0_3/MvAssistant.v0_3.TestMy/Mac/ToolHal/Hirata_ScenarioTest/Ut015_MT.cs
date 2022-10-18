using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_3.Mac.Hal;
using MvAssistant.v0_3.Mac.Hal.Assembly;
using MvAssistant.v0_3.Mac.JSon;
using MvAssistant.v0_3.Mac.JSon.RobotTransferFile;
using MvAssistant.v0_3.Mac.Manifest;

namespace MvAssistant.v0_3.Mac.TestMy.ToolHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut015_MT
    {
        MaskrobotTransferPathFile pathFileObj;//= new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
        public Ut015_MT()
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
                    var ic = halContext.HalDevices[EnumMacDeviceId.inspectionch_assembly.ToString()] as MacHalInspectionCh;
                    var lpa = halContext.HalDevices[EnumMacDeviceId.loadportA_assembly.ToString()] as MacHalLoadPort;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    lpa.HalConnect();
                    ic.HalConnect();

                    ic.Initial();

                    //1. Mask Robot (無夾持光罩) 從Home點移動至Inspection Chamber
                    ic.SetRobotIntrude(true);
                    mt.RobotMoving(true);
                    mt.ChangeDirection(pathFileObj.InspChHomePathFile());
                    mt.ExePathMove(pathFileObj.FromICHomeToICFrontSidePathFile());
                    mt.RobotMoving(false);

                    //2. 雷射sensor可以偵測到Mask Robot進入Inspection Chamber & 報值
                    var about = ic.ReadRobotPosLeftRight();
                    var UpDown = ic.ReadRobotPosUpDown();


                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromICFrontSideToICHomePathFile());
                    ic.SetRobotIntrude(false);
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
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

                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    ic.HalConnect();

                    ic.Initial();

                    //1. 光罩放置於Load Port A的Pod上

                    //2. Mask Robot從Home點移動至Load Port A
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLPHomeToLP1PathFile());
                    mt.RobotMoving(false);

                    //3.Mask Robot在Load Port A的Pod 內進行光罩夾取
                    mt.Clamp(1);

                    //4. Mask Robot將光罩從Load Port A移動至Inspection Chamber內 (不放置)
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLP1ToLPHomePathFile());
                    mt.ChangeDirection(pathFileObj.InspChHomePathFile());
                    ic.SetRobotIntrude(true);
                    mt.ExePathMove(pathFileObj.FromICHomeToICFrontSidePathFile());
                    mt.RobotMoving(false);

                    //5. 雷射sensor可以偵測到Mask Robot進入Inspection Chamber & 報值
                    var About = ic.ReadRobotPosLeftRight();
                    var UpDown = ic.ReadRobotPosUpDown();

                    //6. Mask Robot將光罩從Inspection Chamber, 移回Load Port A
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromICFrontSideToICHomePathFile());
                    ic.SetRobotIntrude(false);
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
                    mt.ExePathMove(pathFileObj.FromLPHomeToLP1PathFile());
                    mt.RobotMoving(false);

                    //7. Mask Robot將光罩放置於Load Port A Pod上
                    mt.Unclamp();

                    //8. Mask Robot (無夾持光罩) 從Load Port A移回Home點
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLP1ToLPHomePathFile());
                    mt.RobotMoving(false);


                    About = 0;
                    UpDown = 0;
                    //9 重複1~8, 執行Load Port B->Inspection Chamber光罩傳送
                    //1. 光罩放置於Load Port B的Pod上

                    //2. Mask Robot從Home點移動至Load Port B
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLPHomeToLP2PathFile());
                    mt.RobotMoving(false);

                    //3.Mask Robot在Load Port B的Pod 內進行光罩夾取
                    mt.Clamp(1);

                    //4. Mask Robot將光罩從Load Port B移動至Inspection Chamber內 (不放置)
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLP2ToLPHomePathFile());
                    mt.ChangeDirection(pathFileObj.InspChHomePathFile());
                    ic.SetRobotIntrude(true);
                    mt.ExePathMove(pathFileObj.FromICHomeToICFrontSidePathFile());
                    mt.RobotMoving(false);

                    //5. 雷射sensor可以偵測到Mask Robot進入Inspection Chamber & 報值
                    About = ic.ReadRobotPosLeftRight();
                    UpDown = ic.ReadRobotPosUpDown();

                    //6. Mask Robot將光罩從Inspection Chamber, 移回Load Port B
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromICFrontSideToICHomePathFile());
                    ic.SetRobotIntrude(false);
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
                    mt.ExePathMove(pathFileObj.FromLPHomeToLP2PathFile());
                    mt.RobotMoving(false);

                    //7. Mask Robot將光罩放置於Load Port B Pod上
                    mt.Unclamp();

                    //8. Mask Robot (無夾持光罩) 從Load Port B移回Home點
                    mt.RobotMoving(true);
                    mt.ExePathMove(pathFileObj.FromLP2ToLPHomePathFile());
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
                    var os = halContext.HalDevices[EnumMacDeviceId.openstage_assembly.ToString()] as MacHalOpenStage;
                    var ic = halContext.HalDevices[EnumMacDeviceId.inspectionch_assembly.ToString()] as MacHalInspectionCh;
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

                    //4.Mask Robot將光罩從Open Stage移動至Inspection Chamber處(不放置)
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
                    mt.RobotMoving(false);

                    //5. 雷射sensor可以偵測到Mask Robot進入Inspection Chamber & 報值
                    var About = ic.ReadRobotPosLeftRight();
                    var UpDown = ic.ReadRobotPosUpDown();

                    //6. Mask Robot將光罩從Inspection Chamber, 移回Open Stage
                    mt.RobotMoving(true);
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

                    //7.Mask Robot將光罩放置於Open Stage上
                    mt.Unclamp();

                    //8. Mask Robot (無夾持光罩) 從Open Stage移回Home點
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

                    //4.Mask Robot將光罩從Open Stage移動至Inspection Chamber處(不放置)
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
                    mt.RobotMoving(false);

                    //5. 雷射sensor可以偵測到Mask Robot進入Inspection Chamber & 報值
                    About = ic.ReadRobotPosLeftRight();
                    UpDown = ic.ReadRobotPosUpDown();

                    //6. Mask Robot將光罩從Inspection Chamber, 移回Open Stage
                    mt.RobotMoving(true);
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

                    //7.Mask Robot將光罩放置於Open Stage上
                    mt.Unclamp();

                    //8. Mask Robot (無夾持光罩) 從Open Stage移回Home點
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
