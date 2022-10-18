using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer
{
    /// <summary>接收到訊號回呼函式的委派型別</summary>
    /// <param name="message"></param>
    /// <param name="ipEndpoint"></param>
    public delegate void DelOnRcvMessage(string message, IPEndPoint ipEndpoint);

}
