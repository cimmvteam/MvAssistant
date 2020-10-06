using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.BoxTransferStateMachineException
{
    public class BoxTransferException: StateMachineExceptionBase
    {
        public BoxTransferException(string message) :base (EnumStateMachineExceptionCode.BoxTransferException,message)
        {

        }
        public BoxTransferException() : this("")
        {

        }
    }
}
