using System.Reflection;
using System.ComponentModel;

namespace MvAssistant.Mac.DrawerSocketTest
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
        [Description("SetMotionSpeed")]
        SetMotionSpeed,
        [Description("SetTimeOut")]
        SetTimeOut,
        [Description("SetParameter")]
        SetParameter,
        [Description("TrayMotion")]
        TrayMotion,
        [Description("BrightLED")]
        BrightLED,
        [Description("PositionRead")]
        PositionRead,
        [Description("BoxDetection")]
        BoxDetection,
        [Description("WriteNetSetting")]
        WriteNetSetting,
        [Description("LCDMsg")]
        LCDMsg,
        [Description("INI")]
        INI,
    }

    public enum Drawer_RcvCMDList
    {
        [Description("ReplyTrayMotion")]
        ReplyTrayMotion,
        [Description("ReplySetSpeed")]
        ReplySetSpeed,
        [Description("ReplySetTimeOut")]
        ReplySetTimeOut,
        [Description("ReplyPosition")]
        ReplyPosition,
        [Description("ReplyBoxDetection")]
        ReplyBoxDetection,
        [Description("TrayArrive")]
        TrayArrive,
        [Description("ButtonEvent")]
        ButtonEvent,
    }

    public enum Drawer_EnumRcvAlarmList
    {
        [Description("TimeOutEvent")]
        TimeOutEvent,
        [Description("TrayMotioning")]
        TrayMotioning,
        [Description("INIFailed")]
        INIFailed,
        [Description("TrayMotionError")]
        TrayMotionError,
        [Description("Error")]
        Error,
        [Description("SysStartUp")]
        SysStartUp,
    }
}
