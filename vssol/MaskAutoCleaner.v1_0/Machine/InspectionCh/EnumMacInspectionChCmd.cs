using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.InspectionCh
{
    public enum EnumMacInspectionChCmd
    {
        /// <summary> 狀態機啟動 </summary>
        SystemBootup,
        /// <summary> Inspection Chamber初始化 </summary>
        Initial,

        /// <summary> 檢測Pellicle </summary>
        InspectPellicle,
        /// <summary> Mask被取出後將狀態改為Idle ( 必須先由Mask Transfer取出Mask ) </summary>
        ReturnToIdleAfterReleasePellicle,

        /// <summary> 檢測Glass </summary>
        InspectGlass,
        /// <summary> Mask被取出後將狀態改為Idle ( 必須先由Mask Transfer取出Mask ) </summary>
        ReturnToIdleAfterReleaseGlass,
    }
}
