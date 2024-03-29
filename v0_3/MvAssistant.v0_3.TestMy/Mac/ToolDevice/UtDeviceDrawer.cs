﻿using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_2.DeviceDrive.KjMachineDrawer;
using System.Net;
using System.Threading;
// vs 2013
//using static MvAssistant.v0_2.DeviceDrive.KjMachineDrawer.MvKjMachineDrawerLdd;
using MvAssistant.v0_2.Mac.Hal;
using MvAssistant.v0_2.Mac.Manifest;
using MvAssistant.v0_2.Mac.Hal.Assembly;
using MvAssistant.v0_2.DeviceDrive.KjMachineDrawer.ReplyCode;
using MvAssistant.v0_2.DeviceDrive.KjMachineDrawer.DrawerEventArgs;
using System.Diagnostics;

namespace MvAssistant.v0_2.Mac.TestMy.ToolDevice
{
    [TestClass]
    public class UtDeviceDrawer
    {
        // 建一個 Listen 的 Server;
        // public UdpServerSocket UdpServer;
        public int RemotePort = 5000;

        public string LocalIP = "192.168.0.16";
       // public string LocalIP = "127.0.0.1";

        public string ClientIP_01_01_01 = "192.168.0.42";
        //public string ClientIP_01_01_01 = "127.0.0.1";

        public MvaKjMachineDrawerLdd Drawer_01_01_01 = null;
        private MvaKjMachineDrawerManager ldd = null;
        int PortBegin = 5000;
        int PortEnd = 5999;
        private int ListenStartupPort = 6000;
        private void BindEvent()
        {
            foreach (var drawer in ldd.Drawers)
            {
                drawer.OnReplyTrayMotionHandler += this.OnReplyTrayMotion;
                drawer.OnReplySetSpeedHandler += this.OnReplySetSpeed;
                drawer.OnReplySetTimeOutHandler += this.OnReplySetTimeOut;
                drawer.OnReplyBrightLEDHandler += this.OnReplyBrightLED;
                drawer.OnReplyPositionHandler += this.OnReplyPosition;
                drawer.OnReplyBoxDetection += this.OnReplyBoxDetection;
                drawer.OnTrayArriveHandler += this.OnTrayArrive;
                drawer.OnButtonEventHandler += this.OnButtonEvent;
                drawer.OnTimeOutEventHandler += this.OnTimeOutEvent;
                drawer.OnTrayMotioningHandler += this.OnTrayMotioning;
                drawer.OnINIFailedHandler += this.OnINIFailed;
                drawer.OnTrayMotionErrorHandler += this.OnTryMotionError;
                drawer.OnErrorHandler += this.OnError;
                drawer.OnSysStartUpHandler += this.OnSysStartUp;
                drawer.OnTrayMotionSensorOFFHandler += this.OnTrayMotionSensorOFF;
                drawer.OnLCDCMsgHandler += this.OnLCDCMsg;
            }
        }


        /// <summary>產生 Drawer</summary>
        private void InitialDrawers()
        {
            var deviceEndPoint = new IPEndPoint(IPAddress.Parse(ClientIP_01_01_01), RemotePort);
            Drawer_01_01_01 = ldd.CreateDrawer(1, "01_01", deviceEndPoint, LocalIP);
        }

        public UtDeviceDrawer()
        {
            ldd = new MvaKjMachineDrawerManager(PortBegin, PortEnd, ListenStartupPort);
            InitialDrawers();
            BindEvent();
            ldd.ListenSystStartUpEvent();
        }
        void Repeat()
        {
            var b = false;
            while (true)
            {
                Thread.Sleep(10);
                if(b)
                {
                    break;
                }
            }
        }

       

        #region Test Command
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]//[V] 2020/06/22
        public void INI()
        {
            // ~901,TrayMotioning@~115,TrayArrive,0@
            string commandText =    Drawer_01_01_01.CommandINI();
            NoteCommand(commandText);
            Repeat();

        }
        [TestMethod]// [V] 2020/06/23
        public void SetMotionSpeed()
        {   // ~100,ReplySetSpeed,1@
            string commandText = Drawer_01_01_01.CommandSetMotionSpeed(100);
            NoteCommand(commandText);
            Repeat(); 
        }
        [TestMethod] // [V]  2020/06/23
        public void SetTimeOut()
        {   //~101,ReplySetTimeOut,1@
            string commandText = Drawer_01_01_01.CommandSetTimeOut(100);
            NoteCommand(commandText);
            Repeat();
        }

