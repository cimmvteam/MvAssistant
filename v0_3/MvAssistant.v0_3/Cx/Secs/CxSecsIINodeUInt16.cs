using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeExpress.v1_1Core.Secs
{



    public class CxSecsIINodeUInt16 : CtkSecsIINodeT<UInt16>
    {

        public CxSecsIINodeUInt16()
        {
            this.m_formatCode = CxSecsIIFormatCode.UInt16;
            this.Data = new List<UInt16>();
        }
        public CxSecsIINodeUInt16(UInt16 value)
        {
            this.m_formatCode = CxSecsIIFormatCode.UInt16;
            this.Data = new List<UInt16>();
            this.DataSetSingle(value);
        }
        public CxSecsIINodeUInt16(List<UInt16> value)
        {
            this.m_formatCode = CxSecsIIFormatCode.UInt16;
            this.Data = value;
        }

    }
}
