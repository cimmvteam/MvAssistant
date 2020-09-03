using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.OpenStageStateMachineException
{
    public class OpenStagePLCExecuteFailException : StateMachineExceptionBase
    {
        public OpenStagePLCExecuteFailException(string message) : base(EnumStateMachineExceptionCode.OpenStagePLCExecuteFailException, message)
        {

        }
        public OpenStagePLCExecuteFailException() : this("")
        {

        }
    }
}
