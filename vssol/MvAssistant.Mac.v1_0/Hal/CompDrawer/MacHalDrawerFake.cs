using MvAssistant.DeviceDrive.KjMachineDrawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MvAssistant.DeviceDrive.KjMachineDrawer.ReplyCode;
using MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment;
using System.Diagnostics;
using MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand;
using MvAssistant.DeviceDrive;
using System.Net;
using MvAssistant.DeviceDrive.KjMachineDrawer.DrawerEventArgs;
using System.Threading;

namespace MvAssistant.Mac.v1_0.Hal.CompDrawer
{
    [Guid("408E693D-EEB2-4458-BB91-FE1D6A9B8AD5")]
    public class MacHalDrawerFake : MacHalComponentBase, IMacHalDrawer
    {


        public MacHalDrawerFake()
        {
            //Fake 預設在正常位置, 不用檢查
            this.SetDrawerWorkState(DrawerWorkState.TrayArriveAtPositionHome);
        }


        MvKjMachineDrawerLddPool LddPool;
        // private bool IsCommandINI = false;
        /// <summary>工作狀態</summary>
        public DrawerWorkState CurrentWorkState { get; private set; }
        /// <summary>設定工作狀態</summary>
        /// <param name="state"></param>
        public void SetDrawerWorkState(DrawerWorkState state)
        {
            CurrentWorkState = state;
        }
        /// <summary>將工作狀態設為 AnyState</summary>
        public void ResetCurrentWorkState()
        {
            CurrentWorkState = DrawerWorkState.AnyState;
        }

        #region Const
        public const string DevConnStr_Ip = "ip";
        public const string DevConnStr_Port = "port";

        public const string DevConnStr_LocalIp = "local_ip";
        public const string DevConnStr_LocalPort = "local_port";

        public const string DevConnStr_StartPort = "start_port";
        public const string DevConnStr_EndPort = "end_port";

        public const string DevConnStr_Index = "index";

        public event EventHandler OnBoxDetectionResultHandler;
        #endregion


        public string DeviceIndex
        {
            get
            {
                return this.DevSettings["index"];
            }
        }
        public override bool HalIsConnected()
        {
            /* real
            if (LddPool == null) { return false; }
            if (Ldd == null) { return false; }
            return true;
            */

            #region fake
            Sleep1Sec();
            return true;
            #endregion
        }
        public MvKjMachineDrawerLdd Ldd { get; set; }


        /// <summary>Host 對Drawer硬體 發送指令及監聽一般事件的 Port(Host上的Port) 範圍(起始) </summary>
        public int HostListenDrawerPortRangeStart
        {
            get
            {

                return Convert.ToInt32(this.DevSettings["startport"]);
            }
        }
        /// <summary>Host 對Drawer硬體 發送指令及監聽一般事件的 Port(Host上的Port) 範圍(結束) </summary>
        public int HostListenDrawerPortRangeEnd
        {
            get
            {
                return Convert.ToInt32(this.DevSettings["endport"]);
            }
        }

        /// <summary>Host 監聽Drawer 系統事件的 port(Host 上的)</summary>
        public int HostListenDrawerSysEventPort
        {
            get
            {
                return Convert.ToInt32(this.DevSettings["local_port"]);
            }
        }

        /// <summary>硬體裝置 的IP </summary>
        public string DeviceIP
        {
            get
            {
                return this.DevSettings["ip"];
            }
        }
        /// <summary>硬體裝置的 Listen Port</summary>
        public int DevicePort
        {
            get
            {
                return Convert.ToInt32(this.DevSettings["port"]);
            }
        }

        /// <summary>硬體裝置的 Listen Port</summary>
        public IPEndPoint DeviceEndPoint
        {
            get
            {
                return new IPEndPoint(IPAddress.Parse(DeviceIP), DevicePort);
            }
        }

        public string HostIP
        {
            get
            {
                return this.DevSettings["local_ip"];
            }
        }

