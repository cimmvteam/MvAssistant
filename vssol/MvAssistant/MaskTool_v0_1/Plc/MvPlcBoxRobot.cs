using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MvAssistant.MaskTool_v0_1.Plc
{
    public class MvPlcBoxRobot
    {
        MvPlcContext m_PlcContext;

        public MvPlcBoxRobot(MvPlcContext plc)
        {
            this.m_PlcContext = plc;
        }
        public string Clamp(uint BoxType)
        {
            var Result = "";
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_BT_Box_Type, BoxType);
            plc.Write(MvEnumPlcVariable.PC_TO_BT_Clamp, true);

            if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_ClampCmd_Reply), 1000))
                throw new MvException("Box Hand Clamp T0 timeout");
            else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_ClampCmd_Complete), 5000))
                throw new MvException("Box Hand Clamp T2 timeout");

            SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.PC_TO_BT_Clamp), 1000);

            if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_ClampCmd_Complete), 1000))
                throw new MvException("Box Hand Clamp T4 timeout");
            switch (plc.Read<uint>(MvEnumPlcVariable.BT_TO_PC_ClampCmd_Result))
            {
                case 0:
                    Result = "Invalid";
                    break;
                case 1:
                    Result = "OK";
                    break;
                case 2:
                    Result = "Clamp no box type";
                    break;
                case 3:
                    Result = "Tactile out range";
                    break;
                case 4:
                    Result = "Motor error";
                    break;
                case 5:
                    Result = "Please initial";
                    break;
                case 6:
                    Result = "System not ready";
                    break;
            }
            return Result;
        }

        public string Unclamp()
        {
            var Result = "";
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_BT_Unclamp, true);

            if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_UnclampCmd_Reply), 1000))
                throw new MvException("Box Hand Unclamp T0 timeout");
            else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_UnclampCmd_Complete), 5000))
                throw new MvException("Box Hand Unclamp T2 timeout");

            SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.PC_TO_BT_Unclamp), 1000);

            if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_UnclampCmd_Complete), 1000))
                throw new MvException("Box Hand Unclamp T4 timeout");
            switch (plc.Read<uint>(MvEnumPlcVariable.BT_TO_PC_UnclampCmd_Result))
            {
                //case 0:
                //    Result = "Invalid";
                //    break;
                //case 1:
                //    Result = "OK";
                //    break;
                //case 2:
                //    Result = "Clamp no box type";
                //    break;
                //case 3:
                //    Result = "Tactile out range";
                //    break;
                //case 4:
                //    Result = "Motor error";
                //    break;
                //case 5:
                //    Result = "Please initial";
                //    break;
                //case 6:
                //    Result = "System not ready";
                //    break;
            }
            return Result;
        }
        
        public string Initial()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_BT_Initial_A03, true);

            if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_Initial_A03_Reply), 1000))
                throw new MvException("Box Hand Initial T0 timeout");
            else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_Initial_A03_Complete), 5000))
                throw new MvException("Box Hand Initial T2 timeout");

            SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.PC_TO_BT_Initial_A03), 1000);

            if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_Initial_A03_Complete), 1000))
                throw new MvException("Box Hand Initial T4 timeout");
            switch (plc.Read<uint>(MvEnumPlcVariable.BT_TO_PC_Initial_A03_Result))
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

        //由軟體確認夾爪位置
        public double CheckHandPos()
        {
            var plc = this.m_PlcContext;
            return plc.Read<double>(MvEnumPlcVariable.BT_TO_PC_HandPosition);
        }

<<<<<<< HEAD
        //確認是否有Box
        public bool CheckBox()
=======
        //讀取夾爪前方是否有Box
        public bool ReadBoxDetect()
>>>>>>> 7d76a0e8abe70a0aea1c750ff12f3c502bf5a150
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_LoadSensor);
        }

<<<<<<< HEAD
        //由雷射確認硬體夾爪位置
        public double CheckHandPosByLSR(double FLS, double RLS)
=======
        #region 夾爪間距
        //設定夾爪間距的極限值
        public void SetHandSpaceLimit(double Minimum, double Maximum)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_BT_Laser1_FLS, Minimum);//夾爪最小夾距
            plc.Write(MvEnumPlcVariable.PC_TO_BT_Laser1_RLS, Maximum);//夾爪最大夾距
        }

        //讀取夾爪間距的極限值設定
        public Tuple<double, double> ReadHandSpaceLimitSetting()
        {
            var plc = this.m_PlcContext;

            return new Tuple<double, double>(
            plc.Read<double>(MvEnumPlcVariable.PC_TO_BT_Laser1_FLS),//夾爪最小夾距
            plc.Read<double>(MvEnumPlcVariable.PC_TO_BT_Laser1_RLS)//夾爪最大夾距
            );
        }
        #endregion

        //讀取由雷射檢測的夾爪位置
        public double ReadHandPosByLSR()
>>>>>>> 7d76a0e8abe70a0aea1c750ff12f3c502bf5a150
        {
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_BT_Laser1_FLS, FLS);
            plc.Write(MvEnumPlcVariable.PC_TO_BT_Laser1_RLS, RLS);
            return plc.Read<double>(MvEnumPlcVariable.BT_TO_PC_LaserPosition1);
        }

<<<<<<< HEAD
        //確認夾取距離
        public double CheckClampLength(double ClampLength)
