using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component
{
    [GuidAttribute("8E060ECD-CBD7-4493-B7E6-AC1A757E7AE0")]
    public interface IHalOpticRuler : IMacHalComponent
    {
        /// <summary>
        /// 讀取[光學尺]位置
        /// </summary>
        /// <returns>光學尺位置, unit: float</returns>
        float GetPosition();
    }
}