        public override int HalConnect()
        {  // LddPool
           /** real
           var connected = false;
           try
           {

               LddPool = MvKjMachineDrawerLddPool.GetInstance(HostListenDrawerPortRangeStart,HostListenDrawerPortRangeEnd, HostListenDrawerSysEventPort);
               if (LddPool == null)
               {
                   connected = false;

               }
               else
               {
                   Ldd = LddPool.CreateLdd(DeviceIndex, DeviceEndPoint, HostIP);
               }
               if (Ldd == null || LddPool==null)
               {
                   connected = false;
               }
               else
               {
                   BindLddEvent();
                   
                   connected = true;
               }

           }
           catch (Exception ex)
           {
               connected = false;
           }
           return connected ? 1:0;
        */
            #region  Fake
            LddPool = MvKjMachineDrawerLddPool.GetFakeInstance(HostListenDrawerPortRangeStart, HostListenDrawerPortRangeEnd, HostListenDrawerSysEventPort);
            Ldd = LddPool.CreateFakeLdd(DeviceIndex, DeviceEndPoint, HostIP);
            Debug.WriteLine("[Fake] Drawer HalConnect(); DeviceIndex=" + DeviceIndex + ", HostIP=" + HostIP + ", DeviceEndPoint=" + DeviceEndPoint.Address + ":" + DeviceEndPoint.Port);

            return 1;
            #endregion
        }

        public override int HalClose()
        {
            //throw new NotImplementedException();
            return 0;
        }
        public object Tag { get; set; }
        public Action PressButtonToLoad { get; set; }

        //public string Index { get; set; }
        public void BindResult()
        {
            Ldd.BoxDetectionResult += this.BoxDetectionResult;

            Ldd.BrightLEDResult = this.BrightLEDResult;
            Ldd.INIResult += this.INIResult;
            Ldd.PositionReadResult += this.PositionReadResult;
            Ldd.SetMotionSpeedResult += this.SetMotionSpeedResult;
            Ldd.SetTimeOutResult += this.SetTimeOutResult;
            Ldd.TrayArriveResult += this.TrayArriveResult;

        }


        public void InvokeMethod(string rtnMsgCascade)
        {
            Debug.WriteLine(rtnMsgCascade);
            string[] rtnMsgArray = rtnMsgCascade.Replace("\0", "").Split(new string[] { BaseCommand.CommandPostfixText }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var rtnMsg in rtnMsgArray)
            {
                var msg = rtnMsg.Replace(BaseCommand.CommandPrefixText, "");
                var msgArray = msg.Split(new string[] { BaseCommand.CommandSplitSign }, StringSplitOptions.RemoveEmptyEntries);
                ReplyMessage rplyMsg = new ReplyMessage
                {
                    StringCode = msgArray[0],
                    StringFunc = msgArray[1],
                    Value = msgArray.Length == 3 ? Convert.ToInt32(msgArray[2]) : default(int?)
                };
                // 取得要呼叫方法名稱
                var method = this.GetType().GetMethod(rplyMsg.StringFunc);
                if (method != null)
                {
                    // 呼叫方法
                    method.Invoke(this, new object[] { rplyMsg });
                }
            }
        }

        public event EventHandler OnTrayMotionFailedHandler;
        public event EventHandler OnTrayMotionOKHandler;
        public event EventHandler OnSetMotionSpeedFailedHandler;
        public event EventHandler OnSetMotionSpeedOKHandler;
        public event EventHandler OnSetTimeOutOKHandler;
        public event EventHandler OnSetTimeOutFailedHandler;

        public event EventHandler OnTrayArriveHomeHandler;
        public event EventHandler OnTrayArriveInHandler;
        public event EventHandler OnTrayArriveOutHandler;
        public event EventHandler OnTrayMotioningHandler;
        public event EventHandler OnPositionStatusHandler;


        public event EventHandler OnDetectedHasBoxHandler;
        public event EventHandler OnDetectedEmptyBoxHandler;
        public event EventHandler OnTrayMotionSensorOFFHandler;
        public event EventHandler OnERRORREcoveryHandler;
        public event EventHandler OnERRORErrorHandler;

        public event EventHandler OnSysStartUpHandler;
        public event EventHandler OnButtonEventHandler;

        public event EventHandler OnBrightLEDOKHandler;
        public event EventHandler OnBrightLEDFailedHandler;

        public event EventHandler OnLCDCMsgOKHandler;
        public event EventHandler OnLCDCMsgFailedHandler;

        public event EventHandler OnINIFailedHandler;
        public event EventHandler OnINIOKHandler;

