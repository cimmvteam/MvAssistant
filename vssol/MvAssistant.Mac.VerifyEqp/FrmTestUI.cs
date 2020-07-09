using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MvAssistantMacVerifyEqp
{
    public partial class FrmTestUI : Form
    {
       
        TestDrawers drawers;
        TestLoadPorts loadPorts;
        public FrmTestUI()
        {

            //
            InitializeComponent();
        }



        private void FrmTestUI_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            drawers = new TestDrawers(this);
            loadPorts = new TestLoadPorts(this);
        }

        private void btnInitialDrawerA_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerA);
            drawers.DisableDrawerComps(drawers.DrawerA);
            drawers.DrawerA.CommandINI();
        }

        private void GrpDrawerA_Enter(object sender, EventArgs e)
        {

        }

        private void btnMoveDrawerAHome_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerA);
            drawers.DisableDrawerComps(drawers.DrawerA);
            drawers.DrawerA.CommandTrayMotionHome();
        }

        private void txtBxDetectDrawerA_Click(object sender, EventArgs e)
        {
            if (sender != null) { drawers.countDrawerBox = false; }
            chkBoxDrawerAHasbox.Checked = false;
            drawers.InitialDRawer(drawers.DrawerA);
            drawers.DisableDrawerComps(drawers.DrawerA);
            drawers.DrawerA.CommandBoxDetection();
        }

        private void btnInitialDrawerB_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerB);
            drawers.DisableDrawerComps(drawers.DrawerB);
            drawers.DrawerB.CommandINI();
        }

        private void btnMoveDrawerBHome_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerB);
            drawers.DisableDrawerComps(drawers.DrawerB);
            drawers.DrawerB.CommandTrayMotionHome();
        }

        private void txtBxDetectDrawerB_Click(object sender, EventArgs e)
        {
            if (sender != null) { drawers.countDrawerBox = false; }
            chkBoxDrawerBHasbox.Checked = false;
            drawers.InitialDRawer(drawers.DrawerB);
            drawers.DisableDrawerComps(drawers.DrawerB);
            drawers.DrawerB.CommandBoxDetection();
        }

        private void btnInitialDrawerC_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerC);
            drawers.DisableDrawerComps(drawers.DrawerC);
            drawers.DrawerC.CommandINI();
        }

        private void btnMoveDrawerCHome_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerC);
            drawers.DisableDrawerComps(drawers.DrawerC);
            drawers.DrawerC.CommandTrayMotionHome();
        }

        private void txtBxDetectDrawerC_Click(object sender, EventArgs e)
        {
            if (sender != null) { drawers.countDrawerBox = false; }
            chkBoxDrawerCHasbox.Checked = false;
            drawers.InitialDRawer(drawers.DrawerC);
            drawers.DisableDrawerComps(drawers.DrawerC);
            drawers.DrawerC.CommandBoxDetection();
        }
            
        //}

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void txtBoxType_Click(object sender, EventArgs e)
        {
            txtBoxType.Text = "";
        }

        private void BTGetDR_0204_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBoxType.Text != "1" && txtBoxType.Text != "2")
                { MessageBox.Show("Box Type請輸入數字1或2"); return; }
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    bt.HalConnect();
                    
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_02_04_GET.json");
                    Console.WriteLine(bt.Clamp(Convert.ToUInt32(txtBoxType.Text)));
                    bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_02_04_Backward_Cabinet_01_Home_GET.json");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        private void BTPutOS_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBoxType.Text != "1" && txtBoxType.Text != "2")
                { MessageBox.Show("Box Type請輸入數字1或2"); return; }
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

        private void btnInitialDrawerD_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerD);
            drawers.DisableDrawerComps(drawers.DrawerD);
            drawers.DrawerD.CommandINI();
        }
        private void Unlock_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBoxType.Text != "1" && txtBoxType.Text != "2")
                { MessageBox.Show("Box Type請輸入數字1或2"); return; }
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
                    
                    os.SetBoxType(Convert.ToUInt32(txtBoxType.Text));
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

        private void BTPutDR_0204_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBoxType.Text != "1" && txtBoxType.Text != "2")
                { MessageBox.Show("Box Type請輸入數字1或2"); return; }
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

        private void BTGetOS_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBoxType.Text != "1" && txtBoxType.Text != "2")
                { MessageBox.Show("Box Type請輸入數字1或2"); return; }
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
                    bt.Clamp(Convert.ToUInt32(txtBoxType.Text));
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

        private void Lock_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBoxType.Text != "1" && txtBoxType.Text != "2")
                { MessageBox.Show("Box Type請輸入數字1或2"); return; }
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

        private void btnMoveDrawerDHome_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerD);
            drawers.DisableDrawerComps(drawers.DrawerD);
            drawers.DrawerD.CommandTrayMotionHome();
        }

        private void txtBxDetectDrawerD_Click(object sender, EventArgs e)
        {
            if (sender != null) { drawers.countDrawerBox = false; }
            chkBoxDrawerDHasbox.Checked = false;
            drawers.InitialDRawer(drawers.DrawerD);
            drawers.DisableDrawerComps(drawers.DrawerD);
            drawers.DrawerD.CommandBoxDetection();
        }

        private void btnInitialAllDrawer_Click(object sender, EventArgs e)
        {
            this.btnInitialDrawerA_Click(null, null);
            this.btnInitialDrawerB_Click(null, null);
            this.btnInitialDrawerC_Click(null, null);
            this.btnInitialDrawerD_Click(null, null);
        }

        private void btnMoveAllDrawersHome_Click(object sender, EventArgs e)
        {
            this.btnMoveDrawerAHome_Click(null, null);
            this.btnMoveDrawerBHome_Click(null, null);
            this.btnMoveDrawerCHome_Click(null, null);
            this.btnMoveDrawerDHome_Click(null, null);
        }

        private void btnDetectAllDrawers_Click(object sender, EventArgs e)
        {
            txtDrawerBoxNum.Text = "0";
            drawers.countDrawerBox = true;
            this.txtBxDetectDrawerA_Click(null, null);
            this.txtBxDetectDrawerB_Click(null, null);
            this.txtBxDetectDrawerC_Click(null, null);
            this.txtBxDetectDrawerD_Click(null, null);
        }

        private void BtnReleaseAllComp_Click(object sender, EventArgs e)
        {
            this.grpDrawerAComp.Enabled = this.grpDrawerBComp.Enabled = this.grpDrawerCComp.Enabled = this.grpDrawerDComp.Enabled = true;
        }

        private void btnLoadPortBAlarmReset_Click(object sender, EventArgs e)
        {
            this.loadPorts.ResetResult(loadPorts.LoadPort2);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort2);
            loadPorts.LoadPort2.CommandAlarmReset();
            //this.loadPorts.ResetResult(loadPorts.LoadPort2);
            //this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort2);
        }

        private void btnInitialLoadportB_Click(object sender, EventArgs e)
        {
            this.loadPorts.ResetResult(loadPorts.LoadPort2);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort2);
            loadPorts.LoadPort2.CommandInitialRequest();
        }

        private void btnLoadportBBarcode_Click(object sender, EventArgs e)
        {
            this.loadPorts.ResetResult(loadPorts.LoadPort2);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort2);
            loadPorts.LoadPort2.CommandAskBarcodeStatus();
        }

        private void btnLoadportBRFID_Click(object sender, EventArgs e)
        {
            this.loadPorts.ResetResult(loadPorts.LoadPort2);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort2);
            loadPorts.LoadPort2.CommandAskRFIDStatus();
        }

        private void btnLoadportABarcode_Click(object sender, EventArgs e)
        {
            this.loadPorts.ResetResult(loadPorts.LoadPort1);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort1);
            loadPorts.LoadPort1.CommandAskBarcodeStatus();
        }

        private void btnLoadportARFID_Click(object sender, EventArgs e)
        {
            this.loadPorts.ResetResult(loadPorts.LoadPort1);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort1);
            loadPorts.LoadPort1.CommandAskRFIDStatus();
        }

        private void btnLoadPortAAlarmReset_Click(object sender, EventArgs e)
        {

            this.loadPorts.ResetResult(loadPorts.LoadPort1);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort1);
            loadPorts.LoadPort1.CommandAlarmReset();
        }

        private void btnInitialLoadportA_Click(object sender, EventArgs e)
        {
            this.loadPorts.ResetResult(loadPorts.LoadPort1);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort1);
            loadPorts.LoadPort1.CommandInitialRequest();
        }

        private void btnMoveDrawerCIn_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerC);
            drawers.DisableDrawerComps(drawers.DrawerC);
            drawers.DrawerC.CommandTrayMotionIn();
        }

        private void btnMoveDrawerAIn_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerA);
            drawers.DisableDrawerComps(drawers.DrawerA);
            drawers.DrawerA.CommandTrayMotionIn();
        }

        private void btnMoveDrawerBIn_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerB);
            drawers.DisableDrawerComps(drawers.DrawerB);
            drawers.DrawerB.CommandTrayMotionIn();
        }

        private void btnMoveDrawerDIn_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerD);
            drawers.DisableDrawerComps(drawers.DrawerD);
            drawers.DrawerD.CommandTrayMotionIn();
        }

        private void btnMoveAllDrawersIn_Click(object sender, EventArgs e)
        {
            this.btnMoveDrawerAIn_Click(null, null);
            this.btnMoveDrawerBIn_Click(null, null);
            this.btnMoveDrawerCIn_Click(null, null);
            this.btnMoveDrawerDIn_Click(null, null);
        }
    }


    
}
