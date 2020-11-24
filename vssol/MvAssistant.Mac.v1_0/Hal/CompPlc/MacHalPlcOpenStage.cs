using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MvAssistant.Mac.v1_0.Hal.CompPlc
{
    [Guid("90A5ACB4-3B35-429C-B9B4-DF1E63AF267B")]
    public class MacHalPlcOpenStage : MacHalPlcBase, IMacHalPlcOpenStage
    {


        public MacHalPlcOpenStage() { }
        public MacHalPlcOpenStage(MacHalPlcContext plc = null)
        {
            this.plcContext = plc;
        }

        public string Open()
        {
            var Result = "";
            var plc = this.plcContext;
            try
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Open, false);
                Thread.Sleep(100);
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Open, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Open_Reply), 1000))
                    throw new MvException("Open Stage Open Box T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Open_Complete), 10 * 1000))
                    throw new MvException("Open Stage Open Box T2 timeout");

                switch (plc.Read<int>(MacHalPlcEnumVariable.OS_TO_PC_Open_Result))
                {
                    case 0:
                        throw new MvException("Open Stage Open Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        throw new MvException("Open Stage Open Error : No vacuum not ready");
                    case 3:
                        throw new MvException("Open Stage Open Error : Slider point error");
                    case 4:
                        throw new MvException("Open Stage Open Error : Sort not unclamp");
                    case 5:
                        throw new MvException("Open Stage Open Error : Jaws not clamp");
                    default:
                        throw new MvException("Open Stage Open Error : Unknown error");
                }

                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Open, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Open_Complete), 1000))
                    throw new MvException("Open Stage Open Box T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Open, false);
                throw ex;
            }
            return Result;
        }

        public string Close()
        {
            var Result = "";
            var plc = this.plcContext;
            try
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Close, false);
                Thread.Sleep(100);
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Close, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Close_Reply), 1000))
                    throw new MvException("Open Stage Close Box T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Close_Complete), 10 * 1000))
                    throw new MvException("Open Stage Close Box T2 timeout");

                switch (plc.Read<int>(MacHalPlcEnumVariable.OS_TO_PC_Close_Result))
                {
                    case 0:
                        throw new MvException("Open Stage Close Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        throw new MvException("Open Stage Close Error : No box");
                    case 3:
                        throw new MvException("Open Stage Close Error : Slider point unknown");
                    case 4:
                        throw new MvException("Open Stage Close Error : Cover point unknown");
                    default:
                        throw new MvException("Open Stage Close Error : Unknown error");
                }

                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Close, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Close_Complete), 1000))
                    throw new MvException("Open Stage Close Box T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Close, false);
                throw ex;
            }
            return Result;
        }

        /// <summary>
        /// 開盒夾爪閉合
        /// </summary>
        /// <returns></returns>
        public string Clamp()
        {
            var Result = "";
            var plc = this.plcContext;
            try
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Clamp, false);
                Thread.Sleep(100);
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Clamp, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Clamp_Reply), 1000))
                    throw new MvException("Open Stage Clamp T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Clamp_Complete), 5000))
                    throw new MvException("Open Stage Clamp T2 timeout");

                switch (plc.Read<int>(MacHalPlcEnumVariable.OS_TO_PC_Clamp_Result))
                {
                    case 0:
                        throw new MvException("Open Stage Clamp Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        throw new MvException("Open Stage Clamp Error : Vacuum not ready");
                    case 3:
                        throw new MvException("Open Stage Clamp Error : Not close");
                    default:
                        throw new MvException("Open Stage Clamp Error : Unknown error");
                }

                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Clamp, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Clamp_Complete), 1000))
                    throw new MvException("Open Stage Clamp T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Clamp, false);
                throw ex;
            }
            return Result;
        }

        /// <summary>
        /// 開盒夾爪鬆開
        /// </summary>
        /// <returns></returns>
        public string Unclamp()
        {
            var Result = "";
            var plc = this.plcContext;
            try
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Unclamp, false);
                Thread.Sleep(100);
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Unclamp, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Unclamp_Reply), 1000))
                    throw new MvException("Open Stage Unclamp T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Unclamp_Complete), 5000))
                    throw new MvException("Open Stage Unclamp T2 timeout");

                switch (plc.Read<int>(MacHalPlcEnumVariable.OS_TO_PC_Unclamp_Result))
                {
                    case 0:
                        throw new MvException("Open Stage Unclamp Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        throw new MvException("Open Stage Unclamp Error : Fail");
                    case 3:
                        throw new MvException("Open Stage Unclamp Error : Not close");
                    default:
                        throw new MvException("Open Stage Unclamp Error : Unknown error");
                }

                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Unclamp, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Unclamp_Complete), 1000))
                    throw new MvException("Open Stage Unclamp T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Unclamp, false);
                throw ex;
            }
            return Result;
        }

        /// <summary>
        /// Stage上固定Box的夾具閉合
        /// </summary>
        /// <returns></returns>
        public string SortClamp()
        {
            var Result = "";
            var plc = this.plcContext;
            try
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_SortClamp, false);
                Thread.Sleep(100);
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_SortClamp, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_SortClamp_Reply), 1000))
                    throw new MvException("Open Stage SortClamp T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_SortClamp_Complete), 5000))
                    throw new MvException("Open Stage SortClamp T2 timeout");

                switch (plc.Read<int>(MacHalPlcEnumVariable.OS_TO_PC_SortClamp_Result))
                {
                    case 0:
                        throw new MvException("Open Stage SortClamp Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        throw new MvException("Open Stage SortClamp Error : No box");
                    case 3:
                        throw new MvException("Open Stage SortClamp Error : Jaws clamp");
                    case 4:
                        throw new MvException("Open Stage SortClamp Error : No box type");
                    default:
                        throw new MvException("Open Stage SortClamp Error : Unknown error");
                }

                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_SortClamp, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_SortClamp_Complete), 1000))
                    throw new MvException("Open Stage SortClamp T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_SortClamp, false);
                throw ex;
            }
            return Result;
        }

        /// <summary>
        /// Stage上固定Box的夾具鬆開
        /// </summary>
        /// <returns></returns>
        public string SortUnclamp()
        {
            var Result = "";
            var plc = this.plcContext;
            try
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_SortUnclamp, false);
                Thread.Sleep(100);
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_SortUnclamp, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_SortUnclamp_Reply), 1000))
                    throw new MvException("Open Stage SortUnclamp T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_SortUnclamp_Complete), 5000))
                    throw new MvException("Open Stage SortUnclamp T2 timeout");

                switch (plc.Read<int>(MacHalPlcEnumVariable.OS_TO_PC_SortUnclamp_Result))
                {
                    case 0:
                        throw new MvException("Open Stage SortUnclamp Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    default:
                        throw new MvException("Open Stage SortUnclamp Error : Unknown error");
                }

                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_SortUnclamp, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_SortUnclamp_Complete), 1000))
                    throw new MvException("Open Stage SortUnclamp T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_SortUnclamp, false);
                throw ex;
            }
            return Result;
        }

        /// <summary>
        /// 移到開/關盒鎖的位置
        /// </summary>
        /// <returns></returns>
        public string Lock()
        {
            var Result = "";
            var plc = this.plcContext;
            try
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Lock, false);
                Thread.Sleep(100);
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Lock, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Lock_Reply), 1000))
                    throw new MvException("Open Stage Lock/Unlock T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Lock_Complete), 15 * 1000))
                    throw new MvException("Open Stage Lock/Unlock T2 timeout");

                switch (plc.Read<int>(MacHalPlcEnumVariable.OS_TO_PC_Lock_Result))
                {
                    case 0:
                        throw new MvException("Open Stage Lock/Unlock Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        throw new MvException("Open Stage Lock/Unlock Error : Jaws not unclamp");
                    default:
                        throw new MvException("Open Stage Lock/Unlock Error : Unknown error");
                }

                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Lock, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Lock_Complete), 1000))
                    throw new MvException("Open Stage Lock/Unlock T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Lock, false);
                throw ex;
            }
            return Result;
        }

        /// <summary>
        /// 控制是否吸真空固定盒子
        /// </summary>
        /// <param name="isSuck">True：吸取，False：釋放</param>
        /// <returns></returns>
        public string Vacuum(bool isSuck)
        {
            var Result = "";
            var plc = this.plcContext;
            try
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Vacuum_ON, false);
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Vacuum_OFF, false);
                Thread.Sleep(100);
                if (isSuck)
                    plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Vacuum_ON, true);
                else if (isSuck == false)
                    plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Vacuum_OFF, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Vacuum_Reply), 1000))
                    throw new MvException("Open Stage Vacuum T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Vacuum_Complete), 5000))
                    throw new MvException("Open Stage Vacuum T2 timeout");

                switch (plc.Read<int>(MacHalPlcEnumVariable.OS_TO_PC_Vacuum_Result))
                {
                    case 0:
                        throw new MvException("Open Stage Vacuum Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        throw new MvException("Open Stage Vacuum Error : Fail");
                    case 3:
                        throw new MvException("Open Stage Vacuum Error : No box");
                    case 4:
                        throw new MvException("Open Stage Vacuum Error : Sort not clamp");
                    default:
                        throw new MvException("Open Stage Vacuum Error : Unknown error");
                }

                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Vacuum_ON, false);
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Vacuum_OFF, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Vacuum_Complete), 1000))
                    throw new MvException("Open Stage Vacuum T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Vacuum_ON, false);
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Vacuum_OFF, false);
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
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Initial_A05, false);
                Thread.Sleep(100);
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Initial_A05, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Initial_A05_Reply), 1000))
                    throw new MvException("Open Stage Initial T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Initial_A05_Complete), 300 * 1000))
                    throw new MvException("Open Stage Initial T2 timeout");

                switch (plc.Read<int>(MacHalPlcEnumVariable.OS_TO_PC_Initial_A05_Result))
                {
                    case 0:
                        throw new MvException("Open Stage Initial Error : Invalid");
                    case 1:
                        Result = "OK";
                        break;
                    default:
                        throw new MvException("Open Stage Initial Error : Unknown error");
                }

                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Initial_A05, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_Initial_A05_Complete), 1000))
                    throw new MvException("Open Stage Initial T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Initial_A05, false);
                throw ex;
            }
            return Result;

        }

        /// <summary>
        /// BoxType = 1：鐵盒 , 2：水晶盒
        /// </summary>
        /// <param name="BoxType">BoxType = 1：鐵盒 , 2：水晶盒</param>
        public void SetBoxType(uint BoxType)
        {
            var plc = this.plcContext;
            plc.Write(MacHalPlcEnumVariable.PC_TO_OS_BoxType, BoxType);

        }

        /// <summary>
        /// 讀取盒子種類設定，1:鐵盒 , 2:水晶盒
        /// </summary>
        /// <returns>1:鐵盒 , 2:水晶盒</returns>
        public int ReadBoxTypeSetting()
        {
            string Result = "";
            var plc = this.plcContext;
            return plc.Read<int>(MacHalPlcEnumVariable.PC_TO_OS_BoxType);
        }

        /// <summary>
        /// 設定速度(%)
        /// </summary>
        /// <param name="Speed"></param>
        public void SetSpeed(uint Speed)
        {
            var plc = this.plcContext;
            plc.Write(MacHalPlcEnumVariable.PC_TO_OS_Speed, Speed);

        }

        public int ReadSpeedSetting()
        {
            var plc = this.plcContext;
            return plc.Read<int>(MacHalPlcEnumVariable.PC_TO_OS_Speed);

        }

        /// <summary>
        /// 發送入侵訊號，回傳是否可以入侵
        /// </summary>
        /// <param name="isBTIntrude">BT Robot是否要入侵</param>
        /// <param name="isMTIntrude">MT Robot是否要入侵</param>
        /// <returns></returns>
        public Tuple<bool, bool> ReadRobotIntrude(bool? isBTIntrude, bool? isMTIntrude)
        {
            var plc = this.plcContext;
            try
            {
                if (isBTIntrude != null)
                    plc.Write(MacHalPlcEnumVariable.PC_TO_OS_BTIntrude, !isBTIntrude);
                if (isMTIntrude != null)
                    plc.Write(MacHalPlcEnumVariable.PC_TO_OS_MTIntrude, !isMTIntrude);
                Thread.Sleep(100);

                if (isBTIntrude != null && plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_BTLicense) != isBTIntrude)//如果BT要入侵但不被許可
                    throw new MvException("Box Transfer Intrude is not allowed");
                else if (isMTIntrude != null && plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_MTLicense) != isMTIntrude)//如果MT要入侵但不被許可
                    throw new MvException("Mask Transfer Intrude is not allowed");
            }
            catch (Exception ex)
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_BTIntrude, true);//復歸入侵請求，因為訊號是反向觸發所以復歸成 True
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_MTIntrude, true);//復歸入侵請求，因為訊號是反向觸發所以復歸成 True
                throw ex;
            }

            return new Tuple<bool, bool>(
                plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_BTLicense),
                plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_MTLicense)
                );
        }

        /// <summary>
        /// 讀取開盒夾爪狀態
        /// </summary>
        /// <returns>1: Clamp , 2: Unclamp</returns>
        public string ReadClampStatus()
        {
            string Result = "";
            var plc = this.plcContext;
            switch (plc.Read<int>(MacHalPlcEnumVariable.OS_TO_PC_ClampStatus))
            {
                case 1:
                    Result = "Clamp";
                    break;
                case 2:
                    Result = "Unclamp";
                    break;
            }
            return Result;
        }

        /// <summary>
        /// 讀取Stage上固定Box的夾具位置
        /// </summary>
        /// <returns></returns>
        public Tuple<long, long> ReadSortClampPosition()
        {
            var plc = this.plcContext;

            return new Tuple<long, long>(
                plc.Read<long>(MacHalPlcEnumVariable.OS_TO_PC_SortClamp1_Position),
                plc.Read<long>(MacHalPlcEnumVariable.OS_TO_PC_SortClamp2_Position)
                );
        }

        /// <summary>
        /// 讀取Slider的位置
        /// </summary>
        /// <returns></returns>
        public Tuple<long, long> ReadSliderPosition()
        {
            var plc = this.plcContext;

            return new Tuple<long, long>(
                plc.Read<long>(MacHalPlcEnumVariable.OS_TO_PC_Slider1_Position),
                plc.Read<long>(MacHalPlcEnumVariable.OS_TO_PC_Slider2_Position)
                );
        }

        /// <summary>
        /// 讀取盒蓋位置
        /// </summary>
        /// <returns></returns>
        public Tuple<double, double> ReadCoverPos()
        {
            var plc = this.plcContext;

            return new Tuple<double, double>(
                plc.Read<double>(MacHalPlcEnumVariable.OS_TO_PC_Cover1_Position),
                plc.Read<double>(MacHalPlcEnumVariable.OS_TO_PC_Cover2_Position)
                );
        }

        #region Particle數量監控
        //設定各種大小Particle的數量限制
        public void SetParticleCntLimit(uint? L_Limit, uint? M_Limit, uint? S_Limit)
        {
            var plc = this.plcContext;

            if (L_Limit != null)
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_PD_L_Limit, L_Limit);
            if (M_Limit != null)
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_PD_M_Limit, M_Limit);
            if (S_Limit != null)
                plc.Write(MacHalPlcEnumVariable.PC_TO_OS_PD_S_Limit, S_Limit);
        }

        //讀取各種大小Particle的數量限制
        public Tuple<int, int, int> ReadParticleCntLimitSetting()
        {
            var plc = this.plcContext;

            return new Tuple<int, int, int>(
                plc.Read<int>(MacHalPlcEnumVariable.PC_TO_OS_PD_L_Limit),
                plc.Read<int>(MacHalPlcEnumVariable.PC_TO_OS_PD_M_Limit),
                plc.Read<int>(MacHalPlcEnumVariable.PC_TO_OS_PD_S_Limit)
                );
        }

        //讀取各種大小Particle的數量
        public Tuple<int, int, int> ReadParticleCount()
        {
            var plc = this.plcContext;

            return new Tuple<int, int, int>(
                plc.Read<int>(MacHalPlcEnumVariable.OS_TO_PC_PD_L),
                plc.Read<int>(MacHalPlcEnumVariable.OS_TO_PC_PD_M),
                plc.Read<int>(MacHalPlcEnumVariable.OS_TO_PC_PD_S)
                );
        }
        #endregion Particle數量監控

        /// <summary>
        /// 讀取盒蓋開闔
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, bool> ReadCoverSensor()
        {
            var plc = this.plcContext;

            return new Tuple<bool, bool>(
                plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_CoverSensor_Open),
                plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_CoverSensor_Close)
                );
        }

        /// <summary>
        /// 讀取盒子是否變形
        /// </summary>
        /// <returns></returns>
        public double ReadBoxDeform()
        {
            var plc = this.plcContext;
            return plc.Read<double>(MacHalPlcEnumVariable.OS_TO_PC_SoundWave);
        }

        /// <summary>
        /// 讀取平台上的重量
        /// </summary>
        /// <returns></returns>
        public double ReadWeightOnStage()
        {
            var plc = this.plcContext;
            return plc.Read<double>(MacHalPlcEnumVariable.OS_TO_PC_Weight_Cruuent);
        }

        /// <summary>
        /// 讀取是否有Box
        /// </summary>
        /// <returns></returns>
        public bool ReadBoxExist()
        {
            var plc = this.plcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_BoxCheckOK);
        }

        public string ReadOpenStageStatus()
        {
            string Result = "";
            var plc = this.plcContext;
            switch (plc.Read<int>(MacHalPlcEnumVariable.OS_TO_PC_A05Status))
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

        public bool ReadBeenIntruded()
        {
            var plc = this.plcContext;
            return !plc.Read<bool>(MacHalPlcEnumVariable.PC_TO_OS_BTIntrude) || !plc.Read<bool>(MacHalPlcEnumVariable.PC_TO_OS_MTIntrude);
        }
    }
}
