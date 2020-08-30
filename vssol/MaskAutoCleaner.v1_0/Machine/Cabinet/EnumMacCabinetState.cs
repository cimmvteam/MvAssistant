using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    public enum EnumMacCabinetState
    {
        /// <summary>空等</summary>
        Idle,
        /// <summary>接收Load 指令</summary>
        WaitingAcceptLoad,
        /// <summary>將 Tray 移到 Out~開始</summary>
        MoveDrawerTraysToOutStart,
        /// <summary>將 Tray 移到 Out ~ 進行中 </summary>
        MoveDrawerTraysToOutIng,
        /// <summary>所有Drawer完成動作</summary>
        /// <remarks>包含完成退到 Out 及發生例外的 Drawer Tray</remarks>
        MoveDrawerTraysToOutComplete,

        /// <summary>Initial全部Drawer~ 開始</summary>
        InitialAllDrawerStart,
        /// <summary>Initial全部Drawer~ 進行中</summary>
        InitialAllDrawerIng,
        /// <summary>Initial全部Drawer~ 完成</summary>
        /// <remarks>包含正常完成Initial 的 Drawer, 及發生例外的 Drawer</remarks>
        InitialAllDrawerComplete,

        
    }
}
