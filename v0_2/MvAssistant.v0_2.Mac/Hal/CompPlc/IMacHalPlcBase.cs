using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Hal.CompPlc
{
    public interface IMacHalPlcBase: IMacHalComponent
    {
        Dictionary<MacHalPlcEnumVariable, Object> ReadMulti(IEnumerable<MacHalPlcEnumVariable> varNames);
    }
}
