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
        public Func<DateTime, bool> Guard { get; set; }
    }
}
