using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineException.DrawerStateMachineException
{
   public  class DrawerMustResetException:StateMachineExceptionBase
    {
        public DrawerMustResetException(string message) : base(EnumStateMachineExceptionCode.DrawerMustResetException, message)
        {

        }
        public DrawerMustResetException() : this("")
        {

        }
    }
}
