using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.CabinetDrawer
{
  public   enum EnumMacCabinetDrawerTransition
  {
        /// <summary>系統啟動=> 設定為末端狀態</summary>
        SystemBootup_SystemBootupInitialStart,
        /// <summary>啟動Initial 開始 => 啟動 Initial 進行中</summary>
        SystemBootupInitialStart_SystemBootupInitialIng,
        /// <summary>啟動 Initial 進行中=> 啟動 Initial 完成</summary>
        SystemBootupInitialIng_SystemBootupInitialComplete,
        /// <summary>啟動 Initial 完成 => 等待 Load 命令</summary>
        SystemBootupInitialComplete_WaitingLoadInstruction,
        /// <summary>等待 Load 命令=> 設定為未端狀態</summary>
        WaitingLoadInstruction_NULL,

        /// <summary>Load, 移動 Tray 到 Out 開始=> 移動中</summary>
        LoadMoveTrayToOutStart_LoadMoveTrayToOutIng,
        /// <summary>Load, 移動 Tray 到 Out 移動中=> 完成</summary>
        LoadMoveTrayToOutIng_LoadMoveTrayToOutComplete,
        /// <summary>Load, 移動 Tray 到 Out 完成=> 等待將 Box 放到Tray 上</summary>
        LoadMoveTrayToOutComplete_LoadWaitingPutBoxOnTray,
        /// <summary>Load,等待將 Box 放到Tray 上=> 設為末端狀態 </summary>
        LoadWaitingPutBoxOnTray_NULL,
        /// <summary>Load, 將 Tray 移到Home 開始=> 移動中</summary>
        LoadMoveTrayToHomeStart_LoadMoveTrayToHomeIng,
        /// <summary>Load, 將 Tray 移到Home 移動中=>完成</summary>
        LoadMoveTrayToHomeIng_LoadMoveTrayToHomeComplete,
        /// <summary>Load, 將 Tray 移到Home 完成=> 檢查Tray 上有没有盒子開始</summary>
        LoadMoveTrayToHomeComplete_LoadCheckBoxExistenceStart,
        /// <summary>Load, 檢查Tray 上有没有盒子開始=> 檢查Tray 上有没有盒子檢查中</summary>
        LoadCheckBoxExistenceStart_LoadCheckBoxExistenceIng,
        /// <summary>Load, 檢查Tray 上有没有盒子檢查中=> 檢查Tray 上有没有盒子檢查完成</summary>
        LoadCheckBoxExistenceIng_LoadCheckBoxExistenceComplete,
        /// <summary>Load, 檢查Tray 上有盒子=> 等待命令將Tray 送到 In</summary>
        LoadCheckBoxExistenceComplete_LoadWaitingMoveTrayToIn,
        /// <summary>Load, 等待命令將Tray 送到 In => 設為最末狀態</summary>
        LoadWaitingMoveTrayToIn_NULL,
        /// <summary>Load, 檢查Tray 上没有盒子=> 將 Tray 回退到 Out 開始</summary>
        LoadCheckBoxExistenceComplete_LoadRejectToOutStart,
        /// <summary>Load, 檢查Tray 上没有盒子將 Tray 回退到 Out 開始=> 移動中</summary>
        LoadRejectToOutStart_LoadRejectToOutIng,
        /// <summary>Load, 檢查Tray 上没有盒子 將 Tray 回退到 Out 移動中=> 到逹Out</summary>
        LoadRejectToOutIng_LoadRejectToOutComplete,
        /// <summary>Load, 檢查Tray 上没有盒子 將 Tray 回退到 Out  到逹Out=>等待將 Box 放到Tray 上</summary>
        LoadRejectToOutComplete_LoadWaitingPutBoxOnTray,

        /// <summary>Load, 將 Tray 移到 In 開始=>移動中  </summary>
        LoadMoveTrayToInStart_LoadMoveTrayToInIng,
        /// <summary>Load, 將 Tray 移到 In, 移動中=>完成</summary>
        LoadMoveTrayToInIng_LoadMoveTrayToInComplete,
        /// <summary>Load, 將Tray 移到 In, 完成=> 等待盒子被拿走</summary>
        LoadMoveTrayToInComplete_LoadWaitingGetBoxOnTray,
        /// <summary>Load, 將Tray 移到 In完成, 等待盒子被拿走=>設為最末狀態</summary>
        LoadWaitingGetBoxOnTray_NULL,


        /// <summary>將 Tray 移到 Home 以等待 Unload 指令: 開始=>移動中 </summary>
        MoveTrayToHomeWaitingUnloadInstructionStart_MoveTrayToHomeWaitingUnloadInstructionIng,
        /// <summary>將 Tray 移到 Home 以等待 Unload 指令: 移動中 => 完成 </summary>
        MoveTrayToHomeWaitingUnloadInstructionIng_MoveTrayToHomeWaitingUnloadInstructionComplete,
        /// <summary>將 Tray 移到 Home 以等待 Unload 指令:  完成  => 等待 Unload 指令</summary>
        MoveTrayToHomeWaitingUnloadInstructionComplete_WaitingUnloadInstruction,
        /// <summary>等待 Unload 指令</summary>
        WaitingUnloadInstruction_NULL,
     
        /// <summary>Unload, 將 Tray 移到 In 開始 => 移動中</summary>
        UnloadMoveTrayToInStart_UnloadMoveTrayToInIng,
        /// <summary>Unload, 將Tray移到 In 移動中 => 完成   /// </summary>
        UnloadMoveTrayToInIng_UnloadMoveTrayToInComplete,
        /// <summary>Unload, 將 Tray 移到 In 完成 => 等待將 Box 放到 Tray </summary>
        UnloadMoveTrayToInComplete_UnloadWaitingPutBoxOnTray,
        /// <summary>Unload, 等待將盒子放到 Tray 上=> 設為最末狀態</summary>
        UnloadWaitingPutBoxOnTray_NULL,

        /// <summary>Unload, 將Tray 移到Home 開始=> 移動中</summary>
        UnloadMoveTrayToHomeStart_UnloadMoveTrayToHomeIng,
        /// <summary>Unload, 將Tray 移到Home 移動中=> 完成</summary>
        UnloadMoveTrayToHomeIng_UnloadMoveTrayToHomeComplete,
        /// <summary>Unload, 將Tray 移到Home 完成=> 檢查有没有盒子開始</summary>
        UnloadMoveTrayToHomeComplete_UnloadCheckBoxExistenceStart,
        /// <summary>Unload, 檢查有没有盒子, 開始=> 檢查中</summary>
        UnloadCheckBoxExistenceStart_UnloadCheckBoxExistenceIng,
        /// <summary>Unload, 檢查有没有盒子, 檢查中=> 完成</summary>
        UnloadCheckBoxExistenceIng_UnloadCheckBoxExistenceComplete,
        /// <summary>Unload, 檢查有没有盒子, 完成=> 將Tray 送到 Out 開始</summary>
        UnloadCheckBoxExistenceComplete_UnloadMoveTrayToOutStart,
        /// <summary>Unload, 將Tray 送到 Out 開始 => 移動中</summary>
        UnloadMoveTrayToOutStart_UnloadMoveTrayToOutIng,
        /// <summary>Unload, 將 Tray 送到 Out 移動中=> 完成 </summary>
        UnloadMoveTrayToOutIng_UnloadMoveTrayToOutComplete,
        /// <summary>Unload, 將 Tray 送到 Out 完成=> 等待將 盒子拿走 </summary>
        UnloadMoveTrayToOutComplete_UnloadWaitingGetBoxOnTray,
        /// <summary>Unload, 等待將 盒子拿走=>設為最末狀態 </summary>
        UnloadWaitingGetBoxOnTray_NULL,      

        /// <summary>Unload, 在Tray 從In 退到 Home 時, 檢查到没有盒子, 將狀態轉為 WaitingUnloadInstruction </summary>
        UnloadCheckBoxExistenceComplete_WaitingUnloadInstruction,

        /// <summary>將Tray移到 Home 準備接收 Load 指令, 開始移動=> 移動中</summary>
        MoveTrayToHomeWaitingLoadInstructionStart_MoveTrayToHomeWaitingLoadInstructionIng,
        /// <summary>將Tray移到 Home 準備接收 Load 指令, 移動中=> 完成</summary>
        MoveTrayToHomeWaitingLoadInstructionIng_MoveTrayToHomeWaitingLoadInstructionComplete,
        /// <summary>將Tray移到 Home 準備接收 Load 指令, 移動中=> 等待接收 Load 指令</summary>
        MoveTrayToHomeWaitingLoadInstructionComplete_WaitingLoadInstruction,
        WaitingLoadInstruction_LoadMoveTrayToOutStart,
        LoadWaitingPutBoxOnTray_LoadMoveTrayToHomeStart,
        LoadWaitingMoveTrayToIn_LoadMoveTrayToInStart,
        /**
/// <summary>初始化: 開始=> 進行中</summary>
InitialStart_InitialIng,
/// <summary>初始化: 進行中=> 完成</summary>
InitialIng_InitialComplete,
/// <summary>初始化: 完成 => 結束 </summary>
InitialComplete_NULL,

/// <summary>將托盤移到Home: 開始 => 移動中</summary>
MoveTrayToHomeStart_MoveTrayToHomeIng,
/// <summary>將托盤移到Home: 移動中 => 完成</summary>
MoveTrayToHomeIng_MoveTrayToHomeComplete,
/// <summary>將托盤移到Home: 完成 => 結束</summary>
MoveTrayToHomeComplete_NULL,

/// <summary>將托盤移到Out: 開始 => 移動中</summary>
MoveTrayToOutStart_MoveTrayToOutIng,
/// <summary>將托盤移到Out: 移動中 => 完成 </summary>
MoveTrayToOutIng_MoveTrayToOutComplete,
/// <summary>將托盤移到Out: 完成=> 結束</summary>
MoveTrayToOutComplete_NULL,

/// <summary>將托盤移到In: 開始 => 移動中</summary>
MoveTrayToInStart_MoveTrayToInIng,
/// <summary>將托盤移到In: 移動中 => 完成 </summary>
MoveTrayToInIng_MoveTrayToInComplete,
/// <summary>將托盤移到Out: 完成=> 結束</summary>
MoveTrayToInComplete_NULL,
*/
    }
}
