using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.StateMachine_v1_0
{
    public class TransitionEvaluation
    {
        public Enum transName;
        public Func<bool> transBool;

        public TransitionEvaluation(Enum transName, Func<bool> transBool)
        {
            this.transName = transName;
            this.transBool = transBool;
        }
    }
}
