using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeExpress.v1_1Core.Secs
{



    public class CxSecsIINodeFloat32 : CtkSecsIINodeT<Single>
    {

        public CxSecsIINodeFloat32()
        {
            this.m_formatCode = CxSecsIIFormatCode.Float32;
            this.Data = new List<Single>();
        }
        public CxSecsIINodeFloat32(Single value)
        {
            this.m_formatCode = CxSecsIIFormatCode.Float32;
            this.Data = new List<Single>();
            this.DataSetSingle(value);
        }
        public CxSecsIINodeFloat32(List<Single> value)
        {
            this.m_formatCode = CxSecsIIFormatCode.Float32;
            this.Data = value;
        }

    }
}
