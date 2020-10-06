using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
   public  class DrawerLoadNoBoxRejectTrayToPositionOutFromPositionHomeFailException : StateMachineExceptionBase
    {
        public DrawerLoadNoBoxRejectTrayToPositionOutFromPositionHomeFailException(string message) : base(EnumStateMachineExceptionCode.DrawerLoadNoBoxRejectTrayToPositionOutFromPositionHomeFailException, message)
        {

        }
        public DrawerLoadNoBoxRejectTrayToPositionOutFromPositionHomeFailException() : this("")
        {

        }
    }
}
