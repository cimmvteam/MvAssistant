using MaskAutoCleaner.v1_0.Msg.PrescribedJobNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Msg
{
    public class MsgJobNotify : MsgBase
    {
        public EnumJobNotify JobNotify;

        public PrescribedJobNotifyBase PrescribedJobNotify;
    }
}
