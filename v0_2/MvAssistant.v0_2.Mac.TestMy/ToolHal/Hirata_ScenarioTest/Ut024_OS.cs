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
    public class Ut024_OS
    {
        BoxrobotTransferPathFile pathFileObj;//= new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
        public Ut024_OS()
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
                    //1.光罩鐵盒(無光罩)放置於Open Stage上方且上蓋已是開啟狀態
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

                    //2. Fiber Sensor確認光罩鐵盒上蓋已開啟 & 辨識光罩盒種類
                    if (os.ReadCoverSensor().Item1 == false)
                        throw new Exception("Box status was not opened");

                    //3. (編號10 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //4. (編號11 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //5. 開關盒機構可正常闔上光罩鐵盒上蓋
                    os.Close();
                    os.Unclamp();
                    os.Lock();

                    //6. 透過Box Robot PIN, 關閉光罩鐵盒鈕扣
                    if (BoxType == 1)
                    {
                        bt.RobotMoving(true);
                        os.ReadRobotIntrude(true, null);
                        bt.ExePathMove(pathFileObj.LockIronBoxPathFile());
                        os.ReadRobotIntrude(false, null);
                        bt.RobotMoving(false);
                    }
                    else if (BoxType == 2)
                    {
                        bt.RobotMoving(true);
                        os.ReadRobotIntrude(true, null);
                        bt.ExePathMove(pathFileObj.LockCrystalBoxPathFile());
                        os.ReadRobotIntrude(false, null);
                        bt.RobotMoving(false);
                    }

                    //7. Front CCD可以拍照取像(FOV正確)
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //8. Open Stage吸盤鬆開光罩鐵盒
                    os.Vacuum(false);

                    //9. Open Stage整定棒鬆開光罩鐵盒
                    os.SortUnclamp();

                    //10. (編號10 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //11. (編號11 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                    os.LightForFrontBarDfsSetValue(0);
                    #endregion Iron Box
                    //12. 重複1~11步驟, 再執行光罩鐵盒(有光罩) / 光罩水晶盒(無光罩) / 光罩水晶盒(有光罩)之情境
                    #region Iron Box with Mask
                    //1.光罩鐵盒(有光罩)放置於Open Stage上方且上蓋已是開啟狀態
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

                    //2. Fiber Sensor確認光罩鐵盒上蓋已開啟 & 辨識光罩盒種類
                    if (os.ReadCoverSensor().Item1 == false)
                        throw new Exception("Box status was not opened");

                    //3. (編號10 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //4. (編號11 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //5. 開關盒機構可正常闔上光罩鐵盒上蓋
                    os.Close();
                    os.Unclamp();
                    os.Lock();

                    //6. 透過Box Robot PIN, 關閉光罩鐵盒鈕扣
                    if (BoxType == 1)
                    {
                        bt.RobotMoving(true);
                        os.ReadRobotIntrude(true, null);
                        bt.ExePathMove(pathFileObj.LockIronBoxPathFile());
                        os.ReadRobotIntrude(false, null);
                        bt.RobotMoving(false);
                    }
                    else if (BoxType == 2)
                    {
                        bt.RobotMoving(true);
                        os.ReadRobotIntrude(true, null);
                        bt.ExePathMove(pathFileObj.LockCrystalBoxPathFile());
                        os.ReadRobotIntrude(false, null);
                        bt.RobotMoving(false);
                    }

                    //7. Front CCD可以拍照取像(FOV正確)
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //8. Open Stage吸盤鬆開光罩鐵盒
                    os.Vacuum(false);

                    //9. Open Stage整定棒鬆開光罩鐵盒
                    os.SortUnclamp();

                    //10. (編號10 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //11. (編號11 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                    os.LightForFrontBarDfsSetValue(0);
                    #endregion Iron Box with Mask
                    BoxType = 2;
                    //12. 重複1~11步驟, 再執行光罩水晶盒(無光罩) / 光罩水晶盒(有光罩)之情境
                    #region Crystal Box
                    //1.光罩水晶盒(無光罩)放置於Open Stage上方且上蓋已是開啟狀態
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

                    //2. Fiber Sensor確認光罩水晶盒上蓋已開啟 & 辨識光罩盒種類
                    if (os.ReadCoverSensor().Item1 == false)
                        throw new Exception("Box status was not opened");

                    //3. (編號10 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //4. (編號11 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //5. 開關盒機構可正常闔上光罩水晶盒上蓋
                    os.Close();
                    os.Unclamp();
                    os.Lock();

                    //6. 透過Box Robot PIN, 關閉光罩水晶盒鈕扣
                    if (BoxType == 1)
                    {
                        bt.RobotMoving(true);
                        os.ReadRobotIntrude(true, null);
                        bt.ExePathMove(pathFileObj.LockIronBoxPathFile());
                        os.ReadRobotIntrude(false, null);
                        bt.RobotMoving(false);
                    }
                    else if (BoxType == 2)
                    {
                        bt.RobotMoving(true);
                        os.ReadRobotIntrude(true, null);
                        bt.ExePathMove(pathFileObj.LockCrystalBoxPathFile());
                        os.ReadRobotIntrude(false, null);
                        bt.RobotMoving(false);
                    }

                    //7. Front CCD可以拍照取像(FOV正確)
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //8. Open Stage吸盤鬆開光罩水晶盒
                    os.Vacuum(false);

                    //9. Open Stage整定棒鬆開光罩水晶盒
                    os.SortUnclamp();

                    //10. (編號10 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //11. (編號11 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                    os.LightForFrontBarDfsSetValue(0);
                    #endregion Crystal Box
                    //12. 重複1~11步驟, 再執行光罩水晶盒(有光罩)之情境
                    #region Crystal Box with Mask
                    //1.光罩水晶盒(有光罩)放置於Open Stage上方且上蓋已是開啟狀態
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

                    //2. Fiber Sensor確認光罩水晶盒上蓋已開啟 & 辨識光罩盒種類
                    if (os.ReadCoverSensor().Item1 == false)
                        throw new Exception("Box status was not opened");

                    //3. (編號10 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //4. (編號11 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //5. 開關盒機構可正常闔上光罩水晶盒上蓋
                    os.Close();
                    os.Unclamp();
                    os.Lock();

                    //6. 透過Box Robot PIN, 關閉光罩水晶盒鈕扣
                    if (BoxType == 1)
                    {
                        bt.RobotMoving(true);
                        os.ReadRobotIntrude(true, null);
                        bt.ExePathMove(pathFileObj.LockIronBoxPathFile());
                        os.ReadRobotIntrude(false, null);
                        bt.RobotMoving(false);
                    }
                    else if (BoxType == 2)
                    {
                        bt.RobotMoving(true);
                        os.ReadRobotIntrude(true, null);
                        bt.ExePathMove(pathFileObj.LockCrystalBoxPathFile());
                        os.ReadRobotIntrude(false, null);
                        bt.RobotMoving(false);
                    }

                    //7. Front CCD可以拍照取像(FOV正確)
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //8. Open Stage吸盤鬆開光罩水晶盒
                    os.Vacuum(false);

                    //9. Open Stage整定棒鬆開光罩水晶盒
                    os.SortUnclamp();

                    //10. (編號10 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Left_CapToSave("D:/Image/OS/Left", "jpg");
                    os.LightForFrontBarDfsSetValue(0);

                    //11. (編號11 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForFrontBarDfsSetValue(6);
                    os.Camera_Right_CapToSave("D:/Image/OS/Right", "jpg");
                    os.LightForFrontBarDfsSetValue(0);
                    #endregion Crystal Box with Mask
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
