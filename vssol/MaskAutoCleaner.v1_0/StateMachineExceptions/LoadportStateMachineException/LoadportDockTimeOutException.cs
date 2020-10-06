using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
    public class LoadportDockTimeOutException : StateMachineExceptionBase
    {
        public LoadportDockTimeOutException(string message) : base(EnumStateMachineExceptionCode.LoadportDockTimeOutException, message)
        {

        }
        public LoadportDockTimeOutException() : this("")
        {

        }
    }
}
