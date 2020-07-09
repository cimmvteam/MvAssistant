using System;
using System.Threading;
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
using static MvAssistant.Mac.v1_0.Hal.CompDrawer.MacHalDrawerKjMachine;
using MvAssistant.Mac.v1_0.Hal.CompLoadPort;
using MvAssistant.DeviceDrive.GudengLoadPort.LoadPortEventArgs;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{

    [TestClass]
    public class UtScenario
    {
        #region Mask Robot Move
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
                    mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
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
                    mt.ExePathMove(@"D:\Positions\MTRobot\CCHomeFrontSideToClean.json");
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
                    //os.SetBoxType(1);
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
                    //os.SetBoxType(1);
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
                    var ttt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()];
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
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet1_Home_Forward_OpenStage_PUT.json");
                    bt.Unclamp();
                    bt.ExePathMove(@"D:\Positions\BTRobot\OpenStage_Backward_Cabinet1_Home_PUT.json");
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
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet1_Home_Forward_OpenStage_GET.json");
                    bt.Clamp(1);
                    bt.ExePathMove(@"D:\Positions\BTRobot\OpenStage_Backward_Cabinet1_Home_GET.json");
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
                    bt.ExePathMove(@"D:\Positions\BTRobot\UnlockBox.json");
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
                    bt.ExePathMove(@"D:\Positions\BTRobot\LockBox.json");
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
        public void TestRobotCB1HomePutBoxToCB_01_01()
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

                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_02_04_PUT.json");
                    bt.Unclamp();
                    bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_02_04_Backward_Cabinet_01_Home_PUT.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }
        [TestMethod]
        public void TestRobotCB1HomeGetBoxFromCB_01_01()
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

                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_02_04_GET.json");
                    bt.Clamp(1);
                    bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_02_04_Backward_Cabinet_01_Home_GET.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        #region Cabinet2
        [TestMethod]
        public void TestRobotCB2HomePutBoxToCB_04_01()
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
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_04_01_PUT.json");
                    bt.Unclamp();
                    bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_04_01_Backward_Cabinet_02_Home_PUT.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }
        [TestMethod]
        public void TestRobotCB2HomeGetBoxFromCB_04_01()
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
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_04_01_GET.json");
                    bt.Clamp(1);
                    bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_04_01_Backward_Cabinet_02_Home_GET.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        #region TransferPathFile
        [TestMethod]
        public void BoxTransferPathFile()
        {
            BoxrobotTransferPathFile fileObj = new BoxrobotTransferPathFile(@"D:\Positions\BTRobot\");
            var OpenStageToCabinet01Home_GET = fileObj.FromOpenStageToCabinet01Home_GET_PathFile();
            var OpenStageToCabinet01Home_PUT = fileObj.FromOpenStageToCabinet01Home_PUT_PathFile();
            var Cabinet01HomeToOpenStage_GET = fileObj.FromCabinet01HomeToOpenStage_GET_PathFile();
            var Cabinet01HomeToOpenStage_PUT = fileObj.FromCabinet01HomeToOpenStage_PUT_PathFile();
            var Cabinet01HomeToDrawer_GET = fileObj.FromCabinet01HomeToDrawer_GET_PathFile(BoxrobotTransferLocation.Drawer_01_01);
            var Cabinet01HomeToDrawer_PUT = fileObj.FromCabinet01HomeToDrawer_PUT_PathFile(BoxrobotTransferLocation.Drawer_01_01);

        }

        [TestMethod]
        public void MaskTransferPathFile()
        {
            MaskrobotTransferPathFile fileObj = new MaskrobotTransferPathFile(@"D:\Positions\MTRobot\");
            var loadPortHomePath = fileObj.LoadPortHomePathFile(); //V 10
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
            var ICHomeFrontSideToICPath = fileObj.FromICHomeFrontSideToICPathFile();//V //120
            var ICHomeBackSideToICPath = fileObj.FromICHomeBackSideToICPathFile();//V   //130
            var ICFrontSideToICHomePath = fileObj.FromICFrontSideToICHomePathFile();//V  //140
            var ICBackSideToICHomePath = fileObj.FromICBackSideToICHomePathFile();//V   150
            var CCHomeFrontSideToCCPath = fileObj.FromCCHomeFrontSideToCCPathFile();  //160
            var CCFrontSideToCCHomePath = fileObj.FromCCFrontSideToCCHomePathFile();  //170
            var CCFrontSideToCleanPath = fileObj.FromCCFrontSideToCleanPathFile();   //180
            var FrontSideCleanFinishToCCPath = fileObj.FromFrontSideCleanFinishToCCPathFile();  //190
            var CCFrontSideToCapturePath = fileObj.FromCCFrontSideToCapturePathFile();//     //200
            var FrontSideCaptureFinishToCCPath = fileObj.FromFrontSideCaptureFinishToCCPathFile();//210
            var CCHomeBackSideToCCPath = fileObj.FromCCHomeBackSideToCCPathFile();   //220
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

                    os.SetBoxType(1);
                    os.SortClamp();
                    os.Vacuum(true);
                    os.SortUnclamp();
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
                    bt.ExePathMove(@"D:\Positions\BTRobot\UnlockBox.json");
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

                    os.Close();
                    os.Unclamp();
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
                    bt.ExePathMove(@"D:\Positions\BTRobot\LockBox.json");
                    bt.RobotMoving(false);
                    for (int i = 0; i < 2; i++)
                    {
                        BTIntrude = os.ReadRobotIntrude(false, false).Item1;
                        if (i == 1 && BTIntrude == true || os.ReadBeenIntruded() == true)
                            throw new Exception("Open Stage has been BT intrude,can net execute command!!");
                    }
                    os.Vacuum(false);
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region Cabinet Action

        void Repeat()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(100);
            }
        }
        #region Drawer
        [TestMethod]
        public void DrawerTest()
        {

            try
            {

                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var cabinet = halContext.HalDevices[MacEnumDevice.cabinet_assembly.ToString()] as MacHalCabinet;
                    // HalBase hbaseDrawer;


                    cabinet.CreateDrawers<MacHalDrawerKjMachine>(cabinet.Hals);

                    //hbaseDrawer = cabinet.Hals[MacEnumDevice.cabinet_drawer_01_01.ToString()];
                    // MacHalDrawerKjMachine drawer = (MacHalDrawerKjMachine)hbaseDrawer;
                    //drawer.Index = MacEnumDevice.cabinet_drawer_01_01.ToString();
                  //  var drawer_01_01 = (MacHalDrawerKjMachine)cabinet.GetDrawer(MacEnumDevice.cabinet_drawer_01_01.ToString());
                   // var drawer_01_02 = (MacHalDrawerKjMachine)cabinet.GetDrawer(MacEnumDevice.cabinet_drawer_01_02.ToString());
                    //var drawer_01_03 = (MacHalDrawerKjMachine)cabinet.GetDrawer(MacEnumDevice.cabinet_drawer_01_03.ToString());
                    //var drawer_01_04 = (MacHalDrawerKjMachine)cabinet.GetDrawer(MacEnumDevice.cabinet_drawer_01_04.ToString());
                    var drawer_01_01 = halContext.HalDevices[MacEnumDevice.cabinet_drawer_01_02.ToString()] as MacHalDrawerKjMachine;
                    var drawer_01_02 = halContext.HalDevices[MacEnumDevice.cabinet_drawer_01_02.ToString()] as MacHalDrawerKjMachine;
                    var drawer_01_03 = halContext.HalDevices[MacEnumDevice.cabinet_drawer_01_02.ToString()] as MacHalDrawerKjMachine;
                    var drawer_01_04 = halContext.HalDevices[MacEnumDevice.cabinet_drawer_01_02.ToString()] as MacHalDrawerKjMachine;

                    if (cabinet.LddManager == null)
                    {
                        // manager = new MvKjMachineDrawerManager(Convert.ToInt32(drawer.DevSettings["startport"]), Convert.ToInt32(drawer.DevSettings["startport"]), Convert.ToInt32(drawer.DevSettings["local_port"]));
                        var cabinet_plc = cabinet.Hals[MacEnumDevice.cabinet_plc.ToString()];
                        var startPort = Convert.ToInt32(cabinet_plc.DevSettings["startport"]);
                        var endPort = Convert.ToInt32(cabinet_plc.DevSettings["endport"]);
                        var localPort = Convert.ToInt32(cabinet_plc.DevSettings["local_port"]);
                        cabinet.LddManager = new MvKjMachineDrawerManager(startPort, endPort, localPort);
                    }

                    var deviceEndPoint = new IPEndPoint(IPAddress.Parse(drawer_01_01.DevSettings["ip"]), Convert.ToInt32(drawer_01_01.DevSettings["port"]));
                    drawer_01_01.Ldd = cabinet.LddManager.CreateLdd(0, drawer_01_01.Index, deviceEndPoint, drawer_01_01.DevSettings["local_ip"]);
                    drawer_01_01.BindResult();

                    deviceEndPoint = new IPEndPoint(IPAddress.Parse(drawer_01_02.DevSettings["ip"]), Convert.ToInt32(drawer_01_02.DevSettings["port"]));
                    drawer_01_02.Ldd = cabinet.LddManager.CreateLdd(0, drawer_01_02.Index, deviceEndPoint, drawer_01_02.DevSettings["local_ip"]);
                    drawer_01_02.BindResult();

                    deviceEndPoint = new IPEndPoint(IPAddress.Parse(drawer_01_03.DevSettings["ip"]), Convert.ToInt32(drawer_01_03.DevSettings["port"]));
                    drawer_01_03.Ldd = cabinet.LddManager.CreateLdd(0, drawer_01_03.Index, deviceEndPoint, drawer_01_03.DevSettings["local_ip"]);
                    drawer_01_03.BindResult();

                    deviceEndPoint = new IPEndPoint(IPAddress.Parse(drawer_01_04.DevSettings["ip"]), Convert.ToInt32(drawer_01_04.DevSettings["port"]));
                    drawer_01_04.Ldd = cabinet.LddManager.CreateLdd(0, drawer_01_04.Index, deviceEndPoint, drawer_01_04.DevSettings["local_ip"]);
                    drawer_01_04.BindResult();



                    /**  Led Light
                    drawer1.Tag = BrightLEDType.AllOn;
                    drawer1.CommandBrightLEDAllOn();
                    drawer2.BindResult();
                    drawer2.Tag = BrightLEDType.AllOff;
                    drawer3.Tag = BrightLEDType.GreenOn;
                    drawer3.CommandBrightLEDGreenOn();

                    drawer1.Tag = BrightLEDType.RedOn;
                    drawer1.CommandBrightLEDRedOn();
                      */

                    /**  Motion Move  
                    drawer1.CommandTrayMotionHome();
                    drawer2.CommandTrayMotionIn();
                    drawer3.CommandTrayMotionOut();
                    drawer4.CommandTrayMotionIn();
                      */
                    /** INI
                 drawer1.CommandINI();
                  drawer2.CommandINI();
                 drawer3.CommandINI();
                drawer4.CommandINI();
            */

                    /**  set motion speed
                    drawer1.CommandSetMotionSpeed(100);
                    drawer2.CommandSetMotionSpeed(100);
                    drawer3.CommandSetMotionSpeed(100);
                    drawer4.CommandSetMotionSpeed(100);
                  */
                    /**  set TimeOut
                      drawer1.CommandSetTimeOut(100);
                      drawer2.CommandSetTimeOut(100);
                      drawer3.CommandSetTimeOut(100);
                      drawer4.CommandSetTimeOut(100);
                       */

                    /** CommandPositionRead()                    
                    drawer1.CommandPositionRead();
                    drawer2.CommandPositionRead();
                    drawer3.CommandPositionRead();
                    drawer4.CommandPositionRead();
                    */
                    /**              CommandBoxDetection*/
                    drawer_01_01.CommandBoxDetection();
                    drawer_01_01.OnBoxDetectionResultHandler += OnDetectDrawerBoxResult;
                    drawer2.CommandBoxDetection();
                    drawer3.CommandBoxDetection();
                    drawer4.CommandBoxDetection();
                    Repeat();
                }
            }
            catch (Exception ex)
            {

            }

        }
        void  OnDetectDrawerBoxResult(object sender, EventArgs e)
        {
            var drawer = (MacHalDrawerKjMachine)sender;
            var eventArgs = (HalDrawerBoxDetectReturn)e;

            if (eventArgs.HasBox.HasValue)
            {
                if((bool)eventArgs.HasBox.HasValue)
                {
                    Debug.WriteLine($"{nameof(OnDetectDrawerBoxResult)}: 有盒子" );
                }
                else
                {
                    Debug.WriteLine($"{nameof(OnDetectDrawerBoxResult)}: 没有盒子");
                }
            }
            else
            {
                Debug.WriteLine($"{nameof(OnDetectDrawerBoxResult)}: 無法判定");
            }
        }

        #endregion
        #endregion

        #region Load port 

        [TestMethod]
        public void TestLoadPort()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var cabinet = halContext.HalDevices[MacEnumDevice.loadport_assembly.ToString()] as MacHalLoadPort;
                    var loadport1 = cabinet.Hals[MacEnumDevice.loadport_1.ToString()] as MacHalGudengLoadPort;
                    var loadport2 = cabinet.Hals[MacEnumDevice.loadport_2.ToString()] as MacHalGudengLoadPort;
                    loadport1.HalConnect();
                    loadport2.HalConnect();
                    BindLoadPortEvent(loadport1);
                    BindLoadPortEvent(loadport2);
                    loadport1.CommandAlarmReset();
                    loadport2.CommandAlarmReset();

                   
                }
            }
            catch (Exception ex)
            {

            }
        }
        #region Loadport Event Handler
        void BindLoadPortEvent<T>(T loadport) where T:IMacHalLoadPortComp
        {
            loadport.OnPlacementHandler += this.OnLoadPortPlacementHandler;
            loadport.OnPresentHandler += OnLoadPortPresentHandler;
            loadport.OnClamperHandler+=   this.OnLoadPortClamperHandler;
            loadport.OnRFIDHandler += this.OnLoadPortRFIDHandler;
            loadport.OnBarcode_IDHandler += this.OnLoadPortBarcode_IDHandler;
            loadport.OnClamperLockCompleteHandler += this.OnLoadPortClamperLockCompleteHandler;
            loadport.OnVacuumCompleteHandler += this.OnLoadPortVacuumCompleteHandler;
            loadport.OnDockPODStartHandler += this.OnLoadPortDockPODStartHandler;
            loadport.OnDockPODComplete_HasReticleHandler += this.OnLoadPortDockPODComplete_HasReticleHandler;
            loadport.OnDockPODComplete_EmptyHandler += this.OnLoadPortDockPODComplete_EmptyHandler;
            loadport.OnUndockCompleteHandler += this.OnLoadPortUndockCompleteHandler;
            loadport.OnClamperUnlockCompleteHandler += this.OnLoadPortClamperUnlockCompleteHandler;
            loadport.OnAlarmResetSuccessHandler += this.OnLoadPortAlarmResetSuccessHandler;
            loadport.OnAlarmResetFailHandler += this.OnLoadPortAlarmResetFailHandler;
            loadport.OnExecuteInitialFirstHandler += this.OnLoadPortExecuteInitialFirstHandler;
            loadport.OnExecuteAlarmResetFirstHandler += this.OnLoadPortExecuteAlarmResetFirstHandler;
            loadport.OnStagePositionHandler += this.OnLoadPortStagePositionHandler;
            loadport.OnLoadportStatusHandler += this.OnLoadportStatusHandler;
            loadport.OnInitialCompleteHandler += this.OnLoadPortInitialCompleteHandler;
            loadport.OnInitialUnCompleteHandler += this.OnLoadPortInitialUnCompleteHandler;
            loadport.OnMustInAutoModeHandler += this.OnLoadPortMustInAutoModeHandler;
            loadport.OnMustInManualModeHandler += this.OnLoadPortMustInManualModeHandler;
            loadport.OnClamperNotLockHandler += this.OnLoadPortClamperNotLockHandler;
            loadport.OnPODNotPutProperlyHandler += this.OnLoadPortPODNotPutProperlyHandler;
            loadport.OnClamperActionTimeOutHandler += this.OnLoadPortClamperActionTimeOutHandler;
            loadport.OnClamperUnlockPositionFailedHandler += this.OnLoadPortClamperUnlockPositionFailedHandler;
            loadport.OnVacuumAbnormalityHandler += this.OnLoadPortVacuumAbnormalityHandler;
            loadport.OnStageMotionTimeoutHandler += this.OnLoadPortStageMotionTimeoutHandler;
            loadport.OnStageOverUpLimitationHandler += this.OnLoadPortStageOverUpLimitationHandler;
            loadport.OnStageOverDownLimitationHandler += this.OnLoadPortStageOverDownLimitationHandler;
            loadport.OnReticlePositionAbnormalityHandler += this.OnLoadPortReticlePositionAbnormalityHandler;
            loadport.OnClamperLockPositionFailedHandler += this.OnLoadPortClamperLockPositionFailed;
            loadport.OnPODPresentAbnormalityHandler += this.OnLoadPortPODPresentAbnormalityHandler;
            loadport.OnClamperMotorAbnormalityHandler += this.OnLoadPortClamperMotorAbnormality;
            loadport.OnStageMotorAbnormalityHandler += this.OnLoadPortStageMotorAbnormality;
        }


        void OnLoadPortPlacementHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
            var args = (OnPlacementEventArgs)e;
        }
        void OnLoadPortPresentHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
            var args = (OnPresentEventArgs)e;
        }
        void OnLoadPortClamperHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
            var args = (OnClamperEventArgs)e;
        }
        void OnLoadPortRFIDHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
            var args = (OnRFIDEventArgs)e;

        }
        void OnLoadPortBarcode_IDHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
            var args = (OnBarcode_IDEventArgs)e;
        }
        void OnLoadPortClamperLockCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
            var args = (OnClamperLockCompleteEventArgs)e;
        }
        void OnLoadPortVacuumCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
            var args = (OnVacuumCompleteEventArgs)e;
        }
        void OnLoadPortDockPODStartHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortDockPODComplete_HasReticleHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortDockPODComplete_EmptyHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortUndockCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortClamperUnlockCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortAlarmResetSuccessHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortAlarmResetFailHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortExecuteInitialFirstHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortExecuteAlarmResetFirstHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortStagePositionHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
            var args = (OnStagePositionEventArgs)e;
        }
        void OnLoadportStatusHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
            var args = (OnLoadportStatusEventArgs)e;
        }
        void OnLoadPortInitialCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortInitialUnCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortMustInAutoModeHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortMustInManualModeHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortClamperNotLockHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortPODNotPutProperlyHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortClamperActionTimeOutHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortClamperUnlockPositionFailedHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortVacuumAbnormalityHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortStageMotionTimeoutHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortStageOverUpLimitationHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortStageOverDownLimitationHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortReticlePositionAbnormalityHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortClamperLockPositionFailed(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortPODPresentAbnormalityHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortClamperMotorAbnormality(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortStageMotorAbnormality(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        #endregion
        #endregion
    }
}
