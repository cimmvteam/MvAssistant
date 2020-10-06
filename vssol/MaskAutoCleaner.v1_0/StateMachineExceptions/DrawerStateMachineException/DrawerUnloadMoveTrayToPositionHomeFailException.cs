using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
   public class DrawerUnloadMoveTrayToPositionHomeFailException : StateMachineExceptionBase
    {
        public DrawerUnloadMoveTrayToPositionHomeFailException(string message) : base(EnumStateMachineExceptionCode.DrawerUnloadMoveTrayToPositionHomeFailException, message)
        {

        }
        public DrawerUnloadMoveTrayToPositionHomeFailException() : this("")
        {

        }
    }
}
