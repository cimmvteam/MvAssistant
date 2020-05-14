using MvLib.StateMachine;
using MvLib.StateMachine.SmExp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.StateOp
{
    public class MacExceptionState : ExceptionState
    {
        //public List<StateJobJudgeSpec>

        public MacExceptionState() { }
        public MacExceptionState(string stateName)
            : base(stateName)
        {
            OnEntry += SECSCommucation;
        }

        public MacExceptionState(StateMachine sm, string stateName)
            : base(sm, stateName)
        {
            OnEntry += SECSCommucation;
        }

        public MacExceptionState(string name, IStateErrorInfo ex)
            : base(name)
        {
            // TODO: Complete member initialization
            this.ReasonOfState = ex;
        }  
    }
}
