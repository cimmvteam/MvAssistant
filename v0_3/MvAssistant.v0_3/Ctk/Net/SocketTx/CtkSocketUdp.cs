using MvaCToolkitCs.v1_2.Protocol;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MvaCToolkitCs.v1_2.Net.SocketTx
{
    public class CtkSocketUdp : CtkSocket
    {


        public override Socket CreateSocket() { return new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); }

    }

}
