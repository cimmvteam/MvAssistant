using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.CabinetDrawer
{
    public enum EnumMacCabinetDrawerState
    {
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
    }
}
