using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotTest
{
    public partial class Form1 : Form
    {
        RobotHandler robotHandler;
        RobotMotionInfo motionInfo;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int connectRes = robotHandler.ConnectIfNO();
            if (robotHandler != null)
            {
                robotHandler.ldd.StopProgram();
                robotHandler.ldd.AlarmReset();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            robotHandler = new RobotHandler();
            motionInfo = new RobotMotionInfo();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            robotHandler.getCurrentPOS();
            textBox1.Text = robotHandler.curPos.CurrentX.ToString();
            textBox2.Text = robotHandler.curPos.CurrentY.ToString();
            textBox3.Text = robotHandler.curPos.CurrentZ.ToString();
            textBox4.Text = robotHandler.curPos.CurrentW.ToString();
            textBox5.Text = robotHandler.curPos.CurrentP.ToString();
            textBox6.Text = robotHandler.curPos.CurrentR.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            robotHandler.ldd.ExecutePNS("PNS0101");
            float[] target = new float[6];
            target[0] = float.Parse(textBox1.Text);
            target[1] = float.Parse(textBox2.Text);
            target[2] = float.Parse(textBox3.Text);
            target[3] = float.Parse(textBox4.Text);
            target[4] = float.Parse(textBox5.Text);
            target[5] = float.Parse(textBox6.Text);
            robotHandler.ExecuteMove(target);
        }
    }
}
