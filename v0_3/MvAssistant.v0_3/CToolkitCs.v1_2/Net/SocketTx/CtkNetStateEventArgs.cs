using CToolkitCs.v1_2.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace CToolkitCs.v1_2.Net.SocketTx
{
    public class CtkNetStateEventArgs : CtkProtocolEventArgs
    {
        public Object WorkClient;
        public CtkProtocolBufferMessage TrxMessageBuffer
        {
            get
            {
                if (this.TrxMessage == null) this.TrxMessage = new CtkProtocolBufferMessage();
                if (!this.TrxMessage.Is<CtkProtocolBufferMessage>()) throw new InvalidOperationException("TrxMessage is not Buffer");
                return this.TrxMessage.As<CtkProtocolBufferMessage>();
            }
            set { this.TrxMessage = value; }
        }

        public Socket WorkSocket { get { return this.WorkClient as Socket; } set { this.WorkClient = value; } }
        public TcpClient WorkTcpClient { get { return this.WorkClient as TcpClient; }  set { this.WorkClient = value; } }
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
