using MvaCToolkitCs.v1_2;
using MvaCToolkitCs.v1_2.Net;
using MvaCToolkitCs.v1_2.Protocol;
using MvAssistant.v0_3.DeviceDrive.GudengLoadPort.LoadPortEventArgs;
using MvAssistant.v0_3.DeviceDrive.GudengLoadPort.ReplyCode;
using MvAssistant.v0_3.DeviceDrive.GudengLoadPort.TCPCommand.HostToLoadPort;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.GudengLoadPort
{
    public class MvGudengLoadPortLdd : IDisposable
    {

        private OriginalInvokeMethod DelgateOriginalMethod = null;

        /// <summary>建構式</summary>
        public MvGudengLoadPortLdd()
        {

            EhReceviceRtnFromServer += ReceiveMessageFromServer;
        }

        /// <summary>建構式</summary>
        /// <param name="serverEndpoint">Server 端 Endpoint</param>
        /// <param name="loadportNo">Load Port 序號</param>
        public MvGudengLoadPortLdd(IPEndPoint serverEndpoint, int loadportNo) : this()
        {
            ServerEndPoint = serverEndpoint;
            LoadPortNo = loadportNo;
        }

        /// <summary>建構式</summary>
        /// <param name="serverIP">Server 端IP</param>
        /// <param name="serverPort">Server Port</param>
        /// <param name="loadportNo">Load port序號</param>
        public MvGudengLoadPortLdd(string serverIP, int serverPort, int loadportNo) : this(new IPEndPoint(IPAddress.Parse(serverIP), serverPort), loadportNo)
        {

        }

        public MvGudengLoadPortLdd(string serverIP, int serverPort, string index) : this()
        {
            ServerEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
            Index = index;
        }

        public delegate string OriginalInvokeMethod();
        public bool HasInvokeOriginalMethod
        {
            get
            {
                if (DelgateOriginalMethod == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public string Index { get; set; }
        /// <summary>Load Port 編號</summary>
        public int LoadPortNo { get; private set; }

        public void ClearOriginalMethod()
        {
            DelgateOriginalMethod = null;
        }

        public void InvokeOriginalMethod()
        {
            if (DelgateOriginalMethod != null)
            {
                DelgateOriginalMethod.Invoke();
            }
        }


        public void SetOriginalMethod(OriginalInvokeMethod delegateMethod)
        {
            DelgateOriginalMethod = delegateMethod;
        }





        /// <summary>Client(本地)端 Listen 收到 Server 端回傳資料後引發的事件程序</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ReceiveMessageFromServer(object sender, EventArgs args)
        {
            var eventArgs = (OnReceviceRtnFromServerEventArgs)args;
            ReturnFromServer rtnContent = new ReturnFromServer(eventArgs.RtnContent);
            var methodName = rtnContent.StringContent.Replace(" ", "_");//.Replace("\0", "");
            try
            {
                var method = typeof(MvGudengLoadPortLdd).GetMethod(methodName);
                method.Invoke(this, new object[] { rtnContent });
            }
            catch (Exception ex)
            {
                CtkLog.WarnAn(this, ex);
            }

        }





        #region Socket Communication

        List<Byte> DataBuffer = new List<byte>();
        Queue<String> DataMessage = new Queue<string>();


        /// <summary>啟動監聽 Server 端的 Thread</summary>
        public bool StartListenServerThread()
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
                CtkLog.WarnAn(this, ex);
                IsListenServer = false;
            }
            return IsListenServer;
        }


        /// <summary>Client(本地端) 端監 Server</summary>
        public Thread ThreadClientListen = null;

        /// <summary>本地端要送資料到 Server 端的 Client 物件</summary>
        private Socket ClientSocket = null;

        /// <summary>連線後遺失Host 和 TCP Server 間的連線</summary>
        public event EventHandler EhHostLostTcpServer;
        //   public event EventHandler OnHostCannotConnectToTcpServerHandler;
        /// <summary>收到 Server 端傳回資料時的事件處理程序</summary>
        private event EventHandler EhReceviceRtnFromServer = null;

        ///<summary>是否已完成監聽, 這個變數為 false 時, 均無法送出指令 </summary>
        public bool IsListenServer { get; private set; }

        /// <summary>Server 端 End point</summary>
        public IPEndPoint ServerEndPoint { get; private set; }
        /// <summary>監聽 Server 的Method</summary>
        private void ListenFromServer()
        {
            int rtnEmptyCount = 0;
            while (!this.disposed)
            {
                try
                {
                    //cmd = "~001,Placement,0@\0\0\0\0";
                    byte[] buffer = new byte[1023];
                    int cntReadByte = ClientSocket.Receive(buffer);//從Server端回復(Listen Point)
                    for (var idx = 0; idx < cntReadByte; idx++) this.DataBuffer.Add(buffer[idx]);


                    //--- Confirm 是否斷線 ---
                    var cmd = Encoding.Default.GetString(buffer, 0, cntReadByte);
                    cmd = cmd.Replace("\0", "");
                    if (string.IsNullOrEmpty(cmd))
                    {/*忽然斷線(將一直收到空白 50 次視為遺失連線)*/
                        if (++rtnEmptyCount > 50 && EhHostLostTcpServer != null)
                        {
                            EhHostLostTcpServer.Invoke(this, EventArgs.Empty);
                            break;
                        }
                        Thread.Sleep(100);
                        continue;
                    }
                    else { rtnEmptyCount = 0; }

                    //--- Receive Message ---
                    for (var idx = 0; idx < 99 && !this.disposed; idx++)
                    {/*一次最多99個, 也不應該累積到99個*/

                        while (this.DataBuffer.Count > 0 && this.DataBuffer[0] != '~')
                            this.DataBuffer.RemoveAt(0);//機率很低, 所以一個個移除比較好
                        var endIndex = this.DataBuffer.IndexOf((Byte)'@');

                        if (endIndex < 0) break;

                        var cmdArray = this.DataBuffer.Take(endIndex + 1).ToArray();
                        cmd = Encoding.Default.GetString(cmdArray, 0, cmdArray.Length);
                        this.DataBuffer.RemoveRange(0, endIndex + 1);
                        this.DataMessage.Enqueue(cmd);
                        CtkLog.DebugAn(this, "[LoadPort] " + cmd);
                    }

                    //--- Process Message ---
                    for (var idx = 0; idx < 99 && this.DataMessage.Count > 0 && !this.disposed; idx++)
                    {/*一次最多99個, 也不應該累積到99個*/
                        cmd = this.DataMessage.Dequeue();
                        cmd = cmd.Replace("\0", "");
                        if (EhReceviceRtnFromServer != null)
                        {
                            var eventArgs = new OnReceviceRtnFromServerEventArgs(cmd);
                            EhReceviceRtnFromServer.Invoke(this, eventArgs);
                        }
                    }

                }
                catch (Exception ex)
                {
                    CtkLog.WarnAn(this, ex);
                }
            }
        }
        /// <summary>送出 指令</summary>
        /// <param name="commandText">指令</param>
        private void Send(string commandText)
        {
            // Debug.WriteLine("[COMMAND]" + commandText);
            byte[] B = Encoding.Default.GetBytes(commandText);
            ClientSocket.Send(B, 0, B.Length, SocketFlags.None);
        }


        #endregion











        #region Command

        /// <summary>AlarmReset(109)</summary>
        /// <remarks>
        /// Main Event: 
        /// <para>AlarmResetSuccess</para>
        /// <para>.OR.</para>
        /// <para>AlarmResetFail</para>
        /// </remarks>
        public string CommandAlarmReset()
        {
            string command = "";
            if (IsListenServer)
            {
                command = new AlarmReset().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        /// <summary>Command AskBarcodeStatus(106)</summary>
        /// <remarks>Main Event: Barcode ID(Invoke: Barcode_ID)</remarks>
        public string CommandAskBarcodeStatus()
        {
            string command = "";
            if (IsListenServer)
            {
                command = new AskBarcodeStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        /// <summary>Command AskClamperStatus(104)</summary>
        /// <remarks>Main Event: Clamper</remarks>
        public string CommandAskClamperStatus()
        {
            string command = "";
            if (IsListenServer)
            {
                command = new AskClamperStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        /// <summary>Command AskLoadportStatus(111)</summary>
        /// <remarks>Main Event: LoadportStatus</remarks>
        public string CommandAskLoadportStatus()
        {
            string command = "";
            if (IsListenServer)
            {
                command = new AskLoadportStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        /// <summary>Command AskPlacementStatus(102)</summary>
        /// <remarks>Main Event: Placement</remarks>
        public string CommandAskPlacementStatus()
        {
            string command = "";
            if (IsListenServer)
            {

                command = new AskPlacementStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        /// <summary>Command AskPresentStatus(103)</summary>
        /// <remarks>Main Event: Present</remarks>
        public string CommandAskPresentStatus()
        {
            string command = "";
            if (IsListenServer)
            {
                command = new AskPresentStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        /// <summary>Command AskReticleExistStatus(108)</summary>
        /// <remarks>
        /// Main Event: 
        /// <para>DockPODComplete_HasReticle(009)</para><para>.OR.</para>
        /// <para>DockPODComplete_Empty(010)</para>
        /// </remarks>
        public string CommandAskReticleExistStatus()
        {
            string command = "";
            if (IsListenServer)
            {
                command = new AskReticleExistStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        /// <summary>Command AskRFIDStatus(105)</summary>
        /// <remarks>Main Event: RFID</remarks>
        public string CommandAskRFIDStatus()
        {
            string command = "";
            if (IsListenServer)
            {
                command = new AskRFIDStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        /// <summary>Command AskStagePosition(110)</summary>
        /// <remarks>Main Event: StagePosition</remarks>
        public string CommandAskStagePosition()
        {
            string command = "";
            if (IsListenServer)
            {
                command = new AskStagePosition().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        /// <summary>Command AskVacuumStatus(107)</summary>
        /// <remarks>Main Event: VacuumComplete</remarks>
        public string CommandAskVacuumStatus()
        {
            string command = "";
            if (IsListenServer)
            {
                command = new AskVacuumStatus().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        /// <summary>Command DockRequest(100)</summary>
        /// <remarks>Main Event: DockPODStart</remarks>
        public string CommandDockRequest()
        {

            string command = null;
            if (IsListenServer)
            {

                command = new DockRequest().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        /**
        /// <summary>Fake Command DockRequest(100)</summary>
        /// <remarks>
        /// 2020/10/23 14:20 King [C]
        /// </remarks>
        /// 
         public string FakeCommandDockRequest()
        {

            string command = null;
            command = new DockRequest().GetCommandText<IHostToLoadPortCommandParameter>(null);
            return command;
        }
        */

        /// <summary>Command Initilial Request(112)</summary>
        public string CommandInitialRequest()
        {
            string command = "";
            {
                if (IsListenServer)
                    command = new InitialRequest().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        /// <summary>Command ManualClamperLock</summary>
        /// <remarks>Main Event: Clamper</remarks>
        public string CommandManualClamperLock()
        {
            string command = "";
            if (IsListenServer)
            {
                command = new ManualClamperLock().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        public string CommandManualClamperOPR()
        {
            string command = "";
            if (IsListenServer)
            {
                command = new ManualClamperOPR().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        public string CommandManualClamperUnlock()
        {
            string command = "";
            if (IsListenServer)
            {
                command = new ManualClamperUnlock().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        public string CommandManualStageDown()
        {
            string command = "";
            if (IsListenServer)
            {
                command = new ManualStageDown().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        public string CommandManualStageInspection()
        {
            string command = "";
            if (IsListenServer)
            {
                command = new ManualStageInspection().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        public string CommandManualStageOPR()
        {
            string command = "";
            if (IsListenServer)
            {
                command = new ManualStageOPR().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        public string CommandManualStageUp()
        {
            string command = "";
            if (IsListenServer)
            {
                command = new ManualStageUp().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        public string CommandManualVacuumOff()
        {
            string command = "";
            if (IsListenServer)
            {
                command = new ManualVacuumOff().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        public string CommandManualVacuumOn()
        {
            string command = "";
            if (IsListenServer)
            {
                command = new ManualVacuumOn().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        /// <summary>Command UndockRequest(101)</summary>
        /// <remarks></remarks>
        public string CommandUndockRequest()
        {

            string command = "";
            if (IsListenServer)
            {

                command = new UndockRequest().GetCommandText<IHostToLoadPortCommandParameter>(null);
                Send(command);
            }
            return command;
        }

        /**
        /// <summary>Command UndockRequest(101)</summary>
        /// <remarks>
        /// 2020/10/23 14:24 King[C]
        /// </remarks>
        public string FakeCommandUndockRequest()
        {

            string command = "";
            command = new UndockRequest().GetCommandText<IHostToLoadPortCommandParameter>(null);
         //   Send(command);
            return command;
        }
        */
        /**
        /// <summary>AlarmReset(109)</summary>
        /// <remarks>
        /// 2020/10/23 14:12 King [C]
        /// </remarks>
        public string FakeCommandAlarmReset()
        {
            string command = "";
            command = new AlarmReset().GetCommandText<IHostToLoadPortCommandParameter>(null);
            return command;
        }
        */
        /**
        /// <summary>FakeCommand Initilial Request(112)</summary>
        /// <remarks>
        /// 2020/10/23 King [C]
        /// </remarks>
        public string FakeCommandInitialRequest()
        {
            string command = "";
            command = new InitialRequest().GetCommandText<IHostToLoadPortCommandParameter>(null);
            return command;
        }
        */
        #endregion


        #region Normal Event

        public event EventHandler EhAlarmResetFail = null;

        public event EventHandler EhAlarmResetSuccess = null;

        public event EventHandler EhBarcode_ID = null;

        public event EventHandler EhClamper = null;

        public event EventHandler EhClamperLockComplete = null;

        public event EventHandler EhClamperNotLock = null;

        public event EventHandler EhClamperUnlockComplete = null;

        public event EventHandler EhDockPODComplete_Empty = null;

        public event EventHandler EhDockPODComplete_HasReticle = null;

        public event EventHandler EhDockPODStart = null;

        public event EventHandler EhExecuteAlarmResetFirst = null;

        public event EventHandler EhExecuteInitialFirst = null;

        public event EventHandler EhInitialComplete = null;

        public event EventHandler EhInitialUnComplete = null;

        public event EventHandler EhLoadportStatus = null;

        public event EventHandler EhMustInAutoMode = null;

        public event EventHandler EhMustInManualMode = null;

        public event EventHandler EhPlacement = null;

        public event EventHandler EhPODNotPutProperly = null;

        public event EventHandler EhPresent = null;

        public event EventHandler EhRFID = null;

        public event EventHandler EhStagePosition = null;

        public event EventHandler EhUndockComplete = null;

        public event EventHandler EhVacuumComplete = null;

        /// <summary>Event AlarmResetFail(014)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>異常恢復失敗</remarks>
        public void AlarmResetFail(ReturnFromServer rtnFromServer)
        {

            if (EhAlarmResetFail != null)
            {
                EhAlarmResetFail.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>Event AlarmResetSuccess (013)</summary>
        /// <remarks>異常恢復成功</remarks>
        /// <param name="rtnFromServer"></param>
        public void AlarmResetSuccess(ReturnFromServer rtnFromServer)
        {


            if (EhAlarmResetSuccess != null)
            {
                EhAlarmResetSuccess.Invoke(this, EventArgs.Empty);
            }

        }

        /// <summary>Event Barcode ID (005)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. Load貨完成讀取Barcode後</para>
        /// <para>2. 收到AskBarcodeStatus</para>
        /// </remarks>
        public void Barcode_ID(ReturnFromServer rtnFromServer)
        {
            // rtnCode: 讀取成功或失敗
            var rtnCode = (EventBarcodeIDCode)(Convert.ToInt32(rtnFromServer.ReturnCode));
            // barcodeID: 讀取成功時的 barcode ID  
            var barcodeID = rtnFromServer.ReturnValue;

            if (EhBarcode_ID != null)
            {
                var eventArgs = new OnBarcode_IDEventArgs(rtnCode, barcodeID);
                EhBarcode_ID.Invoke(this, eventArgs);
            }
        }

        /// <summary>Event Clamper(003)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. Clamper 狀態改變</para>
        /// <para>2. 收到 AskClamperStatus</para>
        /// </remarks>
        public void Clamper(ReturnFromServer rtnFromServer)
        {
            //rtnCode: Clamper狀態(關閉, 開啟, 不在定位需復歸)
            var rtnCode = (EventClamperCode)(Convert.ToInt32(rtnFromServer.ReturnCode));

            if (EhClamper != null)
            {
                var eventArgs = new OnClamperEventArgs(rtnCode);
                EhClamper.Invoke(this, eventArgs);
            }
        }

        /// <summary>Event ClamperLockComplete(006)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. Load貨完成後開啟Clamper</para>
        /// <para>2. 收到AskClamperStatus</para>
        /// </remarks>
        public void ClamperLockComplete(ReturnFromServer rtnFromServer)
        {
            // rtnCode: Clamper 關閉/開啟  
            var rtnCode = (EventClamperLockCompleteCode)(Convert.ToInt32(rtnFromServer.ReturnCode));

            if (EhClamperLockComplete != null)
            {
                var eventArgs = new OnClamperLockCompleteEventArgs(rtnCode);
                EhClamperLockComplete.Invoke(this, eventArgs);
            }
        }

        /// <summary>Event ClamperNotLock(022)</summary>
        /// <param name="rtnFromServer"></param>
        public void ClamperNotLock(ReturnFromServer rtnFromServer)
        {

            if (EhClamperNotLock != null)
            {
                EhClamperNotLock.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>Event ClamperUnlockComplete(012)</summary>
        /// <param name="rtnFromServer"></param>
        public void ClamperUnlockComplete(ReturnFromServer rtnFromServer)
        {

            if (EhClamperUnlockComplete != null)
            {
                EhClamperUnlockComplete.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>Event  DockPODComplete_Empty(010)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. StageDock完畢後</para>
        /// <para>2. 收到AskReticleExistStatus</para>
        /// </remarks>
        public void DockPODComplete_Empty(ReturnFromServer rtnFromServer)
        {


            if (EhDockPODComplete_Empty != null)
            {
                EhDockPODComplete_Empty.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>Event  DockPODComplete_HasReticle(009)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. StageDock完畢後</para>
        /// <para>2. 收到AskReticleExistStatus</para>
        /// </remarks>
        public void DockPODComplete_HasReticle(ReturnFromServer rtnFromServer)
        {

            if (EhDockPODComplete_HasReticle != null)
            {
                EhDockPODComplete_HasReticle.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>Event  DockPODStart(008)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>收到DockRequest後, 開始移動Stage前</remarks>
        public void DockPODStart(ReturnFromServer rtnFromServer)
        {

            if (EhDockPODStart != null)
            {
                EhDockPODStart.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>Event AlarmResetFirst (016)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>提示進行異常復歸動作</remarks>
        public void ExecuteAlarmResetFirst(ReturnFromServer rtnFromServer)
        {

            if (EhExecuteAlarmResetFirst != null)
            {
                EhExecuteAlarmResetFirst.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>Event  ExecuteInitialFirst(015)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>提示進行原點復歸動作</remarks>
        public void ExecuteInitialFirst(ReturnFromServer rtnFromServer)
        {

            if (EhExecuteInitialFirst != null)
            {
                EhExecuteInitialFirst.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>Event Initial Complete(019)</summary>
        /// <remarks>初始化完畢後</remarks>
        /// <param name="rtnFromServer"></param>
        public void InitialComple(ReturnFromServer rtnFromServer)
        {
            /*[d20220113] 名稱必須為 InitialComple, 這是 Gudeng Load Port 回傳的字串*/
            this.InitialComplete(rtnFromServer);
        }

        public void InitialComplete(ReturnFromServer rtnFromServer)
        {
            /*[d20220113] 名稱必須為 InitialComple, 這是 Gudeng Load Port 回傳的字串*/

            if (EhInitialComplete != null)
            {
                EhInitialComplete.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// <para>傳送Initial Request 時沒有在指定時間內收到 InitialComplete 事件</para>
        /// <para>原始文件中未定義這個事件, 本事件為自定</para>
        /// </summary>
        /// <param name="rtnFromServer"></param>
        public void InitialUnComplete(/*ReturnFromServer rtnFromServer*/)
        {

            if (EhInitialUnComplete != null)
            {
                EhInitialUnComplete.Invoke(this, EventArgs.Empty);
            }
        }

        public void LoadportStatu2(ReturnFromServer rtnFromServer) { this.LoadportStatus(rtnFromServer); }

        /// <summary>Event LoadportStatus(018)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. Loadport內部狀態改變</para>
        /// <para>2. 收到AskLoadportStatus</para>
        /// </remarks>
        public void LoadportStatus(ReturnFromServer rtnFromServer)
        {
            var rtnCode = (EventLoadportStatusCode)(Convert.ToInt32(rtnFromServer.ReturnCode));

            if (EhLoadportStatus != null)
            {
                var eventArgs = new OnLoadportStatusEventArgs(rtnCode);
                EhLoadportStatus.Invoke(this, eventArgs);
            }

        }

        /// <summary>Event MustInAutoMode(020)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>2020/06/11 new</remarks>
        public void MustInAutoMode(ReturnFromServer rtnFromServer)
        {

            if (EhMustInAutoMode != null)
            {
                EhMustInAutoMode.Invoke(this, EventArgs.Empty);
            }
        }

        //~021,MustInManualMode@
        public void MustInManualMode(ReturnFromServer rtnFromServer)
        {

            if (EhMustInManualMode != null)
            {
                EhMustInManualMode.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>Event Placement (001)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. placement 狀態改變</para>
        /// <para>2. 收到 AskPlacement </para>
        /// </remarks>
        public void Placement(ReturnFromServer rtnFromServer)
        {
            // rtnCode: 有無Placement 信號
            var rtnCode = (EventPlacementCode)(Convert.ToInt32(rtnFromServer.ReturnCode));

            var eventArgs = new OnPlacementEventArgs(rtnCode);
            if (EhPlacement != null)
            {
                EhPlacement.Invoke(this, eventArgs);
            }
        }
        /// <summary>Event PODNotPutProperly(023)</summary>
        /// <remarks>2020/06/11 new</remarks>
        public void PODNotPutProperly(ReturnFromServer rtnFromServer)
        {

            if (EhPODNotPutProperly != null)
            {
                EhPODNotPutProperly.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>Event Present(002)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. Present狀態改變</para>
        /// <para>2. 收到 AskPresentStatus</para>
        /// </remarks>
        public void Present(ReturnFromServer rtnFromServer)
        {
            // rtnCode: 有無Present訊息
            var rtnCode = (EventPresentCode)(Convert.ToInt32(rtnFromServer.ReturnCode));

            if (EhPresent != null)
            {
                var eventArgs = new OnPresentEventArgs(rtnCode);
                EhPresent.Invoke(this, eventArgs);
            }
        }

        public void ResetDockOnPODStartHandler() { EhDockPODStart = null; }

        public void ResetExecuteOnAlarmResetFirstHandler() { EhExecuteAlarmResetFirst = null; }

        public void ResetInitialOnCompleteHandler() { EhInitialComplete = null; }

        public void ResetMustInManualModeHandler() { EhMustInAutoMode = null; }

        public void ResetOnAlarmResetFailHandler() { EhAlarmResetFail = null; }

        public void ResetOnAlarmResetSuccessHandler() { EhAlarmResetSuccess = null; }

        public void ResetOnBarcode_IDHandler() { EhBarcode_ID = null; }

        public void ResetOnClamperHandler() { EhClamper = null; }

        public void ResetOnClamperLockCompleteHandler() { EhClamperLockComplete = null; }

        public void ResetOnClamperNotLockHandler() { EhClamperNotLock = null; }

        public void ResetOnClamperUnlockCompleteHandler() { EhClamperUnlockComplete = null; }

        public void ResetOnDockPODComplete_EmptyHandler() { EhDockPODComplete_Empty = null; }

        public void ResetOnDockPODComplete_HasReticleHandler() { EhDockPODComplete_HasReticle = null; }

        public void ResetOnExecuteInitialFirstHandler() { EhExecuteInitialFirst = null; }

        public void ResetOnInitialUnCompleteHandler() { EhInitialUnComplete = null; }

        public void ResetOnLoadportStatusHandler() { EhLoadportStatus = null; }

        public void ResetOnMustInAutoModeHandler() { EhMustInAutoMode = null; }

        public void ResetOnPlacementHandler() { EhPlacement = null; }
        public void ResetOnPODNotPutProperlyHandler() { EhPODNotPutProperly = null; }

        public void ResetOnPresentHandler() { EhPresent = null; }
        public void ResetOnRFIDHandler() { EhRFID = null; }

        public void ResetOnStagePositionHandler() { EhStagePosition = null; }

        public void ResetOnUndockCompleteHandler() { EhUndockComplete = null; }

        public void ResetOnVacuumCompleteHandler() { EhVacuumComplete = null; }

        /// <summary>Event RFID(004)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. Load貨, 完成讀取RFID後</para>
        /// <para>2. 收到AskRFIDStatus</para>
        /// </remarks>
        public void RFID(ReturnFromServer rtnFromServer)
        {   // rfID: 讀取的 RFID
            var rfID = rtnFromServer.ReturnCode;

            if (EhRFID != null)
            {
                var eventArgs = new OnRFIDEventArgs(rfID);
                if (EhRFID != null)
                {
                    EhRFID.Invoke(this, eventArgs);
                }
            }
        }
        /// <summary>Event StagePosition (017)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. Stage位置改變</para>
        /// <para>2. 收到AskStagePosition</para>
        /// </remarks>
        public void StagePosition(ReturnFromServer rtnFromServer)
        {
            // rtnCode: Stage 位置
            var rtnCode = (EventStagePositionCode)(Convert.ToInt32(rtnFromServer.ReturnCode));

            if (EhStagePosition != null)
            {
                var eventArgs = new OnStagePositionEventArgs(rtnCode);
                EhStagePosition.Invoke(this, eventArgs);
            }
        }

        /// <summary>Event UndockComplete(011)</summary>
        /// <param name="rtnFromServer"></param>
        public void UndockComplete(ReturnFromServer rtnFromServer)
        {

            if (EhUndockComplete != null)
            {
                EhUndockComplete.Invoke(this, EventArgs.Empty);
            }
        }

        public void VacuumComplet0(ReturnFromServer rtnFromServer) { this.VacuumComplete(rtnFromServer); }
        public void VacuumComplet1(ReturnFromServer rtnFromServer) { this.VacuumComplete(rtnFromServer); }

        /// <summary>Event VacummComplete(007)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>
        /// <para>1. Load貨完成後底盤吸住</para>
        /// <para>2. 收到AskVacuumStatus</para>
        /// <para>3. 底座真空狀態改變</para>
        /// </remarks>
        public void VacuumComplete(ReturnFromServer rtnFromServer)
        {
            // rtnCode: 是否建立真空
            var rtnCode = (EventVacuumCompleteCode)(Convert.ToInt32(rtnFromServer.ReturnCode));

            if (EhVacuumComplete != null)
            {
                var eventArgs = new OnVacuumCompleteEventArgs(rtnCode);
                EhVacuumComplete.Invoke(this, eventArgs);
            }
        }
        #endregion


        #region Alarm Event

        public event EventHandler EhClamperActionTimeOut = null;

        public event EventHandler EhClamperLockPositionFailed = null;

        public event EventHandler EhClamperMotorAbnormality = null;

        public event EventHandler EhClamperUnlockPositionFailed = null;

        public event EventHandler EhPODPresentAbnormality = null;

        public event EventHandler EhReticlePositionAbnormality = null;

        public event EventHandler EhStageMotionTimeout = null;

        public event EventHandler EhStageMotorAbnormality = null;

        public event EventHandler EhStageOverDownLimitation = null;

        public event EventHandler EhStageOverUpLimitation = null;

        public event EventHandler EhVacuumAbnormality = null;

        /// <summary>Alarm ClamperActionTimeOut(200)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>Clamper馬達運動超時</remarks>
        public void ClamperActionTimeOut(ReturnFromServer rtnFromServer)
        {

            if (EhClamperActionTimeOut != null)
            { EhClamperActionTimeOut.Invoke(this, EventArgs.Empty); }
        }
        /// <summary>Alarm ClamperLockPositionFailed(207)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>ClamerLock完成後位置錯誤</remarks>
        public void ClamperLockPositionFailed(ReturnFromServer rtnFromServer)
        {
            if (EhClamperLockPositionFailed != null) { EhClamperLockPositionFailed.Invoke(this, EventArgs.Empty); }
        }
        /// <summary>Alarm ClamperMotorAbnormality(209)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>Clamper開合馬達驅動器異常</remarks>
        public void ClamperMotorAbnormality(ReturnFromServer rtnFromServer)
        {
            if (EhClamperMotorAbnormality != null) { EhClamperMotorAbnormality.Invoke(this, EventArgs.Empty); }
        }
        /// <summary>Alarm ClamperUnlockPositionFailed(201)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>ClamerUnlock完成後位置錯誤</remarks>
        public void ClamperUnlockPositionFailed(ReturnFromServer rtnFromServer)
        {

            if (EhClamperUnlockPositionFailed != null)
            { EhClamperUnlockPositionFailed.Invoke(this, EventArgs.Empty); }
        }
        /// <summary>Alarm PODPresentAbnormality(208)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>POD上下蓋脫離定位</remarks>
        public void PODPresentAbnormality(ReturnFromServer rtnFromServer)
        {
            if (EhPODPresentAbnormality != null) { EhPODPresentAbnormality.Invoke(this, EventArgs.Empty); }
        }
        public void ResetOnClamperActionTimeOutHandler() { EhClamperActionTimeOut = null; }
        public void ResetOnClamperLockPositionFailed() { EhClamperLockPositionFailed = null; }
        public void ResetOnClamperMotorAbnormality()
        { EhClamperMotorAbnormality = null; }
        public void ResetOnClamperUnlockPositionFailedHandler() { EhClamperUnlockPositionFailed = null; }
        public void ResetOnPODPresentAbnormalityHandler() { EhPODPresentAbnormality = null; }
        public void ResetOnReticlePositionAbnormalityHandler() { EhReticlePositionAbnormality = null; }
        public void ResetOnStageMotionTimeoutHandler() { EhStageMotionTimeout = null; }
        public void ResetOnStageMotorAbnormality() { EhStageMotorAbnormality = null; }
        public void ResetOnStageOverDownLimitationHandler() { EhStageOverDownLimitation = null; }
        public void ResetOnStageOverUpLimitationHandler() { EhStageOverUpLimitation = null; }
        public void ResetOnVacuumAbnormalityHandler() { EhVacuumAbnormality = null; }
        /// <summary>Alarm ReticlePositionAbnormality(206)</summary>
        /// <param name="rtnFromServer"></param>
        ///<remarks>Dock/Undock時, 光罩滑出POD</remarks>
        public void ReticlePositionAbnormality(ReturnFromServer rtnFromServer)
        {
            if (EhReticlePositionAbnormality != null)
            {
                EhReticlePositionAbnormality.Invoke(this, EventArgs.Empty);
            }
        }
        /// <summary>Alarm StageMotionTimeout(203)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>Stage運動超時</remarks>
        public void StageMotionTimeout(ReturnFromServer rtnFromServer)
        {

            if (EhStageMotionTimeout != null) { EhStageMotionTimeout.Invoke(this, EventArgs.Empty); }
        }
        /// <summary>Alarm StageMotorAbnormality(210)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>Stage升降馬達驅動器異常</remarks>
        public void StageMotorAbnormality(ReturnFromServer rtnFromServer)
        {
            if (EhStageMotorAbnormality != null) { EhStageMotorAbnormality.Invoke(this, EventArgs.Empty); }
        }
        /// <summary>Alarm StageOverDownLimitation(205)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>Stage下位限制Sensor觸發</remarks>
        public void StageOverDownLimitation(ReturnFromServer rtnFromServer)
        {
            if (EhStageOverDownLimitation != null) { EhStageOverDownLimitation.Invoke(this, EventArgs.Empty); }
        }
        /// <summary>Alarm StageOverUpLimitation(204)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>Stage上位限制Sensor觸發</remarks>
        public void StageOverUpLimitation(ReturnFromServer rtnFromServer)
        {
            if (EhStageOverUpLimitation != null) { EhStageOverUpLimitation.Invoke(this, EventArgs.Empty); }
        }
        /// <summary>Alarm VacuumAbnormality(202)</summary>
        /// <param name="rtnFromServer"></param>
        /// <remarks>StageDock/Undock前真空值錯誤</remarks>
        public void VacuumAbnormality(ReturnFromServer rtnFromServer)
        {

            if (EhVacuumAbnormality != null)
            {
                EhVacuumAbnormality.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion





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

            this.DisposeClose();

            disposed = true;
        }

        protected virtual void DisposeClose()
        {
            if (this.ClientSocket != null)
            {
                CtkNetUtil.DisposeSocketTry(this.ClientSocket);
            }

        }

        #endregion

    }







}
