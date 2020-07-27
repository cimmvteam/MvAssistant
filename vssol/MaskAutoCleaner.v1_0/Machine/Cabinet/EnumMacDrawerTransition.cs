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
        AnyState_WaitInitial,
        WaitInitial_InitialStart,
        InitialStart_InitialIng,
        Initialing_InitialComplete,
        Initial_InitialTimeOut,
        Initial_InitialFail,
        #endregion Initial


        #region Load
        // Load Prework1 (將 Tray 移到 In 的位置)
        LoadAnyState_LoadGotoInStart,
        LoadGotoInStart_LoadGotoInIng,
        LoadGotoInIng_LoadGotoInComplete,
        LoadPrework1_LoadPrework1TimeOut,
        LoadPrework1_LoadPrework1Fail,


        // Load Prework2 (將盒子放到 Tray)
        LoadGotoInComplete_IdleReadyForLoadBoxAtIn,
        IdleReadyForLoadBoxAtIn_LoadBoxAtInComplete,

        // Load01 (將 Tray 從In 移到 Home )
        LoadBoxAtInComplete_LoadGotoHomeStart,
        LoadGotoHomeStart_LoadGotoHomeIng,
        LoadGotoHomeIng_LoadGotoHomeComplete,
        LoadMainwork_LoadGotoHomeTimeOut,
        LoadMainwork_LoadGotoHomeFail,

        // Load02(將 Tray 從Home 移到 Out)
        LoadGotoHomeComplete_LoadGotoOutStart,
        LoadGotoOutStart_LoadGotoOutIng,
        LoadGotoOutIng_LoadGotoOutComplete,
        LoadMainwork_LoadGotoOutTimeOut,
        LoadMainwork_LoadGotoOutFail,

        // Load Postwork(將 Box 移開)
        LoadGotoOutComplete_IdleReadyForGetBox,
        IdleReadyForGetBox_LoadBoxGetAtOut,
        #endregion Load

        #region unload
        // Unload Prework 01(將 Tray 移到 Out)
        UnloadAnyState_UnloadGotoOutStart,
        UnloadGotoOutStart_UnloadGotoOutIng,
        UnloadGotoOutIng_UnloadGotoOutComplete,
        UnLoadPrework1_UnLoadPrework1TimeOut,
        UnLoadPrework1_UnLoadPrework1Fail,


        // UnLoad Prework02(放入 Box)
        UnloadGotoOutComplete_IdleReadyForUnloadBoxAtOut,
        IdleReadyForUnloadBoxAtOut_UnloadBoxAtOutComplete,

        // Unload01(將 Tray 由Out 移到 Home)
        UnloadBoxAtOutComplete_UnloadGotoHomeStart,
        UnloadGotoHomeStart_UnloadGotoHomeIng,
        UnloadGotoHomeIng_UnloadGotoHomeComplete,
        UnloadMainwork_UnloadGotoHomeTimeOut,
        UnloadMainwork_UnloadGotoHomeFail,

        // Unload02(將 Tray 由Home 移到 In)
        UnloadGotoHomeComplete_UnloadGotoInStart,
        UnloadGotoInStart_UnloadGotoInIng,
        UnloadGotoInIng_UnloadGotoInComplete,
        UnloadMainwork_UnloadGotoInTimeOut,
        UnloadMainwork_UnloadGotoInFail,

        // Unload PostWork(移走 Box)
        UnloadGotoHomeComplete_IdleReadyForUnloadBoxAtIn,
        IdleReadyForUnloadBoxAtIn_UnloadComplete,
        #endregion unload

        
    }
}
