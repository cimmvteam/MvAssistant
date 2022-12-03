using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvaCodeExpress.v1_1.Secs
{



    public class CxSecsIINodeUInt8 : CtkSecsIINodeT<Byte>
    {

        public CxSecsIINodeUInt8()
        {
            this.m_formatCode = CxSecsIIFormatCode.UInt8;
            this.Data = new List<Byte>();
        }
        public CxSecsIINodeUInt8(Byte value)
        {
            this.m_formatCode = CxSecsIIFormatCode.UInt8;
            this.Data = new List<Byte>();
            this.DataSetSingle(value);
        }
        public CxSecsIINodeUInt8(List<Byte> value)
        {
            this.m_formatCode = CxSecsIIFormatCode.UInt8;
            this.Data = value;
        }

    }
}
