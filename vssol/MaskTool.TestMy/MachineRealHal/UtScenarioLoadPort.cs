using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.GudengLoadPort.LoadPortEventArgs;
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
    public   class UtScenarioLoadPort
    {
        void Repeat()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }
        #region Load port 

        #region CommandTest
        [TestMethod]
        public void LoadPortCommandTest()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var loportAssembly = halContext.HalDevices[MacEnumDevice.loadport_assembly.ToString()] as MacHalLoadPort;
                   
                    var testLoadPort = loportAssembly.Hals[MacEnumDevice.loadport_2.ToString()] as MacHalGudengLoadPort;
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
        void BindLoadPortEvent(IMacHalLoadPortComp loadport)
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
        }


        void OnLoadPortPlacementHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
            var args = (OnPlacementEventArgs)e;
        }
        void OnLoadPortPresentHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
            var args = (OnPresentEventArgs)e;
        }
        void OnLoadPortClamperHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
            var args = (OnClamperEventArgs)e;
        }
        void OnLoadPortRFIDHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
            var args = (OnRFIDEventArgs)e;

        }
        void OnLoadPortBarcode_IDHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
            var args = (OnBarcode_IDEventArgs)e;
        }
        void OnLoadPortClamperLockCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
            var args = (OnClamperLockCompleteEventArgs)e;
        }
        void OnLoadPortVacuumCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
            var args = (OnVacuumCompleteEventArgs)e;
        }
        void OnLoadPortDockPODStartHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortDockPODComplete_HasReticleHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortDockPODComplete_EmptyHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortUndockCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortClamperUnlockCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortAlarmResetSuccessHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortInitialCompleteHandler)}");
        }
        void OnLoadPortAlarmResetFailHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortInitialCompleteHandler)}");
        }
        void OnLoadPortExecuteInitialFirstHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortExecuteAlarmResetFirstHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortStagePositionHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
            var args = (OnStagePositionEventArgs)e;
        }
        void OnLoadportStatusHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
            var args = (OnLoadportStatusEventArgs)e;
        }
        void OnLoadPortInitialCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
            Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortInitialCompleteHandler)}");
        }
        void OnLoadPortInitialUnCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortMustInAutoModeHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortMustInManualModeHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortClamperNotLockHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortPODNotPutProperlyHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortClamperActionTimeOutHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortClamperUnlockPositionFailedHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortVacuumAbnormalityHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortStageMotionTimeoutHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortStageOverUpLimitationHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortStageOverDownLimitationHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortReticlePositionAbnormalityHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortClamperLockPositionFailed(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortPODPresentAbnormalityHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortClamperMotorAbnormality(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
        }
        void OnLoadPortStageMotorAbnormality(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortComp)sender;
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
                halContext.MvCfLoad();
                halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
                loportAssembly = halContext.HalDevices[MacEnumDevice.loadport_assembly.ToString()] as MacHalLoadPort;
                TestLoadport= loportAssembly.Hals[MacEnumDevice.loadport_1.ToString()] as MacHalGudengLoadPort;
                BindLoadPortEvent(TestLoadport);
                TestLoadport.CommandDockRequest();
                Repeat();
            }
            catch(Exception ex)
            {

            }
            finally
            {

            }

        }
    }
}
