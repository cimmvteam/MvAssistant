using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using CToolkit.v1_1.Protocol;

namespace SensingNet.v0_2.DvcSensor.Protocol
{
    public class SNetProtoFormatSNetCmd : ISNetProtoFormatBase, IDisposable
    {
        public ConcurrentQueue<string> MsgQueue = new ConcurrentQueue<string>();
        StringBuilder receivingString = new StringBuilder();



        void ReceiveBytes(byte[] buffer, int offset, int length)
        {
            lock (this)
            {
                this.receivingString.Append(Encoding.UTF8.GetString(buffer, offset, length));
                var content = this.receivingString.ToString();
                for (var idx = content.IndexOf('\n'); idx >= 0; idx = content.IndexOf('\n'))
                {
                    var line = content.Substring(0, idx + 1);
                    line = line.Replace("\r", "");
                    line = line.Replace("\n", "");
                    line = line.Trim();
                    if (line.Contains("cmd"))
                        this.MsgQueue.Enqueue(line);
                    content = content.Remove(0, idx + 1);
                }
                this.receivingString.Clear();
                this.receivingString.Append(content);
            }
        }

        #region ISNetProtoFormatBase
        int ISNetProtoFormatBase.Count() { return this.MsgQueue.Count; }
        public bool HasMessage() { return this.MsgQueue.Count > 0; }

        public bool IsReceiving() { return this.receivingString.Length > 0; }

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
            string line = null;
            var flag = this.MsgQueue.TryDequeue(out line);
            msg = line;

            System.Diagnostics.Debug.WriteLine(msg);

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