        void BindLddEvent()
        {
            Ldd.OnTrayMotionFailedHandler += OnTrayMotionFailed;
            Ldd.OnTrayMotionOKHandler += OnTrayMotionOK;
            Ldd.OnSetMotionSpeedFailedHandler += OnSetMotionSpeedFailed;
            Ldd.OnSetMotionSpeedOKHandler += OnSetMotionSpeedOK;

            Ldd.OnSetTimeOutOKHandler += OnSetTimeOutOK;
            Ldd.OnSetTimeOutFailedHandler += OnSetTimeOutFailed;

            Ldd.OnTrayArriveHomeHandler += OnTrayArriveHome;
            Ldd.OnTrayArriveOutHandler += OnTrayArriveOut;
            Ldd.OnTrayArriveInHandler += OnTrayArriveIn;
            Ldd.OnTrayMotioningHandler += OnTrayMotioning;
            Ldd.OnPositionStatusHandler += OnPositionStatus;
            Ldd.OnDetectedHasBoxHandler += OnDetectedHasBox;
            Ldd.OnDetectedEmptyBoxHandler += OnDetectedEmptyBox;
            Ldd.OnTrayMotionSensorOFFHandler += OnTrayMothingSensorOFF;

            Ldd.OnERRORErrorHandler += OnERRORError;
            Ldd.OnERRORRecoveryHandler += OnERRORREcovery;

            Ldd.OnBrightLEDFailedHandler += this.OnBrightLEDFailed;
            Ldd.OnBrightLEDOKHandler += this.OnBrightLEDOK;

            Ldd.OnLCDCMsgOKHandler += this.OnLCDCMsgOK;
            Ldd.OnLCDCMsgFailedHandler += this.OnLCDCMsgFailed;
            Ldd.OnINIFailedHandler += OnINIFailed;

            Ldd.OnButtonEventHandler += OnButtonEvent;
            Ldd.OnSysStartUpHandler += OnSysStartUp;
        }
        #region   Event


        private void OnButtonEvent(object sender, EventArgs e)
        {
            if (PressButtonToLoad != null)
            {
                PressButtonToLoad.Invoke();
            }

            if (OnButtonEventHandler != null)
            {
                OnButtonEventHandler.Invoke(this, e);
            }
        }
        private void OnSysStartUp(object sender, EventArgs e)
        {
            if (OnSysStartUpHandler != null)
            {
                OnSysStartUpHandler.Invoke(this, e);
            }
        }

        private void OnINIFailed(object sender, EventArgs e)
        {
            // Sleep100msecs();
            this.SetDrawerWorkState(DrawerWorkState.InitialFailed);
            if (OnINIFailedHandler != null)
            {
                OnINIFailedHandler.Invoke(this, e);
            }
        }


        private void OnLCDCMsgOK(object sender, EventArgs e)
        {
            if (OnLCDCMsgOKHandler != null)
            {
                OnLCDCMsgOKHandler.Invoke(this, e);
            }
        }
        private void OnLCDCMsgFailed(object sender, EventArgs e)
        {
            if (OnLCDCMsgFailedHandler != null)
            {
                OnLCDCMsgFailedHandler.Invoke(this, e);
            }

        }


        private void OnBrightLEDOK(object sender, EventArgs e)
        {
            if (OnBrightLEDOKHandler != null)
            {
                OnBrightLEDOKHandler.Invoke(this, e);
            }
        }
        private void OnBrightLEDFailed(object sender, EventArgs e)
        {
            if (OnBrightLEDFailedHandler != null)
            {
                OnBrightLEDFailedHandler.Invoke(this, e);
            }
        }

        /// <summary> OnTrayMotionFailedHandler</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTrayMotionFailed(object sender, EventArgs e)
        {

            this.SetDrawerWorkState(DrawerWorkState.TrayMotionFailed);
            if (OnTrayMotionFailedHandler != null)
            {
                OnTrayMotionFailedHandler.Invoke(this, e);
            }
        }

        /// <summary>OnTrayMotionOKHandler</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSetMotionSpeedOK(Object sender, EventArgs e)
        {
            if (OnSetMotionSpeedOKHandler != null)
            {
                OnSetMotionSpeedOKHandler.Invoke(this, e);
            }
        }
        /// <summary>OnSetMotionSpeedFailedHandler</summary>
        /// <param name="sender"></param>
        /// <param name="e">OnSetMotionSpeedOKHandler</param>
        private void OnSetMotionSpeedFailed(Object sender, EventArgs e)
        {
            if (OnSetMotionSpeedFailedHandler != null)
            {
                OnSetMotionSpeedFailedHandler.Invoke(this, e);
            }
        }

