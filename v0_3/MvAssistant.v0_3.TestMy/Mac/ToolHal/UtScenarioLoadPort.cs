﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvaCToolkitCs.v1_2;
using MvAssistant.v0_3.DeviceDrive.GudengLoadPort.LoadPortEventArgs;
using MvAssistant.v0_3.DeviceDrive.GudengLoadPort.ReplyCode;
using MvAssistant.v0_3.Mac.Hal;
using MvAssistant.v0_3.Mac.Hal.Assembly;
using MvAssistant.v0_3.Mac.Hal.CompLoadPort;
using MvAssistant.v0_3.Mac.Manifest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.Mac.TestMy.ToolHal
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
                    halContext.MvaCfLoad();

                    var loportAssembly = halContext.HalDevices[EnumMacDeviceId.loadportA_assembly.ToString()] as MacHalLoadPort;

                    var testLoadPort = loportAssembly.Hals[EnumMacDeviceId.loadport_1.ToString()] as MacHalLoadPortGudeng;
                    var connected = testLoadPort.HalConnect();
                    if (connected == 0)
                    {
                        // vs 2013
                        //throw new Exception($"無法連接到 {testLoadPort.DeviceIndex} ");
                        throw new Exception("無法連接到 " + testLoadPort.DeviceId);
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
                    halContext.MvaCfLoad();

                    var loportAssembly = halContext.HalDevices[EnumMacDeviceId.loadportA_assembly.ToString()] as MacHalLoadPort;

                    var testLoadPort = loportAssembly.Hals[EnumMacDeviceId.loadport_1.ToString()] as MacHalLoadPortGudeng;
                    testLoadPort.HalConnect();
                    //   BindLoadPortEvent(loadport1);
                    BindLoadPortEvent(testLoadPort);
                    testLoadPort.CommandAlarmReset();
                    testLoadPort.CommandInitialRequest();
                    Repeat();

                }
            }
            catch (Exception ex) { CtkLog.WarnAn(this, ex); }
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
        void OnHostLostLoadPortConnection(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            //loadport
            loadport.HalConnect();
        }

        void OnLoadPortPlacementHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            var args = (OnPlacementEventArgs)e;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortPlacementHandler)}, ReturnCode={args.ReturnCode.ToString()}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortPlacementHandler, ReturnCode=" + args.ReturnCode.ToString());

        }
        void OnLoadPortPresentHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            var args = (OnPresentEventArgs)e;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortPresentHandler)}, ReturnCode={args.ReturnCode.ToString()}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortPresentHandler, ReturnCode=" + args.ReturnCode.ToString());
        }
        void OnLoadPortClamperHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            var args = (OnClamperEventArgs)e;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortClamperHandler)}, ReturnCode={args.ReturnCode.ToString()}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke: OnLoadPortClamperHandler, ReturnCode=" + args.ReturnCode.ToString());
        }
        void OnLoadPortRFIDHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            var args = (OnRFIDEventArgs)e;
            if (!string.IsNullOrEmpty(args.RFID))
            {
                // vs 2013
                //Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortRFIDHandler)}, ReturnCode={args.RFID.ToString()}");
                Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortRFIDHandler, ReturnCode=" + args.RFID.ToString());
            }
            else
            {
                // vs 2013
                // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortRFIDHandler)}, ReturnCode=No RFID");
                Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke: OnLoadPortRFIDHandler, ReturnCode=No RFID");
            }
        }
        void OnLoadPortBarcode_IDHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            var args = (OnBarcode_IDEventArgs)e;
            if (args.ReturnCode == EventBarcodeIDCode.Success)
            {
                // // vs 2013
                // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortBarcode_IDHandler)}, ReturnCode={args.BarcodeID}");
                Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortBarcode_IDHandler, ReturnCode=" + args.BarcodeID);
            }
            else
            {
                // vs 2013
                //Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortBarcode_IDHandler)}, ReturnCode=No Barcode");
                Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortBarcode_IDHandler, ReturnCode=No Barcode");
            }

        }
        void OnLoadPortClamperLockCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            var args = (OnClamperLockCompleteEventArgs)e;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortClamperLockCompleteHandler)}, ReturnCode={args.ReturnCode.ToString()}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortClamperLockCompleteHandler, ReturnCode=" + args.ReturnCode.ToString());
        }
        void OnLoadPortVacuumCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            var args = (OnVacuumCompleteEventArgs)e;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortVacuumCompleteHandler)}, ReturnCode={args.ReturnCode.ToString()}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortVacuumCompleteHandler, ReturnCode=" + args.ReturnCode.ToString());
        }
        void OnLoadPortDockPODStartHandler(object sender, EventArgs e)
        {

            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortDockPODStartHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortDockPODStartHandler");
        }
        void OnLoadPortDockPODComplete_HasReticleHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortDockPODComplete_HasReticleHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortDockPODComplete_HasReticleHandler");
        }
        void OnLoadPortDockPODComplete_EmptyHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortDockPODComplete_EmptyHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortDockPODComplete_EmptyHandler");
        }
        void OnLoadPortUndockCompleteHandler(object sender, EventArgs e)
        {

            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortUndockCompleteHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortUndockCompleteHandler");
        }
        void OnLoadPortClamperUnlockCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortClamperUnlockCompleteHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortClamperUnlockCompleteHandler");
        }
        void OnLoadPortAlarmResetSuccessHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            //Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortAlarmResetSuccessHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortAlarmResetSuccessHandler");
        }
        void OnLoadPortAlarmResetFailHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortAlarmResetFailHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortAlarmResetFailHandler");
        }
        void OnLoadPortExecuteInitialFirstHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            //Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortExecuteInitialFirstHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortExecuteInitialFirstHandler");
        }
        void OnLoadPortExecuteAlarmResetFirstHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortExecuteAlarmResetFirstHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortExecuteAlarmResetFirstHandler");
        }
        void OnLoadPortStagePositionHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            var args = (OnStagePositionEventArgs)e;
            // vs 2013
            //  Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortStagePositionHandler)}, ReturnCode={args.ReturnCode}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortStagePositionHandler, ReturnCode=" + args.ReturnCode);
        }
        void OnLoadportStatusHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            var args = (OnLoadportStatusEventArgs)e;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadportStatusHandler)}, ReturnCode={args.ReturnCode}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadportStatusHandler, ReturnCode=" + args.ReturnCode);
        }
        void OnLoadPortInitialCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            //Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortInitialCompleteHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortInitialCompleteHandler");
        }
        void OnLoadPortInitialUnCompleteHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortInitialUnCompleteHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortInitialUnCompleteHandler");
        }
        void OnLoadPortMustInAutoModeHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortMustInAutoModeHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortMustInAutoModeHandler");
        }
        void OnLoadPortMustInManualModeHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortMustInManualModeHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortMustInManualModeHandler");
        }
        void OnLoadPortClamperNotLockHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortClamperNotLockHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortClamperNotLockHandler");
        }
        void OnLoadPortPODNotPutProperlyHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortPODNotPutProperlyHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortPODNotPutProperlyHandler");
        }
        void OnLoadPortClamperActionTimeOutHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortClamperActionTimeOutHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortClamperActionTimeOutHandler");
        }
        void OnLoadPortClamperUnlockPositionFailedHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortClamperUnlockPositionFailedHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortClamperUnlockPositionFailedHandler");
        }
        void OnLoadPortVacuumAbnormalityHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortVacuumAbnormalityHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortVacuumAbnormalityHandler");
        }
        void OnLoadPortStageMotionTimeoutHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            //  // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortStageMotionTimeoutHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortStageMotionTimeoutHandler");
        }
        void OnLoadPortStageOverUpLimitationHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortStageOverUpLimitationHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortStageOverUpLimitationHandler");
        }
        void OnLoadPortStageOverDownLimitationHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortStageOverDownLimitationHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortStageOverDownLimitationHandler");
        }
        void OnLoadPortReticlePositionAbnormalityHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortReticlePositionAbnormalityHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortReticlePositionAbnormalityHandler");

        }
        void OnLoadPortClamperLockPositionFailed(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortClamperLockPositionFailed)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortClamperLockPositionFailed");
        }
        void OnLoadPortPODPresentAbnormalityHandler(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortPODPresentAbnormalityHandler)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + " , Invoke:OnLoadPortPODPresentAbnormalityHandler");
        }
        void OnLoadPortClamperMotorAbnormality(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            //Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortClamperMotorAbnormality)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortClamperMotorAbnormality");
        }
        void OnLoadPortStageMotorAbnormality(object sender, EventArgs e)
        {
            var loadport = (IMacHalLoadPortUnit)sender;
            // vs 2013
            // Debug.WriteLine($"Index={loadport.DeviceIndex}, Invoke:{nameof(OnLoadPortStageMotorAbnormality)}");
            Debug.WriteLine("Index=" + loadport.DeviceId + ", Invoke:OnLoadPortStageMotorAbnormality");
        }
        #endregion
        #endregion
        #endregion

        [TestMethod]
        public void LoadportAlarmReset()
        {
            MacHalContext halContext = null;
            MacHalLoadPort loportAssembly = null;
            MacHalLoadPortGudeng TestLoadport = null;
            try
            {
                halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
                halContext.MvaCfLoad();

                loportAssembly = halContext.HalDevices[EnumMacDeviceId.loadportA_assembly.ToString()] as MacHalLoadPort;
                TestLoadport = loportAssembly.Hals[EnumMacDeviceId.loadport_1.ToString()] as MacHalLoadPortGudeng;
                TestLoadport.HalConnect();
                BindLoadPortEvent(TestLoadport);
                TestLoadport.CommandAlarmReset();
                Repeat();
            }
            catch (Exception ex) { CtkLog.WarnAn(this, ex); }
            finally
            {
                if (halContext != null)
                {
                    halContext.Dispose();
                }
            }
        }


        [TestMethod]
        public void LoadportInitial()
        {
            MacHalContext halContext = null;
            MacHalLoadPort loportAssembly = null;
            MacHalLoadPortGudeng TestLoadport = null;
            try
            {
                halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
                halContext.MvaCfLoad();

                loportAssembly = halContext.HalDevices[EnumMacDeviceId.loadportA_assembly.ToString()] as MacHalLoadPort;
                TestLoadport = loportAssembly.Hals[EnumMacDeviceId.loadport_1.ToString()] as MacHalLoadPortGudeng;
                TestLoadport.HalConnect();
                BindLoadPortEvent(TestLoadport);
                TestLoadport.CommandInitialRequest();
                Repeat();
            }
            catch (Exception ex) { CtkLog.WarnAn(this, ex); }
            finally
            {
                if (halContext != null)
                {
                    halContext.Dispose();
                }
            }
        }

        [TestMethod]
        public void LoadPortDock()
        {
            MacHalContext halContext = null;
            MacHalLoadPort loportAssembly = null;
            MacHalLoadPortGudeng TestLoadport = null;
            try
            {
                halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
                halContext.MvaCfLoad();

                loportAssembly = halContext.HalDevices[EnumMacDeviceId.loadportA_assembly.ToString()] as MacHalLoadPort;
                TestLoadport = loportAssembly.Hals[EnumMacDeviceId.loadport_1.ToString()] as MacHalLoadPortGudeng;
                TestLoadport.HalConnect();
                BindLoadPortEvent(TestLoadport);
                TestLoadport.CommandDockRequest();
                Repeat();
            }
            catch (Exception ex) { CtkLog.WarnAn(this, ex); }
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
            MacHalLoadPortGudeng TestLoadport = null;
            try
            {
                halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
                halContext.MvaCfLoad();

                loportAssembly = halContext.HalDevices[EnumMacDeviceId.loadportA_assembly.ToString()] as MacHalLoadPort;
                TestLoadport = loportAssembly.Hals[EnumMacDeviceId.loadport_1.ToString()] as MacHalLoadPortGudeng;
                TestLoadport.HalConnect();

                BindLoadPortEvent(TestLoadport);
                TestLoadport.CommandUndockRequest();
                Repeat();
            }
            catch (Exception ex) { CtkLog.WarnAn(this, ex); }
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
