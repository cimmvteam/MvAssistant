using CodeExpress.v1_0.Secs;
using MaskAutoCleaner.v1_0.Machine;
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
    public class MacS3F17DockStart : MacSecsReportBase
    {

        public MacS3F17DockStart() { }
        public MacS3F17DockStart(CxHsmsMessage msg) { this.Message = msg; }


        public string UnitId { get { return (this.RootList[0] as CxSecsIINodeASCII).GetString(); } set { this.RootList[0] = (CxSecsIINodeASCII)value; } }

    




        public static MacS3F17DockStart Create()
        {
            var rs = new MacS3F17DockStart();

            var secsList = new CxSecsIINodeList();
            rs.RootList = secsList;

            secsList.Data.AddRange(new CxSecsIINodeASCII[1]);
            //rs.UnitId = EnumMachineId.DE_LP_A_ASB.ToString();


            return rs;
        }






    }
}