        /// <summary>OnSetTimeOutOKHandler</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSetTimeOutOK(Object sender, EventArgs e)
        {
            if (OnSetTimeOutOKHandler != null)
            {
                OnSetTimeOutOKHandler.Invoke(this, e);
            }
        }
        /// <summary>OnSetTimeOutFailedHandler</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSetTimeOutFailed(Object sender, EventArgs e)
        {


            if (OnSetTimeOutFailedHandler != null)
            {
                OnSetTimeOutFailedHandler.Invoke(this, e);
            }
        }

        /// <summary>OnTrayArriveHomeHandler</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTrayArriveHome(object sender, EventArgs e)
        {
            //Sleep100msecs();
            this.SetDrawerWorkState(DrawerWorkState.TrayArriveAtPositionHome);

            if (OnTrayArriveHomeHandler != null)
            {
                OnTrayArriveHomeHandler.Invoke(this, e);
            }

        }
        /// <summary>OnTrayArriveOutHandler</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTrayArriveOut(object sender, EventArgs e)
        {
            //Sleep100msecs();
            this.SetDrawerWorkState(DrawerWorkState.TrayArriveAtPositionOut);
            if (OnTrayArriveOutHandler != null)
            {
                OnTrayArriveOutHandler.Invoke(this, e);
            }
        }
        /// <summary>OnTrayArriveInHandler</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTrayArriveIn(object sender, EventArgs e)
        {
            //Sleep100msecs();
            this.SetDrawerWorkState(DrawerWorkState.TrayArriveAtPositionIn);
            if (OnTrayArriveInHandler != null)
            {
                OnTrayArriveInHandler.Invoke(this, e);
            }
        }

        /// <summary>OnTrayMotioningHandler</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTrayMotioning(object sender, EventArgs e)
        {
            if (OnTrayMotioningHandler != null)
            {
                OnTrayMotioningHandler.Invoke(this, e);
            }
        }

        /// <summary>OnPositionStatusHandler </summary>
        /// <param name="sender"></param>
        /// <param name="e">typeof(OnReplyPositionEventArgs)</param>
        private void OnPositionStatus(object sender, EventArgs e)
        {
            var args = (OnReplyPositionEventArgs)e;
            if (OnPositionStatusHandler != null)
            {
                OnPositionStatusHandler.Invoke(this, e);
            }
        }

        /// <summary>OnDetectedHasBoxHandler</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDetectedHasBox(object sender, EventArgs e)
        {
            SetDrawerWorkState(DrawerWorkState.BoxExist);
            if (OnDetectedHasBoxHandler != null)
            {
                OnDetectedHasBoxHandler.Invoke(this, e);
            }

        }
        /// <summary>OnDetectedHasBoxHandler</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDetectedEmptyBox(object sender, EventArgs e)
        {
            SetDrawerWorkState(DrawerWorkState.BoxNotExist);
            if (OnDetectedEmptyBoxHandler != null)
            {
                OnDetectedEmptyBoxHandler.Invoke(this, e);
            }
        }

        /// <summary>OnTrayMotionOKHandler</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTrayMotionOK(object sender, EventArgs e)
        {
            if (OnTrayMotionOKHandler != null)
            {
                OnTrayMotionOKHandler.Invoke(this, e);
            }
        }


        /// <summary>OnTrayMothingSensorOFFHandler</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTrayMothingSensorOFF(object sender, EventArgs e)
        {
            if (OnTrayMotionSensorOFFHandler != null)
            {
                OnTrayMotionSensorOFFHandler.Invoke(this, e);
            }
        }

        /// <summary></summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnERRORREcovery(object sender, EventArgs e)
        {
            if (OnERRORREcoveryHandler != null)
            {
                OnERRORREcoveryHandler.Invoke(this, e);
            }
        }

        /// <summary></summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnERRORError(object sender, EventArgs e)
        {
            if (OnERRORErrorHandler != null)
            {
                OnERRORErrorHandler.Invoke(this, e);
            }
        }


        #endregion

        #region command
        private void Sleep100msecs()
        {
            Thread.Sleep(100);
        }

