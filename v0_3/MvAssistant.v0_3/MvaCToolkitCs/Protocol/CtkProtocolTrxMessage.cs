using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvaCToolkitCs.v1_2.Protocol
{
    public class CtkProtocolTrxMessage
    {
        /// <summary>
        /// CtkProtocolBufferMessage, String, Byte[]
        /// </summary>
        public Object TrxMessage;


        public T As<T>() where T : class { return this.TrxMessage as T; }

        public string GetString(Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;

            var bufferMsg = this.As<CtkProtocolBufferMessage>();
            if (bufferMsg != null)
                return bufferMsg.GetString(encoding);

            if (this.TrxMessage is String)
                return this.TrxMessage as string;

            if (this.TrxMessage is byte[])
            {
                var buffer = this.TrxMessage as byte[];
                return encoding.GetString(buffer, 0, buffer.Length);
            }

            return null;
        }

        public bool Is<T>() { return this.TrxMessage is T; }
        public CtkProtocolBufferMessage ToBuffer(Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;

            var bufferMsg = this.As<CtkProtocolBufferMessage>();
            if (bufferMsg != null) return bufferMsg;

            var buffer = new byte[0];
            if (this.TrxMessage is String)
                buffer = encoding.GetBytes(this.TrxMessage as String);
            else if (this.TrxMessage is byte[])
                buffer = this.TrxMessage as byte[];
            else return null;


            bufferMsg = new CtkProtocolBufferMessage();
            bufferMsg.Buffer = buffer;
            bufferMsg.Offset = 0;
            bufferMsg.Length = buffer.Length;
            return bufferMsg;
        }




        #region Static

        public static CtkProtocolTrxMessage Create(Object msg) { return new CtkProtocolTrxMessage() { TrxMessage = msg }; }
        public static CtkProtocolTrxMessage Create(CtkProtocolTrxMessage msg) { return new CtkProtocolTrxMessage() { TrxMessage = msg.TrxMessage }; }
        public static CtkProtocolTrxMessage Create(byte[] msg, int offset, int length) { return new CtkProtocolBufferMessage() { Buffer = msg, Offset = offset, Length = length }; }


        public static implicit operator CtkProtocolTrxMessage(Byte[] msg) { return new CtkProtocolTrxMessage() { TrxMessage = msg }; }
        public static implicit operator CtkProtocolTrxMessage(String msg) { return new CtkProtocolTrxMessage() { TrxMessage = msg }; }
        public static implicit operator CtkProtocolTrxMessage(CtkProtocolBufferMessage msg) { return new CtkProtocolTrxMessage() { TrxMessage = msg }; }

        #endregion



    }
}
