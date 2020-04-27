using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MvAssistant.Mac.v1_0.Hal.CompPlc
{
    [Guid("17793D56-17D7-4DFD-850B-EEB443CE9231")]
    public class MacHalPlcLoadPort : MacHalComponentBase
    {
        private MacHalPlcContext m_PlcContext;

        public MacHalPlcLoadPort(MacHalPlcContext plc = null)
        {
            this.m_PlcContext = plc;
        }

        //設定壓差極限值
        public void SetPressureDiffLimit(uint Gauge1Limit, uint Gauge2Limit)
        {
            var plc = this.m_PlcContext;

            plc.Write(MacHalPlcEnumVariable.PC_TO_LP_DP1Limit, Gauge1Limit);
            plc.Write(MacHalPlcEnumVariable.PC_TO_LP_DP2Limit, Gauge2Limit);
        }

        //讀取壓差極限值
        public Tuple<int, int> ReadPressureDiffLimitSrtting()
        {
            var plc = this.m_PlcContext;

            return new Tuple<int, int>(
                plc.Read<int>(MacHalPlcEnumVariable.PC_TO_LP_DP1Limit),
                plc.Read<int>(MacHalPlcEnumVariable.PC_TO_LP_DP2Limit)
                );
        }

        //讀取實際壓差
        public Tuple<int, int> ReadPressureDiff()
        {
            var plc = this.m_PlcContext;

            return new Tuple<int, int>(
                plc.Read<int>(MacHalPlcEnumVariable.LP_TO_PC_DP1),
                plc.Read<int>(MacHalPlcEnumVariable.LP_TO_PC_DP2)
                );
        }
    }
}
