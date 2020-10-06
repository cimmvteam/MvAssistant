using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.PresenceDetector
{
    [GuidAttribute("8B925765-EB3D-44F3-B0C2-79CC4934B963")]
    public interface IHalPresenceDetector : IMacHalComponent
    {
        /// <summary>
        /// 壓力感知是否有東西
        /// </summary>
        /// <returns></returns>
        bool HalIsPresent();
    }
}
