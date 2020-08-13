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
       
        LoadMoveTrayToPositionOutStart_LoadMoveTrayToPositionOutIng,
        LoadMoveTrayToPositionOutIng_LoadMoveTrayToPositionOutComplete,
        LoadMoveTrayToPositionOutIng_LoadMoveTrayToPositionOutTimeOut,
        LoadMoveTrayToPositionOutIng_LoadMoveTrayToPositionOutFail,
        LoadMoveTrayToPositionOutComplete_IdleForPutBoxOnTrayAtPositionOut,

        // Load (將 Tray 從In 移到 Home )
        LoadMoveTrayToPositionHomeStart_LoadMoveTrayToPositionHomeIng,
        LoadMoveTrayToPositionHomeIng_LoadMoveTrayToPositionHomeComplete,
        LoadMoveTrayToPositionHomeIng_LoadMoveTrayToPositionHomeTimeOut,
        LoadMoveTrayToPositionHomeIng_LoadMoveTrayToPositionHomeFail,

        // Load, Tray 移到 Home 之後, 檢查Box 是否存在
        /// <summary>Box 存在</summary><remarks>2020/08/03 King Add</remarks>
        LoadMoveTrayToPositionHomeComplete_LoadCheckBoxExistenceAtPositionHome,

        LoadCheckBoxExistenceAtPositionHome_LoadBoxExistAtPositionHome,
        /// <summary>Box 不存在</summary><remarks>2020/08/03 King Add</remarks>
        LoadCheckBoxExistenceAtPositionHome_LoadBoxNotExistAtPositionHome,
        /// <summary>檢查逾時</summary><remarks>2020/08/03 King Add</remarks>
        LoadCheckBoxExistenceAtPositionHome_LoadCheckBoxExistenceAtPositionHomeTimeOut,
        /// <summary>檢查合格再回到 LoadGotoHomeComplete</summary>
        LoadBoxExistAtPositionHome_LoadMoveTrayToPositionHomeComplete,
        /// <summary>檢查不合格再回到 LoadGotoHomeComplete</summary>
        LoadBoxNotExistAtPositionHome_LoadMoveTrayToPositionHomeComplete,

        LoadMoveToPositionHomeComplete_LoadNoBoxRejectToPositionOutFromPositionHomeStart,
        UnloadNoBoxRejectToInFromHomeStart_UnloadNoBoxRejectToOutFromHomeIng,
        LoadNoBoxRejectTrayToPositionOutFromPositionHomeIng_LoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete,
        LoadNoBoxRejectTrayToPositionOutFromPositionHomeIng_LoadNoBoxRejectTrayToOutFromPositionHomeFail,
        LoadNoBoxRejectTrayToPositionOutFromPositionHomeIng_LoadNoBoxRejectTrayToPositionOutFromPositionHomeTimeOut,
        LoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete_IdleForPutBoxOnTrayAtPositionOut,

        // Load(將 Tray 從Home 移到 Out)
        LoadMoveTrayToPositionHomeComplete_LoadMoveTrayToPositionInStart,//TODO: =>
        LoadMoveTrayToPositionInStart_LoadMoveTrayToPositionInIng,
        LoadMoveTrayToPositionInIng_LoadMoveTrayToPositionInComplete,
        LoadMoveTrayToPositionInIng_LoadMoveTrayToPositionInTimeOut,
        LoadMoveTrayToPositionInIng_LoadMoveTrayToPositionInFail,
        // Load(將 Box 移開)
        LoadMoveTrayToPositionInComplete_IdleForGetBoxOnTrayAtPositionIn,
       #endregion Load

        #region unload
        // Unload(將 Tray 移到 Out)
      
        UnloadMoveTrayToPositionInStart_UnloadMoveTrayToPosiotionInIng,
        UnloadMoveTrayToPositionInIng_UnloadMoveTrayToPositionInComplete,
        UnloadMoveTrayToPositionInIng_UnloadMoveTrayToPositionInTimeOut,
        UnloadMoveTrayToPositionInIng_UnloadMoveTrayToPOsitionInFail,
        UnloadMoveTrayToInComplete_IdleForPutBoxOnTrayAtPositionIn,

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
        InitialComplete_NULL,
        IdleForPutBoxOnTrayAtPositionOut_NULL,
        LoadNoBoxRejectTrayToPositionOutFromPositionHomeStart_LoadNoBoxRejectTrayToPositionOutFromHomeIng,
        IdleForGetBoxOnTrayAtPositionIn_NULL,
        IdleForPutBoxOnTrayAtPositionIn_NULL,
        // LoadGotoHomeComplete_LoadRejectToInFromHomeStart,
        // Unload (移走 Box)
        //UnloadGotoHomeComplete_IdleReadyForUnloadBoxAtIn,

        #endregion unload


    }
}