        public string CommandINI()
        {

            /** real
            this.SetDrawerWorkState(DrawerWorkState.InitialIng);
            var commandText = Ldd.CommandINI();

            return commandText;
           */
            #region fake

            this.SetDrawerWorkState(DrawerWorkState.InitialIng);
            //var commandText = Ldd.FakeCommandINI();
            var commandText = "Fake Test CommandINI";
            Debug.WriteLine("[Fake] Drawer; DeviceIndex=" + DeviceIndex + ", HostIP=" + HostIP + ", DeviceEndPoint=" + DeviceEndPoint.Address + ":" + DeviceEndPoint.Port + "\r\n[Fake] Drawer; Command Name=CommandINI(), Command Text=" + commandText);
            //Debug.WriteLine("[Fake] Drawer; Command Name=CommandINI(), Command Text=" + commandText );
            new Task(
                () =>
                {
                    FakeSleep();
                    this.SetDrawerWorkState(DrawerWorkState.TrayArriveAtPositionHome);
                    Debug.WriteLine("[Fake] Drawer; DeviceIndex=" + DeviceIndex + ", HostIP=" + HostIP + ", DeviceEndPoint=" + DeviceEndPoint.Address + ":" + DeviceEndPoint.Port + "\r\n[Fake] Drawer; State=" + DrawerWorkState.TrayArriveAtPositionHome.ToString());
                    // Debug.WriteLine("[Fake] Drawer; State=" + DrawerWorkState.TrayArriveAtPositionHome.ToString());
                    OnTrayArriveHome(this,null);
                }
                ).Start();
            return commandText;
            #endregion
        }

        public string CommandSetMotionSpeed(int speed)
        {

            var commandText = Ldd.CommandSetMotionSpeed(speed);
            return commandText;
        }

        public string CommandSetTimeOut(int timeoutSeconds)
        {

            var commandText = Ldd.CommandSetTimeOut(timeoutSeconds);
            return commandText;
        }

        public string CommandTrayMotionHome()
        {
            //ResetCurrentWorkState();
            /** real 
            this.SetDrawerWorkState(DrawerWorkState.MoveTrayToPositionHomeIng);
            var commandText = Ldd.CommandTrayMotionHome();
           
            return commandText;
           */

            #region Fake
            this.SetDrawerWorkState(DrawerWorkState.MoveTrayToPositionHomeIng);
            // var commandText = Ldd.FakeCommandTrayMotionHome();
            var commandText = "Fake Test: CommandTrayMotionHome()";

            Debug.WriteLine("[Fake] Drawer; DeviceIndex=" + DeviceIndex + ", HostIP=" + HostIP + ", DeviceEndPoint=" + DeviceEndPoint.Address + ":" + DeviceEndPoint.Port + "\r\n[Fake] Drawer; Command Name=CommandTrayMotionHome(), Command Text=" + commandText);
            //Debug.WriteLine("[Fake] Drawer; Command Name=CommandTrayMotionHome(), Command Text=" + commandText);
            new Task(
                () =>
                {
                    FakeSleep();
                    this.SetDrawerWorkState(DrawerWorkState.TrayArriveAtPositionHome);
                    Debug.WriteLine("[Fake] Drawer; DeviceIndex=" + DeviceIndex + ", HostIP=" + HostIP + ", DeviceEndPoint=" + DeviceEndPoint.Address + ":" + DeviceEndPoint.Port + "\r\n[Fake] Drawer; State=" + DrawerWorkState.TrayArriveAtPositionHome.ToString());
                    //Debug.WriteLine("[Fake] Drawer; State=" + DrawerWorkState.TrayArriveAtPositionHome.ToString());
                    OnTrayArriveHome(this, null);
                }
                ).Start();
            return commandText;
            #endregion
        }

        private void SetDrawerWorkState(object moveTrayToPositionHomeIng)
        {
            throw new NotImplementedException();
        }

