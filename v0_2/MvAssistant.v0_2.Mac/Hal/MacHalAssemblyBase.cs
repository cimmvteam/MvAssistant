using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal
{
    [GuidAttribute("4F3F6C35-3EA5-427A-923D-CA544E8A0AF1")]
    public abstract class MacHalAssemblyBase : MacHalBase
    {

        #region Hal

        public override int HalConnect()
        {
            var errcnt = 0;
            foreach (var kv in this.Hals)
            {
                if (kv.Value.HalConnect() != 0)
                    errcnt++;
            }
            return errcnt;
        }

        public override int HalClose()
        {
            var errcnt = 0;
            foreach (var kv in this.Hals)
            {
                if (kv.Value.HalClose() != 0)
                    errcnt++;
            }
            return errcnt;
        }

        #endregion


    }
}
