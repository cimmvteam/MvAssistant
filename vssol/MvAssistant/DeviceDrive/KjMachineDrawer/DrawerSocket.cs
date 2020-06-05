using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer
{
    public class DrawerSocket
    {

        /// <summary></summary>
        private UdpClientSocket UdpClient { get; set; }

        /// <summary></summary>
        /// <param name="deviceIP">Device IP</param>
        /// <param name="udpServerPort">Udp Server Port</param>
        public DrawerSocket(string deviceIP, int udpServerPort)
        {
            UdpClient = new UdpClientSocket(deviceIP, udpServerPort);
        }

        /// <summary>傳送訊息</summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public int SentTo(string message)
        {
            var feedBack = UdpClient.SenTo(message);
            return feedBack;
        }
    }
}
