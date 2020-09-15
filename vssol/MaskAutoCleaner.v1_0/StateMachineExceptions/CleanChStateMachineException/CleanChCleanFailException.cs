using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.CleanChStateMachineException
{
    public class CleanChCleanFailException : StateMachineExceptionBase
    {
        public CleanChCleanFailException(string message) : base(EnumStateMachineExceptionCode.CleanChCleanFailException, message)
        {

        }
        public CleanChCleanFailException() : this("")
        {

        }
    }
}
