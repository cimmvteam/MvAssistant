using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MvAssistant.MaskTool_v0_1.Plc
{
    public class MvPlcCleanCh
    {
        private MvPlcContext m_PlcContext;

        public MvPlcCleanCh(MvPlcContext plc)
        {
            this.m_PlcContext = plc;
        }

        public Tuple<int, int, int> ParticleCount(uint L_Limit, uint M_Limit, uint S_Limit)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_CC_PD_L_Limit, L_Limit);
            plc.Write(MvEnumPlcVariable.PC_TO_CC_PD_M_Limit, M_Limit);
            plc.Write(MvEnumPlcVariable.PC_TO_CC_PD_S_Limit, S_Limit);

            return new Tuple<int, int, int>(
                plc.Read<int>(MvEnumPlcVariable.CC_TO_PC_PD_L),
                plc.Read<int>(MvEnumPlcVariable.CC_TO_PC_PD_M),
                plc.Read<int>(MvEnumPlcVariable.CC_TO_PC_PD_S)
                );
        }

        //確認Mask水平
        public Tuple<double, double, double> CheckMaskLevel()
        {
            var plc = this.m_PlcContext;

            return new Tuple<double, double, double>(
                plc.Read<double>(MvEnumPlcVariable.CC_TO_PC_MaskLevel1),
                plc.Read<double>(MvEnumPlcVariable.CC_TO_PC_MaskLevel2),
                plc.Read<double>(MvEnumPlcVariable.CC_TO_PC_MaskLevel3)
                );
        }

        //檢測Robot入侵位置(左右)
        public double RobotPosAbout(double Limit_R, double Limit_L)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_CC_Robot_AboutLimit_R, Limit_R);
            plc.Write(MvEnumPlcVariable.PC_TO_CC_Robot_AboutLimit_L, Limit_L);

            return plc.Read<double>(MvEnumPlcVariable.CC_TO_PC_RobotPosition_About);
        }

        //檢測Robot入侵位置(上下)
        public double RobotPosUpDown(double Limit_U, double Limit_D)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_CC_Robot_UpDownLimit_U, Limit_U);
            plc.Write(MvEnumPlcVariable.PC_TO_CC_Robot_UpDownLimit_D, Limit_D);

            return plc.Read<double>(MvEnumPlcVariable.CC_TO_PC_RobotPosition_UpDown);
        }

        //為壓差計
        public int PressureGauge(uint PressureLimit)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_CC_DP_Limit, PressureLimit);

            return plc.Read<int>(MvEnumPlcVariable.CC_TO_PC_DP);
        }

        //空氣閥吹風
        public bool GasValveBlow(uint BlowTime)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_CC_BlowTime, BlowTime);
            plc.Write(MvEnumPlcVariable.PC_TO_CC_Blow, false);
            Thread.Sleep(100);
            plc.Write(MvEnumPlcVariable.PC_TO_CC_Blow, true);

            if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.CC_TO_PC_Blow_Reply), 1000))
                throw new MvException("Open Stage Lock/Unlock T0 timeout");
            else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.CC_TO_PC_Blow_Complete), 5000))
                throw new MvException("Open Stage Lock/Unlock T2 timeout");

            plc.Write(MvEnumPlcVariable.PC_TO_CC_Blow, false);

            if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.CC_TO_PC_Blow_Complete), 1000))
                throw new MvException("Open Stage Lock/Unlock T4 timeout");
            
            return plc.Read<bool>(MvEnumPlcVariable.CC_TO_PC_Blow_Output);
        }

        //氣壓控制
        public double PressureCtl(double AirPressure)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_CC_PressureControl, AirPressure);

            return plc.Read<double>(MvEnumPlcVariable.CC_TO_PressureControl);
        }

        //確認壓力表數值
        public double CheckPressure()
        {
            var plc = this.m_PlcContext;

            return plc.Read<double>(MvEnumPlcVariable.CC_TO_PC_Pressure);
        }

        //一排一個 各自獨立，遮斷時True，Reset time 500ms
        public Tuple<bool, bool, bool> CheckAreaSensor()
        {
            var plc = this.m_PlcContext;

            return new Tuple<bool, bool, bool>(
            plc.Read<bool>(MvEnumPlcVariable.CC_TO_PC_Area1),
            plc.Read<bool>(MvEnumPlcVariable.CC_TO_PC_Area2),
            plc.Read<bool>(MvEnumPlcVariable.CC_TO_PC_Area3)
            );
        }
    }
}
