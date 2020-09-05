using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    public enum EnumMacCabinetTransition
    {
       
        /// <summary>載入所有Drawer State Machine 資料, 開始=>載入中</summary>
        StateMachineLoadAllDrawersStateMchineStart_StateMachineLoadAllDrawersStateMchineIng,
        /// <summary>載入所有Drawer State Machine 資料, 載入中=>完成</summary>
        StateMachineLoadAllDrawersStateMchineIng_StateMachineLoadAllDrawersStateMchineComplete,
        /// <summary>載入所有Drawer State Machine 資料, 完成 => 設為最末State </summary>
        StateMachineLoadAllDrawersStateMchineComplete_NULL,
        /// <summary>載入所有Drawer State Machine 資料, 完成=> AnyState</summary>
        //StateMachineLoadAllDrawersStateMchineComplete_AnyState,

        /// <summary>Load, 將 Drawer 的Tray 移到 Out 開始=> 移動中</summary>
        LoadMoveDrawerTraysToOutStart_LoadMoveDrawerTraysToOutIng,
        /// <summary>Load, 將 Drawer 的Tray 移到 Out 移動中=> 完成</summary>
        LoadMoveDrawerTraysToOutIng_LoadMoveDrawerTraysToOutComplete,
        /// <summary>Load, 將 Drawer 的Tray 移到 Out 完成=> 設為最末 State</summary>
        LoadMoveDrawerTraysToOutComplete_NULL,
        /// <summary>Load, 將 Drawer 的Tray 移到 Out 完成=> Any State </summary>
        //LoadMoveDrawerTraysToOutComplete_AnyState,

        /// <summary>系統啟動後 Initial Drawer, 開始=> 動作中</summary>
        BootupInitialDrawersStart_BootupInitialDrawersIng,
        /// <summary>系統啟動後 Initial Drawer, 動作中=> 完成</summary>
        BootupInitialDrawersIng_BootupInitialDrawersComplete,
        /// <summary>系統啟動後 Initial Drawer 完成 =>   AnyState </summary>
        //BootupInitialDrawersComplete_AnyState,

        /// <summary>同步所有 Drawer  的 State, 開始=> 同步中</summary>
        SynchronousDrawerStatesStart_SynchronousDrawerStatesIng,
        /// <summary>同步所有 Drawer  的 State, 同步中= 完成 </summary>
        SynchronousDrawerStatesIng_SynchronousDrawerStatesComplete,
        /// <summary>同步所有 Drawer  的 State 完成=>  AnyState</summary>
        //SynchronousDrawerStatesComplete_AnyState

    }
}
