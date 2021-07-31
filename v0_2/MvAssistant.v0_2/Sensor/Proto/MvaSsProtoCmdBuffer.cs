using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using CToolkit.v1_1.Net;
using CToolkit.v1_1.Protocol;

namespace MvAssistant.v0_2.Sensor.Proto

{
    public class MvaSsProtoCmdBuffer : IDisposable
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
        public int Count() { return this.MsgQueue.Count; }
        public CtkProtocolTrxMessage CreateAckMsg(IList<MvaSsProtoCmdSvidCfg> reqSvids)
        {
            var result = new StringBuilder();
            result.Append("cmd  \n");
            return result.ToString();
        }

        public CtkProtocolTrxMessage CreateDataReqMsg(IList<MvaSsProtoCmdSvidCfg> reqSvids)
        {

            var result = new StringBuilder();
            result.Append("cmd -reqData -svid ");

            foreach (var svid in reqSvids)
                result.Append($" {svid.Svid}");

            result.Append("\n");//.AppendLine();

            return result.ToString();
        }

        public void FirstConnect(CtkTcpClient protoConn)
        {
        }

        public bool HasMessage() { return this.MsgQueue.Count > 0; }

        public bool IsReceiving() { return this.receivingString.Length > 0; }

        public List<MvaSsProtoCmdMsg> Parse(string msg)
        {
            var rtn = new List<MvaSsProtoCmdMsg>();

            var line = msg as string;

            var ea = new MvaSsProtoCmdMsg();
            var args = line.Split(new char[] { '\0', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            ea.Data = new List<double>();

            UInt32 val = 0;
            for (int idx = 0; idx < args.Length; idx++)
            {
                var arg = args[idx];


                if (string.Compare(args[idx], $"-RespData", true) == 0
                    || string.Compare(args[idx], "-resp_data", true) == 0)
                {
                    ea.ProtoCmdCat = EnumMvaSsProtoCmdCat.RespData;
                    continue;
                }
                else if (string.Compare(args[idx], "-svid", true) == 0
                    || string.Compare(args[idx], "-channel", true) == 0)
                {
                    idx++;
                    if (args.Length <= idx) continue;

                    if (UInt32.TryParse(args[idx], out val))
                        ea.Svid = val;
                    continue;
                }
                else if (args[idx] == "-data")
                {
                    idx = this.ReadData(args, idx, ea.Data);
                    continue;
                }
            }

            rtn.Add(ea);
            return rtn;
        }

        public bool ProcessSession(CtkTcpClient protoConn, object msg)
        {
            return false;
        }

        public void ReceiveMsg(CtkProtocolTrxMessage msg)
        {
            if (msg.Is<CtkProtocolBufferMessage>())
            {
                var buffer = msg.As<CtkProtocolBufferMessage>();
                this.ReceiveBytes(buffer.Buffer, buffer.Offset, buffer.Length);
            }
            else throw new ArgumentException("Not support type");
        }

        public bool TryDequeueMsg(out string msg)
        {
            string line = null;
            var flag = this.MsgQueue.TryDequeue(out line);
            msg = line;

            System.Diagnostics.Debug.WriteLine(msg);

            return flag;
        }





        int ReadData(String[] args, int start, List<double> data)
        {
            //讀取資料, 皆為double, 否則視為結束
            //return 最後一筆資料的索引

            var d = 0.0;
            //第一筆為 -data
            int idx = 0;
            for (idx = start + 1; idx < args.Length; idx++)
            {
                if (Double.TryParse(args[idx], out d))
                    data.Add(d);
                else
                    break;
            }

            return idx - 1;
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
