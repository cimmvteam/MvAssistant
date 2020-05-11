using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MvAssistant.Mac.v1_0.Hal.CompPlc
{
    [Guid("614E49F2-1F21-400D-ABAE-715C494A5654")]
    public class MacHalPlcCabinet : MacHalPlcBase, IMacHalPlcCabinet
    {
  

        public MacHalPlcCabinet() { }
        public MacHalPlcCabinet(MacHalPlcContext plc = null)
        {
            this.m_PlcContext = plc;
        }

      






        #region 壓差表
        //設定Cabinet內部與外部環境最大壓差限制
        public void SetPressureDiffLimit(uint Gauge1Limit, uint Gauge2Limit)
        {
            var plc = this.m_PlcContext;
            plc.Write(MacHalPlcEnumVariable.PC_TO_DB_DP1Limit, Gauge1Limit);
            plc.Write(MacHalPlcEnumVariable.PC_TO_DB_DP2Limit, Gauge2Limit);
        }

        //讀取Cabinet內部與外部環境最大壓差限制
        public Tuple<int, int> ReadPressureDiffLimitSetting()
        {
            var plc = this.m_PlcContext;
            return new Tuple<int, int>(
                plc.Read<int>(MacHalPlcEnumVariable.PC_TO_DB_DP1Limit),
                plc.Read<int>(MacHalPlcEnumVariable.PC_TO_DB_DP2Limit)
                );
        }

        //讀取Cabinet內部與外部環境壓差
        public Tuple<int, int> ReadPressureDiff()
        {
            var plc = this.m_PlcContext;
            return new Tuple<int, int>(
                plc.Read<int>(MacHalPlcEnumVariable.DB_TO_PC_DP1),
                plc.Read<int>(MacHalPlcEnumVariable.DB_TO_PC_DP2)
                );
        }
        #endregion

        #region 節流閥
        //設定節流閥開啟大小
        public void SetExhaustFlow(int Valve1, int Valve2)
        {
            var plc = this.m_PlcContext;
            plc.Write(MacHalPlcEnumVariable.PC_TO_DB_Exhaust1, Valve1);
            plc.Write(MacHalPlcEnumVariable.PC_TO_DB_Exhaust2, Valve2);
        }

        //設定節流閥開啟大小
        public Tuple<int, int> ReadExhaustFlowSetting()
        {
            var plc = this.m_PlcContext;
            return new Tuple<int, int>(
            plc.Read<int>(MacHalPlcEnumVariable.PC_TO_DB_Exhaust1),
            plc.Read<int>(MacHalPlcEnumVariable.PC_TO_DB_Exhaust2)
            );
        }
        #endregion

        //讀取光閘是否遮斷，一排一個 各自獨立，遮斷時True，Reset time 500ms
        public Tuple<bool, bool, bool, bool, bool, bool, bool> ReadLightCurtain()
        {
            var plc = this.m_PlcContext;
            return new Tuple<bool, bool, bool, bool, bool, bool, bool>(
            plc.Read<bool>(MacHalPlcEnumVariable.DR_TO_PC_Area1),
            plc.Read<bool>(MacHalPlcEnumVariable.DR_TO_PC_Area2),
            plc.Read<bool>(MacHalPlcEnumVariable.DR_TO_PC_Area3),
            plc.Read<bool>(MacHalPlcEnumVariable.DR_TO_PC_Area4),
            plc.Read<bool>(MacHalPlcEnumVariable.DR_TO_PC_Area5),
            plc.Read<bool>(MacHalPlcEnumVariable.DR_TO_PC_Area6),
            plc.Read<bool>(MacHalPlcEnumVariable.DR_TO_PC_Area7)
            );
        }
    }
}
