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

        private void btnLoadPortADock_Click(object sender, EventArgs e)
        {
            this.loadPorts.ResetResult(loadPorts.LoadPort1);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort1);
            loadPorts.LoadPort1.CommandDockRequest();
        }

        private void btnLoadPortAUnDock_Click(object sender, EventArgs e)
        {
            this.loadPorts.ResetResult(loadPorts.LoadPort1);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort1);
            loadPorts.LoadPort1.CommandUndockRequest();
        }

        private void btnLoadPortBDock_Click(object sender, EventArgs e)
        {
            this.loadPorts.ResetResult(loadPorts.LoadPort2);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort2);
            loadPorts.LoadPort2.CommandDockRequest();
        }

        private void btnLoadPortBUnDock_Click(object sender, EventArgs e)
        {
            this.loadPorts.ResetResult(loadPorts.LoadPort2);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort2);
            loadPorts.LoadPort2.CommandUndockRequest();
        }

        private void btnMoveDrawerCOut_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerC);
            drawers.DisableDrawerComps(drawers.DrawerC);
            drawers.DrawerC.CommandTrayMotionOut();
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
    }

}
