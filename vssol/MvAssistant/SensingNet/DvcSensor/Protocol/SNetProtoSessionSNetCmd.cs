using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensingNet.v0_2.DvcSensor.Protocol
{
    public class SNetProtoSessionSNetCmd : ISNetProtoSessionBase
    {

        public bool ProcessSession(ISNetProtoConnectBase protoConn, object msg)
        {
            return false;
        }

        public void FirstConnect(ISNetProtoConnectBase protoConn)
        {
        }




    }
}
