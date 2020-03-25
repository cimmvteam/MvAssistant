using System.Reflection;
using System.ComponentModel;

namespace MvAssistant.MaskTool_v0_1.SocketComm
{
    public enum LP_EnumSendCMDList
    {
        [Description("AskPlacementStatus")]
        AskPlacementStatus=1,
        [Description("AskPresentStatus")]
        AskPresentStatus,
        [Description("AskClamperStatus")]
        AskClamperStatus,
        [Description("AskRFIDStatus")]
        AskRFIDStatus,
        [Description("AskBarcodeStatus")]
        AskBarcodeStatus,
        [Description("AskVacuumStatus")]
        AskVacuumStatus,
        [Description("AskReticleExistStatus")]
        AskReticleExistStatus,
        [Description("DockRequest")]
        DockRequest,
        [Description("UndockRequest")]
        UndockRequest,
        [Description("AlarmReset")]
        AlarmReset,
    }

    public enum LP_EnumRcvCMDList
    {
        [Description("Placement")]
        Placement,
        [Description("Present")]
        Present,
        [Description("Clamper")]
        Clamper,
        [Description("ClamperUnlockComplete")]
        ClamperUnlockComplete,
        [Description("RFID")]
        RFID,
        [Description("Barcode ID")]
        BarcodeID,
        [Description("VacuumComplete")]
        VacuumComplete,
        [Description("DockPODComplete_Empty")]
        DockPODComplete_Empty,
        [Description("DockPODComplete_HasReticle")]
        DockPODComplete_HasReticle,
        [Description("DockPODStart")]
        DockPODStart,
        [Description("CoverDisappear")]
        CoverDisappear,
        [Description("UndockComplete")]
        UndockComplete,
        [Description("ClamperLockComplete")]
        ClamperLockComplete,
        [Description("AlarmResetSuccess")]
        AlarmResetSuccess,
    }

    public enum LP_EnumRcvAlarmList
    {
        [Description("ClamperActionTimeOut")]
        ClamperActionTimeOut,
        [Description("ClamperUnlockPositionFailed")]
        ClamperUnlockPositionFailed,
        [Description("VacuumAbnormality")]
        VacuumAbnormality,
        [Description("StageMotionTimeout")]
        StageMotionTimeout,
        [Description("StageOverUpLimitation")]
        StageOverUpLimitation,
        [Description("StageOverDownLimitation")]
        StageOverDownLimitation,
    }

    public enum Drawer_SendCMDList
    {

    }

    public enum Drawer_RcvCMDList
    {

    }
}
