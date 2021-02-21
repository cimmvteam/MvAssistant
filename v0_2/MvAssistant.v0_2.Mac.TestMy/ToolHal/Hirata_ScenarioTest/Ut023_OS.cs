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
    public class Ut023_OS
    {
        BoxrobotTransferPathFile pathFileObj;//= new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
        public Ut023_OS()
        {
            PositionInstance.Load(); // 在這裏載入所有(Boxtransfer 及 Masktransfer)的路徑點位資料
            pathFileObj = new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
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
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    var ic = halContext.HalDevices[MacEnumDevice.inspectionch_assembly.ToString()] as MacHalInspectionCh;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    bt.HalConnect();
                    ic.HalConnect();
                    os.HalConnect();

                    uint BoxType = 1;//1：鐵盒 , 2：水晶盒

                    os.Initial();
                    #region Iron Box                    
                    //1. 光罩鐵盒(無光罩)放置於Open Stage上方

                    //2. Load Cell可以讀出光罩鐵盒(無光罩)重量, 確認鐵盒有放置好位置
                    var BoxWeight = os.ReadWeightOnStage();
                    if (BoxType == 1)
                    {
                        if ((BoxWeight < 560 || BoxWeight > 590) && (BoxWeight < 895 || BoxWeight > 925))
                            throw new Exception("Wrong iron box weight, box weight = " + BoxWeight.ToString());
                    }
                    else if (BoxType == 2)
                    {
                        if ((BoxWeight < 290 || BoxWeight > 320) && (BoxWeight < 625 || BoxWeight > 655))
                            throw new Exception("Wrong crystal box weight, box weight = " + BoxWeight.ToString());
                    }

                    //3. (編號10 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //4. (編號11 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //5. Open Stage固定光罩鐵盒 by 整定棒
                    os.SetBoxType(BoxType);
                    os.SortClamp();

                    //6. Open Stage固定光罩鐵盒 by 吸盤
                    os.Vacuum(true);
                    os.SortUnclamp();
                    os.Lock();

                    //7. Front CCD可以拍照取像(FOV正確)
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //8. 透過Box Robot PIN, 開啟光罩鐵盒鈕扣
                    if (BoxType == 1)
                    {
                        bt.RobotMoving(true);
                        os.ReadRobotIntrude(true, null);
                        bt.ExePathMove(pathFileObj.UnlockIronBoxPathFile());
                        os.ReadRobotIntrude(false, null);
                        bt.RobotMoving(false);
                    }
                    else if (BoxType == 2)
                    {
                        bt.RobotMoving(true);
                        os.ReadRobotIntrude(true, null);
                        bt.ExePathMove(pathFileObj.UnlockCrystalBoxPathFile());
                        os.ReadRobotIntrude(false, null);
                        bt.RobotMoving(false);
                    }

                    //9. 開盒機構正常開啟光罩鐵盒
                    os.Close();
                    os.Clamp();
                    os.Open();

                    //10. Fiber Sensor確認光罩鐵盒已開啟 & 辨識光罩盒種類
                    if (os.ReadCoverSensor().Item2 == false)
                        throw new Exception("Box status was not closed");

                    os.Close();
                    os.Unclamp();
                    os.Lock();
                    os.Vacuum(false);
                    #endregion Iron Box
                    //11. 重複1~10步驟, 再執行光罩鐵盒(有光罩) / 光罩水晶盒(無光罩) / 光罩水晶盒(有光罩)之情境
                    #region Iron Box with Mask
                    //1. 光罩鐵盒(有光罩)放置於Open Stage上方

                    //2. Load Cell可以讀出光罩鐵盒(有光罩)重量, 確認鐵盒有放置好位置
                    if (BoxType == 1)
                    {
                        if ((BoxWeight < 560 || BoxWeight > 590) && (BoxWeight < 895 || BoxWeight > 925))
                            throw new Exception("Wrong iron box weight, box weight = " + BoxWeight.ToString());
                    }
                    else if (BoxType == 2)
                    {
                        if ((BoxWeight < 290 || BoxWeight > 320) && (BoxWeight < 625 || BoxWeight > 655))
                            throw new Exception("Wrong crystal box weight, box weight = " + BoxWeight.ToString());
                    }

                    //3. (編號10 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //4. (編號11 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //5. Open Stage固定光罩鐵盒 by 整定棒
                    os.SetBoxType(BoxType);
                    os.SortClamp();

                    //6. Open Stage固定光罩鐵盒 by 吸盤
                    os.Vacuum(true);
                    os.SortUnclamp();
                    os.Lock();

                    //7. Front CCD可以拍照取像(FOV正確)
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //8. 透過Box Robot PIN, 開啟光罩鐵盒鈕扣
                    if (BoxType == 1)
                    {
                        bt.RobotMoving(true);
                        os.ReadRobotIntrude(true, null);
                        bt.ExePathMove(pathFileObj.UnlockIronBoxPathFile());
                        os.ReadRobotIntrude(false, null);
                        bt.RobotMoving(false);
                    }
                    else if (BoxType == 2)
                    {
                        bt.RobotMoving(true);
                        os.ReadRobotIntrude(true, null);
                        bt.ExePathMove(pathFileObj.UnlockCrystalBoxPathFile());
                        os.ReadRobotIntrude(false, null);
                        bt.RobotMoving(false);
                    }

                    //9. 開盒機構正常開啟光罩鐵盒
                    os.Close();
                    os.Clamp();
                    os.Open();

                    //10. Fiber Sensor確認光罩鐵盒已開啟 & 辨識光罩盒種類
                    if (os.ReadCoverSensor().Item2 == false)
                        throw new Exception("Box status was not closed");

                    os.Close();
                    os.Unclamp();
                    os.Lock();
                    os.Vacuum(false);
                    #endregion Iron Box with Mask
                    BoxType = 2;
                    //11. 重複1~10步驟, 再執行光罩水晶盒(無光罩) / 光罩水晶盒(有光罩)之情境
                    #region Crystal Box
                    //1. 光罩水晶盒(無光罩)放置於Open Stage上方

                    //2. Load Cell可以讀出光罩水晶盒(無光罩)重量, 確認水晶盒有放置好位置
                    BoxWeight = os.ReadWeightOnStage();
                    if (BoxType == 1)
                    {
                        if ((BoxWeight < 560 || BoxWeight > 590) && (BoxWeight < 895 || BoxWeight > 925))
                            throw new Exception("Wrong iron box weight, box weight = " + BoxWeight.ToString());
                    }
                    else if (BoxType == 2)
                    {
                        if ((BoxWeight < 290 || BoxWeight > 320) && (BoxWeight < 625 || BoxWeight > 655))
                            throw new Exception("Wrong crystal box weight, box weight = " + BoxWeight.ToString());
                    }

                    //3. (編號10 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //4. (編號11 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //5. Open Stage固定光罩水晶盒 by 整定棒
                    os.SetBoxType(BoxType);
                    os.SortClamp();

                    //6. Open Stage固定光罩水晶盒 by 吸盤
                    os.Vacuum(true);
                    os.SortUnclamp();
                    os.Lock();

                    //7. Front CCD可以拍照取像(FOV正確)
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //8. 透過Box Robot PIN, 開啟光罩水晶盒鈕扣
                    if (BoxType == 1)
                    {
                        bt.RobotMoving(true);
                        os.ReadRobotIntrude(true, null);
                        bt.ExePathMove(pathFileObj.UnlockIronBoxPathFile());
                        os.ReadRobotIntrude(false, null);
                        bt.RobotMoving(false);
                    }
                    else if (BoxType == 2)
                    {
                        bt.RobotMoving(true);
                        os.ReadRobotIntrude(true, null);
                        bt.ExePathMove(pathFileObj.UnlockCrystalBoxPathFile());
                        os.ReadRobotIntrude(false, null);
                        bt.RobotMoving(false);
                    }

                    //9. 開盒機構正常開啟光罩水晶盒
                    os.Close();
                    os.Clamp();
                    os.Open();

                    //10. Fiber Sensor確認光罩水晶盒已開啟 & 辨識光罩盒種類
                    if (os.ReadCoverSensor().Item2 == false)
                        throw new Exception("Box status was not closed");

                    os.Close();
                    os.Unclamp();
                    os.Lock();
                    os.Vacuum(false);
                    #endregion Crystal Box
                    //11. 重複1~10步驟, 再執光罩水晶盒(有光罩)之情境
                    #region Crystal Box with Mask
                    //1. 光罩水晶盒(有光罩)放置於Open Stage上方

                    //2. Load Cell可以讀出光罩水晶盒(有光罩)重量, 確認水晶盒有放置好位置
                    if (BoxType == 1)
                    {
                        if ((BoxWeight < 560 || BoxWeight > 590) && (BoxWeight < 895 || BoxWeight > 925))
                            throw new Exception("Wrong iron box weight, box weight = " + BoxWeight.ToString());
                    }
                    else if (BoxType == 2)
                    {
                        if ((BoxWeight < 290 || BoxWeight > 320) && (BoxWeight < 625 || BoxWeight > 655))
                            throw new Exception("Wrong crystal box weight, box weight = " + BoxWeight.ToString());
                    }

                    //3. (編號10 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //4. (編號11 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //5. Open Stage固定光罩水晶盒 by 整定棒
                    os.SetBoxType(BoxType);
                    os.SortClamp();

                    //6. Open Stage固定光罩水晶盒 by 吸盤
                    os.Vacuum(true);
                    os.SortUnclamp();
                    os.Lock();

                    //7. Front CCD可以拍照取像(FOV正確)
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //8. 透過Box Robot PIN, 開啟光罩水晶盒鈕扣
                    if (BoxType == 1)
                    {
                        bt.RobotMoving(true);
                        os.ReadRobotIntrude(true, null);
                        bt.ExePathMove(pathFileObj.UnlockIronBoxPathFile());
                        os.ReadRobotIntrude(false, null);
                        bt.RobotMoving(false);
                    }
                    else if (BoxType == 2)
                    {
                        bt.RobotMoving(true);
                        os.ReadRobotIntrude(true, null);
                        bt.ExePathMove(pathFileObj.UnlockCrystalBoxPathFile());
                        os.ReadRobotIntrude(false, null);
                        bt.RobotMoving(false);
                    }

                    //9. 開盒機構正常開啟光罩水晶盒
                    os.Close();
                    os.Clamp();
                    os.Open();

                    //10. Fiber Sensor確認光罩水晶盒已開啟 & 辨識光罩盒種類
                    if (os.ReadCoverSensor().Item2 == false)
                        throw new Exception("Box status was not closed");

                    os.Close();
                    os.Unclamp();
                    os.Lock();
                    os.Vacuum(false);
                    #endregion Crystal Box with Mask
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
