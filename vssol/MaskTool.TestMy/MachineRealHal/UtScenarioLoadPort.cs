using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.GudengLoadPort.LoadPortEventArgs;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.CompLoadPort;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public   class UtScenarioLoadPort
    {
        #region Load port 

        [TestMethod]
        public void TestLoadPort()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();

                    var loportAssembly = halContext.HalDevices[MacEnumDevice.loadport_assembly.ToString()] as MacHalLoadPort;
                    //var loadport1 = cabinet.Hals[MacEnumDevice.loadport_1.ToString()] as MacHalGudengLoadPort;
                    var loadport2 = loportAssembly.Hals[MacEnumDevice.loadport_2.ToString()] as MacHalGudengLoadPort;
                    //loadport1.HalConnect();
                    //loadport2.HalConnect();
                    //   BindLoadPortEvent(loadport1);
                    BindLoadPortEvent(loadport2);
                    // loadport1.CommandAlarmReset();
                    loadport2.HalConnect();
                    loadport2.CommandAlarmReset();
                    //Repeat();

                }
            }
            catch (Exception ex)
            {

            }
        }
        #region Loadport Event Handler
        void BindLoadPortEvent<T>(T loadport) where T : IMacHalLoadPortComp
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
            var loadport = (MacHalGudengLoadPort)sender;
            var args = (OnBarcode_IDEventArgs)e;
        }
        void OnLoadPortClamperLockCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
            var args = (OnClamperLockCompleteEventArgs)e;
        }
        void OnLoadPortVacuumCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
            var args = (OnVacuumCompleteEventArgs)e;
        }
        void OnLoadPortDockPODStartHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortDockPODComplete_HasReticleHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortDockPODComplete_EmptyHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortUndockCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortClamperUnlockCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortAlarmResetSuccessHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortAlarmResetFailHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortExecuteInitialFirstHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortExecuteAlarmResetFirstHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortStagePositionHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
            var args = (OnStagePositionEventArgs)e;
        }
        void OnLoadportStatusHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
            var args = (OnLoadportStatusEventArgs)e;
        }
        void OnLoadPortInitialCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortInitialUnCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortMustInAutoModeHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortMustInManualModeHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortClamperNotLockHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortPODNotPutProperlyHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortClamperActionTimeOutHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortClamperUnlockPositionFailedHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortVacuumAbnormalityHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortStageMotionTimeoutHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortStageOverUpLimitationHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortStageOverDownLimitationHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortReticlePositionAbnormalityHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortClamperLockPositionFailed(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortPODPresentAbnormalityHandler(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortClamperMotorAbnormality(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        void OnLoadPortStageMotorAbnormality(object sender, EventArgs e)
        {
            var loadport = (MacHalGudengLoadPort)sender;
        }
        #endregion
        #endregion
    }
}
