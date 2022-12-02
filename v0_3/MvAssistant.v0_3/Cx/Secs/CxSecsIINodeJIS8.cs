using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeExpress.v1_1Core.Secs
{


    public class CxSecsIINodeJIS8 : CtkSecsIINodeT<Byte>
    {

        public CxSecsIINodeJIS8()
        {
            this.m_formatCode = CxSecsIIFormatCode.JIS8;
            this.Data = new List<Byte>();
        }
        public CxSecsIINodeJIS8(Byte value)
        {
            this.m_formatCode = CxSecsIIFormatCode.JIS8;
            this.Data = new List<Byte>();
            this.DataSetSingle(value);
        }
        public CxSecsIINodeJIS8(List<Byte> value)
        {
            this.m_formatCode = CxSecsIIFormatCode.JIS8;
            this.Data = value;
        }


    }
}
