using MvaCToolkitCs.v1_2.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeExpress.v1_1Core.Secs
{
    public class CxHsmsMessageReceiver : Queue<CxHsmsMessage>
    {

        public List<byte> DataBuffer = new List<byte>();
        /// <summary> Default=true : 每次 Receive 後自動 Parse, 減少使用者理解時間 </summary>
        public bool IsAutoParse = true;

        public int GetDataBufferLength() { return this.DataBuffer.Count; }

        public int ParseMessage()
        {
            //一次最多分析99個訊息.
            //讓Thread可以出去執行別的任務.
            //基本上, 訊息不應該快速累積, 也不應該沒處理
            int cnt;
            for (cnt = 0; cnt < 99; cnt++)
            {
                if (!this.ParseMessage_OneMsg())
                    break;
            }
            return cnt;
        }

        public virtual void Receive(byte[] buffer)
        {
            lock (this)
                DataBuffer.AddRange(buffer);
            if (this.IsAutoParse) this.ParseMessage();
        }
        public virtual void Receive(byte[] buffer, int offset, int length)
        {
            var mybuffer = new Byte[length];
            Array.Copy(buffer, offset, mybuffer, 0, length);
            this.Receive(mybuffer);
        }
        public void Receive(CtkProtocolBufferMessage buffer)
        {
            var mybuffer = new Byte[buffer.Length];
            Array.Copy(buffer.Buffer, buffer.Offset, mybuffer, 0, buffer.Length);
            this.Receive(mybuffer);
        }
        protected bool ParseMessage_OneMsg()
        {
            lock (this)
            {
                //前 4-byte 是長度資訊
                if (this.DataBuffer.Count < 4) return false;

                Int32 dataLength = DataBuffer[0];
                //big to little-endian
                dataLength = (dataLength << 8) + DataBuffer[1];//位移 8-bit = 1-byte
                dataLength = (dataLength << 8) + DataBuffer[2];
                dataLength = (dataLength << 8) + DataBuffer[3];

                //總長度 = 4-byte 長度資訊 + 資料長度
                if (this.DataBuffer.Count >= dataLength + 4)
                {
                    //Copy 完整的 Msessage 到 buffer
                    var buffer = new byte[dataLength];
                    this.DataBuffer.CopyTo(4, buffer, 0, dataLength);
                    //移除 Length+Data 的資料
                    this.DataBuffer.RemoveRange(0, 4 + dataLength);
                    lock (this)
                        this.Enqueue(CxHsmsMessage.GetFromBytes(buffer));
                    return true;
                }

                return false;
            }
        }




    }
}
