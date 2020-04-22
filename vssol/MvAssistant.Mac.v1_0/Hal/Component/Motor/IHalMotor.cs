using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Intf.Component
{
    [GuidAttribute("01F1DF6F-E0A0-4870-9F6A-64239D480AAD")]
    public interface IHalMotor : IHalComponent
    {
        /// <summary>
        /// 移動馬達
        /// </summary>
        /// <returns></returns>
        bool Move();
    }
}
