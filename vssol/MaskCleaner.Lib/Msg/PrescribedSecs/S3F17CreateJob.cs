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
    public class S3F17CreateJob : PrescribedSecsBase
    {

        public S3F17CreateJob() { }
        public S3F17CreateJob(CtkHsmsMessage msg) { this.Message = msg; }


        public string RecipeType { get { return (this.RootList[0] as CtkSecsIINodeASCII).GetString(); } set { this.RootList[0] = (CtkSecsIINodeASCII)value; } }
        public string RecipeName { get { return (this.RootList[1] as CtkSecsIINodeASCII).GetString(); } set { this.RootList[1] = (CtkSecsIINodeASCII)value; } }
        public string PodBarcode { get { return (this.RootList[2] as CtkSecsIINodeASCII).GetString(); } set { this.RootList[2] = (CtkSecsIINodeASCII)value; } }
        public string BoxBarcode { get { return (this.RootList[3] as CtkSecsIINodeASCII).GetString(); } set { this.RootList[3] = (CtkSecsIINodeASCII)value; } }
        public string MaskBarcodeInAccount { get { return (this.RootList[4] as CtkSecsIINodeASCII).GetString(); } set { this.RootList[4] = (CtkSecsIINodeASCII)value; } }
        public string LoadPortUnit { get { return (this.RootList[5] as CtkSecsIINodeASCII).GetString(); } set { this.RootList[5] = (CtkSecsIINodeASCII)value; } }
        public string DrawerUnit { get { return (this.RootList[6] as CtkSecsIINodeASCII).GetString(); } set { this.RootList[6] = (CtkSecsIINodeASCII)value; } }

        public static S3F17CreateJob Create(string source, string type)
        {
            var rs = new S3F17CreateJob();

            var secsList = new CtkSecsIINodeList();
            rs.RootList = secsList;

            secsList.Data.AddRange(new CtkSecsIINodeASCII[7]);

            rs.LoadPortUnit = source;
            rs.MaskBarcodeInAccount = "MBC456789";
            rs.PodBarcode = "PBC123456";
            rs.BoxBarcode = "BBC654321";            
            rs.RecipeType = type;

            return rs;
        }





    }
}
