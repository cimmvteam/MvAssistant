using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Motor
{
    [GuidAttribute("CE79F8ED-086B-41CE-9C51-528631D38C85")]
    public interface IHalCylinder : IMacHalComponent
    {
        /// <summary>
        /// 開啟Cylinder裝置 (夾緊物體)
        /// </summary>
        /// <returns>bool, true: success; false: fail</returns>
        bool TurnOn();

        /// <summary>
        /// 關閉Cylinder裝置 (鬆開物體)
        /// </summary>
        /// <returns>bool, true: success; false: fail</returns>
        bool TurnOff();
    }
}
