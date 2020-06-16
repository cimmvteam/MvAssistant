using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.GudengLoadPort;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalLoadPort
    {

        LoadPort LoadPort1 = null;
        LoadPort LoadPort2 = null;
        MvGudengLoadPortLdd ldd = new MvGudengLoadPortLdd();
      public UtHalLoadPort()
      {
         
            LoadPort1 = ldd.CreateLoadPort("192.168.0.20", 1024, 1);
            LoadPort2 = ldd.CreateLoadPort("192.168.0.21", 1024, 2);
            LoadPort1.StartListenServerThread();
            LoadPort2.StartListenServerThread();
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
            EventHandler InitialCompleteHandler = (sender, e)=>{  };
            EventHandler InitialUnCompleteHandler = (sender, e) =>{ };

            LoadPort1.ResetInitialOnCompleteHandler();
            LoadPort1.ResetOnInitialUnCompleteHandler();
            LoadPort1.OnInitialCompleteHandler += InitialCompleteHandler;
            LoadPort1.OnInitialUnCompleteHandler += InitialUnCompleteHandler;

            LoadPort1.CommandInitialRequest();
        }

        /// <summary>(100)</summary>
        [TestMethod]
        public void TestCommandDockRequest()
        {
            /**
           LoadPort1.CommandAlarmReset();  // ~013,AlarmResetSuccess@
           LoadPort1.CommandInitialRequest();//[~018,LoadportStatus,2@],[~202,VacuumAbnormality@~018,LoadportStatus,3@]
                                              // LoadPort1.CommandAlarmReset();

            // ~100,DockRequest@
            LoadPort1.CommandDockRequest();// ~015,ExecuteInitialFirst@  有沒有(沒有實際的 POD 無法測)
            */
            EventHandler ExecuteAlarmResetFirstHandler = (sender, e)=> {      };
            EventHandler AlarmResetSuccessHandler = (sender, e) =>{  };
            EventHandler AlarmResetFailHandler = (sender, e) =>{ };
            EventHandler ExecuteInitialFirstHandler = (sender, e)=>{};
            EventHandler InitialCompleteHandler = (sender, e) =>{};
            EventHandler InitialUnCompleteHandler = (sender, e) => { };
            EventHandler DockPODStartHandler = (sender, e) =>{};

            LoadPort1.ResetExecuteOnAlarmResetFirstHandler();
            LoadPort1.OnExecuteAlarmResetFirstHandler += ExecuteAlarmResetFirstHandler;
            LoadPort1.ResetOnAlarmResetSuccessHandler();
            LoadPort1.OnAlarmResetSuccessHandler += AlarmResetSuccessHandler;
            LoadPort1.ResetOnAlarmResetFailHandler();
            LoadPort1.OnAlarmResetFailHandler += AlarmResetFailHandler;
            LoadPort1.ResetOnExecuteInitialFirstHandler();
            LoadPort1.OnExecuteInitialFirstHandler += ExecuteInitialFirstHandler;
            LoadPort1.ResetInitialOnCompleteHandler();
            LoadPort1.OnExecuteInitialFirstHandler += ExecuteInitialFirstHandler;
            LoadPort1.ResetOnInitialUnCompleteHandler();
            LoadPort1.OnInitialUnCompleteHandler += InitialUnCompleteHandler;
            LoadPort1.ResetInitialOnCompleteHandler();
            LoadPort1.OnInitialCompleteHandler += InitialCompleteHandler;
            LoadPort1.ResetDockOnPODStartHandler();
            LoadPort1.OnDockPODStartHandler += DockPODStartHandler;

            LoadPort1.CommandDockRequest();
        }

        /// <summary>(101)</summary>
        [TestMethod]
        public void TestCommandUndockRequest()
        {
            /**
            LoadPort1.CommandAlarmReset();//~013,AlarmResetSuccess@
            LoadPort1.CommandInitialRequest();
            //~101,UndockRequest@
            LoadPort1.CommandUndockRequest(); //~020,MustInAutoMode@(缺)(若是和前面指令一起連續執行則沒有回覆)
            */
            EventHandler ExecuteAlarmResetFirstHandler = (sender, e) => { };
            EventHandler AlarmResetSuccessHandler = (sender, e) => { };
            EventHandler AlarmResetFailHandler = (sender, e) => { };
            EventHandler ExecuteInitialFirstHandler = (sender, e) => { };
            EventHandler InitialCompleteHandler = (sender, e) => { };
            EventHandler InitialUnCompleteHandler = (sender, e) => { };

            LoadPort1.ResetExecuteOnAlarmResetFirstHandler();
            LoadPort1.OnExecuteAlarmResetFirstHandler += ExecuteAlarmResetFirstHandler;
            LoadPort1.ResetOnAlarmResetSuccessHandler();
            LoadPort1.OnAlarmResetSuccessHandler += AlarmResetSuccessHandler;
            LoadPort1.ResetOnAlarmResetFailHandler();
            LoadPort1.OnAlarmResetFailHandler += AlarmResetFailHandler;
            LoadPort1.ResetOnExecuteInitialFirstHandler();
            LoadPort1.OnExecuteInitialFirstHandler += ExecuteInitialFirstHandler;
            LoadPort1.ResetInitialOnCompleteHandler();
            LoadPort1.OnExecuteInitialFirstHandler += ExecuteInitialFirstHandler;
            LoadPort1.ResetOnInitialUnCompleteHandler();
            LoadPort1.OnInitialUnCompleteHandler += InitialUnCompleteHandler;
            LoadPort1.ResetInitialOnCompleteHandler();
            LoadPort1.OnInitialCompleteHandler += InitialCompleteHandler;

            LoadPort1.CommandUndockRequest();
        }

       

        [TestMethod]
        public void TestCommandAskPlacementStatus()
        {
            //  ~102,AskPlacementStatus@
           LoadPort1.CommandAskPlacementStatus();//~001,Placement,1@
          //  LoadPort2.CommandAskPlacementStatus();//~001,Placement,0@
        }

        [TestMethod]
        public void TestCommandAskPresentStatus()
        {
           // ~103,AskPresentStatus@
            LoadPort1.CommandAskPresentStatus();//~002,Present,1@
           // LoadPort1.CommandAskPresentStatus();//~002,Present,0@
        }

        [TestMethod]
        public void TestCommandAskClamperStatus()
        {
            //~104,AskClamperStatus@
            LoadPort1.CommandAskClamperStatus();//[~003,Clamper,0@],[~006,ClamperLockComplete,0@]
           // LoadPort2.CommandAskClamperStatus();//[~003,Clamper,2@],[~006,ClamperLockComplete,1@]
        }

        [TestMethod]
        public void TestCommandAskRFIDStatus()
        {
            // ~105,AskRFIDStatus@
            LoadPort1.CommandAskRFIDStatus();//~004,RFID,1343833305251403@
           // LoadPort2.CommandAskRFIDStatus();// ~004,RFID,ERROR@
        }

        [TestMethod]
        public void TestCommandAskBarcodeStatus()
        {
            //~106,AskBarcodeStatus@
            LoadPort1.CommandAskBarcodeStatus();//~005,Barcode ID,0@
         //   LoadPort2.CommandAskBarcodeStatus();//没有回覆
        }

        [TestMethod]
        public void TestCommandAskVacuumStatus()
        {
            //~107,AskVacuumStatus@
            LoadPort1.CommandAskVacuumStatus();//~007,VacuumComplete,0@
          //  LoadPort2.CommandAskVacuumStatus();//~007,VacuumComplete,0@
        }

        [TestMethod]
        public void TestCommandAskReticleExistStatus()
        {
            //~108,AskReticleExistStatus@
            LoadPort1.CommandAskReticleExistStatus();//~010,DockPODComplete_Empty@
         //   LoadPort2.CommandAskReticleExistStatus();//~010,DockPODComplete_Empty@
        }

        [TestMethod]

        public void TestCommandAlarmReset()
        {
            /**
             //~109,AlarmReset@
             LoadPort1.CommandAlarmReset();//~013,AlarmResetSuccess@
             LoadPort2.CommandAlarmReset();//~013,AlarmResetSuccess@
            */
        
            LoadPort1.CommandAlarmReset();
        }

        [TestMethod]
        public void TestCommandAskStagePosition()
        {

            /**
             //~110,AskStagePosition@
             //LoadPort2.CommandAskStagePosition();// ~017,StagePosition,0@
             */

         
            // LoadPort1.ReleaseCommandCasadeMode(); ??
            LoadPort1.CommandAskStagePosition();// ~017,StagePosition,3@
        }

        [TestMethod]
        public void TestCommandAskLoadportStatus()
        {
            /**
            // ~111,AskLoadportStatus@
            LoadPort2.CommandAskLoadportStatus();//~018,LoadportStatus,1@
            */

        
            // LoadPort1.ReleaseCommandCasadeMode(); ??
            LoadPort1.CommandAskLoadportStatus();//~018,LoadportStatus,4
          
        }

        [TestMethod]
        public void TestCommandManualClamperLock()
        {
            /**
              //LoadPort2.CommandInitialRequest();
              //~113,ManualClamperLock@
              //LoadPort2.CommandManualClamperLock();//POD Problem
              */

    
            // LoadPort1.ReleaseCommandCasadeMode(); ??
            LoadPort1.CommandManualClamperLock();//~003,Clamper,1@(第二次執行時沒有回覆)
           
        }

        [TestMethod]
        public void TestCommandManualClamperUnlock()
        {
            //~114,ManualClamperUnlock@
            LoadPort1.CommandManualClamperUnlock();//~003,Clamper,0@
            //LoadPort2.CommandInitialRequest();
            LoadPort2.CommandManualClamperUnlock();//POD Problem
        }

        [TestMethod]
        public void TestCommandManualClamperOPR()
        {
            //~115,ManualClamperOPR@
            LoadPort1.CommandManualClamperOPR();// [RETURN] ~003,Clamper,0@
            LoadPort2.CommandManualClamperOPR();// [RETURN] ~003,Clamper,0@
        }

        [TestMethod]
        public void TestCommandManualStageUp()
        {
            // LoadPort1.CommandAlarmReset();
            // LoadPort1.CommandInitialRequest();//[~018,LoadportStatus,2@],[ ~202,VacuumAbnormality@~018,LoadportStatus,3@]
            //~116,ManualStageUp@
             LoadPort1.CommandManualStageUp();//  ~022,ClamperNotLock@
           // LoadPort2.CommandInitialRequest();
            LoadPort2.CommandManualStageUp();// POD Problem
        }

        [TestMethod]
        public void TestCommandManualStageInspection()
        {
            //  LoadPort1.CommandAlarmReset();
            //    LoadPort1.CommandInitialRequest();
            //~117,ManualStageInspection@
            LoadPort1.CommandManualStageInspection();// ~022,ClamperNotLock@
            //LoadPort2.CommandInitialRequest();
            LoadPort2.CommandManualStageInspection(); //POD Problem

        }

        [TestMethod]
        public void TestCommandManualStageDown()
        {

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
            //~118,ManualStageDown@
             LoadPort1.CommandManualStageDown();// ~022,ClamperNotLock@


            //  LoadPort2.CommandAlarmReset();
            //                                      LoadPort2.CommandInitialRequest();
           LoadPort2.CommandManualStageDown();// POD Problem
        }

        [TestMethod]
        public void TestCommandManualStageOPR()
        {
           // ~119,ManualStageOPR@
            LoadPort1.CommandManualStageOPR();//~022,ClamperNotLock@
            LoadPort2.CommandManualStageOPR();//POD Problem
        }

        [TestMethod]
        public void TestCommandManualVacuumOn()
        {
            //~120,ManualVacuumOn@
           // LoadPort1.CommandManualVacuumOn(); // ~007,VacuumComplete,1@
            LoadPort2.CommandManualVacuumOn();// ~007,VacuumComplete,1@
        }

        [TestMethod]
        public void TestCommandManualVacuumOff()
        {
            //~121,ManualVacuumOff@
            //LoadPort1.CommandManualVacuumOff();// ~007,VacuumComplete,0@
            LoadPort2.CommandManualVacuumOff();// ~007,VacuumComplete,0@
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
    }
}
