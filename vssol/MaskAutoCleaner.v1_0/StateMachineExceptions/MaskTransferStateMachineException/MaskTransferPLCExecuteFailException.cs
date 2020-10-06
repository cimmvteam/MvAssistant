using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.MaskTransferStateMachineException
{
    class MaskTransferPLCExecuteFailException : StateMachineExceptionBase
    {
        public MaskTransferPLCExecuteFailException(string message) : base(EnumStateMachineExceptionCode.MaskTransferPLCExecuteFailException, message)
        {

        }
        public MaskTransferPLCExecuteFailException() : this("")
        {

        }
    }
}
