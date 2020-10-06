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
    public class MacS3F17CreateJob : MacSecsReportBase
    {

        public MacS3F17CreateJob() { }
        public MacS3F17CreateJob(CxHsmsMessage msg) { this.Message = msg; }


        public string RecipeType { get { return (this.RootList[0] as CxSecsIINodeASCII).GetString(); } set { this.RootList[0] = (CxSecsIINodeASCII)value; } }
        public string RecipeName { get { return (this.RootList[1] as CxSecsIINodeASCII).GetString(); } set { this.RootList[1] = (CxSecsIINodeASCII)value; } }
        public string PodBarcode { get { return (this.RootList[2] as CxSecsIINodeASCII).GetString(); } set { this.RootList[2] = (CxSecsIINodeASCII)value; } }
        public string BoxBarcode { get { return (this.RootList[3] as CxSecsIINodeASCII).GetString(); } set { this.RootList[3] = (CxSecsIINodeASCII)value; } }
        public string MaskBarcodeInAccount { get { return (this.RootList[4] as CxSecsIINodeASCII).GetString(); } set { this.RootList[4] = (CxSecsIINodeASCII)value; } }
        public string LoadPortUnit { get { return (this.RootList[5] as CxSecsIINodeASCII).GetString(); } set { this.RootList[5] = (CxSecsIINodeASCII)value; } }
        public string DrawerUnit { get { return (this.RootList[6] as CxSecsIINodeASCII).GetString(); } set { this.RootList[6] = (CxSecsIINodeASCII)value; } }

        public static MacS3F17CreateJob Create(string source, string type)
        {
            var rs = new MacS3F17CreateJob();

            var secsList = new CxSecsIINodeList();
            rs.RootList = secsList;

            secsList.Data.AddRange(new CxSecsIINodeASCII[7]);

            rs.LoadPortUnit = source;
            rs.MaskBarcodeInAccount = "MBC456789";
            rs.PodBarcode = "PBC123456";
            rs.BoxBarcode = "BBC654321";            
            rs.RecipeType = type;

            return rs;
        }





    }
}
