using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
   public class DrawerLoadMoveTrayToPositionHomeFailException : StateMachineExceptionBase
    {
        public DrawerLoadMoveTrayToPositionHomeFailException(string message):base(EnumStateMachineExceptionCode.DrawerLoadMoveTrayToPositionHomeFailException)
        {

        }
        public DrawerLoadMoveTrayToPositionHomeFailException() : this("")
        {

        }
    }
}
