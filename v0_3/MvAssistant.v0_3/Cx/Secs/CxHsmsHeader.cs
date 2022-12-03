using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MvaCodeExpress.v1_1.Secs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CxHsmsHeader
    {
        /*欄位順序及大小 必須固定, 直接用記憶體分配填入*/
        public Int16 DeviceId;
        public byte WStreamId;
        public byte FunctionId;
        public byte PType;
        public byte SType;
        public byte SystemByte1;
        public byte SystemByte2;
        public byte SystemByte3;
        public byte SystemByte4;



        public bool WBit { get { return (this.WStreamId & 0x80) != 0; } set { this.WStreamId = (byte)((value ? 0x80 : 0x00) + (this.WStreamId & 0x7f)); } }
        public byte StreamId { get { return (byte)(this.WStreamId & 0x7f); } set { this.WStreamId = (byte)((this.WStreamId & 0x80) + (value & 0x7f)); } }


        public byte[] ToBytes()
        {
            //Byte[] bytes = new Byte[Marshal.SizeOf(typeof(CxHsmsHeader))];
            Byte[] bytes = new Byte[Marshal.SizeOf<CxHsmsHeader>()];
            GCHandle pinStructure = GCHandle.Alloc(this, GCHandleType.Pinned);
            try
            {
                Marshal.Copy(pinStructure.AddrOfPinnedObject(), bytes, 0, bytes.Length);
                return bytes;
            }
            finally
            {
                pinStructure.Free();
            }
        }

        public static CxHsmsHeader FromBytes(byte[] arr)
        {
            CxHsmsHeader str = new CxHsmsHeader();

            //int size = Marshal.SizeOf(typeof(CxHsmsHeader));
            int size = Marshal.SizeOf<CxHsmsHeader>();
            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.Copy(arr, 0, ptr, size);

            //str = (CxHsmsHeader)Marshal.PtrToStructure(ptr, typeof(CxHsmsHeader));
            str = Marshal.PtrToStructure<CxHsmsHeader>(ptr);
            Marshal.FreeHGlobal(ptr);

            return str;
        }
    }
}
