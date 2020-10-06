using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
   public  class DrawerUnloadNoBoxRejectTrayToPositionInFromPositionHomeFailException : StateMachineExceptionBase
    {
        public DrawerUnloadNoBoxRejectTrayToPositionInFromPositionHomeFailException(string message) : base(EnumStateMachineExceptionCode.DrawerUnloadNoBoxRejectTrayToPositionInFromPositionHomeFailException, message)
        {

        }
        public DrawerUnloadNoBoxRejectTrayToPositionInFromPositionHomeFailException() : this("")
        {

        }
    }
}