        [TestMethod] //???
        public void SetParameter()
        {

        }
        [TestMethod]//[V] 2020/06/23
        public void TrayMotionHome()
        {
            string commandText = Drawer_01_01_01.CommandTrayMotionHome();
            NoteCommand(commandText);
            Repeat();

        }
        [TestMethod]//[V] 2020/06/23
        public void TrayMotionOut()
        {

            string commandText = Drawer_01_01_01.CommandTrayMotionOut();
            NoteCommand(commandText);
            Repeat();

        }
        [TestMethod]//[V] 2020/06/23
        public void TrayMotionIn()
        {
            string commandText = Drawer_01_01_01.CommandTrayMotionIn();
            NoteCommand(commandText);
            Repeat();
        }
        [TestMethod]//[V] 2020/06/22
        public void BrightLEDAllOn()
        {

            string commandText = Drawer_01_01_01.CommandBrightLEDAllOn();
            NoteCommand(commandText);
        }
        [TestMethod]//[V] 2020/06/22
        public void BrightLedAllOff()
        {

            string commandText = Drawer_01_01_01.CommandBrightLEDAllOff();
            NoteCommand(commandText);

        }
        [TestMethod]//[V] 2020/06/22
        public void BrightLEDGreenOn()
        {

            string commandText = Drawer_01_01_01.CommandBrightLEDGreenOn();
            NoteCommand(commandText);

        }
        [TestMethod]// [V] 2020/06/22
        public void BrightLEDRedOn()
        {

            string commandText = Drawer_01_01_01.CommandBrightLEDRedOn();
            NoteCommand(commandText);

        }
        [TestMethod]// [V] 2020/06/22
        public void PositionRead()
        {

            string commandText = Drawer_01_01_01.CommandPositionRead();
            NoteCommand(commandText);

        }
        [TestMethod]//[V] No Box ,2020.06/22; [] Has Box
        public void BoxDetection()
        {

            string commandText = Drawer_01_01_01.CommandBoxDetection();
            NoteCommand(commandText);

        }
        [TestMethod]
        public void WriteNetSetting()
        {

            string commText = Drawer_01_01_01.CommandWriteNetSetting();


        }
        [TestMethod]// [V] 2020/06/24
        public void LCDMsg()
        {

            string commandText = Drawer_01_01_01.CommandLCDMsg("01_01\r\ntSMC Setting");
            NoteCommand(commandText);
            //"~141,LCDCMsg,1@
            Repeat();
        }

        [TestMethod]//[V] 2020/06/24(回到6000 port)
        public void StartUp()
        {
            Repeat();
        }

        [TestMethod]//[V] 2020/06/24(回到6000 port)
        public void ButtonEvent()
        {
            Repeat();
        }
        #endregion

        #region Event


        /// <summary>Event ReplyTrayMotion(111)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnReplyTrayMotion(object sender, EventArgs args)
        {
            var drawer = (MvaKjMachineDrawerLdd)sender;
            var eventArgs = (OnReplyTrayMotionEventArgs)args;
            if (eventArgs.ReplyResultCode == ReplyResultCode.Set_Successfully)
            {  // 成功
              
            }
            else //if(eventArgs.ReplyResultCode == ReplyResultCode.Failed)
            { // 失敗
               
            }
            // vs 2013
            // NoteEvent(drawer, nameof(OnReplyTrayMotion), $"{eventArgs.ReplyResultCode.ToString()}({(int)eventArgs.ReplyResultCode })" );
            NoteEvent(drawer, "OnReplyTrayMotion", eventArgs.ReplyResultCode.ToString() + ((int)eventArgs.ReplyResultCode ) );
        }
        /// <summary>Event ReplySetSpeed(100)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnReplySetSpeed(object sender, EventArgs args)
        {
            var drawer = (MvaKjMachineDrawerLdd)sender;
            var eventArgs = (OnReplySetSpeedEventArgs)args;
            if (eventArgs.ReplyResultCode == ReplyResultCode.Set_Successfully)
            {

            }
            else if (eventArgs.ReplyResultCode == ReplyResultCode.Failed)
            {

            }
           // vs 2013
            //NoteEvent(drawer, nameof(OnReplySetSpeed), $"{eventArgs.ReplyResultCode.ToString()}({(int)eventArgs.ReplyResultCode })");
            NoteEvent(drawer, "OnReplySetSpeed", eventArgs.ReplyResultCode.ToString()+ "(" + (int)eventArgs.ReplyResultCode  +")");
        }
        /// <summary>Event ReplySetTimeOut(101)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnReplySetTimeOut(object sender, EventArgs args)
        {
            var drawer = (MvaKjMachineDrawerLdd)sender;
            var eventArgs = (OnReplySetTimeOutEventArgs)args;
            if (eventArgs.ReplyResultCode == ReplyResultCode.Set_Successfully)
            {

            }
            else //if(eventArgs.ReplyResultCode == ReplyResultCode.Failed)
            {

            }
             // vs 2013
            //NoteEvent(drawer, nameof(OnReplySetTimeOut), $"{eventArgs.ReplyResultCode.ToString()}({(int)eventArgs.ReplyResultCode })");
            NoteEvent(drawer, "OnReplySetTimeOut", eventArgs.ReplyResultCode.ToString()+ "("  + (int)eventArgs.ReplyResultCode + ")");
        }

