using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineException.BoxTransferStateMachineException
{
   public class BoxTransferInitialFailException:StateMachineExceptionBase
    {
        public BoxTransferInitialFailException(string message):base(EnumStateMachineExceptionCode.BoxTransferMustInitialException,message)
        {

        }
        public BoxTransferInitialFailException() : this("")
        {

        }
    }
}
