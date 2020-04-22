﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Intf.Component.Infrared
{
    [GuidAttribute("DB7A2EE2-A811-454D-ADC8-CDB71FCBAF84")]
    public interface IHalInfraredPhotointerrupter : IHalComponent
    {
        /// <summary>
        /// 讀取[光遮斷] value
        /// </summary>
        /// <returns></returns>
        float GetValue();
    }
}
