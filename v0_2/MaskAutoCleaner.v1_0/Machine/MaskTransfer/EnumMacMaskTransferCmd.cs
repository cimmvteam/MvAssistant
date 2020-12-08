using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.MaskTransfer
{
    public enum EnumMacMaskTransferCmd
    {
        /// <summary> 狀態機啟動 </summary>
        SystemBootup,
        /// <summary> Mask Transfer初始化 </summary>
        Initial,
        /// <summary> 從 LP Home 到 Load Port A 夾取 Mask 並返回 LP Home </summary>
        LPHomeToLPAGetMaskReturnToLPHomeClamped,
        /// <summary> 從 LP Home 到 Load Port B 夾取 Mask 並返回 LP Home </summary>
        LPHomeToLPBGetMaskReturnToLPHomeClamped,
        /// <summary> 從 LP Home 到 Open Stage 夾取 Mask 並返回 LP Home </summary>
        LPHomeToOSGetMaskReturnToLPHomeClamped,
        /// <summary> 從 LP Home 轉向到 IC Home(夾著Mask) </summary>
        LPHomeClampedToICHomeClamped,
        /// <summary> 從 LP Home 轉向到 IC Home(不夾Mask) </summary>
        LPHomeToICHome,
        /// <summary> 從 IC Home 轉向到 LP Home(不夾Mask) </summary>
        ICHomeToLPHome,
        /// <summary> 從 IC Home 夾著 Mask 放入 Inspection Chamber(Pellicle面向上) </summary>
        ICHomeClampedToICPellicleReleaseReturnToICHome,
        /// <summary> 從 IC Home 到 Inspection Chamber 取出 Mask(Pellicle面向上) </summary>
        ICHomeToICPellicleGetReturnToICClamped,
        /// <summary> 從 IC Home 夾著 Mask 放入 Inspection Chamber(Glass面向上) </summary>
        ICHomeClampedToICGlassReleaseReturnToICHome,
        /// <summary> 從 IC Home 到 Inspection Chamber 取出 Mask(Glass面向上) </summary>
        ICHomeToICGlassGetReturnToICClamped,
        /// <summary> 將 IC Home 夾著 Mask 的狀態轉成 IC Home 夾著 Mask 並且兩面都完成檢測 </summary>
        ICHomeClampedToICHomeInspected,
        /// <summary> 在 IC Home 夾著 Mask 並且兩面都完成檢測後，需要清潔 Mask ，轉向到 CC Home </summary>
        ICHomeInspectedToCCHomeClamped,
        /// <summary> 從 CC Home 夾著 Mask 進入 Clean Chamber(Pellicle面向下) </summary>
        CCHomeClampedToCCPellicle,
        /// <summary> 在 Clean Chamber 內夾著 Mask ，移動到 Air Gun 上方(Pellicle面向下) </summary>
        InCCPellicleMoveToClean,
        /// <summary> 開始進行清理Pellicle的動作 </summary>
        CleanPellicle,
        /// <summary> Pellicle 清理完回到 Clean Chamber 內的起始點 </summary>
        CCPellicleCleanedReturnInCCPellicle,
        /// <summary> 在 Clean Chamber 內夾著 Mask ，移動到 Camera 上方(Pellicle面向下) </summary>
        InCCPellicleMoveToInspect,
        /// <summary> 開始進行檢測Pellicle的動作 </summary>
        InspectPellicle,
        /// <summary> Pellicle 檢測完回到 Clean Chamber 內的起始點 </summary>
        CCPellicleInspectedReturnInCCPellicle,
        /// <summary> 從 Clean Cjamber 內(Pellecle面向下)，夾著 Mask 回到 CC Home </summary>
        InCCPellicleToCCHomeClamped,
        /// <summary> 從 CC Home 夾著 Mask 進入 Clean Chamber(Glass面向下) </summary>
        CCHomeClampedToCCGlass,
        /// <summary> 在 Clean Chamber 內夾著 Mask ，移動到 Air Gun 上方(Glass面向下) </summary>
        InCCGlassMoveToClean,
        /// <summary> 開始進行清理Glass的動作 </summary>
        CleanGlass,
        /// <summary> Glass 清理完回到 Clean Chamber 內的起始點 </summary>
        CCGlassCleanedReturnInCCGlass,
        /// <summary> 在 Clean Chamber 內夾著 Mask ，移動到 Camera 上方(Glass面向下) </summary>
        InCCGlassMoveToInspect,
        /// <summary> 開始進行檢測Glass的動作 </summary>
        InspectGlass,
        /// <summary> Glass 檢測完回到 Clean Chamber 內的起始點 </summary>
        CCGlassInspectedReturnInCCGlass,
        /// <summary> 從 Clean Cjamber 內(Glass面向下)，夾著 Mask 回到 CC Home </summary>
        InCCGlassToCCHomeClamped,
        /// <summary> 將 CC Home 夾著 Mask 的狀態轉成 CC Home 夾著 Mask 並且完成清潔 </summary>
        CCHomeClampedToCCHomeCleaned,
        /// <summary> 在 IC Home 夾著 Mask 並且兩面都完成檢測，不用清潔直接轉向到 LP Home </summary>
        ICHomeInspectedToLPHomeInspected,
        /// <summary> 在 CC Home 夾著 Mask 並且完成清潔，轉向到 LP Home </summary>
        CCHomeCleanedToLPHomeCleaned,
        /// <summary> 從 LP Home 將未經過檢查的 Mask 放到 Open Stage，回到 LP Home </summary>
        LPHomeClampedToOSReleaseMaskReturnToLPHome,
        /// <summary> 從 LP Home 將已經檢測過，不需清理的 Mask 放到 Load Port A </summary>
        LPHomeInspectedToLPARelease,
        /// <summary> 從 LP Home 將已經檢測過，不需清理的 Mask 放到 Load Port B </summary>
        LPHomeInspectedToLPBRelease,
        /// <summary> 從 LP Home 將已經檢測過，不需清理的 Mask 放到 Open Stage </summary>
        LPHomeInspectedToOSRelease,
        /// <summary> 從 LP Home 將已經清理過的 Mask 放到 Load Port A </summary>
        LPHomeCleanedToLPARelease,
        /// <summary> 從 LP Home 將已經清理過的 Mask 放到 Load Port B </summary>
        LPHomeCleanedToLPBRelease,
        /// <summary> 從 LP Home 將已經清理過的 Mask 放到 Open Stage </summary>
        LPHomeCleanedToOSRelease,
        /// <summary> 從 LP Home 移動到 BarcodeReader </summary>
        LPHomeToBarcodeReader,
        /// <summary> 從 BarcodeReader移動到 LP Home </summary>
        BarcodeReaderToLPHome,
        /// <summary> 從 IC Home 移動到變形檢測裝置 </summary>
        ICHomeToInspDeform,
        /// <summary> 從變形檢測裝置移動到 IC Home </summary>
        InspDeformToICHome,
    }
}

