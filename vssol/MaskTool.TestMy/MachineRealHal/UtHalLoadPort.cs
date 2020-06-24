using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.GudengLoadPort;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;
using static MvAssistant.DeviceDrive.GudengLoadPort.MvGudengLoadPortLdd;
using MvAssistant.DeviceDrive.GudengLoadPort.LoadPortEventArgs;
using MvAssistant.DeviceDrive.GudengLoadPort.ReplyCode;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalLoadPort
    {

        MvGudengLoadPortLdd LoadPort1 = null;
        MvGudengLoadPortLdd LoadPort2 = null;
        MvGudengLoadPortCollection ldd = new MvGudengLoadPortCollection();
      public UtHalLoadPort()
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
            foreach(var loadport in ldd.LoadPorts)
            {
                loadport.OnAlarmResetFailHandler += this.OnAlarmResetFail;//014
                loadport.OnAlarmResetSuccessHandler += this.OnAlarmResetSuccess;//013
                loadport.OnBarcode_IDHandler += this.OnBarcode_ID;//005
                loadport.OnClamperActionTimeOutHandler += this.OnClamperActionTimeOut;// 200
                loadport.OnClamperHandler += this.OnClamper;//003
                loadport.OnClamperLockCompleteHandler += this.OnClamperLockComplete;//012
                loadport.OnClamperLockPositionFailed += this.OnClamperLockPositionFailed;//207
                loadport.OnClamperMotorAbnormality += this.OnClamperMotorAbnormality;//209
                loadport.OnClamperNotLockHandler += this.OnClamperNotLock;//022
                loadport.OnClamperUnlockCompleteHandler += this.OnClamperUnlockComplete;//006
                loadport.OnClamperUnlockPositionFailedHandler += this.OnClamperUnlockPositionFailed;//201
                loadport.OnCoverDisappearHandler += this.OnCoverDisappear;//208
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
                loadport.OnStageMotorAbnormality += this.OnStageMotorAbnormality;//210
                loadport.OnStageOverDownLimitationHandler += this.OnStageOverDownLimitation;//205
                loadport.OnStageOverUpLimitationHandler += this.OnStageOverUpLimitation;//204
                loadport.OnStagePositionHandler += this.OnStagePosition;//017
                loadport.OnUndockCompleteHandler += this.OnUndockComplete;//011
                loadport.OnVacuumAbnormalityHandler += this.OnVacuumAbnormality;//202
                loadport.OnVacuumCompleteHandler += this.OnVacuumComplete;//007

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
        {
            /**
              // LoadPort2.CommandAlarmReset();
             // LoadPort2.CommandInitialRequest();
             // return;
              // ~109,AlarmReset@
              LoadPort1.CommandAlarmReset(); //~013,AlarmResetSuccess@

              //~112,InitialRequest@
              LoadPort1.CommandInitialRequest();//[~018,LoadportStatus,2@][~202,VacuumAbnormality@~018,LoadportStatus,3@]
              // 沒有收到 019,InitialComplete()
              */

          //  LoadPort1.SetOriginalMethod(LoadPort1.CommandInitialRequest);
           var commandText1= LoadPort1.CommandInitialRequest();
            var commandText2 = LoadPort2.CommandInitialRequest();
            NoteCommand(commandText1);//~002,Present,1@
            NoteCommand(commandText2);
            Repeat();
        }

        /// <summary>(100)</summary>
        [TestMethod]
        public void TestCommandDockRequest()
        {   //~100,DockRequest@
            
            /**
           LoadPort1.CommandAlarmReset();  // ~013,AlarmResetSuccess@
           LoadPort1.CommandInitialRequest();//[~018,LoadportStatus,2@],[~202,VacuumAbnormality@~018,LoadportStatus,3@]
                                              // LoadPort1.CommandAlarmReset();

            // ~100,DockRequest@
            LoadPort1.CommandDockRequest();// ~015,ExecuteInitialFirst@  有沒有(沒有實際的 POD 無法測)
            */

            //   LoadPort1.SetOriginalMethod(LoadPort1.CommandDockRequest);
            var commandText = LoadPort1.CommandDockRequest();
            NoteCommand(commandText);//~002,Present,1@
            Repeat();
        }

        /// <summary>(101)</summary>
        [TestMethod]
        public void TestCommandUndockRequest()
        { //~101,UndockRequest@
            /**
            LoadPort1.CommandAlarmReset();//~013,AlarmResetSuccess@
            LoadPort1.CommandInitialRequest();
            //~101,UndockRequest@
            LoadPort1.CommandUndockRequest(); //~020,MustInAutoMode@(缺)(若是和前面指令一起連續執行則沒有回覆)
            */

            //LoadPort1.SetOriginalMethod(LoadPort1.CommandUndockRequest);
            var commandText1=LoadPort1.CommandUndockRequest();
            NoteCommand(commandText1);//~002,Present,1@
            Repeat();
        }

       

        [TestMethod]//[V].20    2020/06/23, [V].21   2020/06/23
        public void TestCommandAskPlacementStatus()
        {  //~102,AskPlacementStatus@

            //LoadPort1.SetOriginalMethod(LoadPort1.CommandAskPlacementStatus);
            var commandText1=LoadPort1.CommandAskPlacementStatus();//~001,Placement,1@
            var commandText2 = LoadPort2.CommandAskPlacementStatus();
            NoteCommand(commandText1);//~002,Present,1@
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod]//[V].20 2020/06/23,  //[V].21  2020/06/23
        public void TestCommandAskPresentStatus()
        { 
            // 2020/06/23 23:10 一直收到空白訊息
           
            var commandText1 = LoadPort1.CommandAskPresentStatus();
            var commandText2 = LoadPort2.CommandAskPresentStatus();
            NoteCommand(commandText1);//~002,Present,1@
            NoteCommand(commandText2);//~002,Present,1@
            Repeat();
        }

        [TestMethod] //[V].20 2020/06/23,  //[V].21 2020/06/23
        public void TestCommandAskClamperStatus()
        {  //~104,AskClamperStatus@
            //LoadPort1.SetOriginalMethod(LoadPort1.CommandAskClamperStatus);
            var commandText1=LoadPort1.CommandAskClamperStatus();//[~003,Clamper,0@],[~006,ClamperLockComplete,0@]
            var commandText2 = LoadPort2.CommandAskClamperStatus();
            NoteCommand(commandText1);
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod]//[V].20 2020/06.23,  [V].21 2020/06/23 (* No RFID)
        public void TestCommandAskRFIDStatus()
        { // ~105,AskRFIDStatus@
            //LoadPort1.SetOriginalMethod(LoadPort1.CommandAskRFIDStatus);
            //LoadPort1.SetOriginalMethod(LoadPort1.CommandAskRFIDStatus);
            var commandText1 = LoadPort1.CommandAskRFIDStatus();//~004,RFID,1343833305251403@
            var commandText2 = LoadPort2.CommandAskRFIDStatus();//~004,RFID,1343833305251403@
            NoteCommand(commandText1);//~002,Present,1@
            NoteCommand(commandText2);//~002,Present,1@
            Repeat();
        }

        [TestMethod]//[V].20 2020/06/23(* no barcode),  [V].21 2020/06/23 (* no  barcode) 
        public void TestCommandAskBarcodeStatus()
        {  //~106,AskBarcodeStatus@
            //LoadPort1.SetOriginalMethod(LoadPort1.CommandAskBarcodeStatus);
            var commandText1 = LoadPort1.CommandAskBarcodeStatus();//~005,Barcode ID,0@
            var commandText2 = LoadPort2.CommandAskBarcodeStatus();
            NoteCommand(commandText1);//~002,Present,1@
            NoteCommand(commandText2);//~002,Present,1@
            Repeat();
        }

        [TestMethod]//[V].20 2020/06/23(* has vacuum), [V].21 2020/06/23(no vacuum)
        public void TestCommandAskVacuumStatus()
        {
            //~107,AskVacuumStatus@
            //LoadPort1.SetOriginalMethod(LoadPort1.CommandAskVacuumStatus);
            var commandText1 = LoadPort1.CommandAskVacuumStatus();//~007,VacuumComplete,0@
            var commandText2 = LoadPort2.CommandAskVacuumStatus();//~007,VacuumComplete,0@
            NoteCommand(commandText1);//~002,Present,1@
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod]//[V].20 2020/06/23, [V].21 2020/06/23
        public void TestCommandAskReticleExistStatus()
        {
            //~108,AskReticleExistStatus@
           // LoadPort1.SetOriginalMethod(LoadPort1.CommandAskReticleExistStatus);
            var commandText1 = LoadPort1.CommandAskReticleExistStatus();//~010,DockPODComplete_Empty@
            var commandText2 = LoadPort2.CommandAskReticleExistStatus();//~010,DockPODComplete_Empty@
            NoteCommand(commandText1);//~002,Present,1@
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod] //[V].20 2020/06/23,[V].21 2020/06/23
        public void TestCommandAlarmReset()
        {
            /**
             //~109,AlarmReset@
             LoadPort1.CommandAlarmReset();//~013,AlarmResetSuccess@
             LoadPort2.CommandAlarmReset();//~013,AlarmResetSuccess@
            */
          //  LoadPort1.SetOriginalMethod(LoadPort1.CommandAlarmReset);
            var commandText1 = LoadPort1.CommandAlarmReset();
            var commandText2 = LoadPort2.CommandAlarmReset();
            NoteCommand(commandText1);//~002,Present,1@
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod]
        public void TestCommandAskStagePosition()
        {//~110,AskStagePosition@

            /**
             
             //LoadPort2.CommandAskStagePosition();// ~017,StagePosition,0@
             */


            // LoadPort1.ReleaseCommandCasadeMode(); ??
            LoadPort1.SetOriginalMethod(LoadPort1.CommandAskStagePosition);
            var commandText = LoadPort1.CommandAskStagePosition();// ~017,StagePosition,3@
            NoteCommand(commandText);//~002,Present,1@
            Repeat();
        }

        [TestMethod]
        public void TestCommandAskLoadportStatus()
        { // ~111,AskLoadportStatus@
            /**
           
            LoadPort2.CommandAskLoadportStatus();//~018,LoadportStatus,1@
            */


            // LoadPort1.ReleaseCommandCasadeMode(); ??
            LoadPort1.SetOriginalMethod(LoadPort1.CommandAskLoadportStatus);
            var commandText = LoadPort1.CommandAskLoadportStatus();//~018,LoadportStatus,4
            NoteCommand(commandText);//~002,Present,1@
            Repeat();

        }

        [TestMethod]
        public void TestCommandManualClamperLock()
        { //~113,ManualClamperLock@
            /**
              //LoadPort2.CommandInitialRequest();
             
              //LoadPort2.CommandManualClamperLock();//POD Problem
              */


            // LoadPort1.ReleaseCommandCasadeMode(); ??
            LoadPort1.SetOriginalMethod(LoadPort1.CommandManualClamperLock);
            var commandText = LoadPort1.CommandManualClamperLock();//~003,Clamper,1@(第二次執行時沒有回覆)
            NoteCommand(commandText);//~002,Present,1@
            Repeat();

        }

        [TestMethod]
        public void TestCommandManualClamperUnlock()
        {
            //~114,ManualClamperUnlock@
            LoadPort1.SetOriginalMethod(LoadPort1.CommandManualClamperUnlock);
            var commandText = LoadPort1.CommandManualClamperUnlock();//~003,Clamper,0@
                                                                //LoadPort2.CommandInitialRequest();
                                                                // LoadPort2.CommandManualClamperUnlock();//POD Problem
            NoteCommand(commandText);//~002,Present,1@
            Repeat();
        }

        [TestMethod]
        public void TestCommandManualClamperOPR()
        {
            //~115,ManualClamperOPR@
            LoadPort1.SetOriginalMethod(LoadPort1.CommandManualClamperOPR);
            var commandText = LoadPort1.CommandManualClamperOPR();// [RETURN] ~003,Clamper,0@
                                                             // LoadPort2.CommandManualClamperOPR();// [RETURN] ~003,Clamper,0@
            NoteCommand(commandText);//~002,Present,1@
            Repeat();
        }

        [TestMethod]
        public void TestCommandManualStageUp()
        {   //~116,ManualStageUp@
            // LoadPort1.CommandAlarmReset();
            // LoadPort1.CommandInitialRequest();//[~018,LoadportStatus,2@],[ ~202,VacuumAbnormality@~018,LoadportStatus,3@]

            LoadPort1.SetOriginalMethod(LoadPort1.CommandManualStageUp);
            var commandText = LoadPort1.CommandManualStageUp();//  ~022,ClamperNotLock@
                                                          // LoadPort2.CommandInitialRequest();
                                                          // LoadPort2.CommandManualStageUp();// POD Problem
            NoteCommand(commandText);//~002,Present,1@
            Repeat();
        }

        [TestMethod]
        public void TestCommandManualStageInspection()
        {  //~116,ManualStageUp@
            //  LoadPort1.CommandAlarmReset();
            //    LoadPort1.CommandInitialRequest();
            //~117,ManualStageInspection@
            LoadPort1.SetOriginalMethod(LoadPort1.CommandManualStageInspection);
            var commandText = LoadPort1.CommandManualStageInspection();// ~022,ClamperNotLock@
                                                                  //LoadPort2.CommandInitialRequest();
                                                                  //  LoadPort2.CommandManualStageInspection(); //POD Problem
            NoteCommand(commandText);//~002,Present,1@
            Repeat();

        }

        [TestMethod]
        public void TestCommandManualStageDown()
        {   //~118,ManualStageDown@

            // LoadPort1.CommandAlarmReset();
            /**
             [RETURN] ~001,Placement,0@~002,Present,0@~002,Present,1@~002,Present,0@~002,Present,1@~002,Present,0@~002,Present,1@~002,Present,0@~002,Present,1@
[INVOKE METHOD] Placement, Parameter: 0
[INVOKE METHOD] Present, Parameter: 0
[INVOKE METHOD] Present, Parameter: 1
[INVOKE METHOD] Present, Parameter: 0
[INVOKE METHOD] Present, Parameter: 1
[INVOKE METHOD] Present, Parameter: 0
[INVOKE METHOD] Present, Parameter: 1
[INVOKE METHOD] Present, Parameter: 0
[INVOKE METHOD] Present, Parameter: 1
[RETURN] ~002,Present,0@~002,Present,1@~002,Present,0@~002,Present,1@~001,Placement,1@~001,Placement,0@
             */
            //  LoadPort1.CommandInitialRequest();

            LoadPort1.SetOriginalMethod(LoadPort1.CommandManualStageDown);
            var commandText = LoadPort1.CommandManualStageDown();// ~022,ClamperNotLock@
            NoteCommand(commandText);//~002,Present,1@
            Repeat();

            //  LoadPort2.CommandAlarmReset();
            //                                      LoadPort2.CommandInitialRequest();
            // LoadPort2.CommandManualStageDown();// POD Problem
        }

        [TestMethod]
        public void TestCommandManualStageOPR()
        {  // ~119,ManualStageOPR@

            LoadPort1.SetOriginalMethod(LoadPort1.CommandManualStageOPR);
            var commandText = LoadPort1.CommandManualStageOPR();//~022,ClamperNotLock@
            NoteCommand(commandText);//~002,Present,1@
            Repeat();
            // LoadPort2.CommandManualStageOPR();//POD Problem
        }

        [TestMethod]
        public void TestCommandManualVacuumOn()
        {    //~120,ManualVacuumOn@

            LoadPort1.SetOriginalMethod(LoadPort1.CommandManualVacuumOn);
            var commandText = LoadPort1.CommandManualVacuumOn(); // ~007,VacuumComplete,1@
            NoteCommand(commandText);//~002,Present,1@
            Repeat();
            //  LoadPort2.CommandManualVacuumOn();// ~007,VacuumComplete,1@
        }

        [TestMethod]
        public void TestCommandManualVacuumOff()
        {  //~121,ManualVacuumOff@
            LoadPort1.SetOriginalMethod(LoadPort1.CommandManualVacuumOff);
            var commandText = LoadPort1.CommandManualVacuumOff();// ~007,VacuumComplete,0@
            NoteCommand(commandText);//~002,Present,1@
            Repeat();
            //LoadPort2.CommandManualVacuumOff();// ~007,VacuumComplete,0@
        }
        #endregion

        [TestMethod]
        public void TestSetParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var lp = halContext.HalDevices[MacEnumDevice.loadport_assembly.ToString()] as MacHalLoadPort;
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

                var lp = halContext.HalDevices[MacEnumDevice.loadport_assembly.ToString()] as MacHalLoadPort;
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

                var lp = halContext.HalDevices[MacEnumDevice.loadport_assembly.ToString()] as MacHalLoadPort;
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

                var lp = halContext.HalDevices[MacEnumDevice.loadport_assembly.ToString()] as MacHalLoadPort;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                lp.HalConnect();
            }
        }

        #region Event Handler
        private void OnPlacement(object sender,EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnPlacementEventArgs)args;
            NoteEventResult($"IP={loadport.ServerEndPoint.Address.ToString()}, Placement", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }

        private void OnPresent(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnPresentEventArgs)args;
            NoteEventResult($"IP={loadport.ServerEndPoint.Address.ToString()},   Present", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }

        private void OnClamper(object sender,EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnClamperEventArgs)args;
            NoteEventResult($"IP={loadport.ServerEndPoint.Address.ToString()}, Clamper", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }
        private void OnRFID(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnRFIDEventArgs)args;
            NoteEventResult($"IP={loadport.ServerEndPoint.Address.ToString()}, RFID", "RFID:" +eventArgs.RFID);
        }
        private void OnBarcode_ID(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnBarcode_IDEventArgs)args;
            if (eventArgs.ReturnCode == EventBarcodeIDCode.Success)
            {
                NoteEventResult($"IP={loadport.ServerEndPoint.Address.ToString()},  Barcode ID", "Barcode ID:" + eventArgs.BarcodeID);
            }
            else
            {
                NoteEventResult($"IP={loadport.ServerEndPoint.Address.ToString()},  Barcode ID", "請取 Barcode ID失敗");
            }
        }
        private void OnClamperUnlockComplete(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnClamperUnlockCompleteEventArgs)args;
            NoteEventResult($"IP={loadport.ServerEndPoint.Address.ToString()},  ClamperUnlockComplete", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }
        private void OnVacuumComplete(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnVacuumCompleteEventArgs)args;
            NoteEventResult($"IP={loadport.ServerEndPoint.ToString()}, VacuumComplete", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }
        private void OnDockPODStart(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            
            NoteEventResult("DockPODStart");
        }

      
        private void OnDockPODComplete_HasReticle(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            NoteEventResult("DockPODComplete_HasReticle");
        }
        private void OnDockPODComplete_Empty(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            NoteEventResult($"IP={loadport.ServerEndPoint.Address.ToString()}, DockPODComplete_Empty");
        }

        private void OnUndockComplete (object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            NoteEventResult("UndockComplete");
        }

        private void OnClamperLockComplete(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            NoteEventResult($"IP={loadport.ServerEndPoint.Address.ToString()}, ClamperLockComplete");
        }

        private void OnAlarmResetSuccess(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            if (loadport.HasInvokeOriginalMethod)
            {
                loadport.InvokeOriginalMethod();
            }
            NoteEventResult($"IP={loadport.ServerEndPoint.Address.ToString()},  AlarmResetSuccess");
        }
        private void OnAlarmResetFail(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            NoteEventResult("AlarmResetFail");

        }
        private void OnExecuteInitialFirst(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            if (loadport.HasInvokeOriginalMethod)
            {
                 loadport.CommandInitialRequest();
            }
            NoteEventResult("ExecuteInitialFirst");
        }
        private void OnExecuteAlarmResetFirst(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            if (loadport.HasInvokeOriginalMethod)
            {
                loadport.CommandAlarmReset();
            }
            NoteEventResult("ExecuteAlarmResetFirst");
        }
        private void OnStagePosition(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnStagePositionEventArgs)args;
            NoteEventResult("StagePosition", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }
        private void OnLoadportStatus(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnLoadportStatusEventArgs)args;
            NoteEventResult("LoadportStatus", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }
        private void OnInitialComplete(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            if (loadport.HasInvokeOriginalMethod)
            {
                loadport.CommandAlarmReset();
            }
            NoteEventResult($"IP={loadport.ServerEndPoint.Address.ToString()}, InitialComplete");

        }

        private void OnInitialUnComplete(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;

            NoteEventResult("InitialUnComplete");
        }
        private void OnMustInAutoMode(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;

            NoteEventResult("MustInAutoMode");
        }

        private void OnClamperNotLock(object sender,EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;

            NoteEventResult("ClamperNotLock");
        }

        private void OnPODNotPutProperly(object sender,EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;

            NoteEventResult("PODNotPutProperly"); 
        }
        #endregion
        
        #region Alarm Handler
        private void OnClamperActionTimeOut(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;

            NoteAlarmResult("ClamperActionTimeOut");

        }
        private void OnClamperUnlockPositionFailed(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;

            NoteAlarmResult("ClamperUnlockPositionFailed");
        }
        private void OnVacuumAbnormality(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            NoteAlarmResult("VacuumAbnormality");
        }
        private void OnStageMotionTimeout(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            NoteAlarmResult("StageMotionTimeout");
        }
        private void OnStageOverUpLimitation(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            NoteAlarmResult("StageOverUpLimitation");
        }
        private void OnStageOverDownLimitation(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            NoteAlarmResult("StageOverDownLimitation");
        }
        private void OnReticlePositionAbnormality(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            NoteAlarmResult("ReticlePositionAbnormality");
        }
        private void OnClamperLockPositionFailed(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            NoteAlarmResult("ClamperLockPositionFailed");
        }
        private void OnCoverDisappear(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            NoteAlarmResult("CoverDisappear");
        }
        private void OnClamperMotorAbnormality(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            NoteAlarmResult("ClamperMotorAbnormality");
        }
        private void OnStageMotorAbnormality(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            NoteAlarmResult("StageMotorAbnormality");
        }
        #endregion

 

        private void NoteCommand(string command)
        {
            Debug.WriteLine("[COMMAND]  " + command);
        }

        private void NoteEventResult(string eventName)
        {
            Debug.WriteLine("[EVENT]  " + eventName);
        }
        private void NoteEventResult(string eventName,string result)
        {
            Debug.WriteLine("[EVENT]  " + eventName   + ",  [RESULT]  " + result);
        }
        private void NoteAlarmResult(string alarmName)
        {
            Debug.WriteLine("[ALARM]  " + alarmName);
        }
    }
}
