using CodeExpress.v1_0.Secs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Msg.SecsReport
{

    /// <summary>
    /// Secs 內容變更, 只需修改 Prescribed 類別
    /// </summary>
    public class MacS6F11DockComplete : MacS6F11
    {

        MacS6F11DockComplete() { }
        public MacS6F11DockComplete(CxHsmsMessage msg) { this.Message = msg; }



        public string PodBarcode { get { return this.S6F11CustomContent[0].As<CxSecsIINodeASCII>().GetString(); } set { this.S6F11CustomContent[0] = (CxSecsIINodeASCII)value; } }
        public string BoxBarcode { get { return this.S6F11CustomContent[1].As<CxSecsIINodeASCII>().GetString(); } set { this.S6F11CustomContent[1] = (CxSecsIINodeASCII)value; } }
        public string DrawerNumber { get { return this.S6F11CustomContent[1].As<CxSecsIINodeASCII>().GetString(); } set { this.S6F11CustomContent[1] = (CxSecsIINodeASCII)value; } }



        public new static MacS6F11DockComplete Create()
        {
            var rs = new MacS6F11DockComplete(MacS6F11.Create());

            rs.MessageName = EnumMacCeid.S6F11_DockComplete.ToString();

            rs.S6F11CustomContent.Data.AddRange(new CxSecsIINodeASCII[2]);

            rs.PodBarcode = "PBC123456";
            rs.BoxBarcode = "BBC654321";


            return rs;
        }


    }
}
