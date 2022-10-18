using CToolkitCs.v1_2.Protocol;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace CToolkitCs.v1_2.Net.SocketTx
{
    public class CtkSocketTcp : CtkSocket
    {

        ~CtkSocketTcp() { this.Dispose(false); }
    }

}
