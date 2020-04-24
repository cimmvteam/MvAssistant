using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.AirPressure
{
    [GuidAttribute("97D0228B-B781-4C2F-B531-6C6D95AA4B2A")]
    public interface IHalGasValve : IHalComponent
    {
        /// <summary>
        /// 開啟Valve
        /// </summary>
        /// <returns>bool, true: success; false: fail</returns>
        bool TurnOn();

        /// <summary>
        /// 關閉Valve
        /// </summary>
        /// <returns>bool, true: success; false: fail</returns>
        bool TurnOff();
    }
}
