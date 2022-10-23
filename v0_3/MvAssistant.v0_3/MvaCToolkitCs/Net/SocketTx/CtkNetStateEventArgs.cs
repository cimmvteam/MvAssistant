using MvaCToolkitCs.v1_2.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MvaCToolkitCs.v1_2.Net.SocketTx
{
    public class CtkNetStateEventArgs : CtkProtocolEventArgs
    {
        public Object Target;
        public CtkProtocolBufferMessage TrxBuffer
        {
            get
            {
                if (this.TrxMessage == null) this.TrxMessage = new CtkProtocolBufferMessage();
                if (!this.TrxMessage.Is<CtkProtocolBufferMessage>()) throw new InvalidOperationException("TrxMessage is not Buffer");
                return this.TrxMessage.As<CtkProtocolBufferMessage>();
            }
            set { this.TrxMessage = value; }
        }

        /// <summary> 本次作業對象 </summary>
        public Socket TargetSocket { get { return this.Target as Socket; } set { this.Target = value; } }
        public TcpClient WorkTcpClient { get { return this.Target as TcpClient; }  set { this.Target = value; } }
        /// <summary> 本次接收資料的遠端位置 </summary>
        public EndPoint TargetEndPoint = new IPEndPoint(IPAddress.Any, 0);
  
        public void WriteMsg(byte[] buff, int offset, int length)
        {
            if (this.WorkTcpClient == null) return;
            if (!this.WorkTcpClient.Connected) return;

            var stm = this.WorkTcpClient.GetStream();
            stm.Write(buff, offset, length);

        }
        public void WriteMsg(byte[] buff, int length) { this.WriteMsg(buff, 0, length); }
        public void WriteMsg(String msg)
        {
            var buff = Encoding.UTF8.GetBytes(msg);
            this.WriteMsg(buff, 0, buff.Length);
        }
    }
}
