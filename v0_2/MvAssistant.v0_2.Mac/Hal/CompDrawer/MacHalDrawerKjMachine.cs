using MvAssistant.v0_2.DeviceDrive.KjMachineDrawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MvAssistant.v0_2.DeviceDrive.KjMachineDrawer.ReplyCode;
using MvAssistant.v0_2.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment;
using System.Diagnostics;
using MvAssistant.v0_2.DeviceDrive.KjMachineDrawer.UDPCommand;
using MvAssistant.v0_2.DeviceDrive;
using System.Net;
using MvAssistant.v0_2.DeviceDrive.KjMachineDrawer.DrawerEventArgs;
using System.Threading;

namespace MvAssistant.v0_2.Mac.Hal.CompDrawer
{
    // [Guid("D0F66AC7-5CD9-42FB-8B05-AAA31C647979")]
    [Guid("AE0A6C92-6B34-495A-B591-93AE4DF65976")]
    public class MacHalDrawerKjMachine : MacHalComponentBase, IMacHalDrawer
    {

        public bool IsInitialing { get; set; }
        MvaKjMachineDrawerLddPool LddPool;
        // private bool IsCommandINI = false;
        /// <summary>工作狀態</summary>
        public DrawerWorkState CurrentWorkState { get; private set; }
        /// <summary>設定工作狀態</summary>
        /// <param name="state"></param>
        public void SetDrawerWorkState(DrawerWorkState state)
        {
            //此狀態非State Machine使用, Drawer本身不知道自己的狀態
            //需要由Drawer軟體內部自己要記錄
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
                return this.DevSettings[DevConnStr_Index];
            }
        }
        public override bool HalIsConnected()
        {
            if (LddPool == null) { return false; }
            if (Ldd == null) { return false; }

            return true;
        }
        public MvaKjMachineDrawerLdd Ldd { get; set; }


        /// <summary>Host 對Drawer硬體 發送指令及監聽一般事件的 Port(Host上的Port) 範圍(起始) </summary>
        public int HostListenDrawerPortRangeStart
        {
            get
            {

                return Convert.ToInt32(this.DevSettings[DevConnStr_StartPort]);
            }
        }
        /// <summary>Host 對Drawer硬體 發送指令及監聽一般事件的 Port(Host上的Port) 範圍(結束) </summary>
        public int HostListenDrawerPortRangeEnd
        {
            get
            {
                return Convert.ToInt32(this.DevSettings[DevConnStr_EndPort]);
            }
        }

        /// <summary>Host 監聽Drawer 系統事件的 port(Host 上的)</summary>
        public int HostListenDrawerSysEventPort
        {
            get
            {
                return Convert.ToInt32(this.DevSettings[DevConnStr_LocalPort]);
            }
        }

        /// <summary>硬體裝置 的IP </summary>
        public string DeviceIP
        {
            get
            {
                return this.DevSettings[DevConnStr_Ip];
            }
        }
        /// <summary>硬體裝置的 Listen Port</summary>
        public int DevicePort
        {
            get
            {
                return Convert.ToInt32(this.DevSettings[DevConnStr_Port]);
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

                return this.DevSettings[DevConnStr_LocalIp];
            }
        }

        public override int HalConnect()
        {  // LddPool
            var connected = false;
            try
            {

                LddPool = MvaKjMachineDrawerLddPool.GetInstance(HostListenDrawerPortRangeStart, HostListenDrawerPortRangeEnd, HostListenDrawerSysEventPort);
                if (LddPool == null)
                {
                    connected = false;

                }
                else
                {

                    Ldd = LddPool.CreateLdd(DeviceId, DeviceEndPoint, HostIP);
                }
                if (Ldd == null || LddPool == null)
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
                MvaLog.WarnNs(this, ex);
            }
            return connected ? 1 : 0;
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
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnButtonEvent");
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
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnSysStartUp");
            if (OnSysStartUpHandler != null)
            {
                OnSysStartUpHandler.Invoke(this, e);
            }
        }

