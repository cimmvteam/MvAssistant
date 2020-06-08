using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer
{
    public class UdpClientSocket
    {
        IPEndPoint TargetEndpoint = null;
        Socket UdpClient = null;

        public UdpClientSocket(string clientIP, int clientPort)
        {
            TargetEndpoint = new IPEndPoint(IPAddress.Parse(clientIP), clientPort);
            UdpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        }
        public int SenTo(string message)
        {
            var feedBack = UdpClient.SendTo(Encoding.UTF8.GetBytes(message), TargetEndpoint);
            return feedBack;
        }

    }
}
