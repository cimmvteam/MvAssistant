using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
    public class DrawerInitialTimeOutException : StateMachineExceptionBase
    {
        public DrawerInitialTimeOutException(string message):base(EnumStateMachineExceptionCode.DrawerInitialTimeOutException)
        {

        }
        public DrawerInitialTimeOutException() : this("")
        {

        }
    }
}
