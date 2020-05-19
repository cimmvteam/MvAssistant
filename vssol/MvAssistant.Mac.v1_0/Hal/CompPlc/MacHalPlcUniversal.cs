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

        public void ResetAll()
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
        public bool ReadPowerON()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_PowerON);
        }

        public bool ReadBCP_Maintenance()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_Maintenance);
        }

        public bool ReadCB_Maintenance()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_CB_Maintenance);
        }

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

        public Tuple<bool, bool, bool> ReadCB_EMO()
        {
            var plc = this.m_PlcContext;
            return new Tuple<bool, bool, bool>(
                plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_CB_EMO1),
                plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_CB_EMO2),
                plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_CB_EMO3)
                );
        }

        public bool ReadLP1_EMO()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_LP1_EMO);
        }

        public bool ReadLP2_EMO()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_LP2_EMO);
        }

        public bool ReadBCP_Door()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_Door);
        }

        public bool ReadLP1_Door()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_LP1_Door);
        }

        public bool ReadLP2_Door()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_LP2_Door);
        }

        public bool ReadBCP_Smoke()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_Smoke);
        }

        #endregion

        #region PLC alarm signal
        public string Alarm_General()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                switch (plc.Read<int>(MacHalPlcEnumVariable.General_Alarm))
                {
                    case 0:
                        Result = "PC Not Reset CheckClock";
                        break;
                    case 1:
                        Result = "PLC Error";
                        break;
                    case 2:
                        Result = "EIP Error";
                        break;
                    case 3:
                        Result = "EtherCat Error";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string Alarm_Cabinet()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                switch (plc.Read<int>(MacHalPlcEnumVariable.A01_Alarm))
                {
                    case 0:
                        Result = "DP1 TCP net error";
                        break;
                    case 1:
                        Result = "DP2 TCP net error";
                        break;
                    case 2:
                        Result = "DP1 Out range";
                        break;
                    case 3:
                        Result = "DP2 Out range";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string Alarm_CleanCh()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                switch (plc.Read<int>(MacHalPlcEnumVariable.A02_Alarm))
                {
                    case 0:
                        Result = "PD skt error";
                        break;
                    case 1:
                        Result = "DP skt error";
                        break;
                    case 2:
                        Result = "DP out range";
                        break;
                    case 3:
                        Result = "PD-S out range";
                        break;
                    case 4:
                        Result = "PD-M out range";
                        break;
                    case 5:
                        Result = "PD-L out range";
                        break;
                    case 6:
                        Result = "About laser sensor open circuit";
                        break;
                    case 7:
                        Result = "UpDown laser sensor open circuit";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string Alarm_BTRobot()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                switch (plc.Read<int>(MacHalPlcEnumVariable.A03_Alarm))
                {
                    case 0:
                        Result = "BT servo error";
                        break;
                    case 1:
                        Result = "Hand point laser sensor open circuit";
                        break;
                    case 2:
                        Result = "Prevent collision laser sensor open circuit";
                        break;
                    case 3:
                        Result = "Level sensor X open circuit";
                        break;
                    case 4:
                        Result = "Level sensor Y open circuit";
                        break;
                    case 5:
                        Result = "Clamp T1 Timeout";
                        break;
                    case 6:
                        Result = "Unclamp T1 Timeout";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string Alarm_MTRobot()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                switch (plc.Read<int>(MacHalPlcEnumVariable.A04_Alarm))
                {
                    case 0:
                        Result = "Up定位誤差過大";
                        break;
                    case 1:
                        Result = "Down定位誤差過大";
                        break;
                    case 2:
                        Result = "Left定位誤差過大";
                        break;
                    case 3:
                        Result = "Right定位誤差過大";
                        break;
                    case 4:
                        Result = "Up Move Error";
                        break;
                    case 5:
                        Result = "Down Move Error";
                        break;
                    case 6:
                        Result = "Left Move Error";
                        break;
                    case 7:
                        Result = "Right Move Error";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string Alarm_OpenStage()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                switch (plc.Read<int>(MacHalPlcEnumVariable.A05_Alarm))
                {
                    case 0:
                        Result = "Clamp步進馬達Alarm";
                        break;
                    case 1:
                        Result = "Cover1 servo alarm";
                        break;
                    case 2:
                        Result = "Cover2 servo alarm";
                        break;
                    case 3:
                        Result = "整定電動缸1Alarm";
                        break;
                    case 4:
                        Result = "整定電動缸2Alarm";
                        break;
                    case 5:
                        Result = "滑台1Alarm";
                        break;
                    case 6:
                        Result = "滑台2Alarm";
                        break;
                    case 7:
                        Result = "Slider未同步做動";
                        break;
                    case 8:
                        Result = "Clamp扭力錯誤";
                        break;
                    case 9:
                        Result = "氣壓不足";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string Alarm_InspCh()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                switch (plc.Read<int>(MacHalPlcEnumVariable.A06_Alarm))
                {
                    case 1:
                        Result = "X 驅動器Alarm";
                        break;
                    case 2:
                        Result = "Y 驅動器Alarm";
                        break;
                    case 3:
                        Result = "Z 驅動器Alarm";
                        break;
                    case 4:
                        Result = "W 驅動器Alarm";
                        break;
                    case 5:
                        Result = "動作中 Robot侵入";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string Alarm_LoadPort()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                switch (plc.Read<int>(MacHalPlcEnumVariable.A07_Alarm))
                {
                    case 0:
                        Result = "DP1 TCP Error";
                        break;
                    case 1:
                        Result = "DP2 TCP Error";
                        break;
                    case 2:
                        Result = "DP1 Out Range";
                        break;
                    case 3:
                        Result = "DP2 Out Range";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string Alarm_CoverFan()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                switch (plc.Read<int>(MacHalPlcEnumVariable.A08_Alarm))
                {
                    case 0:
                        Result = "FFU1 Error";
                        break;
                    case 1:
                        Result = "FFU2 Error";
                        break;
                    case 2:
                        Result = "FFU3 Error";
                        break;
                    case 3:
                        Result = "FFU4 Error";
                        break;
                    case 4:
                        Result = "FFU5 Error";
                        break;
                    case 5:
                        Result = "FFU6 Error";
                        break;
                    case 6:
                        Result = "FFU7 Error";
                        break;
                    case 7:
                        Result = "FFU8 Error";
                        break;
                    case 8:
                        Result = "FFU9 Error";
                        break;
                    case 9:
                        Result = "FFU10 Error";
                        break;
                    case 10:
                        Result = "FFU11 Error";
                        break;
                    case 11:
                        Result = "FFU12 Error";
                        break;
                    case 12:
                        Result = "RS485 Error";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string Alarm_MTClampInsp()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                switch (plc.Read<int>(MacHalPlcEnumVariable.A09_Alarm))
                {
                    case 0:
                        Result = "Laser1 Error (Signal disconnection )";
                        break;
                    case 1:
                        Result = "Laser2 Error (Signal disconnection )";
                        break;
                    case 2:
                        Result = "Laser3 Error (Signal disconnection )";
                        break;
                    case 3:
                        Result = "Laser4 Error (Signal disconnection )";
                        break;
                    case 4:
                        Result = "Laser5 Error (Signal disconnection )";
                        break;
                    case 5:
                        Result = "Laser6 Error (Signal disconnection )";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        #endregion

        #region PLC warning signal
        public string Warning_General()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                switch (plc.Read<int>(MacHalPlcEnumVariable.General_Warning))
                {
                    case 0:
                        Result = "";
                        break;
                    case 1:
                        Result = "";
                        break;
                    case 2:
                        Result = "";
                        break;
                    case 3:
                        Result = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string Warning_Cabinet()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                switch (plc.Read<int>(MacHalPlcEnumVariable.A01_Warning))
                {
                    case 0:
                        Result = "";
                        break;
                    case 1:
                        Result = "";
                        break;
                    case 2:
                        Result = "";
                        break;
                    case 3:
                        Result = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string Warning_CleanCh()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                switch (plc.Read<int>(MacHalPlcEnumVariable.A02_Warning))
                {
                    case 0:
                        Result = "";
                        break;
                    case 1:
                        Result = "";
                        break;
                    case 2:
                        Result = "";
                        break;
                    case 3:
                        Result = "";
                        break;
                    case 4:
                        Result = "";
                        break;
                    case 5:
                        Result = "";
                        break;
                    case 6:
                        Result = "";
                        break;
                    case 7:
                        Result = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string Warning_BTRobot()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                switch (plc.Read<int>(MacHalPlcEnumVariable.A03_Warning))
                {
                    case 0:
                        Result = "Under maintenance";
                        break;
                    case 1:
                        Result = "Clamp T3 Timeout";
                        break;
                    case 2:
                        Result = "Unclamp T3 Timeout";
                        break;
                    case 3:
                        Result = "動作中Command訊號消失";
                        break;
                    case 4:
                        Result = "No Power ON";
                        break;
                    case 5:
                        Result = "Please Initial";
                        break;
                    case 6:
                        Result = "RS422 Error";
                        break;
                    case 7:
                        Result = "Setting speed out range";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string Warning_MTRobot()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                switch (plc.Read<int>(MacHalPlcEnumVariable.A04_Warning))
                {
                    case 0:
                        Result = "RS422 Error";
                        break;
                    case 1:
                        Result = "RS232 Error";
                        break;
                    case 2:
                        Result = "";
                        break;
                    case 3:
                        Result = "";
                        break;
                    case 4:
                        Result = "";
                        break;
                    case 5:
                        Result = "";
                        break;
                    case 6:
                        Result = "";
                        break;
                    case 7:
                        Result = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string Warning_OpenStage()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                switch (plc.Read<int>(MacHalPlcEnumVariable.A05_Warning))
                {
                    case 0:
                        Result = "Robot 侵入中不可執行Command";
                        break;
                    case 1:
                        Result = "Please initial";
                        break;
                    case 2:
                        Result = "";
                        break;
                    case 3:
                        Result = "";
                        break;
                    case 4:
                        Result = "";
                        break;
                    case 5:
                        Result = "";
                        break;
                    case 6:
                        Result = "";
                        break;
                    case 7:
                        Result = "";
                        break;
                    case 8:
                        Result = "";
                        break;
                    case 9:
                        Result = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string Warning_InspCh()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                switch (plc.Read<int>(MacHalPlcEnumVariable.A06_Warning))
                {
                    case 0:
                        Result = "X軸 驅動器Warning";
                        break;
                    case 1:
                        Result = "Y軸 驅動器Warning";
                        break;
                    case 2:
                        Result = "Z軸 驅動器Warning";
                        break;
                    case 3:
                        Result = "W軸 驅動器Warning";
                        break;
                    case 4:
                        Result = "動作中Jog Command下達";
                        break;
                    case 5:
                        Result = "動作中下達W Command";
                        break;
                    case 6:
                        Result = "動作中下達Z Command";
                        break;
                    case 7:
                        Result = "動作中下達XY Command";
                        break;
                    case 8:
                        Result = "Robot侵入中不可做動";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string Warning_LoadPort()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                switch (plc.Read<int>(MacHalPlcEnumVariable.A07_Warning))
                {
                    case 0:
                        Result = "";
                        break;
                    case 1:
                        Result = "";
                        break;
                    case 2:
                        Result = "";
                        break;
                    case 3:
                        Result = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string Warning_CoverFan()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                switch (plc.Read<int>(MacHalPlcEnumVariable.A08_Warning))
                {
                    case 0:
                        Result = "";
                        break;
                    case 1:
                        Result = "";
                        break;
                    case 2:
                        Result = "";
                        break;
                    case 3:
                        Result = "";
                        break;
                    case 4:
                        Result = "";
                        break;
                    case 5:
                        Result = "";
                        break;
                    case 6:
                        Result = "";
                        break;
                    case 7:
                        Result = "";
                        break;
                    case 8:
                        Result = "";
                        break;
                    case 9:
                        Result = "";
                        break;
                    case 10:
                        Result = "";
                        break;
                    case 11:
                        Result = "";
                        break;
                    case 12:
                        Result = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string Warning_MTClampInsp()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                switch (plc.Read<int>(MacHalPlcEnumVariable.A09_Warning))
                {
                    case 0:
                        Result = "";
                        break;
                    case 1:
                        Result = "";
                        break;
                    case 2:
                        Result = "";
                        break;
                    case 3:
                        Result = "";
                        break;
                    case 4:
                        Result = "";
                        break;
                    case 5:
                        Result = "";
                        break;
                }
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
