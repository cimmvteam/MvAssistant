using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompPlc
{
    public interface IMacHalPlcUniversal
    {
        void SetSignalTower(bool Red, bool Orange, bool Blue);

        void SetBuzzer(uint BuzzerType);

        string CoverFanCtrl(uint FanID, uint WindSpeed);

        List<int> ReadCoverFanSpeed();

        void ResetAll();

        void EMSAlarm(bool BT_EMS, bool RT_EMS, bool OS_EMS, bool IC_EMS);

        #region PLC狀態訊號
        bool ReadPowerON();

        bool ReadBCP_Maintenance();

        bool ReadCB_Maintenance();

        Tuple<bool, bool, bool, bool, bool> ReadBCP_EMO();

        Tuple<bool, bool, bool> ReadCB_EMO();

        bool ReadLP1_EMO();

        bool ReadLP2_EMO();

        bool ReadBCP_Door();

        bool ReadLP1_Door();

        bool ReadLP2_Door();

        bool ReadBCP_Smoke();
        #endregion

        #region PLC alarm signal
        string Alarm_General();
        string Alarm_Cabinet();
        string Alarm_CleanCh();
        string Alarm_BTRobot();
        string Alarm_MTRobot();
        string Alarm_OpenStage();
        string Alarm_InspCh();
        string Alarm_LoadPort();
        string Alarm_CoverFan();
        string Alarm_MTClampInsp();
        #endregion

        #region PLC warning signal
        string Warning_General();
        string Warning_Cabinet();
        string Warning_CleanCh();
        string Warning_BTRobot();
        string Warning_MTRobot();
        string Warning_OpenStage();
        string Warning_InspCh();
        string Warning_LoadPort();
        string Warning_CoverFan();
        string Warning_MTClampInsp();
        #endregion
    }
}
