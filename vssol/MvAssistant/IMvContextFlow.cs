using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant
{
    public interface IMvContextFlow
    {

        int MvCfInit();
        int MvCfLoad();
        int MvCfUnload();
        int MvCfFree();

    }
}
