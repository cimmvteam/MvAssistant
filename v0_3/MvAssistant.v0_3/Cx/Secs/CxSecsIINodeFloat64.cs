using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvaCodeExpress.v1_1.Secs
{


    public class CxSecsIINodeFloat64 : CtkSecsIINodeT<Double>
    {

        public CxSecsIINodeFloat64()
        {
            this.m_formatCode = CxSecsIIFormatCode.Float64;
            this.Data = new List<Double>();
        }
        public CxSecsIINodeFloat64(List<Double> value)
        {
            this.m_formatCode = CxSecsIIFormatCode.Float64;
            this.Data = value;
        }
        public CxSecsIINodeFloat64(Double value)
        {
            this.m_formatCode = CxSecsIIFormatCode.Float64;
            this.Data = new List<Double>();
            this.DataSetSingle(value);
        }
    }
}
