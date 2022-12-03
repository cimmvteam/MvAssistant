using MvaCodeExpress.v1_1.Secs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvaCodeExpress.v1_1.SecsMsg
{
    public class CxSecsS01F02
    {
        public CxHsmsMessage SecsMsg;

        public CxSecsS01F02()
        {
            var msg = this.SecsMsg = new CxHsmsMessage();
            msg.Header.StreamId = 1;
            msg.Header.FunctionId = 2;

            var root = new CxSecsIINodeList();
            root.AddAscii("");//Model
            root.AddAscii("");//Version
            root.AddAscii("");//Date Time

            msg.RootNode = root;

        }
        public CxSecsS01F02(CxHsmsMessage secsMsg) { this.SecsMsg = secsMsg; }

        protected CxSecsIINodeList RootNode { get { return this.SecsMsg.RootNode.As<CxSecsIINodeList>(); } }

        public String Model { get { return this.RootNode[0].As<CxSecsIINodeASCII>(); } set { this.RootNode[0].As<CxSecsIINodeASCII>().SetString(value); } }
        public String Version { get { return this.RootNode[1].As<CxSecsIINodeASCII>(); } set { this.RootNode[1].As<CxSecsIINodeASCII>().SetString(value); } }
        public String DateTime { get { return this.RootNode[2].As<CxSecsIINodeASCII>(); } set { this.RootNode[2].As<CxSecsIINodeASCII>().SetString(value); } }






        #region Static

        public static CxSecsS01F02 Create(CxHsmsMessage secsMsg)
        {
            var sxfy = new CxSecsS01F02(secsMsg);
            return sxfy;
        }
        public static CxSecsS01F02 Create(String sml)
        {
            var secsMsg = CxHsmsMessage.GetFromSml(sml);
            var sxfy = new CxSecsS01F02(secsMsg);
            return sxfy;
        }


        #endregion


        #region Static Operator

        public static implicit operator CxHsmsMessage(CxSecsS01F02 data) { return data.SecsMsg; }
        public static implicit operator CxSecsS01F02(CxHsmsMessage data) { return new CxSecsS01F02(data); }

        #endregion

    }
}
