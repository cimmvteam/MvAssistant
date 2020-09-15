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
    public class MacS6F11 : MacSecsReportBase
    {

        protected MacS6F11() { }
        public MacS6F11(CxHsmsMessage msg) { this.Message = msg; }


        public string MessageName { get { return this.RootList[0].As<CxSecsIINodeASCII>().GetString(); } set { this.RootList[0] = (CxSecsIINodeASCII)value; } }
        public CxSecsIINodeList S6F11CommonContent { get { return (this.RootList[1] as CxSecsIINodeList); } set { this.RootList[1] = value; } }//S6F11共同內容
        public string CommonUnitId { get { return this.S6F11CommonContent[0].As<CxSecsIINodeASCII>().GetString(); } set { this.S6F11CommonContent[0] = (CxSecsIINodeASCII)value; } }
        public CxSecsIINodeList S6F11CustomContent { get { return (this.RootList[2] as CxSecsIINodeList); } set { this.RootList[2] = value; } }//S6F11自訂內容






        public static MacS6F11 Create()
        {
            var rs = new MacS6F11();
            rs.Message.header.StreamId = 6;
            rs.Message.header.FunctionId = 11;
            rs.RootList = new CxSecsIINodeList();
            var rootList = rs.RootList;


            rootList.Data.AddRange(new CxSecsIINode[]{
                (CxSecsIINodeASCII)"",

                //Common
                new CxSecsIINodeList(){ Data = new List<CxSecsIINode>(new []{
                    (CxSecsIINodeASCII)"Common[0]"
                })},

                //Content
                new CxSecsIINodeList(),
            });



            return rs as MacS6F11;
        }















    }
}
