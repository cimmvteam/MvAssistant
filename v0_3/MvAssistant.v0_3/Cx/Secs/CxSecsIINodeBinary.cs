using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvaCodeExpress.v1_1.Secs
{



    public class CxSecsIINodeBinary : CtkSecsIINodeT<Byte>
    {

        public CxSecsIINodeBinary()
        {
            this.m_formatCode = CxSecsIIFormatCode.Binary;
            this.Data = new List<Byte>();
        }
        public CxSecsIINodeBinary(Byte value)
        {
            this.m_formatCode = CxSecsIIFormatCode.Binary;
            this.Data = new List<Byte>();
            this.DataSetSingle(value);
        }
        public CxSecsIINodeBinary(List<Byte> value)
        {
            this.m_formatCode = CxSecsIIFormatCode.Binary;
            this.Data = value;
        }

        public override string ToSml_DataString(object obj) { return string.Format("{0:x}", obj); }
        public override byte FromSml_DataString(string obj)
        {
            if (obj.IndexOf("0x", StringComparison.OrdinalIgnoreCase) < 0)
                return base.FromSml_DataString(obj);
            return Convert.ToByte(obj, 16);
        }
    }
}
