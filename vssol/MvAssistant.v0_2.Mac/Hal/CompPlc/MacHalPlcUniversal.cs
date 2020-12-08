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
        { this.plcContext = plc; }


        #region MacHalPlcBase

        public override int HalConnect()
        {
            var rtn = base.HalConnect();
            this.plcContext.Connect();
            return rtn;
        }

        #endregion



        //信號燈
        public void SetSignalTower(bool Red, bool Orange, bool Blue)
        {
            var plc = this.plcContext;
            plc.Write(MacHalPlcEnumVariable.PC_TO_DR_Red, Red);
            plc.Write(MacHalPlcEnumVariable.PC_TO_DR_Orange, Orange);
            plc.Write(MacHalPlcEnumVariable.PC_TO_DR_Blue, Blue);
        }

        //蜂鳴器
        public void SetBuzzer(uint BuzzerType)
        {
            var plc = this.plcContext;
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
            var plc = this.plcContext;
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
                        throw new MvException("Outer Cover Fan Control Error : Failed");
                    default:
                        throw new MvException("Outer Cover Fan Control Error : Unknown error");
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
            var plc = this.plcContext;
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
            var plc = this.plcContext;
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
        /// <param name="MT_EMS">Mask Transfer是否緊急停止</param>
        /// <param name="OS_EMS">Open Stage是否緊急停止</param>
        /// <param name="IC_EMS">Inspection Chamber是否緊急停止</param>
        public void EMSAlarm(bool BT_EMS, bool MT_EMS, bool OS_EMS, bool IC_EMS)
        {
            var plc = this.plcContext;
            plc.Write(MacHalPlcEnumVariable.PC_TO_BT_EMS, BT_EMS);
            plc.Write(MacHalPlcEnumVariable.PC_TO_MT_EMS, MT_EMS);
            plc.Write(MacHalPlcEnumVariable.PC_TO_OS_EMS, OS_EMS);
            plc.Write(MacHalPlcEnumVariable.PC_TO_IC_EMS, IC_EMS);
            Thread.Sleep(1000);
            if (plc.Read<bool>(MacHalPlcEnumVariable.BT_TO_PC_EMS_Reply) != BT_EMS)
                throw new MvException("PLC did not get 'Box Transfer EMS' alarm signal");
            else if (plc.Read<bool>(MacHalPlcEnumVariable.MT_TO_PC_EMS_Reply) != MT_EMS)
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
            var plc = this.plcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_PowerON);
        }

        /// <summary>
        /// 讀取設備內部，主控盤旁鑰匙鎖狀態，True：Maintenance 、 False：Auto
        /// </summary>
        /// <returns>True：Maintenance、False：Auto</returns>
        public bool ReadBCP_Maintenance()
        {
            var plc = this.plcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_Maintenance);
        }

        /// <summary>
        /// 讀取設備外部，抽屜旁鑰匙鎖狀態，True：Maintenance 、 False：Auto
        /// </summary>
        /// <returns>True：Maintenance、False：Auto</returns>
        public bool ReadCB_Maintenance()
        {
            var plc = this.plcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_CB_Maintenance);
        }

        /// <summary>
        /// 讀取主控盤EMO按鈕是否觸發，True：Push 、 False：Release
        /// </summary>
        /// <returns>True：Push、False：Release</returns>
        public Tuple<bool, bool, bool, bool, bool> ReadBCP_EMO()
        {
            var plc = this.plcContext;
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
            var plc = this.plcContext;
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
            var plc = this.plcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_LP1_EMO);
        }

        /// <summary>
        /// 讀取Load Port 2 EMO按鈕是否觸發，True：Push 、 False：Release
        /// </summary>
        /// <returns>True：Push、False：Release</returns>
        public bool ReadLP2_EMO()
        {
            var plc = this.plcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_LP2_EMO);
        }

        /// <summary>
        /// 讀取主控盤的門，True：Open 、 False：Close
        /// </summary>
        /// <returns>True：Open、False：Close</returns>
        public bool ReadBCP_Door()
        {
            var plc = this.plcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_Door);
        }

        /// <summary>
        /// 讀取Load Port 1的門，True：Open 、 False：Close
        /// </summary>
        /// <returns>True：Open、False：Close</returns>
        public bool ReadLP1_Door()
        {
            var plc = this.plcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_LP1_Door);
        }

        /// <summary>
        /// 讀取Load Port 2的門，True：Open 、 False：Close
        /// </summary>
        /// <returns>True：Open、False：Close</returns>
        public bool ReadLP2_Door()
        {
            var plc = this.plcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_LP2_Door);
        }

        /// <summary>
        /// 讀取主控箱內的偵煙器是否偵測到訊號，True：Alarm 、 False：Normal
        /// </summary>
        /// <returns>True：Alarm、False：Normal</returns>
        public bool ReadBCP_Smoke()
        {
            var plc = this.plcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_Smoke);
        }

        #endregion

        #region PLC alarm signal
        public string ReadAlarm_General()
        {
            string Result = "";
            var plc = this.plcContext;
            try
            {
                var AlarmList = plc.Read<bool[]>(MacHalPlcEnumVariable.General_Alarm);

                for (int i = 0; i < AlarmList.Length; i++)
                {
                    if (AlarmList[i])
                        if (i == 0)
                            Result += "General:PC Not Reset CheckClock, ";
                        else if (i == 1)
                            Result += "General:PLC Error, ";
                        else if (i == 2)
                            Result += "General:EIP Error, ";
                        else if (i == 3)
                            Result += "General:EtherCat Error, ";
                        else
                            Result += "General Unknown Alarm Signal, ";
                }

                //if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                //    Result = Result.Substring(0, Result.Length - 2);
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
            var plc = this.plcContext;
            try
            {
                var AlarmList = plc.Read<bool[]>(MacHalPlcEnumVariable.A01_Alarm);

                for (int i = 0; i < AlarmList.Length; i++)
                {
                    if (AlarmList[i])
                        if (i == 0)
                            Result += "Cabinet:DP1 TCP net error, ";
                        else if (i == 1)
                            Result += "Cabinet:DP2 TCP net error, ";
                        else if (i == 2)
                            Result += "Cabinet:DP1 Out range, ";
                        else if (i == 3)
                            Result += "Cabinet:DP2 Out range, ";
                        else if (i == 4)
                            Result += "Cabinet:DP1 Inexecutable mode error, ";
                        else if (i == 5)
                            Result += "Cabinet:DP1 Sensor error, ";
                        else if (i == 6)
                            Result += "Cabinet:DP2 Inexecutable mode error, ";
                        else if (i == 7)
                            Result += "Cabinet:DP2 Sensor error, ";
                        else if (i == 8)
                            Result += "Cabinet:DP1 未知異常, ";
                        else if (i == 9)
                            Result += "Cabinet:DP2 未知異常, ";
                        else
                            Result += "Cabinet Unknown Alarm Signal, ";
                }

                //if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                //    Result = Result.Substring(0, Result.Length - 2);
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
            var plc = this.plcContext;
            try
            {
                var AlarmList = plc.Read<bool[]>(MacHalPlcEnumVariable.A02_Alarm);

                for (int i = 0; i < AlarmList.Length; i++)
                {
                    if (AlarmList[i])
                        if (i == 0)
                            Result += "CC:PD skt error, ";
                        else if (i == 1)
                            Result += "CC:DP skt error, ";
                        else if (i == 2)
                            Result += "CC:DP out range, ";
                        else if (i == 3)
                            Result += "CC:PD-S out range, ";
                        else if (i == 4)
                            Result += "CC:PD-M out range, ";
                        else if (i == 5)
                            Result += "CC:PD-L out range, ";
                        else if (i == 6)
                            Result += "CC:About laser sensor open circuit, ";
                        else if (i == 7)
                            Result += "CC:UpDown laser sensor open circuit, ";
                        else
                            Result += "Clean Chamber Unknown Alarm Signal, ";
                }

                //if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                //    Result = Result.Substring(0, Result.Length - 2);
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
            var plc = this.plcContext;
            try
            {
                var AlarmList = plc.Read<bool[]>(MacHalPlcEnumVariable.A03_Alarm);

                for (int i = 0; i < AlarmList.Length; i++)
                {
                    if (AlarmList[i])
                        if (i == 0)
                            Result += "BT:servo error, ";
                        else if (i == 1)
                            Result += "BT:Hand point laser sensor open circuit, ";
                        else if (i == 2)
                            Result += "BT:Prevent collision laser sensor open circuit, ";
                        else if (i == 3)
                            Result += "BT:Level sensor X open circuit, ";
                        else if (i == 4)
                            Result += "BT:Level sensor Y open circuit, ";
                        else if (i == 5)
                            Result += "BT:Level limit out X, ";
                        else if (i == 6)
                            Result += "BT:Level limit out Y, ";
                        else if (i == 7)
                            Result += "BT:Hand Start Timeout, ";
                        else if (i == 8)
                            Result += "BT:Axis7 Vacuum not ready, ";
                        else if (i == 9)
                            Result += "BT:Hand Vacuum not ready, ";
                        else if (i == 10)
                            Result += "BT:Robot動作中不可執行指令, ";
                        else if (i == 11)
                            Result += "BT:Clamp/Unclamp Flow Timeout, ";
                        else if (i == 12)
                            Result += "BT:Initial Flow Timeout, ";
                        else
                            Result += "Box Transfer Unknown Alarm Signal, ";
                }

                //if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                //    Result = Result.Substring(0, Result.Length - 2);
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
            var plc = this.plcContext;
            try
            {
                var AlarmList = plc.Read<bool[]>(MacHalPlcEnumVariable.A04_Alarm);

                for (int i = 0; i < AlarmList.Length; i++)
                {
                    if (AlarmList[i])
                        if (i == 0)
                            Result += "MT:Up定位誤差過大, ";
                        else if (i == 1)
                            Result += "MT:Down定位誤差過大, ";
                        else if (i == 2)
                            Result += "MT:Left定位誤差過大, ";
                        else if (i == 3)
                            Result += "MT:Right定位誤差過大, ";
                        else if (i == 4)
                            Result += "MT:Up Move Error, ";
                        else if (i == 5)
                            Result += "MT:Down Move Error, ";
                        else if (i == 6)
                            Result += "MT:Left Move Error, ";
                        else if (i == 7)
                            Result += "MT:Right Move Error, ";
                        else if (i == 8)
                            Result += "MT:Tactile out range, ";
                        else if (i == 9)
                            Result += "MT:Up開始動作逾時, ";
                        else if (i == 10)
                            Result += "MT:Down開始動作逾時, ";
                        else if (i == 11)
                            Result += "MT:Left開始動作逾時, ";
                        else if (i == 12)
                            Result += "MT:Right開始動作逾時, ";
                        else if (i == 13)
                            Result += "MT:Robot動作中不可執行指令, ";
                        else if (i == 14)
                            Result += "MT:Clamp Flow Tiomeout, ";
                        else if (i == 15)
                            Result += "MT:Unclamp Flow Tiomeout, ";
                        else if (i == 16)
                            Result += "MT:Initial Flow Timeout, ";
                        else
                            Result += "Mask Transfer Unknown Alarm Signal, ";
                }

                //if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                //    Result = Result.Substring(0, Result.Length - 2);
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
            var plc = this.plcContext;
            try
            {
                var AlarmList = plc.Read<bool[]>(MacHalPlcEnumVariable.A05_Alarm);

                for (int i = 0; i < AlarmList.Length; i++)
                {
                    if (AlarmList[i])
                        if (i == 0)
                            Result += "OS:Clamp步進馬達Alarm, ";
                        else if (i == 1)
                            Result += "OS:Cover1 servo alarm, ";
                        else if (i == 2)
                            Result += "OS:Cover2 servo alarm, ";
                        else if (i == 3)
                            Result += "OS:整定電動缸1Alarm, ";
                        else if (i == 4)
                            Result += "OS:整定電動缸2Alarm, ";
                        else if (i == 5)
                            Result += "OS:滑台1Alarm, ";
                        else if (i == 6)
                            Result += "OS:滑台2Alarm, ";
                        else if (i == 7)
                            Result += "OS:Slider未同步做動, ";
                        else if (i == 8)
                            Result += "OS:Clamp扭力錯誤, ";
                        else if (i == 9)
                            Result += "OS:氣壓不足, ";
                        else if (i == 10)
                            Result += "OS:動作中破真空, ";
                        else if (i == 11)
                            Result += "OS:Open Sensor Timeout, ";
                        else if (i == 12)
                            Result += "OS:Close Sensor Timeout, ";
                        else if (i == 13)
                            Result += "OS:Cover Start Timeout, ";
                        else if (i == 14)
                            Result += "OS:Slider Start Timeout, ";
                        else if (i == 15)
                            Result += "OS:Sort Clamp Start Timeout, ";
                        else if (i == 16)
                            Result += "OS:Clamp Start Timeout, ";
                        else if (i == 17)
                            Result += "OS:Cover1 Deviation is too large, ";
                        else if (i == 18)
                            Result += "OS:Cover2 Deviation is too large, ";
                        else if (i == 19)
                            Result += "OS:Clamp Flow Timeout, ";
                        else if (i == 20)
                            Result += "OS:Conver Flow Timeout, ";
                        else if (i == 21)
                            Result += "OS:Slider Flow Timeout, ";
                        else if (i == 22)
                            Result += "OS:Conver Flow Timeout, ";
                        else if (i == 23)
                            Result += "OS:SortClamp Flow Timeout, ";
                        else
                            Result += "Open Stage Unknown Alarm Signal, ";
                }

                //if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                //    Result = Result.Substring(0, Result.Length - 2);
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
            var plc = this.plcContext;
            try
            {
                var AlarmList = plc.Read<bool[]>(MacHalPlcEnumVariable.A06_Alarm);


                for (int i = 0; i < AlarmList.Length; i++)
                {
                    if (AlarmList[i])
                        if (i == 1)
                            Result += "IC:X 驅動器Alarm, ";
                        else if (i == 2)
                            Result += "IC:Y 驅動器Alarm, ";
                        else if (i == 3)
                            Result += "IC:Z 驅動器Alarm, ";
                        else if (i == 4)
                            Result += "IC:W 驅動器Alarm, ";
                        else if (i == 5)
                            Result += "IC:動作中 Robot侵入, ";
                        else if (i == 6)
                            Result += "IC:W動作逾時, ";
                        else if (i == 7)
                            Result += "IC:Z動作逾時, ";
                        else if (i == 8)
                            Result += "IC:XY動作逾時, ";
                        else
                            Result += "Inspection Chamber Unknown Alarm Signal, ";
                }

                //if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                //    Result = Result.Substring(0, Result.Length - 2);
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
            var plc = this.plcContext;
            try
            {
                var AlarmList = plc.Read<bool[]>(MacHalPlcEnumVariable.A07_Alarm);

                for (int i = 0; i < AlarmList.Length; i++)
                {
                    if (AlarmList[i])
                        if (i == 0)
                            Result += "LP:DP1 TCP Error, ";
                        else if (i == 1)
                            Result += "LP:DP2 TCP Error, ";
                        else if (i == 2)
                            Result += "LP:DP1 Out Range, ";
                        else if (i == 3)
                            Result += "LP:DP2 Out Range, ";
                        else if (i == 4)
                            Result += "LP:DP1 Inexecutable mode error, ";
                        else if (i == 5)
                            Result += "LP:DP1 Sensor error, ";
                        else if (i == 6)
                            Result += "LP:DP2 Inexecutable mode error, ";
                        else if (i == 7)
                            Result += "LP:DP2 Sensor error, ";
                        else if (i == 8)
                            Result += "LP:DP1 未知異常, ";
                        else if (i == 9)
                            Result += "LP:DP2 未知異常, ";
                        else
                            Result += "Load Port Unknown Alarm Signal, ";
                }


                //if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                //    Result = Result.Substring(0, Result.Length - 2);
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
            var plc = this.plcContext;
            try
            {
                var AlarmList = plc.Read<bool[]>(MacHalPlcEnumVariable.A08_Alarm);

                for (int i = 0; i < AlarmList.Length; i++)
                {
                    if (AlarmList[i])
                        if (i == 0)
                            Result += "Outer Cover Fans:FFU1 Error, ";
                        else if (i == 1)
                            Result += "Outer Cover Fans:FFU2 Error, ";
                        else if (i == 2)
                            Result += "Outer Cover Fans:FFU3 Error, ";
                        else if (i == 3)
                            Result += "Outer Cover Fans:FFU4 Error, ";
                        else if (i == 4)
                            Result += "Outer Cover Fans:FFU5 Error, ";
                        else if (i == 5)
                            Result += "Outer Cover Fans:FFU6 Error, ";
                        else if (i == 6)
                            Result += "Outer Cover Fans:FFU7 Error, ";
                        else if (i == 7)
                            Result += "Outer Cover Fans:FFU8 Error, ";
                        else if (i == 8)
                            Result += "Outer Cover Fans:FFU9 Error, ";
                        else if (i == 9)
                            Result += "Outer Cover Fans:FFU10 Error, ";
                        else if (i == 10)
                            Result += "Outer Cover Fans:FFU11 Error, ";
                        else if (i == 11)
                            Result += "Outer Cover Fans:FFU12 Error, ";
                        else if (i == 12)
                            Result += "Outer Cover Fans:RS485 Error, ";
                        else
                            Result += "Outer Cover Fans Unknown Alarm Signal, ";
                }

                //if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                //    Result = Result.Substring(0, Result.Length - 2);
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
            var plc = this.plcContext;
            try
            {
                var AlarmList = plc.Read<bool[]>(MacHalPlcEnumVariable.A09_Alarm);

                for (int i = 0; i < AlarmList.Length; i++)
                {
                    if (AlarmList[i])
                        if (i == 0)
                            Result += "Clamp Inspect Deform:Laser1 Error (Signal disconnection ), ";
                        else if (i == 1)
                            Result += "Clamp Inspect Deform:Laser2 Error (Signal disconnection ), ";
                        else if (i == 2)
                            Result += "Clamp Inspect Deform:Laser3 Error (Signal disconnection ), ";
                        else if (i == 3)
                            Result += "Clamp Inspect Deform:Laser4 Error (Signal disconnection ), ";
                        else if (i == 4)
                            Result += "Clamp Inspect Deform:Laser5 Error (Signal disconnection ), ";
                        else if (i == 5)
                            Result += "Clamp Inspect Deform:Laser6 Error (Signal disconnection ), ";
                        else
                            Result += "Clamp Inspect Deform Unknown Alarm Signal, ";
                }

                //if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                //    Result = Result.Substring(0, Result.Length - 2);
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
            var plc = this.plcContext;
            try
            {
                var WarningList = plc.Read<bool[]>(MacHalPlcEnumVariable.General_Warning);

                for (int i = 0; i < WarningList.Length; i++)
                {
                    if (WarningList[i])
                        Result += "General Unknown Warning Signal, ";
                }

                //if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                //    Result = Result.Substring(0, Result.Length - 2);
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
            var plc = this.plcContext;
            try
            {
                var WarningList = plc.Read<bool[]>(MacHalPlcEnumVariable.A01_Warning);

                for (int i = 0; i < WarningList.Length; i++)
                {
                    if (WarningList[i])
                        Result += "Cabinet Unknown Warning Signal, ";
                }

                //if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                //    Result = Result.Substring(0, Result.Length - 2);
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
            var plc = this.plcContext;
            try
            {
                var WarningList = plc.Read<bool[]>(MacHalPlcEnumVariable.A02_Warning);

                for (int i = 0; i < WarningList.Length; i++)
                {
                    if (WarningList[i])
                        if (i == 0)
                            Result += "CC:Clean T3 Timeout, ";
                        else
                            Result += "Clean Chamber Unknown Warning Signal, ";
                }

                //if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                //    Result = Result.Substring(0, Result.Length - 2);
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
            var plc = this.plcContext;
            try
            {
                var WarningList = plc.Read<bool[]>(MacHalPlcEnumVariable.A03_Warning);

                for (int i = 0; i < WarningList.Length; i++)
                {
                    if (WarningList[i])
                        if (i == 0)
                            Result += "BT:Under maintenance, ";
                        else if (i == 1)
                            Result += "BT:Clamp T3 Timeout, ";
                        else if (i == 2)
                            Result += "BT:Unclamp T3 Timeout, ";
                        else if (i == 3)
                            Result += "BT:動作中Command訊號消失, ";
                        else if (i == 4)
                            Result += "BT:No Power ON, ";
                        else if (i == 5)
                            Result += "BT:Please Initial, ";
                        else if (i == 6)
                            Result += "BT:RS422 Error, ";
                        else if (i == 7)
                            Result += "BT:Setting speed out range, ";
                        else if (i == 8)
                            Result += "BT:Clamp T1 Timeout, ";
                        else if (i == 9)
                            Result += "BT:Unclamp T1 Timeout, ";
                        else if (i == 10)
                            Result += "BT:Force positive limit out Fx, ";
                        else if (i == 11)
                            Result += "BT:Force positive limit out Fy, ";
                        else if (i == 12)
                            Result += "BT:Force positive limit out Fz, ";
                        else if (i == 13)
                            Result += "BT:Force positive limit out Mx, ";
                        else if (i == 14)
                            Result += "BT:Force positive limit out My, ";
                        else if (i == 15)
                            Result += "BT:Force positive limit out Mz, ";
                        else if (i == 16)
                            Result += "BT:Force negative limit out Fx, ";
                        else if (i == 17)
                            Result += "BT:Force negative limit out Fy, ";
                        else if (i == 18)
                            Result += "BT:Force negative limit out Fz, ";
                        else if (i == 19)
                            Result += "BT:Force negative limit out Mx, ";
                        else if (i == 20)
                            Result += "BT:Force negative limit out My, ";
                        else if (i == 21)
                            Result += "BT:Force negative limit out Mz, ";
                        else
                            Result += "Box Transfer Unknown Warning Signal, ";
                }

                //if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                //    Result = Result.Substring(0, Result.Length - 2);
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
            var plc = this.plcContext;
            try
            {
                var WarningList = plc.Read<bool[]>(MacHalPlcEnumVariable.A04_Warning);

                for (int i = 0; i < WarningList.Length; i++)
                {
                    if (WarningList[i])
                        if (i == 0)
                            Result += "MT:RS422 Error, ";
                        else if (i == 1)
                            Result += "MT:RS232 Error, ";
                        else if (i == 2)
                            Result += "MT:Initial T1 Timeout, ";
                        else if (i == 3)
                            Result += "MT:Clamp T1 Timeout, ";
                        else if (i == 4)
                            Result += "MT:Unclamp T1 Timeout, ";
                        else if (i == 5)
                            Result += "MT:Initial T3 Timeout, ";
                        else if (i == 6)
                            Result += "MT:Clamp T3 Timeout, ";
                        else if (i == 7)
                            Result += "MT:Unclamp T3 Timeout, ";
                        else if (i == 8)
                            Result += "MT:Force positive limit out Fx, ";
                        else if (i == 9)
                            Result += "MT:Force positive limit out Fy, ";
                        else if (i == 10)
                            Result += "MT:Force positive limit out Fz, ";
                        else if (i == 11)
                            Result += "MT:Force positive limit out Mx, ";
                        else if (i == 12)
                            Result += "MT:Force positive limit out My, ";
                        else if (i == 13)
                            Result += "MT:Force positive limit out Mz, ";
                        else if (i == 14)
                            Result += "MT:Force negative limit out Fx, ";
                        else if (i == 15)
                            Result += "MT:Force negative limit out Fy, ";
                        else if (i == 16)
                            Result += "MT:Force negative limit out Fz, ";
                        else if (i == 17)
                            Result += "MT:Force negative limit out Mx, ";
                        else if (i == 18)
                            Result += "MT:Force negative limit out My, ";
                        else if (i == 19)
                            Result += "MT:Force negative limit out Mz, ";
                        else
                            Result += "Mask Transfer Unknown Warning Signal, ";
                }

                //if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                //    Result = Result.Substring(0, Result.Length - 2);
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
            var plc = this.plcContext;
            try
            {
                var WarningList = plc.Read<bool[]>(MacHalPlcEnumVariable.A05_Warning);

                for (int i = 0; i < WarningList.Length; i++)
                {
                    if (WarningList[i])
                        if (i == 0)
                            Result += "OS:Robot 侵入中不可執行Command, ";
                        else if (i == 1)
                            Result += "OS:Please initial, ";
                        else if (i == 2)
                            Result += "OS:SortClamp T1 Timeout, ";
                        else if (i == 3)
                            Result += "OS:SortUnclamp T1 Timeout, ";
                        else if (i == 4)
                            Result += "OS:Vacuum T1 Timeout, ";
                        else if (i == 5)
                            Result += "OS:Clamp T1 Timeout, ";
                        else if (i == 6)
                            Result += "OS:Unclamp T1 Timeout, ";
                        else if (i == 7)
                            Result += "OS:Open T1 Timeout, ";
                        else if (i == 8)
                            Result += "OS:Close T1 Timeout, ";
                        else if (i == 9)
                            Result += "OS:Lock T1 Timeout, ";
                        else if (i == 10)
                            Result += "OS:Initial T1 Timeout, ";
                        else if (i == 11)
                            Result += "OS:SortClamp T3 Timeout, ";
                        else if (i == 12)
                            Result += "OS:SortUnclamp T3 Timeout, ";
                        else if (i == 13)
                            Result += "OS:Vacuum T3 Timeout, ";
                        else if (i == 14)
                            Result += "OS:Clamp T3 Timeout, ";
                        else if (i == 15)
                            Result += "OS:Unclamp T3 Timeout, ";
                        else if (i == 16)
                            Result += "OS:Open T3 Timeout, ";
                        else if (i == 17)
                            Result += "OS:Close T3 Timeout, ";
                        else if (i == 18)
                            Result += "OS:Lock T3 Timeout, ";
                        else if (i == 19)
                            Result += "OS:Initial T3 Timeout, ";
                        else
                            Result += "Open Stage Unknown Warning Signal, ";
                }

                //if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                //    Result = Result.Substring(0, Result.Length - 2);

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
            var plc = this.plcContext;
            try
            {
                var WarningList = plc.Read<bool[]>(MacHalPlcEnumVariable.A06_Warning);

                for (int i = 0; i < WarningList.Length; i++)
                {
                    if (WarningList[i])
                        if (i == 0)
                            Result += "IC:X軸 驅動器Warning, ";
                        else if (i == 1)
                            Result += "IC:Y軸 驅動器Warning, ";
                        else if (i == 2)
                            Result += "IC:Z軸 驅動器Warning, ";
                        else if (i == 3)
                            Result += "IC:W軸 驅動器Warning, ";
                        else if (i == 4)
                            Result += "IC:動作中Jog Command下達, ";
                        else if (i == 5)
                            Result += "IC:動作中下達W Command, ";
                        else if (i == 6)
                            Result += "IC:動作中下達Z Command, ";
                        else if (i == 7)
                            Result += "IC:動作中下達XY Command, ";
                        else if (i == 8)
                            Result += "IC:Robot侵入中不可做動, ";
                        else if (i == 9)
                            Result += "IC:Initial T1 Timeout, ";
                        else if (i == 10)
                            Result += "IC:Z T1 Timeout, ";
                        else if (i == 11)
                            Result += "IC:XY T1 Timeout, ";
                        else if (i == 12)
                            Result += "IC:W T1 Timeout, ";
                        else if (i == 13)
                            Result += "IC:Initial T3 Timeout, ";
                        else if (i == 14)
                            Result += "IC:Z T3 Timeout, ";
                        else if (i == 15)
                            Result += "IC:XY T3 Timeout, ";
                        else if (i == 16)
                            Result += "IC:W T3 Timeout, ";
                        else
                            Result += "Inspection Chamber Unknown Warning Signal, ";
                }

                //if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                //    Result = Result.Substring(0, Result.Length - 2);
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
            var plc = this.plcContext;
            try
            {
                var WarningList = plc.Read<bool[]>(MacHalPlcEnumVariable.A07_Warning);

                for (int i = 0; i < WarningList.Length; i++)
                {
                    if (WarningList[i])
                        Result += "Load Port Unknown Warning Signal, ";
                }

                //if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                //    Result = Result.Substring(0, Result.Length - 2);
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
            var plc = this.plcContext;
            try
            {
                var WarningList = plc.Read<bool[]>(MacHalPlcEnumVariable.A08_Warning);

                for (int i = 0; i < WarningList.Length; i++)
                {
                    if (WarningList[i])
                        Result += "Outer Cover Fans Unknown Warning Signal, ";
                }

                //if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                //    Result = Result.Substring(0, Result.Length - 2);
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
            var plc = this.plcContext;
            try
            {
                var WarningList = plc.Read<bool[]>(MacHalPlcEnumVariable.A09_Warning);

                for (int i = 0; i < WarningList.Length; i++)
                {
                    if (WarningList[i])
                        Result += "Deform Inspection Unknown Warning Signal, ";
                }

                //if (Result.Length > 0 && Result.Substring(Result.Length - 2, 2) == ", ")
                //    Result = Result.Substring(0, Result.Length - 2);
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
