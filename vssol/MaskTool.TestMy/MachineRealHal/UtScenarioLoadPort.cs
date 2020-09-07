using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.GudengLoadPort.LoadPortEventArgs;
using MvAssistant.DeviceDrive.GudengLoadPort.ReplyCode;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.CompLoadPort;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtScenarioLoadPort
    {
        void Repeat()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(10);
            }
        }
        #region Load port 

        #region CommandTest
        [TestMethod]
        public void LoadPortBasicTest()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var loportAssembly = halContext.HalDevices[MacEnumDevice.loadportA_assembly.ToString()] as MacHalLoadPort;

                    var testLoadPort = loportAssembly.Hals[MacEnumDevice.loadport_1.ToString()] as MacHalGudengLoadPort;
                    var connected = testLoadPort.HalConnect();
                    if (connected == 0)
                    {
                        throw new Exception($"無法連接到 {testLoadPort.DeviceIndex} ");
                    }
                    //   BindLoadPortEvent(loadport1);
                    BindLoadPortEvent(testLoadPort);
                  
                    Repeat();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        [TestMethod]
        public void LoadPortCommandTest()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var loportAssembly = halContext.HalDevices[MacEnumDevice.loadportA_assembly.ToString()] as MacHalLoadPort;

                    var testLoadPort = loportAssembly.Hals[MacEnumDevice.loadport_1.ToString()] as MacHalGudengLoadPort;
                    testLoadPort.HalConnect();
                    //   BindLoadPortEvent(loadport1);
                    BindLoadPortEvent(testLoadPort);
                    testLoadPort.CommandAlarmReset();
                    testLoadPort.CommandInitialRequest();
                    Repeat();

                }
            }
            catch (Exception ex)
            {

            }
        }
        #region Loadport Event Handler
        void BindLoadPortEvent(IMacHalLoadPortUnit loadport)
        {
            loadport.OnPlacementHandler += this.OnLoadPortPlacementHandler;
            loadport.OnPresentHandler += OnLoadPortPresentHandler;
            loadport.OnClamperHandler += this.OnLoadPortClamperHandler;
            loadport.OnRFIDHandler += this.OnLoadPortRFIDHandler;
            loadport.OnBarcode_IDHandler += this.OnLoadPortBarcode_IDHandler;
            loadport.OnClamperLockCompleteHandler += this.OnLoadPortClamperLockCompleteHandler;
            loadport.OnVacuumCompleteHandler += this.OnLoadPortVacuumCompleteHandler;
            loadport.OnDockPODStartHandler += this.OnLoadPortDockPODStartHandler;
            loadport.OnDockPODComplete_HasReticleHandler += this.OnLoadPortDockPODComplete_HasReticleHandler;
            loadport.OnDockPODComplete_EmptyHandler += this.OnLoadPortDockPODComplete_EmptyHandler;
            loadport.OnUndockCompleteHandler += this.OnLoadPortUndockCompleteHandler;
            loadport.OnClamperUnlockCompleteHandler += this.OnLoadPortClamperUnlockCompleteHandler;
            loadport.OnAlarmResetSuccessHandler += this.OnLoadPortAlarmResetSuccessHandler;
            loadport.OnAlarmResetFailHandler += this.OnLoadPortAlarmResetFailHandler;
            loadport.OnExecuteInitialFirstHandler += this.OnLoadPortExecuteInitialFirstHandler;
            loadport.OnExecuteAlarmResetFirstHandler += this.OnLoadPortExecuteAlarmResetFirstHandler;
            loadport.OnStagePositionHandler += this.OnLoadPortStagePositionHandler;
            loadport.OnLoadportStatusHandler += this.OnLoadportStatusHandler;
            loadport.OnInitialCompleteHandler += this.OnLoadPortInitialCompleteHandler;
            loadport.OnInitialUnCompleteHandler += this.OnLoadPortInitialUnCompleteHandler;
            loadport.OnMustInAutoModeHandler += this.OnLoadPortMustInAutoModeHandler;
            loadport.OnMustInManualModeHandler += this.OnLoadPortMustInManualModeHandler;
            loadport.OnClamperNotLockHandler += this.OnLoadPortClamperNotLockHandler;
            loadport.OnPODNotPutProperlyHandler += this.OnLoadPortPODNotPutProperlyHandler;
            loadport.OnClamperActionTimeOutHandler += this.OnLoadPortClamperActionTimeOutHandler;
            loadport.OnClamperUnlockPositionFailedHandler += this.OnLoadPortClamperUnlockPositionFailedHandler;
            loadport.OnVacuumAbnormalityHandler += this.OnLoadPortVacuumAbnormalityHandler;
            loadport.OnStageMotionTimeoutHandler += this.OnLoadPortStageMotionTimeoutHandler;
            loadport.OnStageOverUpLimitationHandler += this.OnLoadPortStageOverUpLimitationHandler;
            loadport.OnStageOverDownLimitationHandler += this.OnLoadPortStageOverDownLimitationHandler;
            loadport.OnReticlePositionAbnormalityHandler += this.OnLoadPortReticlePositionAbnormalityHandler;
            loadport.OnClamperLockPositionFailedHandler += this.OnLoadPortClamperLockPositionFailed;
            loadport.OnPODPresentAbnormalityHandler += this.OnLoadPortPODPresentAbnormalityHandler;
            loadport.OnClamperMotorAbnormalityHandler += this.OnLoadPortClamperMotorAbnormality;
            loadport.OnStageMotorAbnormalityHandler += this.OnLoadPortStageMotorAbnormality;

            loadport.OnHostLostLoadPortConnectionHandler += OnHostLostLoadPortConnection;
        }
        void OnHostLostLoadPortConnection(object sender,EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            //loadport
            loadport.HalConnect();
        }

        void OnLoadPortPlacementHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            var args = (OnPlacementEventArgs)e;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortPlacementHandler)}, ReturnCode={args.ReturnCode.ToString()}");
        }
        void OnLoadPortPresentHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            var args = (OnPresentEventArgs)e;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortPresentHandler)}, ReturnCode={args.ReturnCode.ToString()}");
        }
        void OnLoadPortClamperHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            var args = (OnClamperEventArgs)e;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortClamperHandler)}, ReturnCode={args.ReturnCode.ToString()}");
        }
        void OnLoadPortRFIDHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            var args = (OnRFIDEventArgs)e;
            if (!string.IsNullOrEmpty(args.RFID))
            {
                Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortRFIDHandler)}, ReturnCode={args.RFID.ToString()}");
            }
            else
            {
                Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortRFIDHandler)}, ReturnCode=No RFID");
            }
         }
        void OnLoadPortBarcode_IDHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            var args = (OnBarcode_IDEventArgs)e;
            if(args.ReturnCode== EventBarcodeIDCode.Success)
            {
                Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortBarcode_IDHandler)}, ReturnCode={args.BarcodeID}");
            }
            else
            {
                Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortBarcode_IDHandler)}, ReturnCode=No Barcode");
            }
            
        }
        void OnLoadPortClamperLockCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            var args = (OnClamperLockCompleteEventArgs)e;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortClamperLockCompleteHandler)}, ReturnCode={args.ReturnCode.ToString()}");
        }
        void OnLoadPortVacuumCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            var args = (OnVacuumCompleteEventArgs)e;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortVacuumCompleteHandler)}, ReturnCode={args.ReturnCode.ToString()}");
        }
        void OnLoadPortDockPODStartHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortDockPODStartHandler)}");
        }
        void OnLoadPortDockPODComplete_HasReticleHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortDockPODComplete_HasReticleHandler)}");
        }
        void OnLoadPortDockPODComplete_EmptyHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortDockPODComplete_EmptyHandler)}");
        }
        void OnLoadPortUndockCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortUndockCompleteHandler)}");
        }
        void OnLoadPortClamperUnlockCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortClamperUnlockCompleteHandler)}");
        }
        void OnLoadPortAlarmResetSuccessHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortAlarmResetSuccessHandler)}");
        }
        void OnLoadPortAlarmResetFailHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortAlarmResetFailHandler)}");
        }
        void OnLoadPortExecuteInitialFirstHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortExecuteInitialFirstHandler)}");
        }
        void OnLoadPortExecuteAlarmResetFirstHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortExecuteAlarmResetFirstHandler)}");
        }
        void OnLoadPortStagePositionHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            var args = (OnStagePositionEventArgs)e;
              Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortStagePositionHandler)}, ReturnCode={args.ReturnCode}");
        }
        void OnLoadportStatusHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            var args = (OnLoadportStatusEventArgs)e;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadportStatusHandler)}, ReturnCode={args.ReturnCode}");
        }
        void OnLoadPortInitialCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortInitialCompleteHandler)}");
        }
        void OnLoadPortInitialUnCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortInitialUnCompleteHandler)}");
        }
        void OnLoadPortMustInAutoModeHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortMustInAutoModeHandler)}");
        }
        void OnLoadPortMustInManualModeHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortMustInManualModeHandler)}");
        }
        void OnLoadPortClamperNotLockHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortClamperNotLockHandler)}");
        }
        void OnLoadPortPODNotPutProperlyHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortPODNotPutProperlyHandler)}");
        }
        void OnLoadPortClamperActionTimeOutHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortClamperActionTimeOutHandler)}");
        }
        void OnLoadPortClamperUnlockPositionFailedHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortClamperUnlockPositionFailedHandler)}");
        }
        void OnLoadPortVacuumAbnormalityHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortVacuumAbnormalityHandler)}");
        }
        void OnLoadPortStageMotionTimeoutHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortStageMotionTimeoutHandler)}");
        }
        void OnLoadPortStageOverUpLimitationHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortStageOverUpLimitationHandler)}");
        }
        void OnLoadPortStageOverDownLimitationHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortStageOverDownLimitationHandler)}");
        }
        void OnLoadPortReticlePositionAbnormalityHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortReticlePositionAbnormalityHandler)}");
        }
        void OnLoadPortClamperLockPositionFailed(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortClamperLockPositionFailed)}");
        }
        void OnLoadPortPODPresentAbnormalityHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortPODPresentAbnormalityHandler)}");
        }
        void OnLoadPortClamperMotorAbnormality(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortClamperMotorAbnormality)}");
        }
        void OnLoadPortStageMotorAbnormality(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortStageMotorAbnormality)}");
        }
        #endregion
        #endregion
        #endregion

        [TestMethod]
        public void LoadPortDock()
        {
            MacHalContext halContext = null;
            MacHalLoadPort loportAssembly = null;
            MacHalGudengLoadPort TestLoadport = null;
            try
            {
                halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
                halContext.MvCfLoad();
               
                loportAssembly = halContext.HalDevices[MacEnumDevice.loadportA_assembly.ToString()] as MacHalLoadPort;
                TestLoadport= loportAssembly.Hals[MacEnumDevice.loadport_1.ToString()] as MacHalGudengLoadPort;
                TestLoadport.HalConnect();
                BindLoadPortEvent(TestLoadport);
                TestLoadport.CommandDockRequest();
                Repeat();
            }
            catch(Exception ex)
            {

            }
            finally
            {
                if (halContext != null)
                {
                    halContext.Dispose();
                }
            }

        }

        [TestMethod]
        public void LoadPortUnDock()
        {
            MacHalContext halContext = null;
            MacHalLoadPort loportAssembly = null;
            MacHalGudengLoadPort TestLoadport = null;
            try
            {
                halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
                halContext.MvCfLoad();

                loportAssembly = halContext.HalDevices[MacEnumDevice.loadportA_assembly.ToString()] as MacHalLoadPort;
                TestLoadport = loportAssembly.Hals[MacEnumDevice.loadport_1.ToString()] as MacHalGudengLoadPort;
                TestLoadport.HalConnect();
                
                BindLoadPortEvent(TestLoadport);
                TestLoadport.CommandUndockRequest();
                Repeat();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (halContext != null)
                {
                    halContext.Dispose();
                }
            }

        }
    }
}
