using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    public enum EnumMacCabinetTransition
    {
       
      

        /// <summary>Load, 將 Drawer 的Tray 移到 Out 開始=> 移動中</summary>
        LoadMoveDrawerTraysToOutStart_LoadMoveDrawerTraysToOutIng,
        /// <summary>Load, 將 Drawer 的Tray 移到 Out 移動中=> 完成</summary>
        LoadMoveDrawerTraysToOutIng_LoadMoveDrawerTraysToOutComplete,
        /// <summary>Load, 將 Drawer 的Tray 移到 Out 完成=> 設為最末 State</summary>
        LoadMoveDrawerTraysToOutComplete_NULL,
        

        /// <summary>系統啟動後 Initial Drawer, 開始=> 動作中</summary>
        BootupInitialDrawersStart_BootupInitialDrawersIng,
        /// <summary>系統啟動後 Initial Drawer, 動作中=> 完成</summary>
        BootupInitialDrawersIng_BootupInitialDrawersComplete,
        /// <summary>系統啟動後 完成 Initial Drawer 完成</summary>
        BootupInitialDrawersComplete_NULL,
       

        /// <summary>同步所有 Drawer  的 State, 開始=> 同步中</summary>
        SynchronousDrawerStatesStart_SynchronousDrawerStatesIng,
        /// <summary>同步所有 Drawer  的 State, 同步中= 完成 </summary>
        SynchronousDrawerStatesIng_SynchronousDrawerStatesComplete,
        /// <summary>同步所有 Drawer  的 State:完成  </summary>
        SynchronousDrawerStatesComplete_NULL

    }
}
