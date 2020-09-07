using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
    public class DrawerLoadInitialFailException : StateMachineExceptionBase
    {
        public DrawerLoadInitialFailException(string message):base(EnumStateMachineExceptionCode.DrawerLoadInitialFailException)
        {

        }
        public DrawerLoadInitialFailException() : this("")
        {

        }
    }
}
