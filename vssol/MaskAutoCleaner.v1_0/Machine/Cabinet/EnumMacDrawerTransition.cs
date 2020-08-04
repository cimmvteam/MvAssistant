using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    public enum EnumMacDrawerTransition
    {
        #region Initial
        // Initial
        InitialStart_InitialIng,
        Initialing_InitialComplete,
        Initialing_InitialTimeOut, 
        Initialing_InitialFail,    
        #endregion Initial


        #region Load
        // Load  (將 Tray 移到 In 的位置)
       
        LoadGotoInStart_LoadGotoInIng,
        LoadGotoInIng_LoadGotoInComplete,
        LoadGotoInIng_LoadGotoInTimeOut,
        LoadGotoInIng_LoadGotoInFail,
        LoadGotoInComplete_IdleForPutBoxOnTrayAtIn,

        // Load (將 Tray 從In 移到 Home )
        LoadGotoHomeStart_LoadGotoHomeIng,
        LoadGotoHomeIng_LoadGotoHomeComplete,
        LoadGotoHomeIng_LoadGotoHomeTimeOut,
        LoadGotoHomeIng_LoadGotoHomeFail,

        // Load, Tray 移到 Home 之後, 檢查Box 是否存在
        /// <summary>Box 存在</summary><remarks>2020/08/03 King Add</remarks>
        LoadGotoHomeComplete_LoadCheckBoxExistenceAtHome,

        LoadCheckBoxExistenceAtHome_LoadBoxExistAtHome,
        /// <summary>Box 不存在</summary><remarks>2020/08/03 King Add</remarks>
        LoadCheckBoxExistenceAtHome_LoadBoxNotExistAtHome,
        /// <summary>檢查逾時</summary><remarks>2020/08/03 King Add</remarks>
        LoadCheckBoxExistenceAtHome_LoadCheckBoxExistenceAtHomeTimeOut,
        /// <summary>檢查合格再回到 LoadGotoHomeComplete</summary>
        LoadBoxExistAtHome_LoadGotoHomeComplete,
        /// <summary>檢查不合格再回到 LoadGotoHomeComplete</summary>
        LoadBoxNotExistAtHome_LoadGotoHomeComplete,

        LoadGotoHomeComplete_LoadNoBoxRejectToInFromHomeStart,
        UnloadNoBoxRejectToInFromHomeStart_UnloadNoBoxRejectToOutFromHomeIng,
        LoadNoBoxRejectToInFromHomeIng_LoadNoBoxRejectToInFromHomeComplete,
        LoadNoBoxRejectToInFromHomeIng_LoadNoBoxRejectToInFromHomeFail,
        LoadNoBoxRejectToInFromHomeIng_LoadNoBoxRejectToInFromHomeTimeOut,
        LoadNoBoxRejectToInFromHomeComplete_IdleForPutBoxOnTrayAtIn,

        // Load(將 Tray 從Home 移到 Out)
        LoadGotoHomeComplete_LoadGotoOutStart,//TODO: =>
        LoadGotoOutStart_LoadGotoOutIng,
        LoadGotoOutIng_LoadGotoOutComplete,
        LoadGotoOutIng_LoadGotoOutTimeOut,
        LoadGotoOutIng_LoadGotoOutFail,
        // Load(將 Box 移開)
        LoadGotoOutComplete_IdleForGetBoxOnTrayAtOut,
       #endregion Load

        #region unload
        // Unload(將 Tray 移到 Out)
      
        UnloadGotoOutStart_UnloadGotoOutIng,
        UnloadGotoOutIng_UnloadGotoOutComplete,
        UnloadGotoOutIng_UnloadGotoOutTimeOut,
        UnloadGotoOutIng_UnloadGotoOutFail,
        UnloadGotoOutComplete_IdleForPutBoxOnTrayAtOut,

        // Unload(將 Tray 由Out 移到 Home) Main
        UnloadGotoHomeStart_UnloadGotoHomeIng,
        UnloadGotoHomeIng_UnloadGotoHomeComplete,
        UnloadGotoHomeIng_UnloadGotoHomeTimeOut,
        UnloadGotoHomeIng_UnloadGotoHomeFail,

        // Unload(將 Tray 由Home 移到 In)
        UnloadGotoHomeComplete_UnloadGotoInStart,
        UnloadGotoInStart_UnloadGotoInIng,
        UnloadGotoInIng_UnloadGotoInComplete,
        UnloadGotoInIng_UnloadGotoInTimeOut,
        UnloadGotoInIng_UnloadGotoInFail,

        UnloadGotoInComplete_IdleForGetBoxOnTrayAtIn,
        UnloadGotoHomeComplete_UnloadCheckBoxExistenceAtHome,
        UnloadCheckBoxExistenceAtHome_UnloadBoxExistAtHome,
        UnloadCheckBoxExistenceAtHome_UnloadBoxNotExistAtHome,
        UnloadCheckBoxExistenceAtHome_UnloadCheckBoxExistenceAtHomeTimeOut,
        UnloadBoxExistAtHome_UnloadGotoHomeComplete,
        UnloadBoxNotExistAtHome_UnloadGotoHomeComplete,
        UnloadGotoHomeComplete_UnloadNoBoxRejectToOutFromHomeStart,
        UnloadNoBoxRejectToInFromHomeIng_UnloadNoBoxRejectToOutFromHomeComplete,
        UnloadNoBoxRejectToInFromHomeIng_UnloadNoBoxRejectToOutFromHomeFail,
        UnloadNoBoxRejectToInFromHomeIng_UnloadNoBoxRejectToOutFromHomeTimeOut,
        UnloadNoBoxRejectToOutFromHomeComplete_IdleForPutBoxOnTrayAtOut,
        // LoadGotoHomeComplete_LoadRejectToInFromHomeStart,
        // Unload (移走 Box)
        //UnloadGotoHomeComplete_IdleReadyForUnloadBoxAtIn,

        #endregion unload


    }
}
