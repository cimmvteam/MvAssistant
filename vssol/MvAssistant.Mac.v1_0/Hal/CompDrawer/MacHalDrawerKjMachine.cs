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
using MvAssistant.DeviceDrive;

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

        public event EventHandler OnBoxDetectionResultHandler;
        #endregion



        public  IDrawerLdd Ldd { get; set; }
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
        public object Tag { get; set; }
        public string Index { get; set; }
        public void BindResult()
        {
            Ldd.BoxDetectionResult += this.BoxDetectionResult;
           
            Ldd.BrightLEDResult = this.BrightLEDResult;
            Ldd.INIResult += this.INIResult;
            Ldd.PositionReadResult += this.PositionReadResult;
            Ldd.SetMotionSpeedResult += this.SetMotionSpeedResult;
            Ldd.SetTimeOutResult += this.SetTimeOutResult;
            // Ldd.TrayMotionHomeResult += this.TrayMotionHomeResult;
            // Ldd.TrayMotionInResult += this.TrayMotionInResult;
            //Ldd.TrayMotionOutResult += this.TrayMotionOutResult;
            Ldd.TrayArriveResult += this.TrayArriveResult;

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
            this.Tag = nameof(CommandINI);
            var commandText = Ldd.CommandINI();
            return commandText;
        }

        public string CommandSetMotionSpeed(int speed)
        {
            this.Tag = nameof(CommandSetMotionSpeed);
            var commandText = Ldd.CommandSetMotionSpeed(speed);
            return commandText;
        }

        public string CommandSetTimeOut(int timeoutSeconds)
        {
            this.Tag = nameof(CommandSetTimeOut);
            var commandText = Ldd.CommandSetTimeOut(timeoutSeconds);
            return commandText;
        }

        public string CommandTrayMotionHome()
        {
            this.Tag = nameof(CommandTrayMotionHome);
            var commandText = Ldd.CommandTrayMotionHome();
            return commandText;
        }

        public string CommandTrayMotionOut()
        {
            this.Tag = nameof(CommandTrayMotionOut);
            var commandText = Ldd.CommandTrayMotionOut();
            return commandText;
        }

        public string CommandTrayMotionIn()
        {
            this.Tag = nameof(CommandTrayMotionIn);
            var commandText = Ldd.CommandTrayMotionIn();
            return commandText;
        }

        

        public string CommandBrightLEDAllOn()
        {
            this.Tag = nameof(CommandBrightLEDAllOn);
            var commandText = Ldd.CommandBrightLEDAllOn();
            return commandText;
        }

        public string CommandBrightLEDAllOff()
        {
            this.Tag = nameof(CommandBrightLEDAllOff);
            var commandText = Ldd.CommandBrightLEDAllOff();
            return commandText;
        }

        public string CommandBrightLEDGreenOn()
        {
            this.Tag = nameof(CommandBrightLEDGreenOn);
            var commandText = Ldd.CommandBrightLEDGreenOn();
            return commandText;
        }

        public string CommandBrightLEDRedOn()
        {
            this.Tag = nameof(CommandBrightLEDRedOn);
            var commandText = Ldd.CommandBrightLEDRedOn();
            return commandText;
        }

        public string CommandPositionRead()
        {
            this.Tag = nameof(CommandPositionRead);
            var commandText = Ldd.CommandPositionRead();
            return commandText;
        }

        public string CommandBoxDetection()
        {
            this.Tag = nameof(CommandBoxDetection);
            var commandText = Ldd.CommandBoxDetection();
            return commandText;
        }

        public string CommandWriteNetSetting()
        {
            this.Tag = nameof(CommandWriteNetSetting);
            var commandText = Ldd.CommandWriteNetSetting();
            return commandText;
        }

        public string CommandLCDMsg(string message)
        {
            this.Tag = nameof(CommandLCDMsg);
          var commandText = Ldd.CommandLCDMsg(message);
            return commandText;
        }

        public string CommandSetParameterHomePosition(string homePosition)
        {
            this.Tag = nameof(CommandSetParameterHomePosition);
            var commandText = Ldd.CommandSetParameterHomePosition(homePosition);
            return commandText;
        }

        public string CommandSetParameterOutSidePosition(string outsidePosition)
        {
            this.Tag = nameof(CommandSetParameterOutSidePosition);
            var commandText = Ldd.CommandSetParameterOutSidePosition(outsidePosition);
            return commandText;
        }

        public string CommandSetParameterInSidePosition(string insidePosition)
        {
            this.Tag = nameof(CommandSetParameterInSidePosition);
            var commandText = Ldd.CommandSetParameterInSidePosition(insidePosition);
            return commandText;
        }

        public string CommandSetParameterIPAddress(string ipAddress)
        {
            this.Tag = nameof(CommandSetParameterIPAddress);
            var commandText = Ldd.CommandSetParameterIPAddress(ipAddress);
            return commandText;
        }

        public string CommandSetParameterSubMask(string submaskAddress)
        {
            this.Tag = nameof(CommandSetParameterSubMask);
            var commandText = Ldd.CommandSetParameterSubMask(submaskAddress);
            return commandText;
        }
        public void TrayArriveResult(object sender, int result)
        {
            var arriveType = (TrayArriveType)result;
            if (this.Tag.ToString() == nameof(CommandINI))
            {   // 如果當時是發 initial 指令, 視為 初始化成功
                if (arriveType == TrayArriveType.ArriveHome)
                {
                    this.INIResult(sender,true);
                }
            }
            else
            {
                var ldd = (MvKjMachineDrawerLdd)sender;
                if (arriveType == TrayArriveType.ArriveHome)
                { // 回到 Home

                    DebugLog(ldd, "已經回到 Home");
                }
                else if (arriveType == TrayArriveType.ArriveIn)
                { //  回到 In
                    DebugLog(ldd, "已經回到 In");
                }
                else// if (arriveType == TrayArriveType.ArriveOut)
                {  // 回到 Out
                    DebugLog(ldd, "已經回到 Out");
                }
            }
        }


        
        public void INIResult(object sender,bool result)
        {
            var ldd = (MvKjMachineDrawerLdd)sender;
            if (result)
            {  // 初始化成功
                DebugLog(ldd, "初始化成功");
            }
            else
            { // 初始化失敗
                DebugLog(ldd, "初始化失敗");
            }
        }

        public void SetMotionSpeedResult(object sender, bool result)
        {
            var ldd = (MvKjMachineDrawerLdd)sender;
            if (result)
            {  // 速度設定成功
                DebugLog(ldd, "設定速度成功");
            }
            else
            {  // 速度設定失敗
                DebugLog(ldd, "設定速度失敗");
            }
        }

        public void SetTimeOutResult(object sender, bool result)
        {
            var ldd = (MvKjMachineDrawerLdd)sender;
            if (result)
            { // 逾時時間設定成功
                DebugLog(ldd, "設定Time Out成功");
            }
            else
            {
                DebugLog(ldd, "設定Time Out 失敗");
            }
        }

      
      
        public void BrightLEDResult(object sender, bool result)
        {
            MvKjMachineDrawerLdd ldd = (MvKjMachineDrawerLdd)sender;
            var command = this.Tag.ToString();
            if (result)
            {    // 成功
               
                if(command == nameof(CommandBrightLEDAllOff))
                { // 關掉所有的 led 
                    DebugLog(ldd, "所有 LED off OK");
                }
                else if (command ==nameof(CommandBrightLEDAllOn))
                {// 打亮所有的led
                    DebugLog(ldd, "所有 LED On OK");
                }
                else if (command == nameof(CommandBrightLEDGreenOn))
                {  // 打亮綠色LED
                    DebugLog(ldd, "綠色 LED On OK");
                }
                else 
                {   // 打亮 紅色LED
                    DebugLog(ldd, "紅色 LED On OK");
                }
            }
            else // 失敗
            {
                if (command == nameof(CommandBrightLEDAllOff))
                { // 關掉所有的 led 
                    DebugLog(ldd, "所有 LED off Fail");
                }
                else if (command == nameof(CommandBrightLEDAllOn))
                {// 打亮所有的led
                    DebugLog(ldd, "所有 LED On Fail");
                }
                else if (command == nameof(CommandBrightLEDGreenOn))
                {  // 打亮綠色LED
                    DebugLog(ldd, "綠色 LED On Fail");
                }
                else
                {   // 打亮 紅色LED
                    DebugLog(ldd, "紅色 LED On Fail");
                }
            }
        }
       

        public void PositionReadResult(object sender, string result)
        {
            MvKjMachineDrawerLdd ldd = (MvKjMachineDrawerLdd)sender;
            if (string.IsNullOrEmpty(result))
            {
                DebugLog(ldd, " PositionRead Error");
            }
            else
            {// result 為IOH
                DebugLog(ldd, $"IOH={result}");
            }
        }

        public void BoxDetectionResult(object sender, bool result)
        {
            MvKjMachineDrawerLdd ldd = (MvKjMachineDrawerLdd)sender;
            if (result)
            {  // 有盒子
                DebugLog(ldd, "有盒子");
                
            }
            else
            {
                DebugLog(ldd, "没有盒子");
            }
            if (OnBoxDetectionResultHandler != null)
            {
                OnBoxDetectionResultHandler.Invoke(this, new HalDrawerBoxDetectReturnCode { HasBox = result });
            }
        }
        public class HalDrawerBoxDetectReturnCode:EventArgs
        {
            public bool? HasBox { get; set; }
        }


        public void ErrorResult(object sender,int result)
        {
            MvKjMachineDrawerLdd ldd = (MvKjMachineDrawerLdd)sender;
            var errorResult = (ReplyErrorCode)result;
            if (errorResult == ReplyErrorCode.Error)
            { // Error

                DebugLog(ldd, "Error");
            }
            else //if (errorResult == ReplyErrorCode.Recovery)
            {// Recovery
                DebugLog(ldd, "Recovery");
            }
        }
        
        void DebugLog(MvKjMachineDrawerLdd ldd, string text)
        {
            string str = $"Ldd={ldd.DeviceIP}, Text={text}";
            Debug.WriteLine("\r\n"+ str);
        }
    }
}
