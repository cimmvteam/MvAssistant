using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.LoadPort
{
    /// <summary>狀態</summary>
    /// <remarks>
    /// Postfix:
    /// 完成: Complete
    /// 無法完成: Failed
    /// </remarks>
    public enum  EnumMacMsLoadPortState
    {
  

        //IdleReadyToDock,
        DockStart,
        DockIng,
        DockMustReset,
        DockMustInitial,
        DockComplete,
        DockTimeOut,

        //IdleReadyToUndock,
        UndockStart,
        UndockIng,
        UndockMustReset,
        UndockMustInitial,
        UndockComplete,
        UndockTimeOut,

        //IdleReadyToUnload,
        //UnloadExecuted,

       // WaitReset,
        ResetStart,
        ResetIng,
        ResetComplete,
        ResetFail,
        ResetTimeOut,

        //WaitInitial,
        InitialStart,
        Initialing,
        InitialComplete,
        InitialTimeOut,
        InitialMustReset,
    }
}
