﻿using MvAssistant.v0_3.DeviceDrive.KjMachineDrawer;
using MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.ReplyCode;
using MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.Mac.Hal.CompDrawer
{
    public interface IMacHalDrawer : IMacHalComponent
    {
        string DeviceId { get; }
        
        void SetDrawerWorkState(DrawerWorkState state);
        void ResetCurrentWorkState();
        Action PressButtonToLoad { get; set; }
        DrawerWorkState CurrentWorkState { get; }
        bool IsInitialing { get; set; }

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

        /// <summary>開始 Initial Start</summary>
        InitialStart,

        /// <summary>Intial 進行中</summary>
        InitialIng,

        /// <summary>Initial Failed</summary>
        InitialFailed,
       
        TrayMotionFailed,


        TrayArriveAtPositionHome,

   
        TrayArriveAtPositionOut,

   
        TrayArriveAtPositionIn,

        /// <summary>有盒子</summary>
        BoxExist,

        /// <summary>没有盒子</summary>
        BoxNotExist,
        MoveTrayToPositionOutStart,
        MoveTrayToPositionInStart,
        TrayMoveToOutStart,
        TrayMoveToHomeStart,
        MoveTrayToPositionHomeStart,
        MoveTrayToPositionOutIng,
        MoveTrayToPositionHomeIng,
        MoveTrayToPositionInIng,
    }


   

}
