using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.OpenStageStateMachineException
{
    public class OpenStagePLCWarningException : StateMachineExceptionBase
    {
        public OpenStagePLCWarningException(string message) : base(EnumStateMachineExceptionCode.OpenStagePLCWarningException, message)
        {

        }
        public OpenStagePLCWarningException() : this("")
        {

        }
    }
}
