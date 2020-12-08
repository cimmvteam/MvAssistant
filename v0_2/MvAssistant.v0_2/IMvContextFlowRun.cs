using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2
{
    public interface IMvContextFlowRun : IMvContextFlow
    {

        int MvCfRunOnce();
        int MvCfRunLoop();
        int MvCfRunLoopAsyn();


    }
}
