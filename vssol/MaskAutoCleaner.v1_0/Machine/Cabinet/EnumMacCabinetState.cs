using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    public enum EnumMacCabinetState
    {

       

        

        /// <summary>Load, 將合於條件的Drawer Tray 移到 Out, 開始 </summary>
        LoadMoveDrawerTraysToOutStart,
        /// <summary>Load, 將合於條件的Drawer Tray 移到 Out, 移動中 </summary>
        LoadMoveDrawerTraysToOutIng,
        /// <summary>/// <summary>Load, 將合於條件的Drawer Tray 移到 Out, 完成 </summary></summary>
        LoadMoveDrawerTraysToOutComplete,

        /// <summary>系統啟動時的 Initial, 開始</summary>
        BootupInitialDrawersStart,
        /// <summary>系統啟動時的 Initial, 進行中</summary>
        BootupInitialDrawersIng,
        /// <summary>系統啟動時的 Initial, 結束</summary>
        BootupInitialDrawersComplete,

        /// <summary>同步Drawer 的狀態, 開始</summary>
        SynchronousDrawerStatesStart,
        /// <summary>同步Drawer 的狀態, 進行中</summary>
        SynchronousDrawerStatesIng,
        /// <summary>同步Drawer 的狀態, 結束</summary>
        SynchronousDrawerStatesComplete,
        Start,
        Idle,
        BankOutLoadMoveTraysToOutForPutBoxOnTraysIng,
        BankOutLoadMoveTraysToOutForPutBoxOnTraysStart,
        BankOutLoadMoveTraysToOutForPutBoxOnTraysComplete,
        BankOutLoadMoveSpecificTrayToInForBoxRobotGrabStart,
        BankOutLoadMoveSpecificTrayToInForBoxRobotGrabIng,
        BankOutLoadMoveSpecificTrayToInForBoxRobotGrabComplete,

        BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabStart,
        BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabIng,
        BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabComplete,

        BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutStart,
        BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutIng,
        BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutComplete,

        BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutStart,
        BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutIng,
        BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutComplete,

        BankOutUnLoadMoveSpecificTraysToOutForGrabStart,
        BankOutUnLoadMoveSpecificTraysToOutForGrabIng,
        BankOutUnLoadMoveSpecificTraysToOutForGrabComplete,
        /**

/// <summary>開機後尚未Initial</summary>
BeforeInitial,


/// <summary>Initial全部Drawer~ 開始</summary>
InitialAllDrawersStart,
/// <summary>Initial全部Drawer~ 進行中</summary>
InitialAllDrawersIng,
/// <summary>Initial全部Drawer~ 完成</summary>
/// <remarks>包含正常完成Initial 的 Drawer, 及發生例外的 Drawer</remarks>
InitialAllDrawersComplete,
/// <summary>Initial全部Drawer~ 全部無法完成</summary>
InitialAllDrawersError,

/// <summary>Initial指定Drawer~ 開始</summary>
InitialDrawersStart,
/// <summary>Initial指定Drawer~ 進行中</summary>
InitialDrawersIng,
/// <summary>Initial指定Drawer~ 完成</summary>
InitialDrawersComplete,
/// <summary>Initial指定Drawer~ 全部無法完成</summary>
InitialDrawersError,
/// <summary>接收Load 指令</summary>
WaitingLoad,

/// <summary>將指定 Tray 移到 Out~開始</summary>
MoveDrawerTraysToOutStart,
/// <summary>將 指定Tray 移到 Out ~ 進行中 </summary>
MoveDrawerTraysToOutIng,
/// <summary>將 指定Tray 移到 Out ~ 完成</summary>
MoveDrawerTraysToOutComplete,
*/

    }


}
