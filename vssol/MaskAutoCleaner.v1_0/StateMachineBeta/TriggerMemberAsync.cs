using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineBeta
{

    /// <summary>Transition Member for TriggerAsync</summary>
    public class TriggerMemberAsync : TriggerMemberBase
    {
        /// <summary>TriggerAsync 的 Guard</summary>
        /// <remarks>
        /// parameter: DateTime, 開始時間, 計算逾時用,
        /// return: bool
        /// </remarks>
        public Func<DateTime, bool> Guard { get; set; }
    }
}
