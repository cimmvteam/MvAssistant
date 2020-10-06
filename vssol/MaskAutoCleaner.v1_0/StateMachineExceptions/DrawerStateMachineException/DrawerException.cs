using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
   public  class DrawerException:StateMachineExceptionBase
    {
        public DrawerException(string message) : base(EnumStateMachineExceptionCode.DrawerException, message)
        {

        }
        public DrawerException() : this("")
        {

        }
    }
}
