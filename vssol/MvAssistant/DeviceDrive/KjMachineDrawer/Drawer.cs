using MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer
{
    public class Drawer
    {

        public int CabinetNO { get; private set; }
        public int DrawerNO { get; private set; }
        public string UDPServerIP { get; private set; }
        public DrawerSocket DrawerSocket { get; private set; }

        private Drawer() { }
        public Drawer(int cabinetNO, int drawerNO, string udpServerIP, int udpServerPort) : this()
        {
            DrawerNO = drawerNO;
            CabinetNO = cabinetNO;
            UDPServerIP = udpServerIP;
            DrawerSocket = new DrawerSocket(udpServerIP, udpServerPort);
        }
        public int SentTo(string message)
        {
            var feedBack = DrawerSocket.SentTo(message);
            return feedBack;
        }

        public void INI()
        {
            var commandText = new INI().GetCommandText(new INIParameter());
            DrawerSocket.SentTo(commandText);
        }
        
        public void SetMotionSpeed(int speed)
        {
            var parameter = new SetMotionSpeedParameter { Speed = speed };
            var commandText = new SetMotionSpeed().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }

        public void SetTimeOut(int timeoutSeconds)
        {
            var parameter = new SetTimeOutParameter {  Seconds=timeoutSeconds };
            var commandText = new SetTimeOut().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }
        private void TrayMotion(TrayMotionType trayMotionType)
        {
            var parameter = new TrayMotionParameter { TrayMotionType = trayMotionType };
            var commandText = new TrayMotion().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }

        public void TrayMotionHome()
        {
            TrayMotion(TrayMotionType.Home);
        }
        public void TrayMotionOut()
        {
            TrayMotion(TrayMotionType.Out);
        }
        public void TrayMotionIn()
        {
            TrayMotion(TrayMotionType.In);
        }

        private void BrightLED(BrightLEDType brightLEDType)
        {
            var parameter = new BrightLEDParameter { BrightLEDType = brightLEDType };
            var commandText = new BrightLED().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }
        public void BrightLEDAllOn()
        {
            BrightLED(BrightLEDType.AllOn);
        }
        public void BrightLedAllOff()
        {
            BrightLED(BrightLEDType.AllOff);
        }
        public void BrightLEDGreenOn()
        {
            BrightLED(BrightLEDType.GreenOn);
        }
        public void BrightLEDRedOn()
        {
            BrightLED(BrightLEDType.RedOn);
        }
        public void PositionRead()
        {
            var parameter =  new PositionReadParameter();
            var commandText = new PositionRead().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }
        public void BoxDetection()
        {
            var parameter = new BoxDetectionParameter();
            var commandText = new BoxDetection().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }
        public void WriteNetSetting()
        {
            var parameter = new WriteNetSettingParameter();
            var commandText = new WriteNetSetting().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }

        public void LCDMsg(string message)
        {
            var parameter = new LSDMsgParameter { Message = message };
            var commandText = new LCDMsg().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }
        private void SetParameter(SetParameterType setParameterType,string extendText)
        {
            var parameter = new SetParameterParameter {  ExtendText=extendText, SetParameterType=setParameterType };
            var commandText = new SetParameter().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }
        public void SetParameterHomePosition(string extendText)
        {
            SetParameter(SetParameterType.Home_position, extendText);
        }
        public void SetParameterOutSidePosition(string extendText)
        {
            SetParameter(SetParameterType.Out_side_position, extendText);
        }
        public void SetParameterInSidePosition(string extendText)
        {
            SetParameter(SetParameterType.In_side_position, extendText);
        }
        public void SetParameterIPAddress(string extendText)
        {
            SetParameter(SetParameterType.IP_address, extendText);
        }
        public void SetParameterSubMask(string extendText)
        {
            SetParameter(SetParameterType.SubMask, extendText);
        }
        public void SetParameterGetwayAddress(string extendText)
        {
            SetParameter(SetParameterType.Gateway_address, extendText);
        }
    }
}
