using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.Mac.Hal.CompPlc
{
    public interface IMacHalPlcCabinet : IMacHalPlcBase
    {

        #region Set Parameter
        void SetPressureDiffLimit(uint? Gauge1Limit, uint? Gauge2Limit);

        void SetExhaustFlow(int? Valve1, int? Valve2);
        #endregion

        #region Read Parameter
        Tuple<int, int> ReadPressureDiffLimitSetting();

        Tuple<int, int> ReadExhaustFlowSetting();
        #endregion

        #region Read Component Value
        Tuple<int, int> ReadPressureDiff();

        Tuple<bool, bool, bool, bool, bool, bool, bool> ReadLightCurtain();
        #endregion
    }
}
