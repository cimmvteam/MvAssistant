using CodeExpress.v1_0.Secs;
using CToolkit.v1_1.Protocol;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace SensingNet.v0_2.DvcSensor.Protocol
{

    /// <summary>
    /// 客戶要求的Secs Format
    /// </summary>
    public class SNetProtoFormatSecs : ISNetProtoFormatBase, IDisposable
    {

        public ConcurrentQueue<CxHsmsMessage> MsgQueue = new ConcurrentQueue<CxHsmsMessage>();

        CxHsmsMessageReceiver hsmsMsgRcv = new CxHsmsMessageReceiver();

        ~SNetProtoFormatSecs() { this.Dispose(false); }

        void ReceiveBytes(byte[] buffer, int offset, int length)
        {
            this.hsmsMsgRcv.Receive(buffer, offset, length);
            while (this.hsmsMsgRcv.Count > 0)
            {
                var msg = this.hsmsMsgRcv.Dequeue();
                this.MsgQueue.Enqueue(msg);
            }
        }

        #region ISNetProtoFormatBase

        int ISNetProtoFormatBase.Count() { return this.MsgQueue.Count; }

        public bool HasMessage() { return this.MsgQueue.Count > 0; }

        public bool IsReceiving() { return this.hsmsMsgRcv.GetMsgBufferLength() > 0; }

        public void ReceiveMsg(CtkProtocolTrxMessage msg)
        {
            if (msg.Is<CtkProtocolBufferMessage>())
            {
                var buffer = msg.As<CtkProtocolBufferMessage>();
                this.ReceiveBytes(buffer.Buffer, buffer.Offset, buffer.Length);
            }
            else throw new ArgumentException("Not support type");
        }
        public bool TryDequeueMsg(out object msg)
        {
            CxHsmsMessage mymsg = null;
            var flag = this.MsgQueue.TryDequeue(out mymsg);
            msg = mymsg;
            return flag;
        }
        #endregion



        #region IDisposable
        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            this.DisposeSelf();
            disposed = true;
        }





        void DisposeSelf()
        {

        }




        #endregion

    }
}
