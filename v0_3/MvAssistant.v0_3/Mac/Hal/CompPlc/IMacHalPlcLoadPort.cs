﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.Mac.Hal.CompPlc
{
    public interface IMacHalPlcLoadPort : IMacHalPlcBase
    {
        #region Set Parameter
        void SetPressureDiffLimit(uint? Gauge1Limit, uint? Gauge2Limit);
        #endregion

        #region Read Parameter
        Tuple<int, int> ReadChamberPressureDiffLimit();
        #endregion

        #region Read Component Value
        Tuple<int, int> ReadPressureDiff();

        bool ReadLP_Light_Curtain();
        #endregion
    }
}
