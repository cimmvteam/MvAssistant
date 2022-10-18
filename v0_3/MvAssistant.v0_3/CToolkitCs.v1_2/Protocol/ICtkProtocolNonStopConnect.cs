using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Protocol
{
    public interface ICtkProtocolNonStopConnect : ICtkProtocolConnect
    {
        /// <summary>
        /// 檢查連線狀態的間隔
        /// </summary>
        int IntervalTimeOfConnectCheck { get; set; }
        /// <summary>
        /// 是否開始非停執行
        /// </summary>
        bool IsNonStopRunning { get; }
        /// <summary>
        /// 中斷非停執行
        /// </summary>
        void NonStopRunStop();
        /// <summary>
        /// 非同步的非停連線
        /// </summary>
        void NonStopRunStart();
    }
}
