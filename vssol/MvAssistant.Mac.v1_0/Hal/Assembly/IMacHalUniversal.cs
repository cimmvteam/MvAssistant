using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [Guid("85C61EE9-7989-48D9-B5DC-9A93E7146863")]
    public interface IMacHalUniversal : IMacHalAssembly
    {
        /// <summary>
        /// 設備訊號燈設定
        /// </summary>
        /// <param name="Red"></param>
        /// <param name="Orange"></param>
        /// <param name="Blue"></param>
        void SetSignalTower(bool Red, bool Orange, bool Blue);

        /// <summary>
        /// 蜂鳴器，0:停止鳴叫、1~4分別是不同的鳴叫方式
        /// </summary>
        /// <param name="BuzzerType"></param>
        void SetBuzzer(uint BuzzerType);

        /// <summary>
        /// A08外罩風扇調整風速，風扇編號、風速控制
        /// </summary>
        /// <param name="FanID"></param>
        /// <param name="WindSpeed"></param>
        /// <returns></returns>
        string CoverFanCtrl(uint FanID, uint WindSpeed);

        /// <summary>
        /// 讀取外罩風扇風速
        /// </summary>
        /// <returns></returns>
        List<int> ReadCoverFanSpeed();

        /// <summary>
        /// 重置所有PLC Alarm訊息
        /// </summary>
        void ResetAllAlarm();

        /// <summary>
        /// 當Assembly出錯時，針對部件下緊急停止訊號，Box Transfer、Mask Transfer、Open Stage、Inspection Chamber
        /// </summary>
        /// <param name="BT_EMS">Box Transfer是否緊急停止</param>
        /// <param name="RT_EMS">Mask Transfer是否緊急停止</param>
        /// <param name="OS_EMS">Open Stage是否緊急停止</param>
        /// <param name="IC_EMS">Inspection Chamber是否緊急停止</param>
        void EMSAlarm(bool BT_EMS, bool RT_EMS, bool OS_EMS, bool IC_EMS);

        /// <summary>
        /// 讀取電源狀態，True：Power ON 、 False：Power OFF
        /// </summary>
        /// <returns>True：Power ON、False：Power OFF</returns>
        bool ReadPowerON();

        /// <summary>
        /// 讀取設備內部，主控盤旁鑰匙鎖狀態，True：Maintenance 、 False：Auto
        /// </summary>
        /// <returns>True：Maintenance、False：Auto</returns>
        bool ReadBCP_Maintenance();

        /// <summary>
        /// 讀取設備外部，抽屜旁鑰匙鎖狀態，True：Maintenance 、 False：Auto
        /// </summary>
        /// <returns>True：Maintenance、False：Auto</returns>
        bool ReadCB_Maintenance();

        /// <summary>
        /// 讀取主控盤EMO按鈕是否觸發，True：Push 、 False：Release
        /// </summary>
        /// <returns>True：Push、False：Release</returns>
        Tuple<bool, bool, bool, bool, bool> ReadBCP_EMO();

        /// <summary>
        /// 讀取抽屜EMO按鈕是否觸發，True：Push 、 False：Release
        /// </summary>
        /// <returns>True：Push、False：Release</returns>
        Tuple<bool, bool, bool> ReadCB_EMO();

        /// <summary>
        /// 讀取Load Port 1 EMO按鈕是否觸發，True：Push 、 False：Release
        /// </summary>
        /// <returns>True：Push、False：Release</returns>
        bool ReadLP1_EMO();

        /// <summary>
        /// 讀取Load Port 2 EMO按鈕是否觸發，True：Push 、 False：Release
        /// </summary>
        /// <returns>True：Push、False：Release</returns>
        bool ReadLP2_EMO();

        /// <summary>
        /// 讀取主控盤的門，True：Open 、 False：Close
        /// </summary>
        /// <returns>True：Open、False：Close</returns>
        bool ReadBCP_Door();

        /// <summary>
        /// 讀取Load Port 1的門，True：Open 、 False：Close
        /// </summary>
        /// <returns>True：Open、False：Close</returns>
        bool ReadLP1_Door();

        /// <summary>
        /// 讀取Load Port 2的門，True：Open 、 False：Close
        /// </summary>
        /// <returns>True：Open、False：Close</returns>
        bool ReadLP2_Door();

        /// <summary>
        /// 讀取主控箱內的偵煙器是否偵測到訊號，True：Alarm 、 False：Normal
        /// </summary>
        /// <returns>True：Alarm、False：Normal</returns>
        bool ReadBCP_Smoke();

        string ReadAlarm_General();

        string ReadAlarm_Cabinet();

        string ReadAlarm_CleanCh();

        string ReadAlarm_BTRobot();

        string ReadAlarm_MTRobot();

        string ReadAlarm_OpenStage();

        string ReadAlarm_InspCh();

        string ReadAlarm_LoadPort();

        string ReadAlarm_CoverFan();

        string ReadAlarm_MTClampInsp();

        string ReadAllAlarmMessage();

        string ReadWarning_General();

        string ReadWarning_Cabinet();

        string ReadWarning_CleanCh();

        string ReadWarning_BTRobot();

        string ReadWarning_MTRobot();

        string ReadWarning_OpenStage();

        string ReadWarning_InspCh();

        string ReadWarning_LoadPort();

        string ReadWarning_CoverFan();

        string ReadWarning_MTClampInsp();

        string ReadAllWarningMessage();
    }
}
