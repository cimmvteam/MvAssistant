using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.BoxTransferStateMachineException
{
   public  class BoxTransferInitialTimeOutException:StateMachineExceptionBase
    {
        public BoxTransferInitialTimeOutException( string message):base(EnumStateMachineExceptionCode.BoxTransferInitialTimeOutException,message)
        {

        }
        public BoxTransferInitialTimeOutException() : this("")
        {

        }
    }
}
