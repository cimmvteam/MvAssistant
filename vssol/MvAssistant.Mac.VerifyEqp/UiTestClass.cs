using MvAssistant.DeviceDrive.KjMachineDrawer;
using MvAssistant.DeviceDrive.KjMachineDrawer.DrawerEventArgs;
using MvAssistant.DeviceDrive.KjMachineDrawer.ReplyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MvAssistantMacVerifyEqp
{
    public class UiTestClass
    {
    }
   
    public class TestDrawers
    {
        private static object countBoxobject = new object();
        public bool countDrawerBox = false;
        public FrmTestUI MyFORM = null;
        public TestDrawers(FrmTestUI myFrom):this()
        {
            MyFORM = myFrom;
        }
        private TestDrawers()
        {
            ldd = new MvKjMachineDrawerCollection(PortBegin, PortEnd, ListenStartupPort);
            InitialDrawers();
            BindEvent();
            ldd.ListenSystStartUpEvent();
        }
        public const int RemotePort = 5000;
        public const string LocalIP = "192.168.0.14";
        public const string ClientIP_A = "192.168.0.34";
        public const string ClientIP_B = "192.168.0.42";
        public const string ClientIP_C = "192.168.0.50";
        public const string ClientIP_D = "192.168.0.54";
        public  MvKjMachineDrawerLdd DrawerA = null;
        public  MvKjMachineDrawerLdd DrawerB = null;
        public  MvKjMachineDrawerLdd DrawerC = null;
        public  MvKjMachineDrawerLdd DrawerD = null;
        public  MvKjMachineDrawerCollection ldd = null;
        public const int PortBegin = 5000;
        public const int PortEnd = 5999;
        public const int ListenStartupPort = 6000;
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

        void SetDisplayDrawersBoxNum()
        {
            lock(countBoxobject)
            {
                var n = Convert.ToInt32(MyFORM.txtDrawerBoxNum.Text);
                n++;
                MyFORM.txtDrawerBoxNum.Text = n.ToString();
            }
        } 
        /// <summary>產生 Drawer</summary>
        private void InitialDrawers()
        {
            var deviceEndPoint = new IPEndPoint(IPAddress.Parse(ClientIP_A), RemotePort);
            DrawerA = ldd.CreateDrawer(1, "A", deviceEndPoint, LocalIP);
            deviceEndPoint = new IPEndPoint(IPAddress.Parse(ClientIP_B), RemotePort);
            DrawerB = ldd.CreateDrawer(1, "B", deviceEndPoint, LocalIP);
            deviceEndPoint = new IPEndPoint(IPAddress.Parse(ClientIP_C), RemotePort);
            DrawerC = ldd.CreateDrawer(1, "C", deviceEndPoint, LocalIP);
            deviceEndPoint = new IPEndPoint(IPAddress.Parse(ClientIP_D), RemotePort);
            DrawerD = ldd.CreateDrawer(1, "D", deviceEndPoint, LocalIP);
        }
        private void SetDetectDrawerBoxResult(MvKjMachineDrawerLdd drawer,bool result)
        {
            if (drawer.DrawerNO == "A")
            {
                MyFORM.chkBoxDrawerAHasbox.Checked = result;
            }
            else if (drawer.DrawerNO == "B")
            {
                MyFORM.chkBoxDrawerBHasbox.Checked = result;
            }
            else if (drawer.DrawerNO == "C")
            {
                MyFORM.chkBoxDrawerCHasbox.Checked = result;
            }
            else //if(drawer.DrawerNO == "D")
            {
                MyFORM.chkBoxDrawerDHasbox.Checked = result;
            }
        }
       
        public void InitialDRawer(MvKjMachineDrawerLdd drawer)
        {
            if (drawer.DrawerNO == "A")
            {
                MyFORM.txtBxDrawerAResult.Clear();
            }
            else if(drawer.DrawerNO == "B")
            {
                MyFORM.txtBxDrawerBResult.Clear();
            }
            else if(drawer.DrawerNO == "C")
            {
                MyFORM.txtBxDrawerCResult.Clear();
            }
            else //if(drawer.DrawerNO == "D")
            {
                MyFORM.txtBxDrawerDResult.Clear();
            }
        }
        public void DisableDrawerComps(MvKjMachineDrawerLdd drawer)
        {
          
            if (drawer.DrawerNO == "A")
            {
                MyFORM.grpDrawerAComp.Enabled = false;
            }
            else if (drawer.DrawerNO == "B")
            {
                MyFORM.grpDrawerBComp.Enabled = false;
            }
            else if (drawer.DrawerNO == "C")
            {
                MyFORM.grpDrawerCComp.Enabled = false;
            }
            else //if(drawer.DrawerNO == "D")
            {
                MyFORM.grpDrawerDComp.Enabled = false;
            }

        }
        public void EnableDrawerComps(MvKjMachineDrawerLdd drawer)
        {
           if (drawer.DrawerNO == "A")
            {
                MyFORM.grpDrawerAComp.Enabled = true;
            }
            else if (drawer.DrawerNO == "B")
            {
                MyFORM.grpDrawerBComp.Enabled = true;
            }
            else if (drawer.DrawerNO == "C")
            {
                MyFORM.grpDrawerCComp.Enabled = true;
            }
            else //if(drawer.DrawerNO == "D")
            {
                MyFORM.grpDrawerDComp.Enabled = true;
            }
        }

        public void SetResult(MvKjMachineDrawerLdd drawer,string text)
        {
            TextBox textBox;
            if (drawer.DrawerNO == "A")
            {
                textBox = MyFORM.txtBxDrawerAResult;
            }
            else if (drawer.DrawerNO == "B")
            {
                textBox = MyFORM.txtBxDrawerBResult;
            }
            else if (drawer.DrawerNO == "C")
            {
                textBox = MyFORM.txtBxDrawerCResult;
            }
            else //if(drawer.DrawerNO == "D")
            {
                textBox = MyFORM.txtBxDrawerDResult;
            }
            textBox.Text = textBox.Text + "\r\n" + text;
        }
        /// <summary>Event ReplyTrayMotion(111)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnReplyTrayMotion(object sender, EventArgs args)
        {
            var drawer = (MvKjMachineDrawerLdd)sender;
            var eventArgs = (OnReplyTrayMotionEventArgs)args;
            if (eventArgs.ReplyResultCode == ReplyResultCode.Set_Successfully)
            {  // 成功
                SetResult(drawer, "開始移動 Drawer [" + drawer.DrawerNO + "] Tray"   );
            }
            else //if(eventArgs.ReplyResultCode == ReplyResultCode.Failed)
            { // 失敗
                SetResult(drawer, "無法移動 Drawer [" + drawer.DrawerNO + "] Tray");
            }
            EnableDrawerComps(drawer);
           // NoteEvent(drawer, nameof(OnReplyTrayMotion), $"{eventArgs.ReplyResultCode.ToString()}({(int)eventArgs.ReplyResultCode })");
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
           // NoteEvent(drawer, nameof(OnReplySetSpeed), $"{eventArgs.ReplyResultCode.ToString()}({(int)eventArgs.ReplyResultCode })");
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
           // NoteEvent(drawer, nameof(OnReplySetTimeOut), $"{eventArgs.ReplyResultCode.ToString()}({(int)eventArgs.ReplyResultCode })");
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
          //  NoteEvent(drawer, nameof(OnReplyBrightLED), $"{eventArgs.ReplyResultCode.ToString()}({(int)eventArgs.ReplyResultCode })");
        }

        /// <summary>Event ReplyPosition(113)</summary>
        /// <param name="sender"></param>
        /// <param name=""></param>
        private void OnReplyPosition(object sender, EventArgs args)
        {
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;
            var eventArgs = (OnReplyPositionEventArgs)args;
            var IHO = eventArgs.IHOStatus;
           // NoteEvent(drawer, nameof(OnReplyPosition), $"I={eventArgs.I}, H={eventArgs.H}, O={eventArgs.O},  IHO={IHO}");

        }

        /// <summary>Event ReplyDetection(114)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnReplyBoxDetection(Object sender, EventArgs args)
        {
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;
            var eventArgs = (OnReplyBoxDetectionEventArgs)args;
            var hasBox = eventArgs.HasBox;
            if (hasBox)
            {
                SetResult(drawer, "Drawer [" + drawer.DrawerNO + "] 有Box");
            }
            else
            {
                SetResult(drawer, "Drawer [" + drawer.DrawerNO + "] 沒有Box");
            }
            EnableDrawerComps(drawer);
            SetDetectDrawerBoxResult(drawer, hasBox);
            if (hasBox && countDrawerBox )
            {
                SetDisplayDrawersBoxNum();
            }

            //  NoteEvent(drawer, nameof(OnReplyBoxDetection), $"HasBox={hasBox}");
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
                SetResult(drawer, "Drawer [" + drawer.DrawerNO + "] Tray 到達Home 點");
            }
            else if (eventArgs.TrayArriveType == TrayArriveType.ArriveIn)
            {
                SetResult(drawer, "Drawer [" + drawer.DrawerNO + "] Tray 到達 In ");
            }
            else //if (eventArgs.TrayArriveType == TrayArriveType.ArriveOut)
            {
                SetResult(drawer, "Drawer [" + drawer.DrawerNO + "] Tray 到達 Out ");
            }
            EnableDrawerComps(drawer);
            //  NoteEvent(drawer, nameof(OnTrayArrive), $"{eventArgs.TrayArriveType.ToString()}({(int)eventArgs.TrayArriveType})");
        }

        /// <summary>Event ButtonEvent(120)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnButtonEvent(object sender, EventArgs args)
        {
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;
          //  NoteEvent(drawer, nameof(OnButtonEvent));
        }
        //"~141,LCDCMsg,1@
        public void OnLCDCMsg(object sender, EventArgs args)
        {
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;
            var eventArgs = (OnLCDCMsgEventArgs)args;
            if (eventArgs.ReplyResultCode == ReplyResultCode.Set_Successfully)
            { }
            else { }
          //  NoteEvent(drawer, nameof(OnLCDCMsg), $"{eventArgs.ReplyResultCode.ToString()}({(int)eventArgs.ReplyResultCode})");
        }
        /// <summary>Event TimeOutEvent(900)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTimeOutEvent(object sender, EventArgs args)
        {
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;
          //  NoteEvent(drawer, nameof(OnTimeOutEvent));
        }

        /// <summary>Event TrayMotioning(901)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTrayMotioning(object sender, EventArgs args)
        {
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;
            SetResult(drawer, "Drawer [" + drawer.DrawerNO + "] Tray 開始移動 ");

            //  NoteEvent(drawer, nameof(OnTrayMotioning));
        }

        /// <summary>Event INIFailed(902)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnINIFailed(object sender, EventArgs args)
        {
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;
            SetResult(drawer, "Drawer [" + drawer.DrawerNO + "] 初始化失敗 ");
            EnableDrawerComps(drawer);
            //  NoteEvent(drawer, nameof(OnINIFailed));
        }

        /// <summary>Event TrayMotionError(903)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTryMotionError(object sender, EventArgs args)
        {
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;
            //  NoteEvent(drawer, nameof(OnTryMotionError));
            SetResult(drawer, "Drawer [" + drawer.DrawerNO + "] Tray 移動失敗 ");
            EnableDrawerComps(drawer);

        }

        /// <summary>Event TrayMotionError(903)</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTrayMotionSensorOFF(object sender, EventArgs args)
        {
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;
            //  NoteEvent(drawer, nameof(OnTrayMotionSensorOFF));
            SetResult(drawer, "Drawer [" + drawer.DrawerNO + "] Motion SenserOFF ");
            EnableDrawerComps(drawer);
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
                SetResult(drawer, "Drawer [" + drawer.DrawerNO + "] Recovery ");
            }
            else //if (eventArgs.ReplyErrorCode == ReplyErrorCode.Error)
            {
                SetResult(drawer, "Drawer [" + drawer.DrawerNO + "] Error ");
            }
            EnableDrawerComps(drawer);
            //   NoteEvent(drawer, nameof(OnError), $"{eventArgs.ReplyErrorCode.ToString()}({(int)eventArgs.ReplyErrorCode})");
        }
        /// <summary>Even SystemStartUp</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnSysStartUp(object sender, EventArgs args)
        {
            MvKjMachineDrawerLdd drawer = (MvKjMachineDrawerLdd)sender;
           // NoteEvent(drawer, nameof(OnSysStartUp));
        }

    }
    public class TestLoadPort
    {
        
    }
}
