using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
    class DrawerLoadCheckBoxExistanceAtPositionHomeTimeOutException : StateMachineExceptionBase
    {
        public DrawerLoadCheckBoxExistanceAtPositionHomeTimeOutException(string message) : base(EnumStateMachineExceptionCode.DrawerLoadCheckBoxExistanceAtPositionHomeTimeOutException, message)
        {

        }
        public DrawerLoadCheckBoxExistanceAtPositionHomeTimeOutException() : this("")
        {

        }
    }
}
