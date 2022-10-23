using MvaCToolkitCs.v1_2.Protocol;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MvaCToolkitCs.v1_2.Net.SocketTx
{
    public class CtkSocketTcp : CtkSocket
    {

        ~CtkSocketTcp() { this.Dispose(false); }


        public override Socket CreateSocket() { return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);/*¹w³]*/ }
    }

}
