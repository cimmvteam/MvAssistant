using CToolkit.v1_0.Secs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Msg.PrescribedSecs
{

    /// <summary>
    /// Secs 內容變更, 只需修改 Prescribed 類別
    /// </summary>
    public class S6F11DockComplete : S6F11
    {

        S6F11DockComplete() { }
        public S6F11DockComplete(CtkHsmsMessage msg) { this.Message = msg; }



        public string PodBarcode { get { return this.S6F11CustomContent[0].As<CtkSecsIINodeASCII>().GetString(); } set { this.S6F11CustomContent[0] = (CtkSecsIINodeASCII)value; } }
        public string BoxBarcode { get { return this.S6F11CustomContent[1].As<CtkSecsIINodeASCII>().GetString(); } set { this.S6F11CustomContent[1] = (CtkSecsIINodeASCII)value; } }
        public string DrawerNumber { get { return this.S6F11CustomContent[1].As<CtkSecsIINodeASCII>().GetString(); } set { this.S6F11CustomContent[1] = (CtkSecsIINodeASCII)value; } }



        public new static S6F11DockComplete Create()
        {
            var rs = new S6F11DockComplete(S6F11.Create());

            rs.MessageName = EnumCeid.S6F11_DockComplete.ToString();

            rs.S6F11CustomContent.Data.AddRange(new CtkSecsIINodeASCII[2]);

            rs.PodBarcode = "PBC123456";
            rs.BoxBarcode = "BBC654321";


            return rs;
        }


    }
}
