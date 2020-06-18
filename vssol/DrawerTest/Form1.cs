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

            UdpClient.SendTo(Encoding.UTF8.GetBytes("~012,BrightLED,3@"), TargetEndpoint);
        }
        IPEndPoint TargetEndpoint;
        Socket UdpClient = null;

        Drawer drawer = null;
        MvKjMachineDrawerLdd ldd;
        private void Form1_Load(object sender, EventArgs e)
        {
            ldd = new MvKjMachineDrawerLdd();
            drawer =ldd.CreateDrawer(1, "", "192.168.0.42", 5000);
            TargetEndpoint = new IPEndPoint(IPAddress.Parse("192.168.0.42"), 5000);
            UdpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            UdpClient.Bind(new IPEndPoint(IPAddress.Parse("192.168.0.14"), 6000));
            Task.Run(
                () =>
                {
                    //System.Net.Sockets.UdpClient udpClient = new System.Net.Sockets.UdpClient(new IPEndPoint(IPAddress.Parse("192.168.0.14"), 6000));
                    //var ipep = new IPEndPoint(IPAddress.Any, 0);
                    while (true)
                    {
                        var buffer = new byte[1024];
                        UdpClient.Receive(buffer);

                        var msg = Encoding.UTF8.GetString(buffer);
                    }
                }
                );
        }
    }
}
