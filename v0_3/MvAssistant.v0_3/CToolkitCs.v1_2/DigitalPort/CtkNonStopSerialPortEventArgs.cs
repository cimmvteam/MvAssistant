using CToolkitCs.v1_2.Protocol;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace CToolkitCs.v1_2.DigitalPort
{
    public class CtkNonStopSerialPortEventArgs : CtkProtocolEventArgs
    {
        public SerialPort SerialPort;

        public SerialData DataType;
        public SerialError ErrorType;

        public CtkProtocolBufferMessage TrxMessageBuffer
        {
            get
            {
                if (this.TrxMessage == null) this.TrxMessage = new CtkProtocolBufferMessage();
                else throw new InvalidOperationException("TrxMessage is not Buffer");
                return this.TrxMessage.As<CtkProtocolBufferMessage>();
            }
            set { this.TrxMessage = value; }
        }


        public void WriteMsg(byte[] buff, int offset, int length)
        {
            if (this.SerialPort == null) return;
            if (!this.SerialPort.IsOpen) return;
            this.SerialPort.Write(buff, offset, length);
        }
        public void WriteMsg(byte[] buff, int length) { this.WriteMsg(buff, 0, length); }
        public void WriteMsg(String msg)
        {
            var buff = Encoding.UTF8.GetBytes(msg);
            this.WriteMsg(buff, 0, buff.Length);
        }
    }
}
