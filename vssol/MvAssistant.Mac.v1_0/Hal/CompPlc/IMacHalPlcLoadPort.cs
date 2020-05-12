using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompPlc
{
    public interface IMacHalPlcLoadPort
    {
        #region Set Parameter
        void SetPressureDiffLimit(uint Gauge1Limit, uint Gauge2Limit);
        #endregion

        #region Read Parameter
        Tuple<int, int> ReadPressureDiffLimitSrtting();
        #endregion

        #region Read Component Value
        Tuple<int, int> ReadPressureDiff();
        #endregion
    }
}