        private void OnINIFailed(object sender, EventArgs e)
        {
            // Sleep100msecs();
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnINIFailed");
            this.SetDrawerWorkState(DrawerWorkState.InitialFailed);
            if (OnINIFailedHandler != null)
            {
                OnINIFailedHandler.Invoke(this, e);
            }
        }


        private void OnLCDCMsgOK(object sender, EventArgs e)
        {
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnLCDCMsgOK");
            if (OnLCDCMsgOKHandler != null)
            {
                OnLCDCMsgOKHandler.Invoke(this, e);
            }
        }
        private void OnLCDCMsgFailed(object sender, EventArgs e)
        {
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnLCDCMsgFailed");
            if (OnLCDCMsgFailedHandler != null)
            {
                OnLCDCMsgFailedHandler.Invoke(this, e);
            }

        }


        private void OnBrightLEDOK(object sender, EventArgs e)
        {
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnBrightLEDOK");
            if (OnBrightLEDOKHandler != null)
            {
                OnBrightLEDOKHandler.Invoke(this, e);
            }
        }
        private void OnBrightLEDFailed(object sender, EventArgs e)
        {
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnBrightLEDFailed");
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
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnTrayMotionFailed");
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
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnSetMotionSpeedOK");
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
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnSetMotionSpeedFailed");
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
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnSetTimeOutOK");
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

            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnSetTimeOutFailed");
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
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnTrayArriveHome");
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
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnTrayArriveOut");
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
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnTrayArriveIn");
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
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnTrayMotioning");
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
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnPositionStatus");
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
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnDetectedHasBox");
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
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnDetectedEmptyBox");
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
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnTrayMotionOK");
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
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnTrayMothingSensorOFF");
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
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnERRORREcovery");
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
            Debug.WriteLine("Drawer IP=" + this.DeviceIP + ", Event=" + "OnERRORError");
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

            this.SetDrawerWorkState(DrawerWorkState.InitialIng);
            var commandText = Ldd.CommandINI();

            return commandText;
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
            this.SetDrawerWorkState(DrawerWorkState.MoveTrayToPositionHomeIng);
            var commandText = Ldd.CommandTrayMotionHome();

            return commandText;
        }

        private void SetDrawerWorkState(object moveTrayToPositionHomeIng)
        {
            throw new NotImplementedException();
        }

        public string CommandTrayMotionOut()
        {
            //ResetCurrentWorkState();
            this.SetDrawerWorkState(DrawerWorkState.MoveTrayToPositionOutIng);
            var commandText = Ldd.CommandTrayMotionOut();
            return commandText;
        }

        public string CommandTrayMotionIn()
        {
            // ResetCurrentWorkState();
            this.SetDrawerWorkState(DrawerWorkState.MoveTrayToPositionInIng);
            var commandText = Ldd.CommandTrayMotionIn();

            return commandText;
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
            ResetCurrentWorkState();
            var commandText = Ldd.CommandBoxDetection();
            return commandText;
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
                var ldd = (MvaKjMachineDrawerLdd)sender;
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
            var ldd = (MvaKjMachineDrawerLdd)sender;
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
            var ldd = (MvaKjMachineDrawerLdd)sender;
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
            var ldd = (MvaKjMachineDrawerLdd)sender;
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
            MvaKjMachineDrawerLdd ldd = (MvaKjMachineDrawerLdd)sender;
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
            MvaKjMachineDrawerLdd ldd = (MvaKjMachineDrawerLdd)sender;
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
            MvaKjMachineDrawerLdd ldd = (MvaKjMachineDrawerLdd)sender;
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
            MvaKjMachineDrawerLdd ldd = (MvaKjMachineDrawerLdd)sender;
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
        void DebugLog(MvaKjMachineDrawerLdd ldd, string text)
        {
            // vs 2013
            // string str = $"Ldd={ldd.DeviceIP}, Text={text}";
            string str = "Ldd=" + ldd.DeviceIP + ", Text=" + text;
            Debug.WriteLine("\r\n" + str);
        }

    }
}
