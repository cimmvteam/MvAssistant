using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant
{
    public interface IMvContextFlowRun : IMvContextFlow
    {

        int MvCfRunOnce();
        int MvCfRunLoop();
        int MvCfRunLoopAsyn();


    }
}
