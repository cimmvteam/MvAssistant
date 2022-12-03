using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvaCodeExpress.v1_1.Secs
{

    public class CxSecsIINodeBoolean : CtkSecsIINodeT<Byte>
    {

        public CxSecsIINodeBoolean()
        {
            this.m_formatCode = CxSecsIIFormatCode.Boolean;
            this.Data = new List<Byte>();
        }
        public CxSecsIINodeBoolean(Byte value)
        {
            this.m_formatCode = CxSecsIIFormatCode.Boolean;
            this.Data = new List<Byte>();
            this.DataSetSingle(value);
        }
        public CxSecsIINodeBoolean(List<Byte> value)
        {
            this.m_formatCode = CxSecsIIFormatCode.Boolean;
            this.Data = value;
        }


        public void DataSetSingle(Boolean val)
        {
            this.Data.Clear();
            this.Data.Add(Convert.ToByte(val ? 1 : 0));
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