=======
        #region Clamp前方物距
        //設定Clamp與Cabinet的最小間距
        public void SetClampToCabinetSpaceLimit(double Minimum)
        {
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_BT_Laser2_Limit, Minimum);//夾爪夾取Box時與Cabinet的距離限制
        }

        //讀取Clamp與Cabinet的最小間距設定值
        public double ReadClampToCabinetSpaceLimitSetting()
        {
            var plc = this.m_PlcContext;
            return plc.Read<double>(MvEnumPlcVariable.PC_TO_BT_Laser2_Limit);//夾爪夾取Box時與Cabinet的距離限制
        }

        //讀取Clamp前方物體距離
        public double ReadClampDistance()
>>>>>>> 7d76a0e8abe70a0aea1c750ff12f3c502bf5a150
        {
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_BT_Laser2_Limit, ClampLength);
            return plc.Read<double>(MvEnumPlcVariable.BT_TO_PC_LaserPosition2);
        }
<<<<<<< HEAD

        //確認水平Sensor
        public Tuple<double, double> CheckLevelSensor(double Level_X, double Level_Y)
        {
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_BT_Level_Limit_X, Level_X);
            plc.Write(MvEnumPlcVariable.PC_TO_BT_Level_Limit_Y, Level_Y);

=======
        #endregion

        #region 水平Sensor
        //設定XY軸水平Sensor的標準值
        public void SetLevelSensorLimit(double Level_X, double Level_Y)
        {
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_BT_Level_Limit_X, Level_X);
            plc.Write(MvEnumPlcVariable.PC_TO_BT_Level_Limit_Y, Level_Y);
        }

        //讀取XY軸水平Sensor的標準值
        public Tuple<double, double> ReadLevelSensorLimitSetting()
        {
            var plc = this.m_PlcContext;
            return new Tuple<double, double>(
            plc.Read<double>(MvEnumPlcVariable.PC_TO_BT_Level_Limit_X),
            plc.Read<double>(MvEnumPlcVariable.PC_TO_BT_Level_Limit_Y)
            );
        }

        //讀取XY軸水平Sensor目前數值
        public Tuple<double, double> ReadLevelSensor()
        {
            var plc = this.m_PlcContext;
>>>>>>> 7d76a0e8abe70a0aea1c750ff12f3c502bf5a150
            return new Tuple<double, double>(
                plc.Read<double>(MvEnumPlcVariable.BT_TO_PC_Level_X),
                plc.Read<double>(MvEnumPlcVariable.BT_TO_PC_Level_Y)
                );
        }

<<<<<<< HEAD
        //確認六軸力覺Sensor
        public Tuple<uint, uint, uint, uint, uint, uint> CheckSixAxisSensor(uint Fx, uint Fy, uint Fz, uint Mx, uint My, uint Mz)
=======
        #region 六軸Sensor
        //設定六軸力覺Sensor的壓力極限值
        public void SetSixAxisSensorLimit(uint Fx, uint Fy, uint Fz, uint Mx, uint My, uint Mz)
>>>>>>> 7d76a0e8abe70a0aea1c750ff12f3c502bf5a150
        {
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_BT_ForceLimit_Fx, Fx);
            plc.Write(MvEnumPlcVariable.PC_TO_BT_ForceLimit_Fy, Fy);
            plc.Write(MvEnumPlcVariable.PC_TO_BT_ForceLimit_Fz, Fz);
            plc.Write(MvEnumPlcVariable.PC_TO_BT_ForceLimit_Mx, Mx);
            plc.Write(MvEnumPlcVariable.PC_TO_BT_ForceLimit_My, My);
            plc.Write(MvEnumPlcVariable.PC_TO_BT_ForceLimit_Mz, Mz);
<<<<<<< HEAD
=======
        }

        //讀取六軸力覺Sensor的壓力極限值
        public Tuple<int, int, int, int, int, int> ReadSixAxisSensorLimitSetting()
        {
            var plc = this.m_PlcContext;
            return new Tuple<int, int, int, int, int, int>(
                plc.Read<int>(MvEnumPlcVariable.PC_TO_BT_ForceLimit_Fx),
                plc.Read<int>(MvEnumPlcVariable.PC_TO_BT_ForceLimit_Fy),
                plc.Read<int>(MvEnumPlcVariable.PC_TO_BT_ForceLimit_Fz),
                plc.Read<int>(MvEnumPlcVariable.PC_TO_BT_ForceLimit_Mx),
                plc.Read<int>(MvEnumPlcVariable.PC_TO_BT_ForceLimit_My),
                plc.Read<int>(MvEnumPlcVariable.PC_TO_BT_ForceLimit_Mz)
                );
        }
>>>>>>> 7d76a0e8abe70a0aea1c750ff12f3c502bf5a150

            return new Tuple<uint, uint, uint, uint, uint, uint>(
                plc.Read<uint>(MvEnumPlcVariable.BT_TO_PC_ForceFx),
                plc.Read<uint>(MvEnumPlcVariable.BT_TO_PC_ForceFy),
                plc.Read<uint>(MvEnumPlcVariable.BT_TO_PC_ForceFz),
                plc.Read<uint>(MvEnumPlcVariable.BT_TO_PC_ForceMx),
                plc.Read<uint>(MvEnumPlcVariable.BT_TO_PC_ForceMx),
                plc.Read<uint>(MvEnumPlcVariable.BT_TO_PC_ForceMx)
                );
        }

        //確認Hand吸塵狀態
        public bool CheakHandVacuum()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_Vacuum);
        }
    }
}
