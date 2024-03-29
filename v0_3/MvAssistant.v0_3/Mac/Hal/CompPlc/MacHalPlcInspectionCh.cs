﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MvAssistant.v0_3.Mac.Hal.CompPlc
{
    [Guid("56D4923B-F3CC-46C9-BFC5-9ACD13E1897A")]
    public class MacHalPlcInspectionCh : MacHalPlcBase, IMacHalPlcInspectionCh
    {


        public MacHalPlcInspectionCh() { }
        public MacHalPlcInspectionCh(MacHalPlcContext plc = null)
        {
            this.plcContext = plc;
        }






        //Stage進行XY移動
        public string XYPosition(double? X_Position, double? Y_Position)
        {
            string Result = "";
            var plc = this.plcContext;
            try
            {
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_XYCmd, false);
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_ZCmd, false);
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_WCmd, false);



                double originalX = plc.Read<double>(EnumMacHalPlcVariable.IC_TO_PC_Positon_X);
                double originalY = plc.Read<double>(EnumMacHalPlcVariable.IC_TO_PC_Positon_Y);

                if (X_Position != null)
                    plc.Write(EnumMacHalPlcVariable.PC_TO_IC_XPoint, X_Position);
                if (Y_Position != null)
                    plc.Write(EnumMacHalPlcVariable.PC_TO_IC_YPoint, Y_Position);
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_XYCmd, false);
                Thread.Sleep(100);
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_XYCmd, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(EnumMacHalPlcVariable.IC_TO_PC_XYReply), 1000))
                    throw new MvaException("Inspection Chamber XY-axis Move T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(EnumMacHalPlcVariable.IC_TO_PC_XYComplete), 10 * 1000))
                    throw new MvaException("Inspection Chamber XY-axis Move T2 timeout");

                switch (plc.Read<int>(EnumMacHalPlcVariable.IC_TO_PC_XYResult))
                {
                    case 0:
                        throw new MvaException("Inspection Chamber XY-axis Move Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        plc.Write(EnumMacHalPlcVariable.PC_TO_IC_XPoint, originalX);//當帶入的數值超出範圍，恢復成原先的數值
                        plc.Write(EnumMacHalPlcVariable.PC_TO_IC_YPoint, originalY);//當帶入的數值超出範圍，恢復成原先的數值
                        throw new MvaException("Inspection Chamber XY-axis Move Error : ABS point out range");
                    case 5:
                        Result = "ABS point not change";
                        break;
                    default:
                        throw new MvaException("Inspection Chamber XY-axis Move Error : Unknown error");
                }

                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_XYCmd, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(EnumMacHalPlcVariable.IC_TO_PC_XYComplete), 1000))
                    throw new MvaException("Inspection Chamber XY-axis Move T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_XYCmd, false);
                throw ex;
            }
            return Result;
        }

        //CCD高度變更
        public string ZPosition(double Z_Position)
        {
            string Result = "";
            var plc = this.plcContext;
            try
            {
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_ZPoint, Z_Position);
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_ZCmd, false);
                Thread.Sleep(100);
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_ZCmd, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(EnumMacHalPlcVariable.IC_TO_PC_ZReply), 1000))
                    throw new MvaException("Inspection Chamber Z-axis Move T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(EnumMacHalPlcVariable.IC_TO_PC_ZComplete), 5000))
                    throw new MvaException("Inspection Chamber Z-axis Move T2 timeout");

                switch (plc.Read<int>(EnumMacHalPlcVariable.IC_TO_PC_ZResult))
                {
                    case 0:
                        throw new MvaException("Inspection Chamber Z-axis Move Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        throw new MvaException("Inspection Chamber Z-axis Move Error : ABS point out range");
                    case 5:
                        Result = "ABS point not change";
                        break;
                    default:
                        throw new MvaException("Inspection Chamber Z-axis Move Error : Unknown error");
                }

                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_ZCmd, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(EnumMacHalPlcVariable.IC_TO_PC_ZComplete), 1000))
                    throw new MvaException("Inspection Chamber Z-axis Move T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_ZCmd, false);
                throw ex;
            }
            return Result;
        }

        //Mask方向旋轉
        public string WPosition(double W_Position)
        {
            string Result = "";
            var plc = this.plcContext;
            try
            {
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_WPoint, W_Position);
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_WCmd, false);
                Thread.Sleep(100);
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_WCmd, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(EnumMacHalPlcVariable.IC_TO_PC_WReply), 1000))
                    throw new MvaException("Inspection Chamber W-axis T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(EnumMacHalPlcVariable.IC_TO_PC_WComplete), 5000))
                    throw new MvaException("Inspection Chamber W-axis T2 timeout");

                switch (plc.Read<int>(EnumMacHalPlcVariable.IC_TO_PC_WResult))
                {
                    case 0:
                        throw new MvaException("Inspection Chamber W-axis Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        throw new MvaException("Inspection Chamber W-axis Error : ABS point out range");
                    case 5:
                        Result = "ABS point not change";
                        break;
                    default:
                        throw new MvaException("Inspection Chamber W-axis Error : Unknown error");
                }

                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_WCmd, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(EnumMacHalPlcVariable.IC_TO_PC_WComplete), 1000))
                    throw new MvaException("Inspection Chamber W-axis T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_WCmd, false);
                throw ex;
            }
            return Result;
        }

        public string Initial()
        {
            string Result = "";
            var plc = this.plcContext;
            try
            {
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_Initial_A06, false);
                Thread.Sleep(100);
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_Initial_A06, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(EnumMacHalPlcVariable.IC_TO_PC_Initial_A06_Reply), 1000))
                    throw new MvaException("Inspection Chamber Initial T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(EnumMacHalPlcVariable.IC_TO_PC_Initial_A06_Complete), 120 * 1000))
                    throw new MvaException("Inspection Chamber Initial T2 timeout");

                switch (plc.Read<int>(EnumMacHalPlcVariable.IC_TO_PC_Initial_A06_Result))
                {
                    case 0:
                        throw new MvaException("Inspection Chamber Initial Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    default:
                        throw new MvaException("Inspection Chamber Initial Error : Unknown error");
                }

                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_Initial_A06, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(EnumMacHalPlcVariable.IC_TO_PC_Initial_A06_Complete), 1000))
                    throw new MvaException("Inspection Chamber Initial T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_Initial_A06, false);
                throw ex;
            }
            return Result;
        }

        public void SetSpeedVar(double? StageXYSpeed, double? CcdZSpeed, double? MaskWSpeed)
        {
            var plc = this.plcContext;
            if (StageXYSpeed != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_XY_Speed, StageXYSpeed);// (mm/sec)
            if (CcdZSpeed != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_Z_Speed, CcdZSpeed);// (mm/sec)
            if (MaskWSpeed != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_W_Speed, MaskWSpeed);// angle per second(degree/sec)
        }

        #region Particle數量監控
        //設定各種大小Particle的數量限制
        public void SetParticleCntLimit(uint? L_Limit, uint? M_Limit, uint? S_Limit)
        {
            var plc = this.plcContext;

            if (L_Limit != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_PD_L_Limit, L_Limit);
            if (M_Limit != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_PD_M_Limit, M_Limit);
            if (S_Limit != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_PD_S_Limit, S_Limit);
        }

        //讀取各種大小Particle的數量限制
        public Tuple<int, int, int> ReadParticleCntLimit()
        {
            var plc = this.plcContext;

            return new Tuple<int, int, int>(
                plc.Read<int>(EnumMacHalPlcVariable.PC_TO_IC_PD_L_Limit),
                plc.Read<int>(EnumMacHalPlcVariable.PC_TO_IC_PD_M_Limit),
                plc.Read<int>(EnumMacHalPlcVariable.PC_TO_IC_PD_S_Limit)
                );
        }

        //讀取各種大小Particle的數量
        public Tuple<int, int, int> ReadParticleCount()
        {
            var plc = this.plcContext;

            return new Tuple<int, int, int>(
                plc.Read<int>(EnumMacHalPlcVariable.IC_TO_PC_PD_L),
                plc.Read<int>(EnumMacHalPlcVariable.IC_TO_PC_PD_M),
                plc.Read<int>(EnumMacHalPlcVariable.IC_TO_PC_PD_S)
                );
        }
        #endregion Particle數量監控

        #region 微壓差計
        //設定壓差極限值
        public void SetChamberPressureDiffLimit(uint? GaugeLimit)
        {
            var plc = this.plcContext;

            if (GaugeLimit != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_DP_Limit, GaugeLimit);
        }

        //讀取壓差極限值
        public int ReadChamberPressureDiffLimit()
        {
            var plc = this.plcContext;

            return plc.Read<int>(EnumMacHalPlcVariable.PC_TO_IC_DP_Limit);
        }

        //讀取實際壓差
        public int ReadChamberPressureDiff()
        {
            var plc = this.plcContext;

            return plc.Read<int>(EnumMacHalPlcVariable.IC_TO_PC_DP);
        }
        #endregion 微壓差計

        //讀取速度設定
        public Tuple<double, double, double> ReadSpeedVar()
        {
            var plc = this.plcContext;

            return new Tuple<double, double, double>(
                plc.Read<double>(EnumMacHalPlcVariable.PC_TO_IC_XY_Speed),
                plc.Read<double>(EnumMacHalPlcVariable.PC_TO_IC_Z_Speed),
                plc.Read<double>(EnumMacHalPlcVariable.PC_TO_IC_W_Speed)
                );
        }

        //讀取Robot入侵
        public bool SetRobotIntrude(bool isIntrude)
        {
            var plc = this.plcContext;
            try
            {
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_RobotIntrude, !isIntrude);
                Thread.Sleep(100);
                if (plc.Read<bool>(EnumMacHalPlcVariable.IC_TO_PC_RobotLicense) != isIntrude)//如果BT要入侵但不被許可
                    throw new MvaException("Mask Transfer Intrude is not allowed");
            }
            catch (Exception ex)
            {
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_RobotIntrude, true);//復歸入侵請求，因為訊號是反向觸發所以復歸成 True
                throw ex;
            }
            return plc.Read<bool>(EnumMacHalPlcVariable.IC_TO_PC_RobotLicense);
        }

        public bool ReadRobotIntruded()
        {
            var plc = this.plcContext;
            return !plc.Read<bool>(EnumMacHalPlcVariable.PC_TO_IC_RobotIntrude);
        }

        //讀取 XY Stage位置
        public Tuple<double, double> ReadXYPosition()
        {
            var plc = this.plcContext;

            return new Tuple<double, double>(
                plc.Read<double>(EnumMacHalPlcVariable.IC_TO_PC_Positon_X),
                plc.Read<double>(EnumMacHalPlcVariable.IC_TO_PC_Positon_Y)
                );
        }

        //讀取 CCD Z軸位置
        public double ReadZPosition()
        {
            var plc = this.plcContext;

            return plc.Read<double>(EnumMacHalPlcVariable.IC_TO_PC_Positon_Z);
        }

        //讀取旋轉位置
        public double ReadWPosition()
        {
            var plc = this.plcContext;

            return plc.Read<double>(EnumMacHalPlcVariable.IC_TO_PC_Positon_W);
        }

        #region 手臂入侵(左右)
        //設定手臂可侵入的左右區間極限值
        public void SetRobotPosLeftRightLimit(double? AboutLimit_L, double? AboutLimit_R)
        {
            var plc = this.plcContext;

            if (AboutLimit_R != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_Robot_AboutLimit_R, AboutLimit_R);
            if (AboutLimit_L != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_Robot_AboutLimit_L, AboutLimit_L);
        }

        //讀取手臂可侵入的左右區間極限值
        public Tuple<double, double> ReadRobotPosLeftRightLimit()
        {
            var plc = this.plcContext;

            return new Tuple<double, double>(
                plc.Read<double>(EnumMacHalPlcVariable.PC_TO_IC_Robot_AboutLimit_L),
                plc.Read<double>(EnumMacHalPlcVariable.PC_TO_IC_Robot_AboutLimit_R)
                );
        }

        //讀取Robot侵入位置(左右)
        public double ReadRobotPosLeftRight()
        {
            var plc = this.plcContext;

            return plc.Read<double>(EnumMacHalPlcVariable.IC_TO_PC_RobotPosition_About);
        }
        #endregion

        #region 手臂入侵(上下)
        //設定手臂可侵入的上下區間極限值
        public void SetRobotPosUpDownLimit(double? UpDownLimit_U, double? UpDownLimit_D)
        {
            var plc = this.plcContext;

            if (UpDownLimit_U != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_Robot_UpDownLimit_U, UpDownLimit_U);
            if (UpDownLimit_D != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_IC_Robot_UpDownLimit_D, UpDownLimit_D);
        }

        //讀取手臂可侵入的上下區間極限值
        public Tuple<double, double> ReadRobotPosUpDownLimit()
        {
            var plc = this.plcContext;

            return new Tuple<double, double>(
                plc.Read<double>(EnumMacHalPlcVariable.PC_TO_IC_Robot_UpDownLimit_U),
                plc.Read<double>(EnumMacHalPlcVariable.PC_TO_IC_Robot_UpDownLimit_D)
                );
        }

        //讀取Robot侵入位置(上下)
        public double ReadRobotPosUpDown()
        {
            var plc = this.plcContext;

            return plc.Read<double>(EnumMacHalPlcVariable.IC_TO_PC_RobotPosition_UpDown);
        }
        #endregion

        public EnumMacPlcAssemblyStatus ReadICStatus()
        {
            var plc = this.plcContext;
            var status = plc.Read<int>(EnumMacHalPlcVariable.IC_TO_PC_A06Status);
            return (EnumMacPlcAssemblyStatus)status;
        }
    }
}
