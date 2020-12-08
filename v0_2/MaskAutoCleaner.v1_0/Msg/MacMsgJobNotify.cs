using MaskAutoCleaner.v1_0.Msg.JobNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Msg
{
    public class MacMsgJobNotify : MacMsgBase
    {
        public EnumMacJobNotify JobNotify;

        public MacJobNotifyBase PrescribedJobNotify;
    }
}
