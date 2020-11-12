using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.OpenStage
{
    public enum EnumMacOpenStageCmd
    {
        /// <summary> 狀態機啟動 </summary>
        SystemBootup,
        /// <summary> Open Stage 初始化 </summary>
        Initial,
        /// <summary> 等待放入 Box(內無Mask) </summary>
        InputBox,
        /// <summary> 放入Box後，校正 Box 的位置(內無Mask)，等待 Unlock </summary>
        CalibrationClosedBox,
        /// <summary> Unlock 後，開啟 Box(內無Mask) </summary>
        OpenBox,
        /// <summary> 原先要放入 Mask ，但沒有放入 Mask ，關上 Box(內無Mask)，等待 Lock </summary>
        ReturnCloseBox,
        /// <summary> 關上 Box(內有Mask)，等待 Lock </summary>
        CloseBoxWithMask,
        /// <summary> Lock 後，停止吸真空固定 Box ，等待取走Box(內有Mask) </summary>
        ReleaseBoxWithMask,
        /// <summary> 取走 Box(內有Mask)後，將狀態改為Idle </summary>
        ReturnToIdleAfterReleaseBoxWithMask,


        /// <summary> 等待放入 Box(內有Mask) </summary>
        InputBoxWithMask,
        /// <summary> 放入Box後，校正 Box 的位置(內有Mask)，等待 Unlock </summary>
        CalibrationClosedBoxWithMask,
        /// <summary> Unlock 後，開啟 Box(內有Mask) </summary>
        OpenBoxWithMask,
        /// <summary> 原先要取出 Mask ，但又將 Mask 放回 Box 內，關上 Box(內有Mask)，等待 Lock </summary>
        ReturnCloseBoxWithMask,
        /// <summary> 關上 Box(內無Mask)，等待 Lock </summary>
        CloseBox,
        /// <summary> Lock 後，停止吸真空固定 Box ，等待取走Box(內無Mask) </summary>
        ReleaseBox,
        /// <summary> 取走 Box(內無Mask)後，將狀態改為Idle </summary>
        ReturnToIdleAfterReleaseBox,
    }
}
