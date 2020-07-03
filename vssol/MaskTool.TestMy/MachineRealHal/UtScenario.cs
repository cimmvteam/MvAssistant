using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using System.Diagnostics;

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
            var Cabinet01HomeToDrawer_PUT= fileObj.FromCabinet01HomeToDrawer_PUT_PathFile(BoxrobotTransferLocation.Drawer_01_01);

        }
        [TestMethod]
        public void MaskTransferPathFile()
        {
            MaskrobotTransferPathFile fileObj = new MaskrobotTransferPathFile(@"D:\Positions\MTRobot\");
            var loadPortHomePath = fileObj.LoadPortHomePathFile();
            var InspChHomePath = fileObj.InspChHomePathFile();
            var cleanChHomePath = fileObj.CleanChHomePathFile();
            var LPHomeToLP1Path = fileObj.FromLPHomeToLP1PathFile();
            var LPHomeToLP2Path = fileObj.FromLPHomeToLP2PathFile();
            var LP1ToLPHomePath = fileObj.FromLP1ToLPHomePathFile();
            var LP2ToLPHomePath = fileObj.FromLP2ToLPHomePathFile();
            var LPHomeToOSPath = fileObj.FromLPHomeToOSPathFile();
            var OSToLPHomePath= fileObj.FromOSToLPHomePathFile();
            var ICHomeFrontSideToICPath = fileObj.FromICHomeFrontSideToICPathFile();
            var ICHomeBackSideToICPath = fileObj.FromICHomeBackSideToICPathFile();
            var ICFrontSideToICHomePath = fileObj.FromICFrontSideToICHomePathFile();
            var ICBackSideToICHomePath = fileObj.FromICBackSideToICHomePathFile();
            var CCHomeFrontSideToCleanPath = fileObj.FromCCHomeFrontSideToCleanPathFile();
            var CCHomeFrontSideToCameraPath = fileObj.FromCCHomeFrontSideToCameraPathFile();
            var CCHomeBackSideToCleanPath = fileObj.FromCCHomeBackSideToCleanPathFile();
            var CCHomeBackSideToCameraPath = fileObj.FromCCHomeBackSideToCameraPathFile();
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

                    ic.XYPosition(100,100);
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
    }
}
