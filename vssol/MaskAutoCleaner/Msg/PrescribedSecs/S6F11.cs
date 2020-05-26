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
    public class S6F11 : PrescribedSecsBase
    {

        protected S6F11() { }
        public S6F11(CtkHsmsMessage msg) { this.Message = msg; }


        public string MessageName { get { return this.RootList[0].As<CtkSecsIINodeASCII>().GetString(); } set { this.RootList[0] = (CtkSecsIINodeASCII)value; } }
        public CtkSecsIINodeList S6F11CommonContent { get { return (this.RootList[1] as CtkSecsIINodeList); } set { this.RootList[1] = value; } }//S6F11共同內容
        public string CommonUnitId { get { return this.S6F11CommonContent[0].As<CtkSecsIINodeASCII>().GetString(); } set { this.S6F11CommonContent[0] = (CtkSecsIINodeASCII)value; } }
        public CtkSecsIINodeList S6F11CustomContent { get { return (this.RootList[2] as CtkSecsIINodeList); } set { this.RootList[2] = value; } }//S6F11自訂內容






        public static S6F11 Create()
        {
            var rs = new S6F11();
            rs.Message.header.StreamId = 6;
            rs.Message.header.FunctionId = 11;
            rs.RootList = new CtkSecsIINodeList();
            var rootList = rs.RootList;


            rootList.Data.AddRange(new CtkSecsIINode[]{
                (CtkSecsIINodeASCII)"",

                //Common
                new CtkSecsIINodeList(){ Data = new List<CtkSecsIINode>(new []{
                    (CtkSecsIINodeASCII)"Common[0]"
                })},

                //Content
                new CtkSecsIINodeList(),
            });



            return rs as S6F11;
        }















    }
}
