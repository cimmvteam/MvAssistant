using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.CleanCh
{
    public enum EnumMacCleanChCmd
    {
        /// <summary> 狀態機啟動 </summary>
        SystemBootup,
        /// <summary> 清理Pellicle </summary>
        CleanPellicle,
        /// <summary> 停止/結束清理Pellicle </summary>
        FinishCleanPellicle,
        /// <summary> 檢測Pellicle </summary>
        InspectPellicle,
        /// <summary> 停止/結束檢測Pellicle </summary>
        FinishInspectPellicle,

        /// <summary> 清理Glass </summary>
        CleanGlass,
        /// <summary> 停止/結束清理Glass </summary>
        FinishCleanGlass,
        /// <summary> 檢測Glass </summary>
        InspectGlass,
        /// <summary> 停止/結束檢測Glass </summary>
        FinishInspectGlass,
    }
}
