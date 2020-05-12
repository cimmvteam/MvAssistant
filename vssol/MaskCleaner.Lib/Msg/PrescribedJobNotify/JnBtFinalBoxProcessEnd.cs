using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Msg.PrescribedJobNotify
{
    public class JnBtFinalBoxProcessEnd : PrescribedJobNotifyBase
    {
        public List<BoxInfo> BoxInfoList;
    }
}
