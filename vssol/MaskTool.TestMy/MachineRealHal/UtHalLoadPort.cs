using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.GudengLoadPort;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;
using MvAssistant.DeviceDrive.GudengLoadPort.LoadPortEventArgs;
using MvAssistant.DeviceDrive.GudengLoadPort.ReplyCode;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalLoadPort
    {
#region Ready To Abandon
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
                loadport.OnClamperUnlockCompleteHandler += this.OnClamperLockComplete;//012
                loadport.OnClamperLockPositionFailedHandler += this.OnClamperLockPositionFailed;//207
                loadport.OnClamperMotorAbnormalityHandler += this.OnClamperMotorAbnormality;//209
                loadport.OnClamperNotLockHandler += this.OnClamperNotLock;//022
                loadport.OnClamperLockCompleteHandler += this.OnClamperUnlockComplete;//006
                loadport.OnClamperUnlockPositionFailedHandler += this.OnClamperUnlockPositionFailed;//201
                loadport.OnPODPresentAbnormalityHandler += this.OnCoverDisappear;//208
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
        [TestMethod]//[V].20 2020/06/24, [V].21   2020/06/24
        public void TestCommandInitialRequest()
        {//~112,InitialRequest@
         /** .20
          * [
          * ~018,LoadportStatus,2@ |
          * ~003,Clamper,1@~012,ClamperUnlockComplete@~019,InitialComplete@
          * ]
          */
            /** .21
            [~018,LoadportStatus,2@ | ~007,VacuumComplete,1@~003,Clamper,1@~012,ClamperUnlockComplete@~019,InitialComplete@~007,VacuumComplete,0@  ]
             */

            // LoadPort1.SetOriginalMethod(LoadPort1.CommandInitialRequest);
              var commandText1 = LoadPort1.CommandInitialRequest();
               NoteCommand(commandText1);//~002,Present,1@
            var commandText2 = LoadPort2.CommandInitialRequest();
             NoteCommand(commandText2);
            Repeat();
        }

        /// <summary>(100)</summary>
        [TestMethod]//[V].20 2020/06/24   [V].21 2020/06/24
        public void TestCommandDockRequest()
        {   //~100,DockRequest@
            /** .20
             * [~023,PODNotPutProperly@]
             */
            /** .21
             * [ ~008,DockPODStart@ | ~018,LoadportStatus,2@ | ~007,VacuumComplete,1@ | ~007,VacuumComplete,0@~004,RFID,1343833305251403@
             * | ~202,VacuumAbnormality@~017,StagePosition,3@ |  ~018,LoadportStatus,3@]
             */

             var commandText1 = LoadPort1.CommandDockRequest();
             NoteCommand(commandText1);
            //var commandText2 = LoadPort2.CommandDockRequest();
          //  NoteCommand(commandText2);
            Repeat();
        }

        /// <summary>(101)</summary>
        [TestMethod] //[V].20 2020/06/24  [V] .21/2020/06/24
        public void TestCommandUndockRequest()
        { //~101,UndockRequest@
          /** .20
           * [~008,DockPODStart@ | ~018,LoadportStatus,2@ | ~007,VacuumComplete,1@ | ~004,RFID,1343833305251403@ 
           * | ~003,Clamper,1@ | ~006,ClamperLockComplete,1@~017,StagePosition,3@ | ~017,StagePosition,1@~017,StagePosition,3@ |
           * ~005,Barcode ID,0@ |  ~017,StagePosition,2@ |~010,DockPODComplete_Empty@]
          */
            /** .21
             * [
             * ~018,LoadportStatus,2@ | ~007,VacuumComplete,1@~003,Clamper,1@~012,ClamperUnlockComplete@~011,UndockComplete@ | ~007,VacuumComplete,0@
             * ]
             */
            //LoadPort1.SetOriginalMethod(LoadPort1.CommandUndockRequest);
            /** var commandText1 =LoadPort1.CommandUndockRequest();
            NoteCommand(commandText1);
            */
            var commandText2 = LoadPort2.CommandUndockRequest();
            NoteCommand(commandText2);
            Repeat();
        }

       

        [TestMethod]//[V].20    2020/06/23, [V].21   2020/06/23
        public void TestCommandAskPlacementStatus()
        {  //~102,AskPlacementStatus@
           /** .20
            * [~001,Placement,0@]
            * */
            /**.21
             * [~001,Placement,1@]
             */

             var commandText1 =LoadPort1.CommandAskPlacementStatus();
             NoteCommand(commandText1);
            
            var commandText2 = LoadPort2.CommandAskPlacementStatus();
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod]//[V].20 2020/06/23,  //[V].21  2020/06/23
        public void TestCommandAskPresentStatus()
        { //~103,AskPresentStatus@
          /** .20
           * [~002,Present,0@]
           */
            /** .21
             * [~002,Present,1@]
             */
            var commandText1 = LoadPort1.CommandAskPresentStatus();
            NoteCommand(commandText1);

           var commandText2 = LoadPort2.CommandAskPresentStatus();
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod] //[V].20 2020/06/23,  //[V].21 2020/06/23
        public void TestCommandAskClamperStatus()
        {  //~104,AskClamperStatus@
           /**.20
            * [
            *  ~003,Clamper,0@ | ~006,ClamperLockComplete,0@
            * ]
            * */
            /**.21
              * [
            *  ~003,Clamper,0@ | ~006,ClamperLockComplete,0@
            * ]
             * */

            //LoadPort1.SetOriginalMethod(LoadPort1.CommandAskClamperStatus);
            var commandText1 =LoadPort1.CommandAskClamperStatus();
            NoteCommand(commandText1);
           
            var commandText2 = LoadPort2.CommandAskClamperStatus();
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod]//[V].20 2020/06.23,  [V].21 2020/06/23 
        public void TestCommandAskRFIDStatus()
        { // ~105,AskRFIDStatus@
          /** .20
           * [
           * ~004,RFID,ERROR@
           * ]
           */
            /** .21
             * [
             * ~004,RFID,1343833305251403@
             * ]
            */

            var commandText1 = LoadPort1.CommandAskRFIDStatus();
            NoteCommand(commandText1);

           var commandText2 = LoadPort2.CommandAskRFIDStatus();
           NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod]//[V].20 2020/06/23,  [V].21 2020/06/23 
        public void TestCommandAskBarcodeStatus()
        {  //~106,AskBarcodeStatus@
           /** .20
            * [
            * ~005,Barcode ID,0@
            * ]
           */
            /** .21
             * [
             * ~005,Barcode ID,0@
             * ]
             */
            //LoadPort1.SetOriginalMethod(LoadPort1.CommandAskBarcodeStatus);
            var commandText1 = LoadPort1.CommandAskBarcodeStatus();
            NoteCommand(commandText1);

           var commandText2 = LoadPort2.CommandAskBarcodeStatus();
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod]//[V].20 2020/06/23, [V].21 2020/06/23
        public void TestCommandAskVacuumStatus()
        {       //~107,AskVacuumStatus@

            /** .20
             * [
             * ~007,VacuumComplete,0@
             * ]
             */
            /** .21
             * [
             * ~007,VacuumComplete,0@
             * ]
             */

            //LoadPort1.SetOriginalMethod(LoadPort1.CommandAskVacuumStatus);
            var commandText1 = LoadPort1.CommandAskVacuumStatus();
            NoteCommand(commandText1);

            var commandText2 = LoadPort2.CommandAskVacuumStatus();
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod]//[V].20 2020/06/23, [V].21 2020/06/23
        public void TestCommandAskReticleExistStatus()
        {//~108,AskReticleExistStatus@
         /** .20
          * [
          * ~010,DockPODComplete_Empty@
          * ]
          */
            /**.21
             * [
             *  ~010,DockPODComplete_Empty@
             *  ]
             * */

            // LoadPort1.SetOriginalMethod(LoadPort1.CommandAskReticleExistStatus);
            var commandText1 = LoadPort1.CommandAskReticleExistStatus();
             NoteCommand(commandText1);

           var commandText2 = LoadPort2.CommandAskReticleExistStatus();
           NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod] //[V].20 2020/06/23,[V].21 2020/06/23
        public void TestCommandAlarmReset()
        {  //~109,AlarmReset@
           /**.20
            * [
            * ~013,AlarmResetSuccess@
            * ]
            */
            /**.21
             * [
             * ~013,AlarmResetSuccess@
             * ]
             */
            //  LoadPort1.SetOriginalMethod(LoadPort1.CommandAlarmReset);
            var commandText1 = LoadPort1.CommandAlarmReset();
             NoteCommand(commandText1);

            var commandText2 = LoadPort2.CommandAlarmReset();
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod]//[V].20 2020/06/24,[V].21 2020/06/24
        public void TestCommandAskStagePosition()
        {//~110,AskStagePosition@

            /** .20
          * [
          * ~017,StagePosition,0@
          * ]
          */
            /** .21
             * [
             * ~017,StagePosition,0@
             * ]
             */

            // LoadPort1.ReleaseCommandCasadeMode(); ??
            //LoadPort1.SetOriginalMethod(LoadPort1.CommandAskStagePosition);
            var commandText1 = LoadPort1.CommandAskStagePosition();
            NoteCommand(commandText1);
            var commandText2 = LoadPort2.CommandAskStagePosition();
           NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod]//[V].20 2020/06/23,[V].21 2020/06/23
        public void TestCommandAskLoadportStatus()
        { // ~111,AskLoadportStatus@
          /** .20
           * [~018,LoadportStatus,1@]
         */
            /** .21
             * [
             * ~018,LoadportStatus,1@
             * ]
            */

            // LoadPort1.ReleaseCommandCasadeMode(); ??
            // LoadPort1.SetOriginalMethod(LoadPort1.CommandAskLoadportStatus);
           var commandText1 = LoadPort1.CommandAskLoadportStatus();
            NoteCommand(commandText1);
            var commandText2 = LoadPort2.CommandAskLoadportStatus();
           NoteCommand(commandText2);
            Repeat();

        }

        [TestMethod]  //[V].20 2020/06/24,    //[V] .21   2020/06/24
       
        public void TestCommandManualClamperLock()
        { //~113,ManualClamperLock@
            /** Note
             *執行前先 執行AskClamperStatus,
             * 收到 clamper 事件後, 若為 unlock, 再執行本指令
             */
            /**.20(相同狀態不回覆)
             * [~021,MustInManualMode@]
             * ManualMode:[~003,Clamper,1@]
            */
            /**.21(相同狀態不回覆)
             * [~021,MustInManualMode@]
             * ManualMode:[~003,Clamper,1@]
             */
            // LoadPort1.ReleaseCommandCasadeMode(); ??
            //LoadPort1.SetOriginalMethod(LoadPort1.CommandManualClamperLock);
            var commandText1 = LoadPort1.CommandManualClamperLock();
            NoteCommand(commandText1);
            var commandText2 = LoadPort2.CommandManualClamperLock();
            NoteCommand(commandText2);
            Repeat();

        }

        [TestMethod] //[V] .20  2020/06/24,   [V] .21  2020/06/24,  
        public void TestCommandManualClamperUnlock()
        {  //~114,ManualClamperUnlock@
           /** Note
           *執行前先 執行AskClamperStatus,
           * 收到 clamper 事件後, 若為 lock, 再執行本指令
           */
            /**.20(相同狀態不回覆)
             * ~021,MustInManualMode@
             * ManualMode:[~003,Clamper,0@]
             */
            /**.21(相同狀態不回覆)
             * ~021,MustInManualMode@
             * ManualMode:[~003,Clamper,0@]
             */

            //LoadPort1.SetOriginalMethod(LoadPort1.CommandManualClamperUnlock);
            var commandText1 = LoadPort1.CommandManualClamperUnlock();
            NoteCommand(commandText1);
            var commandText2 = LoadPort2.CommandManualClamperUnlock();
            NoteCommand(commandText2);

            Repeat();
        }

        [TestMethod] //[V] .20  2020/06/24,   [V] .21  2020/06/24,  
        public void TestCommandManualClamperOPR()
        {  //~115,ManualClamperOPR@
            /** Note
             clamper lock 時才有作用, 執行完畢後會 clamper unlock, 觸發 clamper 事件
             */
           /** .20
            * [~021,MustInManualMode@]
            * ManualMode:~003,Clamper,0@
            */
            /** .21
             * [~021,MustInManualMode@]
             *  ManualMode:~003,Clamper,0@
           */
            // LoadPort1.SetOriginalMethod(LoadPort1.CommandManualClamperOPR);
            var commandText1 = LoadPort1.CommandManualClamperOPR();
            NoteCommand(commandText1);

            var commandText2=LoadPort2.CommandManualClamperOPR();
            NoteCommand(commandText2);

            Repeat();
        }

        [TestMethod] //[V] .20  2020/06/24,   [V] .21  2020/06/24,  
        public void TestCommandManualStageUp()
        {   //~116,ManualStageUp@
            /** Note
                1. 先將 clamper lock  才可執行
                2. 執行AskStagePosition , 待 回覆 StagePosition 事件後,若Stage 不是這個位置, 再這個命令
             */
            /** .20
             *  [~022,ClamperNotLock@]
             *  [~017,StagePosition,3@ | ~017,StagePosition,0@] 0:Stage於上升位置
             */
            /** .21
             *  [~023,PODNotPutProperly@]
             *  [~017,StagePosition,3@ | ~017,StagePosition,0@]0:Stage於上升位置
             */


            // LoadPort1.CommandAlarmReset();
            // LoadPort1.CommandInitialRequest();//[~018,LoadportStatus,2@],[ ~202,VacuumAbnormality@~018,LoadportStatus,3@]

            //LoadPort1.SetOriginalMethod(LoadPort1.CommandManualStageUp);
           var commandText1 = LoadPort1.CommandManualStageUp();
            NoteCommand(commandText1);
            var commandText2 = LoadPort2.CommandManualStageUp();
            NoteCommand(commandText2);
            Repeat();
        }

        [TestMethod]//[V] .20  2020/06/24,   [V] .21  2020/06/24,  
        public void TestCommandManualStageInspection()
        {  //~116,ManualStageUp@
           /** Note 
            * 先 Ask StatgePosition,待 回覆 StagePosition 事件後,若Stage 不是這個位置, 再下這個命令
            */
            /** .20
             * [~023,PODNotPutProperly@]
               [~017,StagePosition,3@ | ~017,StagePosition,1@] 1: Stage於光罩檢查位置
             */
            /** .21
             * [~022,ClamperNotLock@]
               [~017,StagePosition,3@ | ~017,StagePosition,1@] 1: Stage於光罩檢查位置
             **/


            //  LoadPort1.SetOriginalMethod(LoadPort1.CommandManualStageInspection);
            var commandText1 = LoadPort1.CommandManualStageInspection();
            NoteCommand(commandText1);

            var commandText2 = LoadPort2.CommandManualStageInspection();
            NoteCommand(commandText2);
            Repeat();

        }

        [TestMethod]//[V] .20  2020/06/24,   [V] .21  2020/06/24,  
        public void TestCommandManualStageDown()
        {   //~118,ManualStageDown@
            /** Note 
           * 先 AskStagePosition,待 回覆 StagePosition 事件後,若Stage 不是這個位置, 再下這個命令
           */
            /** .20
             * [~023,PODNotPutProperly@]
             * [~017,StagePosition,3@ |  ~017,StagePosition,2@],2: Stage於下降位置
             */
            /** .21
             * [~022,ClamperNotLock@]
             * [~017,StagePosition,3@ |  ~017,StagePosition,2@],2: Stage於下降位置
             */

            // LoadPort1.CommandAlarmReset();

            //  LoadPort1.CommandInitialRequest();

            // LoadPort1.SetOriginalMethod(LoadPort1.CommandManualStageDown);
            var commandText1 = LoadPort1.CommandManualStageDown();
            NoteCommand(commandText1);
           var commandText2 = LoadPort2.CommandManualStageDown();
          NoteCommand(commandText2);
            Repeat();

           
        }

        [TestMethod]//[V] .20  2020/06/24,   [V] .21  2020/06/24,  
        public void TestCommandManualStageOPR()
        {  // ~119,ManualStageOPR@
           /** .20
            * [~022,ClamperNotLock@]
            */
            /** .21
             *  ~023,PODNotPutProperly@
             * */


            //  LoadPort1.SetOriginalMethod(LoadPort1.CommandManualStageOPR);
            var commandText1 = LoadPort1.CommandManualStageOPR();
            NoteCommand(commandText1);
            var commandText2 = LoadPort2.CommandManualStageOPR();
            NoteCommand(commandText2);
            Repeat();
            // LoadPort2.CommandManualStageOPR();//POD Problem
        }

        [TestMethod]//[V] .20  2020/06/24,   [V] .21  2020/06/24,
        public void TestCommandManualVacuumOn()
        {    //~120,ManualVacuumOn@

            /** Note. 
             * 先 執行 AskVacuumStatus, 目前 Vacuum off 的才可以 Vacuumn On
             *        [Manual Mode]
             *        (1)20 有盒子, .21 沒有盒子
                       (2)AskVacuumStatus: .20 沒有真空,  .21 沒有真空
                       (3) ManualVacuumOn: ~120,ManualVacuumOn@=>  .20:[~007,VacuumComplete,1@], .21:没有回覆
                        (4)AskVacuumStatus: .20 有真空,  .21 沒有真空
                        (5)盒子調換後,AskVacuumStatus: .20無真空, .21 無真空
                        (6)盒子再調換後,AskVacuumStatus: .20有真空, .21 無真空
                 
             */


            /** .20
             * [~021,MustInManualMode@]
               */
            /**.21
             * [~021,MustInManualMode@]
             */

            // LoadPort1.SetOriginalMethod(LoadPort1.CommandManualVacuumOn);
            var commandText1 = LoadPort1.CommandManualVacuumOn(); 
            NoteCommand(commandText1);

            var commandText2 = LoadPort2.CommandManualVacuumOn();
            NoteCommand(commandText2);
            Repeat();
            //  LoadPort2.CommandManualVacuumOn();// ~007,VacuumComplete,1@
        }

        [TestMethod] //[V] .20  2020/06/24,   [V] .21  2020/06/24,
        public void TestCommandManualVacuumOff()
        {  //~121,ManualVacuumOff@

            /** Note.
            * 先 執行 AskVacuumStatus, 目前 Vacuum On 的才可以 Vacuumn off
            * Manual Mode:
                       (1)20 有盒子, .21 沒有盒子
                       (2)AskVacuumStatus: .20 有真空,  .21 沒有真空
                       (3) ManualVacuumOff(~121,ManualVacuumOff@)=> .20,    .21
             */

            /** .20
              * [~021,MustInManualMode@]
              *
              */
            /**.21
             * [~021,MustInManualMode@]
             * 
             */

            // LoadPort1.SetOriginalMethod(LoadPort1.CommandManualVacuumOff);
            var commandText1 = LoadPort1.CommandManualVacuumOff();
            NoteCommand(commandText1);
           var commandText2 = LoadPort2.CommandManualVacuumOff();
            NoteCommand(commandText2);
            Repeat();
            
        }
        #endregion


        #region old
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
        #endregion
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
            var eventArgs = (OnClamperLockCompleteEventArgs)args;
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
            
            NoteEventResult($"IP={loadport.ServerEndPoint.Address.ToString()}, DockPODStart");
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
            NoteEventResult($"IP={loadport.ServerEndPoint.Address.ToString()}, UndockComplete");
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
            NoteEventResult($"IP={loadport.ServerEndPoint.Address.ToString()}, ExecuteAlarmResetFirst");
        }
        private void OnStagePosition(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnStagePositionEventArgs)args;
            NoteEventResult($"IP={loadport.ServerEndPoint.Address.ToString()},  StagePosition", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
        }
        private void OnLoadportStatus(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;
            var eventArgs = (OnLoadportStatusEventArgs)args;
            NoteEventResult($"IP={loadport.ServerEndPoint.Address.ToString()}, LoadportStatus", eventArgs.ReturnCode.ToString() + "(" + (int)eventArgs.ReturnCode + ")");
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

            NoteEventResult($"IP={loadport.ServerEndPoint.Address.ToString()}, InitialUnComplete");
        }
        private void OnMustInAutoMode(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;

            NoteEventResult($"IP={loadport.ServerEndPoint.Address.ToString()}, MustInAutoMode");
        }

        private void OnMustInManualMode(object sender, EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;

            NoteEventResult($"IP={loadport.ServerEndPoint.Address.ToString()}, MustInManualMode");
        }

        private void OnClamperNotLock(object sender,EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;

            NoteEventResult($"IP={loadport.ServerEndPoint.Address.ToString()}, ClamperNotLock");
        }

        private void OnPODNotPutProperly(object sender,EventArgs args)
        {
            var loadport = (MvGudengLoadPortLdd)sender;

            NoteEventResult($"IP={loadport.ServerEndPoint.Address}, PODNotPutProperly"); 
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
#endregion
}
}
