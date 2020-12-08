using MvAssistant.v0_2.DeviceDrive.KjMachineDrawer;
using MvAssistant.v0_2.DeviceDrive.KjMachineDrawer.ReplyCode;
using MvAssistant.v0_2.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.DeviceDrive
{
  public   interface IDrawerLdd
    {
       // string DeviceIP { get; set; }
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
        string CommandSetParameterGetwayAddress(string getwayAddress);
        event EventHandler OnReplyTrayMotionHandler;
        event EventHandler OnReplySetSpeedHandler;
        event EventHandler OnReplySetTimeOutHandler;
        event EventHandler OnReplyBrightLEDHandler;
        event EventHandler OnReplyPositionHandler;
        event EventHandler OnReplyBoxDetection;
        event EventHandler OnTrayArriveHandler;
        event EventHandler OnButtonEventHandler;
        event EventHandler OnLCDCMsgHandler;
        event EventHandler OnTimeOutEventHandler;
        event EventHandler OnTrayMotioningHandler;
        event EventHandler OnINIFailedHandler;
        event EventHandler OnTrayMotionErrorHandler;
        event EventHandler OnTrayMotionSensorOFFHandler;
        event EventHandler OnErrorHandler;
        DelegateDrawerBooleanResult INIResult { get; set; }
        DelegateDrawerBooleanResult SetMotionSpeedResult { get; set; }
        DelegateDrawerBooleanResult SetTimeOutResult { get; set; }
        DelegateDrawerBooleanResult BrightLEDResult { get; set; }
        DelegateDrawerStringResult PositionReadResult { get; set; }
        DelegateDrawerBooleanResult BoxDetectionResult { get; set; }
        DelegateDrawerIntResult TrayArriveResult { get; set; }

    }
}
