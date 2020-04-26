using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MvAssistant.Mac.v1_0.CompPlc
{
    public class MvPlcMaskRobot
    {
        private MvPlcContext m_PlcContext;

        public MvPlcMaskRobot(MvPlcContext plc)
        {
            this.m_PlcContext = plc;
        }

        public string Clamp(uint MaskType)
        {
            var Result = "";
            var plc = this.m_PlcContext;
            try
            {
                plc.Write(MvEnumPlcVariable.PC_TO_MT_MaskType, MaskType);
                plc.Write(MvEnumPlcVariable.PC_TO_MT_Clamp, false);
                Thread.Sleep(100);
                plc.Write(MvEnumPlcVariable.PC_TO_MT_Clamp, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.MT_TO_PC_ClampCmd_Reply), 1000))
                    throw new MvException("Mask Hand Clamp T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.MT_TO_PC_ClampCmd_Complete), 10 * 1000))
                    throw new MvException("Mask Hand Clamp T2 timeout");

                switch (plc.Read<int>(MvEnumPlcVariable.MT_TO_PC_ClampCmd_Result))
                {
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        Result = "No mask type";
                        break;
                    case 3:
                        Result = "";
                        break;
                    case 4:
                        Result = "";
                        break;
                }

                plc.Write(MvEnumPlcVariable.PC_TO_MT_Clamp, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.MT_TO_PC_ClampCmd_Complete), 1000))
                    throw new MvException("Mask Hand Clamp T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MvEnumPlcVariable.PC_TO_MT_MaskType, 0);
                plc.Write(MvEnumPlcVariable.PC_TO_MT_Clamp, false);
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
                plc.Write(MvEnumPlcVariable.PC_TO_MT_Unclamp, false);
                Thread.Sleep(100);
                plc.Write(MvEnumPlcVariable.PC_TO_MT_Unclamp, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.MT_TO_PC_UnclampCmd_Reply), 1000))
                    throw new MvException("Mask Hand Unclamp T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.MT_TO_PC_UnclampCmd_Complete), 10 * 1000))
                    throw new MvException("Mask Hand Unclamp T2 timeout");

                switch (plc.Read<int>(MvEnumPlcVariable.MT_TO_PC_UnclampCmd_Result))
                {
                    case 0:
                        Result = "Invalid";
                        break;
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        Result = "Clamp no mask type";
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
                plc.Write(MvEnumPlcVariable.PC_TO_MT_Unclamp, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.MT_TO_PC_UnclampCmd_Complete), 1000))
                    throw new MvException("Mask Hand Unclamp T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MvEnumPlcVariable.PC_TO_MT_Unclamp, false);
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
                plc.Write(MvEnumPlcVariable.PC_TO_MT_Initial_A04, false);
                Thread.Sleep(100);
                plc.Write(MvEnumPlcVariable.PC_TO_MT_Initial_A04, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.MT_TO_PC_Initial_A04_Reply), 1000))
                    throw new MvException("Mask Hand Initial T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.MT_TO_PC_Initial_A04_Complete), 120 * 1000))
                    throw new MvException("Mask Hand Initial T2 timeout");

                switch (plc.Read<int>(MvEnumPlcVariable.MT_TO_PC_Initial_A04_Result))
                {
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        Result = "Have Mask";
                        break;
                    case 3:
                        Result = "";
                        break;
                }

                plc.Write(MvEnumPlcVariable.PC_TO_MT_Initial_A04, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.MT_TO_PC_Initial_A04_Complete), 1000))
                    throw new MvException("Mask Hand Initial T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MvEnumPlcVariable.PC_TO_MT_Initial_A04, false);
                throw ex;
            }
            return Result;
        }

        #region Speed setting
        public void SetSpeed(double? ClampSpeed, long? CCDSpinSpeed, int? SpinDegreeLimit)
        {
            var plc = m_PlcContext;
            if (ClampSpeed != null)
                plc.Write(MvEnumPlcVariable.PC_TO_MT_Speed, ClampSpeed);
            if (CCDSpinSpeed != null)
                plc.Write(MvEnumPlcVariable.PC_TO_MT_Spin_Speed, CCDSpinSpeed);
            if (SpinDegreeLimit != null)
                plc.Write(MvEnumPlcVariable.PC_TO_MT_Tactile_Limit, SpinDegreeLimit);
        }

        public Tuple<double, long, int> ReadSpeedSetting()
        {
            var plc = m_PlcContext;
            return new Tuple<double, long, int>(
                  plc.Read<double>(MvEnumPlcVariable.PC_TO_MT_Speed),
                  plc.Read<long>(MvEnumPlcVariable.PC_TO_MT_Spin_Speed),
                  plc.Read<int>(MvEnumPlcVariable.PC_TO_MT_Tactile_Limit)
                );
        }
        #endregion

        public Tuple<double, double, double, double> ReadClampGripPos()
        {
            var plc = this.m_PlcContext;
            return new Tuple<double, double, double, double>(
                plc.Read<double>(MvEnumPlcVariable.MT_TO_PC_Position_Up),
                plc.Read<double>(MvEnumPlcVariable.MT_TO_PC_Position_Down),
                plc.Read<double>(MvEnumPlcVariable.MT_TO_PC_Position_Left),
                plc.Read<double>(MvEnumPlcVariable.MT_TO_PC_Position_Right)
                );
        }

        #region CCD Spin
        public string CCDSpin(int SpinDegree)
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                plc.Write(MvEnumPlcVariable.PC_TO_MT_Spin, false);
                Thread.Sleep(100);
                plc.Write(MvEnumPlcVariable.PC_TO_MT_Spin_Point, SpinDegree);
                plc.Write(MvEnumPlcVariable.PC_TO_MT_Spin, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.MT_TO_PC_Spin_Reply), 1000))
                    throw new MvException("Mask Hand Initial T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.MT_TO_PC_Spin_Complete), 20 * 1000))
                    throw new MvException("Mask Hand Initial T2 timeout");

                switch (plc.Read<int>(MvEnumPlcVariable.MT_TO_PC_Spin_Result))
                {
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        Result = "Out of range";
                        break;
                    case 3:
                        Result = "";
                        break;
                }

                plc.Write(MvEnumPlcVariable.PC_TO_MT_Spin, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.MT_TO_PC_Spin_Complete), 1000))
                    throw new MvException("Mask Hand Initial T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MvEnumPlcVariable.PC_TO_MT_Spin, false);
                throw ex;
            }
            return Result;
        }

        public long ReadCCDSpinDegree()
        {
            var plc = this.m_PlcContext;
            return plc.Read<long>(MvEnumPlcVariable.MT_TO_PC_Position_Spin);
        }
        #endregion

        #region 六軸Sensor
        //設定六軸力覺Sensor的壓力極限值
        public void SetSixAxisSensorLimit(uint Fx, uint Fy, uint Fz, uint Mx, uint My, uint Mz)
        {
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_MT_ForceLimit_Fx, Fx);
            plc.Write(MvEnumPlcVariable.PC_TO_MT_ForceLimit_Fy, Fy);
            plc.Write(MvEnumPlcVariable.PC_TO_MT_ForceLimit_Fz, Fz);
            plc.Write(MvEnumPlcVariable.PC_TO_MT_ForceLimit_Mx, Mx);
            plc.Write(MvEnumPlcVariable.PC_TO_MT_ForceLimit_My, My);
            plc.Write(MvEnumPlcVariable.PC_TO_MT_ForceLimit_Mz, Mz);
        }

        //讀取六軸力覺Sensor的壓力極限值設定
        public Tuple<int, int, int, int, int, int> ReadSixAxisSensorLimitSetting()
        {
            var plc = this.m_PlcContext;
            return new Tuple<int, int, int, int, int, int>(
                plc.Read<int>(MvEnumPlcVariable.PC_TO_MT_ForceLimit_Fx),
                plc.Read<int>(MvEnumPlcVariable.PC_TO_MT_ForceLimit_Fy),
                plc.Read<int>(MvEnumPlcVariable.PC_TO_MT_ForceLimit_Fz),
                plc.Read<int>(MvEnumPlcVariable.PC_TO_MT_ForceLimit_Mx),
                plc.Read<int>(MvEnumPlcVariable.PC_TO_MT_ForceLimit_My),
                plc.Read<int>(MvEnumPlcVariable.PC_TO_MT_ForceLimit_Mz)
                );
        }

        public Tuple<int, int, int, int, int, int> ReadSixAxisSensor()
        {
            var plc = this.m_PlcContext;
            return new Tuple<int, int, int, int, int, int>(
                plc.Read<int>(MvEnumPlcVariable.MT_TO_PC_ForceFx),
                plc.Read<int>(MvEnumPlcVariable.MT_TO_PC_ForceFy),
                plc.Read<int>(MvEnumPlcVariable.MT_TO_PC_ForceFz),
                plc.Read<int>(MvEnumPlcVariable.MT_TO_PC_ForceMx),
                plc.Read<int>(MvEnumPlcVariable.MT_TO_PC_ForceMy),
                plc.Read<int>(MvEnumPlcVariable.MT_TO_PC_ForceMz)
                );
        }
        #endregion

        #region 靜電感測
        //設定靜電感測的區間限制
        public void SetStaticElecLimit(double Minimum, double Maximum)
        {
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.MT_TO_PC_StaticElectricity_Limit_UP, Maximum);
            plc.Write(MvEnumPlcVariable.MT_TO_PC_StaticElectricity_Limit_Down, Minimum);
        }

        //讀取靜電感測的區間限制設定值
        public Tuple<double, double> ReadStaticElecLimitSetting()
        {
            var plc = this.m_PlcContext;
            return new Tuple<double, double>(
                plc.Read<double>(MvEnumPlcVariable.MT_TO_PC_StaticElectricity_Limit_Down),
                plc.Read<double>(MvEnumPlcVariable.MT_TO_PC_StaticElectricity_Limit_UP)
            );
        }

        //讀取靜電測量值
        public double ReadStaticElec()
        {
            var plc = this.m_PlcContext;
            return plc.Read<double>(MvEnumPlcVariable.MT_TO_PC_StaticElectricity_Value);
        }
        #endregion

        public string ReadMTRobotStatus()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            switch (plc.Read<int>(MvEnumPlcVariable.MT_TO_PC_A04Status))
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

        //將夾爪伸到LoadPort，讀取感測器的值，確認夾爪有無變形
        public Tuple<double, double, double, double, double, double> ReadHandInspection()
        {
            var plc = this.m_PlcContext;
            return new Tuple<double, double, double, double, double, double>(
            plc.Read<double>(MvEnumPlcVariable.LD_TO_PC_Laser1),
            plc.Read<double>(MvEnumPlcVariable.LD_TO_PC_Laser2),
            plc.Read<double>(MvEnumPlcVariable.LD_TO_PC_Laser3),
            plc.Read<double>(MvEnumPlcVariable.LD_TO_PC_Laser4),
            plc.Read<double>(MvEnumPlcVariable.LD_TO_PC_Laser5),
            plc.Read<double>(MvEnumPlcVariable.LD_TO_PC_Laser6)
            );
        }

        //當手臂作動時，需要讓指令讓PLC知道目前Robot是移動狀態
        public void RobotMoving(bool isMoving)
        {
            var plc = m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_MT_RobotMoving, isMoving);
        }
    }

}
