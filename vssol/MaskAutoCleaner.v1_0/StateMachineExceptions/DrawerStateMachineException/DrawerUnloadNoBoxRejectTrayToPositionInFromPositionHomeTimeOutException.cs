using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
   public class DrawerUnloadNoBoxRejectTrayToPositionInFromPositionHomeTimeOutException : StateMachineExceptionBase
    {
        public DrawerUnloadNoBoxRejectTrayToPositionInFromPositionHomeTimeOutException(string message) : base(EnumStateMachineExceptionCode.DrawerUnloadNoBoxRejectTrayToPositionInFromPositionHomeTimeOutException, message)
        {

        }
        public DrawerUnloadNoBoxRejectTrayToPositionInFromPositionHomeTimeOutException() : this("")
        {

        }
    }
}
