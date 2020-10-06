using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineAlpha.SmExp
{
    public enum EnumAlarmAction
    {
        /// <summary>
        /// 機台正常動作
        /// </summary>
        None = 1,
        /// <summary>
        /// 計畫裡機台Warning (e.q. Sensor報值異常, 但不應直接使機台暫停, 會影響產能)
        /// </summary>
        Warning = 1000,
        /// <summary>
        /// 計畫裡的暫停異常
        /// </summary>
        Pause = 2000,
        /// <summary>
        /// 計畫裡的停機異常
        /// </summary>
        Assist = 3000,
        /// <summary>
        /// 計畫裡的停機異常
        /// </summary>
        ToolAssist = 4000,

        /// <summary>
        /// 計畫外, 機台設計可能出問題, 需請CIM確認
        /// </summary>
        WaitCIM = 9000,
    }
}

