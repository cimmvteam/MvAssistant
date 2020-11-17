﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut023_OS
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
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
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
                        if ((BoxWeight < 775 || BoxWeight > 778) && (BoxWeight < 1102 || BoxWeight > 1104))
                            throw new Exception("Wrong iron box weight, box weight = " + BoxWeight.ToString());
                    }
                    else if (BoxType == 2)
                    {
                        if ((BoxWeight < 589 || BoxWeight > 590) && (BoxWeight < 918 || BoxWeight > 920))
                            throw new Exception("Wrong crystal box weight, box weight = " + BoxWeight.ToString());
                    }

                    //3. (編號10 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(200);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //4. (編號11 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(200);
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

                    //8. 透過Box Robot PIN, 開啟光罩鐵盒鈕扣
                    if (BoxType == 1)
                        bt.ExePathMove(@"D:\Positions\BTRobot\UnlockIronBox.json");
                    else if (BoxType == 2)
                        bt.ExePathMove(@"D:\Positions\BTRobot\UnlockCrystalBox.json");

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
                        if ((BoxWeight < 775 || BoxWeight > 778) && (BoxWeight < 1102 || BoxWeight > 1104))
                            throw new Exception("Wrong iron box weight, box weight = " + BoxWeight.ToString());
                    }
                    else if (BoxType == 2)
                    {
                        if ((BoxWeight < 589 || BoxWeight > 590) && (BoxWeight < 918 || BoxWeight > 920))
                            throw new Exception("Wrong crystal box weight, box weight = " + BoxWeight.ToString());
                    }

                    //3. (編號10 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(200);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //4. (編號11 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(200);
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

                    //8. 透過Box Robot PIN, 開啟光罩鐵盒鈕扣
                    if (BoxType == 1)
                        bt.ExePathMove(@"D:\Positions\BTRobot\UnlockIronBox.json");
                    else if (BoxType == 2)
                        bt.ExePathMove(@"D:\Positions\BTRobot\UnlockCrystalBox.json");

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
                        if ((BoxWeight < 775 || BoxWeight > 778) && (BoxWeight < 1102 || BoxWeight > 1104))
                            throw new Exception("Wrong iron box weight, box weight = " + BoxWeight.ToString());
                    }
                    else if (BoxType == 2)
                    {
                        if ((BoxWeight < 589 || BoxWeight > 590) && (BoxWeight < 918 || BoxWeight > 920))
                            throw new Exception("Wrong crystal box weight, box weight = " + BoxWeight.ToString());
                    }

                    //3. (編號10 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(200);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //4. (編號11 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(200);
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

                    //8. 透過Box Robot PIN, 開啟光罩水晶盒鈕扣
                    if (BoxType == 1)
                        bt.ExePathMove(@"D:\Positions\BTRobot\UnlockIronBox.json");
                    else if (BoxType == 2)
                        bt.ExePathMove(@"D:\Positions\BTRobot\UnlockCrystalBox.json");

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
                        if ((BoxWeight < 775 || BoxWeight > 778) && (BoxWeight < 1102 || BoxWeight > 1104))
                            throw new Exception("Wrong iron box weight, box weight = " + BoxWeight.ToString());
                    }
                    else if (BoxType == 2)
                    {
                        if ((BoxWeight < 589 || BoxWeight > 590) && (BoxWeight < 918 || BoxWeight > 920))
                            throw new Exception("Wrong crystal box weight, box weight = " + BoxWeight.ToString());
                    }

                    //3. (編號10 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(200);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //4. (編號11 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(200);
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

                    //8. 透過Box Robot PIN, 開啟光罩水晶盒鈕扣
                    if (BoxType == 1)
                        bt.ExePathMove(@"D:\Positions\BTRobot\UnlockIronBox.json");
                    else if (BoxType == 2)
                        bt.ExePathMove(@"D:\Positions\BTRobot\UnlockCrystalBox.json");

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
