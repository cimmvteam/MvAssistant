using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeExpress.v1_1Core.Secs
{


    public class CxSecsIINodeUInt64 : CtkSecsIINodeT<UInt64>
    {

        public CxSecsIINodeUInt64()
        {
            this.m_formatCode = CxSecsIIFormatCode.UInt64;
            this.Data = new List<UInt64>();
        }
        public CxSecsIINodeUInt64(UInt64 value)
        {
            this.m_formatCode = CxSecsIIFormatCode.UInt64;
            this.Data = new List<UInt64>();
            this.DataSetSingle(value);
        }
        public CxSecsIINodeUInt64(List<UInt64> value)
        {
            this.m_formatCode = CxSecsIIFormatCode.UInt64;
            this.Data = value;
        }


    }
}
