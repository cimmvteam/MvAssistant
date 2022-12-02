using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeExpress.v1_1Core.Secs
{


    public class CxSecsIINodeUInt32 : CtkSecsIINodeT<UInt32>
    {

        public CxSecsIINodeUInt32()
        {
            this.m_formatCode = CxSecsIIFormatCode.UInt32;
            this.Data = new List<UInt32>();
        }
        public CxSecsIINodeUInt32(UInt32 value)
        {
            this.m_formatCode = CxSecsIIFormatCode.UInt32;
            this.Data = new List<UInt32>();
            this.DataSetSingle(value);
        }
        public CxSecsIINodeUInt32(List<UInt32> value)
        {
            this.m_formatCode = CxSecsIIFormatCode.UInt32;
            this.Data = value;
        }



    }
}
