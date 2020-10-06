using CodeExpress.v1_0.Secs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Msg.SecsReport
{
    public class MacSecsReportBase
    {
        public CxHsmsMessage Message = new CxHsmsMessage();

        public MacSecsReportBase() { }
        public MacSecsReportBase(CxHsmsMessage msg) { this.Message = msg; }
        public CxSecsIINodeList RootList { get { return this.Message.rootNode as CxSecsIINodeList; } set { this.Message.rootNode = value; } }

        public T As<T>() where T : MacSecsReportBase { return this as T; }




        #region Operator

        public static implicit operator CxHsmsMessage(MacSecsReportBase prescribedSecs) { return prescribedSecs.Message; }
        public static implicit operator MacSecsReportBase(CxHsmsMessage prescribedSecs) { return new MacSecsReportBase(prescribedSecs); }

        #endregion

    }
}
