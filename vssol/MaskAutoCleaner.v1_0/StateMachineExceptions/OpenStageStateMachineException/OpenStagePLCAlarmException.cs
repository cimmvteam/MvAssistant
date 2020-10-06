using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.OpenStageStateMachineException
{
    public class OpenStagePLCAlarmException : StateMachineExceptionBase
    {
        public OpenStagePLCAlarmException(string message) : base(EnumStateMachineExceptionCode.OpenStageException, message)
        {

        }
        public OpenStagePLCAlarmException() : this("")
        {

        }
    }
}
