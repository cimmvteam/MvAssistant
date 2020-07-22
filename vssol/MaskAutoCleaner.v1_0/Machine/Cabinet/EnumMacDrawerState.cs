using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    public enum EnumMacDrawerState
    {
        AnyState,
     

        WaitInitial,
        InitialStart,
        InitialIng,
        InitialComplete,

        LoadAnyState,
        LoadGotoInStart,
        LoadGotoInIng,
        LoadGotoInComplete,
        IdleReadyForLoadBoxAtIn=LoadGotoInComplete,
        LoadBoxAtInComplete,

        LoadGotoHomeStart,
        LoadGotoHomeIng,
        LoadGotoHomeComplete,
        LoadGotoOutStart,
        LoadGotoOutIng,
        LoadGotoOutComplete,
        LoadComplete,
        IdleReadyForGet,
        LoadBoxGetAtOut,

        UnloadAnyState,
        UnloadGotoOutStart,
        UnloadGotoOutIng,
        UnloadGotoOutComplete,
        IdleReadyForUnloadBoxAtOut,
        UnloadBoxPutAtOut,

        UnloadGotoHomeStart,
        UnloadGotoHomeIng,
        UnloadGotoHomeComplete,
        UnloadGotoInStart,
        UnloadGotoInIng,
        UnloadGotoInComplete,
        UnloadComplete,
        IdleReadyForUnloadBoxAtIn= UnloadComplete,
        UnloadBoxAtInComplete,

        ExpInitialFail,
        ExpInitialTimeout,
        ExpGotoInFail,
        ExpGotoInTimeout,
        ExpGotoHomeFail,
        ExpGotoHomeTimeout,
        ExpGotoOutFail,
        ExpGotoOutTimeout
    }
}
