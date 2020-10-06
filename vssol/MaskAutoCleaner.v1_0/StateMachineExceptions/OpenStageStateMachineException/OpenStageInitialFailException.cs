using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.OpenStageStateMachineException
{
    public class OpenStageInitialFailException : StateMachineExceptionBase
    {
        public OpenStageInitialFailException(string message) : base(EnumStateMachineExceptionCode.OpenStageInitialFailException, message)
        {

        }
        public OpenStageInitialFailException() : this("")
        {

        }
    }
}
