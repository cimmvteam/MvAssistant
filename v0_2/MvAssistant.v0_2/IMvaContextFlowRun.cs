using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2
{
    public interface IMvaContextFlowRun : IMvaContextFlow
    {

        int MvaCfRunOnce();
        int MvaCfRunLoop();
        int MvaCfRunLoopAsyn();


    }
}
