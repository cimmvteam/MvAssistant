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

        #region"Particle數量監控"
        //設定各種大小Particle的數量限制
        public void SetParticleCntLimit(uint L_Limit, uint M_Limit, uint S_Limit)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_CC_PD_L_Limit, L_Limit);
            plc.Write(MvEnumPlcVariable.PC_TO_CC_PD_M_Limit, M_Limit);
            plc.Write(MvEnumPlcVariable.PC_TO_CC_PD_S_Limit, S_Limit);
        }

        //讀取各種大小Particle的數量限制
        public Tuple<int, int, int> ReadParticleCntLimit()
        {
            var plc = this.m_PlcContext;

            return new Tuple<int, int, int>(
                plc.Read<int>(MvEnumPlcVariable.CC_TO_PC_PD_L),
                plc.Read<int>(MvEnumPlcVariable.CC_TO_PC_PD_M),
                plc.Read<int>(MvEnumPlcVariable.CC_TO_PC_PD_S)
                );
        }

        //讀取各種大小Particle的數量
        public Tuple<int, int, int> ReadParticleCount()
        {
            var plc = this.m_PlcContext;

            return new Tuple<int, int, int>(
                plc.Read<int>(MvEnumPlcVariable.CC_TO_PC_PD_L),
                plc.Read<int>(MvEnumPlcVariable.CC_TO_PC_PD_M),
                plc.Read<int>(MvEnumPlcVariable.CC_TO_PC_PD_S)
                );
        }
        #endregion

        //讀取Mask水平
        public Tuple<double, double, double> ReadMaskLevel()
        {
            var plc = this.m_PlcContext;

            return new Tuple<double, double, double>(
                plc.Read<double>(MvEnumPlcVariable.CC_TO_PC_MaskLevel1),
                plc.Read<double>(MvEnumPlcVariable.CC_TO_PC_MaskLevel2),
                plc.Read<double>(MvEnumPlcVariable.CC_TO_PC_MaskLevel3)
                );
        }

        #region 手臂侵入(左右)
        //設定手臂可侵入的左右區間極限值
        public void SetRobotPosAboutLimit(double Limit_R, double Limit_L)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_CC_Robot_AboutLimit_R, Limit_R);
            plc.Write(MvEnumPlcVariable.PC_TO_CC_Robot_AboutLimit_L, Limit_L);
        }

        //讀取手臂可侵入的左右區間極限值
        public Tuple<double, double> ReadRobotPosAboutLimit()
        {
            var plc = this.m_PlcContext;

            return new Tuple<double, double>(
                        plc.Read<double>(MvEnumPlcVariable.PC_TO_CC_Robot_AboutLimit_R),
                        plc.Read<double>(MvEnumPlcVariable.PC_TO_CC_Robot_AboutLimit_L)
                        );
        }

        //讀取Robot入侵位置(左右)
        public double ReadRobotPosAbout()
        {
            var plc = this.m_PlcContext;

            return plc.Read<double>(MvEnumPlcVariable.CC_TO_PC_RobotPosition_About);
        }
        #endregion

        #region 手臂侵入(上下)
        //設定手臂可侵入的上下區間極限值
        public void SetRobotPosUpDownLimit(double Limit_U, double Limit_D)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_CC_Robot_UpDownLimit_U, Limit_U);
            plc.Write(MvEnumPlcVariable.PC_TO_CC_Robot_UpDownLimit_D, Limit_D);
        }

        //讀取手臂可侵入的上下區間極限值
        public Tuple<double, double> ReadRobotPosUpDownLimit()
        {
            var plc = this.m_PlcContext;

            return new Tuple<double, double>(
                        plc.Read<double>(MvEnumPlcVariable.PC_TO_CC_Robot_UpDownLimit_U),
                        plc.Read<double>(MvEnumPlcVariable.PC_TO_CC_Robot_UpDownLimit_D)
                        );
        }

        //讀取Robot入侵位置(上下)
        public double ReadRobotPosUpDown()
        {
            var plc = this.m_PlcContext;

            return plc.Read<double>(MvEnumPlcVariable.CC_TO_PC_RobotPosition_UpDown);
        }
        #endregion

        #region 壓差表
        //設定壓力表壓差限制
        public void SetPressureDiffLimit(uint PressureLimit)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_CC_DP_Limit, PressureLimit);
        }

        //讀取壓力表壓差限制
        public int ReadPressureDiffLimit()
        {
            var plc = this.m_PlcContext;

            return plc.Read<int>(MvEnumPlcVariable.PC_TO_CC_DP_Limit);
        }

        //讀取實際壓差
        public int ReadPressureDiff()
        {
            var plc = this.m_PlcContext;

            return plc.Read<int>(MvEnumPlcVariable.CC_TO_PC_DP);
        }
        #endregion

        //空氣閥吹風(BlowTime單位為ms)
        public string GasValveBlow(uint BlowTime)
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                plc.Write(MvEnumPlcVariable.PC_TO_CC_BlowTime, BlowTime);
                plc.Write(MvEnumPlcVariable.PC_TO_CC_Blow, false);
                Thread.Sleep(100);
                plc.Write(MvEnumPlcVariable.PC_TO_CC_Blow, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.CC_TO_PC_Blow_Reply), 1000))
                    throw new MvException("Open Stage Lock/Unlock T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.CC_TO_PC_Blow_Complete), 5000))
                    throw new MvException("Open Stage Lock/Unlock T2 timeout");

                switch (plc.Read<int>(MvEnumPlcVariable.CC_TO_PC_Blow_Result))
                {
                    case 0:
                        Result = "Invalid";
                        break;
                    case 1:
                        Result = "Idle";
                        break;
                    case 2:
                        Result = "Busy";
                        break;
                    case 3:
                        Result = "Error";
                        break;
                }

                plc.Write(MvEnumPlcVariable.PC_TO_CC_Blow, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.CC_TO_PC_Blow_Complete), 1000))
                    throw new MvException("Open Stage Lock/Unlock T4 timeout");
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                plc.Write(MvEnumPlcVariable.PC_TO_CC_BlowTime, 0);
                plc.Write(MvEnumPlcVariable.PC_TO_CC_Blow, false);
            }
            return Result;
        }

        #region 吹氣壓力控制
        //設定吹氣壓力值
        public void SetPressureCtrl(double AirPressure)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_CC_PressureControl, AirPressure);
        }

        //讀取吹氣壓力設定值
        public double ReadPressureCtrl()
        {
            var plc = this.m_PlcContext;

            return plc.Read<double>(MvEnumPlcVariable.PC_TO_CC_PressureControl);
        }

        //讀取實際吹氣壓力
        public double ReadBlowPressure()
        {
            var plc = this.m_PlcContext;

            return plc.Read<double>(MvEnumPlcVariable.CC_TO_PC_PressureControl);
        }
        #endregion

        //讀取壓力表數值
        public double ReadPressure()
        {
            var plc = this.m_PlcContext;

            return plc.Read<double>(MvEnumPlcVariable.CC_TO_PC_Pressure);
        }

        //讀取光閘，一排一個 各自獨立，遮斷時True，Reset time 500ms
        public Tuple<bool, bool, bool> ReadAreaSensor()
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
