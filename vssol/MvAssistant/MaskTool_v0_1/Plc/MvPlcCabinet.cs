using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MvAssistant.MaskTool_v0_1.Plc
{
    public class MvPlcCabinet
    {
        private MvPlcContext m_PlcContext;

        public MvPlcCabinet(MvPlcContext plc)
        {
            this.m_PlcContext = plc;
        }

        #region 壓差表
        //設定Cabinet內部與外部環境最大壓差限制
        public void SetPressureDiffLimit(uint Gauge1Limit, uint Gauge2Limit)
        {
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_DB_DP1Limit, Gauge1Limit);
            plc.Write(MvEnumPlcVariable.PC_TO_DB_DP2Limit, Gauge2Limit);
        }

        //讀取Cabinet內部與外部環境最大壓差限制
        public Tuple<int, int> ReadPressureDiffLimitSetting()
        {
            var plc = this.m_PlcContext;
            return new Tuple<int, int>(
                plc.Read<int>(MvEnumPlcVariable.PC_TO_DB_DP1Limit),
                plc.Read<int>(MvEnumPlcVariable.PC_TO_DB_DP2Limit)
                );
        }

        //讀取Cabinet內部與外部環境壓差
        public Tuple<int, int> ReadPressureDiff()
        {
            var plc = this.m_PlcContext;
            return new Tuple<int, int>(
                plc.Read<int>(MvEnumPlcVariable.DB_TO_PC_DP1),
                plc.Read<int>(MvEnumPlcVariable.DB_TO_PC_DP2)
                );
        }
        #endregion

        #region 節流閥
        //設定節流閥開啟大小
        public void SetExhaustFlow(int Valve1, int Valve2)
        {
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_DB_Exhaust1, Valve1);
            plc.Write(MvEnumPlcVariable.PC_TO_DB_Exhaust2, Valve2);
        }

        //設定節流閥開啟大小
        public Tuple<int, int> ReadExhaustFlowSetting()
        {
            var plc = this.m_PlcContext;
            return new Tuple<int, int>(
            plc.Read<int>(MvEnumPlcVariable.PC_TO_DB_Exhaust1),
            plc.Read<int>(MvEnumPlcVariable.PC_TO_DB_Exhaust2)
            );
        }

        //讀取節流閥實際的開啟大小
        public Tuple<int, int> ReadExhaustFlow()
        {
            var plc = this.m_PlcContext;
            return new Tuple<int, int>(
                plc.Read<int>(MvEnumPlcVariable.DR_Analog_Output_Exhaust_1),
                plc.Read<int>(MvEnumPlcVariable.DR_Analog_Output_Exhaust_2)
                );
        }
        #endregion

        //讀取光閘是否遮斷，一排一個 各自獨立，遮斷時True，Reset time 500ms
        public Tuple<bool, bool, bool, bool, bool, bool, bool> ReadAreaSensor()
        {
            var plc = this.m_PlcContext;
            return new Tuple<bool, bool, bool, bool, bool, bool, bool>(
            plc.Read<bool>(MvEnumPlcVariable.DR_TO_PC_Area1),
            plc.Read<bool>(MvEnumPlcVariable.DR_TO_PC_Area2),
            plc.Read<bool>(MvEnumPlcVariable.DR_TO_PC_Area3),
            plc.Read<bool>(MvEnumPlcVariable.DR_TO_PC_Area4),
            plc.Read<bool>(MvEnumPlcVariable.DR_TO_PC_Area5),
            plc.Read<bool>(MvEnumPlcVariable.DR_TO_PC_Area6),
            plc.Read<bool>(MvEnumPlcVariable.DR_TO_PC_Area7)
            );
        }
    }
}
