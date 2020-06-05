using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer
{
    /// <summary>Equipment to Host Command</summary>
    public enum EquipmentToHostContent
    {
        ReplyTrayMotion = 111,
        ReplySetSpeed = 100,
        ReplySetTimeOut = 101,
        ReplyPosition = 113,
        ReplyBoxDetection = 114,
        TrayArrive = 115,
        ButtonEvent = 120,
        TimeOutEvent = 900,
        TrayMotioning = 901,
        INIFailed = 902,
        TrayMotionError = 903,
        Error = 904,
        SysTartUp = 999,
    }
}
