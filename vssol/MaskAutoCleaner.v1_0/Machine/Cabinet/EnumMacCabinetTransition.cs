using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    public enum EnumMacCabinetTransition
    {
        /// <summary>開機後未 initial => Initial 所有Drawer,開始</summary>
        BeforeInitial_InitialAllDrawersStart,
        /// <summary>Initial 所有Drawer 開始=> Initial 所有Drawer,進行中</summary>
        InitialAllDrawersStart_InitialAllDrawersIng,
        /// <summary>Initial 所有Drawer 進行中=> Initial 所有Drawer完成</summary>
        InitialAllDrawersIng_InitialAllDrawersComplete,
        /// <summary>Initial 所有Drawer 完成=>等待 接收 Load 指令</summary>
        InitialAllDrawersComplete_WaitingLoad,
       /// <summary>Initial 所有Drawer 完成=> Initial 所有Drawer,所有可 Initial 的Drawer 都無法 Initial</summary>
        InitialAllDrawersComplete_InitialAllDrawersError,
        /// <summary>等待接收Load 指令, 最後一個狀態</summary>
        WaitingLoad_NULL,
        /// <summary>Initial, 所有的Drawr 都不無法 完成 Initial, 最後一個狀態</summary>
        InitialAllDrawersError_NULL,
        

        /// <summary>等待接收 Load=>Initial 指定的 Drawer </summary>
        WaitingLoad_InitialDrawersStart,
        /// <summary>Initial 特定的 Drawer, 開始=> Initial 特定的 Drawer, 進行中</summary>
        InitialDrawersStart_InitialDrawersIng,
        /// <summary>Initial 特定的 Drawer, 進行中=> Initial 特定的 Drawer, 完成</summary>
        InitialDrawersIng_InitialDrawersComplete,
        /// <summary>Initial 特定的 Drawer, 完成=> Initial 特定的 Drawer, 等待 Load</summary>
        InitialDrawersComplete_WaitingtLoad,
        /// <summary>Initial 特定的 Drawer , 完成=> 都無法完成Initial</summary>
        InitialDrawersComplete_InitialDrawersError,
        /// <summary>無法 Initial 所有指定的 Drawer</summary>
        InitialDrawersError_NULL,

        /// <summary>等待Load=> 將Tray 移到 Out 閧始</summary>
        WaitingLoad_MoveDrawerTraysToOutStart,
        /// <summary>將Tray 移到 Out 閧始=> 將Tray 移到 Out 進行中</summary>
        MoveDrawerTraysToOutStart_MoveDrawerTraysToOutIng,
        /// <summary>將Tray 移到 Out進行中=> 將Tray 移到 Out 完成</summary>
        MoveDrawerTraysToOutIng_MoveDrawerTraysToOutComplete,
        /// <summary>將Tray 移到 Out進行中=> 將Tray 移到 Out 完成</summary>
        MoveDrawerTraysToOutComplete_WaitingLoad,
       
    }
}
