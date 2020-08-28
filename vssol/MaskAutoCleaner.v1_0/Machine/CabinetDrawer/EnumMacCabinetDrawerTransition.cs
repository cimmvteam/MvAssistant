using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.CabinetDrawer
{
  public   enum EnumMacCabinetDrawerTransition
  {
        /// <summary>Initial: 開始 => 移動中</summary>
        InitialStart_InitialIng,
        /// <summary>Initial: 移動中 => 完成</summary>
        InitialIng_InitialComplete,
        /// <summary>Initial: 完成 => 結束</summary>
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
  }
}
