using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.MaskTransferStateMachineException
{
    public class MaskTransferPLCWarningException : StateMachineExceptionBase
    {
        public MaskTransferPLCWarningException(string message) : base(EnumStateMachineExceptionCode.MaskTransferPLCWarningException, message)
        {

        }
        public MaskTransferPLCWarningException() : this("")
        {

        }
    }
}
