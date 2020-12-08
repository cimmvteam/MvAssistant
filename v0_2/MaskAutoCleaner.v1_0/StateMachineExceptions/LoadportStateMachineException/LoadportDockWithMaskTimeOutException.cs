using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
    class LoadportDockWithMaskTimeOutException : StateMachineExceptionBase
    {
        public LoadportDockWithMaskTimeOutException(string message) : base(EnumStateMachineExceptionCode.LoadportDockWithMaskTimeOutException, message)
        {

        }
        public LoadportDockWithMaskTimeOutException() : this("")
        {

        }
    }
}
