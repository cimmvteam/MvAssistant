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

        public bool ReadLP_Light_Curtain()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_LP_Light_Curtain);
        }
        #endregion
    }
}
