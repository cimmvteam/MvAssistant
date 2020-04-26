using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MvAssistant.Mac.v1_0.CompPlc
{
    public class MvPlcLoadPort
    {
        private MvPlcContext m_PlcContext;

        public MvPlcLoadPort(MvPlcContext plc)
        {
            this.m_PlcContext = plc;
        }

        //設定壓差極限值
        public void SetPressureDiffLimit(uint Gauge1Limit, uint Gauge2Limit)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_LP_DP1Limit, Gauge1Limit);
            plc.Write(MvEnumPlcVariable.PC_TO_LP_DP2Limit, Gauge2Limit);
        }

        //讀取壓差極限值
        public Tuple<int, int> ReadPressureDiffLimitSrtting()
        {
            var plc = this.m_PlcContext;

            return new Tuple<int, int>(
                plc.Read<int>(MvEnumPlcVariable.PC_TO_LP_DP1Limit),
                plc.Read<int>(MvEnumPlcVariable.PC_TO_LP_DP2Limit)
                );
        }

        //讀取實際壓差
        public Tuple<int, int> ReadPressureDiff()
        {
            var plc = this.m_PlcContext;

            return new Tuple<int, int>(
                plc.Read<int>(MvEnumPlcVariable.LP_TO_PC_DP1),
                plc.Read<int>(MvEnumPlcVariable.LP_TO_PC_DP2)
                );
        }
    }
}
