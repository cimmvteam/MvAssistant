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
    public class MacS1F3QuerySvid : MacSecsReportBase
    {

        protected MacS1F3QuerySvid() { }
        public MacS1F3QuerySvid(CxHsmsMessage msg) { this.Message = msg; }


        public List<UInt64> SvidList
        {
            get
            {
                var rs = new List<UInt64>();
                foreach (var row in this.RootList.Data)
                {
                    if (row is CxSecsIINodeUInt32)
                    {
                        var node = row as CxSecsIINodeUInt32;
                        rs.Add(node.Data[0]);
                    }
                    else if (row is CxSecsIINodeUInt64)
                    {
                        var node = row as CxSecsIINodeUInt64;
                        rs.Add(node.Data[0]);
                    }
                    else { throw new ArgumentException("Svid應只有UInt32/UInt64"); }
                }
                return rs;
            }
        }





        public static MacS1F3QuerySvid Create()
        {
            var rs = new MacS1F3QuerySvid();

            rs.RootList = new CxSecsIINodeList();
            var rootList = rs.RootList;


            rootList.Data.AddRange(new CxSecsIINodeUInt64[3]);

            return rs;
        }















    }
}
