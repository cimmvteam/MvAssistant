using MvaCToolkitCs.v1_2;
using MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.DrawerEventArgs;
using MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.Exceptions;
using MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.ReplyCode;
using MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.UDPCommand;
using MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer
{
    /// <summary>Drawer Class</summary>
    public class MvaKjMachineDrawerLdd_v1_0 : IDisposable
    {
        /// <summary>Cabinet 編號</summary>        
        private int CabinetNO { get; set; }

        /// <summary>Drawer 編號</summary>
        [Obsolete]
        private string DrawerNO { get; set; }

        /// <summary>對應的 Drawer Index</summary>
        public string DrawerIndex { get; set; }

        /// <summary>裝置IP</summary>
        public string DeviceIP { get; set; }

        /// <summary>目的端點</summary>
        IPEndPoint TargetEndpoint = null;

        /// <summary>傳送命令/回收訊息的 Socket</summary>
        private Socket UdpSocket = null;

        /// <summary>監聽回復訊息的 Thread</summary>
        private Thread ListenThread;

        /// <summary>建構式/summary>
        private MvaKjMachineDrawerLdd_v1_0() { }

        /// <summary>建構式</summary>
        /// <param name="cabinetNO">Cabinet 編號</param>
        /// <param name="drawerNO">drawer 編號</param>
        /// <param name="deviceEndpoint">drawer 的端點</param>
        /// <param name="localIp">本地IP</param>
        /// <param name="portTable">本地端 Port 使用狀況</param>
        [Obsolete]
        public MvaKjMachineDrawerLdd_v1_0(int cabinetNO, string drawerNO, IPEndPoint deviceEndpoint, string localIp, IDictionary<int, bool?> portTable) : this()
        {
            DrawerNO = drawerNO;
            CabinetNO = cabinetNO;
            DeviceIP = deviceEndpoint.Address.ToString();

            TargetEndpoint = deviceEndpoint;
            UdpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            while (true)
            {
                // 可用的 port
                int variablePort = 0;
                try
                {
                    KeyValuePair<int, bool?> keyValuePair = portTable.Where(m => m.Value == default(bool?)).FirstOrDefault();
                    if (keyValuePair.Equals(default(KeyValuePair<int, bool?>)))
                    { // 無 Port 可用時
                        // TODO : To Thorw an Exception
                    }

                    variablePort = keyValuePair.Key;
                    // Bind 的端點名稱
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(localIp), variablePort);
                    UdpSocket.Bind(endPoint);
                    portTable.Remove(variablePort);
                    portTable.Add(variablePort, true);
                    break;
                }
                catch (Exception ex)
                {
                    portTable.Remove(variablePort);
                    portTable.Add(variablePort, false);
                    CtkLog.WarnAn(this, ex);
                }
            }
            ListenThread = new Thread(Listen);
            ListenThread.IsBackground = true;
            ListenThread.Start();

        }


        /// <summary>Constructor for Fake MvKjMachineDrawerLdd Instance</summary>
        /// <param name="isFakeInstance"></param>
        /// <param name="drawerIndex"></param>
        /// <param name="deviceEndpoint"></param>
        /// <param name="localIp"></param>
        /// <param name="portTable"></param>
        /// <remarks>
        /// <para>2020/10/23 King [C]</para>
        /// <para>保留</para>
        /// </remarks>
        public MvaKjMachineDrawerLdd_v1_0(bool isFakeInstance, string drawerIndex, IPEndPoint deviceEndpoint, string localIp, IDictionary<int, bool?> portTable) : this()
        {
            DrawerIndex = drawerIndex;
            TargetEndpoint = deviceEndpoint;
            DeviceIP = deviceEndpoint.Address.ToString();
            while (true)
            {
                // 可用的 port
                int variablePort = 0;
                KeyValuePair<int, bool?> keyValuePair = portTable.Where(m => m.Value == default(bool?)).FirstOrDefault();
                variablePort = keyValuePair.Key;
                portTable.Remove(variablePort);
                portTable.Add(variablePort, true);
                break;
            }
        }




        /// <summary></summary>
        /// <param name="drawerIndex"></param>
        /// <param name="deviceEndpoint"></param>
        /// <param name="localIp"></param>
        /// <param name="portTable"></param>
        public MvaKjMachineDrawerLdd_v1_0(string drawerIndex, IPEndPoint deviceEndpoint, string localIp, IDictionary<int, bool?> portTable) : this()
        {
            DrawerIndex = drawerIndex;
            TargetEndpoint = deviceEndpoint;
            DeviceIP = deviceEndpoint.Address.ToString();

            UdpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            while (true)
            {
                // 可用的 port
                int variablePort = 0;
                try
                {
                    KeyValuePair<int, bool?> keyValuePair = portTable.Where(m => m.Value == default(bool?)).FirstOrDefault();
                    if (keyValuePair.Equals(default(KeyValuePair<int, bool?>)))
                    { // 無 Port 可用時
                        // TODO : To Thorw an Exception
                        throw new OutOfListenPortsException();
                    }

                    variablePort = keyValuePair.Key;
                    // Bind 的端點名稱
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(localIp), variablePort);
                    UdpSocket.Bind(endPoint);
                    portTable.Remove(variablePort);
                    portTable.Add(variablePort, true);
                    break;
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(OutOfListenPortsException))
                    {
                        throw ex;
                    }
                    else
                    {
                        portTable.Remove(variablePort);
                        portTable.Add(variablePort, false);
                    }
                }
            }
            ListenThread = new Thread(Listen);
            ListenThread.IsBackground = true;
            ListenThread.Start();

        }

        ~MvaKjMachineDrawerLdd_v1_0() { this.Dispose(false); }

        /// <summary>監聽的函式</summary>
        private void Listen()
        {

            while (true)
            {
                try
                {

                    byte[] buffer = new byte[1024];
                    // 監聽點
                    UdpSocket.Receive(buffer);
                    var msg = Encoding.UTF8.GetString(buffer);
                    InvokeMethod(msg);
                }
                catch (Exception)
                {

                }
            }

        }

        /// <summary>傳送</summary>
        /// <param name="commandText">Command 內容</param>
        /// <returns></returns>
        public int Send(string commandText)
        {
            var len = this.UdpSocket.SendTo(Encoding.UTF8.GetBytes(commandText), TargetEndpoint);
            return len;
        }

        /// <summary>監聽到回覆訊息時 呼收訊息同名的函式</summary>
        /// <param name="rtnMsg"></param>
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

        /// <summary>Command INI(099)</summary>
        /// <returns></returns>
        public string CommandINI()
        {
            var tryCounter = 0;
            while (true)
            {
                try
                {
                    tryCounter++;
                    var commandText = new INI().GetCommandText(new INIParameter());
                    UdpSocket.SendTo(Encoding.UTF8.GetBytes(commandText), TargetEndpoint);
                    return commandText;
                }
                catch (Exception)
                {
                    if (tryCounter >= 3)
                    {
                        return string.Empty;
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }

                }

            }
        }

        /**
        /// <summary>Fake Command INI(099)</summary>
        /// <remarks>
        /// 2020/10/23 King [C]
        /// </remarks>
        /// <returns></returns>
        public string FakeCommandINI()
        {
            var commandText = new INI().GetCommandText(new INIParameter());
            return commandText;
        }
        */
        /// <summary>Command SetMotionSpeed(000)</summary>
        /// <param name="speed"></param>
        public string CommandSetMotionSpeed(int speed)
        {
            /**
            if (speed > 100 || speed < 1)
            { throw new MotionSpeedOutOfRangeException(); }
            */
            var parameter = new SetMotionSpeedParameter { Speed = speed };
            var commandText = new SetMotionSpeed().GetCommandText(parameter);
            UdpSocket.SendTo(Encoding.UTF8.GetBytes(commandText), TargetEndpoint);
            return commandText;
        }



        /// <summary>Command SetTimeOut(001)</summary>
        /// <param name="timeoutSeconds"></param>
        public string CommandSetTimeOut(int timeoutSeconds)
        {
            /**
            if (timeoutSeconds < 1 || timeoutSeconds > 100)
            {
                throw new TimeOutSecondOutOfRangeException();
            }
            */
            var parameter = new SetTimeOutParameter { Seconds = timeoutSeconds };
            var commandText = new SetTimeOut().GetCommandText(parameter);
            UdpSocket.SendTo(Encoding.UTF8.GetBytes(commandText), TargetEndpoint);
            return commandText;
        }

        /// <summary>Command TrayMotion(011)</summary>
        /// <param name="trayMotionType"></param>
        /// <remarks>移動托盤: 0.Home, 1.Out, 2.In</remarks>
        private string CommandTrayMotion(TrayMotionType trayMotionType)
        {
            var parameter = new TrayMotionParameter { TrayMotionType = trayMotionType };
            var commandText = new TrayMotion().GetCommandText(parameter);
            UdpSocket.SendTo(Encoding.UTF8.GetBytes(commandText), TargetEndpoint);
            return commandText;
        }

        /**
         /// <summary>Command TrayMotion(011)</summary>
         /// <param name="trayMotionType"></param>
         /// <remarks>移動托盤: 0.Home, 1.Out, 2.In</remarks>
         private string FakeCommandTrayMotion(TrayMotionType trayMotionType)
         {
             var parameter = new TrayMotionParameter { TrayMotionType = trayMotionType };
             var commandText = new TrayMotion().GetCommandText(parameter);
             //UdpSocket.SendTo(Encoding.UTF8.GetBytes(commandText), TargetEndpoint);
             return commandText;
         }
           */

        /// <summary>Command TrayMotion ~ Home(011) </summary>
        /// <remarks>Main Event: ReplyTrayMotion(111)</remarks>
        public string CommandTrayMotionHome()
        {
            var commandText = CommandTrayMotion(TrayMotionType.Home);
            //UdpSocket.SendTo(Encoding.UTF8.GetBytes(commandText), TargetEndpoint);
            return commandText;
        }


        /**
        ///<summary>Fake Command TrayMotion ~Home(011) </summary>
        ///<remarks>2020/10/23 12:00 King [C]</remarks>
        public string FakeCommandTrayMotionHome()
        {
            var commandText = FakeCommandTrayMotion(TrayMotionType.Home);
           
            return commandText;
        }*/



        /// <summary>Command TrayMotion ~ Out(011) </summary>
        public string CommandTrayMotionOut()
        {
            var commandText = CommandTrayMotion(TrayMotionType.Out);
            // UdpSocket.SendTo(Encoding.UTF8.GetBytes(commandText), TargetEndpoint);
            return commandText;
        }

        /**
          /// <summary>Fake Command TrayMotion ~ Out(011) </summary>
          ///<remarks>
          ///2020/10/23 13:15 King [C]
          /// </remarks>
          public string FakeCommandTrayMotionOut()
          {
              var commandText = FakeCommandTrayMotion(TrayMotionType.Out);
              
              return commandText;
          }
      */

        /// <summary>Command TrayMotion ~ In(011) </summary>
        public string CommandTrayMotionIn()
        {
            var commandText = CommandTrayMotion(TrayMotionType.In);
            //UdpSocket.SendTo(Encoding.UTF8.GetBytes(commandText), TargetEndpoint);
            return commandText;
        }
        /**
        /// <summary>Fake Command TrayMotion ~ In(011) </summary>
        /// <remarks>
        /// 2020/10/23 13:24 King[C]
        /// </remarks>
        public string FakeCommandTrayMotionIn()
        {
            var commandText = FakeCommandTrayMotion(TrayMotionType.In);
            
            return commandText;
        }
    */



        /// <summary>Command BrightLED(012)</summary>
        /// <param name="brightLEDType"></param>
        private string CommandBrightLED(BrightLEDType brightLEDType)
        {
            var parameter = new BrightLEDParameter { BrightLEDType = brightLEDType };
            var commandText = new BrightLED().GetCommandText(parameter);
            UdpSocket.SendTo(Encoding.UTF8.GetBytes(commandText), TargetEndpoint);
            return commandText;
        }

        /// <summary>Command BrightLED~All on(012)</summary>
        public string CommandBrightLEDAllOn()
        {

            var commandText = CommandBrightLED(BrightLEDType.AllOn);
            return commandText;
        }
        /// <summary>Command BrightLED All off(012)</summary>
        /// <returns></returns>
        public string CommandBrightLEDAllOff()
        {
            var commandText = CommandBrightLED(BrightLEDType.AllOff);
            return commandText;
        }

        /// <summary>Command BrightLED Green on(012)</summary>
        public string CommandBrightLEDGreenOn()
        {
            var commandText = CommandBrightLED(BrightLEDType.GreenOn);
            return commandText;
        }

        /// <summary>Command BrightLED Red on(012)</summary>
        public string CommandBrightLEDRedOn()
        {
            var commandText = CommandBrightLED(BrightLEDType.RedOn);
            return commandText;
        }

        /// <summary>Command PositionRead (013)</summary>
        public string CommandPositionRead()
        {
            var parameter = new PositionReadParameter();
            var commandText = new PositionRead().GetCommandText(parameter);
            //DrawerSocket.SentTo(commandText);
            UdpSocket.SendTo(Encoding.UTF8.GetBytes(commandText), TargetEndpoint);
            return commandText;
        }

        /// <summary>Command BoxDetection(014)</summary>
        public string CommandBoxDetection()
        {
            var parameter = new BoxDetectionParameter();
            var commandText = new BoxDetection().GetCommandText(parameter);
            UdpSocket.SendTo(Encoding.UTF8.GetBytes(commandText), TargetEndpoint);
            return commandText;
        }
        /**
         /// <summary>Fake Command BoxDetection(014)</summary>
         /// <remarks>
         /// 2020/10/23 13:30 King [C]
         /// </remarks>
         public string FakeCommandBoxDetection()
         {
             var parameter = new BoxDetectionParameter();
             var commandText = new BoxDetection().GetCommandText(parameter);
             // UdpSocket.SendTo(Encoding.UTF8.GetBytes(commandText), TargetEndpoint);
             return commandText;
         }
     */

        /// <summary>Command WriteNetSetting(031)</summary>
        public string CommandWriteNetSetting()
        {
            var parameter = new WriteNetSettingParameter();
            var commandText = new WriteNetSetting().GetCommandText(parameter);
            UdpSocket.SendTo(Encoding.UTF8.GetBytes(commandText), TargetEndpoint);
            return commandText;
        }

        /// <summary>Command LCDMsg(041)</summary>
        /// <param name="message"></param>
        public string CommandLCDMsg(string message)
        {
            var parameter = new LSDMsgParameter { Message = message };
            var commandText = new LCDMsg().GetCommandText(parameter);
            UdpSocket.SendTo(Encoding.UTF8.GetBytes(commandText), TargetEndpoint);
            return commandText;
        }

        /// <summary>Command SetParameter(007)</summary>
        /// <param name="setParameterType"></param>
        /// <param name="parameterValue"></param>
        private string CommandSetParameter(SetParameterType setParameterType, string parameterValue)
        {
            var parameter = new SetParameterParameter { ParameterValue = parameterValue, SetParameterType = setParameterType };
            var commandText = new SetParameter().GetCommandText(parameter);
            UdpSocket.SendTo(Encoding.UTF8.GetBytes(commandText), TargetEndpoint);
            return commandText;
        }

        /// <summary>Command SetParameter~ HomePosition(007)</summary>
        /// <param name="homePosition"></param>
        public string CommandSetParameterHomePosition(string homePosition)
        {
            var commandText = CommandSetParameter(SetParameterType.Home_position, homePosition);
            return commandText;
        }

        /// <summary>Command SetParameter~ OutSidePosition(007)</summary>
        /// <param name="outsidePosition"></param>
        public string CommandSetParameterOutSidePosition(string outsidePosition)
        {
            var commandText = CommandSetParameter(SetParameterType.Out_side_position, outsidePosition);
            return commandText;
        }

        /// <summary>Command SetParameter~ InSidePosition(007)</summary>
        /// <param name="insidePosition"></param>
        public string CommandSetParameterInSidePosition(string insidePosition)
        {
            var commandText = CommandSetParameter(SetParameterType.In_side_position, insidePosition);
            return commandText;
        }

        /// <summary>Command SetParameter~ IPAddress(007)</summary>
        /// <param name="ipAddress"></param>
        public string CommandSetParameterIPAddress(string ipAddress)
        {
            var commandText = CommandSetParameter(SetParameterType.IP_address, ipAddress);
            return commandText;
        }

        /// <summary>Command SetParameter~ SubMask(007)</summary>
        /// <param name="submaskAddress"></param>
        public string CommandSetParameterSubMask(string submaskAddress)
        {
            var commandText = CommandSetParameter(SetParameterType.SubMask, submaskAddress);
            return commandText;
        }

        /// <summary>Command SetParameter~ GetwayAddress(007)</summary>
        /// <param name="getwayAddress"></param>
        public string CommandSetParameterGetwayAddress(string getwayAddress)
        {
            var commandText = CommandSetParameter(SetParameterType.Gateway_address, getwayAddress);
            return commandText;
        }

        #region event
        /// <summary>Event ReplyTrayMotion(111)</summary>
        /// <param name="reply">回覆的訊息(執行結果)</param>
        /// <remarks>
        /// <para>除非規格書有異動, 否則</para>
        /// <para>1. 函式名稱不得修改</para>
        /// <para>2. 函式不得刪除</para>
        /// </remarks>
        public void ReplyTrayMotion(ReplyMessage reply)
        {
            ReplyResultCode replyResultCode = (ReplyResultCode)((int)(reply.Value));
            if (OnReplyTrayMotionHandler != null)
            {
                var eventArgs = new OnReplyTrayMotionEventArgs(replyResultCode);
                OnReplyTrayMotionHandler.Invoke(this, eventArgs);
            }
            if (replyResultCode == ReplyResultCode.Set_Successfully)
            {
                if (OnTrayMotionOKHandler != null)
                {
                    OnTrayMotionOKHandler.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                if (OnTrayMotionFailedHandler != null)
                {
                    OnTrayMotionFailedHandler.Invoke(this, EventArgs.Empty);
                }

            }
        }
        /// <summary>ReplyMotion 事件處理程序</summary>
        [Obsolete]
        public event EventHandler OnReplyTrayMotionHandler = null;
        public event EventHandler OnTrayMotionFailedHandler = null;
        public event EventHandler OnTrayMotionOKHandler = null;
        /// <summary>將ReplyMotion事件程序指向 null</summary>
        [Obsolete]
        public void ResetOnReplyTrayMotionHandler() { OnReplyTrayMotionHandler = null; }


        /// <summary>Event ReplySetSpeed(100)</summary>
        /// <param name="reply">回覆的訊息(執行結果)</param>
        /// <remarks>
        /// <para>除非規格書有異動, 否則</para>
        /// <para>1. 函式名稱不得修改</para>
        /// <para>2. 函式不得刪除</para>
        /// </remarks>
        public void ReplySetSpeed(ReplyMessage reply)
        {
            ReplyResultCode replyResultCode = (ReplyResultCode)((int)(reply.Value));
            if (OnReplySetSpeedHandler != null)
            {
                var eventArgs = new OnReplySetSpeedEventArgs(replyResultCode);
                OnReplySetSpeedHandler.Invoke(this, eventArgs);
            }
            if (SetMotionSpeedResult != null)
            {
                SetMotionSpeedResult.Invoke(this, replyResultCode == ReplyResultCode.Set_Successfully ? true : false);
            }
            if (replyResultCode == ReplyResultCode.Set_Successfully)
            {
                if (OnSetMotionSpeedOKHandler != null)
                {
                    OnSetMotionSpeedOKHandler.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                if (OnSetMotionSpeedFailedHandler != null)
                {
                    OnSetMotionSpeedFailedHandler.Invoke(this, EventArgs.Empty);
                }
            }


        }
        /// <summary>ReplySetSpeed事件程序</summary>
        [Obsolete]
        public event EventHandler OnReplySetSpeedHandler = null;
        public event EventHandler OnSetMotionSpeedFailedHandler = null;
        public event EventHandler OnSetMotionSpeedOKHandler = null;

        /// <summary>將ReplySetSpeed事件程序重設為null</summary>
        [Obsolete]
        public void ResetOnReplySetSpeedHandler() { OnReplySetSpeedHandler = null; }




        /// <summary>Event  ReplySetTimeOut(101)</summary>
        /// <param name="reply">回覆的訊息(執行結果)</param>
        /// <remarks>
        /// <para>除非規格書有異動, 否則</para>
        /// <para>1. 函式名稱不得修改</para>
        /// <para>2. 函式不得刪除</para>
        /// </remarks>
        public void ReplySetTimeOut(ReplyMessage reply)
        {
            ReplyResultCode replyResultCode = (ReplyResultCode)((int)(reply.Value));
            var eventArgs = new OnReplySetTimeOutEventArgs(replyResultCode);
            if (OnReplySetTimeOutHandler != null)
            {
                OnReplySetTimeOutHandler.Invoke(this, eventArgs);
            }
            if (SetTimeOutResult != null)
            {
                SetTimeOutResult.Invoke(this, replyResultCode == ReplyResultCode.Set_Successfully ? true : false);
            }
            if (OnSetTimeOutOKHandler != null && replyResultCode == ReplyResultCode.Set_Successfully)
            {
                OnSetTimeOutOKHandler.Invoke(this, EventArgs.Empty);
            }
            if (OnSetTimeOutFailedHandler != null && replyResultCode == ReplyResultCode.Failed)
            {
                OnSetTimeOutFailedHandler.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler OnSetTimeOutOKHandler = null;
        public event EventHandler OnSetTimeOutFailedHandler = null;

        /// <summary>ReplySetTimeOut 事件程序</summary>
        [Obsolete]
        public event EventHandler OnReplySetTimeOutHandler = null;
        /// <summary>將OnReplySetTimeOut 事件程序設為null</summary>
        [Obsolete]
        public void ResetOnReplySetTimeOutHandler() { OnReplySetTimeOutHandler = null; }

        //@~112,ReplyBrightLED,1@
        /// <summary>Event ReplyBrightLED(112)</summary>
        /// <param name="reply">回覆的訊息(執行結果)</param>
        /// <remarks>
        /// <para>除非規格書有異動, 否則</para>
        /// <para>1. 函式名稱不得修改</para>
        /// <para>2. 函式不得刪除</para>
        /// </remarks>
        public void ReplyBrightLED(ReplyMessage reply)
        {
            ReplyResultCode replyResultCode = (ReplyResultCode)((int)(reply.Value));
            var eventArgs = new OnReplyBrightLEDEventArgs(replyResultCode);
            if (OnReplyBrightLEDHandler != null)
            {
                OnReplyBrightLEDHandler.Invoke(this, eventArgs);
            }
            if (this.BrightLEDResult != null)
            {
                this.BrightLEDResult.Invoke(this, replyResultCode == ReplyResultCode.Set_Successfully ? true : false);
            }
            if (OnBrightLEDOKHandler != null && replyResultCode == ReplyResultCode.Set_Successfully)
            {
                OnBrightLEDOKHandler.Invoke(this, EventArgs.Empty);
            }
            if (OnBrightLEDFailedHandler != null && replyResultCode == ReplyResultCode.Failed)
            {
                OnBrightLEDFailedHandler.Invoke(this, EventArgs.Empty);
            }

        }
        public event EventHandler OnBrightLEDOKHandler;
        public event EventHandler OnBrightLEDFailedHandler;

        [Obsolete]
        /// <summary>ReplyBrightLED 事件程序</summary>
        public event EventHandler OnReplyBrightLEDHandler = null;
        /// <summary>將  OnReplyBrightLED 事件程設為null</summary>
        [Obsolete]
        public void ResetOnReplyBrightLEDHandler() { OnReplyBrightLEDHandler = null; }


        /// <summary>Event ReplyPosition(113) </summary>
        /// <param name="reply">回覆的訊息(執行結果)</param>
        /// <remarks>
        /// <para>除非規格書有異動, 否則</para>
        /// <para>1. 函式名稱不得修改</para>
        /// <para>2. 函式不得刪除</para>
        /// </remarks>
        public void ReplyPosition(ReplyMessage reply)
        {
            var IHO = "000";
            switch ((int)reply.Value)
            {
                case 0:
                    IHO = "000";
                    break;
                case 1:
                    IHO = "001";
                    break;
                case 2:
                    IHO = "010";
                    break;
                case 3:
                    IHO = "011";
                    break;
                case 4:
                    IHO = "100";
                    break;
                case 5:
                    IHO = "101";
                    break;
                case 6:
                    IHO = "110";
                    break;
                case 7:
                    IHO = "111";
                    break;
            }
            var eventArgs = new OnReplyPositionEventArgs(IHO);
            if (OnReplyPositionHandler != null)
            {

                OnReplyPositionHandler.Invoke(this, eventArgs);

            }
            if (PositionReadResult != null) { PositionReadResult.Invoke(this, IHO); }
            if (OnPositionStatusHandler != null)
            {
                OnPositionStatusHandler.Invoke(this, eventArgs);

            }
        }


        public event EventHandler OnPositionStatusHandler = null;
        [Obsolete]
        /// <summary>ReplyPosition 事件程序</summary>
        public event EventHandler OnReplyPositionHandler = null;
        /// <summary>重設ReplyPosition事件程序為 null </summary>
        [Obsolete]
        public void ResetOnReplyPositionHandler() { OnReplyPositionHandler = null; }


        /// <summary>Event ReplyBoxDetection(114)</summary>
        /// <param name="reply">回覆的訊息(執行結果)</param>
        /// <remarks>
        /// <para>除非規格書有異動, 否則</para>
        /// <para>1. 函式名稱不得修改</para>
        /// <para>2. 函式不得刪除</para>
        /// </remarks>
        public void ReplyBoxDetection(ReplyMessage reply)
        {
            var hasBox = false;
            if (reply.Value.HasValue && (int)reply.Value == 1)
            {
                hasBox = true;
            }
            if (OnReplyBoxDetection != null)
            {
                var args = new OnReplyBoxDetectionEventArgs(hasBox);
                OnReplyBoxDetection.Invoke(this, args);
            }
            if (BoxDetectionResult != null)
            {
                BoxDetectionResult.Invoke(this, hasBox);
            }

            if (OnDetectedHasBoxHandler != null && hasBox)
            {
                OnDetectedHasBoxHandler.Invoke(this, EventArgs.Empty);
            }
            if (OnDetectedEmptyBoxHandler != null && !hasBox)
            {
                OnDetectedEmptyBoxHandler.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler OnDetectedHasBoxHandler;
        public event EventHandler OnDetectedEmptyBoxHandler;
        [Obsolete]
        /// <summary>ReplyBoxDetection 事件程序</summary>
        public event EventHandler OnReplyBoxDetection = null;
        /// <summary>重設 ReplyBoxDetection 事件程序為 null</summary>
        [Obsolete]
        public void ResetOnReplyBoxDetection() { OnReplyBoxDetection = null; }



        /// <summary>Event TrayArrive (115)</summary>
        /// <param name="reply">回覆的訊息(執行結果)</param>
        /// <remarks>
        /// <para>除非規格書有異動, 否則</para>
        /// <para>1. 函式名稱不得修改</para>
        /// <para>2. 函式不得刪除</para>
        /// </remarks>
        public void TrayArrive(ReplyMessage reply)
        {
            TrayArriveType trayArriveType = (TrayArriveType)((int)reply.Value);
            if (OnTrayArriveHandler != null)
            {
                var args = new OnTrayArriveEventArgs(trayArriveType);
                OnTrayArriveHandler.Invoke(this, args);
            }

            if (this.TrayArriveResult != null) { this.TrayArriveResult.Invoke(this, (int)trayArriveType); }
            if (OnTrayArriveHomeHandler != null && trayArriveType == TrayArriveType.ArriveHome)
            {
                OnTrayArriveHomeHandler.Invoke(this, EventArgs.Empty);
            }
            if (OnTrayArriveInHandler != null && trayArriveType == TrayArriveType.ArriveIn)
            {
                OnTrayArriveInHandler.Invoke(this, EventArgs.Empty);
            }
            if (OnTrayArriveOutHandler != null && trayArriveType == TrayArriveType.ArriveOut)
            {
                OnTrayArriveOutHandler.Invoke(this, EventArgs.Empty);
            }
            //   OnTrayArriveHomeHandler.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler OnTrayArriveHomeHandler;
        public event EventHandler OnTrayArriveInHandler;
        public event EventHandler OnTrayArriveOutHandler;

        /// <summary>TrayArrive 事件程序</summary>
        public event EventHandler OnTrayArriveHandler = null;
        /// <summary>將TrayArrive 事件程序重設為 null</summary>
        [Obsolete]
        public void ResetOnTrayArriveHandler() { OnTrayArriveHandler = null; }



        /// <summary>Event ButtonEvent(120)</summary>
        /// <param name="reply">回覆的訊息(執行結果)</param>
        /// <remarks>
        /// <para>除非規格書有異動, 否則</para>
        /// <para>1. 函式名稱不得修改</para>
        /// <para>2. 函式不得刪除</para>
        /// </remarks>
        public void ButtonEvent(ReplyMessage reply)
        {
            if (OnButtonEventHandler != null)
            {
                OnButtonEventHandler.Invoke(this, EventArgs.Empty);
            }
        }
        /// <summary>ButtonEvent 事件程序</summary>
        public event EventHandler OnButtonEventHandler = null;
        /// <summary>將 ButtonEvent 事件程序重設為 null</summary>
        [Obsolete]
        public void ResetOnButtonEventHandler() { OnButtonEventHandler = null; }

        public void LCDCMsg(ReplyMessage reply)
        {
            ReplyResultCode replyResultCode = (ReplyResultCode)((int)(reply.Value));
            var eventArgs = new OnLCDCMsgEventArgs(replyResultCode);
            if (OnLCDCMsgHandler != null)
            {
                OnLCDCMsgHandler.Invoke(this, eventArgs);
            }
            if (OnLCDCMsgOKHandler != null && replyResultCode == ReplyResultCode.Set_Successfully)
            {
                OnLCDCMsgOKHandler.Invoke(this, EventArgs.Empty);
            }
            if (OnLCDCMsgFailedHandler != null && replyResultCode == ReplyResultCode.Failed)
            {
                OnLCDCMsgFailedHandler.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler OnLCDCMsgOKHandler = null;
        public event EventHandler OnLCDCMsgFailedHandler = null;
        [Obsolete]
        public event EventHandler OnLCDCMsgHandler = null;
        /// <summary>將 ButtonEvent 事件程序重設為 null</summary>
        [Obsolete]
        public void ResetOnLCDCMsgHandler() { OnLCDCMsgHandler = null; }

        /// <summary>Event TimeOutEvent(900)</summary>
        /// <param name="reply">回覆的訊息(執行結果)</param>
        /// <remarks>
        /// <para>除非規格書有異動, 否則</para>
        /// <para>1. 函式名稱不得修改</para>
        /// <para>2. 函式不得刪除</para>
        /// </remarks>
        private void TimeOutEvent(ReplyMessage reply)
        {
            if (OnTimeOutEventHandler != null)
            {
                OnTimeOutEventHandler.Invoke(this, EventArgs.Empty);
            }

        }
        /// <summary>TimeOutEvent事件程序</summary>
        public event EventHandler OnTimeOutEventHandler = null;
        /// <summary>將TimeOutEventk事件程序重設為 null</summary>
        [Obsolete]
        public void ResetOnTimeOutEventHandler() { OnTimeOutEventHandler = null; }

        /// <summary>Event TrayMotioning(901)</summary>
        /// <param name="reply">回覆的訊息(執行結果)</param>
        /// <remarks>
        /// <para>除非規格書有異動, 否則</para>
        /// <para>1. 函式名稱不得修改</para>
        /// <para>2. 函式不得刪除</para>
        /// </remarks>
        public void TrayMotioning(ReplyMessage reply)
        {
            if (OnTrayMotioningHandler != null)
            {
                OnTrayMotioningHandler.Invoke(this, EventArgs.Empty);
            }
        }


        /// <summary>TrayMotioning 事件程序</summary>
        public event EventHandler OnTrayMotioningHandler = null;
        /// <summary>將TrayMotioning事件程序重設為 null</summary>
        [Obsolete]
        public void ResetOnTrayMotioning() { OnTrayMotioningHandler = null; }

        /// <summary>event INIFailed (902)</summary>
        /// <param name="reply">回覆的訊息(執行結果)</param>
        /// <remarks>
        /// <para>除非規格書有異動, 否則</para>
        /// <para>1. 函式名稱不得修改</para>
        /// <para>2. 函式不得刪除</para>
        /// </remarks>
        public void INIFailed(ReplyMessage reply)
        {
            if (OnINIFailedHandler != null)
            {
                OnINIFailedHandler.Invoke(this, EventArgs.Empty);
            }
            if (INIResult != null)
            {
                INIResult.Invoke(this, false);
            }
        }
        /// <summary>INIFailed 事件程序</summary>
        public event EventHandler OnINIFailedHandler = null;
        /// <summary>將INIFailed 事件程序重設為null</summary>
        [Obsolete]
        public void ResetOnINIFailedHandler() { OnINIFailedHandler = null; }


        /// <summary>Event TrayMotionError(903)</summary>
        /// <param name="reply">回覆的訊息(執行結果)</param>
        /// <remarks>
        /// <para>除非規格書有異動, 否則</para>
        /// <para>1. 函式名稱不得修改</para>
        /// <para>2. 函式不得刪除</para>
        /// </remarks>
        public void TrayMotionError(ReplyMessage reply)
        {
            if (OnTrayMotionErrorHandler != null)
            {
                OnTrayMotionErrorHandler.Invoke(this, EventArgs.Empty);
            }

        }


        [Obsolete]
        /// <summary>TrayMotionError 事件程序</summary>
        public event EventHandler OnTrayMotionErrorHandler = null;
        /// <summary>將TrayMotionError 事件程序重設為0</summary>
        [Obsolete]
        public void ResetOnTrayMotionErrorHandler()
        { OnTrayMotionErrorHandler = null; }


        /// <summary>TrayMotionSensorOFF 事件程序(也是903)</summary>
        /// <param name="reply"></param>
        /// <remarks>
        /// <para>除非規格書有異動, 否則</para>
        /// <para>1. 函式名稱不得修改</para>
        /// <para>2. 函式不得刪除</para>
        /// </remarks>
        public void TrayMotionSensorOFF(ReplyMessage reply)
        {
            if (OnTrayMotionSensorOFFHandler != null)
            {
                OnTrayMotionSensorOFFHandler.Invoke(this, EventArgs.Empty);
            }
        }
        /// <summary>TrayMotionSensorOFF 事件程序</summary>
        public event EventHandler OnTrayMotionSensorOFFHandler = null;
        /// <summary>將TrayMotionSensorOFF 事件程序重設為 null</summary>
        [Obsolete]
        public void ResetOnTrayMotionSensorOFFHandler() { OnTrayMotionSensorOFFHandler = null; }



        /// <summary>Event Error(904)</summary>
        /// <param name="reply">回覆的訊息(執行結果)</param>
        /// <remarks>
        /// <para>除非規格書有異動, 否則</para>
        /// <para>1. 函式名稱不得修改</para>
        /// <para>2. 函式不得刪除</para>
        /// </remarks>
        public void ERROR(ReplyMessage reply)
        {
            ReplyErrorCode replyErrorCode = (ReplyErrorCode)((int)reply.Value);
            if (OnErrorHandler != null)
            {
                var args = new OnErrorEventArgs(replyErrorCode);
                OnErrorHandler.Invoke(this, args);
            }
            if (OnERRORRecoveryHandler != null && replyErrorCode == ReplyErrorCode.Recovery)
            {
                OnERRORRecoveryHandler.Invoke(this, EventArgs.Empty);
            }
            if (OnERRORErrorHandler != null && replyErrorCode == ReplyErrorCode.Error)
            {
                OnERRORErrorHandler.Invoke(this, EventArgs.Empty);
            }
        }

        public void Error(ReplyMessage reply) { ERROR(reply); }

        public event EventHandler OnERRORRecoveryHandler = null;
        public event EventHandler OnERRORErrorHandler = null;

        [Obsolete]
        /// <summary>Error 事件程序</summary>
        public event EventHandler OnErrorHandler = null;
        /// <summary>將Error 事件程序重設為 null</summary>
        [Obsolete]
        public void ResetOnErrorHandler() { OnErrorHandler = null; }



        /// <summary>Event SysStartUp(999)</summary>
        /// <param name="reply">回覆的訊息(執行結果)</param>
        /// <remarks>
        /// <para>除非規格書有異動, 否則</para>
        /// <para>1. 函式名稱不得修改</para>
        /// <para>2. 函式不得刪除</para>
        /// </remarks>
        public void SysStartUp(ReplyMessage reply)
        {
            if (OnSysStartUpHandler != null)
            {
                OnSysStartUpHandler.Invoke(this, EventArgs.Empty);
            }
        }
        /// <summary>SysStartUp 事件程序</summary>
        public event EventHandler OnSysStartUpHandler = null;
        /// <summary>將SysStartUp 事件程序重設為 null</summary>
        [Obsolete]
        public void ResetOnSysStartUp() { OnSysStartUpHandler = null; }




        #endregion

        public DelegateDrawerBooleanResult INIResult { get; set; } //= null;
        public DelegateDrawerBooleanResult SetMotionSpeedResult { get; set; } //= null;
        public DelegateDrawerBooleanResult SetTimeOutResult { get; set; }//= null;
                                                                         // public DelegateDrawerBooleanResult TrayMotionHomeResult=null;
                                                                         //  public DelegateDrawerBooleanResult TrayMotionOutResult =null;
                                                                         //  public DelegateDrawerBooleanResult TrayMotionInResult=null;


        public DelegateDrawerBooleanResult BrightLEDResult { get; set; } //= null;
        public DelegateDrawerStringResult PositionReadResult { get; set; } //= null;
        public DelegateDrawerBooleanResult BoxDetectionResult { get; set; } //= null;

        //public DelegateDrawerBooleanResult TrayArriveHomeResult = null;
        //public DelegateDrawerBooleanResult TrayArriveInResult = null;
        //public DelegateDrawerBooleanResult TrayArriveOutResult = null;
        public DelegateDrawerIntResult TrayArriveResult { get; set; } //= null;


        #region IDisposable
        protected bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //

            this.DisposeSelf();

            disposed = true;
        }

        protected virtual void DisposeSelf()
        {
            if (this.UdpSocket != null)
                using (var obj = this.UdpSocket)
                {
                    try
                    {
                        obj.Shutdown(SocketShutdown.Both);
                        obj.Close();
                    }
                    catch (Exception) { }
                }

            if (this.ListenThread != null)
            {
                try { this.ListenThread.Abort(); }
                catch (Exception) { /*不需要catch, 到dispose階段的中斷成敗都要結束*/ }
            }
        }
        #endregion
    }
}