using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.AirPressure
{
    [GuidAttribute("9F995912-D4DF-4E24-AD49-FD1CA8A423FB")]
    public interface IHalPressureCtrl : IMacHalComponent
    {
        /// <summary>
        /// 讀取[Pressure Controller] value
        /// </summary>
        /// <returns>value, unit: float</returns>
        float ReadPresureValue();
        /// <summary>
        /// 設定[Pressure Controller] value
        /// </summary>
        /// <param name="value"></param>
        void SetPressureValue(double value);
        /// <summary>
        /// 設定[Pressure Controller] flow time
        /// </summary>
        /// <param name="value"></param>
        void SetFlowTimeValue(int value);
    }
}
