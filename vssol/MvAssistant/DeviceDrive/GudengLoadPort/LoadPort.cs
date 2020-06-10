using MvAssistant.DeviceDrive.GudengLoadPort.TCPCommand.HostToLoadPort;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.GudengLoadPort
{
   public class LoadPort
    {
        public int LoadPortNo { get; private set; }
        public IPEndPoint ServerEndPoint { get; private set; }
        private Socket ClientSocket = null; 
        public bool IsListenServer { get; private set; }
        public Thread ThreadClientListen = null;
        private event EventHandler OnReceviceRtnFromServerHandler=null;
        public LoadPort()
        {
            OnReceviceRtnFromServerHandler += ReceiveMessageFromServer;
        }
        public LoadPort(IPEndPoint serverEndpoint,int loadportNo):this()
        {
            ServerEndPoint = serverEndpoint;
            LoadPortNo = loadportNo;
        }
        public LoadPort(string serverIP,int serverPort,int loadportNo):this(new IPEndPoint(IPAddress.Parse(serverIP),serverPort),loadportNo )
        {
            
        }

        private void ReceiveMessageFromServer(object sender, EventArgs args)
        {
            var eventArgs = (OnReceviceRtnFromServerEventArgs)args;
            ReturnFromServer rtnContent = new ReturnFromServer(eventArgs.RtnContent);
            if (rtnContent.Parameter.HasValue)
            {
                typeof(LoadPort).GetMethod(rtnContent.StringContent).Invoke(this, new object[] { (int)rtnContent.Parameter });
            }
            else
            {
                typeof(LoadPort).GetMethod(rtnContent.StringContent).Invoke(this, null);
            }
        }

        public void  ListenServer()
        {
            try
            {
                ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ClientSocket.Connect(ServerEndPoint);
                ThreadClientListen = new Thread(ListenFromServer);
                ThreadClientListen.Start();
                IsListenServer = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void ListenFromServer()
        {
            while (true)
            {
                byte[] B = new byte[1023];
                int inLine = ClientSocket.Receive(B);//從Server端回復
                string rtn = Encoding.Default.GetString(B, 0, inLine);
                Debug.WriteLine("[RETURN] " + rtn);
                if (OnReceviceRtnFromServerHandler != null)
                {
                    var eventArgs = new OnReceviceRtnFromServerEventArgs(rtn);
                    OnReceviceRtnFromServerHandler.Invoke(this, eventArgs);
                }
            }
        }

        private void Send(string commandText)
        {
            Debug.WriteLine("[COMMAND] " + commandText);
            byte[] B = Encoding.Default.GetBytes(commandText);
           ClientSocket.Send(B, 0, B.Length, SocketFlags.None);
        }
        #region Command
        public void CommandInitialRequest()
        {
            if (IsListenServer)
            {
                var command = new InitialRequest().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandDockRequest()
        {
            if (IsListenServer)
            {
                var command = new DockRequest().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandUndockRequest()
        {
            if (IsListenServer)
            {
                var command = new UndockRequest().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandAskPlacementStatus()
        {
            if (IsListenServer)
            {
                var command = new AskPlacementStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandAskPresentStatus()
        {
            if (IsListenServer)
            {
                var command = new AskPresentStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandAskClamperStatus()
        {
            if (IsListenServer)
            {
                var command = new AskClamperStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandAskRFIDStatus()
        {
            if (IsListenServer)
            {
                var command = new AskRFIDStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandAskBarcodeStatus()
        {
            if (IsListenServer)
            {
                var command = new AskBarcodeStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandAskVacuumStatus()
        {
            if (IsListenServer)
            {
                var command = new AskVacuumStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandAskReticleExistStatus()
        {
            if (IsListenServer)
            {
                var command = new AskReticleExistStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandAlarmReset()
        {
            if (IsListenServer)
            {
                var command = new AlarmReset().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public  void CommandAskStagePosition()
        {
            if (IsListenServer)
            {
                var command = new AskStagePosition().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandAskLoadportStatus()
        {
            if (IsListenServer)
            {
                var command = new AskLoadportStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandManualClamperLock()
        {
            if (IsListenServer)
            {
                var command = new ManualClamperLock().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandManualClamperUnlock()
        {
            if (IsListenServer)
            {
                var command = new ManualClamperUnlock().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandManualClamperOPR()
        {
            if (IsListenServer)
            {
                var command = new ManualClamperOPR().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandManualStageUp()
        {
            if (IsListenServer)
            {
                var command = new ManualStageUp().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandManualStageInspection()
        {
            if (IsListenServer)
            {
                var command = new ManualStageInspection().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandManualStageDown()
        {
            if (IsListenServer)
            {
                var command = new ManualStageDown().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandManualStageOPR()
        {
            if (IsListenServer)
            {
                var command = new ManualStageOPR().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandManualVacuumOn()
        {
            if (IsListenServer)
            {
                var command = new ManualVacuumOn().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }

        public void CommandManualVacuumOff()
        {
            if (IsListenServer)
            {
                var command = new ManualVacuumOff().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
        }
        #endregion 

        #region Event & Alarm

        #endregion

    }

    public class OnReceviceRtnFromServerEventArgs : EventArgs
    {
        public string RtnContent { get; private set; }
        public OnReceviceRtnFromServerEventArgs(string rtnContent)
        {
            RtnContent = rtnContent;
        }
    }

    public class ReturnFromServer
    {
        public string StringCode { get; set; }
        public string StringContent { get; set; }
        public int? Parameter { get; set; }
        public ReturnFromServer(string content)
        {
            content = content.Replace(BaseHostToLoadPortCommand.CommandPrefixText, "").Replace(BaseHostToLoadPortCommand.CommandPostfixText, "");
            var contentAry = content.Split(new string[] { BaseHostToLoadPortCommand.CommandSplitSign }, StringSplitOptions.RemoveEmptyEntries);
            StringCode = contentAry[0];
            StringCode = contentAry[1];
            if (contentAry.Length == 3)
            {
                Parameter= Convert.ToInt32(contentAry[2]);
            }
        }
    }
}
