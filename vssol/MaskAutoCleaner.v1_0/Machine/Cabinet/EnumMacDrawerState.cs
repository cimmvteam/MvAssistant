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
        InitialFail,
        InitialTimeout,

        LoadAnyState,
        LoadGotoInStart,
        LoadGotoInIng,
        LoadGotoInComplete,
        LoadPrework1Fail,
        LoadPrework1TimeOut,

        IdleReadyForLoadBoxAtIn ,
        LoadBoxAtInComplete,

        LoadGotoHomeStart,
        LoadGotoHomeIng,
        LoadGotoHomeComplete,
        LoadGotoOutStart,
        LoadGotoOutIng,
        LoadGotoOutComplete,
        LoadComplete,
        LoadMainworkGotoHomeFail,
        LoadMainworkGotoHomeTimeOut,
        LoadMainworkGotoOutFail,
        LoadMainworkGotoOutTimeOut,


        IdleReadyForGet,
        LoadBoxGetAtOut,

        UnloadAnyState,
        UnloadGotoOutStart,
        UnloadGotoOutIng,
        UnloadGotoOutComplete,
        UnloadPrework1Fail,
        UnloadPrework1TimeOut,

        IdleReadyForUnloadBoxAtOut,
        UnloadBoxPutAtOut,

        UnloadGotoHomeStart,
        UnloadGotoHomeIng,
        UnloadGotoHomeComplete,
        UnloadMainworkGotoHomeFail,
        UnloadMainworkGotoHomeTimeOut,

        UnloadGotoInStart,
        UnloadGotoInIng,
        UnloadGotoInComplete,
        UnloadComplete,
        UnloadMainworkGotoInFail,
        UnloadMainworkGotoInTimeOut,

        IdleReadyForUnloadBoxAtIn,
        UnloadBoxAtInComplete,

       

       

      //  ExpGotoInFail,
       // ExpGotoInTimeout,
      
    }
}
