using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MvAssistant.MaskTool_v0_1.Plc
{
    public class MvPlcLoadPort
    {
        private MvPlcContext m_PlcContext;

        public MvPlcLoadPort(MvPlcContext plc)
        {
            this.m_PlcContext = plc;
        }

        //壓差表數值
        public Tuple<int, int> PressureGauge(uint Gauge1, uint Gauge2)
        {
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_LP_DP1Limit, Gauge1);
            plc.Write(MvEnumPlcVariable.PC_TO_LP_DP2Limit, Gauge2);

            return new Tuple<int, int>(
                plc.Read<int>(MvEnumPlcVariable.LP_TO_PC_DP1),
                plc.Read<int>(MvEnumPlcVariable.LP_TO_PC_DP2)
                );
        }
    }
}
