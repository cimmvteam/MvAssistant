using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.CabinetDrawer
{
    public enum EnumMacCabinetDrawerState
    {

        /// <summary>系統啟動 </summary>
        SystemBootup,
        /// <summary>系統啟後Initial, 開始</summary>
        SystemBootupInitialStart,
        /// <summary>系統啟後Initial, 進行中</summary>
        SystemBootupInitialIng,
        /// <summary>系統啟後Initial, 完成</summary>
        /// <remarks>next: WailtingLoad</remarks>
        SystemBootupInitialComplete,

        /// <summary>等待 Load 指令</summary>
        WaitingLoadInstruction,

        /// <summary>Load: 將 Tray 移到 Out, 開始</summary>
        LoadMoveTrayToOutStart,
        /// <summary>Load: 將 Tray 移到 Out, 進行中</summary>
        LoadMoveTrayToOutIng,
        /// <summary>Load: 將 Tray 移到 Out, 完成</summary>
        LoadMoveTrayToOutComplete,
        /// <summary>Load: 等待將 Box 放到 Tray 上</summary>
        LoadWaitingPutBoxOnTray,

        /// <summary>Load: 將 Tray 移往 Home,開始 </summary>
        LoadMoveTrayToHomeStart,
        /// <summary>Load: 將 Tray 移往 Home, 進行中</summary>
        LoadMoveTrayToHomeIng,
        /// <summary>Load: 將 Tray 移往 Home, 完成 </summary>
        LoadMoveTrayToHomeComplete,

        /// <summary>Load: 檢查Tray 上有没有 Box, 開始</summary>
        LoadCheckBoxExistenceStart,
        /// <summary>Load: 檢查Tray 上有没有 Box, 進行中</summary>
        LoadCheckBoxExistenceIng,
        /// <summary>Load: 檢查Tray 上有没有 Box, 結束</summary>
        LoadCheckBoxExistenceComplete,
        /// <summary>Load: 等待將 Tray 推向 In</summary>
        LoadWaitingMoveTrayToIn,

        /// <summary>Load: 將Trsy 回退到Out, 開始</summary>
        LoadRejectToOutStart,
        /// <summary>Load: 將Trsy 回退到Out, 進行中</summary>
        LoadRejectToOutIng,
        /// <summary>Load: 將Trsy 回退到Out, 完成</summary>
        LoadRejectToOutComplete, //LoadWaitingPutBoxOnTray,


        /*
        MustInitial,

        /// <summary>Initial, 開始</summary>
        InitialStart,
        /// <summary>Initial, 進行中</summary>
        InitialIng,
        /// <summary>Initial, 完成</summary>
        InitialComplete,
        /// <summary>將托盤移到Home, 開始</summary>
        MoveTrayToHomeStart,
        /// <summary>將托盤移到Home, 移動中</summary>
        MoveTrayToHomeIng,
        /// <summary>將托盤移到Home, 完成</summary>
        MoveTrayToHomeComplete,
        /// <summary>將托盤移到Out, 開始</summary>
        MoveTrayToOutStart,
        /// <summary>將托盤移到Out, 移動中 </summary>
        MoveTrayToOutIng,
        /// <summary>將托盤移到Out, 完成</summary>
        MoveTrayToOutComplete,
        /// <summary>將托盤移到In, 開始</summary>
        MoveTrayToInStart,
        /// <summary>將托盤移到In, 移動中</summary>
        MoveTrayToInIng,
        /// <summary>將托盤移到In, 完成</summary>
        MoveTrayToInComplete,
        */
    }
}