        public string CommandTrayMotionOut()
        {
            //ResetCurrentWorkState();
            /** real
            this.SetDrawerWorkState(DrawerWorkState.MoveTrayToPositionOutIng);
            var commandText = Ldd.CommandTrayMotionOut();
            return commandText;
            */

            #region Fake
            this.SetDrawerWorkState(DrawerWorkState.MoveTrayToPositionOutIng);
            // var commandText = Ldd.FakeCommandTrayMotionOut();
            var commandText = "Fake Test: CommandTrayMotionOut()";
            Debug.WriteLine("[Fake] Drawer; DeviceIndex=" + DeviceIndex + ", HostIP=" + HostIP + ", DeviceEndPoint=" + DeviceEndPoint.Address + ":" + DeviceEndPoint.Port + "\r\n[Fake] Drawer; Command Name=CommandTrayMotionOut(), Command Text=" + commandText);
            // Debug.WriteLine("[Fake] Drawer; Command Name=CommandTrayMotionOut(), Command Text=" + commandText);
            new Task(
                () =>
                {
                    FakeSleep();
                    this.SetDrawerWorkState(DrawerWorkState.TrayArriveAtPositionOut);
                    Debug.WriteLine("[Fake] Drawer; DeviceIndex=" + DeviceIndex + ", HostIP=" + HostIP + ", DeviceEndPoint=" + DeviceEndPoint.Address + ":" + DeviceEndPoint.Port + "\r\n[Fake] Drawer; State=" + DrawerWorkState.TrayArriveAtPositionOut.ToString());
                    // Debug.WriteLine("[Fake] Drawer; State=" + DrawerWorkState.TrayArriveAtPositionOut.ToString());
                    OnTrayArriveOut(this,null);
                }
                ).Start();

            return commandText;
            #endregion
        }

        public string CommandTrayMotionIn()
        {
            // ResetCurrentWorkState();

            /** 
            this.SetDrawerWorkState(DrawerWorkState.MoveTrayToPositionInIng);
            var commandText = Ldd.CommandTrayMotionIn();
           
            return commandText;
          */

            #region Fake
            this.SetDrawerWorkState(DrawerWorkState.MoveTrayToPositionInIng);
            //var commandText = Ldd.FakeCommandTrayMotionIn();
            var commandText = "Fake Test: CommandTrayMotionIn()";
            Debug.WriteLine("[Fake] Drawer; DeviceIndex=" + DeviceIndex + ", HostIP=" + HostIP + ", DeviceEndPoint=" + DeviceEndPoint.Address + ":" + DeviceEndPoint.Port + "\r\n[Fake] Drawer; Command Name=CommandTrayMotionIn(), Command Text=" + commandText);
            //Debug.WriteLine("[Fake] Drawer; Command Name=CommandTrayMotionIn(), Command Text=" + commandText);
            new Task(
              () =>
              {
                  FakeSleep();
                  this.SetDrawerWorkState(DrawerWorkState.TrayArriveAtPositionIn);
                  Debug.WriteLine("[Fake] Drawer; DeviceIndex=" + DeviceIndex + ", HostIP=" + HostIP + ", DeviceEndPoint=" + DeviceEndPoint.Address + ":" + DeviceEndPoint.Port + "\r\n[Fake] Drawer; State=" + DrawerWorkState.TrayArriveAtPositionIn.ToString());
                  // Debug.WriteLine("[Fake] Drawer; State=" + DrawerWorkState.TrayArriveAtPositionIn.ToString());
              }
              ).Start();

            return commandText;
            #endregion 
        }



        public string CommandBrightLEDAllOn()
        {

            var commandText = Ldd.CommandBrightLEDAllOn();
            return commandText;
        }

        public string CommandBrightLEDAllOff()
        {

            var commandText = Ldd.CommandBrightLEDAllOff();
            return commandText;
        }

        public string CommandBrightLEDGreenOn()
        {

            var commandText = Ldd.CommandBrightLEDGreenOn();
            return commandText;
        }

        public string CommandBrightLEDRedOn()
        {

            var commandText = Ldd.CommandBrightLEDRedOn();
            return commandText;
        }

        public string CommandPositionRead()
        {

            var commandText = Ldd.CommandPositionRead();
            return commandText;
        }