        /// <summary>Event ReplySetBrightLED(112)</summary> 
        /// <param name="args"></param>
        /// <param name="sender"></param>
        private void OnReplyBrightLED(object sender, EventArgs args)
        {
            var drawer = (MvaKjMachineDrawerLdd)sender;
            var eventArgs = (OnReplyBrightLEDEventArgs)args;
            if (eventArgs.ReplyResultCode == ReplyResultCode.Set_Successfully)
            {

            }
            else //if(eventArgs.ReplyResultCode == ReplyResultCode.Failed)
            {

            }
            // vs 2013
            // NoteEvent(drawer, nameof(OnReplyBrightLED), $"{eventArgs.ReplyResultCode.ToString()}({(int)eventArgs.ReplyResultCode })");
            NoteEvent(drawer, "OnReplyBrightLED", eventArgs.ReplyResultCode.ToString() + "(" + (int)eventArgs.ReplyResultCode + ")");
        }

        /// <summary>Event ReplyPosition(113)</summary>
        /// <param name="sender"></param>
        /// <param name=""></param>
        private void OnReplyPosition(object sender, EventArgs args)
        {
            MvaKjMachineDrawerLdd drawer = (MvaKjMachineDrawerLdd)sender;
            var eventArgs = (OnReplyPositionEventArgs)args;
            var IHO = eventArgs.IHOStatus;
            // vs 2013
            // NoteEvent(drawer, nameof(OnReplyPosition), $"I={eventArgs.I}, H={eventArgs.H}, O={eventArgs.O},  IHO={IHO}");
            NoteEvent(drawer, "OnReplyPosition", "I=" + eventArgs.I + ", H=" + eventArgs.H + ", O=" + eventArgs.O + ",  IHO=" +  IHO);

        }

        /// <summary>Event ReplyDetection(114)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnReplyBoxDetection(Object sender, EventArgs args)
        {
            MvaKjMachineDrawerLdd drawer = (MvaKjMachineDrawerLdd)sender;
            var eventArgs = (OnReplyBoxDetectionEventArgs)args;
            var hasBox = eventArgs.HasBox;
            // vs 2013
            // NoteEvent(drawer, nameof(OnReplyBoxDetection), $"HasBox={hasBox}");
            NoteEvent(drawer, "OnReplyBoxDetection", "HasBox="  + hasBox);
        }

        /// <summary>Event TrayArrive(115)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTrayArrive(object sender, EventArgs args)
        {
            MvaKjMachineDrawerLdd drawer = (MvaKjMachineDrawerLdd)sender;
            var eventArgs = (OnTrayArriveEventArgs)args;
            if (eventArgs.TrayArriveType == TrayArriveType.ArriveHome)
            {

            }
            else if (eventArgs.TrayArriveType == TrayArriveType.ArriveIn)
            {

            }
            else //if (eventArgs.TrayArriveType == TrayArriveType.ArriveOut)
            {

            }
            // vs 2013
            // NoteEvent(drawer, nameof(OnTrayArrive), $"{eventArgs.TrayArriveType.ToString()}({(int)eventArgs.TrayArriveType})");
            NoteEvent(drawer, "OnTrayArrive", eventArgs.TrayArriveType.ToString() + "(" + (int)eventArgs.TrayArriveType  + ")");
        }

