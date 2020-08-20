using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
    public class LoadportUndockTimeOutException : StateMachineExceptionBase
    {
        public LoadportUndockTimeOutException(string message) : base(EnumStateMachineExceptionCode.LoadportUndockTimeOutException, message)
        {

        }
        public LoadportUndockTimeOutException() : this("")
        {

        }
    }
}
