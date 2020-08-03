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

        /// <summary>在Home 點 檢查 盒子在不在?</summary><remarks>2020/08/03 new, King</remarks>
        LoadCheckBoxExistenceAtHome,

        /// <summary>在Home 位置檢查盒子存在</summary><remarks>2020/08/03 new, King</remarks>
        LoadBoxExistAtHome,
        /// <summary>在Home 位置檢查盒子不存在</summary><remarks>2020/08/03 new, King</remarks>
        LoadBoxNotExistAtHome,
        /// <summary>在Home 位置檢查盒子存在與否時逾時</summary><remarks>2020/08/03 new, King</remarks>
        LoadCheckBoxExistenceAtHomeTimeOut,

        /// <summary>Load 時因為没有 Box 而退回到 In (Start)</summary> <remarks>2020/08/03 new, King</remarks>       
        LoadNoBoxRejectToInFromHomeStart,
        /// <summary>Load 時因為没有 Box 而退回到 In (Ing)</summary>  <remarks>2020/08/03 new, King</remarks>      
        LoadNoBoxRejectToInFromHomeIng,
        /// <summary>Load 時因為没有 Box 而退回到 In (OK)</summary>   <remarks>2020/08/03 new, King</remarks>
        LoadNoBoxRejectToInFromHomeComplete,
        /// <summary>Load 時因為没有 Box 而退回到 In (失敗)</summary>   <remarks>2020/08/03 new, King</remarks>
        LoadNoBoxRejectToInFromHomeFail,
        /// <summary>Load 時因為没有 Box 而退回到 In (逾時)</summary>   <remarks>2020/08/03 new, King</remarks>
        LoadNoBoxRejectToInFromHomeTimeOut,

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
        UnloadCheckBoxExistenceAtHome,
        UnloadBoxExistAtHome,
        UnloadBoxNotExistAtHome,
        UnloadCheckBoxExistenceAtHomeTimeOut,
        UnloadNoBoxRejectToInFromHomeStart,
        UnloadNoBoxRejectToInFromHomeIng,
        UnloadNoBoxRejectToInFromHomeComplete,
        UnloadNoBoxRejectToInFromHomeFail,
        UnloadNoBoxRejectToInFromHomeTimeOut,
    }
}
