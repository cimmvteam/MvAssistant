using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvaCodeExpress.v1_1.Secs
{

    public class CxSecsIINodeInt64 : CtkSecsIINodeT<Int64>
    {

        public CxSecsIINodeInt64()
        {
            this.m_formatCode = CxSecsIIFormatCode.Int64;
            this.Data = new List<Int64>();
        }
        public CxSecsIINodeInt64(Int64 value)
        {
            this.m_formatCode = CxSecsIIFormatCode.Int64;
            this.Data = new List<Int64>();
            this.DataSetSingle(value);
        }
        public CxSecsIINodeInt64(List<Int64> value)
        {
            this.m_formatCode = CxSecsIIFormatCode.Int64;
            this.Data = value;
        }
    }
}
