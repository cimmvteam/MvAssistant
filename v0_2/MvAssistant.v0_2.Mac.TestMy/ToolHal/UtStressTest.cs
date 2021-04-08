using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_2.Mac.Hal;
using MvAssistant.v0_2.Mac.Hal.Assembly;
using MvAssistant.v0_2.Mac.Manifest;

namespace MvAssistant.v0_2.Mac.TestMy.ToolHal
{
    [TestClass]
    public class UtStressTest
    {
        [TestMethod]
        public void TestRobotBankIn()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvaCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    var ic = halContext.HalDevices[MacEnumDevice.inspectionch_assembly.ToString()] as MacHalInspectionCh;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    ic.HalConnect();
                    os.HalConnect();

                    bool BTIntrude = false;
                    bool MTIntrude = false;
                    ThreadStart GetAndOpenBoxThread = () =>
                    {
                        try
                        {
                            //TODO：Cabinet控制推出Box
                            bt.RobotMoving(true);
                            bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                            bt.Unclamp();
                            bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_01_GET.json");
                            bt.Clamp(1);
                            bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_01_Backward_Cabinet_01_Home_GET.json");
                            bt.RobotMoving(false);
                            for (int i = 0; i < 2; i++)
                            {
                                BTIntrude = os.SetRobotIntrude(true, false).Item1;
                                if (BTIntrude == true)
                                    break;
                                else if (i == 1 && BTIntrude == false)
                                    throw new Exception("Open Stage not allowed to be BT intrude!!");
                                else
                                    os.Initial();
                            }
                            bt.RobotMoving(true);
                            bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet1_Home_Forward_OpenStage_PUT.json");
                            bt.Unclamp();
                            bt.ExePathMove(@"D:\Positions\BTRobot\OpenStage_Backward_Cabinet1_Home_PUT.json");
                            bt.RobotMoving(false);
                            for (int i = 0; i < 2; i++)
                            {
                                BTIntrude = os.SetRobotIntrude(false, false).Item1;
                                if (i == 1 && BTIntrude == true || os.ReadRobotIntruded().Item1 == true || os.ReadRobotIntruded().Item2 == true)
                                    throw new Exception("Open Stage has been BT intrude,can net execute command!!");
                            }
                            os.SetBoxType(1);
                            os.SortClamp();
                            os.Vacuum(true);
                            os.SortUnclamp();
                            os.Lock();
                            for (int i = 0; i < 2; i++)
                            {
                                BTIntrude = os.SetRobotIntrude(true, false).Item1;
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
                                BTIntrude = os.SetRobotIntrude(false, false).Item1;
                                if (i == 1 && BTIntrude == true || os.ReadRobotIntruded().Item1 == true || os.ReadRobotIntruded().Item2 == true)
                                    throw new Exception("Open Stage has been BT intrude,can net execute command!!");
                            }
                            os.Close();
                            os.Clamp();
                            os.Open();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    };
                    ThreadStart CloseAndStoreBoxThread = () =>
                    {
                        try
                        {
                            //TODO：Cabinet控制推出抽屜
                            os.Close();
                            os.Unclamp();
                            os.Lock();
                            for (int i = 0; i < 2; i++)
                            {
                                BTIntrude = os.SetRobotIntrude(true, false).Item1;
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
                                BTIntrude = os.SetRobotIntrude(false, false).Item1;
                                if (i == 1 && BTIntrude == true || os.ReadRobotIntruded().Item1 == true || os.ReadRobotIntruded().Item2 == true)
                                    throw new Exception("Open Stage has been BT intrude,can net execute command!!");
                            }
                            os.Vacuum(false);
                            for (int i = 0; i < 2; i++)
                            {
                                BTIntrude = os.SetRobotIntrude(true, false).Item1;
                                if (BTIntrude == true)
                                    break;
                                else if (i == 1 && BTIntrude == false)
                                    throw new Exception("Open Stage not allowed to be BT intrude!!");
                                else
                                    os.Initial();
                            }
                            bt.RobotMoving(true);
                            bt.Unclamp();
                            bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet1_Home_Forward_OpenStage_GET.json");
                            bt.Clamp(1);
                            bt.ExePathMove(@"D:\Positions\BTRobot\OpenStage_Backward_Cabinet1_Home_GET.json");
                            bt.RobotMoving(false);
                            for (int i = 0; i < 2; i++)
                            {
                                BTIntrude = os.SetRobotIntrude(false, false).Item1;
                                if (i == 1 && BTIntrude == true || os.ReadRobotIntruded().Item1 == true || os.ReadRobotIntruded().Item2 == true)
                                    throw new Exception("Open Stage has been BT intrude,can net execute command!!");
                            }
                            bt.RobotMoving(true);
                            bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_01_PUT.json");
                            bt.Unclamp();
                            bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_01_Backward_Cabinet_01_Home_PUT.json");
                            bt.RobotMoving(false);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    };
                    ThreadStart LPGetMaskToInspThread = () =>
                      {
                          try
                          {
                              mt.RobotMoving(true);
                              mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                              mt.Unclamp();
                              mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                              mt.Clamp(0);
                              mt.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                              mt.RobotMoving(false);
                              //ic.XYPosition(100,200);
                              //ic.WPosition(51);
                              mt.RobotMoving(true);
                              mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                              mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                              mt.Unclamp();
                              mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                              mt.RobotMoving(false);
                              mt.RobotMoving(true);
                              mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                              mt.Clamp(0);
                              mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                              mt.RobotMoving(false);
                              mt.RobotMoving(true);
                              mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeBackSideToIC.json");
                              mt.Unclamp();
                              mt.ExePathMove(@"D:\Positions\MTRobot\ICBackSideToICHome.json");
                              mt.RobotMoving(false);
                              mt.RobotMoving(true);
                              mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeBackSideToIC.json");
                              mt.Clamp(0);
                              mt.ExePathMove(@"D:\Positions\MTRobot\ICBackSideToICHome.json");
                              mt.RobotMoving(false);
                          }
                          catch (Exception ex)
                          {
                              throw ex;
                          }
                      };
                    ThreadStart MaskCleanThread = () =>
                    {
                        try
                        {
                            mt.RobotMoving(true);
                            mt.ChangeDirection(@"D:\Positions\MTRobot\CleanChHome.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCHomeFrontSideToCC.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCFrontSideToClean.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\FrontSideCleanFinishToCC.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCFrontSideToCapture.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\FrontSideCaptureFinishToCC.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCFrontSideToCCHome.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCHomeBackSideToCC.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCBackSideToClean.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\BackSideCleanFinishToCC.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCBackSideToCapture.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\BackSideCaptureFinishToCC.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCBackSideToCCHome.json");
                            mt.RobotMoving(false);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    };
                    ThreadStart MaskToOSThread = () =>
                    {
                        try
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                MTIntrude = os.SetRobotIntrude(false, true).Item2;
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
                                MTIntrude = os.SetRobotIntrude(false, false).Item2;
                                if (i == 1 && MTIntrude == true || os.ReadRobotIntruded().Item1 == true || os.ReadRobotIntruded().Item2 == true)
                                    throw new Exception("Open Stage has been MT intrude,can net execute command!!");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    };
                    
                    Thread BoxGet = new Thread(GetAndOpenBoxThread);
                    Thread BoxStore = new Thread(CloseAndStoreBoxThread);
                    Thread MaskInsp = new Thread(LPGetMaskToInspThread);
                    Thread MaskClean = new Thread(MaskCleanThread);
                    Thread MaskStore = new Thread(MaskToOSThread);

                    BoxGet.Start();
                    MaskInsp.Start();
                    MaskInsp.Join();
                    MaskClean.Start();
                    MaskClean.Join();
                    BoxGet.Join();
                    MaskStore.Start();
                    MaskStore.Join();
                    BoxStore.Start();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestRobotBankOut()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvaCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    var ic = halContext.HalDevices[MacEnumDevice.inspectionch_assembly.ToString()] as MacHalInspectionCh;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    ic.HalConnect();
                    os.HalConnect();

                    bool BTIntrude = false;
                    bool MTIntrude = false;
                    ThreadStart GetAndOpenBoxThread = () =>
                    {
                        try
                        {
                            //TODO：Cabinet控制推出Box
                            bt.RobotMoving(true);
                            bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                            bt.Unclamp();
                            bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_01_GET.json");
                            bt.Clamp(1);
                            bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_01_Backward_Cabinet_01_Home_GET.json");
                            bt.RobotMoving(false);
                            for (int i = 0; i < 2; i++)
                            {
                                BTIntrude = os.SetRobotIntrude(true, false).Item1;
                                if (BTIntrude == true)
                                    break;
                                else if (i == 1 && BTIntrude == false)
                                    throw new Exception("Open Stage not allowed to be BT intrude!!");
                                else
                                    os.Initial();
                            }
                            bt.RobotMoving(true);
                            bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet1_Home_Forward_OpenStage_PUT.json");
                            bt.Unclamp();
                            bt.ExePathMove(@"D:\Positions\BTRobot\OpenStage_Backward_Cabinet1_Home_PUT.json");
                            bt.RobotMoving(false);
                            for (int i = 0; i < 2; i++)
                            {
                                BTIntrude = os.SetRobotIntrude(false, false).Item1;
                                if (i == 1 && BTIntrude == true || os.ReadRobotIntruded().Item1 == true || os.ReadRobotIntruded().Item2 == true)
                                    throw new Exception("Open Stage has been BT intrude,can net execute command!!");
                            }
                            os.SetBoxType(1);
                            os.SortClamp();
                            os.Vacuum(true);
                            os.SortUnclamp();
                            os.Lock();
                            for (int i = 0; i < 2; i++)
                            {
                                BTIntrude = os.SetRobotIntrude(true, false).Item1;
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
                                BTIntrude = os.SetRobotIntrude(false, false).Item1;
                                if (i == 1 && BTIntrude == true || os.ReadRobotIntruded().Item1 == true || os.ReadRobotIntruded().Item2 == true)
                                    throw new Exception("Open Stage has been BT intrude,can net execute command!!");
                            }
                            os.Close();
                            os.Clamp();
                            os.Open();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    };
                    ThreadStart CloseAndStoreBoxThread = () =>
                    {
                        try
                        {
                            //TODO：Cabinet控制推出抽屜
                            os.Close();
                            os.Unclamp();
                            os.Lock();
                            for (int i = 0; i < 2; i++)
                            {
                                BTIntrude = os.SetRobotIntrude(true, false).Item1;
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
                                BTIntrude = os.SetRobotIntrude(false, false).Item1;
                                if (i == 1 && BTIntrude == true || os.ReadRobotIntruded().Item1 == true || os.ReadRobotIntruded().Item2 == true)
                                    throw new Exception("Open Stage has been BT intrude,can net execute command!!");
                            }
                            os.Vacuum(false);
                            for (int i = 0; i < 2; i++)
                            {
                                BTIntrude = os.SetRobotIntrude(true, false).Item1;
                                if (BTIntrude == true)
                                    break;
                                else if (i == 1 && BTIntrude == false)
                                    throw new Exception("Open Stage not allowed to be BT intrude!!");
                                else
                                    os.Initial();
                            }
                            bt.RobotMoving(true);
                            bt.Unclamp();
                            bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet1_Home_Forward_OpenStage_GET.json");
                            bt.Clamp(1);
                            bt.ExePathMove(@"D:\Positions\BTRobot\OpenStage_Backward_Cabinet1_Home_GET.json");
                            bt.RobotMoving(false);
                            for (int i = 0; i < 2; i++)
                            {
                                BTIntrude = os.SetRobotIntrude(false, false).Item1;
                                if (i == 1 && BTIntrude == true || os.ReadRobotIntruded().Item1 == true || os.ReadRobotIntruded().Item2 == true)
                                    throw new Exception("Open Stage has been BT intrude,can net execute command!!");
                            }
                            bt.RobotMoving(true);
                            bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_01_PUT.json");
                            bt.Unclamp();
                            bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_01_Backward_Cabinet_01_Home_PUT.json");
                            bt.RobotMoving(false);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    };
                    ThreadStart OSGetMaskToInspThread = () =>
                    {
                        try
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                MTIntrude = os.SetRobotIntrude(false, true).Item2;
                                if (MTIntrude == true)
                                    break;
                                else if (i == 1 && MTIntrude == false)
                                    throw new Exception("Open Stage not allowed to be MT intrude!!");
                            }
                            mt.RobotMoving(true);
                            mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                            mt.Unclamp();
                            mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                            mt.Clamp(0);
                            mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                            mt.RobotMoving(false);
                            for (int i = 0; i < 2; i++)
                            {
                                MTIntrude = os.SetRobotIntrude(false, false).Item2;
                                if (i == 1 && MTIntrude == true || os.ReadRobotIntruded().Item1 == true || os.ReadRobotIntruded().Item2 == true)
                                    throw new Exception("Open Stage has been MT intrude,can net execute command!!");
                            }
                            //ic.XYPosition(100,200);
                            //ic.WPosition(51);
                            mt.RobotMoving(true);
                            mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                            mt.Unclamp();
                            mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                            mt.RobotMoving(false);
                            mt.RobotMoving(true);
                            mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                            mt.Clamp(0);
                            mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                            mt.RobotMoving(false);
                            mt.RobotMoving(true);
                            mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeBackSideToIC.json");
                            mt.Unclamp();
                            mt.ExePathMove(@"D:\Positions\MTRobot\ICBackSideToICHome.json");
                            mt.RobotMoving(false);
                            mt.RobotMoving(true);
                            mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeBackSideToIC.json");
                            mt.Clamp(0);
                            mt.ExePathMove(@"D:\Positions\MTRobot\ICBackSideToICHome.json");
                            mt.RobotMoving(false);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    };
                    ThreadStart MaskCleanThread = () =>
                    {
                        try
                        {
                            mt.RobotMoving(true);
                            mt.ChangeDirection(@"D:\Positions\MTRobot\CleanChHome.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCHomeFrontSideToCC.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCFrontSideToClean.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\FrontSideCleanFinishToCC.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCFrontSideToCapture.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\FrontSideCaptureFinishToCC.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCFrontSideToCCHome.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCHomeBackSideToCC.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCBackSideToClean.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\BackSideCleanFinishToCC.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCBackSideToCapture.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\BackSideCaptureFinishToCC.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCBackSideToCCHome.json");
                            mt.RobotMoving(false);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    };
                    ThreadStart MaskToLPThread = () =>
                    {
                        try
                        {
                            mt.RobotMoving(true);
                            mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                            mt.Unclamp();
                            mt.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                            mt.RobotMoving(false);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    };

                    Thread BoxGet = new Thread(GetAndOpenBoxThread);
                    Thread BoxStore = new Thread(CloseAndStoreBoxThread);
                    Thread MaskInsp = new Thread(OSGetMaskToInspThread);
                    Thread MaskClean = new Thread(MaskCleanThread);
                    Thread MaskOut = new Thread(MaskToLPThread);

                    BoxGet.Start();
                    BoxGet.Join();
                    MaskInsp.Start();
                    MaskInsp.Join();
                    BoxStore.Start();
                    MaskClean.Start();
                    MaskClean.Join();
                    MaskOut.Start();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [TestMethod]
        public void TestRobotOCAP()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvaCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    var ic = halContext.HalDevices[MacEnumDevice.inspectionch_assembly.ToString()] as MacHalInspectionCh;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    ic.HalConnect();
                    os.HalConnect();

                    bool BTIntrude = false;
                    bool MTIntrude = false;
                    ThreadStart GetAndOpenBoxThread = () =>
                    {
                        try
                        {
                            //TODO：Cabinet控制推出Box
                            bt.RobotMoving(true);
                            bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                            bt.Unclamp();
                            bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_01_GET.json");
                            bt.Clamp(1);
                            bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_01_Backward_Cabinet_01_Home_GET.json");
                            bt.RobotMoving(false);
                            for (int i = 0; i < 2; i++)
                            {
                                BTIntrude = os.SetRobotIntrude(true, false).Item1;
                                if (BTIntrude == true)
                                    break;
                                else if (i == 1 && BTIntrude == false)
                                    throw new Exception("Open Stage not allowed to be BT intrude!!");
                                else
                                    os.Initial();
                            }
                            bt.RobotMoving(true);
                            bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet1_Home_Forward_OpenStage_PUT.json");
                            bt.Unclamp();
                            bt.ExePathMove(@"D:\Positions\BTRobot\OpenStage_Backward_Cabinet1_Home_PUT.json");
                            bt.RobotMoving(false);
                            for (int i = 0; i < 2; i++)
                            {
                                BTIntrude = os.SetRobotIntrude(false, false).Item1;
                                if (i == 1 && BTIntrude == true || os.ReadRobotIntruded().Item1 == true || os.ReadRobotIntruded().Item2 == true)
                                    throw new Exception("Open Stage has been BT intrude,can net execute command!!");
                            }
                            os.SetBoxType(1);
                            os.SortClamp();
                            os.Vacuum(true);
                            os.SortUnclamp();
                            os.Lock();
                            for (int i = 0; i < 2; i++)
                            {
                                BTIntrude = os.SetRobotIntrude(true, false).Item1;
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
                                BTIntrude = os.SetRobotIntrude(false, false).Item1;
                                if (i == 1 && BTIntrude == true || os.ReadRobotIntruded().Item1 == true || os.ReadRobotIntruded().Item2 == true)
                                    throw new Exception("Open Stage has been BT intrude,can net execute command!!");
                            }
                            os.Close();
                            os.Clamp();
                            os.Open();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    };
                    ThreadStart CloseAndStoreBoxThread = () =>
                    {
                        try
                        {
                            //TODO：Cabinet控制推出抽屜
                            os.Close();
                            os.Unclamp();
                            os.Lock();
                            for (int i = 0; i < 2; i++)
                            {
                                BTIntrude = os.SetRobotIntrude(true, false).Item1;
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
                                BTIntrude = os.SetRobotIntrude(false, false).Item1;
                                if (i == 1 && BTIntrude == true || os.ReadRobotIntruded().Item1 == true || os.ReadRobotIntruded().Item2 == true)
                                    throw new Exception("Open Stage has been BT intrude,can net execute command!!");
                            }
                            os.Vacuum(false);
                            for (int i = 0; i < 2; i++)
                            {
                                BTIntrude = os.SetRobotIntrude(true, false).Item1;
                                if (BTIntrude == true)
                                    break;
                                else if (i == 1 && BTIntrude == false)
                                    throw new Exception("Open Stage not allowed to be BT intrude!!");
                                else
                                    os.Initial();
                            }
                            bt.RobotMoving(true);
                            bt.Unclamp();
                            bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet1_Home_Forward_OpenStage_GET.json");
                            bt.Clamp(1);
                            bt.ExePathMove(@"D:\Positions\BTRobot\OpenStage_Backward_Cabinet1_Home_GET.json");
                            bt.RobotMoving(false);
                            for (int i = 0; i < 2; i++)
                            {
                                BTIntrude = os.SetRobotIntrude(false, false).Item1;
                                if (i == 1 && BTIntrude == true || os.ReadRobotIntruded().Item1 == true || os.ReadRobotIntruded().Item2 == true)
                                    throw new Exception("Open Stage has been BT intrude,can net execute command!!");
                            }
                            bt.RobotMoving(true);
                            bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_01_PUT.json");
                            bt.Unclamp();
                            bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_01_Backward_Cabinet_01_Home_PUT.json");
                            bt.RobotMoving(false);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    };
                    ThreadStart LPGetMaskToInspThread = () =>
                    {
                        try
                        {
                            mt.RobotMoving(true);
                            mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                            mt.Unclamp();
                            mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                            mt.Clamp(0);
                            mt.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                            mt.RobotMoving(false);
                            //ic.XYPosition(100,200);
                            //ic.WPosition(51);
                            mt.RobotMoving(true);
                            mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                            mt.Unclamp();
                            mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                            mt.RobotMoving(false);
                            mt.RobotMoving(true);
                            mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                            mt.Clamp(0);
                            mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                            mt.RobotMoving(false);
                            mt.RobotMoving(true);
                            mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeBackSideToIC.json");
                            mt.Unclamp();
                            mt.ExePathMove(@"D:\Positions\MTRobot\ICBackSideToICHome.json");
                            mt.RobotMoving(false);
                            mt.RobotMoving(true);
                            mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeBackSideToIC.json");
                            mt.Clamp(0);
                            mt.ExePathMove(@"D:\Positions\MTRobot\ICBackSideToICHome.json");
                            mt.RobotMoving(false);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    };
                    ThreadStart MaskCleanThread = () =>
                    {
                        try
                        {
                            mt.RobotMoving(true);
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCHomeFrontSideToCC.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCFrontSideToClean.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\FrontSideCleanFinishToCC.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCFrontSideToCapture.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\FrontSideCaptureFinishToCC.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCFrontSideToCCHome.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCHomeBackSideToCC.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCBackSideToClean.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\BackSideCleanFinishToCC.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCBackSideToCapture.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\BackSideCaptureFinishToCC.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\CCBackSideToCCHome.json");
                            mt.RobotMoving(false);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    };
                    ThreadStart MaskToLPThread = () =>
                    {
                        try
                        {
                            mt.RobotMoving(true);
                            mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                            mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                            mt.Unclamp();
                            mt.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                            mt.RobotMoving(false);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    };

                    Thread BoxGet = new Thread(GetAndOpenBoxThread);
                    Thread BoxStore = new Thread(CloseAndStoreBoxThread);
                    Thread MaskInsp = new Thread(LPGetMaskToInspThread);
                    Thread MaskClean = new Thread(MaskCleanThread);
                    Thread MaskOut = new Thread(MaskToLPThread);
                    
                    MaskInsp.Start();
                    MaskInsp.Join();
                    MaskClean.Start();
                    MaskClean.Join();
                    MaskOut.Start();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        
    }
}
