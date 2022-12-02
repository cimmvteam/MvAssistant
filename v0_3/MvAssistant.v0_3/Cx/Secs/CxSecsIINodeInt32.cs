using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeExpress.v1_1Core.Secs
{


    public class CxSecsIINodeInt32 : CtkSecsIINodeT<Int32>
    {

        public CxSecsIINodeInt32()
        {
            this.m_formatCode = CxSecsIIFormatCode.Int32;
            this.Data = new List<Int32>();
        }
        public CxSecsIINodeInt32(Int32 value)
        {
            this.m_formatCode = CxSecsIIFormatCode.Int32;
            this.Data = new List<Int32>();
            this.DataSetSingle(value);
        }
        public CxSecsIINodeInt32(List<Int32> value)
        {
            this.m_formatCode = CxSecsIIFormatCode.Int32;
            this.Data = value;
        }
    }
}
