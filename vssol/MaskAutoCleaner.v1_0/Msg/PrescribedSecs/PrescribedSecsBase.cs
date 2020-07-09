using CodeExpress.v1_0.Secs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Msg.PrescribedSecs
{
    public class PrescribedSecsBase
    {
        public CxHsmsMessage Message = new CxHsmsMessage();

        public PrescribedSecsBase() { }
        public PrescribedSecsBase(CxHsmsMessage msg) { this.Message = msg; }
        public CxSecsIINodeList RootList { get { return this.Message.rootNode as CxSecsIINodeList; } set { this.Message.rootNode = value; } }

        public T As<T>() where T : PrescribedSecsBase { return this as T; }




        #region Operator

        public static implicit operator CxHsmsMessage(PrescribedSecsBase prescribedSecs) { return prescribedSecs.Message; }
        public static implicit operator PrescribedSecsBase(CxHsmsMessage prescribedSecs) { return new PrescribedSecsBase(prescribedSecs); }

        #endregion

    }
}
