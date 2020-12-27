using CToolkit.v1_1.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace SensingNet.v0_2.DvcSensor.Protocol
{
    /// <summary>
    /// 處理Protocol Format相關功能
    /// </summary>
    public interface ISNetProtoFormatBase
    {

        void ReceiveMsg(CtkProtocolTrxMessage msg);
        bool IsReceiving();
        bool HasMessage();
        bool TryDequeueMsg(out object msg);
        int Count();






    }
}