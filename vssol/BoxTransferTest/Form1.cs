using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MvAssistant.MaskTool_v0_1.Robot;
using MvAssistant.MaskTool_v0_1.Drawer_SocketComm;

namespace BoxTransferTest
{
    public partial class Form1 : Form
    {
        BoxRobotHandler robotHandler;
        RobotMotionInfo motionInfo;
        SynchronizationContext _syncContext = null;
        AsynchronousClient client;
        UDPSocket udpServer1;
        UDPSocket udpClient1;
        bool socketConnectFlag = false;
        string drawerData;

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
            robotHandler.ldd.ExecutePNS("PNS0102");
        }

        private void LoadPortConnect(object sender, EventArgs e)
        {
            string ip = textBox9.Text;
            string port = textBox10.Text;
            client = new AsynchronousClient();
            try
            {
                //AsynchronousClient.StartClient(ip, Convert.ToInt32(port));
                //listBox1.Items.Add(AsynchronousClient.response);
                //socketConnectFlag = true;
                //ThreadStart ts = new ThreadStart(AsynchronousClient.RcvMsg);
                //Thread t = new Thread(ts);
                //t.Start();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void SendMsgToLP(object sender, EventArgs e)
        {
            string msg = textBox8.Text;
            ///TO DO:撰寫Socket Client端
            ///
            
        }

        private void DrawerConnect(object sender, EventArgs e)
        {
            string ip = textBox12.Text;
            int port = Convert.ToInt32(textBox11.Text);

            udpServer1 = new UDPSocket(JOBTYPE.RECVDTA,ip,6000);
            udpClient1 = new UDPSocket(JOBTYPE.SENDDATA, ip, port);
            udpServer1.RcvMsgEvent += DrawerEventHandler;
            udpClient1.RcvMsgEvent += DrawerEventHandler;
            Thread listenThread = new Thread(udpServer1.Listen);
            listenThread.Start();
        }

        public void DrawerEventHandler(string msg)
        {
            drawerData = msg;
            DrawerMsgUpdate();
        }

        

        private void SendMsgToDrawer(object sender, EventArgs e)
        {
            string msg = textBox13.Text;
            udpClient1.Send(msg);
        }

        public void DrawerMsgUpdate()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(DrawerMsgUpdate));

            }
            else
            {
                textBox15.Text += drawerData + Environment.NewLine;
                textBox15.SelectionStart = textBox15.TextLength;
                textBox15.ScrollToCaret();
            }
        }
    }
}
