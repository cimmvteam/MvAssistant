using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Drawer
{
    public enum EnumMacDrawerState
    {
       
        InitialStart,
        InitialIng,
        InitialComplete,
        InitialFail,
        InitialTimeout,

      
        LoadMoveTrayToPositionOutStart,
        LoadMoveTrayToPositionOutIng,
        LoadMoveTrayToPositionOutComplete,
        LoadMoveTrayToPositionOutFail,
        LoadMoveTrayToPositionOutTimeOut,
        IdleForPutBoxOnTrayAtIn,

        LoadMoveTrayToPositionHomeStart,
        LoadMoveTrayToPositionHomeIng,
        LoadMoveTrayToPositionHomeComplete,

        /// <summary>在Home 點 檢查 盒子在不在?</summary><remarks>2020/08/03 new, King</remarks>
        LoadCheckBoxExistenceAtPositionHome,

        /// <summary>在Home 位置檢查盒子存在</summary><remarks>2020/08/03 new, King</remarks>
        LoadBoxExistAtPositionHome,
        /// <summary>在Home 位置檢查盒子不存在</summary><remarks>2020/08/03 new, King</remarks>
        LoadBoxNotExistAtPositionHome,
        /// <summary>在Home 位置檢查盒子存在與否時逾時</summary><remarks>2020/08/03 new, King</remarks>
        LoadCheckBoxExistenceAtPositionHomeTimeOut,

        /// <summary>Load 時因為没有 Box 而退回到 In (Start)</summary> <remarks>2020/08/03 new, King</remarks>       
        LoadNoBoxRejectTrayToPositionOutFromPositionHomeStart,
        /// <summary>Load 時因為没有 Box 而退回到 In (Ing)</summary>  <remarks>2020/08/03 new, King</remarks>      
        LoadNoBoxRejectTrayToPositionOutFromPositionHomeIng,
        /// <summary>Load 時因為没有 Box 而退回到 In (OK)</summary>   <remarks>2020/08/03 new, King</remarks>
        LoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete,
        /// <summary>Load 時因為没有 Box 而退回到 In (失敗)</summary>   <remarks>2020/08/03 new, King</remarks>
        LoadNoBoxRejectTrayToPositionOutFromPositionHomeFail,
        /// <summary>Load 時因為没有 Box 而退回到 In (逾時)</summary>   <remarks>2020/08/03 new, King</remarks>
        LoadNoBoxRejectTrayToPositionOutFromPositionHomeTimeOut,

        LoadMoveTrayToPositionInStart,
        LoadMoveTrayToPositionInIng,
        LoadMoveTrayToPositionInComplete,
  
        LoadMoveTrayToPositionHomeFail,
        LoadMoveTrayToPositionHomeTimeOut,
        LoadMoveTrayToPositionInFail,
        LoadMoveTrayToPositionInTimeOut,
        IdleForGetBoxOnTrayAtPositionIn,
      
        UnloadMoveTrayToPositionInStart,
        UnloadMoveTrayToPositionInIng,
        UnloadMoveTrayToPositionInComplete,
        UnloadMoveTrayToPOsitionInFail,
        UnloadMoveTrayToPositionInTimeOut,
        IdleForPutBoxOnTrayAtPositionIn,
        IdleForGetBoxOnTrayAtIn,
     

        UnloadMoveTrayToPositionHomeStart,
        UnloadMoveTrayToPositionHomeIng,
        UnloadMoveTrayToPositionHomeComplete,
        UnloadMoveTrayToPositionHomeFail,
        UnloadMoveTrayToPositionHomeTimeOut,

        UnloadMoveTrayToPositionOutFromPositionHomeStart,
        UnloadMoveTrayToPositionOutFromPositionHomeIng,
        UnloadMoveTrayToPositionOutFromPOsitionHomeComplete,
        UnloadMoveTrayToPositionInFail,
        UnloadMoveTrayToPositionOutTimeOut,
        UnloadCheckBoxExistenceAtPositionHome,
        UnloadBoxExistAtPositionHome,
        UnloadBoxNotExistAtPositionHome,
        UnloadCheckBoxExistenceAtPositionHomeTimeOut,
        UnloadNoBoxRejectTrayToPositionInFromPositionHomeStart,
        UnloadNoBoxRejectTrayToPositionInFromPositionHomeIng,
        UnloadNoBoxRejectTrayToPositionInFromPositionHomeComplete,
        UnloadNoBoxRejectTrayToPositionInFromPositionHomeFail,
        UnloadNoBoxRejectTrayToPositionInFromPOsitionHomeTimeOut,
        IdleForPutBoxOnTrayAtPositionOut,
        LoadInitialComplete,
        LoadInitialIng,
    }
}
