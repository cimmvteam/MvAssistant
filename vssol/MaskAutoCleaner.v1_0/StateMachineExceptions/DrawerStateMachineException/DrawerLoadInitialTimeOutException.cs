using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
    public class DrawerLoadInitialTimeOutException : StateMachineExceptionBase
    {
        public DrawerLoadInitialTimeOutException(string message) : base(EnumStateMachineExceptionCode.DrawerLoadInitialTimeOutException, message)
        {

        }
        public DrawerLoadInitialTimeOutException() : this("")
        {

        }
    }
    {
    }
}
