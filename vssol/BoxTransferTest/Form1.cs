using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MvAssistant.MaskTool_v0_1.Robot;

namespace BoxTransferTest
{
    public partial class Form1 : Form
    {
        BoxRobotHandler robotHandler;
        RobotMotionInfo motionInfo;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            robotHandler = new BoxRobotHandler();
            motionInfo = new RobotMotionInfo();
        }

        private void RobotConnect(object sender, EventArgs e)
        {
            this.robotHandler.ldd.RobotIp = "192.168.0.51";
            int connectRes = robotHandler.ConnectIfNO();
            if (robotHandler != null)
            {
                robotHandler.ldd.StopProgram();
                robotHandler.ldd.AlarmReset();
            }
        }

        private void GetRobotPos(object sender, EventArgs e)
        {
            robotHandler.getCurrentPOS();
            textBox1.Text = robotHandler.curPos.CurrentX.ToString();
            textBox2.Text = robotHandler.curPos.CurrentY.ToString();
            textBox3.Text = robotHandler.curPos.CurrentZ.ToString();
            textBox4.Text = robotHandler.curPos.CurrentW.ToString();
            textBox5.Text = robotHandler.curPos.CurrentP.ToString();
            textBox6.Text = robotHandler.curPos.CurrentR.ToString();
            textBox7.Text = robotHandler.curPos.CurrentE1.ToString();
        }

        private void RobotStepMove(object sender, EventArgs e)
        {
            //robotHandler.ldd.ExecutePNS("PNS0101");
            //float[] target = new float[7];
            //target[0] = float.Parse(textBox1.Text);
            //target[1] = float.Parse(textBox2.Text);
            //target[2] = float.Parse(textBox3.Text);
            //target[3] = float.Parse(textBox4.Text);
            //target[4] = float.Parse(textBox5.Text);
            //target[5] = float.Parse(textBox6.Text);
            //target[6] = float.Parse(textBox7.Text);
            //robotHandler.ExecuteMove(target);
            robotHandler.ldd.ExecutePNS("PNS0100");
        }

        private void LoadPortConnect(object sender, EventArgs e)
        {
            string ip = textBox9.Text;
            string port = textBox10.Text;


        }

        private void SendMsgToLP(object sender, EventArgs e)
        {
            string msg = textBox8.Text;
            ///TO DO:撰寫Socket Host端
        }

        private void DrawerConnect(object sender, EventArgs e)
        {
            string ip = textBox12.Text;
            string port = textBox11.Text;
        }

        private void SendMsgToDrawer(object sender, EventArgs e)
        {
            string msg = textBox10.Text;
            ///TO DO:撰寫Socket Host端
        }
    }
}
