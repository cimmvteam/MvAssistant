using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet.Drawer
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
        UnloadNoBoxRejectTrayToPositionInFromPositionHomeStart_UnloadNoBoxRejectTrayToPositionInFromPositionHomeIng,
        LoadNoBoxRejectTrayToPositionOutFromPositionHomeIng_LoadNoBoxRejectTrayToPositionOutFromPositionHomeComplete,
        LoadNoBoxRejectTrayToPositionOutFromPositionHomeIng_LoadNoBoxRejectTrayToPositionOutFromPositionHomeFail,
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
        UnloadMoveTrayToPositionHomeStart_UnloadMoveTrayToPositionHomeIng,
        UnloadMoveTrayToPositionHomeIng_UnloadMoveTrayToPositionHomeComplete,
        UnloadMoveTrayToPositionHomeIng_UnloadMoveTrayToPositionHomeTimeOut,
        UnloadMoveTrayToPOsitionHomeIng_UnloadMoveTrayToPositionHomeFail,

        // Unload(將 Tray 由Home 移到 In)
        UnloadMoveTrayToPositionHomeComplete_UnloadMoveTrayToPositionOutFromPositionHomeStart,
        UnloadMoveTrayToPositionOutFromPositionHomeStart_UnloadMoveTrayToPositionOutFromPositionHomeIng,
        UnloadMoveTrayToPositionOutFromPositionHomeIng_UnloadMoveTrayToPositionOutFromPOsitionHomeComplete,
        UnloadMoveTrayToPositionOutFromPositionHomeIng_UnloadMoveTrayToPositionOutFromPositionHomeTimeOut,
        UnloadMoveTrayToPositionOutFromPositionHomeIng_UnloadMoveTrayToPositionOutFromPositionHomeFail,

        UnloadMoveTrayToPositionOutComplete_IdleForGetBoxOnTrayAtPositionOut,
        UnloadMoveTrayToPositionHomeComplete_UnloadCheckBoxExistenceAtPositionHome,
        UnloadCheckBoxExistenceAtPositionHome_UnloadBoxExistAtPositionHome,
        UnloadCheckBoxExistenceAtPositionHome_UnloadBoxNotExistAtPositionHome,
        UnloadCheckBoxExistenceAtPositionHome_UnloadCheckBoxExistenceAtPositionHomeTimeOut,
        UnloadBoxExistAtPositionHome_UnloadMoveTrayToHomeComplete,
        UnloadBoxNotExistAtPositionHome_UnloadMoveTrayToPositionHomeComplete,
        UnloadMoveTrayToPositionHomeComplete_UnloadNoBoxRejectTrayToPositionInFromPositionHomeStart,
        UnloadNoBoxRejectTrayToPositionInFromPositionHomeIng_UnloadNoBoxRejectTrayToPositionInFromPositionHomeComplete,
        UnloadNoBoxRejectTrayToPositionInFromPositionHomeIng_UnloadNoBoxRejectTrayToPositionInFromPositionHomeFail,
        UnloadNoBoxRejectTrayToPositionInFromPositionHomeIng_UnloadNoBoxRejectTrayToPositionInFromPositionHomeTimeOut,
        UnloadNoBoxRejectTrayToPositionInFromPositionHomeComplete_IdleForPutBoxOnTrayAtPositionIn,
        InitialComplete_NULL,
        IdleForPutBoxOnTrayAtPositionOut_NULL,
        LoadNoBoxRejectTrayToPositionOutFromPositionHomeStart_LoadNoBoxRejectTrayToPositionOutFromHomeIng,
        IdleForGetBoxOnTrayAtPositionIn_NULL,
        IdleForPutBoxOnTrayAtPositionIn_NULL,
        IdleForGetBoxOnTrayAtPositionOut_NULL,
        LoadInitialStart_LoadInitialIng,
        LoadInitialIngt_LoadInitialComplete,
        LoadInitialComplete_LoadInitialComplete_LoadMoveTrayToPositionOutStart,
        InitialComplete_LoadMoveTrayToPositionOutStart,
        InitialComplete_UnloadMoveTrayToPositionInStart,
        // LoadGotoHomeComplete_LoadRejectToInFromHomeStart,
        // Unload (移走 Box)
        //UnloadGotoHomeComplete_IdleReadyForUnloadBoxAtIn,

        #endregion unload


    }
}
