using MvAssistant.DeviceDrive.KjMachineDrawer.Exceptions;
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
        /// <summary>Cabinet 編號</summary>        
        public int CabinetNO { get; private set; }
        /// <summary>Drawer 編號</summary>
        public int DrawerNO { get; private set; }
        /// <summary>裝置IP</summary>
        public string DeviceIP { get; private set; }
        public DrawerSocket DrawerSocket { get; private set; }

        private Drawer() { }

        public Drawer(int cabinetNO, int drawerNO, string deviceIP, int udpServerPort) : this()
        {
            DrawerNO = drawerNO;
            CabinetNO = cabinetNO;
            DeviceIP = deviceIP;
            DrawerSocket = new DrawerSocket(deviceIP, udpServerPort);
        }
       

        public void Command_INI()
        {
            var commandText = new INI().GetCommandText(new INIParameter());
            DrawerSocket.SentTo(commandText);
        }
        
        public void Command_SetMotionSpeed(int speed)
        {
            if (speed > 100 || speed < 1)
            { throw new MotionSpeedOutOfRangeException(); }
            var parameter = new SetMotionSpeedParameter { Speed = speed };
            var commandText = new SetMotionSpeed().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }

        public void Command_SetTimeOut(int timeoutSeconds)
        {
            if(timeoutSeconds < 1 || timeoutSeconds > 100)
            {
                throw new TimeOutSecondOutOfRangeException();
            }
            var parameter = new SetTimeOutParameter {  Seconds=timeoutSeconds };
            var commandText = new SetTimeOut().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }
        private void Command_TrayMotion(TrayMotionType trayMotionType)
        {
            var parameter = new TrayMotionParameter { TrayMotionType = trayMotionType };
            var commandText = new TrayMotion().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }

        public void Command_TrayMotionHome()
        {
            Command_TrayMotion(TrayMotionType.Home);
        }
        public void Command_TrayMotionOut()
        {
            Command_TrayMotion(TrayMotionType.Out);
        }
        public void Command_TrayMotionIn()
        {
            Command_TrayMotion(TrayMotionType.In);
        }

        private void Command_BrightLED(BrightLEDType brightLEDType)
        {
            var parameter = new BrightLEDParameter { BrightLEDType = brightLEDType };
            var commandText = new BrightLED().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }
        public void Command_BrightLEDAllOn()
        {
            Command_BrightLED(BrightLEDType.AllOn);
        }
        public void Command_BrightLedAllOff()
        {
            Command_BrightLED(BrightLEDType.AllOff);
        }
        public void Command_BrightLEDGreenOn()
        {
            Command_BrightLED(BrightLEDType.GreenOn);
        }
        public void Command_BrightLEDRedOn()
        {
            Command_BrightLED(BrightLEDType.RedOn);
        }
        public void Command_PositionRead()
        {
            var parameter =  new PositionReadParameter();
            var commandText = new PositionRead().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }
        public void Command_BoxDetection()
        {
            var parameter = new BoxDetectionParameter();
            var commandText = new BoxDetection().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }
        public void Command_WriteNetSetting()
        {
            var parameter = new WriteNetSettingParameter();
            var commandText = new WriteNetSetting().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }

        public void Command_LCDMsg(string message)
        {
            var parameter = new LSDMsgParameter { Message = message };
            var commandText = new LCDMsg().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }
        private void Command_SetParameter(SetParameterType setParameterType,string extendText)
        {
            var parameter = new SetParameterParameter {  ExtendText=extendText, SetParameterType=setParameterType };
            var commandText = new SetParameter().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }
        public void Command_SetParameterHomePosition(string extendText)
        {
            Command_SetParameter(SetParameterType.Home_position, extendText);
        }
        public void Command_SetParameterOutSidePosition(string extendText)
        {
            Command_SetParameter(SetParameterType.Out_side_position, extendText);
        }
        public void Command_SetParameterInSidePosition(string extendText)
        {
            Command_SetParameter(SetParameterType.In_side_position, extendText);
        }
        public void Command_SetParameterIPAddress(string extendText)
        {
            Command_SetParameter(SetParameterType.IP_address, extendText);
        }
        public void Command_SetParameterSubMask(string extendText)
        {
            Command_SetParameter(SetParameterType.SubMask, extendText);
        }
        public void Command_SetParameterGetwayAddress(string extendText)
        {
            Command_SetParameter(SetParameterType.Gateway_address, extendText);
        }
    }
}
