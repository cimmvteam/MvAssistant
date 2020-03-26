using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvAssistant.MaskTool_v0_1.Plc
{
    public class MvPlcCabinet
    {
        private MvPlcContext m_PlcContext;

        public MvPlcCabinet(MvPlcContext plc)
        {
            this.m_PlcContext = plc;
        }

        //壓差表數值
        public Tuple<int, int> PressureGauge(uint Gauge1, uint Gauge2)
        {
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_DB_DP1Limit, Gauge1);
            plc.Write(MvEnumPlcVariable.PC_TO_DB_DP2Limit, Gauge2);

            return new Tuple<int, int>(
                plc.Read<int>(MvEnumPlcVariable.DB_TO_PC_DP1),
                plc.Read<int>(MvEnumPlcVariable.DB_TO_PC_DP2)
                );
        }

        //節流閥回授訊號
        public Tuple<int, int> ExhaustValve(int Valve1, int Valve2)
        {
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_DB_Exhaust1, Valve1);
            plc.Write(MvEnumPlcVariable.PC_TO_DB_Exhaust2, Valve2);

            return new Tuple<int, int>(
                plc.Read<int>(MvEnumPlcVariable.DR_Analog_Output_Exhaust_1),
                plc.Read<int>(MvEnumPlcVariable.DR_Analog_Output_Exhaust_2)
                );
        }

        //信號燈
        public void SignalTower(bool Red, bool Orange, bool Blue)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_DR_Red, Red);
            plc.Write(MvEnumPlcVariable.PC_TO_DR_Orange, Orange);
            plc.Write(MvEnumPlcVariable.PC_TO_DR_Blue, Blue);
        }

        //蜂鳴器
        public void Buzzer(uint BuzzerType)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_DR_Buzzer, BuzzerType);
        }

        //一排一個 各自獨立，遮斷時True，Reset time 500ms
        public Tuple<bool, bool, bool, bool, bool, bool, bool> CheckAreaSensor()
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
