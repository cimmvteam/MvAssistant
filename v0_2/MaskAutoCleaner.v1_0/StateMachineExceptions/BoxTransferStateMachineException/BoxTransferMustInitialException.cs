using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.BoxTransferStateMachineException
{
   public  class BoxTransferMustInitialException:StateMachineExceptionBase
    {
        public BoxTransferMustInitialException(string message):base(EnumStateMachineExceptionCode.BoxTransferMustInitialException)
        {

        }
        public BoxTransferMustInitialException() : this("")
        {

        }
    }
}
