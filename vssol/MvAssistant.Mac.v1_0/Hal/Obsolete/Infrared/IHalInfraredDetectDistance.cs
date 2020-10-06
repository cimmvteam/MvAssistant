using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Infrared
{
    [GuidAttribute("828AE68E-E3FE-41D6-912C-B542AA1EBFB4")]
    public interface IHalInfraredDetectDistance : IMacHalComponent
    {
        bool SetIrAddress(string varName);

        /// <summary>
        /// 讀取[測距紅外線] value
        /// </summary>
        /// <returns></returns>
        float GetValue();
    }
}
