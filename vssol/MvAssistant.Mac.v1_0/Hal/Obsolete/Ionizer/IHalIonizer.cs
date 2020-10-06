using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component
{
    [GuidAttribute("56BDE2A3-F77A-4158-A904-BA22C4C8D996")]
    public interface IHalIonizer : IMacHalComponent
    {
        /// <summary>
        /// 開啟Ionizer裝置
        /// </summary>
        /// <returns>bool, true: success; false: fail</returns>
        bool TurnOn();

        /// <summary>
        /// 關閉Ionizer裝置
        /// </summary>
        /// <returns>bool, true: success; false: fail</returns>
        bool TurnOff();
    }
}
