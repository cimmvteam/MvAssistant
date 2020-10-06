using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.OpenStageStateMachineException
{
    public class OpenStageException : StateMachineExceptionBase
    {
        public OpenStageException(string message) : base(EnumStateMachineExceptionCode.OpenStageException, message)
        {

        }
        public OpenStageException() : this("")
        {

        }
    }
}