using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.CleanChStateMachineException
{
    public class CleanChException : StateMachineExceptionBase
    {
        public CleanChException(string message) : base(EnumStateMachineExceptionCode.CleanChException, message)
        {

        }
        public CleanChException() : this("")
        {

        }
    }
}
