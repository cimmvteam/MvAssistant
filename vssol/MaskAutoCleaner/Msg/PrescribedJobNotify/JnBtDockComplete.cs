using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Msg.PrescribedJobNotify
{
    public class JnBtDockComplete: PrescribedJobNotifyBase
    {
        public BoxInfo boxInfo;

        public class BoxInfo
        {
            public EnumBoxType boxType;
            private string boxBarcode;

            public string BoxBarcode
            {
              get { return boxBarcode; }
              set { boxBarcode = value; }
            }
        }
    }
}
