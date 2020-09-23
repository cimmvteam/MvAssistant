using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

        public bool CheckPara()
        {
            bool result = false;
            string BoxType = txtBoxType.Text;
            string DwColumn = txtDrawerColumn.Text.PadLeft(2, '0');
            string DwRow = txtDrawerRow.Text.PadLeft(2, '0');
            if (BoxType != "1" && BoxType != "2")
                MessageBox.Show("Box Type請輸入數字1或2");
            else if (DwColumn != "01" && DwColumn != "02" && DwColumn != "03" && DwColumn != "04" && DwColumn != "05" && DwColumn != "06" && DwColumn != "07")
                MessageBox.Show("Drawer請輸入數字1-1 ~ 7-5之間的數值");
            else if (DwRow != "01" && DwRow != "02" && DwRow != "03" && DwRow != "04" && DwRow != "05")
                MessageBox.Show("Drawer請輸入數字1-1 ~ 7-5之間的數值");
            else
                result = true;
            return result;
        }

        private void BTGetDR_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckPara() == false)
                    return;
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    bt.HalConnect();

                    bt.Initial();
                    if (Convert.ToInt16(txtDrawerColumn.Text) >= 1 && Convert.ToInt16(txtDrawerColumn.Text) <= 3)
                    {
                        bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                        bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_" + txtDrawerColumn.Text.PadLeft(2, '0') + "_" + txtDrawerRow.Text.PadLeft(2, '0') + "_GET.json");
                        Console.WriteLine(bt.Clamp(Convert.ToUInt32(txtBoxType.Text)));
                        bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_" + txtDrawerColumn.Text.PadLeft(2, '0') + "_" + txtDrawerRow.Text.PadLeft(2, '0') + "_Backward_Cabinet_01_Home_GET.json");
                    }
                    else if (Convert.ToInt16(txtDrawerColumn.Text) >= 4 && Convert.ToInt16(txtDrawerColumn.Text) <= 7)
                    {
                        bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                        bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_" + txtDrawerColumn.Text.PadLeft(2, '0') + "_" + txtDrawerRow.Text.PadLeft(2, '0') + "_GET.json");
                        Console.WriteLine(bt.Clamp(Convert.ToUInt32(txtBoxType.Text)));
                        bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_" + txtDrawerColumn.Text.PadLeft(2, '0') + "_" + txtDrawerRow.Text.PadLeft(2, '0') + "_Backward_Cabinet_02_Home_GET.json");
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        private void BTPutOS_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckPara() == false)
                    return;
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

                    BTIntrude = os.ReadRobotIntrude(false, false).Item1;
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
                if (CheckPara() == false)
                    return;
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
                    bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    if (txtBoxType.Text == "1")
                        bt.ExePathMove(@"D:\Positions\BTRobot\UnlockIronBox.json");
                    else if (txtBoxType.Text == "2")
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
                }
            }
            catch (Exception ex) { throw ex; }
        }

        private void BTPutDR_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckPara() == false)
                    return;
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    bt.HalConnect();

                    if (Convert.ToInt16(txtDrawerColumn.Text) >= 1 && Convert.ToInt16(txtDrawerColumn.Text) <= 3)
                    {
                        bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                        bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_" + txtDrawerColumn.Text.PadLeft(2, '0') + "_" + txtDrawerRow.Text.PadLeft(2, '0') + "_PUT.json");
                        Console.WriteLine(bt.Clamp(Convert.ToUInt32(txtBoxType.Text)));
                        bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_" + txtDrawerColumn.Text.PadLeft(2, '0') + "_" + txtDrawerRow.Text.PadLeft(2, '0') + "_Backward_Cabinet_01_Home_PUT.json");
                    }
                    else if (Convert.ToInt16(txtDrawerColumn.Text) >= 4 && Convert.ToInt16(txtDrawerColumn.Text) <= 7)
                    {
                        bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                        bt.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_" + txtDrawerColumn.Text.PadLeft(2, '0') + "_" + txtDrawerRow.Text.PadLeft(2, '0') + "_PUT.json");
                        Console.WriteLine(bt.Clamp(Convert.ToUInt32(txtBoxType.Text)));
                        bt.ExePathMove(@"D:\Positions\BTRobot\Drawer_" + txtDrawerColumn.Text.PadLeft(2, '0') + "_" + txtDrawerRow.Text.PadLeft(2, '0') + "_Backward_Cabinet_02_Home_PUT.json");
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        private void BTGetOS_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckPara() == false)
                    return;
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
                if (CheckPara() == false)
                    return;
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
                    if (txtBoxType.Text == "1")
                        bt.ExePathMove(@"D:\Positions\BTRobot\LockIronBox.json");
                    else if (txtBoxType.Text == "2")
                        bt.ExePathMove(@"D:\Positions\BTRobot\LockCrystalBox.json");
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
            string commandText = drawers.DrawerC.CommandTrayMotionIn();
            Debug.WriteLine("Drawer Rtn= " + commandText);
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

        public void btnLoadPortADock_Click(object sender, EventArgs e)
        {

            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort1);
            if (TestLoadPorts.Loport1CycleRunFlag)
            {
                var times = Convert.ToInt32(txtBxLoadPortACurrentCycle.Text);
                times++;
                // txtBxLoadPortACurrentCycle.Text = times.ToString();
                //  txtBxLoportAResult.Text = txtBxLoportAResult.Text + $"\r\nCycles: {times}............\r\n[DOCK]";
                loadPorts.SetResult(loadPorts.LoadPort1, $"\r\nLoadPort[] Cycles: {times}............\r\n[DOCK]");
            }
            else
            {
                this.loadPorts.ResetResult(loadPorts.LoadPort1);
            }
            loadPorts.LoadPort1.CommandDockRequest();
        }

        public void btnLoadPortAUnDock_Click(object sender, EventArgs e)
        {
            if (TestLoadPorts.Loport1CycleRunFlag)
            {
                // txtBxLoportAResult.Text = txtBxLoportAResult.Text + "\r\n[UN DOCK]";
                loadPorts.SetResult(loadPorts.LoadPort1, "\r\nLoadPort[] [UN DOCK]");
            }
            else
            {
                this.loadPorts.ResetResult(loadPorts.LoadPort1);
            }
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort1);
            loadPorts.LoadPort1.CommandUndockRequest();
        }

        public void btnLoadPortBDock_Click(object sender, EventArgs e)
        {

            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort2);

            if (TestLoadPorts.Loport2CycleRunFlag)
            {
                var times = Convert.ToInt32(txtBxLoadPortBCurrentCycle.Text);
                times++;
                // txtBxLoadPortBCurrentCycle.Text = times.ToString();
                //txtBxLoportBResult.Text = txtBxLoportBResult.Text + $"\r\nCycle: {times}............\r\n[DOCK]";
                loadPorts.SetResult(loadPorts.LoadPort2, $"\r\nLoadPort[] Cycle: {times}............\r\n[DOCK]");
            }
            else
            {
                this.loadPorts.ResetResult(loadPorts.LoadPort2);
            }
            loadPorts.LoadPort2.CommandDockRequest();
            /**
            this.loadPorts.ResetResult(loadPorts.LoadPort1);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort1);
            if (TestLoadPorts.Loport1CycleRunFlag)
            {
                var times = Convert.ToInt32(txtBxLoadPortACurrentCycle.Text);
                times++;
                txtBxLoadPortACurrentCycle.Text = times.ToString();
                txtBxLoportAResult.Text = txtBxLoportAResult.Text + $"\r\nCycle: {times}, Dock";
            }
            loadPorts.LoadPort1.CommandDockRequest();
            */
        }

        public void btnLoadPortBUnDock_Click(object sender, EventArgs e)
        {

            if (TestLoadPorts.Loport2CycleRunFlag)
            {
                //txtBxLoportBResult.Text = txtBxLoportBResult.Text + "\r\n[UN DOCK]";
                loadPorts.SetResult(loadPorts.LoadPort2, "\r\nLoport[] [UN DOCK]");
            }
            else
            {
                this.loadPorts.ResetResult(loadPorts.LoadPort2);
            }
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort2);
            loadPorts.LoadPort2.CommandUndockRequest();
        }

        private void btnMoveDrawerCOut_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerC);
            drawers.DisableDrawerComps(drawers.DrawerC);
            var commandText = drawers.DrawerC.CommandTrayMotionOut();
            Debug.WriteLine("Drawer Rtn = " + commandText);

        }

        private void btnMoveDrawerDOut_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerD);
            drawers.DisableDrawerComps(drawers.DrawerD);
            drawers.DrawerD.CommandTrayMotionOut();
        }

        private void btnMoveDrawerAOut_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerA);
            drawers.DisableDrawerComps(drawers.DrawerA);
            drawers.DrawerA.CommandTrayMotionOut();
        }

        private void btnMoveDrawerBOut_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerB);
            drawers.DisableDrawerComps(drawers.DrawerB);
            drawers.DrawerB.CommandTrayMotionOut();
        }

        private void btnTurnOnDrawerAAllLeds_Click(object sender, EventArgs e)
        {

            drawers.InitialDRawer(drawers.DrawerA);
            drawers.DisableDrawerComps(drawers.DrawerA);
            drawers.DrawerA.CommandBrightLEDAllOn();
        }

        private void btnTurnOffDrawerAAllLeds_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerA);
            drawers.DisableDrawerComps(drawers.DrawerA);
            drawers.DrawerA.CommandBrightLEDAllOff();
        }

        private void btnTurnOffDrawerBAllLeds_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerB);
            drawers.DisableDrawerComps(drawers.DrawerB);
            drawers.DrawerB.CommandBrightLEDAllOff();
        }

        private void btnTurnOnDrawerBAllLeds_Click(object sender, EventArgs e)
        {

            drawers.InitialDRawer(drawers.DrawerB);
            drawers.DisableDrawerComps(drawers.DrawerB);
            drawers.DrawerB.CommandBrightLEDAllOn();
        }

        private void btnTurnOffDrawerCAllLeds_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerC);
            drawers.DisableDrawerComps(drawers.DrawerC);
            drawers.DrawerC.CommandBrightLEDAllOff();
        }

        private void btnTurnOnDrawerCAllLeds_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerC);
            drawers.DisableDrawerComps(drawers.DrawerC);
            drawers.DrawerC.CommandBrightLEDAllOn();
        }

        private void btnTurnOnDrawerDAllLeds_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerD);
            drawers.DisableDrawerComps(drawers.DrawerD);
            drawers.DrawerD.CommandBrightLEDAllOn();
        }

        private void btnTurnOffDrawerDAllLeds_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerD);
            drawers.DisableDrawerComps(drawers.DrawerD);
            drawers.DrawerD.CommandBrightLEDAllOff();
        }

        private void btnMoveAllDrawersOut_Click(object sender, EventArgs e)
        {
            this.btnMoveDrawerAOut_Click(null, null);
            this.btnMoveDrawerBOut_Click(null, null);
            this.btnMoveDrawerCOut_Click(null, null);
            this.btnMoveDrawerDOut_Click(null, null);
        }

        private void btnTurnOnAllLeds_Click(object sender, EventArgs e)
        {
            this.btnTurnOnDrawerAAllLeds_Click(null, null);
            this.btnTurnOnDrawerBAllLeds_Click(null, null);
            this.btnTurnOnDrawerCAllLeds_Click(null, null);
            this.btnTurnOnDrawerDAllLeds_Click(null, null);
        }

        private void btnTurnOffAllLeds_Click(object sender, EventArgs e)
        {
            this.btnTurnOffDrawerAAllLeds_Click(null, null);
            this.btnTurnOffDrawerBAllLeds_Click(null, null);
            this.btnTurnOffDrawerCAllLeds_Click(null, null);
            this.btnTurnOffDrawerDAllLeds_Click(null, null);
        }

        private void btnLoadPortACycleStart_Click(object sender, EventArgs e)
        {
            txtBxLoadPortACurrentCycle.Text = "0";
            this.loadPorts.ResetResult(loadPorts.LoadPort1);
            TestLoadPorts.Loport1CycleRunFlag = true;
            btnLoadPortADock_Click(btnLoadPortADock, EventArgs.Empty);
        }

        private void btnLoadPortACycleStop_Click(object sender, EventArgs e)
        {
            TestLoadPorts.Loport1CycleRunFlag = false;
        }

        private void btnLoadPortBCycleStart_Click(object sender, EventArgs e)
        {
            txtBxLoadPortBCurrentCycle.Text = "0";
            loadPorts.ResetResult(loadPorts.LoadPort2);
            TestLoadPorts.Loport2CycleRunFlag = true;
            btnLoadPortBDock_Click(btnLoadPortBDock, EventArgs.Empty);
        }

        private void btnLoadPortBCycleStop_Click(object sender, EventArgs e)
        {
            TestLoadPorts.Loport2CycleRunFlag = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            grpLoadportA.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            grpLoadportB.Enabled = true;
        }

        private BackgroundWorker worker;
        private BackgroundWorker BT_worker;
        private delegate void Cancelable_DoWork();
        private int MaskTransferWorkTimes = 0;
        private int BoxTransferWorkTimes = 0;
        private DateTime MTStartTime = DateTime.Now;
        private string DateDiff(DateTime EndDate, DateTime StartDate)
        {
            string dateDiff = null;
            TimeSpan ts1 = new TimeSpan(EndDate.Ticks);
            TimeSpan ts2 = new TimeSpan(StartDate.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小時" + ts.Minutes.ToString() + "分鐘" + ts.Seconds.ToString() + "秒";
            return dateDiff;
        }
        public void LogInfo(string pMessage)
        {
            string tFilePath = @"D:\Logg.txt";
            StreamWriter tStreamWriter = null;
            try
            {
                if (!File.Exists(tFilePath))
                    File.Create(tFilePath);
                tStreamWriter = new StreamWriter(tFilePath, true, System.Text.UTF8Encoding.UTF8);
                tStreamWriter.WriteLine(pMessage);
            }
            catch (Exception e) { }
            finally { if (tStreamWriter != null) tStreamWriter.Close(); }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(do_work);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            worker.RunWorkerAsync();
        }

        private void do_work(object sender, DoWorkEventArgs e)
        {
            Cancelable_DoWork work = new Cancelable_DoWork(MaskMove);

            //開始非同步執行(MaskMove)方法 
            IAsyncResult rmm = work.BeginInvoke(null, null);

            //(非同步模式，在執行一個很耗時的方法(MaskMove)時,還能繼續向下執行程式) 

            //執行下面的While，判斷非同步操作是否完成 
            while (!rmm.IsCompleted)
            {
                //還沒完成，判斷是否取消了backgroundworker非同步操作 
                if (worker.CancellationPending)
                {
                    //如果是，馬上取消backgroundwork操作(這個地方才是真正取消非同步執行) 
                    e.Cancel = true;
                    return;
                }
            }
            //e.Result = work.EndInvoke(rmm); //返回查询结果 赋值给e.Result 
        }

        private void MaskMove()
        {
            MTStartTime = DateTime.Now;
            lblMTStartTime.Text = MTStartTime.ToString("yyyyMMdd HH:mm:ss");
            MaskTransferWorkTimes = 0;
            int Times = 0, CycleTimes = 0;
            if (int.TryParse(txtCycleTimes.Text, out CycleTimes))
            { CycleTimes = Convert.ToInt32(txtCycleTimes.Text); }
            else
            { MessageBox.Show("循環次數請輸入數字!!!"); return; }
            try
            {
                btnStart.Enabled = false;
                btnEnd.Enabled = true;
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
                        //os.Initial();
                        mt.Initial();
                        ic.ReadRobotIntrude(false);
                        ic.Initial();

                        //os.SetBoxType(2);
                        //os.SortClamp();
                        //Thread.Sleep(1000);
                        //os.SortUnclamp();
                        //os.SortClamp();
                        //Thread.Sleep(1000);
                        //os.Vacuum(true);
                        //os.SortUnclamp();
                        //os.Lock();
                        //os.Close();
                        //os.Clamp();
                        //os.Open();
                    }
                    for (Times = 1; Times <= CycleTimes; Times++)
                    {
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "第[ " + Times + "]次機械手臂測試開始");
                        MaskTransferWorkTimes = Times;
                        worker.ReportProgress(Times);
                        Thread.Sleep(1000);

                        //ic.ReadRobotIntrude(false);
                        //ic.XYPosition(0, 0);
                        //ic.WPosition(0);

                        mt.RobotMoving(true);
                        mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " LP Home => LP");
                        mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                        mt.Clamp(1);
                        mt.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " LP => LP Home");
                        mt.RobotMoving(false);

                        ic.ReadRobotIntrude(true);
                        mt.RobotMoving(true);
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " LP Home => IC Home");
                        mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " IC Home => IC");
                        mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                        mt.Unclamp();
                        mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " IC => IC Home");
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " IC Home => IC");
                        mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                        mt.Clamp(1);
                        mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " IC => IC Home");
                        mt.RobotMoving(false);
                        ic.ReadRobotIntrude(false);

                        for (int i = 0; i < 2; i++)
                        {
                            MTIntrude = os.ReadRobotIntrude(false, true).Item2;
                            if (MTIntrude == true)
                                break;
                            else if (i == 1 && MTIntrude == false)
                                throw new Exception("Open Stage not allowed to be MT intrude!!");
                        }
                        mt.RobotMoving(true);
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " IC Home => LP Home");
                        mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " LP Home => OS");
                        mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                        mt.Unclamp();
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " OS => LP Home");
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " LP Home => OS");
                        mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                        mt.Clamp(1);
                        mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " OS => LP Home");
                        mt.RobotMoving(false);
                        for (int i = 0; i < 2; i++)
                        {
                            MTIntrude = os.ReadRobotIntrude(false, false).Item2;
                            if (i == 1 && MTIntrude == true || os.ReadBeenIntruded() == true)
                                throw new Exception("Open Stage has been MT intrude,can net execute command!!");
                        }

                        ic.ReadRobotIntrude(true);
                        mt.RobotMoving(true);
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " LP Home => IC Home");
                        mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " IC Home => IC");
                        mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                        mt.Unclamp();
                        mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " IC => IC Home");
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " IC Home => IC");
                        mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                        mt.Clamp(1);
                        mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " IC => IC Home");
                        mt.RobotMoving(false);
                        ic.ReadRobotIntrude(false);

                        mt.RobotMoving(true);
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " IC Home => LP Home");
                        mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " LP Home => LP");
                        mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                        mt.Unclamp();
                        mt.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " LP => LP Home");
                        mt.RobotMoving(false);
                        LogInfo(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "第[ " + Times + "]次機械手臂測試結束");
                        #region 20200814 version
                        ////Get mask from Open Stage 
                        //for (int i = 0; i < 2; i++)
                        //{
                        //    MTIntrude = os.ReadRobotIntrude(false, true).Item2;
                        //    if (MTIntrude == true)
                        //        break;
                        //    else if (i == 1 && MTIntrude == false)
                        //        throw new Exception("Open Stage not allowed to be MT intrude!!");
                        //}
                        //mt.RobotMoving(true);
                        //mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                        //mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                        //mt.Clamp(1);
                        //mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                        //mt.RobotMoving(false);
                        //for (int i = 0; i < 2; i++)
                        //{
                        //    MTIntrude = os.ReadRobotIntrude(false, false).Item2;
                        //    if (i == 1 && MTIntrude == true || os.ReadBeenIntruded() == true)
                        //        throw new Exception("Open Stage has been MT intrude,can net execute command!!");
                        //}

                        ////Put glass side into Inspection Chamber
                        ////ic.Initial();
                        //ic.ReadRobotIntrude(false);
                        //ic.XYPosition(0, 0);
                        //ic.WPosition(0);
                        //ic.ReadRobotIntrude(true);
                        //mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                        //mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                        //mt.Unclamp();
                        //mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                        //ic.ReadRobotIntrude(false);
                        ////Get glass side from Inspection Chamber
                        //ic.ZPosition(-29.6);
                        //for (int i = 158; i <= 296; i += 23)
                        //{
                        //    for (int j = 123; j <= 261; j += 23)
                        //    {
                        //        ic.XYPosition(i, j);
                        //        ic.Camera_TopInsp_CapToSave("D:/Image/IC/TopInsp", "bmp");
                        //        Thread.Sleep(500);
                        //    }
                        //}
                        //ic.XYPosition(246, 208);
                        //for (int i = 0; i < 360; i += 90)
                        //{
                        //    ic.WPosition(i);
                        //    ic.Camera_SideInsp_CapToSave("D:/Image/IC/SideInsp", "bmp");
                        //    Thread.Sleep(500);
                        //}

                        //ic.XYPosition(0, 0);
                        //ic.WPosition(0);
                        //ic.ReadRobotIntrude(true);
                        //mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                        //mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                        //mt.Clamp(1);
                        //mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                        //ic.ReadRobotIntrude(false);

                        ////Release mask to Open Stage
                        //for (int i = 0; i < 2; i++)
                        //{
                        //    MTIntrude = os.ReadRobotIntrude(false, true).Item2;
                        //    if (MTIntrude == true)
                        //        break;
                        //    else if (i == 1 && MTIntrude == false)
                        //        throw new Exception("Open Stage not allowed to be MT intrude!!");
                        //}
                        //mt.RobotMoving(true);
                        //mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                        //mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                        //mt.Unclamp();
                        //mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                        //mt.RobotMoving(false);
                        //for (int i = 0; i < 2; i++)
                        //{
                        //    MTIntrude = os.ReadRobotIntrude(false, false).Item2;
                        //    if (i == 1 && MTIntrude == true || os.ReadBeenIntruded() == true)
                        //        throw new Exception("Open Stage has been MT intrude,can net execute command!!");
                        //}
                        #endregion 20200814 version
                        #region 20200908 version IC => OS => LPA => IC
                        //MaskTransferWorkTimes = Times;
                        //worker.ReportProgress(Times);
                        //Thread.Sleep(1000);

                        //ic.ReadRobotIntrude(false);
                        //ic.XYPosition(0, 0);
                        //ic.WPosition(0);
                        //ic.ReadRobotIntrude(true);
                        //mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                        //mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                        //mt.Clamp(1);
                        //mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                        //ic.ReadRobotIntrude(false);

                        //for (int i = 0; i < 2; i++)
                        //{
                        //    MTIntrude = os.ReadRobotIntrude(false, true).Item2;
                        //    if (MTIntrude == true)
                        //        break;
                        //    else if (i == 1 && MTIntrude == false)
                        //        throw new Exception("Open Stage not allowed to be MT intrude!!");
                        //}
                        //mt.RobotMoving(true);
                        //mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                        //mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                        //mt.Unclamp();
                        //mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                        //mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToOS.json");
                        //mt.Clamp(1);
                        //mt.ExePathMove(@"D:\Positions\MTRobot\OSToLPHome.json");
                        //mt.RobotMoving(false);
                        //for (int i = 0; i < 2; i++)
                        //{
                        //    MTIntrude = os.ReadRobotIntrude(false, false).Item2;
                        //    if (i == 1 && MTIntrude == true || os.ReadBeenIntruded() == true)
                        //        throw new Exception("Open Stage has been MT intrude,can net execute command!!");
                        //}

                        //mt.RobotMoving(true);
                        //mt.ChangeDirection(@"D:\Positions\MTRobot\LoadPortHome.json");
                        //mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                        //mt.Unclamp();
                        //mt.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                        //mt.ExePathMove(@"D:\Positions\MTRobot\LPHomeToLP1.json");
                        //mt.Clamp(1);
                        //mt.ExePathMove(@"D:\Positions\MTRobot\LP1ToLPHome.json");
                        //mt.RobotMoving(false);

                        //ic.ReadRobotIntrude(false);
                        //ic.XYPosition(0, 0);
                        //ic.WPosition(0);
                        //ic.ReadRobotIntrude(true);
                        //mt.ChangeDirection(@"D:\Positions\MTRobot\InspChHome.json");
                        //mt.ExePathMove(@"D:\Positions\MTRobot\ICHomeFrontSideToIC.json");
                        //mt.Unclamp();
                        //mt.ExePathMove(@"D:\Positions\MTRobot\ICFrontSideToICHome.json");
                        //ic.ReadRobotIntrude(false);
                        #endregion 20200908 version
                    }

                }
            }
            catch (InvalidOperationException oex) { }
            catch (Exception ex) { throw ex; }
        }

        private void OpemStageTest()
        {
            uint BoxType;
            if (txtBoxType.Text != "1" && txtBoxType.Text != "2")
            { MessageBox.Show("Box Type請輸入數字1或2"); return; }
            else
                BoxType = Convert.ToUInt32(txtBoxType.Text);
            bool BTIntrude = false;
            bool MTIntrude = false;
            BoxTransferWorkTimes = 0;
            int Times = 0, CycleTimes = 0;
            if (int.TryParse(txtBTCycleTimes.Text, out CycleTimes))
            { CycleTimes = Convert.ToInt32(txtBTCycleTimes.Text); }
            else
            { MessageBox.Show("循環次數請輸入數字!!!"); return; }
            try
            {
                btnBTStart.Enabled = false;
                btnBTEnd.Enabled = true;
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                    var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    mt.HalConnect();
                    bt.HalConnect();
                    os.HalConnect();

                    if (true)
                    {
                        //os.ReadRobotIntrude(false, false);
                        os.Initial();
                        //mt.Initial();
                        bt.Initial();
                    }
                    for (Times = 1; Times <= CycleTimes; Times++)
                    {
                        BoxTransferWorkTimes = Times;
                        BT_worker.ReportProgress(Times);
                        Thread.Sleep(1000);

                        // Unlock & Open
                        for (int i = 0; i < 2; i++)
                        {
                            BTIntrude = os.ReadRobotIntrude(false, false).Item1;
                            if (i == 1 && BTIntrude == true || os.ReadBeenIntruded() == true)
                                throw new Exception("Open Stage has been BT intrude,can net execute command!!");
                        }
                        os.SetBoxType(BoxType);
                        os.SortClamp();
                        Thread.Sleep(1000);
                        os.SortUnclamp();
                        os.SortClamp();
                        Thread.Sleep(1000);
                        os.Vacuum(true);
                        os.SortUnclamp();
                        os.Lock();
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


                        // Close & Lock
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
                    }
                }
            }
            catch (InvalidOperationException ex) { }
            catch (Exception ex) { throw ex; }
        }

        //如果中途按下取消鍵
        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (worker.IsBusy)
            {
                worker.CancelAsync();
            }
            btnStart.Enabled = true;
            btnEnd.Enabled = false;
        }

        //處理進度條更新
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtTimes.Text = MaskTransferWorkTimes.ToString();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("您取消了操作!");
            }
            else if (e.Error != null)
            {
                MessageBox.Show("出现错误!");
            }
            else
            {
                MessageBox.Show("執行完成! 花費時間:" + DateDiff(DateTime.Now, MTStartTime));
            }
            btnStart.Enabled = true;
            btnEnd.Enabled = false;
        }

        private void TabPageDrawerAndLoadPort_Click(object sender, EventArgs e)
        {

        }

        private void FrmTestUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                loadPorts.LoadPort1.ClientSocket.Close();
            }
            catch (Exception exx)
            {

            }
            try
            {
                loadPorts.LoadPort2.ClientSocket.Close();
            }
            catch (Exception ex)
            {

            }

        }

        private void FrmTestUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                loadPorts.LoadPort1.ClientSocket.Close();
            }
            catch (Exception exx)
            {

            }
            try
            {
                loadPorts.LoadPort2.ClientSocket.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnInsp_Click(object sender, EventArgs e)
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(Insp_work);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            worker.RunWorkerAsync();
        }

        private void Insp_work(object sender, DoWorkEventArgs e)
        {
            Cancelable_DoWork work = new Cancelable_DoWork(InspSide);

            //開始非同步執行(MaskMove)方法 
            IAsyncResult rmm = work.BeginInvoke(null, null);

            //(非同步模式，在執行一個很耗時的方法(MaskMove)時,還能繼續向下執行程式) 

            //執行下面的While，判斷非同步操作是否完成 
            while (!rmm.IsCompleted)
            {
                //還沒完成，判斷是否取消了backgroundworker非同步操作 
                if (worker.CancellationPending)
                {
                    //如果是，馬上取消backgroundwork操作(這個地方才是真正取消非同步執行) 
                    e.Cancel = true;
                    return;
                }
            }
            //e.Result = work.EndInvoke(rmm); //返回查询结果 赋值给e.Result 
        }

        private void InspSide()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                ic.HalConnect();
                if (true)
                {
                    ic.ReadRobotIntrude(false);
                    ic.Initial();
                }
                ic.XYPosition(246, 208);
                for (int i = 0; i < 360; i += 90)
                {
                    ic.WPosition(i);
                    ic.Camera_SideInsp_CapToSave("D:/Image/IC/SideInsp", "bmp");
                    Thread.Sleep(500);
                }
                ic.XYPosition(0, 0);
            }
        }

        private void btnBTStart_Click(object sender, EventArgs e)
        {
            BT_worker = new BackgroundWorker();
            BT_worker.WorkerReportsProgress = true;
            BT_worker.WorkerSupportsCancellation = true;
            BT_worker.DoWork += new DoWorkEventHandler(BT_do_work);
            BT_worker.ProgressChanged += new ProgressChangedEventHandler(BT_worker_ProgressChanged);
            BT_worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BT_worker_RunWorkerCompleted);

            BT_worker.RunWorkerAsync();
        }

        private void BT_do_work(object sender, DoWorkEventArgs e)
        {
            Cancelable_DoWork work = new Cancelable_DoWork(OpemStageTest);

            //開始非同步執行(MaskMove)方法 
            IAsyncResult rmm = work.BeginInvoke(null, null);

            //(非同步模式，在執行一個很耗時的方法(MaskMove)時,還能繼續向下執行程式) 

            //執行下面的While，判斷非同步操作是否完成 
            while (!rmm.IsCompleted)
            {
                //還沒完成，判斷是否取消了backgroundworker非同步操作 
                if (BT_worker.CancellationPending)
                {
                    //如果是，馬上取消backgroundwork操作(這個地方才是真正取消非同步執行) 
                    e.Cancel = true;
                    return;
                }
            }
            //e.Result = work.EndInvoke(rmm); //返回查询结果 赋值给e.Result 
        }

        //處理進度條更新
        private void BT_worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtBTTimes.Text = BoxTransferWorkTimes.ToString();
        }

        private void BT_worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("您取消了操作!");
            }
            else if (e.Error != null)
            {
                MessageBox.Show("出现错误!");
            }
            else
            {
                MessageBox.Show("執行完成!");
            }
            btnBTStart.Enabled = true;
            btnBTEnd.Enabled = false;
        }

        private void btnBTEnd_Click(object sender, EventArgs e)
        {
            if (BT_worker.IsBusy)
            {
                BT_worker.CancelAsync();
            }
            btnBTStart.Enabled = true;
            btnBTEnd.Enabled = false;
        }
    }

}
