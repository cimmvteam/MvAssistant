﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer
{
    public class DrawerSocket
    {

        /// <summary>Send(Target IP)</summary>
       // public string ClientIP { get; set; }
        //public int ClientPort { get; set; }
        private UdpClientSocket UdpClient { get; set; }
        public DrawerSocket(string serverIP, int serverPort)
        {
            UdpClient = new UdpClientSocket(serverIP, serverPort);
        }
        public int SentTo(string message)
        {
            var feedBack = UdpClient.SenTo(message);
            return feedBack;
        }
    }
}