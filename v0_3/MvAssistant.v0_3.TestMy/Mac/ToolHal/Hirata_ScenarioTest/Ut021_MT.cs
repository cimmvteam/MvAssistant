﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_3.Mac.Hal;
using MvAssistant.v0_3.Mac.Hal.Assembly;
using MvAssistant.v0_3.Mac.JSon;
using MvAssistant.v0_3.Mac.JSon.RobotTransferFile;
using MvAssistant.v0_3.Mac.Manifest;

namespace MvAssistant.v0_3.Mac.TestMy.ToolHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut021_MT
    {
        MaskrobotTransferPathFile pathFileObj;//= new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
        public Ut021_MT()
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
                    var os = halContext.HalDevices[EnumMacDeviceId.openstage_assembly.ToString()] as MacHalOpenStage;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    ic.HalConnect();
                    os.HalConnect();

                    uint BoxType = 1;//1：鐵盒 , 2：水晶盒

                    os.Initial();

                    //1. 光罩放置於Open Stage上的光罩鐵盒內
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

                    //2. (編號9 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(85);//bar 0~255
                    os.LightForSideBarDfsSetValue(3);//bar 0~255
                    os.LightForTopBarDfsSetValue(50);//bar 0~255
                    os.Camera_Top_CapToSave("D:/Image/OS/Top", "jpg");
                    os.LightForFrontBarDfsSetValue(0);
                    os.LightForSideBarDfsSetValue(0);
                    os.LightForTopBarDfsSetValue(0);

                    //3. (編號12 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(85);//bar 0~255
                    os.LightForSideBarDfsSetValue(250);//bar 0~255
                    os.LightForTopBarDfsSetValue(20);//bar 0~255
                    os.Camera_Side_CapToSave("D:/Image/OS/Side", "jpg");
                    os.LightForFrontBarDfsSetValue(0);
                    os.LightForSideBarDfsSetValue(0);
                    os.LightForTopBarDfsSetValue(0);

                    //4. Mask Robot從Home點移動至Open Stage
                    mt.RobotMoving(true);
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
                    os.SetRobotIntrude(false, true);
                    mt.ExePathMove(pathFileObj.FromLPHomeToOSPathFile());
                    if (BoxType == 1)
                        mt.ExePathMove(pathFileObj.FromOSToIronBoxPathFile());
                    else if (BoxType == 2)
                        mt.ExePathMove(pathFileObj.FromOSToCrystalBoxPathFile());
                    mt.RobotMoving(false);

                    //5. Mask Robot上的傾斜角度傳感器可報值 & 位於水平狀態
                    var Level = mt.ReadLevel();

                    //6. Mask Robot在Open Stage進行光罩夾取(clamp光罩)
                    mt.Clamp(1);

                    //7. Mask Robot將光罩從Open Stage移動至Inspection Chamber Entry處
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

                    //8. Mask Robot將光罩從Inspection Chamber Entry處, 移回Open Stage
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

                    //9. Mask Robot將光罩放置於Open Stage上的光罩鐵盒內(release光罩)
                    mt.Unclamp();

                    //10. Mask Robot(無夾持光罩) 從Open Stage移回Home點
                    mt.RobotMoving(true);
                    if (BoxType == 1)
                        mt.ExePathMove(pathFileObj.FromIronBoxToOSPathFile());
                    else if (BoxType == 2)
                        mt.ExePathMove(pathFileObj.FromCrystalBoxToOSPathFile());
                    mt.ExePathMove(pathFileObj.FromOSToLPHomePathFile());
                    os.SetRobotIntrude(false, false);
                    mt.RobotMoving(false);

                    //11. (編號9 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(85);//bar 0~255
                    os.LightForSideBarDfsSetValue(3);//bar 0~255
                    os.LightForTopBarDfsSetValue(50);//bar 0~255
                    os.Camera_Top_CapToSave("D:/Image/OS/Top", "jpg");
                    os.LightForFrontBarDfsSetValue(0);
                    os.LightForSideBarDfsSetValue(0);
                    os.LightForTopBarDfsSetValue(0);

                    //12. (編號12 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(85);//bar 0~255
                    os.LightForSideBarDfsSetValue(250);//bar 0~255
                    os.LightForTopBarDfsSetValue(20);//bar 0~255
                    os.Camera_Side_CapToSave("D:/Image/OS/Side", "jpg");
                    os.LightForFrontBarDfsSetValue(0);
                    os.LightForSideBarDfsSetValue(0);
                    os.LightForTopBarDfsSetValue(0);


                    os.Close();
                    os.Unclamp();
                    os.Vacuum(false);
                    os.Lock();


                    BoxType = 2;

                    //13. 重複1~12, 完成光罩水晶盒內的光罩clamp & release測試
                    //1. 光罩放置於Open Stage上的光罩水晶盒內
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

                    //2. (編號9 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(85);//bar 0~255
                    os.LightForSideBarDfsSetValue(3);//bar 0~255
                    os.LightForTopBarDfsSetValue(50);//bar 0~255
                    os.Camera_Top_CapToSave("D:/Image/OS/Top", "jpg");
                    os.LightForFrontBarDfsSetValue(0);
                    os.LightForSideBarDfsSetValue(0);
                    os.LightForTopBarDfsSetValue(0);

                    //3. (編號12 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(85);//bar 0~255
                    os.LightForSideBarDfsSetValue(250);//bar 0~255
                    os.LightForTopBarDfsSetValue(20);//bar 0~255
                    os.Camera_Side_CapToSave("D:/Image/OS/Side", "jpg");
                    os.LightForFrontBarDfsSetValue(0);
                    os.LightForSideBarDfsSetValue(0);
                    os.LightForTopBarDfsSetValue(0);

                    //4. Mask Robot從Home點移動至Open Stage
                    mt.RobotMoving(true);
                    mt.ChangeDirection(pathFileObj.LoadPortHomePathFile());
                    os.SetRobotIntrude(false, true);
                    mt.ExePathMove(pathFileObj.FromLPHomeToOSPathFile());
                    if (BoxType == 1)
                        mt.ExePathMove(pathFileObj.FromOSToIronBoxPathFile());
                    else if (BoxType == 2)
                        mt.ExePathMove(pathFileObj.FromOSToCrystalBoxPathFile());
                    mt.RobotMoving(false);

                    //5. Mask Robot上的傾斜角度傳感器可報值 & 位於水平狀態
                    Level = mt.ReadLevel();

                    //6. Mask Robot在Open Stage進行光罩夾取(clamp光罩)
                    mt.Clamp(1);

                    //7. Mask Robot將光罩從Open Stage移動至Inspection Chamber Entry處
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

                    //8. Mask Robot將光罩從Inspection Chamber Entry處, 移回Open Stage
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

                    //9. Mask Robot將光罩放置於Open Stage上的光罩水晶盒內(release光罩)
                    mt.Unclamp();

                    //10. Mask Robot(無夾持光罩) 從Open Stage移回Home點
                    mt.RobotMoving(true);
                    if (BoxType == 1)
                        mt.ExePathMove(pathFileObj.FromIronBoxToOSPathFile());
                    else if (BoxType == 2)
                        mt.ExePathMove(pathFileObj.FromCrystalBoxToOSPathFile());
                    mt.ExePathMove(pathFileObj.FromOSToLPHomePathFile());
                    os.SetRobotIntrude(false, false);
                    mt.RobotMoving(false);

                    //11. (編號9 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(85);//bar 0~255
                    os.LightForSideBarDfsSetValue(3);//bar 0~255
                    os.LightForTopBarDfsSetValue(50);//bar 0~255
                    os.Camera_Top_CapToSave("D:/Image/OS/Top", "jpg");
                    os.LightForFrontBarDfsSetValue(0);
                    os.LightForSideBarDfsSetValue(0);
                    os.LightForTopBarDfsSetValue(0);

                    //12. (編號12 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(85);//bar 0~255
                    os.LightForSideBarDfsSetValue(250);//bar 0~255
                    os.LightForTopBarDfsSetValue(20);//bar 0~255
                    os.Camera_Side_CapToSave("D:/Image/OS/Side", "jpg");
                    os.LightForFrontBarDfsSetValue(0);
                    os.LightForSideBarDfsSetValue(0);
                    os.LightForTopBarDfsSetValue(0);


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
