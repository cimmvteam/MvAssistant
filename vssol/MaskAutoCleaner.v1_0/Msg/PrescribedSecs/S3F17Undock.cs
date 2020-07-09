using CodeExpress.v1_0.Secs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Msg.PrescribedSecs
{

    /// <summary>
    /// Secs 內容變更, 只需修改 Prescribed 類別
    /// </summary>
    public class S3F17Undock : PrescribedSecsBase
    {

        public S3F17Undock() { }
        public S3F17Undock(CxHsmsMessage msg) { this.Message = msg; }




        public static S3F17Undock Create()
        {
            var rs = new S3F17Undock();
            return rs;
        }







    }
}
