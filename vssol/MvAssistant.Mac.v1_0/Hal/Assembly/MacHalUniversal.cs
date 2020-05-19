using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Motor;
using MvAssistant.Mac.v1_0.Hal.CompPlc;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [Guid("FAFCEF2B-6356-4438-890F-30F865CAA742")]
    public class MacHalUniversal : MacHalAssemblyBase, IMacHalUniversal
    {


        #region Device Components


        public IMacHalPlcUniversal plc_01 { get { return (IMacHalPlcUniversal)this.GetMachine(MacEnumDevice.universal_plc_01); } }

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
        public void ResetAll()
        { plc_01.ResetAll(); }

        /// <summary>
        /// 當Assembly出錯時，針對部件下緊急停止訊號，Box Transfer、Mask Transfer、Open Stage、Inspection Chamber
        /// </summary>
        /// <param name="BT_EMS">Box Transfer是否緊急停止</param>
        /// <param name="RT_EMS">Mask Transfer是否緊急停止</param>
        /// <param name="OS_EMS">Open Stage是否緊急停止</param>
        /// <param name="IC_EMS">Inspection Chamber是否緊急停止</param>
        public void EMSAlarm(bool BT_EMS, bool RT_EMS, bool OS_EMS, bool IC_EMS)
        { plc_01.EMSAlarm(BT_EMS, RT_EMS, OS_EMS, IC_EMS); }

        #region PLC狀態訊號
        public bool ReadPowerON()
        { return plc_01.ReadPowerON(); }

        public bool ReadBCP_Maintenance()
        { return plc_01.ReadBCP_Maintenance(); }

        public bool ReadCB_Maintenance()
        { return plc_01.ReadCB_Maintenance(); }

        public Tuple<bool, bool, bool, bool, bool> ReadBCP_EMO()
        { return plc_01.ReadBCP_EMO(); }

        public Tuple<bool, bool, bool> ReadCB_EMO()
        { return plc_01.ReadCB_EMO(); }

        public bool ReadLP1_EMO()
        { return plc_01.ReadLP1_EMO(); }

        public bool ReadLP2_EMO()
        { return plc_01.ReadLP2_EMO(); }

        public bool ReadBCP_Door()
        { return plc_01.ReadBCP_Door(); }

        public bool ReadLP1_Door()
        { return plc_01.ReadLP1_Door(); }

        public bool ReadLP2_Door()
        { return plc_01.ReadLP2_Door(); }

        /// <summary>
        /// 讀取煙霧偵測器訊號
        /// </summary>
        /// <returns></returns>
        public bool ReadBCP_Smoke()
        { return plc_01.ReadBCP_Smoke(); }
        #endregion

        public override int HalClose()
        {
            //return base.HalClose();
            return 0;
        }

        #region PLC alarm signal
        public string Alarm_General()
        { return plc_01.Alarm_General(); }

        public string Alarm_Cabinet()
        { return plc_01.Alarm_Cabinet();}

        public string Alarm_CleanCh()
        { return plc_01.Alarm_CleanCh(); }

        public string Alarm_BTRobot()
        { return plc_01.Alarm_BTRobot(); }

        public string Alarm_MTRobot()
        { return plc_01.Alarm_MTRobot(); }

        public string Alarm_OpenStage()
        { return plc_01.Alarm_OpenStage(); }

        public string Alarm_InspCh()
        { return plc_01.Alarm_InspCh(); }

        public string Alarm_LoadPort()
        { return plc_01.Alarm_LoadPort(); }

        public string Alarm_CoverFan()
        { return plc_01.Alarm_CoverFan(); }

        public string Alarm_MTClampInsp()
        { return plc_01.Alarm_MTClampInsp(); }
        #endregion

        #region PLC warning signal
        public string Warning_General()
        { return plc_01.Warning_General(); }

        public string Warning_Cabinet()
        { return plc_01.Warning_Cabinet(); }

        public string Warning_CleanCh()
        { return plc_01.Warning_CleanCh(); }

        public string Warning_BTRobot()
        { return plc_01.Warning_BTRobot(); }

        public string Warning_MTRobot()
        { return plc_01.Warning_MTRobot(); }

        public string Warning_OpenStage()
        { return plc_01.Warning_OpenStage(); }

        public string Warning_InspCh()
        { return plc_01.Warning_InspCh(); }

        public string Warning_LoadPort()
        { return plc_01.Warning_LoadPort(); }

        public string Warning_CoverFan()
        { return plc_01.Warning_CoverFan(); }

        public string Warning_MTClampInsp()
        { return plc_01.Warning_MTClampInsp(); }
        #endregion
    }
}
