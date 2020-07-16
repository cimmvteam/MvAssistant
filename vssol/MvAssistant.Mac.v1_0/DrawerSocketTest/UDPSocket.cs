using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace MvAssistant.Mac.DrawerSocketTest
{
    public class UDPSocket
    {
        IPEndPoint ipep;
        public int port { get; set; }
        public string ipAddress { get; set; }
        UdpClient udp;
        public JOBTYPE JobType;

        
        public UDPSocket(JOBTYPE jobtype,string ipAddress,int port)
        {
            if(jobtype==JOBTYPE.RECVDTA)
            {
                udp = new UdpClient(port);
            }
            else
            {
                udp = new UdpClient();
            }
            ipep = new IPEndPoint(IPAddress.Parse(ipAddress), port);
        }

        public event Action<string> RcvMsgEvent;

        public void Listen()
        {
            while(true)
            {
                if(udp.Available>0&&RcvMsgEvent!=null)
                {
                    byte[] B = udp.Receive(ref ipep);
                    RcvMsgEvent("RcvMsg:"+Encoding.UTF8.GetString(B));
                }
            }
        }

        public void Send(string msg)
        {
            byte[] b = Encoding.UTF8.GetBytes(msg);
            udp.Send(b, b.Length, ipep);
            if (RcvMsgEvent != null)
            {
                RcvMsgEvent("SendMsg:" + msg);
            }
        }
    }

    public enum JOBTYPE { SENDDATA, RECVDTA };
}
