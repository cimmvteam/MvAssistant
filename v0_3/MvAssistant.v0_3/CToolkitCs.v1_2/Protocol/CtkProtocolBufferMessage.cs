using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Protocol
{
    public class CtkProtocolBufferMessage
    {
        public CtkProtocolBufferMessage() { }
        public CtkProtocolBufferMessage(int bufferSize)
        {
            this.Buffer = new byte[bufferSize];
        }

        public byte[] Buffer = new byte[1024];
        public int Offset;
        public int Length;


        public string GetString(Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            return encoding.GetString(this.Buffer, this.Offset, this.Length);
        }


        public static implicit operator CtkProtocolBufferMessage(Byte[] data) { return new CtkProtocolBufferMessage() { Buffer = data, Offset = 0, Length = data.Length }; }
        public static implicit operator Byte[](CtkProtocolBufferMessage data) { return data.Buffer; }
    }
}
