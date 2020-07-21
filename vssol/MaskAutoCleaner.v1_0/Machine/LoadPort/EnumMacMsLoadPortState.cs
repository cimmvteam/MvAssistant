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
        MustResetBeforeDocking,
        MustInitialBeforeDocking,
        Docking,
        MustResetDuringDocking,
        MustInitialDuringDocking,
        DockComplete,

        IdleReadyToUndock,
        UndockStart,
        MustResetBeforeUndocking,
        MustInitialBeforeUndocking,
        Undocking,
        MustResetDuringUndocking,
        MustInitialDuringUndocking,
        UndockComplete,

        IdleReadyToUnload,
        UnloadExecuted,

        WaitNormalReset,
        NormalResetStart,
        NormalReseting,
        NormalResetComplete,

        WaitNormalInitial,
        NormalInitialStart,
        MustResetBeforeNormalInitialing,
        NormalInitialing,
        MustResetDuringNormalInitialing,
        NormalInitialComplete,
        
    }
}
