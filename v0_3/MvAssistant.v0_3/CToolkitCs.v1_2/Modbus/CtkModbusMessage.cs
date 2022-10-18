using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace MvaCToolkitCs.v1_2.Modbus
{
    /// <summary>
    /// Modbus TCP common driver class. This class implements a modbus TCP master driver.
    /// It supports the following commands:
    /// 
    /// Read coils
    /// Read discrete inputs
    /// Write single coil
    /// Write multiple cooils
    /// Read holding register
    /// Read input register
    /// Write single register
    /// Write multiple register
    /// 
    /// All commands can be sent in synchronous or asynchronous mode. If a value is accessed
    /// in synchronous mode the program will stop and wait for slave to response. If the 
    /// slave didn't answer within a specified time a timeout exception is called.
    /// The class uses multi threading for both synchronous and asynchronous access. For
    /// the communication two lines are created. This is necessary because the synchronous
    /// thread has to wait for a previous command to finish.
    /// 
    /// </summary>
    public class CtkModbusMessage : IDisposable
    {
        // ------------------------------------------------------------------------
        // Constants for access
        public const byte fctReadCoil = 1;
        public const byte fctReadDiscreteInputs = 2;
        public const byte fctReadHoldingRegister = 3;
        public const byte fctReadInputRegister = 4;
        public const byte fctWriteSingleCoil = 5;
        public const byte fctWriteSingleRegister = 6;
        public const byte fctWriteMultipleCoils = 15;
        public const byte fctWriteMultipleRegister = 16;
        public const byte fctReadWriteMultipleRegister = 23;


        /// <summary>Constant for exception illegal function.</summary>
        public const byte excIllegalFunction = 1;
        /// <summary>Constant for exception illegal data address.</summary>
        public const byte excIllegalDataAdr = 2;
        /// <summary>Constant for exception illegal data value.</summary>
        public const byte excIllegalDataVal = 3;
        /// <summary>Constant for exception slave device failure.</summary>
        public const byte excSlaveDeviceFailure = 4;
        /// <summary>Constant for exception acknowledge.</summary>
        public const byte excAck = 5;
        /// <summary>Constant for exception slave is busy/booting up.</summary>
        public const byte excSlaveIsBusy = 6;
        /// <summary>Constant for exception gate path unavailable.</summary>
        public const byte excGatePathUnavailable = 10;
        /// <summary>Constant for exception not connected.</summary>
        public const byte excExceptionNotConnected = 253;
        /// <summary>Constant for exception connection lost.</summary>
        public const byte excExceptionConnectionLost = 254;
        /// <summary>Constant for exception response timeout.</summary>
        public const byte excExceptionTimeout = 255;
        /// <summary>Constant for exception wrong offset.</summary>
        public const byte excExceptionOffset = 128;
        /// <summary>Constant for exception send failt.</summary>
        public const byte excSendFailt = 100;




        // ------------------------------------------------------------------------
        /// <summary>Create master instance without parameters.</summary>
        public CtkModbusMessage()
        {
        }

        // ------------------------------------------------------------------------
        /// <summary>Create master instance with parameters.</summary>
        /// <param name="ip">IP adress of modbus slave.</param>
        /// <param name="port">Port number of modbus slave. Usually port 502 is used.</param>
        ~CtkModbusMessage()
        {
            this.Dispose(false);
        }



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





        public bool isResponse = false;

        public ushort transactionId;//high byte 1st ; low byte 2nd
        public ushort protocolId;//high byte 1st ; low byte 2nd
        public byte unitId;//Slave Address
        public byte funcCode;
        public ushort msgLength;//include unit id and function code

        public byte[] values = new byte[0];
        public ushort readAddress;//Start Address/Reference Number
        public ushort readLength;//Length of data
        public ushort writeAddress;//Start Address/Reference Number
        public ushort writeLength;//Length of data


        public byte[] ToRequestBytes()
        {
            //Big-Endian (same like SECS)
            var buffer = new List<byte>();
            var tids = BitConverter.GetBytes(transactionId);
            buffer.Add(tids[1]);//buffer[0]
            buffer.Add(tids[0]);//buffer[1]
            var pids = BitConverter.GetBytes(protocolId);
            buffer.Add(pids[1]);//buffer[2]
            buffer.Add(pids[0]);//buffer[3]


            //Note. 用 IPAddress.HostToNetworkOrder 可以調整順序
            var dataBytes = new List<byte>();
            dataBytes.Add(unitId);//buffer[6]
            dataBytes.Add(funcCode);//buffer[7]

            var datas = new byte[0];

            System.Diagnostics.Debug.Assert(this.funcCode > 0);//function code需要是有定義的值

            switch (funcCode)
            {
                case CtkModbusMessage.fctReadCoil:
                case CtkModbusMessage.fctReadDiscreteInputs:
                case CtkModbusMessage.fctReadHoldingRegister:
                case CtkModbusMessage.fctReadInputRegister:
                    System.Diagnostics.Debug.Assert(this.values.Length == 0);//不應該有值要寫入
                    this.ToBytes_AddReadWrite(dataBytes, this.readAddress, this.readLength);//buffer[8] [9] [10] [11]
                    break;

                case CtkModbusMessage.fctWriteMultipleCoils:
                case CtkModbusMessage.fctWriteMultipleRegister:
                    this.writeLength = (ushort)Math.Ceiling(this.values.Length / 2.0);
                    //System.Diagnostics.Debug.Assert(this.writeLength * 2 == this.values.Length);//在寫入資料時, Word Count(address length) * 2 要等同於 values bytes 量
                    this.ToBytes_AddReadWrite(dataBytes, this.writeAddress, this.writeLength);//buffer[8] [9] [10] [11]
                    dataBytes.Add((byte)this.values.Length);//buffer[12]
                    break;

                case CtkModbusMessage.fctReadWriteMultipleRegister:
                    this.writeLength = (ushort)Math.Ceiling(this.values.Length / 2.0);
                    //System.Diagnostics.Debug.Assert(this.writeLength * 2 == this.values.Length);//在寫入資料時, Word Count(address length) * 2 要等同於 values bytes 量
                    this.ToBytes_AddReadWrite(dataBytes, this.readAddress, this.readLength);//buffer[8] [9] [10] [11]
                    this.ToBytes_AddReadWrite(dataBytes, this.writeAddress, this.writeLength);//buffer[8] [9] [10] [11]
                    dataBytes.Add((byte)this.values.Length);//buffer[12]


                    break;
            }


            this.msgLength = (ushort)(dataBytes.Count);//include unit id and function code 
            var msgLens = BitConverter.GetBytes(this.msgLength);
            buffer.Add(msgLens[1]);//buffer[4]
            buffer.Add(msgLens[0]);//buffer[5]

            buffer.AddRange(dataBytes);
            buffer.AddRange(this.values);

            return buffer.ToArray();
        }


        void ToBytes_AddReadWrite(List<byte> buffer, ushort startAddr, ushort length)
        {
            var datas = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)startAddr));
            buffer.Add(datas[0]);
            buffer.Add(datas[1]);//buffer[9]

            datas = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)length));
            buffer.Add(datas[0]);//buffer[10]
            buffer.Add(datas[1]);//buffer[11]

        }

        public void LoadRequestBytes(byte[] buffer)
        {
            this.isResponse = false;

            this.transactionId = SwapUInt16(BitConverter.ToUInt16(buffer, 0));
            this.protocolId = SwapUInt16(BitConverter.ToUInt16(buffer, 2));
            this.msgLength = SwapUInt16(BitConverter.ToUInt16(buffer, 4));
            this.unitId = buffer[6];
            this.funcCode = buffer[7];


            byte valLen = 0;

            switch (this.funcCode)
            {
                case CtkModbusMessage.fctReadCoil:
                case CtkModbusMessage.fctReadDiscreteInputs:
                case CtkModbusMessage.fctReadHoldingRegister:
                case CtkModbusMessage.fctReadInputRegister:
                    this.readAddress = SwapUInt16(BitConverter.ToUInt16(buffer, 8));
                    this.readLength = SwapUInt16(BitConverter.ToUInt16(buffer, 10));
                    break;

                case CtkModbusMessage.fctWriteMultipleCoils:
                case CtkModbusMessage.fctWriteMultipleRegister:
                    this.writeAddress = SwapUInt16(BitConverter.ToUInt16(buffer, 8));
                    this.writeLength = SwapUInt16(BitConverter.ToUInt16(buffer, 10));

                    valLen = buffer[12];
                    System.Diagnostics.Debug.Assert(buffer.Length == valLen + 12);
                    this.values = new byte[valLen];
                    Array.Copy(buffer, 13, this.values, 0, valLen);


                    break;

                case CtkModbusMessage.fctReadWriteMultipleRegister:
                    this.readAddress = SwapUInt16(BitConverter.ToUInt16(buffer, 8));
                    this.readLength = SwapUInt16(BitConverter.ToUInt16(buffer, 10));
                    this.writeAddress = SwapUInt16(BitConverter.ToUInt16(buffer, 12));
                    this.writeLength = SwapUInt16(BitConverter.ToUInt16(buffer, 14));

                    valLen = buffer[16];
                    System.Diagnostics.Debug.Assert(buffer.Length == valLen + 16);
                    this.values = new byte[valLen];
                    Array.Copy(buffer, 17, this.values, 0, valLen);

                    break;
            }



        }


        public void LoadResponseBytes(byte[] buffer)
        {
            this.isResponse = true;

            this.transactionId = SwapUInt16(BitConverter.ToUInt16(buffer, 0));
            this.protocolId = SwapUInt16(BitConverter.ToUInt16(buffer, 2));
            this.msgLength = SwapUInt16(BitConverter.ToUInt16(buffer, 4));
            this.unitId = buffer[6];
            this.funcCode = buffer[7];



            switch (this.funcCode)
            {
                case CtkModbusMessage.fctReadCoil:
                case CtkModbusMessage.fctReadDiscreteInputs:
                case CtkModbusMessage.fctReadHoldingRegister:
                case CtkModbusMessage.fctReadInputRegister:
                    this.readLength = buffer[8];
                    this.values = new byte[this.readLength];
                    Array.Copy(buffer, 9, this.values, 0, this.readLength);
                    break;
                case CtkModbusMessage.fctWriteSingleCoil:
                case CtkModbusMessage.fctWriteSingleRegister:
                case CtkModbusMessage.fctWriteMultipleCoils:
                case CtkModbusMessage.fctWriteMultipleRegister:
                case CtkModbusMessage.fctReadWriteMultipleRegister:


                    break;
            }



        }


        public List<Int16> GetValuesToInt16()
        {
            var list = new List<Int16>();

            if (BitConverter.IsLittleEndian)
                for (int idx = 0; idx < this.values.Length; idx += 2)
                    list.Add(SwapUInt16(BitConverter.ToInt16(this.values, idx)));
            else
                for (int idx = 0; idx < this.values.Length; idx += 2)
                    list.Add(BitConverter.ToInt16(this.values, idx));

            return list;
        }


        public CtkModbusMessage CreateReadMessage(byte funcCode, byte unitId, ushort readStart, ushort readLength)
        {
            if (funcCode > CtkModbusMessage.fctReadInputRegister) return null;

            var msg = new CtkModbusMessage();
            msg.funcCode = funcCode;
            msg.unitId = unitId;
            msg.readAddress = readStart;
            msg.readLength = readLength;

            return msg;
        }

        public CtkModbusMessage CreateWriteMessage(byte funcCode, byte unitId, ushort writeAddress, byte[] values)
        {
            var msg = new CtkModbusMessage();
            msg.funcCode = funcCode;
            msg.unitId = unitId;
            msg.writeAddress = writeAddress;
            msg.values = values;

            return msg;
        }

        public CtkModbusMessage CreateReadWriteMessage(byte funcCode, byte unitId, ushort readStart, ushort readLength, ushort writeAddress, byte[] values)
        {
            var msg = new CtkModbusMessage();
            msg.funcCode = funcCode;
            msg.unitId = unitId;
            msg.readAddress = readStart;
            msg.readLength = readLength;
            msg.writeAddress = writeAddress;
            msg.values = values;

            return msg;
        }




        public static CtkModbusMessage FromRequestBytes(byte[] buffer)
        {
            var msg = new CtkModbusMessage();
            msg.LoadRequestBytes(buffer);
            return msg;
        }
        public static CtkModbusMessage FromResponseBytes(byte[] buffer)
        {
            var msg = new CtkModbusMessage();
            msg.LoadResponseBytes(buffer);
            return msg;
        }
        public static bool GetMessageLength(List<byte> buffer, out ushort length)
        {
            length = 0;
            if (buffer.Count < 6) return false;

            var lens = new byte[2];
            lens[0] = buffer[5];
            lens[1] = buffer[4];
            length = BitConverter.ToUInt16(lens, 0);
            return true;
        }


        internal static UInt16 SwapUInt16(UInt16 inValue)
        {
            return (UInt16)(((inValue & 0xff00) >> 8) |
                     ((inValue & 0x00ff) << 8));
        }

        internal static Int16 SwapUInt16(Int16 inValue)
        {
            return (Int16)(((inValue & 0xff00) >> 8) |
                     ((inValue & 0x00ff) << 8));
        }

    }
}
