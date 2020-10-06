using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace SensingNet.v0_2.DvcSensor.Protocol
{
    /// <summary>
    /// 處理會談相關Message
    /// </summary>
    public interface ISNetProtoSessionBase
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="protoConn"></param>
        /// <param name="msg"></param>
        /// <returns>是否由Session所處理</returns>
        bool ProcessSession(ISNetProtoConnectBase protoConn, object msg);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="protoConn">並非所有通訊都是繼續自Stream, 因此請實作並傳入IProtoConnectBase</param>

        void FirstConnect(ISNetProtoConnectBase protoConn);






    }
}