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
    public class S1F3QuerySvid : PrescribedSecsBase
    {

        protected S1F3QuerySvid() { }
        public S1F3QuerySvid(CtkHsmsMessage msg) { this.Message = msg; }


        public List<UInt64> SvidList
        {
            get
            {
                var rs = new List<UInt64>();
                foreach (var row in this.RootList.Data)
                {
                    if (row is CtkSecsIINodeUInt32)
                    {
                        var node = row as CtkSecsIINodeUInt32;
                        rs.Add(node.Data[0]);
                    }
                    else if (row is CtkSecsIINodeUInt64)
                    {
                        var node = row as CtkSecsIINodeUInt64;
                        rs.Add(node.Data[0]);
                    }
                    else { throw new ArgumentException("Svid應只有UInt32/UInt64"); }
                }
                return rs;
            }
        }





        public static S1F3QuerySvid Create()
        {
            var rs = new S1F3QuerySvid();

            rs.RootList = new CtkSecsIINodeList();
            var rootList = rs.RootList;


            rootList.Data.AddRange(new CtkSecsIINodeUInt64[3]);

            return rs;
        }















    }
}
