using MaskAutoCleaner.Msg.PrescribedJobNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Msg
{
    public class MsgJobNotify : MsgBase
    {
        public EnumJobNotify JobNotify;

        public PrescribedJobNotifyBase PrescribedJobNotify;
    }
}
