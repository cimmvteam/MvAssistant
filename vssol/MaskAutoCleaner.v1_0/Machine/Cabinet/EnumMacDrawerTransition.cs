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
        LoadGotoIn_LoadGotoInTimeOut,
        LoadGotoIn_LoadGotoInFail,


        // Load  (將盒子放到 Tray)
        LoadGotoInComplete_IdleReadyForLoadBoxAtIn,
        IdleReadyForLoadBoxAtIn_LoadBoxAtInComplete,

        // Load (將 Tray 從In 移到 Home )
        LoadBoxAtInComplete_LoadGotoHomeStart,
        LoadGotoHomeStart_LoadGotoHomeIng,
        LoadGotoHomeIng_LoadGotoHomeComplete,
        LoadGotoHome_LoadGotoHomeTimeOut,
        LoadGotoHome_LoadGotoHomeFail,

        // Load(將 Tray 從Home 移到 Out)
        LoadGotoHomeComplete_LoadGotoOutStart,
        LoadGotoOutStart_LoadGotoOutIng,
        LoadGotoOutIng_LoadGotoOutComplete,
        LoadGotoOut_LoadGotoOutTimeOut,
        LoadGotoOut_LoadGotoOutFail,

        // Load(將 Box 移開)
        LoadGotoOutComplete_IdleReadyForGetBox,
        IdleReadyForGetBox_LoadBoxGetAtOut,
        #endregion Load

        #region unload
        // Unload(將 Tray 移到 Out)
      
        UnloadGotoOutStart_UnloadGotoOutIng,
        UnloadGotoOutIng_UnloadGotoOutComplete,
        UnloadGotoOut_UnloadGotoOutTimeOut,
        UnloadGotoOut_UnloadGotoOutFail,


        // UnLoad(放入 Box)
        UnloadGotoOutComplete_IdleReadyForUnloadBoxAtOut,
        IdleReadyForUnloadBoxAtOut_UnloadBoxAtOutComplete,

        // Unload(將 Tray 由Out 移到 Home)
        UnloadBoxAtOutComplete_UnloadGotoHomeStart,
        UnloadGotoHomeStart_UnloadGotoHomeIng,
        UnloadGotoHomeIng_UnloadGotoHomeComplete,
        UnloadGotoHome_UnloadGotoHomeTimeOut,
        UnloadGotoHome_UnloadGotoHomeFail,

        // Unload(將 Tray 由Home 移到 In)
        UnloadGotoHomeComplete_UnloadGotoInStart,
        UnloadGotoInStart_UnloadGotoInIng,
        UnloadGotoInIng_UnloadGotoInComplete,
        UnloadGotoIn_UnloadGotoInTimeOut,
        UnloadGotoIn_UnloadGotoInFail,

        // Unload (移走 Box)
        UnloadGotoHomeComplete_IdleReadyForUnloadBoxAtIn,
        
        #endregion unload

        
    }
}