        public string CommandBoxDetection()
        {
            /** real
            ResetCurrentWorkState();
            var commandText = Ldd.CommandBoxDetection();
            return commandText;
           */

            #region Fake
            ResetCurrentWorkState();
            //var commandText = Ldd.FakeCommandBoxDetection();
            var commandText = "Fake Test: CommandBoxDetection()";
            Debug.WriteLine("[Fake] Drawer; DeviceIndex=" + DeviceIndex + ", HostIP=" + HostIP + ", DeviceEndPoint=" + DeviceEndPoint.Address + ":" + DeviceEndPoint.Port + "\r\n[Fake] Drawer; Command Name=CommandBoxDetection(), Command Text=" + commandText);
            //  Debug.WriteLine("[Fake] Drawer; Command Name=CommandBoxDetection(), Command Text=" + commandText);
            new Task(
              () =>
              {
                  FakeSleep();


                  /** 有盒子
                  this.SetDrawerWorkState(DrawerWorkState.BoxExist);
                  Debug.WriteLine("[Fake] Drawer; DeviceIndex=" + DeviceIndex + ", HostIP=" + HostIP + ", DeviceEndPoint=" + DeviceEndPoint.Address + ":" + DeviceEndPoint.Port + "\r\n[Fake] Drawer; State = " + DrawerWorkState.BoxExist.ToString());
                  //Debug.WriteLine("[Fake] Drawer; State=" + DrawerWorkState.BoxExist.ToString());
                  */

                  /** 没盒子
                  this.SetDrawerWorkState(DrawerWorkState.BoxNotExist);
                  Debug.WriteLine("[Fake] Drawer; DeviceIndex=" + DeviceIndex + ", HostIP=" + HostIP + ", DeviceEndPoint=" + DeviceEndPoint.Address + ":" + DeviceEndPoint.Port + "\r\n[Fake] Drawer; State = " + DrawerWorkState.BoxNotExist.ToString());
                  //Debug.WriteLine("[Fake] Drawer; State=" + DrawerWorkState.BoxNotExist.ToString());
            */
              }
              ).Start();
            return commandText;
            #endregion

        }

        public string CommandWriteNetSetting()
        {

            var commandText = Ldd.CommandWriteNetSetting();
            return commandText;
        }

        public string CommandLCDMsg(string message)
        {

            var commandText = Ldd.CommandLCDMsg(message);
            return commandText;
        }

        public string CommandSetParameterHomePosition(string homePosition)
        {

            var commandText = Ldd.CommandSetParameterHomePosition(homePosition);
            return commandText;
        }

        public string CommandSetParameterOutSidePosition(string outsidePosition)
        {

            var commandText = Ldd.CommandSetParameterOutSidePosition(outsidePosition);
            return commandText;
        }

        public string CommandSetParameterInSidePosition(string insidePosition)
        {

            var commandText = Ldd.CommandSetParameterInSidePosition(insidePosition);
            return commandText;
        }

        public string CommandSetParameterIPAddress(string ipAddress)
        {

            var commandText = Ldd.CommandSetParameterIPAddress(ipAddress);
            return commandText;
        }

        public string CommandSetParameterSubMask(string submaskAddress)
        {

            var commandText = Ldd.CommandSetParameterSubMask(submaskAddress);
            return commandText;
        }
        #endregion
        #region Result
        public void TrayArriveResult(object sender, int result)
        {
            var arriveType = (TrayArriveType)result;

            // vs 2013
            // if (this.Tag.ToString() == nameof(CommandINI))
            if (this.Tag.ToString() == "CommandINI")
            {   // 如果當時是發 initial 指令, 視為 初始化成功
                if (arriveType == TrayArriveType.ArriveHome)
                {
                    this.INIResult(sender, true);
                }
            }
            else
            {
                var ldd = (MvKjMachineDrawerLdd)sender;
                if (arriveType == TrayArriveType.ArriveHome)
                { // 回到 Home

                    DebugLog(ldd, "已經回到 Home");
                }
                else if (arriveType == TrayArriveType.ArriveIn)
                { //  回到 In
                    DebugLog(ldd, "已經回到 In");
                }
                else// if (arriveType == TrayArriveType.ArriveOut)
                {  // 回到 Out
                    DebugLog(ldd, "已經回到 Out");
                }
            }
        }



        public void INIResult(object sender, bool result)
        {
            var ldd = (MvKjMachineDrawerLdd)sender;
            if (result)
            {  // 初始化成功
                DebugLog(ldd, "初始化成功");
            }
            else
            { // 初始化失敗
                DebugLog(ldd, "初始化失敗");
            }
        }

        public void SetMotionSpeedResult(object sender, bool result)
        {
            var ldd = (MvKjMachineDrawerLdd)sender;
            if (result)
            {  // 速度設定成功
                DebugLog(ldd, "設定速度成功");
            }
            else
            {  // 速度設定失敗
                DebugLog(ldd, "設定速度失敗");
            }
        }

        public void SetTimeOutResult(object sender, bool result)
        {
            var ldd = (MvKjMachineDrawerLdd)sender;
            if (result)
            { // 逾時時間設定成功
                DebugLog(ldd, "設定Time Out成功");
            }
            else
            {
                DebugLog(ldd, "設定Time Out 失敗");
            }
        }



