using MvAssistant.DeviceDrive.GudengLoadPort.TCPCommand.HostToLoadPort;
using System;
using System.Collections.Generic;
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

        public LoadPort(IPEndPoint serverEndpoint,int loadportNo)
        {
            ServerEndPoint = serverEndpoint;
            LoadPortNo = loadportNo;
        }
        public LoadPort(string serverIP,int serverPort,int loadportNo):this(new IPEndPoint(IPAddress.Parse(serverIP),serverPort),loadportNo )
        {
            
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
                string message = Encoding.Default.GetString(B, 0, inLine);
            }
        }

        private void Send(string sendText)
        {
            byte[] B = Encoding.Default.GetBytes(sendText);
           ClientSocket.Send(B, 0, B.Length, SocketFlags.None);
        }
        #region Command
        public void CommandInitialRequest()
        { }

        public void CommandDockRequest()
        { }

        public void CommandUndockRequest()
        { }

        public void CommandAskPlacementStatus()
        { }

        public void CommandAskPresentStatus()
        { }

        public void CommandAskClamperStatus()
        {
           var command= new AskClamperStatus().GetCommandText();
            Send(command);
        }

        public void CommandAskRFIDStatus()
        { }

        public void CommandAskBarcodeStatus()
        { }

        public void CommandAskVacuumStatus()
        { }

        public void CommandAskReticleExistStatus()
        { }

        public void AlarmReset()
        { }

        public  void CommandAskStagePosition()
        {}

        public void CommandAskLoadportStatus()
        { }

        public void CommandManualClamperLock()
        {}

        public void CommandManualClamperUnlock()
        { }

        public void CommandManualClamperOPR()
        { }

        public void CommandManualStageUp()
        { }

        public void CommandManualStageInspection()
        { }

        public void CommandManualStageDown()
        {}

        public void CommandManualStageOPR()
        { }

        public void CommandManualVacuumOn()
        { }

        public void CommandManualVacuumOff()
        { }
        #endregion 

        #region Event & Alarm

        #endregion

    }


    public class HostTcpServer
    {
     
    }
    public class HostTcpClient
    {
        
    }
    public class EquipmentTcpServer
    {
        
    }

    public class EquipmentTcpClient
    {

    }

    
}
