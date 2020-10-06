using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.MaskTransferStateMachineException
{
    public class MaskTransferException : StateMachineExceptionBase
    {
        public MaskTransferException(string message) : base(EnumStateMachineExceptionCode.MaskTransferException, message)
        {

        }
        public MaskTransferException() : this("")
        {

        }
    }
}
