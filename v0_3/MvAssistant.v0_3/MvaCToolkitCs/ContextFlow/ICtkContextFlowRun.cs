using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvaCToolkitCs.v1_2.ContextFlow
{
    /// <summary>
    /// 建議:
    ///     RunLoop call RunOnce call RunSelf(自定)
    ///     RunLoopStart call RunSelf(自定)
    /// </summary>
    public interface ICtkContextFlowRun : ICtkContextFlow
    {
        /*[d20210714] RunOnce, RunLoop 以及 RunLoopStart.
         * 在介面中只定義各Method存在, 宣告該Method的用途
         * 但實際用途是實作者決定, 實作者應自行注意如何符合定義.
         */

        bool CtkCfIsRunning { get; set; }

        /// <summary>
        /// 會執行一次特定功能的method
        /// Exec: 執行特定功能, 若有需要, 可自行重複執行此作業
        /// </summary>
        /// <returns></returns>
        int CtkCfRunOnce();

        /// <summary>
        /// 會持續執行特定功能的method
        /// Run: 持續跑下去, 被呼叫後會留在這個method直到結束
        /// 若不做事, 請直接return
        /// </summary>
        /// <returns></returns>
        int CtkCfRunLoop();

        /// <summary>
        /// 會持續執行特定功能的method
        /// 需實作非同步作業, e.q. 開啟一個Thread/Task後離開函式
        /// </summary>
        /// <returns></returns>
        int CtkCfRunLoopStart();
    }
}
