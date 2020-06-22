using MvAssistant.DeviceDrive.KjMachineDrawer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MvAssistant.DeviceDrive.KjMachineDrawer.Drawer;

namespace DrawerTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //  UdpClient.SendTo(Encoding.UTF8.GetBytes("~012,BrightLED,3@"), TargetEndpoint);
            drawer.Send("~012,BrightLED,3@");
        }

        Drawer drawer = null;
        MvKjMachineDrawerLdd ldd;
        private void Form1_Load(object sender, EventArgs e)
        {
            ldd = new MvKjMachineDrawerLdd(5000,5999,"192.168.0.14",6000);
            var deviceEndPoint = new IPEndPoint(IPAddress.Parse("192.168.0.42"), 5000);
            var localEndPoint = new IPEndPoint(IPAddress.Parse("192.168.0.14"), 6000);
           // drawer = ldd.CreateDrawer(1, "", deviceEndPoint, localEndPoint);
           // drawer.OnReplyBrightLEDHandler += this.OnReplyBrightLED;

            // UdpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            // UdpClient.Bind(new IPEndPoint(IPAddress.Parse("192.168.0.14"), 6000));
            //Task.Run(
            //    () =>
            //    {
            //        //System.Net.Sockets.UdpClient udpClient = new System.Net.Sockets.UdpClient(new IPEndPoint(IPAddress.Parse("192.168.0.14"), 6000));
            //        //var ipep = new IPEndPoint(IPAddress.Any, 0);
            //        while (true)
            //        {
            //            var buffer = new byte[1024];
            //            UdpClient.Receive(buffer);

            //            var msg = Encoding.UTF8.GetString(buffer);
            //        }
            //    }
            //    );
        }

        private void OnReplyBrightLED(object sender, EventArgs args)
        {
            var drawer = (Drawer)sender;
            var eventArgs = (OnReplyBrightLEDEventArgs)args;
            var result = eventArgs.ReplyResultCode;
            if (result == ReplyResultCode.Set_Successfully)
            {

            }
            else //if (result == ReplyResultCode.Failed)
            {

            }
                
        }
    }
}
