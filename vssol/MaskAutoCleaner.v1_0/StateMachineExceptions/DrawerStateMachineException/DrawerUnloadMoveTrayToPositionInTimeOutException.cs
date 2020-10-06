using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
   public class DrawerUnloadMoveTrayToPositionInTimeOutException : StateMachineExceptionBase
    {
        public DrawerUnloadMoveTrayToPositionInTimeOutException(string message) : base(EnumStateMachineExceptionCode.DrawerUnloadMoveTrayToPositionInTimeOutException, message)
        {

        }
        public DrawerUnloadMoveTrayToPositionInTimeOutException() : this("")
        {

        }
    }
   
}
