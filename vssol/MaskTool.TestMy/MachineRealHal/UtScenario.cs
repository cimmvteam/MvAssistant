using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

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

                    //os.Initial();
                    //os.SetBoxType(1);
                    //os.SortClamp();
                    //os.Vacuum(true);
                    //os.SortUnclamp();
                    //os.Lock();
                    //os.Close();
                    //os.Clamp();
                    //os.Open();
                    if (os.ReadRobotIntrude(false, true).Item2 == true)
                    {
                        mt.RobotMoving(true);
                        mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                        mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                        mt.Clamp(0);
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                    }
                    else
                        throw new Exception("Open Stage not allow Mask Transfer intrude.");
                    mt.RobotMoving(false);
                    os.ReadRobotIntrude(false, false);
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

                    //os.Initial();
                    //os.SetBoxType(1);
                    //os.SortClamp();
                    //os.Vacuum(true);
                    //os.SortUnclamp();
                    //os.Lock();
                    //os.Close();
                    //os.Clamp();
                    //os.Open();
                    if (os.ReadRobotIntrude(false, true).Item2 == true)
                    {
                        mt.RobotMoving(true);
                        mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                        mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                        mt.Unclamp();
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                    }
                    else
                        throw new Exception("Open Stage not allow Mask Transfer intrude.");
                    mt.RobotMoving(false);
                    os.ReadRobotIntrude(false, false);
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
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    bt.HalConnect();

                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet1_Home_Forward_OpenStage_PUT.json");
                    bt.Unclamp();
                    bt.ExePathMove(@"D:\Positions\BTRobot\OpenStage_Backward_Cabinet1_Home_PUT.json");
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
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    bt.HalConnect();

                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet1_Home_Forward_OpenStage_GET.json");
                    bt.Clamp(1);
                    bt.ExePathMove(@"D:\Positions\BTRobot\OpenStage_Backward_Cabinet1_Home_GET.json");
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
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    bt.HalConnect();

                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    bt.ExePathMove(@"D:\Positions\BTRobot\UnlockBox.json");
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
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    bt.HalConnect();

                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    bt.ExePathMove(@"D:\Positions\BTRobot\LockBox.json");
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
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_01_PUT.json");
                    bt.Unclamp();
                    bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_01_Backward_Cabinet_01_Home_PUT.json");
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
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_01_GET.json");
                    bt.Clamp(1);
                    bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_01_Backward_Cabinet_01_Home_GET.json");
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
        #endregion
    }
}
