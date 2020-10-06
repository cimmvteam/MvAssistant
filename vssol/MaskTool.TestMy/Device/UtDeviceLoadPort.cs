using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.GudengLoadPort;
using System.Threading;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Manifest;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.DeviceDrive.GudengLoadPort.LoadPortEventArgs;
using MvAssistant.DeviceDrive.GudengLoadPort.ReplyCode;
using System.Diagnostics;

namespace MvAssistant.Mac.TestMy.Device
{
    [TestClass]
    public class UtDeviceLoadPort
    {

        MvGudengLoadPortLdd LoadPort1 = null;
        MvGudengLoadPortLdd LoadPort2 = null;
        MvGudengLoadPortCollection ldd = new MvGudengLoadPortCollection();
        public UtDeviceLoadPort()
        {

            LoadPort1 = ldd.CreateLoadPort("192.168.0.20", 1024, 1);
            // LoadPort1 = ldd.CreateLoadPort("127.0.0.1", 1024, 1);
            LoadPort2 = ldd.CreateLoadPort("192.168.0.21", 1024, 2);
            BindEventHandler();

            LoadPort1.StartListenServerThread();
            LoadPort2.StartListenServerThread();
        }
        public void BindEventHandler()
        {
            foreach (var loadport in ldd.LoadPorts)
            {
                loadport.OnAlarmResetFailHandler += this.OnAlarmResetFail;//014
                loadport.OnAlarmResetSuccessHandler += this.OnAlarmResetSuccess;//013
                loadport.OnBarcode_IDHandler += this.OnBarcode_ID;//005
                loadport.OnClamperActionTimeOutHandler += this.OnClamperActionTimeOut;// 200
                loadport.OnClamperHandler += this.OnClamper;//003
                loadport.OnClamperUnlockCompleteHandler += this.OnClamperUnlockComplete;//012
                loadport.OnClamperLockPositionFailedHandler += this.OnClamperLockPositionFailed;//207
                loadport.OnClamperMotorAbnormalityHandler += this.OnClamperMotorAbnormality;//209
                loadport.OnClamperNotLockHandler += this.OnClamperNotLock;//022
                loadport.OnClamperLockCompleteHandler += this.OnClamperLockComplete;//006
                loadport.OnClamperUnlockPositionFailedHandler += this.OnClamperUnlockPositionFailed;//201
                loadport.OnPODPresentAbnormalityHandler += this.OnPODPresentAbnormality;//208
                loadport.OnDockPODComplete_EmptyHandler += this.OnDockPODComplete_Empty;//010
                loadport.OnDockPODComplete_HasReticleHandler += this.OnDockPODComplete_HasReticle;// 009
                loadport.OnDockPODStartHandler += this.OnDockPODStart;//008
                loadport.OnExecuteAlarmResetFirstHandler += this.OnExecuteAlarmResetFirst;// 016
                loadport.OnExecuteInitialFirstHandler += this.OnExecuteInitialFirst;//015
                loadport.OnInitialCompleteHandler += this.OnInitialComplete;//019
                loadport.OnInitialUnCompleteHandler += this.OnInitialUnComplete; // 自訂
                loadport.OnLoadportStatusHandler += this.OnLoadportStatus;// 018
                loadport.OnMustInAutoModeHandler += this.OnMustInAutoMode;//020
                loadport.OnPlacementHandler += this.OnPlacement;//001
                loadport.OnPODNotPutProperlyHandler += this.OnPODNotPutProperly;//023
                loadport.OnPresentHandler += this.OnPresent;//002
                loadport.OnReticlePositionAbnormalityHandler += this.OnReticlePositionAbnormality;//206
                loadport.OnRFIDHandler += this.OnRFID;//004
                loadport.OnStageMotionTimeoutHandler += this.OnStageMotionTimeout;//203
                loadport.OnStageMotorAbnormalityHandler += this.OnStageMotorAbnormality;//210
                loadport.OnStageOverDownLimitationHandler += this.OnStageOverDownLimitation;//205
                loadport.OnStageOverUpLimitationHandler += this.OnStageOverUpLimitation;//204
                loadport.OnStagePositionHandler += this.OnStagePosition;//017
                loadport.OnUndockCompleteHandler += this.OnUndockComplete;//011
                loadport.OnVacuumAbnormalityHandler += this.OnVacuumAbnormality;//202
                loadport.OnVacuumCompleteHandler += this.OnVacuumComplete;//007
                loadport.OnMustInManualModeHandler += this.OnMustInManualMode;// 021

            }
        }
        void Repeat()
        {
            while (true)
            {
                Thread.Sleep(100);
            }
        }

