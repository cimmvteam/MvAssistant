using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer
{
    /// <summary>Host to Equiipment Command</summary>
    public enum HostToEquipmentContent
    {
        SetMotionSpeed = 0,
        SetTimeOut = 1,
        SetParameter = 7,
        TrayMotion = 11,
        BrightLED = 12,
        PositionRead = 13,
        BoxDetection = 14,
        WriteNetSetting = 31,
        LCDMsg = 41,
        INI = 99,
    }
    
    public static class DrawerCommandExtends
    {
        /// <summary>取得 String Code</summary>
        /// <param name="inst"></param>
        /// <returns></returns>
        public static string GetStringCode(this HostToEquipmentContent inst)
        {
            string stringCode = ((int)inst).ToString("000");
            return stringCode;
        }
    }
}