using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.MaskTransferStateMachineException
{
    public class MaskTransferPathMoveFailException : StateMachineExceptionBase
    {
        public MaskTransferPathMoveFailException(string message) : base(EnumStateMachineExceptionCode.MaskTransferPathMoveFailException, message)
        {

        }
        public MaskTransferPathMoveFailException() : this("")
        {

        }
    }
}
