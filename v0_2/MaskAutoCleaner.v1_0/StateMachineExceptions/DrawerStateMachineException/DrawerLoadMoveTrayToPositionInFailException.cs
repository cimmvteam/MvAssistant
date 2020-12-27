using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
    public class DrawerLoadMoveTrayToPositionInFailException : StateMachineExceptionBase
    {
        public DrawerLoadMoveTrayToPositionInFailException(string message) : base(EnumStateMachineExceptionCode.DrawerLoadMoveTrayToPositionInFailException, message)
        {

        }
        public DrawerLoadMoveTrayToPositionInFailException() : this("")
        {

        }
    }
}
