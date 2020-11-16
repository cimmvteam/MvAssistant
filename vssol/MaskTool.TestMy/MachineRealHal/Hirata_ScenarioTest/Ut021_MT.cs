using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut021_MT
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
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
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
                    if (os.ReadCoverSensor().Item2 == false)
                        throw new Exception("Box status was not closed");
                    os.Close();
                    os.Clamp();
                    os.Open();
                    if (os.ReadCoverSensor().Item1 == false)
                        throw new Exception("Box status was not opened");

                    //2. (編號9 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForTopBarDfsSetValue(200);
                    os.Camera_Top_CapToSave("D:/Image/OS/Top", "jpg");
                    os.LightForTopBarDfsSetValue(0);

                    //3. (編號12 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForSideBarDfsSetValue(200);
                    os.Camera_Side_CapToSave("D:/Image/OS/Side", "jpg");
                    os.LightForSideBarDfsSetValue(0);

                    //4. Mask Robot從Home點移動至Open Stage
                    mt.RobotMoving(true);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    os.ReadRobotIntrude(false, true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\OSToOSStage.json");
                    mt.RobotMoving(false);

                    //5. Mask Robot上的傾斜角度傳感器可報值 & 位於水平狀態
                    var Level = mt.ReadLevel();

                    //6. Mask Robot在Open Stage進行光罩夾取(clamp光罩)
                    mt.Clamp(1);

                    //7. Mask Robot將光罩從Open Stage移動至Inspection Chamber Entry處
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\OSStageToOS.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                    os.ReadRobotIntrude(false, false);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    ic.ReadRobotIntrude(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeToICFrontSide.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICStage.json");
                    mt.RobotMoving(false);

                    //8. Mask Robot將光罩從Inspection Chamber Entry處, 移回Open Stage
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICStageToICFrontSide.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                    ic.ReadRobotIntrude(false);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    os.ReadRobotIntrude(false, true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\OSToOSStage.json");
                    mt.RobotMoving(false);

                    //9. Mask Robot將光罩放置於Open Stage上的光罩鐵盒內(release光罩)
                    mt.Unclamp();

                    //10. Mask Robot(無夾持光罩) 從Open Stage移回Home點
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\OSStageToOS.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                    mt.RobotMoving(false);

                    //11. (編號9 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForTopBarDfsSetValue(200);
                    os.Camera_Top_CapToSave("D:/Image/OS/Top", "jpg");
                    os.LightForTopBarDfsSetValue(0);

                    //12. (編號12 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForSideBarDfsSetValue(200);
                    os.Camera_Side_CapToSave("D:/Image/OS/Side", "jpg");
                    os.LightForSideBarDfsSetValue(0);


                    BoxType = 2;

                    //13. 重複1~12, 完成光罩水晶盒內的光罩clamp & release測試
                    //1. 光罩放置於Open Stage上的光罩鐵盒內
                    os.SetBoxType(BoxType);
                    os.SortClamp();
                    os.Vacuum(true);
                    os.SortUnclamp();
                    os.Lock();
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
                    if (os.ReadCoverSensor().Item2 == false)
                        throw new Exception("Box status was not closed");
                    os.Close();
                    os.Clamp();
                    os.Open();
                    if (os.ReadCoverSensor().Item1 == false)
                        throw new Exception("Box status was not opened");

                    //2. (編號9 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForTopBarDfsSetValue(200);
                    os.Camera_Top_CapToSave("D:/Image/OS/Top", "jpg");
                    os.LightForTopBarDfsSetValue(0);

                    //3. (編號12 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForSideBarDfsSetValue(200);
                    os.Camera_Side_CapToSave("D:/Image/OS/Side", "jpg");
                    os.LightForSideBarDfsSetValue(0);

                    //4. Mask Robot從Home點移動至Open Stage
                    mt.RobotMoving(true);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    os.ReadRobotIntrude(false, true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\OSToOSStage.json");
                    mt.RobotMoving(false);

                    //5. Mask Robot上的傾斜角度傳感器可報值 & 位於水平狀態
                    Level = mt.ReadLevel();

                    //6. Mask Robot在Open Stage進行光罩夾取(clamp光罩)
                    mt.Clamp(1);

                    //7. Mask Robot將光罩從Open Stage移動至Inspection Chamber Entry處
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\OSStageToOS.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                    os.ReadRobotIntrude(false, false);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    ic.ReadRobotIntrude(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeToICFrontSide.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICStage.json");
                    mt.RobotMoving(false);

                    //8. Mask Robot將光罩從Inspection Chamber Entry處, 移回Open Stage
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICStageToICFrontSide.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                    ic.ReadRobotIntrude(false);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    os.ReadRobotIntrude(false, true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\OSToOSStage.json");
                    mt.RobotMoving(false);

                    //9. Mask Robot將光罩放置於Open Stage上的光罩鐵盒內(release光罩)
                    mt.Unclamp();

                    //10. Mask Robot(無夾持光罩) 從Open Stage移回Home點
                    mt.RobotMoving(true);
                    mt.ExePathMove(@"D:\Positions\MTRobot\OSStageToOS.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                    mt.RobotMoving(false);

                    //11. (編號9 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForTopBarDfsSetValue(200);
                    os.Camera_Top_CapToSave("D:/Image/OS/Top", "jpg");
                    os.LightForTopBarDfsSetValue(0);

                    //12. (編號12 - CCD): 開啟光源->拍照(FOV正確)->關閉光源
                    os.LightForSideBarDfsSetValue(200);
                    os.Camera_Side_CapToSave("D:/Image/OS/Side", "jpg");
                    os.LightForSideBarDfsSetValue(0);
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
