using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component
{
    [GuidAttribute("7104B93A-9735-4E09-9C65-27818C57E0BD")]
    public interface IHalParticleCounter : IMacHalComponent
    {
        /// <summary>
        /// 取得[Particle Conter] value
        /// </summary>
        /// <returns>particle value</returns>
        float GetParticleValue();
    }
}
