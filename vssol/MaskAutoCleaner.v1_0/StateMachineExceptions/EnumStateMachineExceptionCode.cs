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

    }

   
}
