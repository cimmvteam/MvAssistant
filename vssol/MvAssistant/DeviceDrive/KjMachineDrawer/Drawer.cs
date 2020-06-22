using MvAssistant.DeviceDrive.KjMachineDrawer.Exceptions;
using MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand;
using MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer
{
    public class Drawer
    {
        /// <summary>Cabinet 編號</summary>        
        public int CabinetNO { get; private set; }
        /// <summary>Drawer 編號</summary>
        public string DrawerNO { get; private set; }
        /// <summary>裝置IP</summary>
        public string DeviceIP { get; private set; }
        //    public DrawerSocket DrawerSocket { get; private set; }
        IPEndPoint TargetEndpoint = null;
      public   Socket UdpSocket = null;
        private Drawer() {
           
        }

        public Drawer(int cabinetNO, string drawerNO, IPEndPoint deviceEndpoint, string localIp,IDictionary<int,bool?> portTable) : this()
        {
            DrawerNO = drawerNO;
            CabinetNO = cabinetNO;
           
            TargetEndpoint = deviceEndpoint;
            UdpSocket= new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            while (true)
            {
                var port = 0;
                try
                {

                    KeyValuePair<int, bool?> keyValuePair = portTable.Where(m => m.Value == default(bool?)).FirstOrDefault();
                    if (keyValuePair.Equals(default(KeyValuePair<int, bool?>)))
                    {
                        // TODO : To Thorw an Exception
                    }
                    port = keyValuePair.Key;
                    //port = portTable.Where(m => m.Value == default(bool?)).FirstOrDefault();
                    var endPoint = new IPEndPoint(IPAddress.Parse(localIp), port);
                    UdpSocket.Bind(endPoint);
                    portTable.Remove(port);
                    portTable.Add(port, true);
                    break;
                }
                catch (Exception ex)
                {
                    portTable.Remove(port);
                    portTable.Add(port, false);
                }
            }
            Task.Run(
                () =>
                  {
                      try
                      {
                          while (true)
                          {
                              byte[] buffer = new byte[1024];
                              UdpSocket.Receive(buffer);
                              var msg = Encoding.UTF8.GetString(buffer);
                              InvokeMethod(msg);
                          }
                      }
                      catch(Exception ex)
                      {

                      }
                  }
               );
          }
       
        public int Send(string message)
        {
           var len= this.UdpSocket.SendTo(Encoding.UTF8.GetBytes(message), TargetEndpoint);
            return len;
        }

        public void InvokeMethod(string rtnMsg)
        {
            var msg = rtnMsg.Replace(BaseCommand.CommandPostfixText, "").Replace(BaseCommand.CommandPostfixText,"");
            var msgArray = msg.Split(new string[] { BaseCommand.CommandSplitSign }, StringSplitOptions.RemoveEmptyEntries);
            ReplyMessage rplyMsg = new ReplyMessage
            {
                StringCode = msgArray[0],
                StringFunc = msgArray[1],
                Value = msgArray.Length == 3 ? Convert.ToInt32(msgArray[2]) : default(int?)
            };
            var method=this.GetType().GetMethod(rplyMsg.StringFunc);
            if(method != null)
            {
                method.Invoke(this, new object[] { rplyMsg });
            }
        }
       
        /// <summary>Command INI(099)</summary>
        /// <returns></returns>
        public string CommandINI()
        {
            var commandText = new INI().GetCommandText(new INIParameter());
            //DrawerSocket.SentTo(commandText);
            return commandText;
        }
        
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
            //DrawerSocket.SentTo(commandText);
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
            var parameter = new SetTimeOutParameter {  Seconds=timeoutSeconds };
            var commandText = new SetTimeOut().GetCommandText(parameter);
            //DrawerSocket.SentTo(commandText);
            return commandText;
        }

        /// <summary>Command TrayMotion(011)</summary>
        /// <param name="trayMotionType"></param>
        /// <remarks>移動托盤: 0.Home, 1.Out, 2.In</remarks>
        private string CommandTrayMotion(TrayMotionType trayMotionType)
        {
            var parameter = new TrayMotionParameter { TrayMotionType = trayMotionType };
            var commandText = new TrayMotion().GetCommandText(parameter);
            //DrawerSocket.SentTo(commandText);
            return commandText;
        }


        /// <summary>Command TrayMotion ~ Home(011) </summary>
        /// <remarks>Main Event: ReplyTrayMotion(111)</remarks>
        public string CommandTrayMotionHome()
        {
            var commandText=CommandTrayMotion(TrayMotionType.Home);
            return commandText;
        }

        /// <summary>Command TrayMotion ~ Out(011) </summary>
        public string CommandTrayMotionOut()
        {
           var commandText= CommandTrayMotion(TrayMotionType.Out);
            return commandText;
        }
        /// <summary>Command TrayMotion ~ In(011) </summary>
        public string CommandTrayMotionIn()
        {
            var commandText=CommandTrayMotion(TrayMotionType.In);
            return commandText;
        }

        /// <summary>Command BrightLED(012)</summary>
        /// <param name="brightLEDType"></param>
        private string CommandBrightLED(BrightLEDType brightLEDType)
        {
            var parameter = new BrightLEDParameter { BrightLEDType = brightLEDType };
            var commandText = new BrightLED().GetCommandText(parameter);
            //DrawerSocket.SentTo(commandText);
            UdpSocket.SendTo(Encoding.UTF8.GetBytes(commandText), TargetEndpoint);
            return commandText;
        }

        /// <summary>Command BrightLED~All on(012)</summary>
        public string CommandBrightLEDAllOn()
        {
            var commandText=CommandBrightLED(BrightLEDType.AllOn);
            return commandText;
        }
        /// <summary>Command BrightLED All off(012)</summary>
        /// <returns></returns>
        public string CommandBrightLEDAllOff()
        {
            var commandText= CommandBrightLED(BrightLEDType.AllOff);
            return commandText;
        }

        /// <summary>Command BrightLED Green on(012)</summary>
        public string CommandBrightLEDGreenOn()
        {
            var commandText=CommandBrightLED(BrightLEDType.GreenOn);
            return commandText;
        }

        /// <summary>Command BrightLED Red on(012)</summary>
        public string CommandBrightLEDRedOn()
        {
          var commandText=  CommandBrightLED(BrightLEDType.RedOn);
            return commandText;
        }

        /// <summary>Command PositionRead (013)</summary>
        public string CommandPositionRead()
        {
            var parameter =  new PositionReadParameter();
            var commandText = new PositionRead().GetCommandText(parameter);
            //DrawerSocket.SentTo(commandText);
            return commandText;
        }

        /// <summary>Command BoxDetection(014)</summary>
        public string CommandBoxDetection()
        {
            var parameter = new BoxDetectionParameter();
            var commandText = new BoxDetection().GetCommandText(parameter);
            //DrawerSocket.SentTo(commandText);
            return commandText;
        }

        /// <summary>Command WriteNetSetting(031)</summary>
        public string CommandWriteNetSetting()
        {
            var parameter = new WriteNetSettingParameter();
            var commandText = new WriteNetSetting().GetCommandText(parameter);
            //DrawerSocket.SentTo(commandText);
            return commandText;
        }

        /// <summary>Command LCDMsg(041)</summary>
        /// <param name="message"></param>
        public string CommandLCDMsg(string message)
        {
            var parameter = new LSDMsgParameter { Message = message };
            var commandText = new LCDMsg().GetCommandText(parameter);
           // DrawerSocket.SentTo(commandText);
            return commandText;
        }

        /// <summary>Command SetParameter(007)</summary>
        /// <param name="setParameterType"></param>
        /// <param name="parameterValue"></param>
        private void CommandSetParameter(SetParameterType setParameterType, string parameterValue)
        {
            var parameter = new SetParameterParameter {  ParameterValue= parameterValue, SetParameterType=setParameterType };
            var commandText = new SetParameter().GetCommandText(parameter);
            //DrawerSocket.SentTo(commandText);
        }

        /// <summary>Command SetParameter~ HomePosition(007)</summary>
        /// <param name="homePosition"></param>
        public void CommandSetParameterHomePosition(string homePosition)
        {
            CommandSetParameter(SetParameterType.Home_position, homePosition);
        }

        /// <summary>Command SetParameter~ OutSidePosition(007)</summary>
        /// <param name="outsidePosition"></param>
        public void CommandSetParameterOutSidePosition(string outsidePosition)
        {
            CommandSetParameter(SetParameterType.Out_side_position, outsidePosition);
        }

        /// <summary>Command SetParameter~ InSidePosition(007)</summary>
        /// <param name="insidePosition"></param>
        public void CommandSetParameterInSidePosition(string insidePosition)
        {
            CommandSetParameter(SetParameterType.In_side_position, insidePosition);
        }

        /// <summary>Command SetParameter~ IPAddress(007)</summary>
        /// <param name="ipAddress"></param>
        public void CommandSetParameterIPAddress(string ipAddress)
        {
            CommandSetParameter(SetParameterType.IP_address, ipAddress);
        }

        /// <summary>Command SetParameter~ SubMask(007)</summary>
        /// <param name="submaskAddress"></param>
        public void CommandSetParameterSubMask(string submaskAddress)
        {
            CommandSetParameter(SetParameterType.SubMask, submaskAddress);
        }

        /// <summary>Command SetParameter~ GetwayAddress(007)</summary>
        /// <param name="getwayAddress"></param>
        public void CommandSetParameterGetwayAddress(string getwayAddress)
        {
            CommandSetParameter(SetParameterType.Gateway_address, getwayAddress);
        }
        #region event
        /// <summary>Event ReplySetSpeed(111)</summary>
        /// <param name="reply"></param>
        public void ReplyTrayMotion(ReplyMessage reply)
        {
            ReplyResultCode replyResultCode = (ReplyResultCode)((int)(reply.Value));
            if (OnReplyTrayMotionHandler != null)
            {
                var eventArgs = new OnReplyTrayMotionEventArgs(replyResultCode);
                OnReplyTrayMotionHandler.Invoke(this, eventArgs);
            };
        }
        public event EventHandler OnReplyTrayMotionHandler = null;
        public void SetOnReplyTrayMotionHandler() { OnReplyTrayMotionHandler = null; }
        public class OnReplyTrayMotionEventArgs : EventArgs
        {
            public ReplyResultCode ReplyResultCode { get; private set; }
            private OnReplyTrayMotionEventArgs() { }
            public OnReplyTrayMotionEventArgs(ReplyResultCode replyResultCode):this() { ReplyResultCode = ReplyResultCode;      }
        }


        /// <summary>Event ReplySetSpeed(100)</summary>
        /// <param name="reply"></param>
        public void ReplySetSpeed(ReplyMessage reply)
        {
            ReplyResultCode replyResultCode = (ReplyResultCode)((int)(reply.Value));
            if (OnReplySetSpeedHandler != null)
            {
                var eventArgs = new OnReplySetSpeedEventArgs(replyResultCode);
                OnReplySetSpeedHandler.Invoke(this, eventArgs);
            }
        }
        public event EventHandler OnReplySetSpeedHandler = null;
        public void ResetOnReplySetSpeedHandler() { OnReplySetSpeedHandler = null; }
        public class OnReplySetSpeedEventArgs : EventArgs
        {
            public ReplyResultCode ReplyResultCode { get; private set; }
            private OnReplySetSpeedEventArgs() { }
            public OnReplySetSpeedEventArgs(ReplyResultCode replyResultCode):this() { ReplyResultCode = ReplyResultCode; }
        }



        /// <summary>Event  ReplySetTimeOut(101)</summary>
        /// <param name="reply"></param>
        public void ReplySetTimeOut(ReplyMessage reply)
        {
            ReplyResultCode replyResultCode = (ReplyResultCode)((int)(reply.Value));
            var eventArgs = new OnReplySetTimeOutEventArgs(replyResultCode);
            if (OnReplySetTimeOutHandler != null)
            {
                OnReplySetTimeOutHandler.Invoke(this, eventArgs);
            }
        }
        public event EventHandler OnReplySetTimeOutHandler = null;
        public void ResetOnReplySetTimeOutHandler() { OnReplySetTimeOutHandler = null; }
        public class OnReplySetTimeOutEventArgs : EventArgs
        {
            public ReplyResultCode ReplyResultCode { get; private set; }
            private OnReplySetTimeOutEventArgs() { }
            public OnReplySetTimeOutEventArgs(ReplyResultCode replyResultCode) : this() { ReplyResultCode = ReplyResultCode; }
        }

        //@~112,ReplyBrightLED,1@
        /// <summary>Event ReplyBrightLED(112)</summary>
        /// <param name="reply"></param>
        public void ReplyBrightLED(ReplyMessage reply)
        {
            ReplyResultCode replyResultCode = (ReplyResultCode)((int)(reply.Value));
            var eventArgs = new OnReplyBrightLEDEventArgs(replyResultCode);
            if (OnReplyBrightLEDHandler != null)
            {
                OnReplyBrightLEDHandler.Invoke(this, eventArgs);
            }
        }
        public event EventHandler OnReplyBrightLEDHandler = null;
        public void ResetOnReplyBrightLEDHandler() { OnReplyBrightLEDHandler = null; }
        public class OnReplyBrightLEDEventArgs : EventArgs
        {
           public ReplyResultCode ReplyResultCode { get; private set; }
            private OnReplyBrightLEDEventArgs (){}
            public OnReplyBrightLEDEventArgs(ReplyResultCode replyResultCode) : this() { ReplyResultCode = replyResultCode; }

        }


        /// <summary>Event ReplyPosition(113) </summary>
        /// <param name="reply"></param>
        public void  ReplyPosition(ReplyMessage reply)
        {
            var IHO = "";
            switch ((int)reply.Value)
            {    case 0:
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
            if (OnReplyPositionHandler != null)
            {
                var eventArgs = new OnReplyPositionEventArgs(IHO);
                OnReplyPositionHandler.Invoke(this, eventArgs);

            }
        }
        public event EventHandler OnReplyPositionHandler= null;
        public void ResetOnReplyPositionHandler() { OnReplyPositionHandler = null; }
        public class OnReplyPositionEventArgs : EventArgs
        {
            public string IHOStatus { get; private set; }
            private OnReplyPositionEventArgs(){           }
            public OnReplyPositionEventArgs(string ihoStatus) : this() {IHOStatus=ihoStatus; }
        }

        /// <summary>Event ReplyBoxDetection(114)</summary>
        /// <param name="reply"></param>
        public void ReplyBoxDetection(ReplyMessage reply)
        {
            var hasBox = false;
            if(reply.Value.HasValue && (int)reply.Value == 1)
            {
                hasBox = true;
            }
            if (OnReplyBoxDetection != null)
            {
                var args = new OnReplyBoxDetectionEventArgs(hasBox);
                OnReplyBoxDetection.Invoke(this, args);
            }
        }
        public event EventHandler OnReplyBoxDetection = null;
        public void ResetOnReplyBoxDetection() { OnReplyBoxDetection = null; }
        public class OnReplyBoxDetectionEventArgs : EventArgs
        {
            public bool HasBox { get; private set; }
            private OnReplyBoxDetectionEventArgs() { }
            public OnReplyBoxDetectionEventArgs(bool hasBox) { HasBox = hasBox; }
        }


        /// <summary>Event TrayArrive (115)</summary>
        /// <param name="reply"></param>
        private void TrayArrive(ReplyMessage reply)
        {
            TrayArriveType trayArriveType = (TrayArriveType)((int)reply.Value);
            if (OnTrayArriveHandler != null)
            {
                var args = new OnTrayArriveEventArgs(trayArriveType);
                OnTrayArriveHandler.Invoke(this,args);
            }
        }
        public event EventHandler OnTrayArriveHandler = null;
        public void ResetOnTrayArriveHandler() { OnTrayArriveHandler = null; }
        public class OnTrayArriveEventArgs : EventArgs
        {
            public TrayArriveType TrayArriveType { get; private set; }
            private OnTrayArriveEventArgs() { }
            public OnTrayArriveEventArgs(TrayArriveType trayArriveType) : this() { TrayArriveType = TrayArriveType; }
        }

        /// <summary>Event ButtonEvent(120)</summary>
        /// <param name="reply"></param>
        public void ButtonEvent(ReplyMessage reply)
        {
            if (OnButtonEventHandler != null)
            {
                OnButtonEventHandler.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler OnButtonEventHandler = null;
        public void ResetOnButtonEventHandler() { OnButtonEventHandler = null; }

        /// <summary>Event TimeOutEvent(900)</summary>
        /// <param name="reply"></param>
        private void TimeOutEvent(ReplyMessage reply)
        {
            if( OnTimeOutEventHandler != null)
            {
                OnTimeOutEventHandler.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler OnTimeOutEventHandler = null;
        public void ResetOnTimeOutEventHandler() { OnTimeOutEventHandler = null; }

        /// <summary>Event TrayMotioning(901)</summary>
        /// <param name="reply"></param>
        private void TrayMotioning(ReplyMessage reply)
        {
            if(OnTrayMotioningHandler != null)
            {
                OnTrayMotioningHandler.Invoke(this,EventArgs.Empty);
            }
        }
        public event EventHandler OnTrayMotioningHandler = null;
        public void ResetOnTrayMotioning() {  OnTrayMotioningHandler = null; }

        /// <summary>event INIFailed (902)</summary>
        /// <param name="reply"></param>
        private void INIFailed(ReplyMessage reply)
        {
            if (OnINIFailedHandler != null)
            {
               OnINIFailedHandler.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler OnINIFailedHandler = null;
        public void ResetOnINIFailedHandler() { OnINIFailedHandler = null; }

        /// <summary>Event TrayMotionError(903)</summary>
        /// <param name="reply"></param>
        public void TrayMotionError(ReplyMessage reply)
        {
            if (OnTrayMotionErrorHandler != null)
            {
                OnTrayMotionErrorHandler.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler OnTrayMotionErrorHandler = null;
        public void ResetOnTrayMotionErrorHandler()
        {    OnTrayMotionErrorHandler = null;}

        /// <summary>Event Error(904)</summary>
        /// <param name="reply"></param>
        public void Error(ReplyMessage reply)
        {
            ReplyErrorCode replyErrorCode = (ReplyErrorCode)((int)reply.Value);
            if (OnErrorHandler != null)
            {
                var args = new OnErrorEventArgs(replyErrorCode);
                OnErrorHandler.Invoke(this, args);
            }
        }
        public event EventHandler OnErrorHandler = null;
        public void ResetOnErrorHandler() { OnErrorHandler = null; }
        public class OnErrorEventArgs : EventArgs
        {
            public ReplyErrorCode ReplyErrorCode { get; private set; }
            private OnErrorEventArgs() { }
            public OnErrorEventArgs(ReplyErrorCode replyErrorCode) : this()
            {
                ReplyErrorCode = replyErrorCode;
            }
        }

        /// <summary>Event SysStartUp(999)</summary>
        /// <param name="reply"></param>
        public void SysStartUp(ReplyMessage reply)
        {
            if(OnSysStartUpHandler != null)
            {
                OnSysStartUpHandler.Invoke(this,EventArgs.Empty);
            }
        }
        public event EventHandler OnSysStartUpHandler = null;
        public void ResetOnSysStartUp(){OnSysStartUpHandler = null;}


        #endregion
    }
}
