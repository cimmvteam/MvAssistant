using MvAssistant.v0_3.Mac.Hal.CompPlc;
using MvAssistant.v0_3.Mac.Manifest;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MvAssistant.v0_3.Mac.Hal.Assembly
{
    [Guid("FAFCEF2B-6356-4438-890F-30F865CAA742")]
    public class MacHalEqp : MacHalAssemblyBase, IMacHalEqp
    {


        #region Device Components


        public IMacHalPlcEqp Plc { get { return (IMacHalPlcEqp)this.GetHalDevice(EnumMacDeviceId.eqp_plc_01); } }

        #endregion Device Components






        /// <summary>
        /// 設備訊號燈設定
        /// </summary>
        /// <param name="Red"></param>
        /// <param name="Orange"></param>
        /// <param name="Blue"></param>
        public void SetSignalTower(bool Red, bool Orange, bool Blue)
        { Plc.SetSignalTower(Red, Orange, Blue); }

        /// <summary>
        /// 蜂鳴器，0:停止鳴叫、1~4分別是不同的鳴叫方式
        /// </summary>
        /// <param name="BuzzerType"></param>
        public void SetBuzzer(uint BuzzerType)
        { Plc.SetBuzzer(BuzzerType); }

        /// <summary>
        /// A08外罩風扇調整風速，風扇編號、風速控制
        /// </summary>
        /// <param name="FanID"></param>
        /// <param name="WindSpeed"></param>
        /// <returns></returns>
        public string CoverFanCtrl(uint FanID, uint WindSpeed)
        { return Plc.CoverFanCtrl(FanID, WindSpeed); }

        /// <summary>
        /// 讀取外罩風扇風速
        /// </summary>
        /// <returns></returns>
        public List<int> ReadCoverFanSpeed()
        { return Plc.ReadCoverFanSpeed(); }

        /// <summary>
        /// 重置所有PLC Alarm訊息
        /// </summary>
        public void ResetAllAlarm()
        { Plc.ResetAllAlarm(); }

        /// <summary>
        /// 當Assembly出錯時，針對部件下緊急停止訊號，Box Transfer、Mask Transfer、Open Stage、Inspection Chamber
        /// </summary>
        /// <param name="BT_EMS">Box Transfer是否緊急停止</param>
        /// <param name="MT_EMS">Mask Transfer是否緊急停止</param>
        /// <param name="OS_EMS">Open Stage是否緊急停止</param>
        /// <param name="IC_EMS">Inspection Chamber是否緊急停止</param>
        public void EMSAlarm(bool BT_EMS, bool MT_EMS, bool OS_EMS, bool IC_EMS)
        { Plc.EMSAlarm(BT_EMS, MT_EMS, OS_EMS, IC_EMS); }

        #region PLC狀態訊號
        /// <summary>
        /// 讀取電源狀態，True：Power ON 、 False：Power OFF
        /// </summary>
        /// <returns>True：Power ON、False：Power OFF</returns>
        public bool ReadPowerON()
        { return Plc.ReadPowerON(); }

        /// <summary>
        /// 讀取設備內部，主控盤旁鑰匙鎖狀態，True：Maintenance 、 False：Auto
        /// </summary>
        /// <returns>True：Maintenance、False：Auto</returns>
        public bool ReadBCP_Maintenance()
        { return Plc.ReadBCP_Maintenance(); }

        /// <summary>
        /// 讀取設備外部，抽屜旁鑰匙鎖狀態，True：Maintenance 、 False：Auto
        /// </summary>
        /// <returns>True：Maintenance、False：Auto</returns>
        public bool ReadCB_Maintenance()
        { return Plc.ReadCB_Maintenance(); }

        /// <summary>
        /// 讀取主控盤EMO按鈕是否觸發，True：Push 、 False：Release
        /// </summary>
        /// <returns>True：Push、False：Release</returns>
        public Tuple<bool, bool, bool, bool, bool> ReadBCP_EMO()
        { return Plc.ReadBCP_EMO(); }

        /// <summary>
        /// 讀取抽屜EMO按鈕是否觸發，True：Push 、 False：Release
        /// </summary>
        /// <returns>True：Push、False：Release</returns>
        public Tuple<bool, bool, bool> ReadCB_EMO()
        { return Plc.ReadCB_EMO(); }

        /// <summary>
        /// 讀取Load Port 1 EMO按鈕是否觸發，True：Push 、 False：Release
        /// </summary>
        /// <returns>True：Push、False：Release</returns>
        public bool ReadLP1_EMO()
        { return Plc.ReadLP1_EMO(); }

        /// <summary>
        /// 讀取Load Port 2 EMO按鈕是否觸發，True：Push 、 False：Release
        /// </summary>
        /// <returns>True：Push、False：Release</returns>
        public bool ReadLP2_EMO()
        { return Plc.ReadLP2_EMO(); }

        /// <summary>
        /// 讀取主控盤的門，True：Open 、 False：Close
        /// </summary>
        /// <returns>True：Open、False：Close</returns>
        public bool ReadBCP_Door()
        { return Plc.ReadBCP_Door(); }

        /// <summary>
        /// 讀取Load Port 1的門，True：Open 、 False：Close
        /// </summary>
        /// <returns>True：Open、False：Close</returns>
        public bool ReadLP1_Door()
        { return Plc.ReadLP1_Door(); }

        /// <summary>
        /// 讀取Load Port 2的門，True：Open 、 False：Close
        /// </summary>
        /// <returns>True：Open、False：Close</returns>
        public bool ReadLP2_Door()
        { return Plc.ReadLP2_Door(); }

        /// <summary>
        /// 讀取主控箱內的偵煙器是否偵測到訊號，True：Alarm 、 False：Normal
        /// </summary>
        /// <returns>True：Alarm、False：Normal</returns>
        public bool ReadBCP_Smoke()
        { return Plc.ReadBCP_Smoke(); }
        #endregion

        #region PLC alarm signal
        public string ReadAlarm_General()
        { return Plc.ReadAlarm_General(); }

        public string ReadAlarm_Cabinet()
        { return Plc.ReadAlarm_Cabinet(); }

        public string ReadAlarm_CleanCh()
        { return Plc.ReadAlarm_CleanCh(); }

        public string ReadAlarm_BTRobot()
        { return Plc.ReadAlarm_BTRobot(); }

        public string ReadAlarm_MTRobot()
        { return Plc.ReadAlarm_MTRobot(); }

        public string ReadAlarm_OpenStage()
        { return Plc.ReadAlarm_OpenStage(); }

        public string ReadAlarm_InspCh()
        { return Plc.ReadAlarm_InspCh(); }

        public string ReadAlarm_LoadPort()
        { return Plc.ReadAlarm_LoadPort(); }

        public string ReadAlarm_CoverFan()
        { return Plc.ReadAlarm_CoverFan(); }

        public string ReadAlarm_MTClampInsp()
        { return Plc.ReadAlarm_MTClampInsp(); }

        public string ReadAllAlarmMessage()
        {
            string Result = "";
            Result += Plc.ReadAlarm_General();
            Result += Plc.ReadAlarm_Cabinet();
            Result += Plc.ReadAlarm_CleanCh();
            Result += Plc.ReadAlarm_BTRobot();
            Result += Plc.ReadAlarm_MTRobot();
            Result += Plc.ReadAlarm_OpenStage();
            Result += Plc.ReadAlarm_InspCh();
            Result += Plc.ReadAlarm_LoadPort();
            Result += Plc.ReadAlarm_CoverFan();
            Result += Plc.ReadAlarm_MTClampInsp();
            return Result;
        }
        #endregion

        #region PLC warning signal
        public string ReadWarning_General()
        { return Plc.ReadWarning_General(); }

        public string ReadWarning_Cabinet()
        { return Plc.ReadWarning_Cabinet(); }

        public string ReadWarning_CleanCh()
        { return Plc.ReadWarning_CleanCh(); }

        public string ReadWarning_BTRobot()
        { return Plc.ReadWarning_BTRobot(); }

        public string ReadWarning_MTRobot()
        { return Plc.ReadWarning_MTRobot(); }

        public string ReadWarning_OpenStage()
        { return Plc.ReadWarning_OpenStage(); }

        public string ReadWarning_InspCh()
        { return Plc.ReadWarning_InspCh(); }

        public string ReadWarning_LoadPort()
        { return Plc.ReadWarning_LoadPort(); }

        public string ReadWarning_CoverFan()
        { return Plc.ReadWarning_CoverFan(); }

        public string ReadWarning_MTClampInsp()
        { return Plc.ReadWarning_MTClampInsp(); }

        public string ReadAllWarningMessage()
        {
            string Result = "";
            Result += Plc.ReadWarning_General();
            Result += Plc.ReadWarning_Cabinet();
            Result += Plc.ReadWarning_CleanCh();
            Result += Plc.ReadWarning_BTRobot();
            Result += Plc.ReadWarning_MTRobot();
            Result += Plc.ReadWarning_OpenStage();
            Result += Plc.ReadWarning_InspCh();
            Result += Plc.ReadWarning_LoadPort();
            Result += Plc.ReadWarning_CoverFan();
            Result += Plc.ReadWarning_MTClampInsp();
            return Result;
        }
        #endregion
    }
}
