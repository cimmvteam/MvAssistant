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
using MvAssistant.MaskTool_v0_1.SocketComm;

namespace BoxTransferTest
{
    public partial class Form1 : Form
    {
        BoxRobotHandler robotHandler;
        RobotMotionInfo motionInfo;
        SynchronizationContext _syncContext = null;
        AsynchronousClient client;
        bool socketConnectFlag = false;

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
            robotHandler.ldd.ExecutePNS("PNS0101");
            float[] target = new float[7];
            target[0] = float.Parse(textBox1.Text);
            target[1] = float.Parse(textBox2.Text);
            target[2] = float.Parse(textBox3.Text);
            target[3] = float.Parse(textBox4.Text);
            target[4] = float.Parse(textBox5.Text);
            target[5] = float.Parse(textBox6.Text);
            target[6] = float.Parse(textBox7.Text);
            robotHandler.ExecuteMove(target);
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
                listBox1.Items.Add("Exception:"+ex.StackTrace.ToString());
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
            string port = textBox11.Text;
        }

        private void SendMsgToDrawer(object sender, EventArgs e)
        {
            string msg = textBox10.Text;
            ///TO DO:撰寫Socket Host端
        }

        //void UpdateRcvMsg()
        //{
        //    while(socketConnectFlag)
        //    {
        //        AsynchronousClient.RcvMsg();
        //        Control_UpdateCallBack(listBox1, )
        //    }

        //}

        delegate void SetTextCallback(Control ctrl, object obj);
        void Control_UpdateCallBack(Control ctrl, object obj)
        { //31行
            if (ctrl == null || obj == null) return;//判斷若 參數都是null return
            if (ctrl.InvokeRequired) //判斷是否是自己建立的Thrad若是，就進入if內了
            {
                SetTextCallback d = new SetTextCallback(Control_UpdateCallBack);//委派把自己傳進來，所以會在執行這個方法第二次
                                                                                //所以第一次近來跑到地38行結束後，他會再從31行進來再跑一次，這時已經是第2次了，所以會直接掉入else去做Add的動作
                                                                                //每次回圈都Call這個方法，然後每次這個方法他會跑兩次，第二次才會去做Add的動作
                this.Invoke(d, new object[] { ctrl, obj }); //38行
            }
            else
            {
                if (ctrl == listBox1)
                {
                    listBox1.Items.Add(obj);
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                }
            }
        }
    }
}
