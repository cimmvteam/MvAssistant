using MvAssistant.Mac.v1_0.Hal.CompPlc;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [Guid("FAFCEF2B-6356-4438-890F-30F865CAA742")]
    public class MacHalUniversal : MacHalAssemblyBase, IMacHalUniversal
    {


        #region Device Components


        public IMacHalPlcUniversal plc_01 { get { return (IMacHalPlcUniversal)this.GetHalDevice(MacEnumDevice.universal_plc_01); } }

        #endregion Device Components






        /// <summary>
        /// 設備訊號燈設定
        /// </summary>
        /// <param name="Red"></param>
        /// <param name="Orange"></param>
        /// <param name="Blue"></param>
        public void SetSignalTower(bool Red, bool Orange, bool Blue)
        { plc_01.SetSignalTower(Red, Orange, Blue); }

        /// <summary>
        /// 蜂鳴器，0:停止鳴叫、1~4分別是不同的鳴叫方式
        /// </summary>
        /// <param name="BuzzerType"></param>
        public void SetBuzzer(uint BuzzerType)
        { plc_01.SetBuzzer(BuzzerType); }

        /// <summary>
        /// A08外罩風扇調整風速，風扇編號、風速控制
        /// </summary>
        /// <param name="FanID"></param>
        /// <param name="WindSpeed"></param>
        /// <returns></returns>
        public string CoverFanCtrl(uint FanID, uint WindSpeed)
        { return plc_01.CoverFanCtrl(FanID, WindSpeed); }

        /// <summary>
        /// 讀取外罩風扇風速
        /// </summary>
        /// <returns></returns>
        public List<int> ReadCoverFanSpeed()
        { return plc_01.ReadCoverFanSpeed(); }

        /// <summary>
        /// 重置所有PLC Alarm訊息
        /// </summary>
        public void ResetAllAlarm()
        { plc_01.ResetAllAlarm(); }

        /// <summary>
        /// 當Assembly出錯時，針對部件下緊急停止訊號，Box Transfer、Mask Transfer、Open Stage、Inspection Chamber
        /// </summary>
        /// <param name="BT_EMS">Box Transfer是否緊急停止</param>
        /// <param name="MT_EMS">Mask Transfer是否緊急停止</param>
        /// <param name="OS_EMS">Open Stage是否緊急停止</param>
        /// <param name="IC_EMS">Inspection Chamber是否緊急停止</param>
        public void EMSAlarm(bool BT_EMS, bool MT_EMS, bool OS_EMS, bool IC_EMS)
        { plc_01.EMSAlarm(BT_EMS, MT_EMS, OS_EMS, IC_EMS); }

        #region PLC狀態訊號
        /// <summary>
        /// 讀取電源狀態，True：Power ON 、 False：Power OFF
        /// </summary>
        /// <returns>True：Power ON、False：Power OFF</returns>
        public bool ReadPowerON()
        { return plc_01.ReadPowerON(); }

        /// <summary>
        /// 讀取設備內部，主控盤旁鑰匙鎖狀態，True：Maintenance 、 False：Auto
        /// </summary>
        /// <returns>True：Maintenance、False：Auto</returns>
        public bool ReadBCP_Maintenance()
        { return plc_01.ReadBCP_Maintenance(); }

        /// <summary>
        /// 讀取設備外部，抽屜旁鑰匙鎖狀態，True：Maintenance 、 False：Auto
        /// </summary>
        /// <returns>True：Maintenance、False：Auto</returns>
        public bool ReadCB_Maintenance()
        { return plc_01.ReadCB_Maintenance(); }

        /// <summary>
        /// 讀取主控盤EMO按鈕是否觸發，True：Push 、 False：Release
        /// </summary>
        /// <returns>True：Push、False：Release</returns>
        public Tuple<bool, bool, bool, bool, bool> ReadBCP_EMO()
        { return plc_01.ReadBCP_EMO(); }

        /// <summary>
        /// 讀取抽屜EMO按鈕是否觸發，True：Push 、 False：Release
        /// </summary>
        /// <returns>True：Push、False：Release</returns>
        public Tuple<bool, bool, bool> ReadCB_EMO()
        { return plc_01.ReadCB_EMO(); }

        /// <summary>
        /// 讀取Load Port 1 EMO按鈕是否觸發，True：Push 、 False：Release
        /// </summary>
        /// <returns>True：Push、False：Release</returns>
        public bool ReadLP1_EMO()
        { return plc_01.ReadLP1_EMO(); }

        /// <summary>
        /// 讀取Load Port 2 EMO按鈕是否觸發，True：Push 、 False：Release
        /// </summary>
        /// <returns>True：Push、False：Release</returns>
        public bool ReadLP2_EMO()
        { return plc_01.ReadLP2_EMO(); }

        /// <summary>
        /// 讀取主控盤的門，True：Open 、 False：Close
        /// </summary>
        /// <returns>True：Open、False：Close</returns>
        public bool ReadBCP_Door()
        { return plc_01.ReadBCP_Door(); }

        /// <summary>
        /// 讀取Load Port 1的門，True：Open 、 False：Close
        /// </summary>
        /// <returns>True：Open、False：Close</returns>
        public bool ReadLP1_Door()
        { return plc_01.ReadLP1_Door(); }

        /// <summary>
        /// 讀取Load Port 2的門，True：Open 、 False：Close
        /// </summary>
        /// <returns>True：Open、False：Close</returns>
        public bool ReadLP2_Door()
        { return plc_01.ReadLP2_Door(); }

        /// <summary>
        /// 讀取主控箱內的偵煙器是否偵測到訊號，True：Alarm 、 False：Normal
        /// </summary>
        /// <returns>True：Alarm、False：Normal</returns>
        public bool ReadBCP_Smoke()
        { return plc_01.ReadBCP_Smoke(); }
        #endregion

        #region PLC alarm signal
        public string ReadAlarm_General()
        { return plc_01.ReadAlarm_General(); }

        public string ReadAlarm_Cabinet()
        { return plc_01.ReadAlarm_Cabinet(); }

        public string ReadAlarm_CleanCh()
        { return plc_01.ReadAlarm_CleanCh(); }

        public string ReadAlarm_BTRobot()
        { return plc_01.ReadAlarm_BTRobot(); }

        public string ReadAlarm_MTRobot()
        { return plc_01.ReadAlarm_MTRobot(); }

        public string ReadAlarm_OpenStage()
        { return plc_01.ReadAlarm_OpenStage(); }

        public string ReadAlarm_InspCh()
        { return plc_01.ReadAlarm_InspCh(); }

        public string ReadAlarm_LoadPort()
        { return plc_01.ReadAlarm_LoadPort(); }

        public string ReadAlarm_CoverFan()
        { return plc_01.ReadAlarm_CoverFan(); }

        public string ReadAlarm_MTClampInsp()
        { return plc_01.ReadAlarm_MTClampInsp(); }

        public string ReadAllAlarmMessage()
        {
            string Result = "";
            Result += plc_01.ReadAlarm_General();
            Result += plc_01.ReadAlarm_Cabinet();
            Result += plc_01.ReadAlarm_CleanCh();
            Result += plc_01.ReadAlarm_BTRobot();
            Result += plc_01.ReadAlarm_MTRobot();
            Result += plc_01.ReadAlarm_OpenStage();
            Result += plc_01.ReadAlarm_InspCh();
            Result += plc_01.ReadAlarm_LoadPort();
            Result += plc_01.ReadAlarm_CoverFan();
            Result += plc_01.ReadAlarm_MTClampInsp();
            return Result;
        }
        #endregion

        #region PLC warning signal
        public string ReadWarning_General()
        { return plc_01.ReadWarning_General(); }

        public string ReadWarning_Cabinet()
        { return plc_01.ReadWarning_Cabinet(); }

        public string ReadWarning_CleanCh()
        { return plc_01.ReadWarning_CleanCh(); }

        public string ReadWarning_BTRobot()
        { return plc_01.ReadWarning_BTRobot(); }

        public string ReadWarning_MTRobot()
        { return plc_01.ReadWarning_MTRobot(); }

        public string ReadWarning_OpenStage()
        { return plc_01.ReadWarning_OpenStage(); }

        public string ReadWarning_InspCh()
        { return plc_01.ReadWarning_InspCh(); }

        public string ReadWarning_LoadPort()
        { return plc_01.ReadWarning_LoadPort(); }

        public string ReadWarning_CoverFan()
        { return plc_01.ReadWarning_CoverFan(); }

        public string ReadWarning_MTClampInsp()
        { return plc_01.ReadWarning_MTClampInsp(); }

        public string ReadAllWarningMessage()
        {
            string Result = "";
            Result += plc_01.ReadWarning_General();
            Result += plc_01.ReadWarning_Cabinet();
            Result += plc_01.ReadWarning_CleanCh();
            Result += plc_01.ReadWarning_BTRobot();
            Result += plc_01.ReadWarning_MTRobot();
            Result += plc_01.ReadWarning_OpenStage();
            Result += plc_01.ReadWarning_InspCh();
            Result += plc_01.ReadWarning_LoadPort();
            Result += plc_01.ReadWarning_CoverFan();
            Result += plc_01.ReadWarning_MTClampInsp();
            return Result;
        }
        #endregion
    }
}
