using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions
{
    /// <summary>State Machine Exception 代碼</summary>
    public enum EnumStateMachineExceptionCode
    {
        /*
         /// <summary>Masktransfer 無法分類的例外</summary>
         MaskTransferException = 40000,
         /// <summary>Masktransfer Initial 失敗的例外 </summary>
         MaskTransferInitialFailException = 40001,
         /// <summary>Masktransfer Initial 逾時的例外</summary>
         MaskTransferInitialTimeOutException = 40002,
         /// <summary>Masktransfer 必須 Reset 的例外</summary>
         MaskTransferMustResetException = 40003,
         /// <summary>Masktransfer 必須 Initial 的例外</summary>
         MaskTransferMustInitialException = 40004,


         /// <summary>Boxtransfer 無法分類的例外</summary>
         BoxTransferException = 30000,
         /// <summary>Boxtransfer Initial 失敗的例外</summary>
         BoxTransferInitialFailException = 30001,
         /// <summary>Boxtransfer Initial 逾時未完成的例外</summary>
         BoxTransferInitialTimeOutException = 30002,
         /// <summary>Boxtransfer 必須先 Reset 的例外</summary>
         BoxTransferMustResetException = 30003,
         /// <summary>Boxtransfer 必須先 Initial 的例外</summary>
         BoxTransferMustInitialException = 30004,
         */
        /**
        /// <summary>Load port 無法再分類的例外</summary>
        LoadportException = 70000,
        /// <summary>Load port  Initial 失敗的例外</summary>
        LoadportInitialFailException = 70001,
        /// <summary>Load port Initial 逾時的例外</summary>
        LoadportInitialTimeOutException = 70002,
        /// <summary>Load port 必須先 Reset 的例外</summary>
        LoadportInitialMustResetException = 70003,
        /// <summary>Load port 必須先 Initial的例外</summary>
        LoadportMustInitialException = 70004,
        /// <summary>load port Reset 失敗的例外</summary>
        LoadportResetFailException,
        /// <summary>load port Reset 逾時的例外</summary>
        LoadportResetTimeOutException,
        /// <summary>load port dock 要求先 Reset 的例外</summary>
        LoadportDockMustResetException,
        /// <summary>load port dock 要求先 Initial 的例外</summary>
        LoadportDockMustInitialException,
        /// <summary>load port dock 逾時未完成 </summary>
        LoadportDockTimeOutException,
        /// <summary>load port undock 要求先 Reset 的例外</summary>
        LoadportUndockMustResetException,
        /// <summary>load port undock 要求先 Initial 的例外</summary>
        LoadportUndockMustInitialException,
        /// <summary>load port undock 逾時未完成 </summary>
        LoadportUndockTimeOutException,
        */

        /// <summary>Drawer 無法再分類的例外</summary>
        DrawerException = 10000,
        /// <summary>Drawer Initial 失敗的例外</summary>
        DrawerInitialFailException = 10001,
        /// <summary>Drawer Initial 逾時的例外</summary>
        DrawerInitialTimeOutException = 10002,
        /// <summary>Drawer 必須 Reset的例外</summary>
        DrawerMustResetException = 10003,
        /// <summary>Drawer 必須 Initial的例外</summary>
        DrawerMustInitialException = 10004,
        /// <summary> Drawer Load 前的Initial 失敗  </summary>
        DrawerLoadInitialFailException,
        /// <summary> Drawer Load 前的Initial 逾時未完成  </summary>
        DrawerLoadInitialTimeOutException,
        /// <summary>Drawer Load 移到 Out 時發生例外 </summary>
        DrawerLoadMoveTrayToPositionOutFailException,
        /// <summary>Drawer Load 移到 Out 時逾時未到達 </summary>
        DrawerLoadMoveTrayToPositionOutTimeOutException,
        /// <summary>Drawer Load 移到 Home 時發生例外</summary>
        DrawerLoadMoveTrayToPositionHomeFailException,
        /// <summary>Drawer Load 移到 Home 時逾時未到達</summary>
        DrawerLoadMoveTrayToPositionHomeTimeOutException,
        /// <summary>Drawer Load 在Home 點檢查盒子是否存在時,逾時未得結果 </summary>
        DrawerLoadCheckBoxExistanceAtPositionHomeTimeOutException,
        /// <summary>Drawer Load 在Home 點檢查没有 Box , 回退到 Out 時發生錯誤</summary>
        DrawerLoadNoBoxRejectTrayToPositionOutFromPositionHomeFailException,
        /// <summary>Drawer Load 在Home 點檢查没有 Box , 回退到 Out 時逾時未達</summary>
        DrawerLoadNoBoxRejectTrayToPositionOutFromPositionHomeTimeOutException,
        /// <summary>Drawer Load, 從Position Home 移到 Position In 時失敗 </summary>
        DrawerLoadMoveTrayToPositionInFailException,
        /// <summary>Drawer Load, 從Position Home 移到 Position In 逾時未到 </summary>
        DrawerLoadMoveTrayToPositionInTimeOutException,
        /// <summary> Drawer Unload 前的Initial 失敗  </summary>
        DrawerUnloadInitialFailException,
        /// <summary> Drawer Unload 前的Initial 逾時未完成 </summary>
        DrawerUnloadInitialTimeOutException,
        /// <summary>Drawer Unload, 將 Tray 移到 In 失敗 </summary>
        DrawerUnloadMoveTrayToPositionInFailException,
        /// <summary>Drawer Unload, 將 Tray 移到 In 逾時未達</summary>
        DrawerUnloadMoveTrayToPositionInTimeOutException,
        /// <summary>Drawer Unload, 將 Tray 移到 Home 失敗</summary>
        DrawerUnloadMoveTrayToPositionHomeFailException,
        /// <summary>Drawer Unload, 將 Tray 移到 Home 逾時未達</summary>
        DrawerUnloadMoveTrayToPositionHomeTimeOutException,
        /// <summary>Drawer Unload 在Home 點檢查盒子是否存在時,逾時未得結果 </summary>
        DrawerUnloadCheckBoxExistanceAtPositionHomeTimeOutException,
        /// <summary>Drawer Unload 在Home 點檢查没有 Box , 回退到 In 時發生錯誤</summary>
        DrawerUnloadNoBoxRejectTrayToPositionInFromPositionHomeFailException,
        /// <summary>Drawer Unload 在Home 點檢查没有 Box , 回退到 In 時逾時未到</summary>
        DrawerUnloadNoBoxRejectTrayToPositionInFromPositionHomeTimeOutException,
        /// <summary>Drawer Unload, 從Position Home 移到 Position Out 時失敗 </summary>
        DrawerUnloadMoveTrayToPositionOutFailException,
        /// <summary>Drawer Unload, 從Position Home 移到 Position Out 逾時未到 </summary>
        DrawerUnloadMoveTrayToPositionOutTimeOutException,
        /// <summary>Drawer Unload, 從in 移到 Home 等待 Unload 命令時失敗</summary>
        DrawerMoveTrayToHomeWaitingUnloadInstructionFailException,
        /// <summary>Drawer Unload, 從in 移到 Home 等待 Unload 命令時逾時未到</summary>
        DrawerMoveTrayToHomeWaitingUnloadInstructionTimeOutException,
        /// <summary>Drawer Unload, 從in 移到 Home 等待 Unload 命令時失敗</summary>
        DrawerMoveTrayToHomeWaitingLoadInstructionFailException,
        /// <summary>Drawer Unload, 從in 移到 Home 等待 Unload 命令時逾時未到</summary>
        DrawerMoveTrayToHomeWaitingLoadInstructionTimeOutException,
        CabinetPLCAlarmException,
        CabinetPLCWarningException,

        /// <summary>Clean Chamber無法再分類的例外</summary>
        CleanChException = 2000,
        CleanChCleanFailException,
        CleanChInspectFailException,
        CleanChPLCAlarmException,
        CleanChPLCWarningException,

        /// <summary>Boxtransfer 無法分類的例外</summary>
        BoxTransferException = 30000,
        /// <summary>Boxtransfer Initial 失敗的例外</summary>
        BoxTransferInitialFailException = 30001,
        /// <summary>Boxtransfer Initial 逾時未完成的例外</summary>
        BoxTransferInitialTimeOutException = 30002,
        /// <summary>Boxtransfer 必須先 Reset 的例外</summary>
        BoxTransferMustResetException = 30003,
        /// <summary>Boxtransfer 必須先 Initial 的例外</summary>
        BoxTransferMustInitialException = 30004,
        BoxTransferPathMoveFailException,
        BoxTransferPLCExecuteFailException,
        BoxTransferPLCAlarmException,
        BoxTransferPLCWarningException,

        /// <summary>Masktransfer 無法分類的例外</summary>
        MaskTransferException = 40000,
        /// <summary>Masktransfer Initial 失敗的例外 </summary>
        MaskTransferInitialFailException = 40001,
        /// <summary>Masktransfer Initial 逾時的例外</summary>
        MaskTransferInitialTimeOutException = 40002,
        /// <summary>Masktransfer 必須 Reset 的例外</summary>
        MaskTransferMustResetException = 40003,
        /// <summary>Masktransfer 必須 Initial 的例外</summary>
        MaskTransferMustInitialException = 40004,
        /// <summary>Masktransfer 路徑移動失敗的例外</summary>
        MaskTransferPathMoveFailException,
        /// <summary>MaskTransfer PLC指令執行失敗的例外</summary>
        MaskTransferPLCExecuteFailException,
        MaskTransferPLCAlarmException,
        MaskTransferPLCWarningException,
        ClampInspectDeformPLCAlarmException,
        ClampInspectDeformPLCWarningException,

        /// <summary>Open Stage無法再分類的例外</summary>
        OpenStageException = 50000,
        OpenStageInitialFailException,
        OpenStagePLCExecuteFailException,
        OpenStageGuardException,
        OpenStagePLCAlarmException,
        OpenStagePLCWarningException,

        /// <summary>Inspection Chamber無法再分類的例外</summary>
        InspectionChException = 60000,
        InspectionChInitialFailException,
        InspectionChPLCExecuteFailException,
        InspectionChDefenseFailException,
        InspectionChInspectFailException,
        InspectionChPLCAlarmException,
        InspectionChPLCWarningException,

        /// <summary>Load port 無法再分類的例外</summary>
        LoadportException = 70000,
        /// <summary>Load port  Initial 失敗的例外</summary>
        LoadportInitialFailException = 70001,
        /// <summary>Load port Initial 逾時的例外</summary>
        LoadportInitialTimeOutException = 70002,
        /// <summary>Load port 必須先 Reset 的例外</summary>
        LoadportInitialMustResetException = 70003,
        /// <summary>Load port 必須先 Initial的例外</summary>
        LoadportMustInitialException = 70004,
      
        /// <summary>load port AlarmReset 失敗的例外</summary>
        LoadportAlarmResetFailException,
        /// <summary>load port AlarmReset 逾時的例外</summary>
        LoadportAlarmResetTimeOutException,

        /// <summary>Load port系統啟後Alarm Reset 失敗的例外</summary>
        LoadportSystemBootupAlarmResetFailException,
        /// <summary>Load port系統啟後Alarm Reset 失敗的例外</summary>
        LoadportSystemBootupAlarmResetTimeOutException,

        /// <summary>Load port系統啟後Initial 前必須先 Reset</summary>
        LoadportSystemBootupInitialMustResetException,
        /// <summary>Load port系統啟後Initial 逾時未完成</summary>
        LoadportSystemBootupInitialTimeOutException,

        /// <summary>load port dock 要求先 Reset 的例外</summary>
        LoadportDockMustResetException,
        /// <summary>load port dock 要求先 Initial 的例外</summary>
        LoadportDockMustInitialException,
        /// <summary>load port dock 逾時未完成 </summary>
        LoadportDockTimeOutException,
        /// <summary>load port undock 要求先 Reset 的例外</summary>
        LoadportUndockMustResetException,
        /// <summary>load port undock 要求先 Initial 的例外</summary>
        LoadportUndockMustInitialException,
        /// <summary>load port undock 逾時未完成 </summary>
        LoadportUndockTimeOutException,
        LoadportPLCAlarmException,
        LoadportPLCWarningException,
        LoadportUndockWithMaskMustInitialException,
        LoadportUndockWithMaskMustResetException,
        LoadportUndockWithMaskTimeOutException,
        LoadportDockWithMaskMustInitialException,
        LoadportDockWithMaskMustResetException,
        LoadportDockWithMaskTimeOutException,


        /// <summary>Universal 無法再分類的例外</summary>
        UniversalException,
        UniversalPLCExecuteFailException,
        UniversalEquipmentException,
        UniversalGeneralAlarmException,
        UniversalGeneralWarningException,
        UniversalCoverFanPLCAlarmException,
        UniversalCoverFanPLCWarningException
    }


}
