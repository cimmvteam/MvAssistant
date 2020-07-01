using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.KjMachineDrawer;
using System.Net;
using System.Threading;
using static MvAssistant.DeviceDrive.KjMachineDrawer.MvKjMachineDrawerLdd;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Manifest;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.DeviceDrive.KjMachineDrawer.ReplyCode;
using MvAssistant.DeviceDrive.KjMachineDrawer.DrawerEventArgs;

namespace MvAssistant.Mac.TestMy.Device
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

        public MvKjMachineDrawerLdd Drawer_01_01_01 = null;
        private MvKjMachineDrawerCollection ldd = null;
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



        private void InitialDrawers()
        {
            var deviceEndPoint = new IPEndPoint(IPAddress.Parse(ClientIP_01_01_01), RemotePort);
            Drawer_01_01_01 = ldd.CreateDrawer(1, "01_01", deviceEndPoint, LocalIP);
        }

        public UtDeviceDrawer()
        {
            ldd = new MvKjMachineDrawerCollection(PortBegin, PortEnd, ListenStartupPort);
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
            string commText =    Drawer_01_01_01.CommandINI();
            Repeat();

        }
        [TestMethod]// [V] 2020/06/23
        public void SetMotionSpeed()
        {   // ~100,ReplySetSpeed,1@
            string commText = Drawer_01_01_01.CommandSetMotionSpeed(100);
            Repeat(); 
        }
        [TestMethod] // [V]  2020/06/23
        public void SetTimeOut()
        {   //~101,ReplySetTimeOut,1@
            string commText = Drawer_01_01_01.CommandSetTimeOut(100);
            Repeat();
        }

        [TestMethod] //???
        public void SetParameter()
        {

        }
        [TestMethod]//[V] 2020/06/23
        public void TrayMotionHome()
        {
            string commText = Drawer_01_01_01.CommandTrayMotionHome();
            Repeat();

        }
        [TestMethod]//[V] 2020/06/23
        public void TrayMotionOut()
        {

            string commText = Drawer_01_01_01.CommandTrayMotionOut();
            Repeat();

        }
        [TestMethod]//[V] 2020/06/23
        public void TrayMotionIn()
        {
            string commText = Drawer_01_01_01.CommandTrayMotionIn();

            Repeat();
        }
        [TestMethod]//[V] 2020/06/22
        public void BrightLEDAllOn()
        {

            string commText = Drawer_01_01_01.CommandBrightLEDAllOn();
        }
        [TestMethod]//[V] 2020/06/22
        public void BrightLedAllOff()
        {

            string commText = Drawer_01_01_01.CommandBrightLEDAllOff();


        }
        [TestMethod]//[V] 2020/06/22
        public void BrightLEDGreenOn()
        {

            string commText = Drawer_01_01_01.CommandBrightLEDGreenOn();


        }
        [TestMethod]// [V] 2020/06/22
        public void BrightLEDRedOn()
        {

            string commText = Drawer_01_01_01.CommandBrightLEDRedOn();


        }
        [TestMethod]// [V] 2020/06/22
        public void PositionRead()
        {

            string commText = Drawer_01_01_01.CommandPositionRead();


        }
        [TestMethod]//[V] No Box ,2020.06/22; [] Has Box
        public void BoxDetection()
        {

            string commText = Drawer_01_01_01.CommandBoxDetection();


        }
        [TestMethod]
        public void WriteNetSetting()
        {

            string commText = Drawer_01_01_01.CommandWriteNetSetting();


        }
        [TestMethod]// [V] 2020/06/24
        public void LCDMsg()
        {

            string commText = Drawer_01_01_01.CommandLCDMsg("01_01\r\ntSMC Setting");
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
            var drawer = (MvKjMachineDrawerLdd)sender;
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
            var drawer = (MvKjMachineDrawerLdd)sender;
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
            var drawer = (MvKjMachineDrawerLdd)sender;
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
            var drawer = (MvKjMachineDrawerLdd)sender;
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
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;
            var eventArgs = (OnReplyPositionEventArgs)args;
            var IHO = eventArgs.IHOStatus;

        }

        /// <summary>Event ReplyDetection(114)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnReplyBoxDetection(Object sender, EventArgs args)
        {
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;
            var eventArgs = (OnReplyBoxDetectionEventArgs)args;
            var hasBox = eventArgs.HasBox;
        }

        /// <summary>Event TrayArrive(115)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTrayArrive(object sender, EventArgs args)
        {
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;
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
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;

        }
        //"~141,LCDCMsg,1@
        public void OnLCDCMsg(object sender,EventArgs args)
        {
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;
            var eventArgs = (OnLCDCMsgEventArgs)args;
            if (eventArgs.ReplyResultCode == ReplyResultCode.Set_Successfully)
            { }
            else { }

        }
        /// <summary>Event TimeOutEvent(900)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTimeOutEvent(object sender, EventArgs args)
        {
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;
        }

        /// <summary>Event TrayMotioning(901)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTrayMotioning(object sender, EventArgs args)
        {
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;

        }

        /// <summary>Event INIFailed(902)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnINIFailed(object sender, EventArgs args)
        {
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;
        }

        /// <summary>Event TrayMotionError(903)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTryMotionError(object sender, EventArgs args)
        {
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;
        }

        /// <summary>Event TrayMotionError(903)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTrayMotionSensorOFF(object sender, EventArgs args)
        {
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;
        }


        /// <summary>Event Error</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnError(object sender, EventArgs args)
        {
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;
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
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;
        }

        #endregion


    [TestMethod]
        public void TestMethod1()
        {
            using (var drawer = new MvKjMachineDrawerCollection())
            {
                drawer.ConnectIfNo();


            }

        }
    }
}
