using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.BoxTransferStateMachineException
{
   public class BoxTransferMustResetException:StateMachineExceptionBase
    {
        public  BoxTransferMustResetException(string message) : base(EnumStateMachineExceptionCode.BoxTransferMustResetException,message)
        {

        }
        public BoxTransferMustResetException():this("")
        {

        }
   }
}
