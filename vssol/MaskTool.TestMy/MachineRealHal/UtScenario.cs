using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using System.Diagnostics;
using MvAssistant.Mac.v1_0.Hal.CompDrawer;
using MvAssistant.DeviceDrive.KjMachineDrawer;
using System.Net;
using MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment;
// vs 2013
//using static MvAssistant.Mac.v1_0.Hal.CompDrawer.MacHalDrawerKjMachine;
using MvAssistant.Mac.v1_0.Hal.CompLoadPort;
using MvAssistant.DeviceDrive.GudengLoadPort.LoadPortEventArgs;
using MvAssistant.Mac.v1_0.JSon;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{

    [TestClass]
    public class UtScenario
    {
        public uint BoxType = 2;//  1:鐵盒   2:水晶盒
        public string Column = "01";// 左至右位於第幾欄， 01 ~ 07
        public string Row = "05";// 上至下位於第幾列， 01 ~ 05

        #region Mask Robot Move
        #region Clamp test
        [TestMethod]
        public void TestRobotReliability()
        {
            int times = 0;
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    os.HalConnect();
                    ic.HalConnect();
                    bool MTIntrude = false;
                    if (false)
                    {
                        os.Initial();
                        mt.Initial();
                        ic.ReadRobotIntrude(false);
                        ic.Initial();

                        os.SetBoxType(2);
                        os.SortClamp();
                        Thread.Sleep(1000);
                        os.SortUnclamp();
                        os.SortClamp();
                        Thread.Sleep(1000);
                        os.Vacuum(true);
                        os.SortUnclamp();
                        os.Lock();
                        os.Close();
                        os.Clamp();
                        os.Open();
                    }
                    for (times = 1; times <= 10; times++)
                    {
                        //Get mask from Open Stage 
                        for (int i = 0; i < 2; i++)
                        {
                            MTIntrude = os.ReadRobotIntrude(false, true).Item2;
                            if (MTIntrude == true)
                                break;
                            else if (i == 1 && MTIntrude == false)
                                throw new Exception("Open Stage not allowed to be MT intrude!!");
                        }
                        mt.RobotMoving(true);
                        mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                        mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                        mt.Clamp(1);
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                        mt.RobotMoving(false);
                        for (int i = 0; i < 2; i++)
                        {
                            MTIntrude = os.ReadRobotIntrude(false, false).Item2;
                            if (i == 1 && MTIntrude == true || os.ReadBeenIntruded() == true)
                                throw new Exception("Open Stage has been MT intrude,can net execute command!!");
                        }

                        //Put glass side into Inspection Chamber
                        //ic.Initial();
                        ic.ReadRobotIntrude(false);
                        ic.XYPosition(0, 0);
                        ic.WPosition(0);
                        ic.ReadRobotIntrude(true);
                        mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                        mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                        mt.Unclamp();
                        mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                        ic.ReadRobotIntrude(false);
                        //Get glass side from Inspection Chamber
                        ic.ZPosition(-29.6);
                        for (int i = 158; i < 296; i += 23)
                        {
                            for (int j = 123; j < 261; j += 23)
                            {
                                ic.XYPosition(i, j);
                                ic.Camera_TopInsp_CapToSave("D:/Image/IC/TopInsp", "jpg");
                                Thread.Sleep(500);
                            }
                        }
                        ic.XYPosition(246, 208);
                        for (int i = 0; i < 360; i += 90)
                        {
                            ic.WPosition(i);
                            ic.Camera_SideInsp_CapToSave("D:/Image/IC/SideInsp", "jpg");
                            Thread.Sleep(500);
                        }

                        ic.XYPosition(0, 0);
                        ic.WPosition(0);
                        ic.ReadRobotIntrude(true);
                        mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                        mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                        mt.Clamp(1);
                        mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                        ic.ReadRobotIntrude(false);
                        
                        //Release mask to Open Stage
                        for (int i = 0; i < 2; i++)
                        {
                            MTIntrude = os.ReadRobotIntrude(false, true).Item2;
                            if (MTIntrude == true)
                                break;
                            else if (i == 1 && MTIntrude == false)
                                throw new Exception("Open Stage not allowed to be MT intrude!!");
                        }
                        mt.RobotMoving(true);
                        mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                        mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                        mt.Unclamp();
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                        mt.RobotMoving(false);
                        for (int i = 0; i < 2; i++)
                        {
                            MTIntrude = os.ReadRobotIntrude(false, false).Item2;
                            if (i == 1 && MTIntrude == true || os.ReadBeenIntruded() == true)
                                throw new Exception("Open Stage has been MT intrude,can net execute command!!");
                        }
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine("Projram was wrong when " + times + " times."); throw ex; }

        }
        #endregion Clamp test
        #region Change Direction
        [TestMethod]
        public void TestRobotCangeDirection()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();

                    //測試所有轉向路徑的邏輯
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    mt.ChangeDirection(@"D:\Positions\MTRobot\CleanChHome.json");
                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.ChangeDirection(@"D:\Positions\MTRobot\CleanChHome.json");
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        #region LP
        [TestMethod]
        public void TestRobotPutToLoadPort1()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();

                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                    mt.Unclamp();
                    mt.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestRobotGetFromLoadPort1()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();

                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                    mt.Clamp(0);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestRobotPutToLoadPort2()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();

                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
                    mt.Unclamp();
                    mt.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestRobotGetFromLoadPort2()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    ic.HalConnect();

                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP2.json");
                    mt.Clamp(0);
                    mt.ExePathMove(@"D:\Positions\MTRobot\LP2ToLPHome.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        #region IC
        [TestMethod]
        public void TestRobotPutFrontSideToInspCh()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    ic.HalConnect();

                    //ic.XYPosition(100,200);
                    //ic.WPosition(51);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeToICFrontSide.json");
                    mt.Unclamp();
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestRobotGetFrontSideFromInspCh()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    ic.HalConnect();

                    //ic.XYPosition(100,200);
                    //ic.WPosition(51);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                    mt.Clamp(0);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestRobotPutBackSideToInspCh()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    ic.HalConnect();

                    //ic.XYPosition(100,200);
                    //ic.WPosition(51);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeBackSideToIC.json");
                    mt.Unclamp();
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICBackSideToICHome.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestRobotGetBackSideFromInspCh()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    ic.HalConnect();

                    //ic.XYPosition(100,200);
                    //ic.WPosition(51);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeBackSideToIC.json");
                    mt.Clamp(0);
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICBackSideToICHome.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        #region CC
        [TestMethod]
        public void TestRobotFrontSideToClean()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();

                    mt.ChangeDirection(@"D:\Positions\MTRobot\CleanChHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\CCHomeToCCFrontSide.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestRobotBackSideToClean()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();

                    mt.ChangeDirection(@"D:\Positions\MTRobot\CleanChHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\CCHomeBackSideToClean.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestRobotFrontSideToCapture()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();

                    mt.ChangeDirection(@"D:\Positions\MTRobot\CleanChHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\CCHomeFrontSideToCamera.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestRobotBackSideToCapture()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();

                    mt.ChangeDirection(@"D:\Positions\MTRobot\CleanChHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\CCHomeBackSideToCamera.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        #region OS
        [TestMethod]
        public void TestRobotGetFromOpenStage()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    os.HalConnect();
                    bool MTIntrude = false;

                    //os.Initial();
                    //os.SetBoxType(2);
                    //os.SortClamp();
                    //os.Vacuum(true);
                    //os.SortUnclamp();
                    //os.Lock();
                    //os.Close();
                    //os.Clamp();
                    //os.Open();
                    for (int i = 0; i < 2; i++)
                    {
                        MTIntrude = os.ReadRobotIntrude(false, true).Item2;
                        if (MTIntrude == true)
                            break;
                        else if (i == 1 && MTIntrude == false)
                            throw new Exception("Open Stage not allowed to be MT intrude!!");
                    }
                    mt.RobotMoving(true);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                    mt.Clamp(0);
                    mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                    mt.RobotMoving(false);
                    for (int i = 0; i < 2; i++)
                    {
                        MTIntrude = os.ReadRobotIntrude(false, false).Item2;
                        if (i == 1 && MTIntrude == true || os.ReadBeenIntruded() == true)
                            throw new Exception("Open Stage has been MT intrude,can net execute command!!");
                    }
                    //SpinWait.SpinUntil(() => (os.ReadBeenIntruded() == false));
                    //os.Close();
                    //os.Unclamp();
                    //os.Lock();
                    //os.Vacuum(false);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestRobotPutToOpenStage()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    os.HalConnect();
                    bool MTIntrude = false;

                    //os.Initial();
                    //os.SetBoxType(2);
                    //os.SortClamp();
                    //os.Vacuum(true);
                    //os.SortUnclamp();
                    //os.Lock();
                    //os.Close();
                    //os.Clamp();
                    //os.Open();
                    for (int i = 0; i < 2; i++)
                    {
                        MTIntrude = os.ReadRobotIntrude(false, true).Item2;
                        if (MTIntrude == true)
                            break;
                        else if (i == 1 && MTIntrude == false)
                            throw new Exception("Open Stage not allowed to be MT intrude!!");
                    }
                    mt.RobotMoving(true);
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                    mt.Unclamp();
                    mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                    mt.RobotMoving(false);
                    for (int i = 0; i < 2; i++)
                    {
                        MTIntrude = os.ReadRobotIntrude(false, false).Item2;
                        if (i == 1 && MTIntrude == true || os.ReadBeenIntruded() == true)
                            throw new Exception("Open Stage has been MT intrude,can net execute command!!");
                    }
                    //SpinWait.SpinUntil(() => (os.ReadBeenIntruded() == false));
                    //os.Close();
                    //os.Unclamp();
                    //os.Lock();
                    //os.Vacuum(false);
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        #region Deformation Inspection
        [TestMethod]
        public void TestRobotICHomeToDeformInsp()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    ic.HalConnect();

                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeToDeformInsp.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestRobotDeformInspToICHome()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    ic.HalConnect();

                    mt.ExePathMove(@"D:\Positions\MTRobot\DeformInspToICHome.json");
                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        #endregion
        #region Box Robot Move
        #region OS
        [TestMethod]
        public void TestRobotCB1HomePutBoxToOS()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    bt.HalConnect();
                    os.HalConnect();
                    bool BTIntrude = false;

                    os.Initial();
                    for (int i = 0; i < 2; i++)
                    {
                        BTIntrude = os.ReadRobotIntrude(true, false).Item1;
                        if (BTIntrude == true)
                            break;
                        else if (i == 1 && BTIntrude == false)
                            throw new Exception("Open Stage not allowed to be BT intrude!!");
                        else
                            os.Initial();
                    }
                    bt.RobotMoving(true);
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_OpenStage_PUT.json");
                    if (bt.ReadLevelSensor().Item1 >= 1 || bt.ReadLevelSensor().Item1 <= -1 || bt.ReadLevelSensor().Item2 >= 1 || bt.ReadLevelSensor().Item2 <= -1)
                        throw new Exception("Box Transfer Level was out of range");
                    bt.Unclamp();
                    bt.ExePathMove(@"D:\Positions\BTRobot\OpenStage_Backward_Cabinet_01_Home_PUT.json");
                    bt.RobotMoving(false);
                    for (int i = 0; i < 2; i++)
                    {
                        BTIntrude = os.ReadRobotIntrude(false, false).Item1;
                        if (i == 1 && BTIntrude == true || os.ReadBeenIntruded() == true)
                            throw new Exception("Open Stage has been BT intrude,can net execute command!!");
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestRobotCB1HomeGetBoxFromOS()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    bt.HalConnect();
                    os.HalConnect();
                    bool BTIntrude = false;

                    for (int i = 0; i < 2; i++)
                    {
                        BTIntrude = os.ReadRobotIntrude(true, false).Item1;
                        if (BTIntrude == true)
                            break;
                        else if (i == 1 && BTIntrude == false)
                            throw new Exception("Open Stage not allowed to be BT intrude!!");
                        else
                            os.Initial();
                    }
                    bt.RobotMoving(true);
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_OpenStage_GET.json");
                    if (bt.ReadLevelSensor().Item1 >= 1 || bt.ReadLevelSensor().Item1 <= -1 || bt.ReadLevelSensor().Item2 >= 1 || bt.ReadLevelSensor().Item2 <= -1)
                        throw new Exception("Box Transfer Level was out of range");
                    bt.Clamp(BoxType);
                    bt.ExePathMove(@"D:\Positions\BTRobot\OpenStage_Backward_Cabinet_01_Home_GET.json");
                    bt.RobotMoving(false);
                    for (int i = 0; i < 2; i++)
                    {
                        BTIntrude = os.ReadRobotIntrude(false, false).Item1;
                        if (i == 1 && BTIntrude == true || os.ReadBeenIntruded() == true)
                            throw new Exception("Open Stage has been BT intrude,can net execute command!!");
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestRobotToOS_UnlockBox()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    bt.HalConnect();
                    os.HalConnect();
                    bool BTIntrude = false;

                    for (int i = 0; i < 2; i++)
                    {
                        BTIntrude = os.ReadRobotIntrude(true, false).Item1;
                        if (BTIntrude == true)
                            break;
                        else if (i == 1 && BTIntrude == false)
                            throw new Exception("Open Stage not allowed to be BT intrude!!");
                        else
                            os.Initial();
                    }
                    bt.RobotMoving(true);
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    if (BoxType == 1)
                        bt.ExePathMove(@"D:\Positions\BTRobot\UnlockIronBox.json");
                    else if (BoxType == 2)
                        bt.ExePathMove(@"D:\Positions\BTRobot\UnlockCrystalBox.json");
                    bt.RobotMoving(false);
                    for (int i = 0; i < 2; i++)
                    {
                        BTIntrude = os.ReadRobotIntrude(false, false).Item1;
                        if (i == 1 && BTIntrude == true || os.ReadBeenIntruded() == true)
                            throw new Exception("Open Stage has been BT intrude,can net execute command!!");
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestRobotToOS_LockBox()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    bt.HalConnect();
                    os.HalConnect();
                    bool BTIntrude = false;

                    for (int i = 0; i < 2; i++)
                    {
                        BTIntrude = os.ReadRobotIntrude(true, false).Item1;
                        if (BTIntrude == true)
                            break;
                        else if (i == 1 && BTIntrude == false)
                            throw new Exception("Open Stage not allowed to be BT intrude!!");
                        else
                            os.Initial();
                    }
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    if (BoxType == 1)
                        bt.ExePathMove(@"D:\Positions\BTRobot\LockIronBox.json");
                    else if (BoxType == 2)
                        bt.ExePathMove(@"D:\Positions\BTRobot\LockCrystalBox.json");
                    bt.RobotMoving(false);
                    for (int i = 0; i < 2; i++)
                    {
                        BTIntrude = os.ReadRobotIntrude(false, false).Item1;
                        if (i == 1 && BTIntrude == true || os.ReadBeenIntruded() == true)
                            throw new Exception("Open Stage has been BT intrude,can net execute command!!");
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        #region Cabinet1
        [TestMethod]
        public void TestRobotCB1HomePutBoxToCB1Drawer()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    bt.HalConnect();

                    //var cts = new CancellationTokenSource();
                    //var token = cts.Token;
                    ////var tasks=new ConcurrentBag<Task>();

                    //Action BTAction = () =>
                    //{
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_" + Column + "_" + Row + "_PUT.json");
                    if (bt.ReadLevelSensor().Item1 >= 1 || bt.ReadLevelSensor().Item1 <= -1 || bt.ReadLevelSensor().Item2 >= 1 || bt.ReadLevelSensor().Item2 <= -1)
                        throw new Exception("Box Transfer Level was out of range");
                    bt.Unclamp();
                    bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_" + Column + "_" + Row + "_Backward_Cabinet_01_Home_PUT.json");
                    //};
                    //Action PLCSignalAlarm = () =>
                    //{
                    //    while (true)
                    //    {
                    //        if (unv.ReadPowerON() == false) { cts.Cancel(); throw new Exception("Equipment is power off now !!"); }
                    //        if (unv.ReadBCP_Maintenance()) { cts.Cancel(); throw new Exception("Key lock in the electric control box is turn to maintenance"); }
                    //        if (unv.ReadCB_Maintenance()) { cts.Cancel(); throw new Exception("Outside key lock between cabinet_1 and cabinet_2 is turn to maintenance"); }
                    //        if (unv.ReadBCP_EMO().Item1) { cts.Cancel(); throw new Exception("EMO_1 has been trigger"); }
                    //        if (unv.ReadBCP_EMO().Item2) { cts.Cancel(); throw new Exception("EMO_2 has been trigger"); }
                    //        if (unv.ReadBCP_EMO().Item3) { cts.Cancel(); throw new Exception("EMO_3 has been trigger"); }
                    //        if (unv.ReadBCP_EMO().Item4) { cts.Cancel(); throw new Exception("EMO_4 has been trigger"); }
                    //        if (unv.ReadBCP_EMO().Item5) { cts.Cancel(); throw new Exception("EMO_5 has been trigger"); }
                    //        if (unv.ReadCB_EMO().Item1) { cts.Cancel(); throw new Exception("EMO_6 has been trigger"); }
                    //        if (unv.ReadCB_EMO().Item2) { cts.Cancel(); throw new Exception("EMO_7 has been trigger"); }
                    //        if (unv.ReadCB_EMO().Item3) { cts.Cancel(); throw new Exception("EMO_8 has been trigger"); }
                    //        if (unv.ReadLP1_EMO()) { cts.Cancel(); throw new Exception("Load Port_1 EMO has been trigger"); }
                    //        if (unv.ReadLP2_EMO()) { cts.Cancel(); throw new Exception("Load Port_2 EMO has been trigger"); }
                    //        if (unv.ReadBCP_Door()) { cts.Cancel(); throw new Exception("The door of electric control box has been open"); }
                    //        if (unv.ReadLP1_Door()) { cts.Cancel(); throw new Exception("The door of Load Port_1 has been open"); }
                    //        if (unv.ReadLP2_Door()) { cts.Cancel(); throw new Exception("The door of Load Pord_2 has been open"); }
                    //    }
                    //};
                    //Action AlarmAction = () =>
                    //{
                    //    string Result = "";
                    //    while (Result == "")
                    //    {
                    //        Result += unv.ReadAllAlarmMessage();

                    //        if (Result != "") { cts.Cancel(); throw new Exception(Result); }
                    //    }
                    //};
                    //Action WarningAction = () =>
                    //{
                    //    string Result = "";
                    //    while (Result == "")
                    //    {
                    //        Result += unv.ReadAllWarningMessage();

                    //        if (Result != "") { cts.Cancel(); throw new Exception(Result); }
                    //    }
                    //};

                    //Task PLCSignalAlarmTask = new Task(PLCSignalAlarm, token);
                    //Task AlarmTask = new Task(AlarmAction, token);
                    //Task WarningTask = new Task(WarningAction, token);
                    //Task BTTask = new Task(BTAction, token);

                    //PLCSignalAlarmTask.Start();
                    //AlarmTask.Start();
                    //WarningTask.Start();
                    //BTTask.Start();
                    //Task.WaitAll(BTTask);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestRobotCB1HomeGetBoxFromCB1Drawer()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    bt.HalConnect();

                    //var cts = new CancellationTokenSource();
                    //var token = cts.Token;
                    ////var tasks=new ConcurrentBag<Task>();

                    //Action BTAction = () =>
                    //{
                    bt.Initial();
                    bt.LevelReset();
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_" + Column + "_" + Row + "_GET.json");
                    if (bt.ReadLevelSensor().Item1 >= 1 || bt.ReadLevelSensor().Item1 <= -1 || bt.ReadLevelSensor().Item2 >= 1 || bt.ReadLevelSensor().Item2 <= -1)
                        throw new Exception("Box Transfer Level was out of range");
                    Console.WriteLine(bt.Clamp(BoxType));
                    bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_" + Column + "_" + Row + "_Backward_Cabinet_01_Home_GET.json");
                    //    };
                    //    Action PLCSignalAlarm = () =>
                    //    {
                    //        while (true)
                    //        {
                    //            if (unv.ReadPowerON() == false) { cts.Cancel(); throw new Exception("Equipment is power off now !!"); }
                    //            if (unv.ReadBCP_Maintenance()) { cts.Cancel(); throw new Exception("Key lock in the electric control box is turn to maintenance"); }
                    //            if (unv.ReadCB_Maintenance()) { cts.Cancel(); throw new Exception("Outside key lock between cabinet_1 and cabinet_2 is turn to maintenance"); }
                    //            if (unv.ReadBCP_EMO().Item1) { cts.Cancel(); throw new Exception("EMO_1 has been trigger"); }
                    //            if (unv.ReadBCP_EMO().Item2) { cts.Cancel(); throw new Exception("EMO_2 has been trigger"); }
                    //            if (unv.ReadBCP_EMO().Item3) { cts.Cancel(); throw new Exception("EMO_3 has been trigger"); }
                    //            if (unv.ReadBCP_EMO().Item4) { cts.Cancel(); throw new Exception("EMO_4 has been trigger"); }
                    //            if (unv.ReadBCP_EMO().Item5) { cts.Cancel(); throw new Exception("EMO_5 has been trigger"); }
                    //            if (unv.ReadCB_EMO().Item1) { cts.Cancel(); throw new Exception("EMO_6 has been trigger"); }
                    //            if (unv.ReadCB_EMO().Item2) { cts.Cancel(); throw new Exception("EMO_7 has been trigger"); }
                    //            if (unv.ReadCB_EMO().Item3) { cts.Cancel(); throw new Exception("EMO_8 has been trigger"); }
                    //            if (unv.ReadLP1_EMO()) { cts.Cancel(); throw new Exception("Load Port_1 EMO has been trigger"); }
                    //            if (unv.ReadLP2_EMO()) { cts.Cancel(); throw new Exception("Load Port_2 EMO has been trigger"); }
                    //            if (unv.ReadBCP_Door()) { cts.Cancel(); throw new Exception("The door of electric control box has been open"); }
                    //            if (unv.ReadLP1_Door()) { cts.Cancel(); throw new Exception("The door of Load Port_1 has been open"); }
                    //            if (unv.ReadLP2_Door()) { cts.Cancel(); throw new Exception("The door of Load Pord_2 has been open"); }
                    //        }
                    //    };
                    //    Action AlarmAction = () =>
                    //    {
                    //        string Result = "";
                    //        while (Result == "")
                    //        {
                    //            Result += unv.ReadAllAlarmMessage();

                    //            if (Result != "") { cts.Cancel(); throw new Exception(Result); }
                    //        }
                    //    };
                    //    Action WarningAction = () =>
                    //    {
                    //        string Result = "";
                    //        while (Result == "")
                    //        {
                    //            Result += unv.ReadAllWarningMessage();

                    //            if (Result != "") { cts.Cancel(); throw new Exception(Result); }
                    //        }
                    //    };

                    //    Task PLCSignalAlarmTask = new Task(PLCSignalAlarm, token);
                    //    Task AlarmTask = new Task(AlarmAction, token);
                    //    Task WarningTask = new Task(WarningAction, token);
                    //    Task BTTask = new Task(BTAction, token);

                    //    PLCSignalAlarmTask.Start();
                    //    AlarmTask.Start();
                    //    WarningTask.Start();
                    //    BTTask.Start();
                    //    Task.WaitAll(BTTask);
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        #region Cabinet2
        [TestMethod]
        public void TestRobotCB2HomePutBoxToCB2Drawer()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    bt.HalConnect();

                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_" + Column + "_" + Row + "_PUT.json");
                    if (bt.ReadLevelSensor().Item1 >= 1 || bt.ReadLevelSensor().Item1 <= -1 || bt.ReadLevelSensor().Item2 >= 1 || bt.ReadLevelSensor().Item2 <= -1)
                        throw new Exception("Box Transfer Level was out of range");
                    bt.Unclamp();
                    bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_" + Column + "_" + Row + "_Backward_Cabinet_02_Home_PUT.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }
        [TestMethod]
        public void TestRobotCB2HomeGetBoxFromCB2Drawer()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    bt.HalConnect();

                    bt.Initial();
                    bt.LevelReset();
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_" + Column + "_" + Row + "_GET.json");
                    if (bt.ReadLevelSensor().Item1 >= 1 || bt.ReadLevelSensor().Item1 <= -1 || bt.ReadLevelSensor().Item2 >= 1 || bt.ReadLevelSensor().Item2 <= -1)
                        throw new Exception("Box Transfer Level was out of range");
                    bt.Clamp(BoxType);
                    bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_" + Column + "_" + Row + "_Backward_Cabinet_02_Home_GET.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        #region TransferPathFile

        /// <summary>
        ///  預先載入BoxTransferRobot所有的路徑點位資料.
        /// </summary>
        [TestMethod]
        public void BoxTransferRobotPathPositionLoad()
        {
            PositionInstance.Load(); // 在這裏載入所有(Boxtransfer 及 Masktransfer)的路徑點位資料
            BoxrobotTransferPathFile boxRobotFileObj = new BoxrobotTransferPathFile(PositionInstance.BTR_Path);

            var OpenStageToCabinet01Home_GET = boxRobotFileObj.FromOpenStageToCabinet01Home_GET_PathFile();
            var OpenStageToCabinet01Home_PUT = boxRobotFileObj.FromOpenStageToCabinet01Home_PUT_PathFile();
        
            var position1 =JSonHelper.GetPositionPathPositionsFromJson(OpenStageToCabinet01Home_GET);
            var position2 = JSonHelper.GetPositionPathPositionsFromJson(OpenStageToCabinet01Home_PUT);
           
        }

        /// <summary>
        ///  預先載入MaskTransferRobot所有的路徑點位資料.
        ///  </summary>
        [TestMethod]
        public void MaskTransferRobotPathPositionLoad()
        {
            PositionInstance.Load(); // 在這裏載入所有(Boxtransfer 及 Masktransfer)的路徑點位資料
            MaskrobotTransferPathFile maskRobotFileObj = new MaskrobotTransferPathFile(PositionInstance.MTR_Path);

            var position01 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromBackSideCaptureFinishToCCPathFile()); 
            var position02 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromBackSideCleanFinishToCCPathFile());
            var position03 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromBarcodeReaderToLPHomePathFile());
            var position04 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromCCBackSideToCapturePathFile());
            var position05 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromCCBackSideToCCHomePathFile());
            var position06 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromCCBackSideToCleanPathFile());
            var position07 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromCCFrontSideToCapturePathFile());
            var position08 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromCCFrontSideToCCHomePathFile());
            var position09 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromCCFrontSideToCleanPathFile());
            var position10 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromCCHomeToCCBackSidePathFile());
            var position11 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromCCHomeToCCFrontSidePathFile());
            var position12 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromDeformInspTICHomeoPathFile());
            var position13 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromFrontSideCaptureFinishToCCPathFile());
            var position14 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromFrontSideCleanFinishToCCPathFile());
            var position15 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromICBackSideToICHomePathFile());
            var position16 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromICBackSideToICStagePathFile());
            var position17 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromICFrontSideToICHomePathFile());
            var position18 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromICFrontSideToICStagePathFile());
            var position19 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromICHomeToDeformInspPathFile());
            var position20 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromICHomeToICBackSidePathFile());
            var position21 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromICHomeToICFrontSidePathFile());
            var position22 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromICHomeToInspDeformPathFile());
            var position23 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromICStageToICBackSidePathFile());
            var position24 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromICStageToICFrontSidePathFile());
            var position25 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromInspDeformToICHomePathFile());
            var position26 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromLP1ToLPHomePathFile());
            var position27 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromLP2ToLPHomePathFile());
            var position28 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromLPHomeToBarcodeReaderPathFile());
            var position29 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromLPHomeToLP1PathFile());
            var position30 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromLPHomeToLP2PathFile());
            var position31 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromLPHomeToOSPathFile());
            var position32 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromOSStageToOSPathFile());
            var position33 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromOSToLPHomePathFile());
            var position34 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromOSToLPHomePathFile());
            var position35 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.FromOSToOSStagePathFile());
            var position36 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.InspChHomePathFile());
            var position37 = JSonHelper.GetPositionPathPositionsFromJson(maskRobotFileObj.LoadPortHomePathFile());
        }


        [TestMethod]
        public void BoxTransferPathFile()
        {
            BoxrobotTransferPathFile fileObj = new BoxrobotTransferPathFile(@"D:\Positions\BTRobot\");
            // OpenStage 到 Cabitnet01Home, Get 的路徑檔案
            var OpenStageToCabinet01Home_GET = fileObj.FromOpenStageToCabinet01Home_GET_PathFile();
            // OpenStage 到 Cabinet01Home, Put 的路徑檔案
            var OpenStageToCabinet01Home_PUT = fileObj.FromOpenStageToCabinet01Home_PUT_PathFile();
            // Cabinet01Home 到 OpenStage, Get
            var Cabinet01HomeToOpenStage_GET = fileObj.FromCabinet01HomeToOpenStage_GET_PathFile();
            // Cabitnet01Home 到 OpenStage, Put
            var Cabinet01HomeToOpenStage_PUT = fileObj.FromCabinet01HomeToOpenStage_PUT_PathFile();
            // Cabinet01Home  
            var Cabinet01Home = fileObj.Cabinet01HomePathFile();
            // Cabinet02Hime
            var Cabinet02Home = fileObj.Cabinet02HomePathFile();
            // 水晶盒 Lock 的路徑檔案
            var LockCrystalBox = fileObj.LockCrystalBoxPathFile();
            // 水晶盒 Unlock 的路徑檔案
            var UnLockCrystalBox = fileObj.UnlockCrystalBoxPathFile();
            // 鐵盒 Lock 的路徑檔案
            var LockIronBox = fileObj.LockIronBoxPathFile();
            // 鐵盒 Unlock 的路徑檔案
            var UnlockIronBox = fileObj.UnlockIronBoxPathFile();
            // 從Cabnet01Home 到指定 Drawer(Drawer_01_01) ,Get 的的路徑檔案
            var Cabinet01HomeToDrawer_GET = fileObj.FromCabinet01HomeToDrawer_GET_PathFile(BoxrobotTransferLocation.Drawer_01_01);
            // 從Cabnet01Home 到指定 Drawer(Drawer_01_01) ,Put 的的路徑檔案
            var Cabinet01HomeToDrawer_PUT = fileObj.FromCabinet01HomeToDrawer_PUT_PathFile(BoxrobotTransferLocation.Drawer_01_01);
            // 從Cabnet02Home 到指定 Drawer(Drawer_05_01) ,Get 的的路徑檔案
            var Cabinet02HomeToDrawer_GET = fileObj.FromCabinet02HomeToDrawer_GET_PathFile(BoxrobotTransferLocation.Drawer_05_01);
            // 從Cabnet02Home 到指定 Drawer(Drawer_05_01) ,Put 的的路徑檔案
            var Cabinet02HomeToDrawer_PUT = fileObj.FromCabinet02HomeToDrawer_PUT_PathFile(BoxrobotTransferLocation.Drawer_05_01);
            // 從指定 Drawer(Drawer_02_03) 到 Cabinet01Home, Get  的的路徑檔案
            var DrawerToCabinet01Home_GET = fileObj.FromDrawerToCabinet01Home_GET_PathFile(BoxrobotTransferLocation.Drawer_02_03);
            // 從指定 Drawer(Drawer_02_03) 到 Cabinet01Home,  Put  的的路徑檔案
            var DrawerToCabinet01Home_PUT = fileObj.FromDrawerToCabinet01Home_PUT_PathFile(BoxrobotTransferLocation.Drawer_02_03);
            // 從指定 Drawer(Drawer_06_02) 到 Cabinet02Home, Get  的的路徑檔案
            var DrawerToCabinet02Home_GET = fileObj.FromDrawerToCabinet02Home_GET_PathFile(BoxrobotTransferLocation.Drawer_06_02);
            // 從指定 Drawer(Drawer_06_02) 到 Cabinet02Home, Put  的的路徑檔案
            var DrawerToCabinet02Home_PUT = fileObj.FromDrawerToCabinet02Home_PUT_PathFile(BoxrobotTransferLocation.Drawer_06_02);
        }

        [TestMethod]
        public void MaskTransferPathFile()
        {
            MaskrobotTransferPathFile fileObj = new MaskrobotTransferPathFile(@"D:\Positions\MTRobot\");
            var LoadPortHomePath = fileObj.LoadPortHomePathFile(); //V 10
            var InspChHomePath = fileObj.InspChHomePathFile();//V  20
            var cleanChHomePath = fileObj.CleanChHomePathFile();//V 30
            var LPHomeToLP1Path = fileObj.FromLPHomeToLP1PathFile();//V 40
            var LPHomeToLP2Path = fileObj.FromLPHomeToLP2PathFile();//V  50
            var LP1ToLPHomePath = fileObj.FromLP1ToLPHomePathFile();//V  60
            var LP2ToLPHomePath = fileObj.FromLP2ToLPHomePathFile();//V  70
            var LPHomeToOSPath = fileObj.FromLPHomeToOSPathFile();//V   80
            var OSToLPHomePath = fileObj.FromOSToLPHomePathFile();//V  90
            var ICHomeToDeformInspPath = fileObj.FromICHomeToDeformInspPathFile();//100
            var DeformInspToICHomePath = fileObj.FromDeformInspTICHomeoPathFile();//110
            var ICHomeFrontSideToICPath = fileObj.FromICHomeToICFrontSidePathFile();//V //120
            var ICHomeBackSideToICPath = fileObj.FromICHomeToICBackSidePathFile();//V   //130
            var ICFrontSideToICHomePath = fileObj.FromICFrontSideToICHomePathFile();//V  //140
            var ICBackSideToICHomePath = fileObj.FromICBackSideToICHomePathFile();//V   150
            var CCHomeFrontSideToCCPath = fileObj.FromCCHomeToCCFrontSidePathFile();  //160
            var CCFrontSideToCCHomePath = fileObj.FromCCFrontSideToCCHomePathFile();  //170
            var CCFrontSideToCleanPath = fileObj.FromCCFrontSideToCleanPathFile();   //180
            var FrontSideCleanFinishToCCPath = fileObj.FromFrontSideCleanFinishToCCPathFile();  //190
            var CCFrontSideToCapturePath = fileObj.FromCCFrontSideToCapturePathFile();//     //200
            var FrontSideCaptureFinishToCCPath = fileObj.FromFrontSideCaptureFinishToCCPathFile();//210
            var CCHomeBackSideToCCPath = fileObj.FromCCHomeToCCBackSidePathFile();   //220
            var CCBackSideToCCHomePath = fileObj.FromCCBackSideToCCHomePathFile();  //230
            var CCBackSideToCleanPath = fileObj.FromCCBackSideToCleanPathFile();     //240
            var BackSideCleanFinishToCCPath = fileObj.FromBackSideCleanFinishToCCPathFile(); //250
            var CCBackSideToCapturePath = fileObj.FromCCBackSideToCapturePathFile();      //260
            var BackSideCaptureFinishToCCPath = fileObj.FromBackSideCaptureFinishToCCPathFile();  //270

        }

        #endregion

        #endregion
        #region CC Action
        [TestMethod]
        public void TestCleanChPurge()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    cc.HalConnect();

                    cc.SetPressureCtrl(100);
                    cc.GasValveBlow(30);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestCleanChCaptureImage()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    cc.HalConnect();

                    cc.LightSetValue(200);
                    cc.Camera_SideInsp_CapToSave("D:/Image/CC", "jpg");
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        #region IC Action
        [TestMethod]
        public void TestInspChTopInspImage()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    ic.HalConnect();

                    ic.XYPosition(100, 100);
                    ic.WPosition(51);
                    ic.LightForSideInspSetValue(100);
                    //TODO：Camera Link Capture Image
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestInspChTopDfsImage()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    ic.HalConnect();

                    ic.XYPosition(100, 100);
                    ic.WPosition(51);
                    ic.LightForSideDfsSetValue(100);
                    ic.Camera_TopDfs_CapToSave("D:/Image/IC/TopDfs", "jpg");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestInspChSideInspImage()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    ic.HalConnect();

                    ic.XYPosition(100, 100);
                    ic.WPosition(51);
                    ic.LightForSideDfsSetValue(500);
                    ic.Camera_TopDfs_CapToSave("D:/Image/IC/SideInsp", "jpg");
                    ic.WPosition(141);
                    ic.Camera_TopDfs_CapToSave("D:/Image/IC/SideInsp", "jpg");
                    ic.WPosition(231);
                    ic.Camera_TopDfs_CapToSave("D:/Image/IC/SideInsp", "jpg");
                    ic.WPosition(321);
                    ic.Camera_TopDfs_CapToSave("D:/Image/IC/SideInsp", "jpg");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestInspChSideDfsImage()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    ic.HalConnect();

                    ic.XYPosition(100, 100);
                    ic.WPosition(51);
                    ic.LightForSideDfsSetValue(500);
                    ic.Camera_TopDfs_CapToSave("D:/Image/IC/SideDfs", "jpg");
                    ic.WPosition(141);
                    ic.Camera_TopDfs_CapToSave("D:/Image/IC/SideDfs", "jpg");
                    ic.WPosition(231);
                    ic.Camera_TopDfs_CapToSave("D:/Image/IC/SideDfs", "jpg");
                    ic.WPosition(321);
                    ic.Camera_TopDfs_CapToSave("D:/Image/IC/SideDfs", "jpg");
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        #region OS Action
        [TestMethod]
        public void TestOpenStageUnlockAndOpenBox()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    bt.HalConnect();
                    os.HalConnect();
                    bool BTIntrude = false;

                    //var cts = new CancellationTokenSource();
                    //var token = cts.Token;
                    ////var tasks=new ConcurrentBag<Task>();

                    //Action OSAndBTAction = () =>
                    //{
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
                    for (int i = 0; i < 2; i++)
                    {
                        BTIntrude = os.ReadRobotIntrude(true, false).Item1;
                        if (BTIntrude == true)
                            break;
                        else if (i == 1 && BTIntrude == false)
                            throw new Exception("Open Stage not allowed to be BT intrude!!");
                        else
                            os.Initial();
                    }
                    bt.RobotMoving(true);
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    if (BoxType == 1)
                        bt.ExePathMove(@"D:\Positions\BTRobot\UnlockIronBox.json");
                    else if (BoxType == 2)
                        bt.ExePathMove(@"D:\Positions\BTRobot\UnlockCrystalBox.json");
                    bt.RobotMoving(false);
                    for (int i = 0; i < 2; i++)
                    {
                        BTIntrude = os.ReadRobotIntrude(false, false).Item1;
                        if (i == 1 && BTIntrude == true || os.ReadBeenIntruded() == true)
                            throw new Exception("Open Stage has been BT intrude,can net execute command!!");
                    }
                    os.Close();
                    os.Clamp();
                    os.Open();
                    if (os.ReadCoverSensor().Item1 == false)
                        throw new Exception("Box status was not opened");
                    //};
                    //Action PLCSignalAlarm = () =>
                    //{
                    //    while (true)
                    //    {
                    //        if (unv.ReadPowerON() == false) { cts.Cancel(); throw new Exception("Equipment is power off now !!"); }
                    //        if (unv.ReadBCP_Maintenance()) { cts.Cancel(); throw new Exception("Key lock in the electric control box is turn to maintenance"); }
                    //        if (unv.ReadCB_Maintenance()) { cts.Cancel(); throw new Exception("Outside key lock between cabinet_1 and cabinet_2 is turn to maintenance"); }
                    //        if (unv.ReadBCP_EMO().Item1) { cts.Cancel(); throw new Exception("EMO_1 has been trigger"); }
                    //        if (unv.ReadBCP_EMO().Item2) { cts.Cancel(); throw new Exception("EMO_2 has been trigger"); }
                    //        if (unv.ReadBCP_EMO().Item3) { cts.Cancel(); throw new Exception("EMO_3 has been trigger"); }
                    //        if (unv.ReadBCP_EMO().Item4) { cts.Cancel(); throw new Exception("EMO_4 has been trigger"); }
                    //        if (unv.ReadBCP_EMO().Item5) { cts.Cancel(); throw new Exception("EMO_5 has been trigger"); }
                    //        if (unv.ReadCB_EMO().Item1) { cts.Cancel(); throw new Exception("EMO_6 has been trigger"); }
                    //        if (unv.ReadCB_EMO().Item2) { cts.Cancel(); throw new Exception("EMO_7 has been trigger"); }
                    //        if (unv.ReadCB_EMO().Item3) { cts.Cancel(); throw new Exception("EMO_8 has been trigger"); }
                    //        if (unv.ReadLP1_EMO()) { cts.Cancel(); throw new Exception("Load Port_1 EMO has been trigger"); }
                    //        if (unv.ReadLP2_EMO()) { cts.Cancel(); throw new Exception("Load Port_2 EMO has been trigger"); }
                    //        if (unv.ReadBCP_Door()) { cts.Cancel(); throw new Exception("The door of electric control box has been open"); }
                    //        if (unv.ReadLP1_Door()) { cts.Cancel(); throw new Exception("The door of Load Port_1 has been open"); }
                    //        if (unv.ReadLP2_Door()) { cts.Cancel(); throw new Exception("The door of Load Pord_2 has been open"); }
                    //    }
                    //};
                    //Action AlarmAction = () =>
                    //{
                    //    string Result = "";
                    //    while (Result == "")
                    //    {
                    //        Result += unv.ReadAllAlarmMessage();

                    //        if (Result != "") { cts.Cancel(); throw new Exception(Result); }
                    //    }
                    //};
                    //Action WarningAction = () =>
                    //{
                    //    string Result = "";
                    //    while (Result == "")
                    //    {
                    //        Result += unv.ReadAllWarningMessage();

                    //        if (Result != "") { cts.Cancel(); throw new Exception(Result); }
                    //    }
                    //};

                    //Task PLCSignalAlarmTask = new Task(PLCSignalAlarm, token);
                    //Task AlarmTask = new Task(AlarmAction, token);
                    //Task WarningTask = new Task(WarningAction, token);
                    //Task OSAndBTTask = new Task(OSAndBTAction, token);

                    //PLCSignalAlarmTask.Start();
                    //AlarmTask.Start();
                    //WarningTask.Start();
                    //OSAndBTTask.Start();
                    //Task.WaitAll(OSAndBTTask);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestOpenStageCloseAndLockBox()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    bt.HalConnect();
                    os.HalConnect();
                    bool BTIntrude = false;
                    //var cts = new CancellationTokenSource();
                    //var token = cts.Token;


                    //Action OSAndBTAction = () =>
                    //{
                    if (os.ReadCoverSensor().Item1 == false)
                        throw new Exception("Box status was not opened");
                    os.Close();
                    os.Unclamp();
                    if (os.ReadCoverSensor().Item2 == false)
                        throw new Exception("Box status was not closed");
                    os.Lock();
                    for (int i = 0; i < 2; i++)
                    {
                        BTIntrude = os.ReadRobotIntrude(true, false).Item1;
                        if (BTIntrude == true)
                            break;
                        else if (i == 1 && BTIntrude == false)
                            throw new Exception("Open Stage not allowed to be BT intrude!!");
                        else
                            os.Initial();
                    }
                    bt.RobotMoving(true);
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    if (BoxType == 1)
                        bt.ExePathMove(@"D:\Positions\BTRobot\LockIronBox.json");
                    else if (BoxType == 2)
                        bt.ExePathMove(@"D:\Positions\BTRobot\LockCrystalBox.json");
                    bt.RobotMoving(false);
                    for (int i = 0; i < 2; i++)
                    {
                        BTIntrude = os.ReadRobotIntrude(false, false).Item1;
                        if (i == 1 && BTIntrude == true || os.ReadBeenIntruded() == true)
                            throw new Exception("Open Stage has been BT intrude,can net execute command!!");
                    }
                    os.Vacuum(false);
                    //};
                    //Action PLCSignalAlarm = () =>
                    //{
                    //    while (true)
                    //    {
                    //        if (unv.ReadPowerON() == false) { cts.Cancel(); throw new Exception("Equipment is power off now !!"); }
                    //        if (unv.ReadBCP_Maintenance()) { cts.Cancel(); throw new Exception("Key lock in the electric control box is turn to maintenance"); }
                    //        if (unv.ReadCB_Maintenance()) { cts.Cancel(); throw new Exception("Outside key lock between cabinet_1 and cabinet_2 is turn to maintenance"); }
                    //        if (unv.ReadBCP_EMO().Item1) { cts.Cancel(); throw new Exception("EMO_1 has been trigger"); }
                    //        if (unv.ReadBCP_EMO().Item2) { cts.Cancel(); throw new Exception("EMO_2 has been trigger"); }
                    //        if (unv.ReadBCP_EMO().Item3) { cts.Cancel(); throw new Exception("EMO_3 has been trigger"); }
                    //        if (unv.ReadBCP_EMO().Item4) { cts.Cancel(); throw new Exception("EMO_4 has been trigger"); }
                    //        if (unv.ReadBCP_EMO().Item5) { cts.Cancel(); throw new Exception("EMO_5 has been trigger"); }
                    //        if (unv.ReadCB_EMO().Item1) { cts.Cancel(); throw new Exception("EMO_6 has been trigger"); }
                    //        if (unv.ReadCB_EMO().Item2) { cts.Cancel(); throw new Exception("EMO_7 has been trigger"); }
                    //        if (unv.ReadCB_EMO().Item3) { cts.Cancel(); throw new Exception("EMO_8 has been trigger"); }
                    //        if (unv.ReadLP1_EMO()) { cts.Cancel(); throw new Exception("Load Port_1 EMO has been trigger"); }
                    //        if (unv.ReadLP2_EMO()) { cts.Cancel(); throw new Exception("Load Port_2 EMO has been trigger"); }
                    //        if (unv.ReadBCP_Door()) { cts.Cancel(); throw new Exception("The door of electric control box has been open"); }
                    //        if (unv.ReadLP1_Door()) { cts.Cancel(); throw new Exception("The door of Load Port_1 has been open"); }
                    //        if (unv.ReadLP2_Door()) { cts.Cancel(); throw new Exception("The door of Load Pord_2 has been open"); }
                    //    }
                    //};
                    //Action AlarmAction = () =>
                    //{
                    //    string Result = "";
                    //    while (Result == "")
                    //    {
                    //        Result += unv.ReadAllAlarmMessage();

                    //        if (Result != "") { cts.Cancel(); throw new Exception(Result); }
                    //    }
                    //};
                    //Action WarningAction = () =>
                    //{
                    //    string Result = "";
                    //    while (Result == "")
                    //    {
                    //        Result += unv.ReadAllWarningMessage();

                    //        if (Result != "") { cts.Cancel(); throw new Exception(Result); }
                    //    }
                    //};

                    //Task PLCSignalAlarmTask = new Task(PLCSignalAlarm, token);
                    //Task AlarmTask = new Task(AlarmAction, token);
                    //Task WarningTask = new Task(WarningAction, token);
                    //Task OSAndBTTask = new Task(OSAndBTAction, token);

                    //PLCSignalAlarmTask.Start();
                    //AlarmTask.Start();
                    //WarningTask.Start();
                    //OSAndBTTask.Start();
                    //Task.WaitAll(OSAndBTTask);
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region PLC SignalDetect
        [TestMethod]
        public void TestPLCSignalDetect()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    Console.WriteLine(unv.ReadPowerON());
                    var cts = new CancellationTokenSource();
                    var token = cts.Token;
                    //var tasks=new ConcurrentBag<Task>();

                    Action<MacHalUniversal> PLCSignalAlarm = (unv1) =>
                    {
                        while (true)
                        {
                            if (unv1.ReadPowerON() == false) { cts.Cancel(); throw new Exception("Equipment is power off now !!"); }
                            if (unv1.ReadBCP_Maintenance()) { cts.Cancel(); throw new Exception("Key lock in the electric control box is turn to maintenance"); }
                            if (unv1.ReadCB_Maintenance()) { cts.Cancel(); throw new Exception("Outside key lock between cabinet_1 and cabinet_2 is turn to maintenance"); }
                            if (unv1.ReadBCP_EMO().Item1) { cts.Cancel(); throw new Exception("EMO_1 has been trigger"); }
                            if (unv1.ReadBCP_EMO().Item2) { cts.Cancel(); throw new Exception("EMO_2 has been trigger"); }
                            if (unv1.ReadBCP_EMO().Item3) { cts.Cancel(); throw new Exception("EMO_3 has been trigger"); }
                            if (unv1.ReadBCP_EMO().Item4) { cts.Cancel(); throw new Exception("EMO_4 has been trigger"); }
                            if (unv1.ReadBCP_EMO().Item5) { cts.Cancel(); throw new Exception("EMO_5 has been trigger"); }
                            if (unv1.ReadCB_EMO().Item1) { cts.Cancel(); throw new Exception("EMO_6 has been trigger"); }
                            if (unv1.ReadCB_EMO().Item2) { cts.Cancel(); throw new Exception("EMO_7 has been trigger"); }
                            if (unv1.ReadCB_EMO().Item3) { cts.Cancel(); throw new Exception("EMO_8 has been trigger"); }
                            if (unv1.ReadLP1_EMO()) { cts.Cancel(); throw new Exception("Load Port_1 EMO has been trigger"); }
                            if (unv1.ReadLP2_EMO()) { cts.Cancel(); throw new Exception("Load Port_2 EMO has been trigger"); }
                            if (unv1.ReadBCP_Door()) { cts.Cancel(); throw new Exception("The door of electric control box has been open"); }
                            if (unv1.ReadLP1_Door()) { cts.Cancel(); throw new Exception("The door of Load Port_1 has been open"); }
                            if (unv1.ReadLP2_Door()) { cts.Cancel(); throw new Exception("The door of Load Pord_2 has been open"); }
                        }
                    };
                    Action AlarmAction = () =>
                    {
                        string Result = "";
                        while (Result == "")
                        {
                            Result += unv.ReadAllAlarmMessage();

                            if (Result != "") { cts.Cancel(); throw new Exception(Result); }
                        }
                    };
                    Action WarningAction = () =>
                    {
                        string Result = "";
                        while (Result == "")
                        {
                            Result += unv.ReadAllWarningMessage();

                            if (Result != "") { cts.Cancel(); throw new Exception(Result); }
                        }
                    };
                    //Task PLCSignalAlarmTask = new Task(PLCSignalAlarm,unv,token);
                    Task AlarmTask = new Task(AlarmAction, token);
                    Task WarningTask = new Task(WarningAction, token);

                    //PLCSignalAlarmTask.Start();
                    AlarmTask.Start();
                    WarningTask.Start();
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

    }
}
