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

<<<<<<< HEAD
        public Tuple<uint, uint, uint> ParticleCount(uint L_Limit, uint M_Limit, uint S_Limit)
=======
        #region"Particle數量監控"
        //設定各種大小Particle的數量限制
        public void SetParticleCntLimit(uint L_Limit, uint M_Limit, uint S_Limit)
>>>>>>> 7d76a0e8abe70a0aea1c750ff12f3c502bf5a150
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_CC_PD_L_Limit, L_Limit);
            plc.Write(MvEnumPlcVariable.PC_TO_CC_PD_M_Limit, M_Limit);
            plc.Write(MvEnumPlcVariable.PC_TO_CC_PD_S_Limit, S_Limit);
<<<<<<< HEAD

            return new Tuple<uint, uint, uint>(
                plc.Read<uint>(MvEnumPlcVariable.CC_TO_PC_PD_L),
                plc.Read<uint>(MvEnumPlcVariable.CC_TO_PC_PD_M),
                plc.Read<uint>(MvEnumPlcVariable.CC_TO_PC_PD_S)
=======
        }

        //讀取各種大小Particle的數量限制
        public Tuple<int, int, int> ReadParticleCntLimitSetting()
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
>>>>>>> 7d76a0e8abe70a0aea1c750ff12f3c502bf5a150
                );
        }
        #endregion

        //讀取Mask水平
        public Tuple<double, double, double> ReadMaskLevel()
        {
            var plc = this.m_PlcContext;

            return new Tuple<double, double, double>(
                plc.Read<uint>(MvEnumPlcVariable.CC_TO_PC_MaskLevel1),
                plc.Read<uint>(MvEnumPlcVariable.CC_TO_PC_MaskLevel2),
                plc.Read<uint>(MvEnumPlcVariable.CC_TO_PC_MaskLevel3)
                );
        }

        #region 手臂侵入(左右)
        //設定手臂可侵入的左右區間極限值
        public void SetRobotAboutLimit(double Limit_R, double Limit_L)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_CC_Robot_AboutLimit_R, Limit_R);
            plc.Write(MvEnumPlcVariable.PC_TO_CC_Robot_AboutLimit_L, Limit_L);
<<<<<<< HEAD
=======
        }

        //讀取手臂可侵入的左右區間極限值
        public Tuple<double, double> ReadRobotAboutLimitSetting()
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
>>>>>>> 7d76a0e8abe70a0aea1c750ff12f3c502bf5a150

            return plc.Read<double>(MvEnumPlcVariable.CC_TO_PC_RobotPosition_About);
        }
        #endregion

        #region 手臂侵入(上下)
        //設定手臂可侵入的上下區間極限值
        public void SetRobotUpDownLimit(double Limit_U, double Limit_D)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_CC_Robot_UpDownLimit_U, Limit_U);
            plc.Write(MvEnumPlcVariable.PC_TO_CC_Robot_UpDownLimit_D, Limit_D);
<<<<<<< HEAD
=======
        }

        //讀取手臂可侵入的上下區間極限值
        public Tuple<double, double> ReadRobotUpDownLimitSetting()
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
>>>>>>> 7d76a0e8abe70a0aea1c750ff12f3c502bf5a150

            return plc.Read<double>(MvEnumPlcVariable.CC_TO_PC_RobotPosition_UpDown);
        }
        #endregion

<<<<<<< HEAD
        //為壓差計
        public uint DP(uint DP_Limit)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_CC_DP_Limit,DP_Limit);

            return plc.Read<uint>(MvEnumPlcVariable.CC_TO_PC_DP);
=======
        #region 壓差表
        //設定壓力表壓差限制
        public void SetPressureDiffLimit(uint PressureLimit)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_CC_DP_Limit, PressureLimit);
        }

        //讀取壓力表壓差限制
        public int ReadPressureDiffLimitSetting()
        {
            var plc = this.m_PlcContext;

            return plc.Read<int>(MvEnumPlcVariable.PC_TO_CC_DP_Limit);
        }

        //讀取實際壓差
        public int ReadPressureDiff()
        {
            var plc = this.m_PlcContext;

            return plc.Read<int>(MvEnumPlcVariable.CC_TO_PC_DP);
>>>>>>> 7d76a0e8abe70a0aea1c750ff12f3c502bf5a150
        }
        #endregion

<<<<<<< HEAD
        //空氣閥吹風
        public bool GasValveBlow(uint BlowTime)
=======
        //空氣閥吹風(BlowTime單位為ms)
        public string GasValveBlow(uint BlowTime)
>>>>>>> 7d76a0e8abe70a0aea1c750ff12f3c502bf5a150
        {
            string Result = "";
            var plc = this.m_PlcContext;
<<<<<<< HEAD

            plc.Write(MvEnumPlcVariable.PC_TO_CC_BlowTime, BlowTime);
            plc.Write(MvEnumPlcVariable.PC_TO_CC_Blow, true);

            if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.CC_TO_PC_Blow_Reply), 1000))
                throw new MvException("Open Stage Lock/Unlock T0 timeout");
            else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.CC_TO_PC_Blow_Complete), 5000))
                throw new MvException("Open Stage Lock/Unlock T2 timeout");

            SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.PC_TO_CC_Blow), 1000);

            if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.CC_TO_PC_Blow_Complete), 1000))
                throw new MvException("Open Stage Lock/Unlock T4 timeout");
            
            return plc.Read<bool>(MvEnumPlcVariable.CC_TO_PC_Blow_Output);
=======
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
>>>>>>> 7d76a0e8abe70a0aea1c750ff12f3c502bf5a150
        }

        #region 吹氣壓力控制
        //設定吹氣壓力值
        public void SetPressureCtrl(double AirPressure)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_CC_PressureControl, AirPressure);
<<<<<<< HEAD

            return plc.Read<double>(MvEnumPlcVariable.CC_TO_PressureControl);
=======
>>>>>>> 7d76a0e8abe70a0aea1c750ff12f3c502bf5a150
        }

        //讀取吹氣壓力設定值
        public double ReadPressureCtrlSetting()
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
