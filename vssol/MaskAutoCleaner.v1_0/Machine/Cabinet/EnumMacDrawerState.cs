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
        IdleForPutBoxOnTrayAtIn,

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
        IdleForGetBoxOnTrayAtOut,
      
        UnloadGotoOutStart,
        UnloadGotoOutIng,
        UnloadGotoOutComplete,
        UnloadGotoOutFail,
        UnloadGotoOutTimeOut,
        IdleForPutBoxOnTrayAtOut,
        IdleForGetBoxOnTrayAtIn,
     

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
    }
}
