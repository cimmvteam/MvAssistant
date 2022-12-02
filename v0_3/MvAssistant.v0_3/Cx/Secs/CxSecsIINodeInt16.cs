using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeExpress.v1_1Core.Secs
{



    public class CxSecsIINodeInt16 : CtkSecsIINodeT<Int16>
    {

        public CxSecsIINodeInt16()
        {
            this.m_formatCode = CxSecsIIFormatCode.Int16;
            this.Data = new List<Int16>();
        }
        public CxSecsIINodeInt16(Int16 value)
        {
            this.m_formatCode = CxSecsIIFormatCode.Int16;
            this.Data = new List<Int16>();
            this.DataSetSingle(value);
        }
        public CxSecsIINodeInt16(List<Int16> value)
        {
            this.m_formatCode = CxSecsIIFormatCode.Int16;
            this.Data = value;
        }
    }
}
