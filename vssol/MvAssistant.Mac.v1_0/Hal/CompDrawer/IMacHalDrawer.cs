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
    public interface IMacHalDrawer : IMacHalComponent
    {
        string DeviceIndex { get; }
        
        void SetDrawerWorkState(DrawerWorkState state);
        void ResetCurrentWorkState();
        DrawerWorkState CurrentWorkState { get; }


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

        event EventHandler OnTrayMotionOKHandler;
        event EventHandler OnTrayMotionFailedHandler;


        event EventHandler OnSetMotionSpeedOKHandler;
        event EventHandler OnSetMotionSpeedFailedHandler;

        event EventHandler OnSetTimeOutOKHandler;
        event EventHandler OnSetTimeOutFailedHandler;

        event EventHandler OnTrayArriveHomeHandler;
        event EventHandler OnTrayArriveInHandler;
        event EventHandler OnTrayArriveOutHandler;
       
      event EventHandler OnTrayMotioningHandler;

      event EventHandler OnPositionStatusHandler;

        event EventHandler OnDetectedHasBoxHandler;
        event EventHandler OnDetectedEmptyBoxHandler;

       event EventHandler OnTrayMotionSensorOFFHandler;// TrayMotionError(903)
       event EventHandler OnERRORREcoveryHandler;
       event EventHandler OnERRORErrorHandler;

      event EventHandler OnSysStartUpHandler;
      event EventHandler OnButtonEventHandler;

        event EventHandler OnBrightLEDOKHandler;
        event EventHandler OnBrightLEDFailedHandler;

         event EventHandler OnLCDCMsgOKHandler;
         event EventHandler OnLCDCMsgFailedHandler ;

        event EventHandler OnINIFailedHandler;
        event EventHandler OnINIOKHandler;
        #endregion


        #region Result Delegate
        /**
        void INIResult(object sender, bool result);
        void SetMotionSpeedResult(object sender, bool result);
        void BrightLEDResult(object sender, bool result);
        void PositionReadResult(object sender, string result);
        void BoxDetectionResult(object sender, bool result);
        void SetTimeOutResult(object sender, bool result);
    */    
    #endregion

    }
   

    public enum DrawerWorkState
    {
        /// <summary>任何狀態</summary>
        AnyState,
        
        /// <summary>Initial Failed</summary>
        InitialFailed,
       
        TrayMotionFailed,


        TrayArriveAtHome,

   
        TrayArriveAtOut,

   
        TrayArraiveAtIn,

        /// <summary>有盒子</summary>
        BoxExist,

        /// <summary>没有盒子</summary>
        BoxNotExist,
        TrayMoveToInStart,
        TrayMoveToOutStart,
        TrayMoveToHomeStart,
    }
}
