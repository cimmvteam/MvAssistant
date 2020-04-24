using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.AirPressure
{
    [GuidAttribute("8DCE9B6A-4D36-4920-940D-C162B99BB28C")]
    public interface IHalPressureSensor : IHalComponent
    {
        /// <summary>
        /// 設定[氣體壓力計] value
        /// </summary>
        /// <param name="value"></param>
        void SetPressureValue(float value);

        /// <summary>
        /// 讀取[氣體壓力計] value
        /// </summary>
        /// <returns>pressure value, unit: float</returns>
        float GetPressureValue();
    }
}