        #region TestMethod
        [TestMethod]
        public void TestCommandInitialRequest()
        {  //~112,InitialRequest@
            var commandText1 = LoadPort1.CommandInitialRequest();
            NoteCommand(commandText1);//~002,Present,1@
            var commandText2 = LoadPort2.CommandInitialRequest();
            NoteCommand(commandText2);
            Repeat();
        }

        /// <summary>(100)</summary>
        [TestMethod]
        public void TestCommandDockRequest()
        {    //~100,DockRequest@
            var commandText1 = LoadPort1.CommandDockRequest();
            NoteCommand(commandText1);
           Repeat();
        }

        /// <summary>(101)</summary>
        [TestMethod] 
        public void TestCommandUndockRequest()
        { //~101,UndockRequest@
            var commandText2 = LoadPort2.CommandUndockRequest();
            NoteCommand(commandText2);
            Repeat();
        }



        [TestMethod]
        public void TestCommandAskPlacementStatus()
        {  //~102,AskPlacementStatus@
            var commandText1 = LoadPort1.CommandAskPlacementStatus();
            NoteCommand(commandText1);

            var commandText2 = LoadPort2.CommandAskPlacementStatus();
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod]
        public void TestCommandAskPresentStatus()
        { //~103,AskPresentStatus@
            var commandText1 = LoadPort1.CommandAskPresentStatus();
            NoteCommand(commandText1);

            var commandText2 = LoadPort2.CommandAskPresentStatus();
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod] 
        public void TestCommandAskClamperStatus()
        {  //~104,AskClamperStatus@
            var commandText1 = LoadPort1.CommandAskClamperStatus();
            NoteCommand(commandText1);

            var commandText2 = LoadPort2.CommandAskClamperStatus();
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod]
        public void TestCommandAskRFIDStatus()
        { // ~105,AskRFIDStatus@
            var commandText1 = LoadPort1.CommandAskRFIDStatus();
            NoteCommand(commandText1);

            var commandText2 = LoadPort2.CommandAskRFIDStatus();
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod]
        public void TestCommandAskBarcodeStatus()
        {  //~106,AskBarcodeStatus@
            var commandText1 = LoadPort1.CommandAskBarcodeStatus();
            NoteCommand(commandText1);

            var commandText2 = LoadPort2.CommandAskBarcodeStatus();
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod]
        public void TestCommandAskVacuumStatus()
        {       //~107,AskVacuumStatus@
            var commandText1 = LoadPort1.CommandAskVacuumStatus();
            NoteCommand(commandText1);

            var commandText2 = LoadPort2.CommandAskVacuumStatus();
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod]
        public void TestCommandAskReticleExistStatus()
        {//~108,AskReticleExistStatus@
            var commandText1 = LoadPort1.CommandAskReticleExistStatus();
            NoteCommand(commandText1);

            var commandText2 = LoadPort2.CommandAskReticleExistStatus();
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod] 
        public void TestCommandAlarmReset()
        {  //~109,AlarmReset@
           
            var commandText1 = LoadPort1.CommandAlarmReset();
            NoteCommand(commandText1);

            var commandText2 = LoadPort2.CommandAlarmReset();
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod]
        public void TestCommandAskStagePosition()
        {//~110,AskStagePosition@

            var commandText1 = LoadPort1.CommandAskStagePosition();
            NoteCommand(commandText1);
            var commandText2 = LoadPort2.CommandAskStagePosition();
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod]
        public void TestCommandAskLoadportStatus()
        { // ~111,AskLoadportStatus@
          
            var commandText1 = LoadPort1.CommandAskLoadportStatus();
            NoteCommand(commandText1);
            var commandText2 = LoadPort2.CommandAskLoadportStatus();
            NoteCommand(commandText2);
            Repeat();

        }

        [TestMethod] 
        public void TestCommandManualClamperLock()
        { //~113,ManualClamperLock@
           
            var commandText1 = LoadPort1.CommandManualClamperLock();
            NoteCommand(commandText1);
            var commandText2 = LoadPort2.CommandManualClamperLock();
            NoteCommand(commandText2);
            Repeat();

        }

