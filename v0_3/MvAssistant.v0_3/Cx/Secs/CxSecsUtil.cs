using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvaCodeExpress.v1_1.Secs
{
    public class CxSecsUtil
    {



        public static int EndianSwitch(byte[] buffer, int offset, int dataSize, int typeSize)
        {
            if ((offset + dataSize * typeSize) > buffer.Length)
                throw new Exception("資料位元數不夠");



            var stack = new Stack<byte>();
            for (int idx = 0; idx < dataSize; idx++)
            {
                for (int jdx = 0; jdx < typeSize; jdx++)
                    stack.Push(buffer[offset + idx * typeSize + jdx]);
                for (int jdx = 0; jdx < typeSize; jdx++)
                    buffer[offset + idx * typeSize + jdx] = stack.Pop();

#if DEBUG
                System.Diagnostics.Debug.Assert(stack.Count == 0);
#endif
            }

            return 1;
        }



        public static string Utf8GetString(byte[] buffer, int index = 0, int count = -1)
        {
            if (count < 0) count = buffer.Length;
            return Encoding.UTF8.GetString(buffer, index, buffer.Length);
        }

        public static byte[] Utf8GetBytes(string str) { return Encoding.UTF8.GetBytes(str); }


        public static string AsciiGetString(byte[] buffer, int index = 0, int count = -1)
        {
            if (count < 0) count = buffer.Length;
            return Encoding.GetEncoding("us-ascii").GetString(buffer, index, buffer.Length);
        }
        public static byte[] AsciiGetBytes(string str) { return Encoding.GetEncoding("us-ascii").GetBytes(str); }



        public static byte[] MsgToByte(byte[] headBuffer, byte[] dataBuffer)
        {
            CxUtil.CheckLicenseEncrypt();

            var rs = new List<byte>();
            rs.AddRange(headBuffer);
            if (dataBuffer != null) rs.AddRange(dataBuffer);

            var msgLengthBytes = BitConverter.GetBytes(rs.Count);
            if (BitConverter.IsLittleEndian)//.net預設為LittleEndian
                Array.Reverse(msgLengthBytes);

            var rs2 = new List<byte>(msgLengthBytes);//只有Message才會知道total length
            rs2.AddRange(rs);
            return rs2.ToArray();
        }




    }
}
