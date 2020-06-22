using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.KjMachineDrawer;
using System.Net;
using System.Threading;
using static MvAssistant.DeviceDrive.KjMachineDrawer.Drawer;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Manifest;
using MvAssistant.Mac.v1_0.Hal.Assembly;

namespace MvAssistant.Mac.TestMy.Device
{
    [TestClass]
    public class UtDeviceDrawer
    {
        // 建一個 Listen 的 Server;
        // public UdpServerSocket UdpServer;
        public int RemotePort = 5000;

        public string LocalIP = "192.168.0.14";
       // public string LocalIP = "127.0.0.1";

        public string ClientIP_01_01_01 = "192.168.0.42";
        //public string ClientIP_01_01_01 = "127.0.0.1";

        public Drawer Drawer_01_01_01 = null;
        private MvKjMachineDrawerLdd ldd = null;
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
            }
        }



        private void InitialDrawers()
        {
            var deviceEndPoint = new IPEndPoint(IPAddress.Parse(ClientIP_01_01_01), RemotePort);
            Drawer_01_01_01 = ldd.CreateDrawer(1, "01_01", deviceEndPoint, LocalIP);
        }

        public UtDeviceDrawer()
        {
            ldd = new MvKjMachineDrawerLdd(PortBegin, PortEnd, ListenStartupPort);
            InitialDrawers();
            BindEvent();
            ldd.ListenSystStartUpEvent();
        }


        #region Test Command
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]//~901,TrayMotioning@~115,TrayArrive,0@
        public void INI()
        {

           string commText=    Drawer_01_01_01.CommandINI();

        }
        [TestMethod]// 20%,15%,10% //~100,ReplySetSpeed,1@
        public void SetMotionSpeed()
        {

            string commText = Drawer_01_01_01.CommandSetMotionSpeed(100);
        }
        [TestMethod] // 30 seconds, 60 seconds,10 seconds//~101,ReplySetTimeOut,1@
        public void SetTimeOut()
        {
            string commText = Drawer_01_01_01.CommandSetTimeOut(100);

        }

        [TestMethod] //???
        public void SetParameter()
        {

        }
        [TestMethod]//~111,ReplyTrayMotion,1@~901,TrayMotioning@~115,TrayArrive,0@
        public void TrayMotionHome()
        {
            string commText = Drawer_01_01_01.CommandTrayMotionHome();

        }
        [TestMethod]//~111,ReplyTrayMotion,1@~901,TrayMotioning@~115,TrayArrive,1@
        public void TrayMotionOut()
        {

            string commText = Drawer_01_01_01.CommandTrayMotionOut();


        }
        [TestMethod]//~111,ReplyTrayMotion,1@~901,TrayMotioning@~115,TrayArrive,2@
        public void TrayMotionIn()
        {
            string commText = Drawer_01_01_01.CommandTrayMotionIn();


        }
        [TestMethod]//send:V,   Recive: ~112,ReplyBrightLED,1@
        public void BrightLEDAllOn()
        {

            string commText = Drawer_01_01_01.CommandBrightLEDAllOn();
        }
        [TestMethod]//send:V,  Recive: ~112,ReplyBrightLED,1@
        public void BrightLedAllOff()
        {

            string commText = Drawer_01_01_01.CommandBrightLEDAllOff();


        }
        [TestMethod]//send: 燈號看不出來 ~112,ReplyBrightLED,1@
        public void BrightLEDGreenOn()
        {

            string commText = Drawer_01_01_01.CommandBrightLEDGreenOn();


        }
        [TestMethod]// Send:V Receive:~112,ReplyBrightLED,1@
        public void BrightLEDRedOn()
        {

            string commText = Drawer_01_01_01.CommandBrightLEDRedOn();


        }
        [TestMethod]//Home: ~113,ReplyPosition,7@; ~113,ReplyPosition,1@
        public void PositionRead()
        {

            string commText = Drawer_01_01_01.CommandPositionRead();


        }
        [TestMethod]//~114,ReplyBoxDetection,0@
        public void BoxDetection()
        {

            string commText = Drawer_01_01_01.CommandBoxDetection();


        }
        [TestMethod]
        public void WriteNetSetting()
        {

            string commText = Drawer_01_01_01.CommandWriteNetSetting();


        }
        [TestMethod]// send:V, Recieve: ~141,LCDCMsg,1@
        public void LCDMsg()
        {

            string commText = Drawer_01_01_01.CommandLCDMsg("01_01\r\ntSMC Setting");


        }

        [TestMethod]
        public void StartUp()
        {
            while (true)
            {
                Thread.Sleep(100);
            }
        }
        #endregion

        #region Event


        /// <summary>Event ReplyTrayMotion(111)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnReplyTrayMotion(object sender, EventArgs args)
        {
            var drawer = (Drawer)sender;
            var eventArgs = (OnReplyTrayMotionEventArgs)args;
            if (eventArgs.ReplyResultCode == ReplyResultCode.Set_Successfully)
            {  // 成功

            }
            else //if(eventArgs.ReplyResultCode == ReplyResultCode.Failed)
            { // 失敗

            }
        }
        /// <summary>Event ReplySetSpeed(100)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnReplySetSpeed(object sender, EventArgs args)
        {
            var drawer = (Drawer)sender;
            var eventArgs = (OnReplySetSpeedEventArgs)args;
            if (eventArgs.ReplyResultCode == ReplyResultCode.Set_Successfully)
            {

            }
            else if (eventArgs.ReplyResultCode == ReplyResultCode.Failed)
            {

            }
        }
        /// <summary>Event ReplySetTimeOut(101)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnReplySetTimeOut(object sender, EventArgs args)
        {
            var drawer = (Drawer)sender;
            var eventArgs = (OnReplySetTimeOutEventArgs)args;
            if (eventArgs.ReplyResultCode == ReplyResultCode.Set_Successfully)
            {

            }
            else //if(eventArgs.ReplyResultCode == ReplyResultCode.Failed)
            {

            }
        }

        /// <summary>Event ReplySetBrightLED(112)</summary> 
        /// <param name="args"></param>
        /// <param name="sender"></param>
        private void OnReplyBrightLED(object sender, EventArgs args)
        {
            var drawer = (Drawer)sender;
            var eventArgs = (OnReplyBrightLEDEventArgs)args;
            if (eventArgs.ReplyResultCode == ReplyResultCode.Set_Successfully)
            {

            }
            else //if(eventArgs.ReplyResultCode == ReplyResultCode.Failed)
            {

            }
        }

        /// <summary>Event ReplyPosition(113)</summary>
        /// <param name="sender"></param>
        /// <param name=""></param>
        private void OnReplyPosition(object sender, EventArgs args)
        {
            Drawer drawer = (Drawer)sender;
            var eventArgs = (OnReplyPositionEventArgs)args;
            var IHO = eventArgs.IHOStatus;

        }

        /// <summary>Event ReplyDetection(114)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnReplyBoxDetection(Object sender, EventArgs args)
        {
            Drawer drawer = (Drawer)sender;
            var eventArgs = (OnReplyBoxDetectionEventArgs)args;
            var hasBox = eventArgs.HasBox;
        }

        /// <summary>Event TrayArrive(115)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTrayArrive(object sender, EventArgs args)
        {
            Drawer drawer = (Drawer)sender;
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
        }

        /// <summary>Event ButtonEvent(120)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnButtonEvent(object sender, EventArgs args)
        {
            Drawer drawer = (Drawer)sender;

        }
        /// <summary>Event TimeOutEvent(900)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTimeOutEvent(object sender, EventArgs args)
        {
            Drawer drawer = (Drawer)sender;
        }

        /// <summary>Event TrayMotioning(901)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTrayMotioning(object sender, EventArgs args)
        {
            Drawer drawer = (Drawer)sender;

        }

        /// <summary>Event INIFailed(902)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnINIFailed(object sender, EventArgs args)
        {
            Drawer drawer = (Drawer)sender;
        }

        /// <summary>Event TrayMotionError(903)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTryMotionError(object sender, EventArgs args)
        {
            Drawer drawer = (Drawer)sender;
        }

        /// <summary>Event Error</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnError(object sender, EventArgs args)
        {
            Drawer drawer = (Drawer)sender;
            var eventArgs = (OnErrorEventArgs)args;
            if (eventArgs.ReplyErrorCode == ReplyErrorCode.Recovery)
            {

            }
            else //if (eventArgs.ReplyErrorCode == ReplyErrorCode.Error)
            {

            }
        }
        /// <summary>Even SystemStartUp</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnSysStartUp(object sender, EventArgs args)
        {
            Drawer drawer = (Drawer)sender;
        }

        #endregion


    [TestMethod]
        public void TestMethod1()
        {
            using (var drawer = new MvKjMachineDrawerLdd())
            {
                drawer.ConnectIfNo();


            }

        }
    }
}
