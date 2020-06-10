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
            var methodName = rtnContent.StringContent.Replace(" ", "_");//.Replace("\0", "");
            try
            {
                var method = typeof(LoadPort).GetMethod(methodName);
                method.Invoke(this, new object[] { rtnContent });
            }
            catch (Exception ex)
            {

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

                //rtn = "~001,Placement,0@\0\0\0\0";

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

        #region Event 
        /// <summary> [001)</summary>
        /// <param name="rtnFromServer"></param>
        public void Placement(ReturnFromServer rtnFromServer)
        {
            var rtnCode = (EventPlacementCode)(Convert.ToInt32(rtnFromServer.ReturnCode));
            InvokeNote("Placement", rtnFromServer.ReturnCode);
        }

        /// <summary> (002)</summary>
        /// <param name="rtnFromServer"></param>
        public void Present(ReturnFromServer rtnFromServer)
        {
            var rtnCode = (EventPresentCode)(Convert.ToInt32(rtnFromServer.ReturnCode));
            InvokeNote("Present", rtnFromServer.ReturnCode);
        }

        /// <summary> (003)</summary>
        /// <param name="rtnFromServer"></param>
        public void Clamper(ReturnFromServer rtnFromServer)
        {
            var rtnCode = (EventClamperCode)(Convert.ToInt32(rtnFromServer.ReturnCode));
            InvokeNote("Clamper", rtnFromServer.ReturnCode);
        }

        /// <summary> (004)</summary>
        /// <param name="rtnFromServer"></param>
        public void RFID(ReturnFromServer rtnFromServer)
        {
            var rfID = rtnFromServer.ReturnCode;
            InvokeNote("RFID", rtnFromServer.ReturnCode);
        }
        /// <summary> (005)</summary>
        /// <param name="rtnFromServer"></param>
        public void Barcode_ID(ReturnFromServer rtnFromServer)
        {
            var rtnCode = (EventPlacementCode)(Convert.ToInt32(rtnFromServer.ReturnCode));
            var barcodeID = rtnFromServer.ReturnValue;
            InvokeNote("Barcode_ID", rtnFromServer.ReturnCode);
        }
        /// <summary> (006)</summary>
        /// <param name="rtnFromServer"></param>
        public void ClamperUnlockComplete(ReturnFromServer rtnFromServer)
        {
            var rtnCode = (EventClamperUnlockCompleteCode)(Convert.ToInt32(rtnFromServer.ReturnCode));
            InvokeNote("ClamperUnlockComplete", rtnFromServer.ReturnCode);
        }

        /// <summary> (007)</summary>
        /// <param name="rtnFromServer"></param>
        public void VacuumComplete(ReturnFromServer rtnFromServer)
        {
            var rtnCode = (EventVacuumCompleteCode)(Convert.ToInt32(rtnFromServer.ReturnCode));
            InvokeNote("VacuumComplete", rtnFromServer.ReturnCode);
        }

        /// <summary> (008)</summary>
        /// <param name="rtnFromServer"></param>
        public void DockPODStart(ReturnFromServer rtnFromServer)
        {
             InvokeNote("VacuumComplete");
        }
        /// <summary> (009)</summary>
        /// <param name="rtnFromServer"></param>
        public void DockPODComplete_HasReticle(ReturnFromServer rtnFromServer)
        {
             InvokeNote("DockPODComplete_HasReticle");
        }
        /// <summary> (010)</summary>
        /// <param name="rtnFromServer"></param>
        public void DockPODComplete_Empty(ReturnFromServer rtnFromServer)
        {
           
            InvokeNote("DockPODComplete_Empty");
        }
        /// <summary> (011)</summary>
        /// <param name="rtnFromServer"></param>
        public void UndockComplete(ReturnFromServer rtnFromServer)
        {
           InvokeNote("UndockComplete");
        }
        /// <summary> (012)</summary>
        /// <param name="rtnFromServer"></param>
        public void ClamperLockComplete(ReturnFromServer rtnFromServer)
        {
            InvokeNote("ClamperLockComplete");
        }
        /// <summary> (013)</summary>
        /// <param name="rtnFromServer"></param>
        public void AlarmResetSuccess(ReturnFromServer rtnFromServer)
        {
            InvokeNote("AlarmResetSuccess");
        }

        /// <summary> (014)</summary>
        /// <param name="rtnFromServer"></param>
        public void AlarmResetFail(ReturnFromServer rtnFromServer)
        {
            InvokeNote("AlarmResetFail");
        }
        /// <summary> (015)</summary>
        /// <param name="rtnFromServer"></param>
        public void ExecuteInitialFirst(ReturnFromServer rtnFromServer)
        {
            InvokeNote("ExecuteInitialFirst");
        }
        /// <summary> (016)</summary>
        /// <param name="rtnFromServer"></param>
        public void ExecuteAlarmResetFirst(ReturnFromServer rtnFromServer)
        {
            InvokeNote("ExecuteAlarmResetFirst");
        }

        /// <summary> (017)</summary>
        /// <param name="rtnFromServer"></param>
        public void StagePosition(ReturnFromServer rtnFromServer)
        {
            var rtnCode = (EventStagePositionCode)(Convert.ToInt32(rtnFromServer.ReturnCode));
            InvokeNote("StagePosition", rtnFromServer.ReturnCode);
        }

        /// <summary> (018)</summary>
        /// <param name="rtnFromServer"></param>
        public void LoadportStatus(ReturnFromServer rtnFromServer)
        {
            var rtnCode = (EventLoadportStatusCode)(Convert.ToInt32(rtnFromServer.ReturnCode));
            InvokeNote("LoadportStatus", rtnFromServer.ReturnCode);
        }

        /// <summary> (019]</summary>
        /// <param name="rtnFromServer"></param>
        public void InitialComplete(ReturnFromServer rtnFromServer)
        {
          
            InvokeNote("InitialComplete");
        }
        #endregion
        #region Alarm
        /// <summary>(200)</summary>
        /// <param name="rtnFromServer"></param>
        public void ClamperActionTimeOut(ReturnFromServer rtnFromServer)
        {
            InvokeNote("ClamperActionTimeOut");
        }
        /// <summary>(201)</summary>
        /// <param name="rtnFromServer"></param>
        public void ClamperUnlockPositionFailed(ReturnFromServer rtnFromServer)
        {
            InvokeNote("ClamperUnlockPositionFailed");
        }
        /// <summary>(202)</summary>
        /// <param name="rtnFromServer"></param>
        public void VacuumAbnormality(ReturnFromServer rtnFromServer)
        {
            InvokeNote("VacuumAbnormality");
        }
        /// <summary>(203)</summary>
        /// <param name="rtnFromServer"></param>
        public void StageMotionTimeout(ReturnFromServer rtnFromServer)
        {
            InvokeNote("StageMotionTimeout");
        }
        /// <summary>(204)</summary>
        /// <param name="rtnFromServer"></param>
        public void StageOverUpLimitation(ReturnFromServer rtnFromServer)
        {
            InvokeNote("StageOverUpLimitation");
        }
        /// <summary>(205)</summary>
        /// <param name="rtnFromServer"></param>
        public void StageOverDownLimitation(ReturnFromServer rtnFromServer)
        {
            InvokeNote("StageOverDownLimitation");
        }
        /// <summary>(206)</summary>
        /// <param name="rtnFromServer"></param>
        public void ReticlePositionAbnormality(ReturnFromServer rtnFromServer)
        {
            InvokeNote("ReticlePositionAbnormality");
        }

        /// <summary>(207)</summary>
        /// <param name="rtnFromServer"></param>
        public void ClamperLockPositionFailed(ReturnFromServer rtnFromServer)
        {
            InvokeNote("ClamperLockPositionFailed");
        }

        /// <summary>(208)</summary>
        /// <param name="rtnFromServer"></param>
        public void CoverDisappear(ReturnFromServer rtnFromServer)
        {
            InvokeNote("CoverDisappear");
        }

        /// <summary>(209)</summary>
        /// <param name="rtnFromServer"></param>
        public void ClamperMotorAbnormality(ReturnFromServer rtnFromServer)
        {
            InvokeNote("ClamperMotorAbnormality");
        }

        /// <summary>(210)</summary>
        /// <param name="rtnFromServer"></param>
        public void StageMotorAbnormality(ReturnFromServer rtnFromServer)
        {
            InvokeNote("StageMotorAbnormality");
        }
        #endregion



        private void InvokeNote(string methodName)
        {
            Debug.WriteLine("Invoke Method: " +  methodName);
        }
        private void InvokeNote(string methodName,string parameter)
        {
            Debug.WriteLine("Invoke Method: " + methodName + ", Parameter: " + parameter);
        }
       
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
        public string ReturnCode { get; set; }
        public string ReturnValue { get; set; }
        public ReturnFromServer(string content)
        {
            content = content.Replace(BaseHostToLoadPortCommand.CommandPrefixText, "").Replace(BaseHostToLoadPortCommand.CommandPostfixText, "");
            var contentAry = content.Split(new string[] { BaseHostToLoadPortCommand.CommandSplitSign }, StringSplitOptions.RemoveEmptyEntries);
            StringCode = contentAry[0];
            StringContent = contentAry[1];
            if (contentAry.Length >= 3)
            {
                ReturnCode= contentAry[2];
                if(contentAry.Length > 3)
                {
                    ReturnValue= contentAry[3];
                }
            }
            else
            {
                ReturnCode = null;
            }
        }
    }
}
