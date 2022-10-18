using CodeExpress.v1_0.Secs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensingNet.v0_2.DvcSensor.Protocol
{
    public class SNetProtoSessionSecs : ISNetProtoSessionBase
    {
        /// <summary>
        /// 若是Session類訊息, 將由這個Method處理
        /// </summary>
        /// <param name="protoConn"></param>
        /// <param name="msg"></param>
        /// <returns>true代表己處理, 你不需再處理</returns>
        public bool ProcessSession(ISNetProtoConnectBase protoConn, object msg)
        {
            var secsMsg = msg as CxHsmsMessage;
            if (secsMsg == null) throw new ArgumentException("不正確的msg型態");

            switch (secsMsg.header.SType)
            {
                case 1:
                    protoConn.WriteMsg(CxHsmsMessage.CtrlMsg_SelectRsp(0));
                    return true;
                case 2:
                    return true;
                case 5:
                    protoConn.WriteMsg(CxHsmsMessage.CtrlMsg_LinktestRsp());
                    return true;
                case 6:
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="protoConn">並非所有通訊都是繼續自Stream, 因此請實作IProtoConnectBase</param>
        public void FirstConnect(ISNetProtoConnectBase protoConn)
        {
            var txMsg = CxHsmsMessage.CtrlMsg_SelectReq();
            protoConn.WriteMsg(txMsg);
            txMsg = CxHsmsMessage.CtrlMsg_LinktestReq();
            protoConn.WriteMsg(txMsg);
        }



    }
}
