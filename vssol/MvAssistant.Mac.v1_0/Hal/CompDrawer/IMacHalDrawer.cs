using MvAssistant.DeviceDrive.KjMachineDrawer;
using MvAssistant.DeviceDrive.KjMachineDrawer.ReplyCode;
using MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompDrawer
{
    public interface IMacHalDrawer: IMacHalComponent
    {
        object Tag { get; set; }
        string DeviceIndex { get; }
        string Index { get; set; }

        string DeviceIP { get; }

    #region command
        string CommandINI();
        string CommandSetMotionSpeed(int speed);
        string CommandSetTimeOut(int timeoutSeconds);
        string CommandTrayMotionHome();
        string CommandTrayMotionOut();
        string CommandTrayMotionIn();

        string CommandBrightLEDAllOn();
        string CommandBrightLEDAllOff();
        string CommandBrightLEDGreenOn();
        string CommandBrightLEDRedOn();
       
        

        string CommandPositionRead();
        

        string CommandBoxDetection();
     
       
        string CommandWriteNetSetting();
        string CommandLCDMsg(string message);

        string CommandSetParameterHomePosition(string homePosition);
        string CommandSetParameterOutSidePosition(string outsidePosition);
        string CommandSetParameterInSidePosition(string insidePosition);
        string CommandSetParameterIPAddress(string ipAddress);
        string CommandSetParameterSubMask(string submaskAddress);
        #endregion
        #region Event Handler
       
        event EventHandler OnTrayMotionFailedHandler;
        event EventHandler OnTrayMotionOKHandler;
        event EventHandler OnSetSpeedFailedHandler;
        event EventHandler OnSetSpeedOKHandler;

        #endregion

        #region Result Delegate
        void INIResult(object sender, bool result);
        void SetMotionSpeedResult(object sender, bool result);
        void BrightLEDResult(object sender, bool result);
        void PositionReadResult(object sender, string result);
        void BoxDetectionResult(object sender, bool result);
        void SetTimeOutResult(object sender, bool result);
        #endregion

    }
   
}