        [TestMethod] 
        public void TestCommandManualClamperUnlock()
        {  //~114,ManualClamperUnlock@
            var commandText1 = LoadPort1.CommandManualClamperUnlock();
            NoteCommand(commandText1);
            var commandText2 = LoadPort2.CommandManualClamperUnlock();
            NoteCommand(commandText2);

            Repeat();
        }

        [TestMethod] 
        public void TestCommandManualClamperOPR()
        {  //~115,ManualClamperOPR@
            var commandText1 = LoadPort1.CommandManualClamperOPR();
            NoteCommand(commandText1);

            var commandText2 = LoadPort2.CommandManualClamperOPR();
            NoteCommand(commandText2);

            Repeat();
        }

        [TestMethod] 
        public void TestCommandManualStageUp()
        {   //~116,ManualStageUp@
            var commandText1 = LoadPort1.CommandManualStageUp();
            NoteCommand(commandText1);
            var commandText2 = LoadPort2.CommandManualStageUp();
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod]
        public void TestCommandManualStageInspection()
        {  //~116,ManualStageUp@
          
            var commandText1 = LoadPort1.CommandManualStageInspection();
            NoteCommand(commandText1);

            var commandText2 = LoadPort2.CommandManualStageInspection();
            NoteCommand(commandText2);
            Repeat();

        }

        [TestMethod]
        public void TestCommandManualStageDown()
        {   //~118,ManualStageDown@
            
            var commandText1 = LoadPort1.CommandManualStageDown();
            NoteCommand(commandText1);
            var commandText2 = LoadPort2.CommandManualStageDown();
            NoteCommand(commandText2);
            Repeat();


        }

        [TestMethod]
        public void TestCommandManualStageOPR()
        {  // ~119,ManualStageOPR@
           var commandText1 = LoadPort1.CommandManualStageOPR();
            NoteCommand(commandText1);
            var commandText2 = LoadPort2.CommandManualStageOPR();
            NoteCommand(commandText2);
            Repeat();
            // LoadPort2.CommandManualStageOPR();//POD Problem
        }

        [TestMethod]
        public void TestCommandManualVacuumOn()
        {    //~120,ManualVacuumOn@
            var commandText1 = LoadPort1.CommandManualVacuumOn();
            NoteCommand(commandText1);

            var commandText2 = LoadPort2.CommandManualVacuumOn();
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod] 
        public void TestCommandManualVacuumOff()
        {  //~121,ManualVacuumOff@
            var commandText1 = LoadPort1.CommandManualVacuumOff();
            NoteCommand(commandText1);
            var commandText2 = LoadPort2.CommandManualVacuumOff();
            NoteCommand(commandText2);
            Repeat();

        }
        #endregion

        #region Event Handler
        private void OnPlacement(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnPlacementEventArgs)args;
            // vs 2013
            // NoteEventResult($"IP={loadport.ServerEndPoint},Event={nameof(OnPlacement).Replace("On","")}", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
            NoteEventResult("IP=" +loadport.ServerEndPoint+ ",Event=" + "OnPlacement".Replace("On","") ,  eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }

        private void OnPresent(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnPresentEventArgs)args;
            // vs 2013
            // NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnPresent).Replace("On", "")}", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
            NoteEventResult("IP=" + loadport.ServerEndPoint + ", Event=" + "OnPresent".Replace("On", ""), eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }

