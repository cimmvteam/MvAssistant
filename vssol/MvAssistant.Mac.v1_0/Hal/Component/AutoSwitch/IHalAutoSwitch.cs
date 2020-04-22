using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.AutoSwitch
{
    [GuidAttribute("2B175C5C-4E66-4D33-B30C-2370E36D76E5")]
    public interface IHalAutoSwitch : IHalComponent
    {
        /// <summary>
        /// 讀取[Auto Switch] value
        /// </summary>
        /// <returns>value, unit: float</returns>
        bool GetValue();
    }
}
