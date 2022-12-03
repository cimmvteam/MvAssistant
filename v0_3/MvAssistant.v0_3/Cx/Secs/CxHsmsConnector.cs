using MvaCToolkitCs.v1_2.Net;
using MvaCToolkitCs.v1_2.Net.SocketTx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MvaCodeExpress.v1_1.Secs
{
    /// <summary>
    /// HSMS topic 處理, 不用Thread, 不重新連線, 讓使用者自行決定處理方式
    /// </summary>
    public class CxHsmsConnector : IDisposable
    {
        public CtkSocketTcp SocketTcp = new CtkSocketTcp();


        public CxHsmsMessageReceiver Receiver = new CxHsmsMessageReceiver();
        public Uri LocalUri { get { return this.SocketTcp.LocalUri; } set { this.SocketTcp.LocalUri = value; } }
        public Uri RemoteUri { get { return this.SocketTcp.RemoteUri; } set { this.SocketTcp.RemoteUri = value; } }

        public bool IsActively { get { return this.SocketTcp == null ? false : this.SocketTcp.IsActively; } set { if (this.SocketTcp != null) this.SocketTcp.IsActively = value; } }
        public bool IsWaitReceive { get { return this.SocketTcp.IsWaitReceive; } }

        //HSMS
        public int T3 = 45; //Reply timeout, Range 1-120
        public int T5 = 10;//Range 1-240
        public int T6 = 5;//Range 1-240
        public int T7 = 10;//Range 1-240
        public int T8 = 5;//Range 1-120



        public CxHsmsConnector()
        {
            CxHsmsMgr.CxSetup();
            this.SocketTcp.EhDataReceive += TcpSocket_EhDataReceive;
        }

        private void TcpSocket_EhDataReceive(object sender, MvaCToolkitCs.v1_2.Protocol.CtkProtocolEventArgs e)
        {
            var trxBuffer = e.TrxMessage.ToBuffer();
            if (trxBuffer.Length == 0) return;//disconnect
            this.Receiver.Receive(trxBuffer.Buffer, trxBuffer.Offset, trxBuffer.Length);

            while (this.Receiver.Count > 0)
            {
                var msg = this.Receiver.Dequeue();
                this.OnReceiveData(msg);
            }


            //var msgByteList = SplitMsg(trxBuffer.Buffer, 0, trxBuffer.Length);
            //List<CxHsmsMessage> msgList = new List<CxHsmsMessage>();
            //foreach (var buffer in msgByteList)
            //    msgList.Add(CxHsmsMessage.GetFromBytes(buffer));
            //foreach (var msg in msgList)
            //    this.OnReceiveData(msg);
        }

        ~CxHsmsConnector() { this.Dispose(false); }


        public int ConnectTry() { return this.SocketTcp.ConnectTry(); }
        public int ConnectTryStart() { return this.SocketTcp.ConnectTryStart(); }
        public int ReceiveLoop() { return this.SocketTcp.ReceiveLoop(); }
        public int ReceiveOnce() { return this.SocketTcp.ReceiveOnce(); }
        public void Disconnect() { this.SocketTcp.Disconnect(); }
        public void DissconnectIfUnReadable() { this.SocketTcp.DisconnectIfUnReadable(); }

        public void Send(CxHsmsMessage msg)
        {
            this.SocketTcp.WriteMsg(msg.ToBytes());
        }



        #region Receive Data

        public event EventHandler<CxHsmsConnectorRcvDataEventArg> EhReceiveData;
        public void OnReceiveData(CxHsmsMessage msg)
        {
            if (this.EhReceiveData == null)
                return;

            this.EhReceiveData(this, new CxHsmsConnectorRcvDataEventArg() { msg = msg });
        }

        [Obsolete("網路傳輸不會剛好是一個完整訊息")]
        public static List<byte[]> SplitMsg(byte[] msgs, int start, int end)
        {
            /*[d20221001] 此 function 預設 傳進 完整的 message, 不合常理.
             * 實際資料可以包含 2個以上的訊息, 或不到一個的 */

            var rs = new List<byte[]>();


            Int32 length = 0;
            for (int idx = start; idx < end; idx += 4 + length)
            {
                length = 0;

                //Big (SECS) to Little (Windows) endian
                for (int jdx = 0; jdx < 4; jdx++)
                    length = (length << 8) + msgs[idx + jdx];

                //轉成物件時, 不用再帶length
                var dataBytes = new byte[length];
                Array.Copy(msgs, idx + 4, dataBytes, 0, length);
                rs.Add(dataBytes);
            }

            return rs;
        }

        #endregion





        #region IDispose

        bool disposed = false;
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any managed objects here.
            }

            // Free any unmanaged objects here.
            this.DisposeSelf();
            disposed = true;
        }

        public virtual void DisposeSelf()
        {
            if (this.SocketTcp != null)
                this.SocketTcp.Dispose();
        }

        #endregion

    }
}
