﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Intf.Component.Inclinometer
{
    [GuidAttribute("52E1A19D-D84E-463C-A5CD-8F0946DB5309")]
    public interface IHalInclinometer : IHalComponent
    {
        /// <summary>
        /// 讀取[水平儀]角度 value
        /// </summary>
        /// <returns>角度值</returns>
        object GetAngle();
        void HalZeroCalibration();
    }
}
