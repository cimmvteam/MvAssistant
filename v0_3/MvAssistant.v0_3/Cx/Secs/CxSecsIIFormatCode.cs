using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvaCodeExpress.v1_1.Secs
{
    public enum CxSecsIIFormatCode
    {
        List = 0x00,//000 000
        Binary = 0x08,//001 000
        Boolean = 0x09,//001 001
        ASCII = 0x10,//010 000
        JIS8 = 0x11,//010 001
        Char16 = 0x12,//010 010
        Int64 = 0x18,//011 000
        Int8 = 0x19,//011 001
        Int16 = 0x1A,//011 010
        Int32 = 0x1C,//011 100
        Float64 = 0x20,//100 000
        Float32 = 0x24,//100 100
        UInt64 = 0x28,//101 000
        UInt8 = 0x29,//101 001
        UInt16 = 0x2A,//101 010
        UInt32 = 0x2C,//101 100
    }
}
