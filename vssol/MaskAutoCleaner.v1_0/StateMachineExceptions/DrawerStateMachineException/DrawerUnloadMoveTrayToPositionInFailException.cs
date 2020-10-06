using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
   public  class DrawerUnloadMoveTrayToPositionInFailException : StateMachineExceptionBase
    {
        public DrawerUnloadMoveTrayToPositionInFailException(string message) : base(EnumStateMachineExceptionCode.DrawerUnloadMoveTrayToPositionInFailException, message)
        {

        }
        public DrawerUnloadMoveTrayToPositionInFailException() : this("")
        {

        }
    }
}
