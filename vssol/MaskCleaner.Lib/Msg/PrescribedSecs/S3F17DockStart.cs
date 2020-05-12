using CToolkit.v1_0.Secs;
using MaskAutoCleaner.Machine;
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
    public class S3F17DockStart : PrescribedSecsBase
    {

        public S3F17DockStart() { }
        public S3F17DockStart(CtkHsmsMessage msg) { this.Message = msg; }


        public string UnitId { get { return (this.RootList[0] as CtkSecsIINodeASCII).GetString(); } set { this.RootList[0] = (CtkSecsIINodeASCII)value; } }

    




        public static S3F17DockStart Create()
        {
            var rs = new S3F17DockStart();

            var secsList = new CtkSecsIINodeList();
            rs.RootList = secsList;

            secsList.Data.AddRange(new CtkSecsIINodeASCII[1]);
            rs.UnitId = EnumMachineId.DE_LP_A_ASB.ToString();


            return rs;
        }






    }
}
