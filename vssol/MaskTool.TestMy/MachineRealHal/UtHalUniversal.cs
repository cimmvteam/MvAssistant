using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;
using System.Threading;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalUniversal
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

                    mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                    mt.ChangeDirection(@"D:\Positions\MTRobot\CleanChHome.json");
                    mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        #region LP

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

                    os.Initial();
                    os.SetBoxType(1);
                    os.SortClamp();
                    os.Vacuum(true);
                    os.SortUnclamp();
                    os.Lock();
                    os.Close();
                    os.Clamp();
                    os.Open();
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
                    SpinWait.SpinUntil(() => (os.ReadBeenIntruded() == false));
                    os.Close();
                    os.Unclamp();
                    os.Lock();
                    os.Vacuum(false);
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

                    os.Initial();
                    os.SetBoxType(1);
                    os.SortClamp();
                    os.Vacuum(true);
                    os.SortUnclamp();
                    os.Lock();
                    os.Close();
                    os.Clamp();
                    os.Open();
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
                    SpinWait.SpinUntil(() => (os.ReadBeenIntruded() == false));
                    os.Close();
                    os.Unclamp();
                    os.Lock();
                    os.Vacuum(false);
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
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    bt.HalConnect();

                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet1_Home_Forward_OpenStage_PUT.json");
                    bt.ExePathMove(@"D:\Positions\BTRobot\OpenStage_Backward_Cabinet1_Home_PUT.json");
                    bt.ExePathMove(@"D:\Positions\BTRobot\UnlockBox.json");
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
                    bt.ExePathMove(@"D:\Positions\BTRobot\LockBox.json");
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet1_Home_Forward_OpenStage_GET.json");
                    bt.ExePathMove(@"D:\Positions\BTRobot\OpenStage_Backward_Cabinet1_Home_GET.json");
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
        [TestMethod]
        public void TestFlow()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;

                    unv.HalConnect();
                    os.HalConnect();
                    mt.HalConnect();

                    os.Initial();
                    os.SetBoxType(1);
                    os.SortClamp();
                    os.Vacuum(true);
                    os.SortUnclamp();
                    os.Lock();
                    if (os.ReadRobotIntrude(true, false).Item1 == true)
                        bt.RobotMoving(true);
                    else
                        throw new Exception("Open Stage not allow Box Transfer intrude.");
                    //BT開鎖
                    bt.RobotMoving(false);
                    SpinWait.SpinUntil(() => (os.ReadBeenIntruded() == false));
                    os.Close();
                    os.Clamp();
                    os.Open();
                    if (os.ReadRobotIntrude(false, true).Item2 == true)
                        mt.RobotMoving(true);
                    else
                        throw new Exception("Open Stage not allow Mask Transfer intrude.");
                    //MT取mask
                    mt.RobotMoving(false);
                    SpinWait.SpinUntil(() => (os.ReadBeenIntruded() == false));
                    os.Close();
                    os.Unclamp();
                    os.Lock();
                    if (os.ReadRobotIntrude(true, false).Item1 == true)
                        bt.RobotMoving(true);
                    else
                        throw new Exception("Open Stage not allow Box Transfer intrude.");
                    //BT開鎖
                    bt.RobotMoving(false);
                    SpinWait.SpinUntil(() => (os.ReadBeenIntruded() == false));
                    os.Vacuum(false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void TestSetParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;


            }
        }

        [TestMethod]
        public void TestReadParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;


            }
        }

        [TestMethod]
        public void TestReadComponentValue()
        {
            try
            {

                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;

                    if (unv.HalConnect() != 0)
                    {
                        System.Diagnostics.Debug.WriteLine("Connect Fail");
                    }

                    unv.ReadCoverFanSpeed();
                    unv.ReadPowerON();
                    unv.ReadBCP_Maintenance();
                    unv.ReadCB_Maintenance();
                    unv.ReadBCP_EMO();
                    unv.ReadCB_EMO();
                    unv.ReadLP1_EMO();
                    unv.ReadLP2_EMO();
                    unv.ReadBCP_Door();
                    unv.ReadLP1_Door();
                    unv.ReadLP2_Door();
                    unv.ReadBCP_Smoke();

                    unv.ReadAlarm_General();
                    unv.ReadAlarm_Cabinet();
                    unv.ReadAlarm_CleanCh();
                    unv.ReadAlarm_BTRobot();
                    unv.ReadAlarm_MTRobot();
                    unv.ReadAlarm_OpenStage();
                    unv.ReadAlarm_InspCh();
                    unv.ReadAlarm_LoadPort();
                    unv.ReadAlarm_CoverFan();
                    unv.ReadAlarm_MTClampInsp();

                    unv.ReadWarning_General();
                    unv.ReadWarning_Cabinet();
                    unv.ReadWarning_CleanCh();
                    unv.ReadWarning_BTRobot();
                    unv.ReadWarning_MTRobot();
                    unv.ReadWarning_OpenStage();
                    unv.ReadWarning_InspCh();
                    unv.ReadWarning_LoadPort();
                    unv.ReadWarning_CoverFan();
                    unv.ReadWarning_MTClampInsp();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [TestMethod]
        public void TestAssemblyWork()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;

                unv.ResetAllAlarm();
                unv.SetSignalTower(true, false, false);
                unv.SetBuzzer(1);
                unv.CoverFanCtrl(1, 150);
                unv.EMSAlarm(true, false, false, false);

            }
        }
    }
}