        private void OnClamper(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnClamperEventArgs)args;
            // vs 2013
            //NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnClamper).Replace("On", "")}", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
            NoteEventResult("IP=" + loadport.ServerEndPoint +", Event=" + "OnClamper".Replace("On", "") , eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }
        private void OnRFID(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnRFIDEventArgs)args;
            // vs 2013
            // NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnRFID).Replace("On", "")}", "RFID:" + eventArgs.RFID);
            NoteEventResult("IP=" + loadport.ServerEndPoint + ", Event=" + "OnRFID".Replace("On", ""),"RFID:" + eventArgs.RFID);
        }
        private void OnBarcode_ID(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnBarcode_IDEventArgs)args;
            if (eventArgs.ReturnCode == EventBarcodeIDCode.Success)
            {
                 // vs 2013
                //  NoteEventResult($"IP={loadport.ServerEndPoint},  Event={nameof(OnBarcode_ID).Replace("On", "")}", "Barcode ID:" + eventArgs.BarcodeID);
                NoteEventResult("IP="  + loadport.ServerEndPoint + ",  Event=" + "OnBarcode_ID".Replace("On", "") , " Barcode ID:" + eventArgs.BarcodeID);
            }
            else
            {
                // vs 2013
                // NoteEventResult($"IP={loadport.ServerEndPoint},  Event={nameof(OnBarcode_ID).Replace("On", "")}", "請取 Barcode ID失敗");
                NoteEventResult("IP="  +loadport.ServerEndPoint + ",  Event=" + "OnBarcode_ID".Replace("On", "") , "請取 Barcode ID失敗");
            }
        }
        private void OnClamperLockComplete(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
           // var s = nameof(OnClamperLockComplete);
            var eventArgs = (OnClamperLockCompleteEventArgs)args;
            // vs 2013
            // NoteEventResult($"IP={loadport.ServerEndPoint},   Event={nameof(OnClamperLockComplete).Replace("On", "")}", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
            NoteEventResult("IP=" + loadport.ServerEndPoint +",   Event=" + "OnClamperLockComplete".Replace("On", ""), eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }
        private void OnVacuumComplete(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnVacuumCompleteEventArgs)args;
             // vs 2013
            // NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnVacuumComplete).Replace("On", "")}", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
            NoteEventResult("IP=" + loadport.ServerEndPoint + ", Event="  + "OnVacuumComplete".Replace("On", ""), eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }
        private void OnDockPODStart(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
              // vs 2013
            //NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnDockPODStart).Replace("On", "")}");
            NoteEventResult("IP=" + loadport.ServerEndPoint + ", Event=" + "OnDockPODStart".Replace("On", ""));
        }


        private void OnDockPODComplete_HasReticle(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
           // vs 2013
            //NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnDockPODComplete_HasReticle).Replace("On", "")}"); 
            NoteEventResult("IP=" + loadport.ServerEndPoint + ", Event=" + "OnDockPODComplete_HasReticle".Replace("On", ""));
        }
        private void OnDockPODComplete_Empty(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            // vs 2013
            // NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnDockPODComplete_Empty).Replace("On", "")}");
            NoteEventResult("IP=" + loadport.ServerEndPoint + ", Event=" + "OnDockPODComplete_Empty".Replace("On", ""));
        }


        private void OnUndockComplete(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            // vs 2013
            // NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnUndockComplete).Replace("On", "")}");
            NoteEventResult("IP=" + loadport.ServerEndPoint + ", Event=" + "OnUndockComplete".Replace("On", ""));
        }

        private void OnClamperUnlockComplete(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            // vs 2013
            // NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnClamperUnlockComplete).Replace("On", "")}");
            NoteEventResult("IP=" + loadport.ServerEndPoint + ", Event=" + "OnClamperUnlockComplete".Replace("On", ""));
        }

        private void OnAlarmResetSuccess(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            if (loadport.HasInvokeOriginalMethod)
            {
                loadport.InvokeOriginalMethod();
            }
            // vs 2013
            // NoteEventResult($"IP={loadport.ServerEndPoint},  Event={nameof(OnAlarmResetSuccess).Replace("On", "")}");
            NoteEventResult("IP=" + loadport.ServerEndPoint + ",  Event=" +  "OnAlarmResetSuccess".Replace("On", ""));
        }
        private void OnAlarmResetFail(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            // vs 2013
            // NoteEventResult($"IP={loadport.ServerEndPoint},  Event={nameof(OnAlarmResetFail).Replace("On", "")}");
            NoteEventResult("IP=" + loadport.ServerEndPoint + ",  Event=" + "OnAlarmResetFail".Replace("On", ""));

        }
        private void OnExecuteInitialFirst(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            /**
            if (loadport.HasInvokeOriginalMethod)
            {
                loadport.CommandInitialRequest();
            }
    */
             // vs 2013
            // NoteEventResult($"IP={loadport.ServerEndPoint},  Event={nameof(OnExecuteInitialFirst).Replace("On", "")}");
            NoteEventResult("IP="  + loadport.ServerEndPoint + ",  Event=" + "OnExecuteInitialFirst".Replace("On", ""));
        }
        private void OnExecuteAlarmResetFirst(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            /**
            if (loadport.HasInvokeOriginalMethod)
            {
                loadport.CommandAlarmReset();
            }*/
             // vs 2013
            // NoteEventResult($"IP={loadport.ServerEndPoint},  Event={nameof(OnExecuteAlarmResetFirst).Replace("On", "")}");
            NoteEventResult("IP="  + loadport.ServerEndPoint + ",  Event=" + "OnExecuteAlarmResetFirst".Replace("On", ""));
        }
        private void OnStagePosition(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnStagePositionEventArgs)args;
            // vs 2013
            // NoteEventResult($"IP={loadport.ServerEndPoint},   Event={nameof(OnStagePosition).Replace("On", "")}", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
            NoteEventResult("IP=" + loadport.ServerEndPoint + ",   Event=" +"OnStagePosition".Replace("On", ""), eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }
        private void OnLoadportStatus(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnLoadportStatusEventArgs)args;
            // vs 2013
            //  NoteEventResult($"IP={loadport.ServerEndPoint},   Event={nameof(OnLoadportStatus).Replace("On", "")}", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
            NoteEventResult("IP=" + loadport.ServerEndPoint + ",   Event=" + "OnLoadportStatus".Replace("On", ""), eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }
        private void OnInitialComplete(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            /**
            if (loadport.HasInvokeOriginalMethod)
            {
                loadport.CommandAlarmReset();
            }*/
            // vs 2013
            //  NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnInitialComplete).Replace("On", "")}");
            NoteEventResult("IP=" + loadport.ServerEndPoint + ", Event=" + "OnInitialComplete".Replace("On", ""));

        }

        /// <summary>自訂的未完成初始事件</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnInitialUnComplete(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            // vs 2013
            // NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnInitialUnComplete).Replace("On", "")}");
            NoteEventResult("IP=" + loadport.ServerEndPoint + ", Event=" + "OnInitialUnComplete".Replace("On", ""));
        }
        private void OnMustInAutoMode(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            // vs 2013
            //NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnMustInAutoMode).Replace("On", "")}");
            NoteEventResult("IP=" + loadport.ServerEndPoint + ", Event=" + "OnMustInAutoMode".Replace("On", ""));
        }

        private void OnMustInManualMode(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;

            // vs 2013
            //NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnMustInManualMode).Replace("On", "")}");
            NoteEventResult("IP=" + loadport.ServerEndPoint + ", Event=" + "OnMustInManualMode".Replace("On", ""));
        }

        private void OnClamperNotLock(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            // vs 2013
             //NoteEventResult($"IP={loadport.ServerEndPoint}, Event={nameof(OnClamperNotLock).Replace("On", "")}");
            NoteEventResult("IP=" + loadport.ServerEndPoint +", Event=" + "OnClamperNotLock".Replace("On", ""));
        }

        private void OnPODNotPutProperly(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;

            // vs 2013
             //NoteEventResult($"IP={loadport.ServerEndPoint},  Event={nameof(OnPODNotPutProperly).Replace("On", "")}");
            NoteEventResult("IP=" + loadport.ServerEndPoint+  ",  Event=" + "OnPODNotPutProperly".Replace("On", ""));
        }
        #endregion

        #region Alarm Handler
        private void OnClamperActionTimeOut(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            // vs 2013
            // NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnClamperActionTimeOut).Replace("On", "")}");
            NoteAlarmResult("IP="  + loadport.ServerEndPoint + ",  Alarm=" + "OnClamperActionTimeOut".Replace("On", ""));

        }
        private void OnClamperUnlockPositionFailed(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            // vs 2013
            // NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnClamperUnlockPositionFailed).Replace("On", "")}");
            NoteAlarmResult("IP=" +loadport.ServerEndPoint + ",  Alarm=" + "OnClamperUnlockPositionFailed".Replace("On", ""));
        }
        private void OnVacuumAbnormality(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
           // vs 2013
            //NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnVacuumAbnormality).Replace("On", "")}");
            NoteAlarmResult("IP=" + loadport.ServerEndPoint +",  Alarm=" + "OnVacuumAbnormality".Replace("On", ""));
        }

        private void OnStageMotionTimeout(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            // vs 2013
            //NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnStageMotionTimeout).Replace("On", "")}");
            NoteAlarmResult("IP=" + loadport.ServerEndPoint + ",  Alarm=" + "OnStageMotionTimeout".Replace("On", ""));
        }
        private void OnStageOverUpLimitation(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            //  vs 2013
            //  NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnStageOverUpLimitation).Replace("On", "")}");
            NoteAlarmResult("IP=" + loadport.ServerEndPoint + ",  Alarm="  + "OnStageOverUpLimitation".Replace("On", ""));
        }
        private void OnStageOverDownLimitation(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            //  vs 2013
            // NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnStageOverDownLimitation).Replace("On", "")}");
            NoteAlarmResult("IP=" + loadport.ServerEndPoint + ",  Alarm=" + "OnStageOverDownLimitation".Replace("On", ""));
        }
        private void OnReticlePositionAbnormality(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            //  vs 2013
             // NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnStageOverDownLimitation).Replace("On", "")}");
            NoteAlarmResult("IP=" + loadport.ServerEndPoint + ",  Alarm=" + "OnReticlePositionAbnormality".Replace("On", ""));
        }
        private void OnClamperLockPositionFailed(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            //  vs 2013
            // NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnClamperLockPositionFailed).Replace("On", "")}");
            NoteAlarmResult("IP=" + loadport.ServerEndPoint + ",  Alarm=" + "OnClamperLockPositionFailed".Replace("On", ""));
        }
        private void OnPODPresentAbnormality(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
             //  vs 2013
            //NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnPODPresentAbnormality).Replace("On", "")}");
            NoteAlarmResult("IP=" + loadport.ServerEndPoint + ",  Alarm=" + "OnPODPresentAbnormality".Replace("On", ""));
        }
        private void OnClamperMotorAbnormality(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
             //  vs 2013
            // NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnClamperMotorAbnormality).Replace("On", "")}");
            NoteAlarmResult("IP=" + loadport.ServerEndPoint + ",  Alarm=" + "OnClamperMotorAbnormality".Replace("On", ""));
        }
        private void OnStageMotorAbnormality(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            //  vs 2013
            // NoteAlarmResult($"IP={loadport.ServerEndPoint},  Alarm={nameof(OnStageMotorAbnormality).Replace("On", "")}");
            NoteAlarmResult("IP="  + loadport.ServerEndPoint  + ",  Alarm=" + "OnStageMotorAbnormality".Replace("On", ""));
        }
        #endregion


        #region Auxiliary
        private void NoteCommand(string command)
        {
            Debug.WriteLine("[COMMAND]  " + command);
        }

        private void NoteEventResult(string eventName)
        {
            Debug.WriteLine("[EVENT]  " + eventName);
        }
        private void NoteEventResult(string eventName, string result)
        {
            Debug.WriteLine("[EVENT]  " + eventName + ",  [RESULT]  " + result);
        }
        private void NoteAlarmResult(string alarmName)
        {
            Debug.WriteLine("[ALARM]  " + alarmName);
        }
