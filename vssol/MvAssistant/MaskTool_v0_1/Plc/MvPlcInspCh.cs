using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MvAssistant.MaskTool_v0_1.Plc
{
    public class MvPlcInspCh
    {

        MvPlcContext m_PlcContext;

        public MvPlcInspCh(MvPlcContext plc)
        {
            this.m_PlcContext = plc;
        }

        //Stage進行XY移動
        public string XYPosition(double X_Position, double Y_Position)
        {
            string Result = "";
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_IC_XPoint, X_Position);
            plc.Write(MvEnumPlcVariable.PC_TO_IC_YPoint, Y_Position);
            plc.Write(MvEnumPlcVariable.PC_TO_IC_XYCmd, false);
            Thread.Sleep(100);
            plc.Write(MvEnumPlcVariable.PC_TO_IC_XYCmd, true);

            if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.IC_TO_PC_XYReply), 1000))
                throw new MvException("Inspection XY T0 timeout");
            else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.IC_TO_PC_XYComplete), 5000))
                throw new MvException("Inspection XY T2 timeout");


            //SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.PC_TO_IC_XYCmd), 1000);
            plc.Write(MvEnumPlcVariable.PC_TO_IC_XYCmd, false);

            if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.IC_TO_PC_XYComplete), 1000))
                throw new MvException("Inspection XY T4 timeout");
            switch (plc.Read<int>(MvEnumPlcVariable.IC_TO_PC_XYResult))
            {
                case 0:
                    Result = "Invalid";
                    break;
                case 1:
                    Result = "OK";
                    break;
                case 2:
                    Result = "Point out range";
                    break;
                case 3:
                    Result = "Status busy";
                    break;
                case 4:
                    Result = "Motor error";
                    break;
                case 5:
                    Result = "Point Same";
                    break;
            }
            return Result;
        }

        //CCD高度變更
        public string ZPosition(double Z_Position)
        {
            string Result = "";
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_IC_ZPoint, Z_Position);
            plc.Write(MvEnumPlcVariable.PC_TO_IC_ZCmd, false);
            Thread.Sleep(100);
            plc.Write(MvEnumPlcVariable.PC_TO_IC_ZCmd, true);

            if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.IC_TO_PC_ZReply), 1000))
                throw new MvException("Inspection Z T0 timeout");
            else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.IC_TO_PC_ZComplete), 5000))
                throw new MvException("Inspection Z T2 timeout");

            //SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.PC_TO_IC_ZCmd), 1000);
            plc.Write(MvEnumPlcVariable.PC_TO_IC_ZCmd, false);

            if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.IC_TO_PC_ZComplete), 1000))
                throw new MvException("Inspection Z T4 timeout");
            switch (plc.Read<int>(MvEnumPlcVariable.IC_TO_PC_ZResult))
            {
                case 0:
                    Result = "Invalid";
                    break;
                case 1:
                    Result = "OK";
                    break;
                case 2:
                    Result = "Point out range";
                    break;
                case 3:
                    Result = "Status busy";
                    break;
                case 4:
                    Result = "Motor error";
                    break;
                case 5:
                    Result = "Point Same";
                    break;
            }
            return Result;
        }

        //Mask方向旋轉
        public string WPosition(double W_Position)
        {
            string Result = "";
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_IC_WPoint, W_Position);
            plc.Write(MvEnumPlcVariable.PC_TO_IC_WCmd, false);
            Thread.Sleep(100);
            plc.Write(MvEnumPlcVariable.PC_TO_IC_WCmd, true);

            if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.IC_TO_PC_WReply), 1000))
                throw new MvException("Inspection W T0 timeout");
            else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.IC_TO_PC_WComplete), 5000))
                throw new MvException("Inspection W T2 timeout");

            //SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.PC_TO_IC_WCmd), 1000);
            plc.Write(MvEnumPlcVariable.PC_TO_IC_WCmd, false);

            if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.IC_TO_PC_WComplete), 1000))
                throw new MvException("Inspection W T4 timeout");
            switch (plc.Read<int>(MvEnumPlcVariable.IC_TO_PC_WResult))
            {
                case 0:
                    Result = "Invalid";
                    break;
                case 1:
                    Result = "OK";
                    break;
                case 2:
                    Result = "Point out range";
                    break;
                case 3:
                    Result = "Status busy";
                    break;
                case 4:
                    Result = "Motor error";
                    break;
                case 5:
                    Result = "Point Same";
                    break;
            }
            return Result;
        }

        public string Initial()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_IC_Initial_A06, true);

            if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.IC_TO_PC_Initial_A06_Reply), 1000))
                throw new MvException("Inspection Initial T0 timeout");
            else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.IC_TO_PC_Initial_A06_Complete), 5000))
                throw new MvException("Inspection Initial T2 timeout");

            SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.PC_TO_IC_Initial_A06), 1000);

            if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.IC_TO_PC_Initial_A06_Complete), 1000))
                throw new MvException("Inspection Initial T4 timeout");
            switch (plc.Read<uint>(MvEnumPlcVariable.IC_TO_PC_Initial_A06_Result))
            {
                //case 0:
                //    Result = "Invalid";
                //    break;
                //case 1:
                //    Result = "Idle";
                //    break;
                //case 2:
                //    Result = "Busy";
                //    break;
                //case 3:
                //    Result = "Error";
                //    break;
            }
            return Result;
        }

        public string SetCommand()
        {
            string Result = "";

            return Result;
        }

        //確認Robot入侵
        public bool CheckRobotIntrude()
        {
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_IC_RobotIntrude, true);

            return plc.Read<bool>(MvEnumPlcVariable.PC_TO_OS_RobotLicence);
        }

        //確認 XY Stage位置
        public Tuple<double, double> CheckXYPosition()
        {
            var plc = this.m_PlcContext;

            return new Tuple<double, double>(
                plc.Read<double>(MvEnumPlcVariable.IC_TO_PC_Positon_X),
                plc.Read<double>(MvEnumPlcVariable.IC_TO_PC_Positon_Y)
                );
        }

        //確認 CCD Z軸位置
        public double CheckZPosition()
        {
            var plc = this.m_PlcContext;

            return plc.Read<double>(MvEnumPlcVariable.IC_TO_PC_Positon_Z);
        }

        //確認旋轉位置
        public double CheckWPosition()
        {
            var plc = this.m_PlcContext;

            return plc.Read<double>(MvEnumPlcVariable.IC_TO_PC_Positon_W);
        }

        //確認Robot侵入位置(左右)
        public double CheckRobotAbout(double AboutLimit_R, double AboutLimit_L)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_IC_Robot_AboutLimit_R, AboutLimit_R);
            plc.Write(MvEnumPlcVariable.PC_TO_IC_Robot_AboutLimit_L, AboutLimit_L);

            return plc.Read<double>(MvEnumPlcVariable.IC_TO_PC_RobotPosition_About);
        }

        //檢測Robot侵入位置(上下)
        public double CheckRobotUpDown(double UpDownLimit_U, double UpDownLimit_D)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_IC_Robot_UpDownLimit_U, UpDownLimit_U);
            plc.Write(MvEnumPlcVariable.PC_TO_IC_Robot_UpDownLimit_D, UpDownLimit_D);

            return plc.Read<double>(MvEnumPlcVariable.IC_TO_PC_RobotPosition_UpDown);
        }
    }
}
