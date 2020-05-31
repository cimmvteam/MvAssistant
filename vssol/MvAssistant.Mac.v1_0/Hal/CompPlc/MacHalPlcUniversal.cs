using MvAssistant.Mac.v1_0.Hal.CompPlc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompPlc
{

    [Guid("6C08618D-3A39-4B00-A6F0-10CF12D162D7")]
    public class MacHalPlcUniversal : MacHalPlcBase, IMacHalPlcUniversal
    {
        public MacHalPlcUniversal() { }

        public MacHalPlcUniversal(MacHalPlcContext plc = null)
        { this.m_PlcContext = plc; }


        #region MacHalPlcBase

        public override int HalConnect()
        {
            var rtn = base.HalConnect();
            this.m_PlcContext.Connect();
            return rtn;
        }

        #endregion



        //信號燈
        public void SetSignalTower(bool Red, bool Orange, bool Blue)
        {
            var plc = this.m_PlcContext;
            plc.Write(MacHalPlcEnumVariable.PC_TO_DR_Red, Red);
            plc.Write(MacHalPlcEnumVariable.PC_TO_DR_Orange, Orange);
            plc.Write(MacHalPlcEnumVariable.PC_TO_DR_Blue, Blue);
        }

        //蜂鳴器
        public void SetBuzzer(uint BuzzerType)
        {
            var plc = this.m_PlcContext;
            plc.Write(MacHalPlcEnumVariable.PC_TO_DR_Buzzer, BuzzerType);
        }

        /// <summary>
        /// A08外罩風扇編號、風速控制
        /// </summary>
        /// <param name="FanID"></param>
        /// <param name="WindSpeed"></param>
        /// <returns></returns>
        public string CoverFanCtrl(uint FanID, uint WindSpeed)
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_FFU_SetSpeed, WindSpeed);
                plc.Write(MacHalPlcEnumVariable.PC_TO_FFU_Address, FanID);
                plc.Write(MacHalPlcEnumVariable.PC_TO_FFU_Write, false);
                Thread.Sleep(100);
                plc.Write(MacHalPlcEnumVariable.PC_TO_FFU_Write, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.FFU_TO_PC_Write_Reply), 1000))
                    throw new MvException("Outer Cover Fan Control T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.FFU_TO_PC_Write_Complete), 5000))
                    throw new MvException("Outer Cover Fan Control T2 timeout");

                switch (plc.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_Write_Result))
                {
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        Result = "Failed";
                        break;
                }

                plc.Write(MacHalPlcEnumVariable.PC_TO_FFU_Write, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MacHalPlcEnumVariable.FFU_TO_PC_Write_Complete), 1000))
                    throw new MvException("Outer Cover Fan Control T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MacHalPlcEnumVariable.PC_TO_FFU_Write, false);
                throw ex;
            }
            return Result;
        }

        public List<int> ReadCoverFanSpeed()
        {
            var plc = this.m_PlcContext;
            List<int> FanSpeedList = new List<int>();
            FanSpeedList.Add(plc.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_1));
            FanSpeedList.Add(plc.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_2));
            FanSpeedList.Add(plc.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_3));
            FanSpeedList.Add(plc.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_4));
            FanSpeedList.Add(plc.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_5));
            FanSpeedList.Add(plc.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_6));
            FanSpeedList.Add(plc.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_7));
            FanSpeedList.Add(plc.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_8));
            FanSpeedList.Add(plc.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_9));
            FanSpeedList.Add(plc.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_10));
            FanSpeedList.Add(plc.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_11));
            FanSpeedList.Add(plc.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_12));
            return FanSpeedList;

        }

        public void ResetAllAlarm()
        {
            var plc = this.m_PlcContext;
            try
            {
                plc.Write(MacHalPlcEnumVariable.Reset_ALL, false);
                Thread.Sleep(100);
                plc.Write(MacHalPlcEnumVariable.Reset_ALL, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MacHalPlcEnumVariable.Reset_ALL_Complete), 2000))
                    throw new MvException("Reset All T0 timeout");

                plc.Write(MacHalPlcEnumVariable.Reset_ALL, false);
            }
            catch (Exception ex)
            {
                plc.Write(MacHalPlcEnumVariable.Reset_ALL, false);
                throw ex;
            }
        }

        /// <summary>
        /// 當Assembly出錯時，針對部件下緊急停止訊號
        /// </summary>
        /// <param name="BT_EMS">Box Transfer是否緊急停止</param>
        /// <param name="RT_EMS">Mask Transfer是否緊急停止</param>
        /// <param name="OS_EMS">Open Stage是否緊急停止</param>
        /// <param name="IC_EMS">Inspection Chamber是否緊急停止</param>
        public void EMSAlarm(bool BT_EMS, bool RT_EMS, bool OS_EMS, bool IC_EMS)
        {
            var plc = this.m_PlcContext;
            plc.Write(MacHalPlcEnumVariable.PC_TO_BT_EMS, BT_EMS);
            plc.Write(MacHalPlcEnumVariable.PC_TO_MT_EMS, RT_EMS);
            plc.Write(MacHalPlcEnumVariable.PC_TO_OS_EMS, OS_EMS);
            plc.Write(MacHalPlcEnumVariable.PC_TO_IC_EMS, IC_EMS);
            Thread.Sleep(1000);
            if (plc.Read<bool>(MacHalPlcEnumVariable.BT_TO_PC_EMS_Reply) != BT_EMS)
                throw new MvException("PLC did not get 'Box Transfer EMS' alarm signal");
            else if (plc.Read<bool>(MacHalPlcEnumVariable.MT_TO_PC_EMS_Reply) != RT_EMS)
                throw new MvException("PLC did not get 'Mask Transfer EMS' alarm signal");
            else if (plc.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_EMS_Reply) != OS_EMS)
                throw new MvException("PLC did not get 'Open Stage EMS' alarm signal");
            else if (plc.Read<bool>(MacHalPlcEnumVariable.IC_TO_PC_EMS_Reply) != IC_EMS)
                throw new MvException("PLC did not get 'Inspection Chamber EMS' alarm signal");

        }

        #region PLC狀態訊號
        /// <summary>
        /// 讀取電源狀態，True：Power ON 、 False：Power OFF
        /// </summary>
        /// <returns>True：Power ON、False：Power OFF</returns>
        public bool ReadPowerON()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_PowerON);
        }

        /// <summary>
        /// 讀取設備內部，主控盤旁鑰匙鎖狀態，True：Maintenance 、 False：Auto
        /// </summary>
        /// <returns>True：Maintenance、False：Auto</returns>
        public bool ReadBCP_Maintenance()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_Maintenance);
        }

        /// <summary>
        /// 讀取設備外部，抽屜旁鑰匙鎖狀態，True：Maintenance 、 False：Auto
        /// </summary>
        /// <returns>True：Maintenance、False：Auto</returns>
        public bool ReadCB_Maintenance()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_CB_Maintenance);
        }

        /// <summary>
        /// 讀取主控盤EMO按鈕是否觸發，True：Push 、 False：Release
        /// </summary>
        /// <returns>True：Push、False：Release</returns>
        public Tuple<bool, bool, bool, bool, bool> ReadBCP_EMO()
        {
            var plc = this.m_PlcContext;
            return new Tuple<bool, bool, bool, bool, bool>(
                plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_EMO1),
                plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_EMO2),
                plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_EMO3),
                plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_EMO4),
                plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_EMO5)
                );
        }

        /// <summary>
        /// 讀取抽屜EMO按鈕是否觸發，True：Push 、 False：Release
        /// </summary>
        /// <returns>True：Push、False：Release</returns>
        public Tuple<bool, bool, bool> ReadCB_EMO()
        {
            var plc = this.m_PlcContext;
            return new Tuple<bool, bool, bool>(
                plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_CB_EMO1),
                plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_CB_EMO2),
                plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_CB_EMO3)
                );
        }

        /// <summary>
        /// 讀取Load Port 1 EMO按鈕是否觸發，True：Push 、 False：Release
        /// </summary>
        /// <returns>True：Push、False：Release</returns>
        public bool ReadLP1_EMO()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_LP1_EMO);
        }

        /// <summary>
        /// 讀取Load Port 2 EMO按鈕是否觸發，True：Push 、 False：Release
        /// </summary>
        /// <returns>True：Push、False：Release</returns>
        public bool ReadLP2_EMO()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_LP2_EMO);
        }

        /// <summary>
        /// 讀取主控盤的門，True：Open 、 False：Close
        /// </summary>
        /// <returns>True：Open、False：Close</returns>
        public bool ReadBCP_Door()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_Door);
        }

        /// <summary>
        /// 讀取Load Port 1的門，True：Open 、 False：Close
        /// </summary>
        /// <returns>True：Open、False：Close</returns>
        public bool ReadLP1_Door()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_LP1_Door);
        }

        /// <summary>
        /// 讀取Load Port 2的門，True：Open 、 False：Close
        /// </summary>
        /// <returns>True：Open、False：Close</returns>
        public bool ReadLP2_Door()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_LP2_Door);
        }

        /// <summary>
        /// 讀取主控箱內的偵煙器是否偵測到訊號，True：Alarm 、 False：Normal
        /// </summary>
        /// <returns>True：Alarm、False：Normal</returns>
        public bool ReadBCP_Smoke()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_Smoke);
        }

        #endregion

        #region PLC alarm signal
        public string ReadAlarm_General()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                var AlarmList = plc.Read<bool[]>(MacHalPlcEnumVariable.General_Alarm);

                for (int i = 0; i < AlarmList.Length; i++)
                {
                    if (AlarmList[i])
                        if (i == 0)
                            Result += "PC Not Reset CheckClock, ";
                        else if (i == 1)
                            Result += "PLC Error, ";
                        else if (i == 2)
                            Result += "EIP Error, ";
                        else if (i == 3)
                            Result += "EtherCat Error, ";
                        else
                            Result += "Unknown Alarm Signal, ";
                }

                if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                    Result = Result.Substring(0, Result.Length - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string ReadAlarm_Cabinet()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                var AlarmList = plc.Read<bool[]>(MacHalPlcEnumVariable.A01_Alarm);

                for (int i = 0; i < AlarmList.Length; i++)
                {
                    if (AlarmList[i])
                        if (i == 0)
                            Result += "DP1 TCP net error, ";
                        else if (i == 1)
                            Result += "DP2 TCP net error, ";
                        else if (i == 2)
                            Result += "DP1 Out range, ";
                        else if (i == 3)
                            Result += "DP2 Out range, ";
                        else
                            Result += "Unknown Alarm Signal, ";
                }

                if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                    Result = Result.Substring(0, Result.Length - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string ReadAlarm_CleanCh()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                var AlarmList = plc.Read<bool[]>(MacHalPlcEnumVariable.A02_Alarm);

                for (int i = 0; i < AlarmList.Length; i++)
                {
                    if (AlarmList[i])
                        if (i == 0)
                            Result += "PD skt error, ";
                        else if (i == 1)
                            Result += "DP skt error, ";
                        else if (i == 2)
                            Result += "DP out range, ";
                        else if (i == 3)
                            Result += "PD-S out range, ";
                        else if (i == 4)
                            Result += "PD-M out range, ";
                        else if (i == 5)
                            Result += "PD-L out range, ";
                        else if (i == 6)
                            Result += "About laser sensor open circuit, ";
                        else if (i == 7)
                            Result += "UpDown laser sensor open circuit, ";
                        else
                            Result += "Unknown Alarm Signal, ";
                }

                if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                    Result = Result.Substring(0, Result.Length - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string ReadAlarm_BTRobot()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                var AlarmList = plc.Read<bool[]>(MacHalPlcEnumVariable.A03_Alarm);

                for (int i = 0; i < AlarmList.Length; i++)
                {
                    if (AlarmList[i])
                        if (i == 0)
                            Result += "BT servo error, ";
                        else if (i == 1)
                            Result += "Hand point laser sensor open circuit, ";
                        else if (i == 2)
                            Result += "Prevent collision laser sensor open circuit, ";
                        else if (i == 3)
                            Result += "Level sensor X open circuit, ";
                        else if (i == 4)
                            Result += "Level sensor Y open circuit, ";
                        else
                            Result += "Unknown Alarm Signal, ";
                }

                if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                    Result = Result.Substring(0, Result.Length - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string ReadAlarm_MTRobot()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                var AlarmList = plc.Read<bool[]>(MacHalPlcEnumVariable.A04_Alarm);

                for (int i = 0; i < AlarmList.Length; i++)
                {
                    if (AlarmList[i])
                        if (i == 0)
                            Result += "Up定位誤差過大, ";
                        else if (i == 1)
                            Result += "Down定位誤差過大, ";
                        else if (i == 2)
                            Result += "Left定位誤差過大, ";
                        else if (i == 3)
                            Result += "Right定位誤差過大, ";
                        else if (i == 4)
                            Result += "Up Move Error, ";
                        else if (i == 5)
                            Result += "Down Move Error, ";
                        else if (i == 6)
                            Result += "Left Move Error, ";
                        else if (i == 7)
                            Result += "Right Move Error, ";
                        else if (i == 8)
                            Result += "Tactile out range, ";
                        else
                            Result += "Unknown Alarm Signal, ";
                }

                if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                    Result = Result.Substring(0, Result.Length - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string ReadAlarm_OpenStage()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                var AlarmList = plc.Read<bool[]>(MacHalPlcEnumVariable.A05_Alarm);

                for (int i = 0; i < AlarmList.Length; i++)
                {
                    if (AlarmList[i])
                        if (i == 0)
                            Result += "Clamp步進馬達Alarm, ";
                        else if (i == 1)
                            Result += "Cover1 servo alarm, ";
                        else if (i == 2)
                            Result += "Cover2 servo alarm, ";
                        else if (i == 3)
                            Result += "整定電動缸1Alarm, ";
                        else if (i == 4)
                            Result += "整定電動缸2Alarm, ";
                        else if (i == 5)
                            Result += "滑台1Alarm, ";
                        else if (i == 6)
                            Result += "滑台2Alarm, ";
                        else if (i == 7)
                            Result += "Slider未同步做動, ";
                        else if (i == 8)
                            Result += "Clamp扭力錯誤, ";
                        else if (i == 9)
                            Result += "氣壓不足, ";
                        else if (i == 10)
                            Result += "Cover1 Deviation is too large, ";
                        else if (i == 11)
                            Result += "Cover2 Deviation is too large, ";
                        else
                            Result += "Unknown Alarm Signal, ";
                }

                if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                    Result = Result.Substring(0, Result.Length - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string ReadAlarm_InspCh()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                var AlarmList = plc.Read<bool[]>(MacHalPlcEnumVariable.A06_Alarm);


                for (int i = 0; i < AlarmList.Length; i++)
                {
                    if (AlarmList[i])
                        if (i == 1)
                            Result += "X 驅動器Alarm, ";
                        else if (i == 2)
                            Result += "Y 驅動器Alarm, ";
                        else if (i == 3)
                            Result += "Z 驅動器Alarm, ";
                        else if (i == 4)
                            Result += "W 驅動器Alarm, ";
                        else if (i == 5)
                            Result += "動作中 Robot侵入, ";
                        else
                            Result += "Unknown Alarm Signal, ";
                }

                if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                    Result = Result.Substring(0, Result.Length - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string ReadAlarm_LoadPort()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                var AlarmList = plc.Read<bool[]>(MacHalPlcEnumVariable.A07_Alarm);

                for (int i = 0; i < AlarmList.Length; i++)
                {
                    if (AlarmList[i])
                        if (i == 0)
                            Result += "DP1 TCP Error, ";
                        else if (i == 1)
                            Result += "DP2 TCP Error, ";
                        else if (i == 2)
                            Result += "DP1 Out Range, ";
                        else if (i == 3)
                            Result += "DP2 Out Range, ";
                        else
                            Result += "Unknown Alarm Signal, ";
                }


                if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                    Result = Result.Substring(0, Result.Length - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string ReadAlarm_CoverFan()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                var AlarmList = plc.Read<bool[]>(MacHalPlcEnumVariable.A08_Alarm);

                for (int i = 0; i < AlarmList.Length; i++)
                {
                    if (AlarmList[i])
                        if (i == 0)
                            Result += "FFU1 Error, ";
                        else if (i == 1)
                            Result += "FFU2 Error, ";
                        else if (i == 2)
                            Result += "FFU3 Error, ";
                        else if (i == 3)
                            Result += "FFU4 Error, ";
                        else if (i == 4)
                            Result += "FFU5 Error, ";
                        else if (i == 5)
                            Result += "FFU6 Error, ";
                        else if (i == 6)
                            Result += "FFU7 Error, ";
                        else if (i == 7)
                            Result += "FFU8 Error, ";
                        else if (i == 8)
                            Result += "FFU9 Error, ";
                        else if (i == 9)
                            Result += "FFU10 Error, ";
                        else if (i == 10)
                            Result += "FFU11 Error, ";
                        else if (i == 11)
                            Result += "FFU12 Error, ";
                        else if (i == 12)
                            Result += "RS485 Error, ";
                        else
                            Result += "Unknown Alarm Signal, ";
                }

                if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                    Result = Result.Substring(0, Result.Length - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string ReadAlarm_MTClampInsp()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                var AlarmList = plc.Read<bool[]>(MacHalPlcEnumVariable.A09_Alarm);

                for (int i = 0; i < AlarmList.Length; i++)
                {
                    if (AlarmList[i])
                        if (i == 0)
                            Result += "Laser1 Error (Signal disconnection ), ";
                        else if (i == 1)
                            Result += "Laser2 Error (Signal disconnection ), ";
                        else if (i == 2)
                            Result += "Laser3 Error (Signal disconnection ), ";
                        else if (i == 3)
                            Result += "Laser4 Error (Signal disconnection ), ";
                        else if (i == 4)
                            Result += "Laser5 Error (Signal disconnection ), ";
                        else if (i == 5)
                            Result += "Laser6 Error (Signal disconnection ), ";
                        else
                            Result += "Unknown Alarm Signal, ";
                }

                if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                    Result = Result.Substring(0, Result.Length - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        #endregion

        #region PLC warning signal
        public string ReadWarning_General()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                var WarningList = plc.Read<bool[]>(MacHalPlcEnumVariable.General_Warning);

                for (int i = 0; i < WarningList.Length; i++)
                {
                    if (WarningList[i])
                        Result += "Unknown Warning Signal, ";
                }

                if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                    Result = Result.Substring(0, Result.Length - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string ReadWarning_Cabinet()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                var WarningList = plc.Read<bool[]>(MacHalPlcEnumVariable.A01_Warning);

                for (int i = 0; i < WarningList.Length; i++)
                {
                    if (WarningList[i])
                        Result += "Unknown Warning Signal, ";
                }

                if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                    Result = Result.Substring(0, Result.Length - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string ReadWarning_CleanCh()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                var WarningList = plc.Read<bool[]>(MacHalPlcEnumVariable.A02_Warning);

                for (int i = 0; i < WarningList.Length; i++)
                {
                    if (WarningList[i])
                        if (i == 0)
                            Result += "Clean T3 Timeout, ";
                        else
                            Result += "Unknown Warning Signal, ";
                }

                if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                    Result = Result.Substring(0, Result.Length - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string ReadWarning_BTRobot()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                var WarningList = plc.Read<bool[]>(MacHalPlcEnumVariable.A03_Warning);

                for (int i = 0; i < WarningList.Length; i++)
                {
                    if (WarningList[i])
                        if (i == 0)
                            Result += "Under maintenance, ";
                        else if (i == 1)
                            Result += "Clamp T3 Timeout, ";
                        else if (i == 2)
                            Result += "Unclamp T3 Timeout, ";
                        else if (i == 3)
                            Result += "動作中Command訊號消失, ";
                        else if (i == 4)
                            Result += "No Power ON, ";
                        else if (i == 5)
                            Result += "Please Initial, ";
                        else if (i == 6)
                            Result += "RS422 Error, ";
                        else if (i == 7)
                            Result += "Setting speed out range, ";
                        else if (i == 8)
                            Result += "Clamp T1 Timeout, ";
                        else if (i == 9)
                            Result += "Unclamp T1 Timeout, ";
                        else
                            Result += "Unknown Warning Signal, ";
                }

                if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                    Result = Result.Substring(0, Result.Length - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string ReadWarning_MTRobot()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                var WarningList = plc.Read<bool[]>(MacHalPlcEnumVariable.A04_Warning);

                for (int i = 0; i < WarningList.Length; i++)
                {
                    if (WarningList[i])
                        if (i == 0)
                            Result += "RS422 Error, ";
                        else if (i == 1)
                            Result += "RS232 Error, ";
                        else if (i == 2)
                            Result += "Initial T1 Timeout, ";
                        else if (i == 3)
                            Result += "Clamp T1 Timeout, ";
                        else if (i == 4)
                            Result += "Unclamp T1 Timeout, ";
                        else if (i == 5)
                            Result += "Initial T3 Timeout, ";
                        else if (i == 6)
                            Result += "Clamp T3 Timeout, ";
                        else if (i == 7)
                            Result += "Unclamp T3 Timeout, ";
                        else
                            Result += "Unknown Warning Signal, ";
                }

                if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                    Result = Result.Substring(0, Result.Length - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string ReadWarning_OpenStage()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                var WarningList = plc.Read<bool[]>(MacHalPlcEnumVariable.A05_Warning);

                for (int i = 0; i < WarningList.Length; i++)
                {
                    if (WarningList[i])
                        if (i == 0)
                            Result += "Robot 侵入中不可執行Command, ";
                        else if (i == 1)
                            Result += "Please initial, ";
                        else if (i == 2)
                            Result += "SortClamp T1 Timeout, ";
                        else if (i == 3)
                            Result += "SortUnclamp T1 Timeout, ";
                        else if (i == 4)
                            Result += "Vacuum T1 Timeout, ";
                        else if (i == 5)
                            Result += "Clamp T1 Timeout, ";
                        else if (i == 6)
                            Result += "Unclamp T1 Timeout, ";
                        else if (i == 7)
                            Result += "Open T1 Timeout, ";
                        else if (i == 8)
                            Result += "Close T1 Timeout, ";
                        else if (i == 9)
                            Result += "Lock T1 Timeout, ";
                        else if (i == 10)
                            Result += "Initial T1 Timeout, ";
                        else if (i == 11)
                            Result += "SortClamp T3 Timeout, ";
                        else if (i == 12)
                            Result += "SortUnclamp T3 Timeout, ";
                        else if (i == 13)
                            Result += "Vacuum T3 Timeout, ";
                        else if (i == 14)
                            Result += "Clamp T3 Timeout, ";
                        else if (i == 15)
                            Result += "Unclamp T3 Timeout, ";
                        else if (i == 16)
                            Result += "Open T3 Timeout, ";
                        else if (i == 17)
                            Result += "Close T3 Timeout, ";
                        else if (i == 18)
                            Result += "Lock T3 Timeout, ";
                        else if (i == 19)
                            Result += "Initial T3 Timeout, ";
                        else
                            Result += "Unknown Warning Signal, ";
                }

                if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                    Result = Result.Substring(0, Result.Length - 2);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string ReadWarning_InspCh()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                var WarningList = plc.Read<bool[]>(MacHalPlcEnumVariable.A06_Warning);

                for (int i = 0; i < WarningList.Length; i++)
                {
                    if (WarningList[i])
                        if (i == 0)
                            Result += "X軸 驅動器Warning, ";
                        else if (i == 1)
                            Result += "Y軸 驅動器Warning, ";
                        else if (i == 2)
                            Result += "Z軸 驅動器Warning, ";
                        else if (i == 3)
                            Result += "W軸 驅動器Warning, ";
                        else if (i == 4)
                            Result += "動作中Jog Command下達, ";
                        else if (i == 5)
                            Result += "動作中下達W Command, ";
                        else if (i == 6)
                            Result += "動作中下達Z Command, ";
                        else if (i == 7)
                            Result += "動作中下達XY Command, ";
                        else if (i == 8)
                            Result += "Robot侵入中不可做動, ";
                        else if (i == 9)
                            Result += "Initial T1 Timeout, ";
                        else if (i == 10)
                            Result += "Z T1 Timeout, ";
                        else if (i == 11)
                            Result += "XY T1 Timeout, ";
                        else if (i == 12)
                            Result += "W T1 Timeout, ";
                        else if (i == 13)
                            Result += "Initial T3 Timeout, ";
                        else if (i == 14)
                            Result += "Z T3 Timeout, ";
                        else if (i == 15)
                            Result += "XY T3 Timeout, ";
                        else if (i == 16)
                            Result += "W T3 Timeout, ";
                        else
                            Result += "Unknown Warning Signal, ";
                }

                if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                    Result = Result.Substring(0, Result.Length - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string ReadWarning_LoadPort()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                var WarningList = plc.Read<bool[]>(MacHalPlcEnumVariable.A07_Warning);

                for (int i = 0; i < WarningList.Length; i++)
                {
                    if (WarningList[i])
                        Result += "Unknown Warning Signal, ";
                }

                if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                    Result = Result.Substring(0, Result.Length - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string ReadWarning_CoverFan()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                var WarningList = plc.Read<bool[]>(MacHalPlcEnumVariable.A08_Warning);

                for (int i = 0; i < WarningList.Length; i++)
                {
                    if (WarningList[i])
                        Result += "Unknown Warning Signal, ";
                }

                if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                    Result = Result.Substring(0, Result.Length - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string ReadWarning_MTClampInsp()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                var WarningList = plc.Read<bool[]>(MacHalPlcEnumVariable.A09_Warning);

                for (int i = 0; i < WarningList.Length; i++)
                {
                    if (WarningList[i])
                        Result += "Unknown Warning Signal, ";
                }

                if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                    Result = Result.Substring(0, Result.Length - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        #endregion
    }
}
