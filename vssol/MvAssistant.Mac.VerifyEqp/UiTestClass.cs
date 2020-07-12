using MvAssistant.DeviceDrive.GudengLoadPort;
using MvAssistant.DeviceDrive.GudengLoadPort.LoadPortEventArgs;
using MvAssistant.DeviceDrive.GudengLoadPort.ReplyCode;
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
       public string ALight = "";
       public string BLight = "";
       public string CLight = "";
        string DLight = "";
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
                 SetResult(drawer, "Drawer [" + drawer.DrawerNO + "] LED 完成設定");
         
            }
            else //if(eventArgs.ReplyResultCode == ReplyResultCode.Failed)
            {
                SetResult(drawer, "Drawer [" + drawer.DrawerNO + "] LED 無法設定");
            }
            EnableDrawerComps(drawer);
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
            SetResult(drawer, "Drawer [" + drawer.DrawerNO + "] Tray 移動中 ");

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
    public class TestLoadPorts
    {
        private FrmTestUI MyForm = null;
        public  MvGudengLoadPortLdd LoadPort1 = null;
       public MvGudengLoadPortLdd LoadPort2 = null;
        MvGudengLoadPortCollection ldd = new MvGudengLoadPortCollection();
        string LoadPortAIP = "192.168.0.20";
        int LoadPportAPort = 1024;
        string LoadPortBIP = "192.168.0.21";
        int LoadportBPort = 1024;
        private TestLoadPorts()
        {

            LoadPort1 = ldd.CreateLoadPort(LoadPortAIP, LoadPportAPort, 1);
            // LoadPort1 = ldd.CreateLoadPort("127.0.0.1", 1024, 1);
            LoadPort2 = ldd.CreateLoadPort(LoadPortBIP, LoadportBPort, 2);
            BindEventHandler();

            LoadPort1.StartListenServerThread();
            LoadPort2.StartListenServerThread();
        }

        public TestLoadPorts(FrmTestUI myForm) : this()
        {
            MyForm = myForm;
        }
        public void BindEventHandler()
        {
            foreach (var loadport in ldd.LoadPorts)
            {
                loadport.OnAlarmResetFailHandler += this.OnAlarmResetFail;//014
                loadport.OnAlarmResetSuccessHandler += this.OnAlarmResetSuccess;//013
                loadport.OnBarcode_IDHandler += this.OnBarcode_ID;//005
                loadport.OnClamperActionTimeOutHandler += this.OnClamperActionTimeOut;// 200
                loadport.OnClamperHandler += this.OnClamper;//003
                loadport.OnClamperUnlockCompleteHandler += this.OnClamperUnlockComplete;//012
                loadport.OnClamperLockPositionFailed += this.OnClamperLockPositionFailed;//207
                loadport.OnClamperMotorAbnormality += this.OnClamperMotorAbnormality;//209
                loadport.OnClamperNotLockHandler += this.OnClamperNotLock;//022
                loadport.OnClamperLockCompleteHandler += this.OnClamperLockComplete;//006
                loadport.OnClamperUnlockPositionFailedHandler += this.OnClamperUnlockPositionFailed;//201
                loadport.OnPODPresentAbnormalityHandler += this.OnPODPresentAbnormality;//208
                loadport.OnDockPODComplete_EmptyHandler += this.OnDockPODComplete_Empty;//010
                loadport.OnDockPODComplete_HasReticleHandler += this.OnDockPODComplete_HasReticle;// 009
                loadport.OnDockPODStartHandler += this.OnDockPODStart;//008
                loadport.OnExecuteAlarmResetFirstHandler += this.OnExecuteAlarmResetFirst;// 016
                loadport.OnExecuteInitialFirstHandler += this.OnExecuteInitialFirst;//015
                loadport.OnInitialCompleteHandler += this.OnInitialComplete;//019
                loadport.OnInitialUnCompleteHandler += this.OnInitialUnComplete; // 自訂
                loadport.OnLoadportStatusHandler += this.OnLoadportStatus;// 018
                loadport.OnMustInAutoModeHandler += this.OnMustInAutoMode;//020
                loadport.OnPlacementHandler += this.OnPlacement;//001
                loadport.OnPODNotPutProperlyHandler += this.OnPODNotPutProperly;//023
                loadport.OnPresentHandler += this.OnPresent;//002
                loadport.OnReticlePositionAbnormalityHandler += this.OnReticlePositionAbnormality;//206
                loadport.OnRFIDHandler += this.OnRFID;//004
                loadport.OnStageMotionTimeoutHandler += this.OnStageMotionTimeout;//203
                loadport.OnStageMotorAbnormality += this.OnStageMotorAbnormality;//210
                loadport.OnStageOverDownLimitationHandler += this.OnStageOverDownLimitation;//205
                loadport.OnStageOverUpLimitationHandler += this.OnStageOverUpLimitation;//204
                loadport.OnStagePositionHandler += this.OnStagePosition;//017
                loadport.OnUndockCompleteHandler += this.OnUndockComplete;//011
                loadport.OnVacuumAbnormalityHandler += this.OnVacuumAbnormality;//202
                loadport.OnVacuumCompleteHandler += this.OnVacuumComplete;//007
                loadport.OnMustInManualModeHandler += this.OnMustInManualMode;// 021

            }
        }
        void SetResult(MvGudengLoadPortLdd loadport, string text)
        {
            TextBox textBox = null;
            string index = "";
            if (loadport.LoadPortNo == 1)
            {
                textBox = MyForm.txtBxLoportAResult;
                index = "A";
            }
            else if (loadport.LoadPortNo == 2)
            {
                textBox = MyForm.txtBxLoportBResult;
                index = "B";
            }
            textBox.Text = textBox.Text + "\r\n" + text.Replace("[]", "[" +index +"]");
        }
        public void ResetResult(MvGudengLoadPortLdd loadport)
        {
            if (loadport.LoadPortNo == 1) {
                MyForm.txtBxLoportAResult.Clear();
            }
            else if (loadport.LoadPortNo == 2)
            {
                MyForm.txtBxLoportBResult.Clear();
            }
        }
        public void DisableLoadportOperate(MvGudengLoadPortLdd loadport)
        {
           if(loadport.LoadPortNo==1)
            {
                MyForm.grpLoadportA.Enabled = false;
            }
            else if (loadport.LoadPortNo == 2)
            {
                MyForm.grpLoadportB.Enabled = false;
            }
        }

        public void EnableLoadportOperate(MvGudengLoadPortLdd loadport)
        {
            if (loadport.LoadPortNo == 1)
            {
                MyForm.grpLoadportA.Enabled = true;
            }
            else if (loadport.LoadPortNo == 2)
            {
                MyForm.grpLoadportB.Enabled = true;
            }
        }



        #region Event Handler
        private void OnPlacement(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnPlacementEventArgs)args;
            SetResult(loadport, "Placement [] =" + eventArgs.ReturnCode.ToString());
            this.EnableLoadportOperate(loadport);
            //NoteEventResult($"IP={loadport.ServerEndPoint},Event={nameof(OnPlacement).Replace("On", "")}", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }

        private void OnPresent(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnPresentEventArgs)args;
            SetResult(loadport, "Present [] =" + eventArgs.ReturnCode.ToString());
            this.EnableLoadportOperate(loadport);
            //NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnPresent).Replace("On", "")}", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }

        private void OnClamper(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnClamperEventArgs)args;
            SetResult(loadport, "Clamper [] =" + eventArgs.ReturnCode.ToString());
            this.EnableLoadportOperate(loadport);
            //NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnClamper).Replace("On", "")}", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }
        private void OnRFID(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnRFIDEventArgs)args;
            SetResult(loadport, "RFID [] =" + eventArgs.RFID);
            this.EnableLoadportOperate(loadport);
            // NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnRFID).Replace("On", "")}", "RFID:" + eventArgs.RFID);
        }
        private void OnBarcode_ID(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnBarcode_IDEventArgs)args;
            if (eventArgs.ReturnCode == EventBarcodeIDCode.Success)
            {
                //NoteEventResult($"IP={loadport.ServerEndPoint},  Event={nameof(OnBarcode_ID).Replace("On", "")}", "Barcode ID:" + eventArgs.BarcodeID);
                SetResult(loadport, "Barcode ID [] =" + eventArgs.BarcodeID.ToString());
            }
            else
            {
                // NoteEventResult($"IP={loadport.ServerEndPoint},  Event={nameof(OnBarcode_ID).Replace("On", "")}", "請取 Barcode ID失敗");
                SetResult(loadport, "無法讀取 Barcode ID [] ");
            }
            this.EnableLoadportOperate(loadport);
        }
        private void OnClamperLockComplete(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            // var s = nameof(OnClamperLockComplete);
            var eventArgs = (OnClamperLockCompleteEventArgs)args;
            SetResult(loadport, "ClamperLockComplete [] =" + eventArgs.ReturnCode.ToString());
            this.EnableLoadportOperate(loadport);
            // NoteEventResult($"IP={loadport.ServerEndPoint},   Event={nameof(OnClamperLockComplete).Replace("On", "")}", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }
        private void OnVacuumComplete(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnVacuumCompleteEventArgs)args;
            SetResult(loadport, "VacuumComplete [] =" + eventArgs.ReturnCode.ToString());
            this.EnableLoadportOperate(loadport);
            // NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnVacuumComplete).Replace("On", "")}", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }
        private void OnDockPODStart(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "DockPODStart []");
            this.EnableLoadportOperate(loadport);
            // NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnDockPODStart).Replace("On", "")}");
        }


        private void OnDockPODComplete_HasReticle(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "DockPODComplete_HasReticle []");
            this.EnableLoadportOperate(loadport);
            // NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnDockPODComplete_HasReticle).Replace("On", "")}");
        }
        private void OnDockPODComplete_Empty(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "DockPODComplete_Empty []");
            this.EnableLoadportOperate(loadport);
            // NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnDockPODComplete_Empty).Replace("On", "")}");
        }

        private void OnUndockComplete(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "UndockComplete []");
            this.EnableLoadportOperate(loadport);
            //NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnUndockComplete).Replace("On", "")}");
        }

        private void OnClamperUnlockComplete(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "ClamperUnlockComplete []");
            this.EnableLoadportOperate(loadport);
            //  NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnClamperUnlockComplete).Replace("On", "")}");
        }
        
        private void OnAlarmResetSuccess(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "AlarmResetSuccesst []");
            this.EnableLoadportOperate(loadport);
            // NoteEventResult($"IP={loadport.ServerEndPoint},  Event={nameof(OnAlarmResetSuccess).Replace("On", "")}");
        }
        private void OnAlarmResetFail(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "AlarmResetFail []");
            this.EnableLoadportOperate(loadport);
            //NoteEventResult($"IP={loadport.ServerEndPoint},  Event={nameof(OnAlarmResetFail).Replace("On", "")}");

        }
        private void OnExecuteInitialFirst(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "ExecuteInitialFirst []");
            this.EnableLoadportOperate(loadport);
            /**
            if (loadport.HasInvokeOriginalMethod)
            {
                loadport.CommandInitialRequest();
            }
    */
            // NoteEventResult($"IP={loadport.ServerEndPoint},  Event={nameof(OnExecuteInitialFirst).Replace("On", "")}");
        }
        private void OnExecuteAlarmResetFirst(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "AlarmResetFirst []");
            this.EnableLoadportOperate(loadport);
            /**
            if (loadport.HasInvokeOriginalMethod)
            {
                loadport.CommandAlarmReset();
            }*/
            //  NoteEventResult($"IP={loadport.ServerEndPoint},  Event={nameof(OnExecuteAlarmResetFirst).Replace("On", "")}");
        }
        private void OnStagePosition(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnStagePositionEventArgs)args;
            SetResult(loadport, "StagePosition [] =" + eventArgs.ReturnCode.ToString());
            this.EnableLoadportOperate(loadport);
            //  NoteEventResult($"IP={loadport.ServerEndPoint},   Event={nameof(OnStagePosition).Replace("On", "")}", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }
        private void OnLoadportStatus(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnLoadportStatusEventArgs)args;
            SetResult(loadport, "LoadportStatus [] =" + eventArgs.ReturnCode.ToString());
            this.EnableLoadportOperate(loadport);
            // NoteEventResult($"IP={loadport.ServerEndPoint},   Event={nameof(OnLoadportStatus).Replace("On", "")}", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }
        private void OnInitialComplete(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "InitialComplete []" );
            this.EnableLoadportOperate(loadport);
            /**
            if (loadport.HasInvokeOriginalMethod)
            {
                loadport.CommandAlarmReset();
            }*/
            // NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnInitialComplete).Replace("On", "")}");

        }

        /// <summary>自訂的未完成初始事件</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnInitialUnComplete(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "InitialComplete []");
            this.EnableLoadportOperate(loadport);
            // NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnInitialUnComplete).Replace("On", "")}");
        }
        private void OnMustInAutoMode(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "MustInAutoMode []");
            this.EnableLoadportOperate(loadport);
            //  NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnMustInAutoMode).Replace("On", "")}");
        }

        private void OnMustInManualMode(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "MustInManualMode []");
            this.EnableLoadportOperate(loadport);
            // NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnMustInManualMode).Replace("On", "")}");
        }

        private void OnClamperNotLock(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "ClamperNotLock []");
            this.EnableLoadportOperate(loadport);
            // NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnClamperNotLock).Replace("On", "")}");
        }

        private void OnPODNotPutProperly(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "PODNotPutProperly []");
            this.EnableLoadportOperate(loadport);
            // NoteEventResult($"IP={loadport.ServerEndPoint}, Ev
            //  NoteEventResult($"IP={loadport.ServerEndPoint},  Event={nameof(OnPODNotPutProperly).Replace("On", "")}");
        }
        #endregion

        #region Alarm Handler
        private void OnClamperActionTimeOut(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "ClamperActionTimeOut []");
            this.EnableLoadportOperate(loadport);
            // NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnClamperActionTimeOut).Replace("On", "")}");

        }
        private void OnClamperUnlockPositionFailed(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "ClamperUnlockPositionFailed []");
            this.EnableLoadportOperate(loadport);
            // NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnClamperUnlockPositionFailed).Replace("On", "")}");
        }
        private void OnVacuumAbnormality(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "VacuumAbnormality []");
            this.EnableLoadportOperate(loadport);
            //  NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnVacuumAbnormality).Replace("On", "")}");
        }
        private void OnStageMotionTimeout(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "StageMotionTimeout []");
            this.EnableLoadportOperate(loadport);
            //  NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnStageMotionTimeout).Replace("On", "")}");
        }
        private void OnStageOverUpLimitation(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "StageOverUpLimitation []");
            this.EnableLoadportOperate(loadport);
            // NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnStageOverUpLimitation).Replace("On", "")}");
        }
        private void OnStageOverDownLimitation(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "StageOverDownLimitation []");
            this.EnableLoadportOperate(loadport);
            //  NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnStageOverDownLimitation).Replace("On", "")}");
        }
        private void OnReticlePositionAbnormality(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "ReticlePositionAbnormality []");
            this.EnableLoadportOperate(loadport);
            //  NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnReticlePositionAbnormality).Replace("On", "")}");
        }
        private void OnClamperLockPositionFailed(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "ClamperLockPositionFailed []");
            this.EnableLoadportOperate(loadport);
            // NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnClamperLockPositionFailed).Replace("On", "")}");
        }
        private void OnPODPresentAbnormality(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "PODPresentAbnormality []");
            this.EnableLoadportOperate(loadport);
            //  NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnPODPresentAbnormality).Replace("On", "")}");
        }
        private void OnClamperMotorAbnormality(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "ClamperMotorAbnormality []");
            this.EnableLoadportOperate(loadport);
            //  NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnClamperMotorAbnormality).Replace("On", "")}");
        }
        private void OnStageMotorAbnormality(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            SetResult(loadport, "StageMotorAbnormality []");
            this.EnableLoadportOperate(loadport);
            //  NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnStageMotorAbnormality).Replace("On", "")}");
        }
        #endregion
    }
}
