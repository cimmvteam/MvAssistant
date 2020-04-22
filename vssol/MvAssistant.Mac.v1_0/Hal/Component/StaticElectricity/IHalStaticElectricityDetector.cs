using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component
{
    [GuidAttribute("753D9DA8-FD7F-4912-8B42-1BEC87402646")]
    public interface IHalStaticElectricityDetector : IHalComponent
    {
        /// <summary>
        /// 讀取[Tactile]壓力 value
        /// </summary>
        /// <returns></returns>
        float Get();
    }
}
