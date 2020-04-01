using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaskTool.TestMy.Device;

namespace BoxTransferTest.ViewUc
{
    public partial class UcMaskRobot : UserControl
    {
        RobotHandler robotHandler;


        public UcMaskRobot()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {



        }

        private void btnStop_Click(object sender, EventArgs e)
        {

        }

        private void UcMaskRobot_Load(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            robotHandler = new RobotHandler();
            robotHandler.ldd.RobotIp = "192.168.0.50";
            int connectRes = robotHandler.ConnectIfNO();
            if (robotHandler != null)
            {
                robotHandler.ldd.StopProgram();
                robotHandler.ldd.AlarmReset();
            }
            robotHandler.ldd.ExecutePNS("PNS0101");

            robotHandler.getCurrentPOS();

        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {

        }
    }
}
