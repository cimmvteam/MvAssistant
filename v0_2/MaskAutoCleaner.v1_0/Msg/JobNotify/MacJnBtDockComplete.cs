using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Msg.JobNotify
{
    public class MacJnBtDockComplete: MacJobNotifyBase
    {
        public BoxInfo boxInfo;

        public class BoxInfo
        {
            public EnumMacBoxType boxType;
            private string boxBarcode;

            public string BoxBarcode
            {
              get { return boxBarcode; }
              set { boxBarcode = value; }
            }
        }
    }
}
