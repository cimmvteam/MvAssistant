using CodeExpress.v1_1Core.Secs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeExpress.v1_1Core.SecsMsg
{
    public class CxSecsS01F01
    {
        public CxHsmsMessage SecsMsg;

        public CxSecsS01F01()
        {
            var msg = this.SecsMsg = new CxHsmsMessage();
            msg.Header.StreamId = 1;
            msg.Header.FunctionId = 1;
        }
        public CxSecsS01F01(CxHsmsMessage secsMsg) { this.SecsMsg = secsMsg; }






        #region Static

        public static CxSecsS01F01 Create(CxHsmsMessage secsMsg)
        {
            var sxfy = new CxSecsS01F01(secsMsg);
            return sxfy;
        }
        public static CxSecsS01F01 Create(String sml)
        {
            var secsMsg = CxHsmsMessage.GetFromSml(sml);
            var sxfy = new CxSecsS01F01(secsMsg);
            return sxfy;
        }


        #endregion


        #region Static Operator

        public static implicit operator CxHsmsMessage(CxSecsS01F01 data) { return data.SecsMsg; }
        public static implicit operator CxSecsS01F01(CxHsmsMessage data) { return new CxSecsS01F01(data); }

        #endregion

    }
}
