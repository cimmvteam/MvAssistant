using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.BoxTransferStateMachineException
{
    public class BoxTransferPathMoveFailException : StateMachineExceptionBase
    {
        public BoxTransferPathMoveFailException(string message) : base(EnumStateMachineExceptionCode.BoxTransferPathMoveFailException, message)
        {

        }
        public BoxTransferPathMoveFailException() : this("")
        {

        }
    }
}
