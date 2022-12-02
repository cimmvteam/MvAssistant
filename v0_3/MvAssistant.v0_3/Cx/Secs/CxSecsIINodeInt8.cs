using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeExpress.v1_1Core.Secs
{


    public class CxSecsIINodeInt8 : CtkSecsIINodeT<SByte>
    {

        public CxSecsIINodeInt8()
        {
            this.m_formatCode = CxSecsIIFormatCode.Int8;
            this.Data = new List<SByte>();
        }
        public CxSecsIINodeInt8(SByte value)
        {
            this.m_formatCode = CxSecsIIFormatCode.Int8;
            this.Data = new List<SByte>();
            this.DataSetSingle(value);
        }
        public CxSecsIINodeInt8(List<SByte> value)
        {
            this.m_formatCode = CxSecsIIFormatCode.Int8;
            this.Data = value;
        }

    }
}
