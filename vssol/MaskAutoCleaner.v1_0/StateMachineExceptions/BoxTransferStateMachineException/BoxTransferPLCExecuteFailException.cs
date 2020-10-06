using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.BoxTransferStateMachineException
{
    public class BoxTransferPLCExecuteFailException : StateMachineExceptionBase
    {
        public BoxTransferPLCExecuteFailException(string message) : base(EnumStateMachineExceptionCode.BoxTransferPLCExecuteFailException, message)
        {

        }
        public BoxTransferPLCExecuteFailException() : this("")
        {

        }
    }
}