#endregion




        #region old
        [TestMethod]
        public void TestSetParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var lp = halContext.HalDevices[MacEnumDevice.loadportA_assembly.ToString()] as MacHalLoadPort;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                lp.HalConnect();

                lp.SetPressureDiffLimit(40, 50);
            }
        }

        [TestMethod]
        public void TestReadParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var lp = halContext.HalDevices[MacEnumDevice.loadportA_assembly.ToString()] as MacHalLoadPort;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                lp.HalConnect();

                lp.ReadPressureDiffLimitSrtting();
            }
        }

        [TestMethod]
        public void TestReadComponentValue()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var lp = halContext.HalDevices[MacEnumDevice.loadportA_assembly.ToString()] as MacHalLoadPort;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                lp.HalConnect();

                lp.ReadPressureDiff();
                lp.ReadLP_Light_Curtain();
            }
        }

        [TestMethod]
        public void TestAssemblyWork()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var lp = halContext.HalDevices[MacEnumDevice.loadportA_assembly.ToString()] as MacHalLoadPort;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                lp.HalConnect();
            }
        }
        #endregion
       




        [TestMethod]
        public void TestMethod1()
        {
            using (var loadport = new MvGudengLoadPortCollection())
            {
                loadport.ConnectIfNo();


            }

        }
    }
}
