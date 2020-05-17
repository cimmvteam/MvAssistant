using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompPlc
{
    public interface IMacHalPlcUniversal
    {
        //void SetSignalTower(bool Red, bool Orange, bool Blue);

        //void SetBuzzer(uint BuzzerType);

        //string CoverFanCtrl(uint FanID, uint WindSpeed);

        //List<int> ReadCoverFanSpeed();

        //void ResetAll();

        //void EMSAlarm(bool BT_EMS, bool RT_EMS, bool OS_EMS, bool IC_EMS);

        //#region PLC狀態訊號
        //bool ReadPowerON();

        //bool ReadBCP_Maintenance();

        //bool ReadCB_Maintenance();

        //Tuple<bool, bool, bool, bool, bool> ReadBCP_EMO();

        //Tuple<bool, bool, bool> ReadCB_EMO();

        //bool ReadLP1_EMO();

        //bool ReadLP2_EMO();

        //bool ReadBCP_Door();

        //bool ReadLP1_Door();

        //bool ReadLP2_Door();

        //bool ReadBCP_Smoke();

        //bool ReadLP_Light_Curtain();
        //#endregion
    }
}
