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
  

        IdleReadyToDock,
        DockStart,
        Docking,
        DockComplete,

        IdleReadyToUndock,
        UndockStart,
        Undocking,
        UndockComplete,

        IdleReadyToUnload,
        UnloadExecuted,

        WaitReset,
        ResetStart,
        Reseting,
        ResetComplete,

        WaitInitial,
        InitialStart,
        Initialing,
        InitialComplete,

        ExpMustReset,
        ExpResetFail,
        ExpResetTimeout,
        ExpMustInitial,
        ExpInitialFail,
        ExpInitialTimeout,
        ExpDockTimeout,
        ExpUndockTimeOut,
        
    }
}
