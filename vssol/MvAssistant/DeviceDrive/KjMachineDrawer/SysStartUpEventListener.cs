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


    public  class SysStartUpEventListener
   {
        private int Port;
        private Thread ListenThread;
        public UdpClient UdpClient;
        private IPEndPoint IpEndPoint;
        private SysStartUpEventListener()
        {
            ListenThread = new Thread(EventListener);
            ListenThread.IsBackground = true;
        }

        public SysStartUpEventListener(int port) : this()
        {
            this.Port = port;
           
        }
        public void Listen(DelOnRcvMessage onReciveMessageCallback)
        {
            this.OnRcvMessageCallBack = onReciveMessageCallback;
            ListenThread.Start();
        }
        public void EventListener()
        {
           
                IpEndPoint = new IPEndPoint(IPAddress.Any, Port);
                UdpClient = new UdpClient(IpEndPoint.Port);
                while (true)
                {
                   try
                   {
                    var rcvMessage = System.Text.Encoding.UTF8.GetString(UdpClient.Receive(ref IpEndPoint));
                    OnRcvMessage(rcvMessage, IpEndPoint);
                   }
                   catch(Exception ex)
                   {

                   }
                }
           
        }

        public void OnRcvMessage(string message,IPEndPoint ipEndpoint)
        {
            var ip = ipEndpoint.Address;
            if(OnRcvMessageCallBack!=null)
            {
                OnRcvMessageCallBack(message, ipEndpoint);
            }


        }
        public delegate void DelOnRcvMessage(string message, IPEndPoint ipEndpoint);

        public DelOnRcvMessage OnRcvMessageCallBack = null;
    }
}
