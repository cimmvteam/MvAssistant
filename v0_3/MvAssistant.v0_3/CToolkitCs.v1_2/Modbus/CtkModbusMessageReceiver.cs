using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Modbus
{
    public class CtkModbusMessageReceiver : Queue<CtkModbusMessage>
    {

        public bool isResponse = true;//預設Receive為Response

        protected List<byte> msgBuffer = new List<byte>();

        public virtual void Receive(byte[] buf)
        {
            msgBuffer.AddRange(buf);
            this.AnalysisBuffer();
        }
        public virtual void Receive(byte[] buf, int offset, int length)
        {
            for (int idx = 0; idx < length; idx++)
                msgBuffer.Add(buf[offset + idx]);
            this.AnalysisBuffer();
        }

        public int GetMsgBufferLength() { return this.msgBuffer.Count; }


        bool AnalysisBuffer()
        {
            int cnt;
            for (cnt = 0; cnt < 99; cnt++)
            {
                if (!this.AnalysisBuffer_EnqueueIfCompleteMsg())
                    break;
            }


            return cnt > 0;
        }

        bool AnalysisBuffer_EnqueueIfCompleteMsg()
        {
            ushort length = 0;
            if (!CtkModbusMessage.GetMessageLength(this.msgBuffer, out length)) return false;

            if (this.msgBuffer.Count < length + 6) return false;


            //Copy 完整的 Msessage 到 buffer
            var buffer = new byte[length + 6];
            this.msgBuffer.CopyTo(0, buffer, 0, length + 6);
            //移除 Length+Data 的資料
            this.msgBuffer.RemoveRange(0, length + 6);
            lock (this)
                if (this.isResponse)
                    this.Enqueue(CtkModbusMessage.FromResponseBytes(buffer));
                else
                    this.Enqueue(CtkModbusMessage.FromRequestBytes(buffer));
            return true;

        }



    }
}
