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
        public FrmTestUI()
        {
            InitializeComponent();
        }

        private void FrmTestUI_Load(object sender, EventArgs e)
        {
            
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void txtBoxType_Click(object sender, EventArgs e)
        {
            txtBoxType.Text = "";
        }

        private void txtBoxType_TextChanged(object sender, EventArgs e)
        {
            if (txtBoxType.Text != "1" && txtBoxType.Text != "2")
                MessageBox.Show("請輸入數字1或2");
        }

        private void BTGetDR_0204_Click(object sender, EventArgs e)
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

        private void Unlock_Click(object sender, EventArgs e)
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
    }
}