        /// <summary>Event ButtonEvent(120)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnButtonEvent(object sender, EventArgs args)
        {
            MvaKjMachineDrawerLdd drawer = (MvaKjMachineDrawerLdd)sender;
             // vs 2013
            // NoteEvent(drawer, nameof(OnButtonEvent));
            NoteEvent(drawer, "OnButtonEvent");
        }
        //"~141,LCDCMsg,1@
        public void OnLCDCMsg(object sender,EventArgs args)
        {
            MvaKjMachineDrawerLdd drawer = (MvaKjMachineDrawerLdd)sender;
            var eventArgs = (OnLCDCMsgEventArgs)args;
            if (eventArgs.ReplyResultCode == ReplyResultCode.Set_Successfully)
            { }
            else { }
             // vs 2013
            // NoteEvent(drawer, nameof(OnLCDCMsg), $"{eventArgs.ReplyResultCode.ToString()}({(int)eventArgs.ReplyResultCode})");
            NoteEvent(drawer, "OnLCDCMsg", eventArgs.ReplyResultCode.ToString() + "(" + (int)eventArgs.ReplyResultCode+ ")");
        }
        /// <summary>Event TimeOutEvent(900)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTimeOutEvent(object sender, EventArgs args)
        {
            MvaKjMachineDrawerLdd drawer = (MvaKjMachineDrawerLdd)sender;
             // vs 2013
            // NoteEvent(drawer, nameof(OnTimeOutEvent));
            NoteEvent(drawer, "OnTimeOutEvent");
        }

        /// <summary>Event TrayMotioning(901)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTrayMotioning(object sender, EventArgs args)
        {
            MvaKjMachineDrawerLdd drawer = (MvaKjMachineDrawerLdd)sender;
            // vs 2013
            // NoteEvent(drawer, nameof(OnTrayMotioning));
            NoteEvent(drawer, "OnTrayMotioning");
        }

        /// <summary>Event INIFailed(902)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnINIFailed(object sender, EventArgs args)
        {
            MvaKjMachineDrawerLdd drawer = (MvaKjMachineDrawerLdd)sender;
            // vs 2013
            // NoteEvent(drawer, nameof(OnINIFailed));
            NoteEvent(drawer, "OnINIFailed");

        }

        /// <summary>Event TrayMotionError(903)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTryMotionError(object sender, EventArgs args)
        {
            MvaKjMachineDrawerLdd drawer = (MvaKjMachineDrawerLdd)sender;
            // vs 2013
            //NoteEvent(drawer, nameof(OnTryMotionError));
            NoteEvent(drawer, "OnTryMotionError");

        }

        /// <summary>Event TrayMotionError(903)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTrayMotionSensorOFF(object sender, EventArgs args)
        {
            MvaKjMachineDrawerLdd drawer = (MvaKjMachineDrawerLdd)sender;
            // vs 2013
            // NoteEvent(drawer, nameof(OnTrayMotionSensorOFF));
            NoteEvent(drawer, ("OnTrayMotionSensorOFF"));
        }


        /// <summary>Event Error</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnError(object sender, EventArgs args)
        {
            MvaKjMachineDrawerLdd drawer = (MvaKjMachineDrawerLdd)sender;
            var eventArgs = (OnErrorEventArgs)args;
            if (eventArgs.ReplyErrorCode == ReplyErrorCode.Recovery)
            {

            }
            else //if (eventArgs.ReplyErrorCode == ReplyErrorCode.Error)
            {

            }
            // vs 2013
            //NoteEvent(drawer, nameof(OnError),$"{eventArgs.ReplyErrorCode.ToString()}({(int)eventArgs.ReplyErrorCode})");
            NoteEvent(drawer, "OnError",eventArgs.ReplyErrorCode.ToString()+ ((int)eventArgs.ReplyErrorCode));
        }
        /// <summary>Even SystemStartUp</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnSysStartUp(object sender, EventArgs args)
        {
            MvaKjMachineDrawerLdd drawer = (MvaKjMachineDrawerLdd)sender;
            // vs 2013
            //NoteEvent(drawer, nameof(OnSysStartUp));
            NoteEvent(drawer, "OnSysStartUp");
        }

        #endregion

        #region Auxiliary
        public void NoteCommand(string commandText)
        {
            Debug.WriteLine(commandText);
        }
        public void NoteEvent(MvaKjMachineDrawerLdd drawer, string eventName,string result="")
        {
            var endIP = drawer.DeviceIP;
            eventName = eventName.Replace("On","");
            if (result == "")
            {
                // vs 2013
                //Debug.WriteLine($"IP={endIP}, Event={eventName}");
                Debug.WriteLine("IP=" + endIP + ", Event=" + eventName); 
            }
            else
            {
                // vs 2013
                //Debug.WriteLine($"IP={endIP}, Event={eventName}, Result={result}");
                Debug.WriteLine("IP=" + endIP +", Event=" + eventName +", Result=" + result);
            }
        }
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            using (var drawer = new MvaKjMachineDrawerManager())
            {
                drawer.ConnectTry();


            }

        }


        [TestMethod]
        public void TestUdpClient()
        {
        


        }

        [TestMethod]
        public void TestUdpSocket()
        {
            using (var client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {

            }

        }


    }
}
