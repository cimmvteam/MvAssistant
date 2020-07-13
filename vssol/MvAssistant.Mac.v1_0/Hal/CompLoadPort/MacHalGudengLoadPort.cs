using MvAssistant.DeviceDrive;
using MvAssistant.DeviceDrive.GudengLoadPort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompLoadPort
{
    [Guid("F02B078D-30B7-44CF-9D9C-DAC2FE9A26C6")]
    public class MacHalGudengLoadPort : MacHalComponentBase, IMacHalLoadPortComp
    {
        private static object getLddObject = new object();
        private MvGudengLoadPortLdd _ldd;

        public event EventHandler OnPlacementHandler;
        public event EventHandler OnPresentHandler;
        public event EventHandler OnClamperHandler;
        public event EventHandler OnRFIDHandler;
        public event EventHandler OnBarcode_IDHandler;
        public event EventHandler OnClamperLockCompleteHandler;
        public event EventHandler OnVacuumCompleteHandler;
        public event EventHandler OnDockPODStartHandler;
        public event EventHandler OnDockPODComplete_HasReticleHandler;
        public event EventHandler OnDockPODComplete_EmptyHandler;
        public event EventHandler OnUndockCompleteHandler;
        public event EventHandler OnClamperUnlockCompleteHandler;
        public event EventHandler OnAlarmResetSuccessHandler;
        public event EventHandler OnAlarmResetFailHandler;
        public event EventHandler OnExecuteInitialFirstHandler;
        public event EventHandler OnExecuteAlarmResetFirstHandler;
        public event EventHandler OnStagePositionHandler;
        public event EventHandler OnLoadportStatusHandler;
        public event EventHandler OnInitialCompleteHandler;
        public event EventHandler OnInitialUnCompleteHandler;
        public event EventHandler OnMustInAutoModeHandler;
        public event EventHandler OnMustInManualModeHandler;
        public event EventHandler OnClamperNotLockHandler;
        public event EventHandler OnPODNotPutProperlyHandler;
        public event EventHandler OnClamperActionTimeOutHandler;
        public event EventHandler OnClamperUnlockPositionFailedHandler;
        public event EventHandler OnVacuumAbnormalityHandler;
        public event EventHandler OnStageMotionTimeoutHandler;
        public event EventHandler OnStageOverUpLimitationHandler;
        public event EventHandler OnStageOverDownLimitationHandler;
        public event EventHandler OnReticlePositionAbnormalityHandler;
        public event EventHandler OnClamperLockPositionFailedHandler;
        public event EventHandler OnPODPresentAbnormalityHandler;
        public event EventHandler OnClamperMotorAbnormalityHandler;
        public event EventHandler OnStageMotorAbnormalityHandler;

        public string DeviceIP
        {
            get
            {
                var ip = this.DevSettings["ip"];
                return ip;
            }
        }

        public int DevicePort
        {
            get
            {
                var port = Convert.ToInt32(this.DevSettings["port"]);
                return port;
            }
        }
        public string DeviceIndex { get { return HalDeviceCfg.DeviceName; } }

        public bool IsConnected {
            get
            {
                if (_ldd == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }

        public override int HalClose()
        {
            try
            {

                _ldd = null;
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public override int HalConnect()
        {
            try
            {
                var connected = false;
                if (_ldd == null)
                {
                    lock (getLddObject)
                    {
                        if (_ldd == null)
                        {
                            _ldd = new MvGudengLoadPortLdd(DeviceIP, DevicePort, DeviceIndex);
                            this.BindEvents();
                            connected = _ldd.StartListenServerThread();

                            if (!connected)
                            {
                                _ldd = null;
                            }
                        }
                    }
                }
                return connected ? 1 : 0;
            }
            catch (Exception ex)
            {
                _ldd = null;
                return 0;
            }
        }

        /// <summary></summary>
        private void BindEvents()
        {
            _ldd.OnPlacementHandler += this.OnPlacement;
            _ldd.OnPresentHandler += this.OnPresent;
            _ldd.OnClamperHandler += this.OnClamper;
            _ldd.OnBarcode_IDHandler += this.OnBarcode_ID;
            _ldd.OnClamperLockCompleteHandler += this.OnClamperLockComplete;
            _ldd.OnVacuumCompleteHandler += this.OnVacuumComplete;
            _ldd.OnDockPODStartHandler += this.OnDockPODStart;
            _ldd.OnDockPODComplete_HasReticleHandler += this.OnDockPODComplete_HasReticle;
            _ldd.OnDockPODComplete_EmptyHandler += this.OnDockPODComplete_Empty;
            _ldd.OnUndockCompleteHandler += this.OnUndockComplete;
            _ldd.OnClamperUnlockCompleteHandler += this.OnClamperUnlockComplete;
            _ldd.OnAlarmResetSuccessHandler += this.OnAlarmResetSuccess;
            _ldd.OnAlarmResetFailHandler += this.OnAlarmResetFail;
            _ldd.OnExecuteInitialFirstHandler += this.OnExecuteInitialFirst;
            _ldd.OnExecuteAlarmResetFirstHandler += this.OnExecuteAlarmResetFirst;
            _ldd.OnStagePositionHandler += this.OnStagePosition;
            _ldd.OnLoadportStatusHandler += this.OnLoadportStatus;
            _ldd.OnInitialCompleteHandler += this.OnInitialComplete;
            _ldd.OnInitialUnCompleteHandler += this.OnInitialUnComplete;
            _ldd.OnMustInAutoModeHandler += this.OnMustInAutoMode;
            _ldd.OnMustInManualModeHandler += this.OnMustInManualMode;
            _ldd.OnClamperNotLockHandler += this.OnClamperNotLock;
            _ldd.OnPODNotPutProperlyHandler += this.OnPODNotPutProperly;
            _ldd.OnClamperActionTimeOutHandler += this.OnClamperActionTimeOut;
            _ldd.OnClamperUnlockPositionFailedHandler += this.OnClamperUnlockPositionFailed;
            _ldd.OnVacuumAbnormalityHandler += this.OnVacuumAbnormality;
            _ldd.OnStageMotionTimeoutHandler += this.OnStageMotionTimeout;
            _ldd.OnStageOverUpLimitationHandler += this.OnStageOverUpLimitation;
            _ldd.OnStageOverDownLimitationHandler += this.OnStageOverDownLimitation;
            _ldd.OnReticlePositionAbnormalityHandler += this.OnReticlePositionAbnormality;
            _ldd.OnClamperLockPositionFailedHandler += this.OnClamperLockPositionFailed;
            _ldd.OnPODPresentAbnormalityHandler += this.OnPODPresentAbnormality;
            _ldd.OnClamperMotorAbnormalityHandler += this.OnClamperMotorAbnormality;
            _ldd.OnStageMotorAbnormalityHandler += this.OnStageMotorAbnormality;
        }

        public string CommandAlarmReset()
        {
            var commandText = _ldd.CommandAlarmReset();
            return commandText;
        }

        public string CommandDockRequest()
        {
            var commandText = _ldd.CommandDockRequest();
            return commandText;
        }

        public string CommandUndockRequest()
        {
            var commandText = _ldd.CommandUndockRequest();
            return commandText;
        }

        public string CommandAskPlacementStatus()
        {
            var commandText = _ldd.CommandAskPlacementStatus();
            return commandText;
        }

        public string CommandAskPresentStatus()
        {
            var commandText = _ldd.CommandAskPresentStatus();
            return commandText;
        }

        public string CommandAskClamperStatus()
        {
            var commandText = _ldd.CommandAskClamperStatus();
            return commandText;
        }

        public string CommandAskRFIDStatus()
        {
            var commandText = _ldd.CommandAskRFIDStatus();
            return commandText;
        }

        public string CommandAskBarcodeStatus()
        {
            var commandText = _ldd.CommandAskBarcodeStatus();
            return commandText;
        }

        public string CommandAskVacuumStatus()
        {
            var commandText = _ldd.CommandAskVacuumStatus();
            return commandText;
        }

        public string CommandAskReticleExistStatus()
        {
            var commandText = _ldd.CommandAskReticleExistStatus();
            return commandText;
        }

        public string CommandAskStagePosition()
        {
            var commandText = _ldd.CommandAskStagePosition();
            return commandText;
        }

        public string CommandAskLoadportStatus()
        {
            var commandText = _ldd.CommandAskLoadportStatus();
            return commandText;
        }

        public string CommandInitialRequest()
        {
            var commandText = _ldd.CommandInitialRequest();
            return commandText;
        }

        public string CommandManualClamperLock()
        {
            var commandText = _ldd.CommandManualClamperLock();
            return commandText;
        }

        public string CommandManualClamperUnlock()
        {
            var commandText = _ldd.CommandAlarmReset();
            return commandText;
        }

        public string CommandManualClamperOPR()
        {
            var commandText = _ldd.CommandManualClamperUnlock();
            return commandText;
        }

        public string CommandManualStageUp()
        {
            var commandText = _ldd.CommandManualStageUp();
            return commandText;
        }

        public string CommandManualStageInspection()
        {
            var commandText = _ldd.CommandManualStageInspection();
            return commandText;
        }

        public string CommandManualStageDown()
        {
            var commandText = _ldd.CommandManualStageDown();
            return commandText;
        }

        public string CommandManualStageOPR()
        {
            var commandText = _ldd.CommandManualStageOPR();
            return commandText;
        }

        public string CommandManualVacuumOn()
        {
            var commandText = _ldd.CommandManualVacuumOn();
            return commandText;
        }

        public string CommandManualVacuumOff()
        {
            var commandText = _ldd.CommandManualVacuumOff();
            return commandText;
        }

       

        public void OnPlacement(object sender, EventArgs e)
        {
            if (OnPlacementHandler != null)
            {
                OnPlacementHandler.Invoke(this,e);
                
            }
        }
        public void OnPresent(object sender, EventArgs e)
        {
            if (OnPresentHandler != null)
            {
                OnPresentHandler.Invoke(this, e);

            }
        }
        public void OnClamper(object sender, EventArgs e)
        {
            if (OnClamperHandler != null)
            {
                OnClamperHandler.Invoke(this, e);

            }
        }
        public void OnRFID(object sender, EventArgs e)
        {
            if (OnRFIDHandler != null)
            {
                OnRFIDHandler.Invoke(this, e);

            }
        }
        public void OnBarcode_ID(object sender, EventArgs e)
        {
            if (OnBarcode_IDHandler != null)
            {
                OnBarcode_IDHandler.Invoke(this, e);

            }
        }
        public void OnClamperLockComplete(object sender, EventArgs e)
        {
            if (OnClamperLockCompleteHandler != null)
            {
                OnClamperLockCompleteHandler.Invoke(this, e);

            }
        }
        public void OnVacuumComplete(object sender, EventArgs e)
        {
            if (OnVacuumCompleteHandler != null)
            {
                OnVacuumCompleteHandler.Invoke(this, e);

            }
        }
        public void OnDockPODStart(object sender, EventArgs e)
        {
            if (OnDockPODStartHandler != null)
            {
                OnDockPODStartHandler.Invoke(this, e);

            }
        }
        public void OnDockPODComplete_HasReticle(object sender, EventArgs e)
        {
            if (OnDockPODComplete_HasReticleHandler != null)
            {
                OnDockPODComplete_HasReticleHandler.Invoke(this, e);

            }
        }
        public void OnDockPODComplete_Empty(object sender, EventArgs e)
        {
            if (OnDockPODComplete_EmptyHandler != null)
            {
                OnDockPODComplete_EmptyHandler.Invoke(this, e);

            }
        }
        public void OnUndockComplete(object sender, EventArgs e)
        {
            if (OnUndockCompleteHandler != null)
            {
                OnUndockCompleteHandler.Invoke(this, e);

            }
        }
        public void OnClamperUnlockComplete(object sender, EventArgs e)
        {
            if (OnClamperUnlockCompleteHandler != null)
            {
                OnClamperUnlockCompleteHandler.Invoke(this, e);

            }
        }
        public void OnAlarmResetSuccess(object sender, EventArgs e)
        {
            if (OnAlarmResetSuccessHandler != null)
            {
                OnAlarmResetSuccessHandler.Invoke(this, e);

            }
        }
        public void OnAlarmResetFail(object sender, EventArgs e)
        {
            if (OnAlarmResetFailHandler != null)
            {
                OnAlarmResetFailHandler.Invoke(this, e);

            }
        }
        public void OnExecuteInitialFirst(object sender, EventArgs e)
        {
            if (OnExecuteInitialFirstHandler != null)
            {
                OnExecuteInitialFirstHandler.Invoke(this, e);

            }
        }
        public void OnExecuteAlarmResetFirst(object sender, EventArgs e)
        {
            if (OnExecuteAlarmResetFirstHandler != null)
            {
                OnExecuteAlarmResetFirstHandler.Invoke(this, e);

            }
        }
        public void OnStagePosition(object sender, EventArgs e)
        {
            if (OnStagePositionHandler != null)
            {
                OnStagePositionHandler.Invoke(this, e);

            }
        }
        public void OnLoadportStatus(object sender, EventArgs e)
        {
            if (OnLoadportStatusHandler != null)
            {
                OnLoadportStatusHandler.Invoke(this, e);

            }
        }
        public void OnInitialComplete(object sender, EventArgs e)
        {
            if (OnInitialCompleteHandler != null)
            {
                OnInitialCompleteHandler.Invoke(this, e);

            }
        }
        public void OnInitialUnComplete(object sender, EventArgs e)
        {
            if (OnInitialUnCompleteHandler != null)
            {
                OnInitialUnCompleteHandler.Invoke(this, e);

            }
        }
        public void OnMustInAutoMode(object sender, EventArgs e)
        {
            if (OnMustInAutoModeHandler != null)
            {
                OnMustInAutoModeHandler.Invoke(this, e);

            }
        }
        public void OnMustInManualMode(object sender, EventArgs e)
        {
            if (OnMustInManualModeHandler != null)
            {
                OnMustInManualModeHandler.Invoke(this, e);

            }
        }
        public void OnClamperNotLock(object sender, EventArgs e)
        {
            if (OnClamperNotLockHandler != null)
            {
                OnClamperNotLockHandler.Invoke(this, e);

            }
        }
        public void OnPODNotPutProperly(object sender, EventArgs e)
        {
            if (OnPODNotPutProperlyHandler != null)
            {
                OnPODNotPutProperlyHandler.Invoke(this, e);

            }
        }
        public void OnClamperActionTimeOut(object sender, EventArgs e)
        {
            if (OnClamperActionTimeOutHandler != null)
            {
                OnClamperActionTimeOutHandler.Invoke(this, e);

            }
        }
        public void OnClamperUnlockPositionFailed(object sender, EventArgs e)
        {
            if (OnClamperUnlockPositionFailedHandler != null)
            {
                OnClamperUnlockPositionFailedHandler.Invoke(this, e);

            }
        }
        public void OnVacuumAbnormality(object sender, EventArgs e)
        {
            if (OnVacuumAbnormalityHandler != null)
            {
                OnVacuumAbnormalityHandler.Invoke(this, e);

            }
        }
        public void OnStageMotionTimeout(object sender, EventArgs e)
        {
            if (OnStageMotionTimeoutHandler != null)
            {
                OnStageMotionTimeoutHandler.Invoke(this, e);

            }
        }
        public void OnStageOverUpLimitation(object sender, EventArgs e)
        {
            if (OnStageOverUpLimitationHandler != null)
            {
                OnStageOverUpLimitationHandler.Invoke(this, e);

            }
        }
        public void OnStageOverDownLimitation(object sender, EventArgs e)
        {
            if (OnStageOverDownLimitationHandler != null)
            {
                OnStageOverDownLimitationHandler.Invoke(this, e);

            }
        }
        public void OnReticlePositionAbnormality(object sender, EventArgs e)
        {
            if (OnReticlePositionAbnormalityHandler != null)
            {
                OnReticlePositionAbnormalityHandler.Invoke(this, e);

            }
        }
        public void OnClamperLockPositionFailed(object sender, EventArgs e)
        {
            if (OnClamperLockPositionFailedHandler != null)
            {
                OnClamperLockPositionFailedHandler.Invoke(this, e);

            }
        }
        public void OnPODPresentAbnormality(object sender, EventArgs e)
        {
            if (OnPODPresentAbnormalityHandler != null)
            {
                OnPODPresentAbnormalityHandler.Invoke(this, e);

            }
        }
        public void OnClamperMotorAbnormality(object sender, EventArgs e)
        {
            if (OnClamperMotorAbnormalityHandler != null)
            {
                OnClamperMotorAbnormalityHandler.Invoke(this, e);

            }
        }
        public void OnStageMotorAbnormality(object sender, EventArgs e)
        {
            if (OnStageMotorAbnormalityHandler != null)
            {
                OnStageMotorAbnormalityHandler.Invoke(this, e);

            }
        }

    }
}
