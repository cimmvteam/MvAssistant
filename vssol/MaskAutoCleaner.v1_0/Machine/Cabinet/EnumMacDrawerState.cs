using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    public enum EnumMacDrawerState
    {
       
        InitialStart,
        InitialIng,
        InitialComplete,
        InitialFail,
        InitialTimeout,

      
        LoadGotoInStart,
        LoadGotoInIng,
        LoadGotoInComplete,
        LoadGotoInFail,
        LoadGotoInTimeOut,

        IdleReadyForLoadBoxAtIn ,
        LoadBoxAtInComplete,

        LoadGotoHomeStart,
        LoadGotoHomeIng,
        LoadGotoHomeComplete,
        LoadGotoOutStart,
        LoadGotoOutIng,
        LoadGotoOutComplete,
  
        LoadGotoHomeFail,
        LoadGotoHomeTimeOut,
        LoadGotoOutFail,
        LoadGotoOutTimeOut,


        IdleReadyForGet,
        LoadBoxGetAtOut,
      
        UnloadGotoOutStart,
        UnloadGotoOutIng,
        UnloadGotoOutComplete,
        UnloadGotoOutFail,
        UnloadGotoOutTimeOut,

        IdleReadyForUnloadBoxAtOut,
        UnloadBoxPutAtOut,

        UnloadGotoHomeStart,
        UnloadGotoHomeIng,
        UnloadGotoHomeComplete,
        UnloadGotoHomeFail,
        UnloadGotoHomeTimeOut,

        UnloadGotoInStart,
        UnloadGotoInIng,
        UnloadGotoInComplete,
       
        UnloadGotoInFail,
        UnloadGotoInTimeOut,

        IdleReadyForUnloadBoxAtIn,
        UnloadBoxAtInComplete,

       

       

      //  ExpGotoInFail,
       // ExpGotoInTimeout,
      
    }
}
