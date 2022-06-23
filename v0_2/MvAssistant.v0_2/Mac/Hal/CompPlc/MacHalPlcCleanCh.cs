using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MvAssistant.v0_2.Mac.Hal.CompPlc
{
    [Guid("2CA853C0-DE8E-49D5-AF55-02DD20754F3D")]
    public class MacHalPlcCleanCh : MacHalPlcBase, IMacHalPlcCleanCh
    {


        public MacHalPlcCleanCh() { }
        public MacHalPlcCleanCh(MacHalPlcContext plc = null)
        {
            this.plcContext = plc;
        }







        #region Particle數量監控
        //設定各種大小Particle的數量限制
        public void SetParticleCntLimit(uint? L_Limit, uint? M_Limit, uint? S_Limit)
        {
            var plc = this.plcContext;

            if (L_Limit != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_CC_PD_L_Limit, L_Limit);
            if (M_Limit != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_CC_PD_M_Limit, M_Limit);
            if (S_Limit != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_CC_PD_S_Limit, S_Limit);
        }

        //讀取各種大小Particle的數量限制
        public Tuple<int, int, int> ReadParticleCntLimitSetting()
        {
            var plc = this.plcContext;

            return new Tuple<int, int, int>(
                plc.Read<int>(EnumMacHalPlcVariable.PC_TO_CC_PD_L_Limit),
                plc.Read<int>(EnumMacHalPlcVariable.PC_TO_CC_PD_M_Limit),
                plc.Read<int>(EnumMacHalPlcVariable.PC_TO_CC_PD_S_Limit)
                );
        }

        //讀取各種大小Particle的數量
        public Tuple<int, int, int> ReadParticleCount()
        {
            var plc = this.plcContext;

            return new Tuple<int, int, int>(
                plc.Read<int>(EnumMacHalPlcVariable.CC_TO_PC_PD_L),
                plc.Read<int>(EnumMacHalPlcVariable.CC_TO_PC_PD_M),
                plc.Read<int>(EnumMacHalPlcVariable.CC_TO_PC_PD_S)
                );
        }
        #endregion Particle數量監控

        //讀取Mask水平
        public Tuple<double, double, double> ReadMaskLevel()
        {
            var plc = this.plcContext;

            return new Tuple<double, double, double>(
                plc.Read<double>(EnumMacHalPlcVariable.CC_TO_PC_MaskLevel1),
                plc.Read<double>(EnumMacHalPlcVariable.CC_TO_PC_MaskLevel2),
                plc.Read<double>(EnumMacHalPlcVariable.CC_TO_PC_MaskLevel3)
                );
        }

        #region 手臂侵入(左右)
        //設定手臂可侵入的左右區間極限值
        public void SetRobotAboutLimit(double? Limit_L, double? Limit_R)
        {
            var plc = this.plcContext;

            if (Limit_L != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_CC_Robot_AboutLimit_L, Limit_L);
            if (Limit_R != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_CC_Robot_AboutLimit_R, Limit_R);
        }

        //讀取手臂可侵入的左右區間極限值
        public Tuple<double, double> ReadRobotAboutLimitSetting()
        {
            var plc = this.plcContext;

            return new Tuple<double, double>(
                        plc.Read<double>(EnumMacHalPlcVariable.PC_TO_CC_Robot_AboutLimit_L),
                        plc.Read<double>(EnumMacHalPlcVariable.PC_TO_CC_Robot_AboutLimit_R)
                        );
        }

        //讀取Robot入侵位置(左右)
        public double ReadRobotPosAbout()
        {
            var plc = this.plcContext;

            return plc.Read<double>(EnumMacHalPlcVariable.CC_TO_PC_RobotPosition_About);
        }
        #endregion

        #region 手臂侵入(上下)
        //設定手臂可侵入的上下區間極限值
        public void SetRobotUpDownLimit(double? Limit_U, double? Limit_D)
        {
            var plc = this.plcContext;

            if (Limit_U != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_CC_Robot_UpDownLimit_U, Limit_U);
            if (Limit_D != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_CC_Robot_UpDownLimit_D, Limit_D);
        }

        //讀取手臂可侵入的上下區間極限值
        public Tuple<double, double> ReadRobotUpDownLimitSetting()
        {
            var plc = this.plcContext;

            return new Tuple<double, double>(
                        plc.Read<double>(EnumMacHalPlcVariable.PC_TO_CC_Robot_UpDownLimit_U),
                        plc.Read<double>(EnumMacHalPlcVariable.PC_TO_CC_Robot_UpDownLimit_D)
                        );
        }

        //讀取Robot入侵位置(上下)
        public double ReadRobotPosUpDown()
        {
            var plc = this.plcContext;

            return plc.Read<double>(EnumMacHalPlcVariable.CC_TO_PC_RobotPosition_UpDown);
        }
        #endregion

        #region 壓差表
        //設定壓力表壓差限制
        public void SetPressureDiffLimit(uint PressureLimit)
        {
            var plc = this.plcContext;

            plc.Write(EnumMacHalPlcVariable.PC_TO_CC_DP_Limit, PressureLimit);
        }

        //讀取壓力表壓差限制設定
        public int ReadPressureDiffLimitSetting()
        {
            var plc = this.plcContext;

            return plc.Read<int>(EnumMacHalPlcVariable.PC_TO_CC_DP_Limit);
        }

        //讀取實際壓差
        public int ReadPressureDiff()
        {
            var plc = this.plcContext;

            return plc.Read<int>(EnumMacHalPlcVariable.CC_TO_PC_DP);
        }
        #endregion

        //空氣閥吹風(BlowTime單位為100ms)
        public string GasValveBlow(uint BlowTime)
        {
            string Result = "";
            var plc = this.plcContext;
            try
            {
                plc.Write(EnumMacHalPlcVariable.PC_TO_CC_BlowTime, BlowTime);
                plc.Write(EnumMacHalPlcVariable.PC_TO_CC_Blow, false);
                Thread.Sleep(100);
                plc.Write(EnumMacHalPlcVariable.PC_TO_CC_Blow, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(EnumMacHalPlcVariable.CC_TO_PC_Blow_Reply), 1000))
                    throw new MvaException("Clean Chamber Gas Valve Blow T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(EnumMacHalPlcVariable.CC_TO_PC_Blow_Complete), 1000 + (int)BlowTime * 100))
                    throw new MvaException("Clean Chamber Gas Valve Blow T2 timeout");

                switch (plc.Read<int>(EnumMacHalPlcVariable.CC_TO_PC_Blow_Result))
                {
                    case 0:
                        throw new MvaException("Clean Chamber Gas Valve Blow Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    default:
                        throw new MvaException("Clean Chamber Gas Valve Blow Error : Unknown error");
                }

                plc.Write(EnumMacHalPlcVariable.PC_TO_CC_Blow, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(EnumMacHalPlcVariable.CC_TO_PC_Blow_Complete), 1000))
                    throw new MvaException("Clean Chamber Gas Valve Blow T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(EnumMacHalPlcVariable.PC_TO_CC_BlowTime, 0);
                plc.Write(EnumMacHalPlcVariable.PC_TO_CC_Blow, false);
                throw ex;
            }
            return Result;
        }

        #region 吹氣壓力控制
        /// <summary> 設定吹氣壓力值, 預設為25 </summary>
        public void SetPressureCtrl(double AirPressure=25)
        {
            var plc = this.plcContext;

            plc.Write(EnumMacHalPlcVariable.PC_TO_CC_PressureControl, AirPressure);
        }

        //讀取吹氣壓力設定值
        public double ReadPressureCtrlSetting()
        {
            var plc = this.plcContext;

            return plc.Read<double>(EnumMacHalPlcVariable.PC_TO_CC_PressureControl);
        }

        //讀取實際吹氣壓力
        public Single ReadBlowPressure()
        {
            var plc = this.plcContext;

            return plc.Read<Single>(EnumMacHalPlcVariable.CC_TO_PC_PressureControl);
        }
        #endregion

        //讀取壓力表數值
        public double ReadPressure()
        {
            var plc = this.plcContext;

            return plc.Read<double>(EnumMacHalPlcVariable.CC_TO_PC_Pressure);
        }

        //讀取光閘，一排一個 各自獨立，遮斷時True，Reset time 500ms
        public Tuple<bool, bool, bool, bool> ReadLightCurtain()
        {
            var plc = this.plcContext;

            return new Tuple<bool, bool, bool, bool>(
            plc.Read<bool>(EnumMacHalPlcVariable.CC_TO_PC_Area1),//Right
            plc.Read<bool>(EnumMacHalPlcVariable.CC_TO_PC_Area2),//Front
            plc.Read<bool>(EnumMacHalPlcVariable.CC_TO_PC_Area3),//Left
            plc.Read<bool>(EnumMacHalPlcVariable.CC_TO_PC_Area4)
            );
        }
    }
}
