using MvAssistant.DeviceDrive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompLoadPort
{
    public interface IMacHalLoadPortComp: IMacHalComponent
    {
         bool IsConnected { get; }
         string DeviceIndex { get; }
        
        string CommandDockRequest();
        string CommandUndockRequest();
        string CommandAskPlacementStatus();
        string CommandAskPresentStatus();
        string CommandAskClamperStatus();
        string CommandAskRFIDStatus();
        string CommandAskBarcodeStatus();
        string CommandAskVacuumStatus();
        string CommandAskReticleExistStatus();
        string CommandAlarmReset();
        string CommandAskStagePosition();
        string CommandAskLoadportStatus();
        string CommandInitialRequest();
        string CommandManualClamperLock();
        string CommandManualClamperUnlock();
        string CommandManualClamperOPR();
        string CommandManualStageUp();
        string CommandManualStageInspection();
        string CommandManualStageDown();
        string CommandManualStageOPR();
        string CommandManualVacuumOn();
        string CommandManualVacuumOff();
        event EventHandler OnPlacementHandler;
        event EventHandler OnPresentHandler;
        event EventHandler OnClamperHandler;
        event EventHandler OnRFIDHandler;
        event EventHandler OnBarcode_IDHandler;
        event EventHandler OnClamperLockCompleteHandler;
        event EventHandler OnVacuumCompleteHandler;
        event EventHandler OnDockPODStartHandler;
        event EventHandler OnDockPODComplete_HasReticleHandler;
        event EventHandler OnDockPODComplete_EmptyHandler;
        event EventHandler OnUndockCompleteHandler;
        event EventHandler OnClamperUnlockCompleteHandler;
        event EventHandler OnAlarmResetSuccessHandler;
        event EventHandler OnAlarmResetFailHandler;
        event EventHandler OnExecuteInitialFirstHandler;
        event EventHandler OnExecuteAlarmResetFirstHandler;
        event EventHandler OnStagePositionHandler;
        event EventHandler OnLoadportStatusHandler;
        event EventHandler OnInitialCompleteHandler;
        event EventHandler OnInitialUnCompleteHandler;
        event EventHandler OnMustInAutoModeHandler;
        event EventHandler OnMustInManualModeHandler;
        event EventHandler OnClamperNotLockHandler;
        event EventHandler OnPODNotPutProperlyHandler;
        event EventHandler OnClamperActionTimeOutHandler;
        event EventHandler OnClamperUnlockPositionFailedHandler;
        event EventHandler OnVacuumAbnormalityHandler;
        event EventHandler OnStageMotionTimeoutHandler;
        event EventHandler OnStageOverUpLimitationHandler;
        event EventHandler OnStageOverDownLimitationHandler;
        event EventHandler OnReticlePositionAbnormalityHandler;
        event EventHandler OnClamperLockPositionFailedHandler;
        event EventHandler OnPODPresentAbnormalityHandler;
        event EventHandler OnClamperMotorAbnormalityHandler;
        event EventHandler OnStageMotorAbnormalityHandler;
        event EventHandler OnHostLostLoadPortConnectionHandler;
    }
}
