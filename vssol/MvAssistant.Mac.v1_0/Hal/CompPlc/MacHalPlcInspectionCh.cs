using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MvAssistant.Mac.v1_0.Hal.CompPlc
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
                if (X_Position != null)
                    plc.Write(MacHalPlcEnumVariable.PC_TO_IC_XPoint, X_Position);
                if (Y_Position != null)
                    plc.Write(MacHalPlcEnumVariable.PC_TO_IC_YPoint, Y_Position);
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_XYCmd, false);
                Thread.Sleep(100);
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_XYCmd, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.IC_TO_PC_XYReply), 1000))
                    throw new MvException("Inspection Chamber XY-axis Move T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.IC_TO_PC_XYComplete), 10 * 1000))
                    throw new MvException("Inspection Chamber XY-axis Move T2 timeout");

                switch (plc.Read<int>(MacHalPlcEnumVariable.IC_TO_PC_XYResult))
                {
                    case 0:
                        throw new MvException("Inspection Chamber XY-axis Move Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        throw new MvException("Inspection Chamber XY-axis Move Error : ABS point out range");
                    case 5:
                        Result = "ABS point not change";
                        break;
                    default:
                        throw new MvException("Inspection Chamber XY-axis Move Error : Unknown error");
                }

                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_XYCmd, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MacHalPlcEnumVariable.IC_TO_PC_XYComplete), 1000))
                    throw new MvException("Inspection Chamber XY-axis Move T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_XYCmd, false);
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
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_ZPoint, Z_Position);
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_ZCmd, false);
                Thread.Sleep(100);
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_ZCmd, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.IC_TO_PC_ZReply), 1000))
                    throw new MvException("Inspection Chamber Z-axis Move T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.IC_TO_PC_ZComplete), 5000))
                    throw new MvException("Inspection Chamber Z-axis Move T2 timeout");

                switch (plc.Read<int>(MacHalPlcEnumVariable.IC_TO_PC_ZResult))
                {
                    case 0:
                        throw new MvException("Inspection Chamber Z-axis Move Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        throw new MvException("Inspection Chamber Z-axis Move Error : ABS point out range");
                    case 5:
                        Result = "ABS point not change ";
                        break;
                    default:
                        throw new MvException("Inspection Chamber Z-axis Move Error : Unknown error");
                }

                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_ZCmd, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MacHalPlcEnumVariable.IC_TO_PC_ZComplete), 1000))
                    throw new MvException("Inspection Chamber Z-axis Move T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_ZCmd, false);
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
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_WPoint, W_Position);
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_WCmd, false);
                Thread.Sleep(100);
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_WCmd, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.IC_TO_PC_WReply), 1000))
                    throw new MvException("Inspection Chamber W-axis T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.IC_TO_PC_WComplete), 5000))
                    throw new MvException("Inspection Chamber W-axis T2 timeout");

                switch (plc.Read<int>(MacHalPlcEnumVariable.IC_TO_PC_WResult))
                {
                    case 0:
                        throw new MvException("Inspection Chamber W-axis Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        throw new MvException("Inspection Chamber W-axis Error : ABS point out range");
                    case 5:
                        Result = "ABS point not change";
                        break;
                    default:
                        throw new MvException("Inspection Chamber W-axis Error : Unknown error");
                }

                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_WCmd, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MacHalPlcEnumVariable.IC_TO_PC_WComplete), 1000))
                    throw new MvException("Inspection Chamber W-axis T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_WCmd, false);
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
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_Initial_A06, false);
                Thread.Sleep(100);
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_Initial_A06, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.IC_TO_PC_Initial_A06_Reply), 1000))
                    throw new MvException("Inspection Chamber Initial T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.IC_TO_PC_Initial_A06_Complete), 120 * 1000))
                    throw new MvException("Inspection Chamber Initial T2 timeout");

                switch (plc.Read<int>(MacHalPlcEnumVariable.IC_TO_PC_Initial_A06_Result))
                {
                    case 0:
                        throw new MvException("Inspection Chamber Initial Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    default:
                        throw new MvException("Inspection Chamber Initial Error : Unknown error");
                }

                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_Initial_A06, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MacHalPlcEnumVariable.IC_TO_PC_Initial_A06_Complete), 1000))
                    throw new MvException("Inspection Chamber Initial T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_Initial_A06, false);
                throw ex;
            }
            return Result;
        }

        public void SetSpeed(double? StageXYSpeed, double? CcdZSpeed, double? MaskWSpeed)
        {
            var plc = this.plcContext;
            if (StageXYSpeed != null)
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_XY_Speed, StageXYSpeed);// (mm/sec)
            if (CcdZSpeed != null)
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_Z_Speed, CcdZSpeed);// (mm/sec)
            if (MaskWSpeed != null)
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_W_Speed, MaskWSpeed);// angle per second(degree/sec)
        }

        //讀取速度設定
        public Tuple<double, double, double> ReadSpeedSetting()
        {
            var plc = this.plcContext;

            return new Tuple<double, double, double>(
                plc.Read<double>(MacHalPlcEnumVariable.PC_TO_IC_XY_Speed),
                plc.Read<double>(MacHalPlcEnumVariable.PC_TO_IC_Z_Speed),
                plc.Read<double>(MacHalPlcEnumVariable.PC_TO_IC_W_Speed)
                );
        }

        //讀取Robot入侵
        public bool ReadRobotIntrude(bool isIntrude)
        {
            var plc = this.plcContext;
            try
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_RobotIntrude, !isIntrude);
                Thread.Sleep(100);
                if (plc.Read<bool>(MacHalPlcEnumVariable.IC_TO_PC_RobotLicense) != isIntrude)//如果BT要入侵但不被許可
                    throw new MvException("Mask Transfer Intrude is not allowed");
            }
            catch (Exception ex)
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_RobotIntrude, true);//復歸入侵請求，因為訊號是反向觸發所以復歸成 True
                throw ex;
            }
            return plc.Read<bool>(MacHalPlcEnumVariable.IC_TO_PC_RobotLicense);
        }

        //讀取 XY Stage位置
        public Tuple<double, double> ReadXYPosition()
        {
            var plc = this.plcContext;

            return new Tuple<double, double>(
                plc.Read<double>(MacHalPlcEnumVariable.IC_TO_PC_Positon_X),
                plc.Read<double>(MacHalPlcEnumVariable.IC_TO_PC_Positon_Y)
                );
        }

        //讀取 CCD Z軸位置
        public double ReadZPosition()
        {
            var plc = this.plcContext;

            return plc.Read<double>(MacHalPlcEnumVariable.IC_TO_PC_Positon_Z);
        }

        //讀取旋轉位置
        public double ReadWPosition()
        {
            var plc = this.plcContext;

            return plc.Read<double>(MacHalPlcEnumVariable.IC_TO_PC_Positon_W);
        }

        #region 手臂入侵(左右)
        //設定手臂可侵入的左右區間極限值
        public void SetRobotAboutLimit(double? AboutLimit_L, double? AboutLimit_R)
        {
            var plc = this.plcContext;

            if (AboutLimit_R != null)
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_Robot_AboutLimit_R, AboutLimit_R);
            if (AboutLimit_L != null)
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_Robot_AboutLimit_L, AboutLimit_L);
        }

        //讀取手臂可侵入的左右區間極限值
        public Tuple<double, double> ReadRobotAboutLimitSetting()
        {
            var plc = this.plcContext;

            return new Tuple<double, double>(
                plc.Read<double>(MacHalPlcEnumVariable.PC_TO_IC_Robot_AboutLimit_R),
                plc.Read<double>(MacHalPlcEnumVariable.PC_TO_IC_Robot_AboutLimit_L)
                );
        }

        //讀取Robot侵入位置(左右)
        public double ReadRobotPosAbout()
        {
            var plc = this.plcContext;

            return plc.Read<double>(MacHalPlcEnumVariable.IC_TO_PC_RobotPosition_About);
        }
        #endregion

        #region 手臂入侵(上下)
        //設定手臂可侵入的上下區間極限值
        public void SetRobotUpDownLimit(double? UpDownLimit_U, double? UpDownLimit_D)
        {
            var plc = this.plcContext;

            if (UpDownLimit_U != null)
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_Robot_UpDownLimit_U, UpDownLimit_U);
            if (UpDownLimit_D != null)
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_Robot_UpDownLimit_D, UpDownLimit_D);
        }

        //讀取手臂可侵入的上下區間極限值
        public Tuple<double, double> ReadRobotUpDownLimitSetting()
        {
            var plc = this.plcContext;

            return new Tuple<double, double>(
                plc.Read<double>(MacHalPlcEnumVariable.PC_TO_IC_Robot_UpDownLimit_U),
                plc.Read<double>(MacHalPlcEnumVariable.PC_TO_IC_Robot_UpDownLimit_D)
                );
        }

        //讀取Robot侵入位置(上下)
        public double ReadRobotPosUpDown()
        {
            var plc = this.plcContext;

            return plc.Read<double>(MacHalPlcEnumVariable.IC_TO_PC_RobotPosition_UpDown);
        }
        #endregion

        public string ReadInspChStatus()
        {
            string Result = "";
            var plc = this.plcContext;
            switch (plc.Read<int>(MacHalPlcEnumVariable.IC_TO_PC_A06Status))
            {
                case 1:
                    Result = "Idle";
                    break;
                case 2:
                    Result = "Busy";
                    break;
                case 3:
                    Result = "Alarm";
                    break;
                case 4:
                    Result = "Maintenance";
                    break;
            }
            return Result;
        }
    }
}
