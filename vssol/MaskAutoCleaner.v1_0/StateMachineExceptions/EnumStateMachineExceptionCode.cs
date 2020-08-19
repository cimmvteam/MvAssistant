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
        /// <summary>Masktransfer 無法分類的例外</summary>
        MaskTransferException = 10000,
        /// <summary>Masktransfer Initial 失敗的例外 </summary>
        MaskTransferInitialFailException = 10001,
        /// <summary>Masktransfer Initial 逾時的例外</summary>
        MaskTransferInitialTimeOutException = 10002,
        /// <summary>Masktransfer 必須 Reset 的例外</summary>
        MaskTransferMustResetException = 10003,
        /// <summary>Masktransfer 必須 Initial 的例外</summary>
        MaskTransferMustInitialException = 10004,


        /// <summary>Masktransfer 無法分類的例外</summary>
        BoxTransferException = 20000,
        /// <summary>Masktransfer Initial 失敗的例外</summary>
        BoxTransferInitialFailException = 20001,
        /// <summary>Masktransfer Initial 逾時未完成的例外</summary>
        BoxTransferInitialTimeOutException = 20002,
        /// <summary>Masktransfer 必須先 Reset 的例外</summary>
        BoxTransferMustResetException = 20003,
        /// <summary>Masktransfer 必須先 Initial 的例外</summary>
        BoxTransferMustInitialException = 20004,


        /// <summary>Load port 無法再分類的例外</summary>
        LoadportException = 30000,
        /// <summary>Load port  Initial 失敗的例外</summary>
        LoadportInitialFailException = 30001,
        /// <summary>Load port Initial 逾時的例外</summary>
        LoadportInitialTimeOutException = 30002,
        /// <summary>Load port 必須先 Reset 的例外</summary>
        LoadportMustResetException = 30003,
        /// <summary>Load port 必須先 Initial的例外</summary>
        LoadportMustInitialException = 30004,

        /// <summary>Drawer 無法再分類的例外</summary>
        DrawerException = 40000,
        /// <summary>Drawer Initial 失敗的例外</summary>
        DrawerInitialFailException = 40001,
        /// <summary>Drawer Initial 逾時的例外</summary>
        DrawerInitialTimeOutException = 40002,
        /// <summary>Drawer 必須 Reset的例外</summary>
        DrawerMustResetException = 40003,
        /// <summary>Drawer 必須 Initial的例外</summary>
        DrawerMustInitialException = 40004,
        /// <summary>Drawer Load 移到 Out 時發生例外 </summary>
        DrawerLoadMoveTrayToPositionOutFailException = 40005,
        /// <summary>Drawer Load 移到 Out 時逾時未到達 </summary>
        DrawerLoadMoveTrayToPositionOutTimeOutException = 40006,
        /// <summary>Drawer Load 移到 Home 時發生例外</summary>
        DrawerLoadMoveTrayToPositionHomeFailException =40007,
        /// <summary>Drawer Load 移到 Home 時逾時未到達</summary>
        DrawerLoadMoveTrayToPositionHomeTimeOutException = 40008,
        /// <summary>Drawer Load 在Home 點檢查盒子是否存在時,逾時未得結果 </summary>
        DrawerLoadCheckBoxExistanceAtPositionHomeTimeOutException =40009,
        /// <summary>Drawer Load 在Home 點檢查没有 Box , 回退到 Out 時發生錯誤</summary>
        DrawerLoadNoBoxRejectTrayToPositionOutFromPositionHomeFailException=4010,
        /// <summary>Drawer Load 在Home 點檢查没有 Box , 回退到 Out 時逾時未達</summary>
        DrawerLoadNoBoxRejectTrayToPositionOutFromPositionHomeTimeOutException = 4011,
        /// <summary>Drawer Load, 從Position Home 移到 Position In 時失敗 </summary>
        DrawerLoadMoveTrayToPositionInFailException=4012,
        /// <summary>Drawer Load, 從Position Home 移到 Position In 逾時未到 </summary>
        DrawerLoadMoveTrayToPositionInTimeOutException = 4013,
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

    }

   
}
