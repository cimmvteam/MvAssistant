using CToolkit.v1_0.Secs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Msg.PrescribedSecs
{
    public class PrescribedSecsBase
    {
        public CtkHsmsMessage Message = new CtkHsmsMessage();

        public PrescribedSecsBase() { }
        public PrescribedSecsBase(CtkHsmsMessage msg) { this.Message = msg; }
        public CtkSecsIINodeList RootList { get { return this.Message.rootNode as CtkSecsIINodeList; } set { this.Message.rootNode = value; } }

        public T As<T>() where T : PrescribedSecsBase { return this as T; }




        #region Operator

        public static implicit operator CtkHsmsMessage(PrescribedSecsBase prescribedSecs) { return prescribedSecs.Message; }
        public static implicit operator PrescribedSecsBase(CtkHsmsMessage prescribedSecs) { return new PrescribedSecsBase(prescribedSecs); }

        #endregion

    }
}
