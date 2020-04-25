﻿using System;
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
            try
            {
                plc.Write(MvEnumPlcVariable.PC_TO_BT_Box_Type, BoxType);
                plc.Write(MvEnumPlcVariable.PC_TO_BT_Clamp, false);
                Thread.Sleep(100);
                plc.Write(MvEnumPlcVariable.PC_TO_BT_Clamp, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_ClampCmd_Reply), 1000))
                    throw new MvException("Box Hand Clamp T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_ClampCmd_Complete), 5000))
                    throw new MvException("Box Hand Clamp T2 timeout");

                switch (plc.Read<int>(MvEnumPlcVariable.BT_TO_PC_ClampCmd_Result))
                {
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        Result = "No box type";
                        break;
                    case 3:
                        Result = "No box";
                        break;
                }

                plc.Write(MvEnumPlcVariable.PC_TO_BT_Clamp, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_ClampCmd_Complete), 1000))
                    throw new MvException("Box Hand Clamp T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MvEnumPlcVariable.PC_TO_BT_Box_Type, 0);
                plc.Write(MvEnumPlcVariable.PC_TO_BT_Clamp, false);
                throw ex;
            }
            return Result;
        }

        public string Unclamp()
        {
            var Result = "";
            var plc = this.m_PlcContext;
            try
            {
                plc.Write(MvEnumPlcVariable.PC_TO_BT_Unclamp, false);
                Thread.Sleep(100);
                plc.Write(MvEnumPlcVariable.PC_TO_BT_Unclamp, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_UnclampCmd_Reply), 1000))
                    throw new MvException("Box Hand Unclamp T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_UnclampCmd_Complete), 5000))
                    throw new MvException("Box Hand Unclamp T2 timeout");

                switch (plc.Read<int>(MvEnumPlcVariable.BT_TO_PC_UnclampCmd_Result))
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
                plc.Write(MvEnumPlcVariable.PC_TO_BT_Unclamp, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_UnclampCmd_Complete), 1000))
                    throw new MvException("Box Hand Unclamp T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MvEnumPlcVariable.PC_TO_BT_Unclamp, false);
                throw ex;
            }
            return Result;
        }

        public string Initial()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                plc.Write(MvEnumPlcVariable.PC_TO_BT_Initial_A03, false);
                Thread.Sleep(100);
                plc.Write(MvEnumPlcVariable.PC_TO_BT_Initial_A03, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_Initial_A03_Reply), 1000))
                    throw new MvException("Box Hand Initial T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_Initial_A03_Complete), 5000))
                    throw new MvException("Box Hand Initial T2 timeout");

                switch (plc.Read<int>(MvEnumPlcVariable.BT_TO_PC_Initial_A03_Result))
                {
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        Result = "Have Box";
                        break;
                    case 3:
                        Result = "";
                        break;
                }
                plc.Write(MvEnumPlcVariable.PC_TO_BT_Initial_A03, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_Initial_A03_Complete), 1000))
                    throw new MvException("Box Hand Initial T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MvEnumPlcVariable.PC_TO_BT_Initial_A03, false);
                throw ex;
            }
            return Result;
        }

        public void SetSpeed(double ClampSpeed)
        {
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_BT_Speed, ClampSpeed);
        }

        public double ReadSpeedSetting()
        {
            var plc = this.m_PlcContext;
            return plc.Read<double>(MvEnumPlcVariable.PC_TO_BT_Speed);
        }

        //讀取軟體記憶的夾爪位置
        public double ReadHandPos()
        {
            var plc = this.m_PlcContext;
            return plc.Read<double>(MvEnumPlcVariable.BT_TO_PC_HandPosition);
        }

        //讀取夾爪前方是否有Box
        public bool ReadBoxDetect()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_LoadSensor);
        }

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
        {
            var plc = this.m_PlcContext;
            return plc.Read<double>(MvEnumPlcVariable.BT_TO_PC_LaserPosition1);
        }

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
        {
            var plc = this.m_PlcContext;
            return plc.Read<double>(MvEnumPlcVariable.BT_TO_PC_LaserPosition2);
        }
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
            return new Tuple<double, double>(
                plc.Read<double>(MvEnumPlcVariable.BT_TO_PC_Level_X),
                plc.Read<double>(MvEnumPlcVariable.BT_TO_PC_Level_Y)
                );
        }

        //設定XY軸水平Sensor的標準值
        public bool SetLevelReset()
        {
            var plc = this.m_PlcContext;
            try
            {
                plc.Write(MvEnumPlcVariable.PC_TO_BT_Level_Reset, false);
                Thread.Sleep(100);
                plc.Write(MvEnumPlcVariable.PC_TO_BT_Level_Reset, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.PC_TO_BT_Level_Reset_Complete), 1000))
                    throw new MvException("Box Hand Level Reset T0 timeout");

                plc.Write(MvEnumPlcVariable.PC_TO_BT_Level_Reset, false);
            }
            catch (Exception ex)
            {
                plc.Write(MvEnumPlcVariable.PC_TO_BT_Level_Reset, false);
                throw ex;
            }

            return plc.Read<bool>(MvEnumPlcVariable.PC_TO_BT_Level_Reset_Complete);
        }
        #endregion

        #region 六軸Sensor
        //設定六軸力覺Sensor的壓力極限值
        public void SetSixAxisSensorLimit(uint Fx, uint Fy, uint Fz, uint Mx, uint My, uint Mz)
        {
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_BT_ForceLimit_Fx, Fx);
            plc.Write(MvEnumPlcVariable.PC_TO_BT_ForceLimit_Fy, Fy);
            plc.Write(MvEnumPlcVariable.PC_TO_BT_ForceLimit_Fz, Fz);
            plc.Write(MvEnumPlcVariable.PC_TO_BT_ForceLimit_Mx, Mx);
            plc.Write(MvEnumPlcVariable.PC_TO_BT_ForceLimit_My, My);
            plc.Write(MvEnumPlcVariable.PC_TO_BT_ForceLimit_Mz, Mz);
        }

        //讀取六軸力覺Sensor的壓力極限值設定
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

        //讀取六軸力覺Sensor目前數值
        public Tuple<int, int, int, int, int, int> ReadSixAxisSensor()
        {
            var plc = this.m_PlcContext;
            return new Tuple<int, int, int, int, int, int>(
                plc.Read<int>(MvEnumPlcVariable.BT_TO_PC_ForceFx),
                plc.Read<int>(MvEnumPlcVariable.BT_TO_PC_ForceFy),
                plc.Read<int>(MvEnumPlcVariable.BT_TO_PC_ForceFz),
                plc.Read<int>(MvEnumPlcVariable.BT_TO_PC_ForceMx),
                plc.Read<int>(MvEnumPlcVariable.BT_TO_PC_ForceMy),
                plc.Read<int>(MvEnumPlcVariable.BT_TO_PC_ForceMz)
                );
        }
        #endregion

        //確認Hand吸塵狀態
        public bool ReadHandVacuum()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_Vacuum);
        }

        public string ReadBTRobotStatus()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            switch (plc.Read<int>(MvEnumPlcVariable.BT_TO_PC_A03Status))
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

        //當手臂作動時，需要讓指令讓PLC知道目前Robot是移動狀態
        public void RobotMoving(bool isMoving)
        {
            var plc = m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_BT_RobotMoving, isMoving);
        }
    }
}