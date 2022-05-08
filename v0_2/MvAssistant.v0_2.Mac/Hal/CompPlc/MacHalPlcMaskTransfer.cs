using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MvAssistant.v0_2.Mac.Hal.CompPlc
{
    [Guid("DFD72153-DCC6-4949-85DB-B5D7CED91E2E")]
    public class MacHalPlcMaskTransfer : MacHalPlcBase, IMacHalPlcMaskTransfer
    {


        public MacHalPlcMaskTransfer() { }
        public MacHalPlcMaskTransfer(MacHalPlcContext plc = null)
        {
            this.plcContext = plc;
        }





        public string Clamp(uint MaskType)
        {
            var Result = "";
            var plc = this.plcContext;
            try
            {
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_MaskType, MaskType);
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Clamp, false);
                Thread.Sleep(100);
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Clamp, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(EnumMacHalPlcVariable.MT_TO_PC_ClampCmd_Reply), 1000))
                    throw new MvaException("Mask Hand Clamp T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(EnumMacHalPlcVariable.MT_TO_PC_ClampCmd_Complete), 10 * 1000))
                    throw new MvaException("Mask Hand Clamp T2 timeout");

                switch (plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_ClampCmd_Result))
                {
                    case 0:
                        throw new MvaException("Mask Hand Clamp Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    default:
                        throw new MvaException("Mask Hand Clamp Error : Unknown error");
                }

                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Clamp, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(EnumMacHalPlcVariable.MT_TO_PC_ClampCmd_Complete), 1000))
                    throw new MvaException("Mask Hand Clamp T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_MaskType, 0);
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Clamp, false);
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
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Initial_A04, false);
                Thread.Sleep(100);
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Initial_A04, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(EnumMacHalPlcVariable.MT_TO_PC_Initial_A04_Reply), 1000))
                    throw new MvaException("Mask Hand Initial T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(EnumMacHalPlcVariable.MT_TO_PC_Initial_A04_Complete), 120 * 1000))
                    throw new MvaException("Mask Hand Initial T2 timeout");

                switch (plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Initial_A04_Result))
                {
                    case 0:
                        throw new MvaException("Mask Hand Initial Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    default:
                        throw new MvaException("Mask Hand Initial Error : Unknown error");
                }

                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Initial_A04, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(EnumMacHalPlcVariable.MT_TO_PC_Initial_A04_Complete), 1000))
                    throw new MvaException("Mask Hand Initial T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Initial_A04, false);
                throw ex;
            }
            return Result;
        }

        public Tuple<double, double, double, double> ReadClampGripPos()
        {
            var plc = this.plcContext;
            return new Tuple<double, double, double, double>(
                plc.Read<double>(EnumMacHalPlcVariable.MT_TO_PC_Position_Up),
                plc.Read<double>(EnumMacHalPlcVariable.MT_TO_PC_Position_Down),
                plc.Read<double>(EnumMacHalPlcVariable.MT_TO_PC_Position_Left),
                plc.Read<double>(EnumMacHalPlcVariable.MT_TO_PC_Position_Right)
                );
        }

        /// <summary>
        /// 將夾爪伸到LoadPort，讀取感測器的值，確認夾爪有無變形
        /// </summary>
        /// <returns></returns>
        public Tuple<double, double, double, double, double, double> ReadHandInspection()
        {
            var plc = this.plcContext;
            return new Tuple<double, double, double, double, double, double>(
            plc.Read<double>(EnumMacHalPlcVariable.LD_TO_PC_Laser1),
            plc.Read<double>(EnumMacHalPlcVariable.LD_TO_PC_Laser2),
            plc.Read<double>(EnumMacHalPlcVariable.LD_TO_PC_Laser3),
            plc.Read<double>(EnumMacHalPlcVariable.LD_TO_PC_Laser4),
            plc.Read<double>(EnumMacHalPlcVariable.LD_TO_PC_Laser5),
            plc.Read<double>(EnumMacHalPlcVariable.LD_TO_PC_Laser6)
            );
        }

        public EnumMacPlcAssemblyStatus ReadMTStatus()
        {
            var plc = this.plcContext;
            var status = plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_A04Status);
            return (EnumMacPlcAssemblyStatus)status;
        }

        /// <summary>
        /// 當手臂作動或停止時，需要下指令讓PLC知道目前Robot是移動或靜止狀態
        /// </summary>
        /// <param name="isMoving"></param>
        public void RobotMoving(bool isMoving)
        {
            var plc = plcContext;
            plc.Write(EnumMacHalPlcVariable.PC_TO_MT_RobotMoving, isMoving);
            Thread.Sleep(1000);
            if (plc.Read<bool>(EnumMacHalPlcVariable.MT_TO_PC_RobotMoving_Reply) != isMoving)
                throw new MvaException("PLC did not get 'Mask Transfer Moving' signal");
        }

        public string Unclamp()
        {
            var Result = "";
            var plc = this.plcContext;
            try
            {
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Unclamp, false);
                Thread.Sleep(100);
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Unclamp, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(EnumMacHalPlcVariable.MT_TO_PC_UnclampCmd_Reply), 1000))
                    throw new MvaException("Mask Hand Unclamp T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(EnumMacHalPlcVariable.MT_TO_PC_UnclampCmd_Complete), 10 * 1000))
                    throw new MvaException("Mask Hand Unclamp T2 timeout");

                switch (plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_UnclampCmd_Result))
                {
                    case 0:
                        throw new MvaException("Mask Hand Unclamp Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    default:
                        throw new MvaException("Mask Hand Unclamp Error : Unknown error");
                }
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Unclamp, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(EnumMacHalPlcVariable.MT_TO_PC_UnclampCmd_Complete), 1000))
                    throw new MvaException("Mask Hand Unclamp T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Unclamp, false);
                throw ex;
            }
            return Result;
        }
        #region Speed setting
        /// <summary>
        /// 讀取速度設定
        /// </summary>
        /// <returns>夾爪速度 , CCD旋轉速度</returns>
        public Tuple<double, long> ReadSpeedVar()
        {
            var plc = plcContext;
            return new Tuple<double, long>(
                  plc.Read<double>(EnumMacHalPlcVariable.PC_TO_MT_Speed),
                  plc.Read<long>(EnumMacHalPlcVariable.PC_TO_MT_Spin_Speed)
                );
        }

        /// <summary>
        /// 設定速度：ClampSpeed(1~10mm/S)、CCDSpinSpeed * 0.01(deg/S)
        /// </summary>
        /// <param name="ClampSpeed">(1~10mm/S)</param>
        /// <param name="CCDSpinSpeed"> * 0.01(deg/S)</param>
        public void SetSpeedVar(double? ClampSpeed, long? CCDSpinSpeed)
        {
            var plc = plcContext;
            if (ClampSpeed != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Speed, ClampSpeed);
            if (CCDSpinSpeed != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Spin_Speed, CCDSpinSpeed);
        }
        #endregion
        #region CCD Spin
        public string CCDSpin(int SpinDegree)
        {
            string Result = "";
            var plc = this.plcContext;
            try
            {
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Spin, false);
                Thread.Sleep(100);
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Spin_Point, SpinDegree);
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Spin, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(EnumMacHalPlcVariable.MT_TO_PC_Spin_Reply), 1000))
                    throw new MvaException("Mask Hand CCD spin T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(EnumMacHalPlcVariable.MT_TO_PC_Spin_Complete), 20 * 1000))
                    throw new MvaException("Mask Hand CCD spin T2 timeout");

                switch (plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Spin_Result))
                {
                    case 0:
                        throw new MvaException("Mask Hand CCD spin Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        throw new MvaException("Mask Hand CCD spin Error : Position out range");
                    case 3:
                        throw new MvaException("Mask Hand CCD spin Error : Please initial");
                    default:
                        throw new MvaException("Mask Hand CCD spin Error : Unknown error");
                }

                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Spin, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(EnumMacHalPlcVariable.MT_TO_PC_Spin_Complete), 1000))
                    throw new MvaException("Mask Hand CCD spin T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Spin, false);
                throw ex;
            }
            return Result;
        }

        public long ReadCCDSpinDegree()
        {
            var plc = this.plcContext;
            return plc.Read<long>(EnumMacHalPlcVariable.MT_TO_PC_Position_Spin);
        }
        #endregion

        #region 六軸Sensor
        public Tuple<double, double, double, double, double, double> ReadSixAxisSensor()
        {
            var plc = this.plcContext;
            return new Tuple<double, double, double, double, double, double>(
                plc.Read<double>(EnumMacHalPlcVariable.MT_TO_PC_ForceFx),
                plc.Read<double>(EnumMacHalPlcVariable.MT_TO_PC_ForceFy),
                plc.Read<double>(EnumMacHalPlcVariable.MT_TO_PC_ForceFz),
                plc.Read<double>(EnumMacHalPlcVariable.MT_TO_PC_ForceMx),
                plc.Read<double>(EnumMacHalPlcVariable.MT_TO_PC_ForceMy),
                plc.Read<double>(EnumMacHalPlcVariable.MT_TO_PC_ForceMz)
                );
        }

        /// <summary>
        /// 讀取六軸力覺Sensor的壓力值下限設定
        /// </summary>
        /// <returns></returns>
        public Tuple<double, double, double, double, double, double> ReadSixAxisSensorLowerLimit()
        {
            var plc = this.plcContext;
            return new Tuple<double, double, double, double, double, double>(
                plc.Read<double>(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitN_Fx),
                plc.Read<double>(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitN_Fy),
                plc.Read<double>(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitN_Fz),
                plc.Read<double>(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitN_Mx),
                plc.Read<double>(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitN_My),
                plc.Read<double>(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitN_Mz)
                );
        }

        /// <summary>
        /// 讀取六軸力覺Sensor的壓力值上限設定
        /// </summary>
        /// <returns></returns>
        public Tuple<double, double, double, double, double, double> ReadSixAxisSensorUpperLimit()
        {
            var plc = this.plcContext;
            return new Tuple<double, double, double, double, double, double>(
                plc.Read<double>(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitP_Fx),
                plc.Read<double>(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitP_Fy),
                plc.Read<double>(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitP_Fz),
                plc.Read<double>(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitP_Mx),
                plc.Read<double>(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitP_My),
                plc.Read<double>(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitP_Mz)
                );
        }

        /// <summary>
        /// 設定六軸力覺Sensor的壓力下限
        /// </summary>
        /// <param name="Fx"></param>
        /// <param name="Fy"></param>
        /// <param name="Fz"></param>
        /// <param name="Mx"></param>
        /// <param name="My"></param>
        /// <param name="Mz"></param>
        public void SetSixAxisSensorLowerLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz)
        {
            var plc = this.plcContext;
            if (Fx != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitN_Fx, Fx);
            if (Fy != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitN_Fy, Fy);
            if (Fz != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitN_Fz, Fz);
            if (Mx != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitN_Mx, Mx);
            if (My != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitN_My, My);
            if (Mz != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitN_Mz, Mz);
        }

        /// <summary>
        /// 設定六軸力覺Sensor的壓力上限
        /// </summary>
        /// <param name="Fx"></param>
        /// <param name="Fy"></param>
        /// <param name="Fz"></param>
        /// <param name="Mx"></param>
        /// <param name="My"></param>
        /// <param name="Mz"></param>
        public void SetSixAxisSensorUpperLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz)
        {
            var plc = this.plcContext;
            if (Fx != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitP_Fx, Fx);
            if (Fy != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitP_Fy, Fy);
            if (Fz != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitP_Fz, Fz);
            if (Mx != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitP_Mx, Mx);
            if (My != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitP_My, My);
            if (Mz != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_ForceLimitP_Mz, Mz);
        }
        #endregion

        #region 夾爪觸覺Sensor
        /// <summary>
        /// 讀取夾爪後端觸覺數值(後端Sensor會有三個數值)
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int> ReadClampTactile_BehindSide()
        {
            var plc = plcContext;
            return new Tuple<int, int, int>(
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Tactile_Down_1),
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Tactile_Down_2),
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Tactile_Down_3)
                );
        }

        /// <summary>
        /// 讀取夾爪前端觸覺數值(前端Sensor會有三個數值)
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int> ReadClampTactile_FrontSide()
        {
            var plc = plcContext;
            return new Tuple<int, int, int>(
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Tactile_Up_1),
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Tactile_Up_2),
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Tactile_Up_3)
                );
        }

        /// <summary>
        /// 讀取夾爪左側觸覺數值(左側Sensor會有六個數值)
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int, int, int, int> ReadClampTactile_LeftSide()
        {
            var plc = plcContext;
            return new Tuple<int, int, int, int, int, int>(
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Tactile_Left_1),
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Tactile_Left_2),
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Tactile_Left_3),
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Tactile_Left_4),
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Tactile_Left_5),
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Tactile_Left_6)
                );
        }

        /// <summary>
        /// 讀取夾爪右側觸覺數值(右側Sensor會有六個數值)
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int, int, int, int> ReadClampTactile_RightSide()
        {
            var plc = plcContext;
            return new Tuple<int, int, int, int, int, int>(
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Tactile_Right_1),
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Tactile_Right_2),
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Tactile_Right_3),
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Tactile_Right_4),
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Tactile_Right_5),
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Tactile_Right_6)
                );
        }

        /// <summary>
        /// 讀取夾爪觸覺極限設定值，上限、下限
        /// </summary>
        /// <returns>上限、下限</returns>
        public Tuple<int, int> ReadClampTactileLimit()
        {
            var plc = plcContext;
            return new Tuple<int, int>(
                plc.Read<int>(EnumMacHalPlcVariable.PC_TO_MT_Tactile_Limit_Up),
                plc.Read<int>(EnumMacHalPlcVariable.PC_TO_MT_Tactile_Limit_Down)
                );
        }

        /// <summary>
        /// 設定夾爪觸覺極限，上限、下限
        /// </summary>
        /// <param name="TactileLimit_Up">上限</param>
        /// <param name="TactileLimit_Down">下限</param>
        public void SetClampTactileLim(int? TactileLimit_Up, int? TactileLimit_Down)
        {
            var plc = plcContext;
            if (TactileLimit_Up != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Tactile_Limit_Up, TactileLimit_Up);
            if (TactileLimit_Down != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Tactile_Limit_Down, TactileLimit_Down);
        }
        #endregion

        #region 水平Sensor
        /// <summary>
        /// 讀取三軸水平數值
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int> ReadLevel()
        {
            var plc = plcContext;
            return new Tuple<int, int, int>(
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Level_X),
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Level_Y),
                plc.Read<int>(EnumMacHalPlcVariable.MT_TO_PC_Level_Z)
                );
        }

        /// <summary>
        /// 讀取三軸水平極限值設定
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int> ReadLevelLimit()
        {
            var plc = plcContext;
            return new Tuple<int, int, int>(
                plc.Read<int>(EnumMacHalPlcVariable.PC_TO_MT_Level_Limit_X),
                plc.Read<int>(EnumMacHalPlcVariable.PC_TO_MT_Level_Limit_Y),
                plc.Read<int>(EnumMacHalPlcVariable.PC_TO_MT_Level_Limit_Z)
                );
        }

        /// <summary>
        /// 設定三軸水平極限值
        /// </summary>
        /// <param name="Level_X"></param>
        /// <param name="Level_Y"></param>
        /// <param name="Level_Z"></param>
        public void SetLevelLimit(int? Level_X, int? Level_Y, int? Level_Z)
        {
            var plc = plcContext;
            if (Level_X != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Level_Limit_X, Level_X);
            if (Level_Y != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Level_Limit_Y, Level_Y);
            if (Level_Z != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_MT_Level_Limit_Z, Level_Z);
        }
        #endregion

        #region 靜電感測
        /// <summary>
        /// 讀取靜電測量值
        /// </summary>
        /// <returns></returns>
        public double ReadStaticElec()
        {
            var plc = this.plcContext;
            return plc.Read<double>(EnumMacHalPlcVariable.MT_TO_PC_StaticElectricity_Value);
        }

        /// <summary>
        /// 讀取靜電感測的區間限制設定值
        /// </summary>
        /// <returns></returns>
        public Tuple<double, double> ReadStaticElecLimit()
        {
            var plc = this.plcContext;
            return new Tuple<double, double>(
                plc.Read<double>(EnumMacHalPlcVariable.MT_TO_PC_StaticElectricity_Limit_UP),
                plc.Read<double>(EnumMacHalPlcVariable.MT_TO_PC_StaticElectricity_Limit_Down)
            );
        }

        /// <summary>
        /// 設定靜電感測的區間限制
        /// </summary>
        /// <param name="Minimum"></param>
        /// <param name="Maximum"></param>
        public void SetStaticElecLimit(double? Maximum, double? Minimum)
        {
            var plc = this.plcContext;
            if (Maximum != null)
                plc.Write(EnumMacHalPlcVariable.MT_TO_PC_StaticElectricity_Limit_UP, Maximum);
            if (Minimum != null)
                plc.Write(EnumMacHalPlcVariable.MT_TO_PC_StaticElectricity_Limit_Down, Minimum);
        }
        #endregion


        public void WriteInitialCmd(bool flag) { this.plcContext.Write(EnumMacHalPlcVariable.PC_TO_MT_Initial_A04, flag); }
        public void WriteClampCmd(bool flag) { this.plcContext.Write(EnumMacHalPlcVariable.PC_TO_MT_Clamp, flag); }
        public void WriteUnClampCmd(bool flag) { this.plcContext.Write(EnumMacHalPlcVariable.PC_TO_MT_Unclamp, flag); }


    }

}
