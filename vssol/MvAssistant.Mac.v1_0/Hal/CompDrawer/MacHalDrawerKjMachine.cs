using MvAssistant.DeviceDrive.KjMachineDrawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MvAssistant.DeviceDrive.KjMachineDrawer.ReplyCode;
using MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment;
using System.Diagnostics;
using MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand;

namespace MvAssistant.Mac.v1_0.Hal.CompDrawer
{
    [GuidAttribute("D0F66AC7-5CD9-42FB-8B05-AAA31C647979")]
      public class MacHalDrawerKjMachine : MacHalComponentBase, IMacHalDrawer
    {

        #region Const
        public const string DevConnStr_Ip = "ip";
        public const string DevConnStr_Port = "port";

        public const string DevConnStr_LocalIp = "local_ip";
        public const string DevConnStr_LocalPort = "local_port";
        #endregion


       
        public  MvKjMachineDrawerLdd Ldd { get; set; }
        public string DeviceIP { get; set; }


        public override int HalConnect()
        {
            throw new NotImplementedException();
        }

        public override int HalClose()
        {
            //throw new NotImplementedException();
            return 0;
        }


        public void BindResult()
        {
            Ldd.BoxDetectionResult += this.BoxDetectionResult;
            // Ldd.BrightLEDAllOffResult += this.BrightLEDAllOffResult;
            //Ldd.BrightLEDAllOnResult += this.BrightLEDAllOnResult;
            //Ldd.BrightLEDGreenOnResult += this.BrightLEDGreenOnResult;
            //Ldd.BrightLEDRedOnResult += this.BrightLEDRedOnResult;
            Ldd.BrightLEDResult = this.BrightLEDResult;
            Ldd.INIResult += this.INIResult;
            Ldd.PositionReadResult += this.PositionReadResult;
            Ldd.SetMotionSpeedResult += this.SetMotionSpeedResult;
            Ldd.SetTimeOutResult += this.SetTimeOutResult;
            Ldd.TrayMotionHomeResult += this.TrayMotionHomeResult;
            Ldd.TrayMotionInResult += this.TrayMotionInResult;
            Ldd.TrayMotionOutResult += this.TrayMotionOutResult;
            //Ldd.
        }


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

        public string CommandINI()
        {
            var commandText = Ldd.CommandINI();
            return commandText;
        }

        public string CommandSetMotionSpeed(int speed)
        {
            var commandText = Ldd.CommandINI();
            return commandText;
        }

        public string CommandSetTimeOut(int timeoutSeconds)
        {
            var commandText = Ldd.CommandINI();
            return commandText;
        }

        public string CommandTrayMotionHome()
        {
            var commandText = Ldd.CommandINI();
            return commandText;
        }

        public string CommandTrayMotionOut()
        {
            var commandText = Ldd.CommandINI();
            return commandText;
        }

        public string CommandTrayMotionIn()
        {
            var commandText = Ldd.CommandINI();
            return commandText;
        }

        public string CommandBrightLED(BrightLEDType brightLEDType)
        {
            var commandText = Ldd.CommandINI();
            return commandText;
        }

        public string CommandBrightLEDAllOn()
        {
            var commandText = Ldd.CommandINI();
            return commandText;
        }

        public string CommandBrightLEDAllOff()
        {
            var commandText = Ldd.CommandINI();
            return commandText;
        }

        public string CommandBrightLEDGreenOn()
        {
            var commandText = Ldd.CommandINI();
            return commandText;
        }

        public string CommandBrightLEDRedOn()
        {
            var commandText = Ldd.CommandINI();
            return commandText;
        }

        public string CommandPositionRead()
        {
            var commandText = Ldd.CommandINI();
            return commandText;
        }

        public string CommandBoxDetection()
        {
            var commandText = Ldd.CommandINI();
            return commandText;
        }

        public string CommandWriteNetSetting()
        {
            var commandText = Ldd.CommandINI();
            return commandText;
        }

        public string CommandLCDMsg(string message)
        {
            var commandText = Ldd.CommandINI();
            return commandText;
        }

        public string CommandSetParameterHomePosition(string homePosition)
        {
            var commandText = Ldd.CommandINI();
            return commandText;
        }

        public string CommandSetParameterOutSidePosition(string outsidePosition)
        {
            var commandText = Ldd.CommandINI();
            return commandText;
        }

        public string CommandSetParameterInSidePosition(string insidePosition)
        {
            var commandText = Ldd.CommandINI();
            return commandText;
        }

        public string CommandSetParameterIPAddress(string ipAddress)
        {
            var commandText = Ldd.CommandINI();
            return commandText;
        }

        public string CommandSetParameterSubMask(string submaskAddress)
        {
             var commandText = Ldd.CommandINI();
            return commandText;
        }

        public void INIResult(object sender,bool result)
        {
            if (result)
            {

            }
            else
            {

            }
        }

        public void SetMotionSpeedResult(object sender, bool result)
        {
            if (result)
            {

            }
            else
            {

            }
        }

        public void SetTimeOutResult(object sender, bool result)
        {
            if (result)
            {

            }
            else
            {

            }
        }

        public void TrayMotionHomeResult(object sender, bool result)
        {
            if (result)
            {

            }
            else
            {

            }
        }

        public void TrayMotionOutResult(object sender, bool result)
        {
            if (result)
            {

            }
            else
            {

            }
        }

        public void TrayMotionInResult(object sender, bool result)
        {
            if (result)
            {

            }
            else
            {

            }
        }

        public void BrightLEDResult(object sender, bool result)
        {
            if (result)
            {

            }
            else
            {

            }
        }
       

        public void PositionReadResult(object sender, string result)
        {
            if (string.IsNullOrEmpty(result))
            {

            }
            else
            {

            }
        }

        public void BoxDetectionResult(object sender, bool result)
        {
            if (result)
            {

            }
            else
            {

            }
        }

        public void CommandLCDMsg(object sender, bool result)
        {
            if (result)
            {

            }
            else
            {

            }
        }
    }
}
