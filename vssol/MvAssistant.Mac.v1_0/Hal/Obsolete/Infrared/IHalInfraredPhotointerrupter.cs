using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Infrared
{
    [GuidAttribute("DB7A2EE2-A811-454D-ADC8-CDB71FCBAF84")]
    public interface IHalInfraredPhotointerrupter : IMacHalComponent
    {
        /// <summary>
        /// 讀取[光遮斷] value
        /// </summary>
        /// <returns></returns>
        float GetValue();
    }
}
