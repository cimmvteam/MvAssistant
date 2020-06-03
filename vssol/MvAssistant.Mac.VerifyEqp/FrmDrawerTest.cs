using MvAssistant.Mac.Drawer_SocketComm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MvAssistantMacVerifyEqp
{
    public partial class FrmDrawerTest : Form
    {
        public Drawers Drawers = null;
        public UdpServerSocket ServerSocket = null;
        public FrmDrawerTest()
        {
            InitializeComponent();
            Drawers = new Drawers();
        }

        private void FrmDrawerTest_Load(object sender, EventArgs e)
        {
            IniTial_Drawers();
            Initial_Commands();
            ServerSocket = new UdpServerSocket(5000, Handler);
        }
        private void Initial_Commands()
        {
            IniTial_INICommand();
        }
        private void IniTial_Drawers()
        {
            Initial_Cabinet1Drawers();
            Initial_Cabinet2Drawers();
        }
        private void Initial_Cabinet1Drawers()
        {

        }
        private void Initial_Cabinet2Drawers()
        {

        }
        public void IniTial_INICommand()
        {
            var command = new INI_HE_Command();
            GrpBox_INI.Text=command.CommandCategory.ToString() + "(" + command.CommandCategory.GetStringCode() + ")";
        }

        public void Handler(string rcvMessage,string ip)
        {

        }
    }
    #region new Type

    /// <summary>Host to Equipment Command   </summary>
    public enum DrawerHostToEquipmentCommandCategory
    {
        SetMotionSpeed = 0,
        SetTimeOut = 1,
        SetParameter = 7,
        TrayMotion = 11,
        BrightLED = 12,
        PositionRead = 13,
        BoxDetection = 14,
        WriteNetSetting = 31,
        LCDMsg = 41,
        INI = 99,
    }

    /// <summary>Equipment to Host Command</summary>
    public enum DrawerEquipmentToHostCommandCategory
    {
        ReplyTrayMotion = 111,
        ReplySetSpeed = 100,
        ReplySetTimeOut = 101,
        ReplyPosition = 113,
        ReplyBoxDetection = 114,
        TrayArrive = 115,
        ButtonEvent = 120,
        TimeOutEvent = 900,
        TrayMotioning = 901,
        INIFailed = 902,
        TrayMotionError = 903,
        Error = 904,
        SysTartUp = 999,
    }

    /// <summary>DrawerHostToEquipmentCommandCategory 擴展方法</summary>
    public static class DrawerHostToEquipmentCommandCategoryExtends
    {
        /// <summary>取得 String Code</summary>
        /// <param name="inst"></param>
        /// <returns></returns>
        public static string GetStringCode(this DrawerHostToEquipmentCommandCategory inst)
        {
            string stringCode = ((int)inst).ToString("000");
            return stringCode;
        }
    }


    public abstract class BaseDrawerCommand
    {
        public const string CommandPrefixText = "~";
        public const string CommandPostfixText = "@";
        public const string CommandTextReplaceSignPair = "[]";
        public static string TestStatic = "4";
        protected abstract string GetRawCommandText();
        public abstract string GetCommandText(string parameterText);



    }

    public abstract class BaseDrawerEquipmentToHostCommand : BaseDrawerCommand
    {
        private DrawerHostToEquipmentCommandCategory _commandCategory;

        public const string ParaneterSignPair = "[]";
        private BaseDrawerEquipmentToHostCommand()
        {

        }
        public BaseDrawerEquipmentToHostCommand(DrawerHostToEquipmentCommandCategory commandCategory) : this()
        {
            _commandCategory = commandCategory;
        }
        public DrawerHostToEquipmentCommandCategory CommandCategory { get { return _commandCategory; } }
        protected override string GetRawCommandText()
        {
            var stringCode = CommandPrefixText + CommandCategory.GetStringCode() + "," + CommandCategory.ToString() + CommandTextReplaceSignPair + "@";
            return stringCode;
        }
        public override string GetCommandText(string paraneterText)
        {
            var commandText = GetRawCommandText().Replace(CommandTextReplaceSignPair, paraneterText);
            return commandText;
        }



    }

    public class HostToEquipCommands
    {

        private List<BaseDrawerEquipmentToHostCommand> Commands = null;
        public HostToEquipCommands()
        {
            Commands = new List<BaseDrawerEquipmentToHostCommand>();
            Commands.Add(new SetMotionSpeed_HE_Command());
            Commands.Add(new TrayMotion_HE_Command());
        }
        public BaseDrawerEquipmentToHostCommand GetCommandInst(DrawerHostToEquipmentCommandCategory commandCategory)
        {
            var command = Commands.Where(m => m.CommandCategory == commandCategory).FirstOrDefault();
            return command;
        }
    }


    public class SetMotionSpeed_HE_Command : BaseDrawerEquipmentToHostCommand
    {
        public SetMotionSpeed_HE_Command() : base(DrawerHostToEquipmentCommandCategory.SetMotionSpeed)
        {
        }
    }
    public class TrayMotion_HE_Command : BaseDrawerEquipmentToHostCommand
    {
        public TrayMotion_HE_Command() : base(DrawerHostToEquipmentCommandCategory.TrayMotion)
        {
        }
    }
    public class BrightLED_HE_Command : BaseDrawerEquipmentToHostCommand
    {
        public BrightLED_HE_Command() : base(DrawerHostToEquipmentCommandCategory.BrightLED)
        {
        }
    }

    public class INI_HE_Command:BaseDrawerEquipmentToHostCommand
    {
        public INI_HE_Command() : base(DrawerHostToEquipmentCommandCategory.INI)
        {
        }
    }

    public class Drawers
    {

        public List<Drawer> AllDrawers = null;
        public Drawers()
        {
            AllDrawers = new List<Drawer>();
        }
    }

    public class Drawer
    {

        public int CabinetNO { get; private set; }
        public int DrawerNO { get; private set; }
        public DrawerSocket DrawerSocket { get; private set; }
       
        private Drawer() { }
        public Drawer(int cabinetNO, int drawerNO,string clientIP, int clientPort) :this()
        {
            DrawerNO = drawerNO;
            CabinetNO = cabinetNO;
            DrawerSocket = new DrawerSocket(clientIP,  clientPort);
        }
        public int SentTo(string message)
        {
            var feedBack = DrawerSocket.SentTo(message);
            return feedBack;
        }
        public void ReceiveFrom()
        {

        }
    }
    public class DrawerSocket
    {
       
        /// <summary>Send(Target IP)</summary>
        public string ClientIP { get; set; }
         public int ClientPort { get; set; }
         private UdpClientSocket UdpClient { get; set; }
        public DrawerSocket(string clientIP, int clientPort)
        {
            UdpClient = new UdpClientSocket( clientIP,  clientPort);
         }
        public int SentTo(string message)
        {
            var feedBack = UdpClient.SenTo(message);
            return feedBack;
        }
    }
    public class UdpServerSocket
    {
    
        UdpClient UdpClient = null;
        Thread ListenThread = null;
        public delegate void UdpSocketCallBackMethod( string rtnMessage, string ip);
        UdpSocketCallBackMethod _callbackMethod = null;
        public UdpServerSocket(int serverPort, UdpSocketCallBackMethod callBackMethod)
        {
           
            UdpClient = new UdpClient(serverPort);
            ListenThread = new Thread(ListenMessage);
            _callbackMethod = callBackMethod;
            ListenThread.IsBackground = true;
            ListenThread.Start();
        }
        
        public void ListenMessage()
        {
            while (true)
            {
                IPEndPoint IpFrom = new IPEndPoint(IPAddress.Any, 0);
                var rcvMessage = System.Text.Encoding.UTF8.GetString(UdpClient.Receive(ref IpFrom));
                IPEndPoint p = new IPEndPoint(1,2);
                _callbackMethod.Invoke(rcvMessage, IpFrom.Address.ToString());
            }
        }

    }
    public class UdpClientSocket
    {
        IPEndPoint TargetEndpoint = null;
        Socket UdpClient = null;
        
        public UdpClientSocket(string clientIP, int clientPort)
        {
            TargetEndpoint = new IPEndPoint(IPAddress.Parse(clientIP), clientPort);
            UdpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            
        }
        public int SenTo(string message)
        {
            var feedBack = UdpClient.SendTo(Encoding.UTF8.GetBytes(message), TargetEndpoint);
            return feedBack;
        }

    }
    #endregion
}
