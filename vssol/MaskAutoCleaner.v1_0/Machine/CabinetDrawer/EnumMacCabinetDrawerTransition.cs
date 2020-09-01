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
        SystemBootup_NULL,
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
        /// <summary>Load, 檢查Tray 上没有盒子=> 將 Tray 回退到 Out 開始</summary>
        LoadCheckBoxExistenceComplete_LoadRejectToOutStart,
        /// <summary>Load, 檢查Tray 上没有盒子將 Tray 回退到 Out 開始=> 移動中</summary>
        LoadRejectToOutStart_LoadRejectToOutIng,
        /// <summary>Load, 檢查Tray 上没有盒子 將 Tray 回退到 Out 移動中=> 到逹Out</summary>
        LoadRejectToOutIng_LoadRejectToOutComplete,
        /// <summary>Load, 檢查Tray 上没有盒子 將 Tray 回退到 Out  到逹Out=>等待將 Box 放到Tray 上</summary>
        LoadRejectToOutComplete_WaitingLoadInstruction,
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
