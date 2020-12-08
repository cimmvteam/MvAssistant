using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.BoxTransferStateMachineException
{
    public class BoxTransferPLCWarningException : StateMachineExceptionBase
    {
        public BoxTransferPLCWarningException(string message) : base(EnumStateMachineExceptionCode.BoxTransferPLCWarningException, message)
        {

        }
        public BoxTransferPLCWarningException() : this("")
        {

        }
    }
}
