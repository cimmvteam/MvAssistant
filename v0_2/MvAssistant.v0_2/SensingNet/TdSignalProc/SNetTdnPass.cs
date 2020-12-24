using SensingNet.v0_2.TdBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensingNet.v0_2.TdSignalProc
{
    [Serializable]
    public class SNetTdnPass: SNetTdNodeF8
    {
        ~SNetTdnPass() { this.Dispose(false); }

        public void TgInput(object sender, SNetTdSignalSecF8EventArg ea)
        {
            if (!this.IsEnalbed) return;
            this.OnDataChange(ea);
        }
        public void TgInput(object sender, SNetTdSignalSecSetF8EventArg ea)
        {
            if (!this.IsEnalbed) return;
            this.OnDataChange(ea);
        }
    }
}
