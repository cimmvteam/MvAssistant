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
            robotHandler.StartSgsVerify();


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
            if (robotHandler.ConnectIfNO() == 0)
            {
                this.LogWrite("Connection Success");
            }
            else
            {
                this.LogWrite("Connection Fail");
                this.robotHandler.Close();
                return;
            }

           //var currPos = robotHandler.getCurrentPOS();


        }

        void LogWrite(string msg)
        {
            var now = DateTime.Now;
            this.rtbLog.AppendText(string.Format("{0} {1}", now.ToString("yyyyMMdd HH:ii:ss"), msg));
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (robotHandler == null) return;
            robotHandler.ldd.StopProgram();


            if (robotHandler.ldd.AlarmReset())
            {
                this.LogWrite("Reset Done");
            }
            else
            {
                this.LogWrite("Reset Fail");
                return;
            }



            robotHandler.ldd.ExecutePNS("PNS0101");




        }
    }
}
