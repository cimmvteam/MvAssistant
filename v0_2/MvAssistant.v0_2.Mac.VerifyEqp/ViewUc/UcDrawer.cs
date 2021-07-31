using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MvAssistant.v0_2.DrawerSocketTest;
using MvAssistant.v0_2.Threading;
using System.Threading;

namespace MvAssistantMacVerifyEqp.ViewUc
{
    public partial class UcDrawer : UserControl
    {
       


        UDPSocket udpServer1;
        UDPSocket udpClient1;
        MvaTask task;
        string dr_logData;
        public UcDrawer()
        {
            InitializeComponent();
        }

        private void UcDrawer_Load(object sender, EventArgs e)
        {
          
        }

        private void DR_ConnectBtn_Click(object sender, EventArgs e)
        {
            udpServer1 = new UDPSocket(JOBTYPE.RECVDTA, DR_IPAddressBox.Text, Convert.ToInt32(DR_ListenPortBox.Text));
            udpClient1 = new UDPSocket(JOBTYPE.SENDDATA, DR_IPAddressBox.Text, Convert.ToInt32(DR_SendPortBox.Text));
            udpServer1.RcvMsgEvent += DrawerEventHandler;
            udpClient1.RcvMsgEvent += DrawerEventHandler;
            Thread listenThread = new Thread(udpServer1.Listen);
            listenThread.Start();
        }

        public void DrawerEventHandler(string msg)
        {
            dr_logData = msg;
            DrawerMsgUpdate();
        }

        public void DrawerMsgUpdate()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(DrawerMsgUpdate));

            }
            else
            {
                LogWrite(dr_logData);
            }
        }

        void LogWrite(string msg)
        {
            var now = DateTime.Now;
            this.DR_Log.AppendText(string.Format("{0} {1}\r\n", now.ToString("yyyyMMdd HH:mm:ss"), msg));
        }

        void TaskClose()
        {
            if (this.task != null)
            {
                using (var obj = this.task)
                    obj.Cancel();
                this.task = null;
            }
        }

        private void DR_MsgSendBtn_Click(object sender, EventArgs e)
        {
            udpClient1.Send(DR_SendedMsg.Text);
        }
    }
    
}
