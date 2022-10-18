using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer
{
    public class UdpServerSocket
    {

        UdpClient UdpClient = null;
        Thread ListenThread = null;
        public event EventHandler OnReceiveMessage = null;
        public UdpServerSocket(int localPort)
        {
            UdpClient = new UdpClient(localPort);
            ListenThread = new Thread(ListenMessage);
            ListenThread.IsBackground = true;
            ListenThread.Start();
        }
        public void ListenMessage()
        {
            IPEndPoint IpFrom = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {

                var rcvMessage = System.Text.Encoding.UTF8.GetString(UdpClient.Receive(ref IpFrom));
                OnReciveMessageEventArgs args = new OnReciveMessageEventArgs
                {
                    IP = IpFrom.Address.ToString(),
                    Message = rcvMessage
                };
                if (OnReceiveMessage != null)
                {
                    OnReceiveMessage.Invoke(this, args);
                }

            }
        }



    }

}
