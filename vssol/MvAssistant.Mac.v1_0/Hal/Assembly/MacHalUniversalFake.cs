using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [Guid("7380FBCE-0552-4558-991A-771328870B5A")]
    public class MacHalUniversalFake : MacHalFakeComponentBase, IMacHalUniversal
    {
        public string CoverFanCtrl(uint FanID, uint WindSpeed)
        {
            return "OK";
        }

        public void EMSAlarm(bool BT_EMS, bool RT_EMS, bool OS_EMS, bool IC_EMS)
        {
            return;
        }

        public string ReadAlarm_BTRobot()
        {
            return "";
        }

        public string ReadAlarm_Cabinet()
        {
            return "";
        }

        public string ReadAlarm_CleanCh()
        {
            return "";
        }

        public string ReadAlarm_CoverFan()
        {
            return "";
        }

        public string ReadAlarm_General()
        {
            return "";
        }

        public string ReadAlarm_InspCh()
        {
            return "";
        }

        public string ReadAlarm_LoadPort()
        {
            return "";
        }

        public string ReadAlarm_MTClampInsp()
        {
            return "";
        }

        public string ReadAlarm_MTRobot()
        {
            return "";
        }

        public string ReadAlarm_OpenStage()
        {
            return "";
        }

        public string ReadAllAlarmMessage()
        {
            return "";
        }

        public string ReadAllWarningMessage()
        {
            return "";
        }

        public bool ReadBCP_Door()
        {
            return false;
        }

        public Tuple<bool, bool, bool, bool, bool> ReadBCP_EMO()
        {
            return new Tuple<bool, bool, bool, bool, bool>(false,false,false,false,false);
        }

        public bool ReadBCP_Maintenance()
        {
            return false;
        }

        public bool ReadBCP_Smoke()
        {
            return false;
        }

        public Tuple<bool, bool, bool> ReadCB_EMO()
        {
            return new Tuple<bool, bool, bool>(false,false,false);
        }

        public bool ReadCB_Maintenance()
        {
            return false;
        }

        public List<int> ReadCoverFanSpeed()
        {
            List<int> FanSpeedList = new List<int>();
            return FanSpeedList;
        }

        public bool ReadLP1_Door()
        {
            return false;
        }

        public bool ReadLP1_EMO()
        {
            return false;
        }

        public bool ReadLP2_Door()
        {
            return false;
        }

        public bool ReadLP2_EMO()
        {
            return false;
        }

        public bool ReadPowerON()
        {
            return false;
        }

        public string ReadWarning_BTRobot()
        {
            return "";
        }

        public string ReadWarning_Cabinet()
        {
            return "";
        }

        public string ReadWarning_CleanCh()
        {
            return "";
        }

        public string ReadWarning_CoverFan()
        {
            return "";
        }

        public string ReadWarning_General()
        {
            return "";
        }

        public string ReadWarning_InspCh()
        {
            return "";
        }

        public string ReadWarning_LoadPort()
        {
            return "";
        }

        public string ReadWarning_MTClampInsp()
        {
            return "";
        }

        public string ReadWarning_MTRobot()
        {
            return "";
        }

        public string ReadWarning_OpenStage()
        {
            return "";
        }

        public void ResetAllAlarm()
        {
            return;
        }

        public void SetBuzzer(uint BuzzerType)
        {
            return;
        }

        public void SetSignalTower(bool Red, bool Orange, bool Blue)
        {
            return;
        }
    }
}
