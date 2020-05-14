using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Msg
{
    /// <summary>
    /// Naming Rule:Alarm+Component編碼(2碼)+Alarm item(3碼)
    /// Component
    /// 00:LPA
    /// 01:LPB
    /// 02:MaskRobot
    /// 03:InspectionChamber
    /// 04:CleanChamber
    /// 05:SteelOpenStage
    /// 06:CrystalOpenStage
    /// 07:BoxRobot
    /// 08:Drawer
    /// 99:Other
    /// 
    /// Alarm item
    /// 001:TimeOut
    /// 002:EMO
    /// 003:ESD Abnormal
    /// 004:Particle Abnormal
    /// 009:Other
    /// </summary>

    public enum EnumAlarmId
    {
        //None = 1,

        Alarm00001 = 1,
        Alarm01001 = 1001,
        Alarm01002,
        Alarm02001,
        Alarm02010,
        Alarm03010,
        Alarm04010,

        //Common
        Timoueout,
        EMOPressed,
        ConditionAbnormal,
        TAPFeedbackTimeOut,

        StateJobMajorWorkerFail,
        StateJobPreWorkerFail,
        StateJobPostWorkerFail,
        StateJobWorkerCannotAbort,


        //Mask Transfer
        MT_TactileOverSpec1,
        MT_TactileOverSpec2,
        MT_TactileOverSpec3,
        MT_TactileOverSpec4,
        MT_RobotMoveError,
        MT_GripperMoveError,
        MT_ePotentialExcessive,
        MT_RobotForceOverSpec,
        MT_RobotCannotReach,
        MT_MaskInWrongPosition,//應被設為Pasue
        MT_GripperInWrongPosition,//應被設為Pasue
        MT_RobotShouldBeInLoadPort,

        //Mask Transfer
        BT_RobotMoveError,
        BT_GripperMoveError,
        BT_ePotentialExcessive,
        BT_RobotForceOverSpec,
        BT_RobotCannotReach,
        BT_MaskInWrongPosition,//應被設為Pasue
        BT_GripperInWrongPosition,//應被設為Pasue


        //Loadport
        PlungerAbnormal,
        OHTArriveWrongLP,
        LPAlreadyHasPod,
        E84Abnormal,
        StageAbnormal,
        ClamperAbnormal,
        MaskMaterialAbnormal,
        ReadRFIDTimeOut,
        LPE84InitialFailed,
        LPPlungerInitialFailed,
        LPClamperInitialFailed,
        LPRFIDInitialFailed,
        LPStageInitialFailed,
        LPLightGateInitialFailed,
        LPSideIN_LaserInitialFailed,
        LPSideOUT_LaserInitialFailed,
        LPFront_LaserInitialFailed,
        LPTOPCCDInitialFailed,
        LPSideCCDInitialFailed,

        //MaskTransfer
        TactileOverSpec,
        RobotMoveError,
        ePotentialExcessive,
        RobotForceOverSpec,


        //Clean Chamber
        OcapAbn3008,   //Light ON Intensity < OOS SPEC
        PurgePressureOOS,
        PurgePressureOOC,
        PurgePressureStopError,
        ReciveJobTimeout,
        LoadDefectInformationTimeout,
        RobotMoveToPurgTimeout,
        RobotMoveToInspTimeout,
        RobotMoveToHomeTimeout,
        RobotMoveToEntryTimeout,



        //InspectionChamber
        IC_CCDError = 6001,
        IC_StageError,
        IC_LoadJobFail,
        IC_LightError,
        IC_InitialError,
        IC_LaserError,



        //SecsMgr
        Secs_InvalidSecsMsg,




        //Logic Alarm
        MachineCloseFailed,



        DotNetException,
    }
}