        public void BrightLEDResult(object sender, bool result)
        {
            MvKjMachineDrawerLdd ldd = (MvKjMachineDrawerLdd)sender;
            var command = this.Tag.ToString();
            if (result)
            {    // 成功

                // vs 2013
                // if(command == nameof(CommandBrightLEDAllOff))
                if (command == "CommandBrightLEDAllOff")
                { // 關掉所有的 led 
                    DebugLog(ldd, "所有 LED off OK");
                }
                // vs 2013
                //else if (command ==nameof(CommandBrightLEDAllOn))
                else if (command == "CommandBrightLEDAllOn")
                {// 打亮所有的led
                    DebugLog(ldd, "所有 LED On OK");
                }
                // vs 2013
                // else if (command == nameof(CommandBrightLEDGreenOn))
                else if (command == "CommandBrightLEDGreenOn")
                {  // 打亮綠色LED
                    DebugLog(ldd, "綠色 LED On OK");
                }
                else
                {   // 打亮 紅色LED
                    DebugLog(ldd, "紅色 LED On OK");
                }
            }
            else // 失敗
            {
                // vs 2013
                //if (command == nameof(CommandBrightLEDAllOff))
                if (command == "CommandBrightLEDAllOff")
                { // 關掉所有的 led 
                    DebugLog(ldd, "所有 LED off Fail");
                }
                // vs 2013
                // else if (command == nameof(CommandBrightLEDAllOn))
                else if (command == "CommandBrightLEDAllOn")
                {// 打亮所有的led
                    DebugLog(ldd, "所有 LED On Fail");
                }
                // vs 2013
                // else if (command == nameof(CommandBrightLEDGreenOn))
                else if (command == "CommandBrightLEDGreenOn")
                {  // 打亮綠色LED
                    DebugLog(ldd, "綠色 LED On Fail");
                }
                else
                {   // 打亮 紅色LED
                    DebugLog(ldd, "紅色 LED On Fail");
                }
            }
        }


        public void PositionReadResult(object sender, string result)
        {
            MvKjMachineDrawerLdd ldd = (MvKjMachineDrawerLdd)sender;
            if (string.IsNullOrEmpty(result))
            {
                DebugLog(ldd, " PositionRead Error");
            }
            else
            {// result 為IOH
                // vs 2013
                //DebugLog(ldd, $"IOH={result}");
                DebugLog(ldd, "IOH=" + result);
            }
        }

        public void BoxDetectionResult(object sender, bool result)
        {
            MvKjMachineDrawerLdd ldd = (MvKjMachineDrawerLdd)sender;
            if (result)
            {  // 有盒子
                DebugLog(ldd, "有盒子");

            }
            else
            {
                DebugLog(ldd, "没有盒子");
            }
            if (OnBoxDetectionResultHandler != null)
            {
                OnBoxDetectionResultHandler.Invoke(this, new HalDrawerBoxDetectReturn { HasBox = result });
            }
        }



        public void ErrorResult(object sender, int result)
        {
            MvKjMachineDrawerLdd ldd = (MvKjMachineDrawerLdd)sender;
            var errorResult = (ReplyErrorCode)result;
            if (errorResult == ReplyErrorCode.Error)
            { // Error

                DebugLog(ldd, "Error");
            }
            else //if (errorResult == ReplyErrorCode.Recovery)
            {// Recovery
                DebugLog(ldd, "Recovery");
            }
        }

        #endregion
        void DebugLog(MvKjMachineDrawerLdd ldd, string text)
        {
            // vs 2013
            // string str = $"Ldd={ldd.DeviceIP}, Text={text}";
            string str = "Ldd=" + ldd.DeviceIP + ", Text=" + text;
            Debug.WriteLine("\r\n" + str);
        }

        /// <summary>休息 500 毫秒</summary>
        void Sleep500msecs()
        {
            for (var i = 1; i <= 5; i++)
            {
                Sleep100msecs();
            }
        }

        /// <summary>休息 1 秒</summary>
        void Sleep1Sec()
        {
            for (var i = 1; i <= 2; i++)
            {
                Sleep500msecs();
            }
        }

        /// <summary>休息 2 秒</summary>
        void FakeSleep()
        {
            Sleep1Sec();
            Sleep1Sec();
        }

        /// <summary>秒息 1000毫秒</summary>
        void Sleep1000msecs()
        {
            Sleep1Sec();
        }

    }
}
