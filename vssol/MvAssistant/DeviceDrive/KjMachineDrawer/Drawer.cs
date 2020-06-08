using MvAssistant.DeviceDrive.KjMachineDrawer.Exceptions;
using MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand;
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
       
       
        public void CommandINI()
        {
            var commandText = new INI().GetCommandText(new INIParameter());
            DrawerSocket.SentTo(commandText);
        }
        
        public void CommandSetMotionSpeed(int speed)
        {
            if (speed > 100 || speed < 1)
            { throw new MotionSpeedOutOfRangeException(); }
            var parameter = new SetMotionSpeedParameter { Speed = speed };
            var commandText = new SetMotionSpeed().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }

        public void CommandSetTimeOut(int timeoutSeconds)
        {
            if(timeoutSeconds < 1 || timeoutSeconds > 100)
            {
                throw new TimeOutSecondOutOfRangeException();
            }
            var parameter = new SetTimeOutParameter {  Seconds=timeoutSeconds };
            var commandText = new SetTimeOut().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }
        private void CommandTrayMotion(TrayMotionType trayMotionType)
        {
            var parameter = new TrayMotionParameter { TrayMotionType = trayMotionType };
            var commandText = new TrayMotion().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }

        public void CommandTrayMotionHome()
        {
            CommandTrayMotion(TrayMotionType.Home);
        }
        public void CommandTrayMotionOut()
        {
            CommandTrayMotion(TrayMotionType.Out);
        }
        public void CommandTrayMotionIn()
        {
            CommandTrayMotion(TrayMotionType.In);
        }

        private void CommandBrightLED(BrightLEDType brightLEDType)
        {
            var parameter = new BrightLEDParameter { BrightLEDType = brightLEDType };
            var commandText = new BrightLED().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }
        public void CommandBrightLEDAllOn()
        {
            CommandBrightLED(BrightLEDType.AllOn);
        }
        public void CommandBrightLedAllOff()
        {
            CommandBrightLED(BrightLEDType.AllOff);
        }
        public void CommandBrightLEDGreenOn()
        {
            CommandBrightLED(BrightLEDType.GreenOn);
        }
        public void CommandBrightLEDRedOn()
        {
            CommandBrightLED(BrightLEDType.RedOn);
        }
        public void CommandPositionRead()
        {
            var parameter =  new PositionReadParameter();
            var commandText = new PositionRead().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }
        public void CommandBoxDetection()
        {
            var parameter = new BoxDetectionParameter();
            var commandText = new BoxDetection().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }
        public void CommandWriteNetSetting()
        {
            var parameter = new WriteNetSettingParameter();
            var commandText = new WriteNetSetting().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }

        public void CommandLCDMsg(string message)
        {
            var parameter = new LSDMsgParameter { Message = message };
            var commandText = new LCDMsg().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }
        private void CommandSetParameter(SetParameterType setParameterType,string extendText)
        {
            var parameter = new SetParameterParameter {  ExtendText=extendText, SetParameterType=setParameterType };
            var commandText = new SetParameter().GetCommandText(parameter);
            DrawerSocket.SentTo(commandText);
        }
        public void CommandSetParameterHomePosition(string extendText)
        {
            CommandSetParameter(SetParameterType.Home_position, extendText);
        }
        public void CommandSetParameterOutSidePosition(string extendText)
        {
            CommandSetParameter(SetParameterType.Out_side_position, extendText);
        }
        public void CommandSetParameterInSidePosition(string extendText)
        {
            CommandSetParameter(SetParameterType.In_side_position, extendText);
        }
        public void CommandSetParameterIPAddress(string extendText)
        {
            CommandSetParameter(SetParameterType.IP_address, extendText);
        }
        public void CommandSetParameterSubMask(string extendText)
        {
            CommandSetParameter(SetParameterType.SubMask, extendText);
        }
        public void CommandSetParameterGetwayAddress(string extendText)
        {
            CommandSetParameter(SetParameterType.Gateway_address, extendText);
        }

        public void ReplyTrayMotion(ReplyMessage reply)
        {
            ReplyResultCode replyResultCode = (ReplyResultCode)((int)(reply.Value));
        }
        public void ReplySetSpeed(ReplyMessage reply)
        {
            ReplyResultCode replyResultCode = (ReplyResultCode)((int)(reply.Value));
        }

        public void ReplySetTimeOut(ReplyMessage reply)
        {
            ReplyResultCode replyResultCode = (ReplyResultCode)((int)(reply.Value));
        }
        public void  ReplyPosition(ReplyMessage reply)
        {
            var IHO = "";
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

        }
        public void ReplyBoxDetection(ReplyMessage reply)
        {
            var hasBox = false;
            if(reply.Value.HasValue && (int)reply.Value == 1)
            {
                hasBox = true;
            }
        }
        public void TrayArrive(ReplyMessage reply)
        {
            TrayArriveType trayArriveType = (TrayArriveType)((int)reply.Value);
        }
        public void ButtonEvent(ReplyMessage reply)
        {

        }
        public void TimeOutEvent(ReplyMessage reply)
        {

        }
        public void TrayMotioning(ReplyMessage reply)
        {

        }
        public void INIFailed(ReplyMessage reply)
        {

        }
        public void TrayMotionError(ReplyMessage reply)
        {

        }
        public void Error(ReplyMessage reply)
        {
            ReplyErrorCode replyErrorCode = (ReplyErrorCode)((int)reply.Value);
        }
        public void SysStartUp(ReplyMessage reply)
        {

        }
    }
}
