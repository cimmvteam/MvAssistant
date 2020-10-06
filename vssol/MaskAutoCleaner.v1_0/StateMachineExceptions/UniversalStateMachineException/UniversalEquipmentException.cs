using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.UniversalStateMachineException
{
    public class UniversalEquipmentException : StateMachineExceptionBase
    {
        public UniversalEquipmentException(string message) : base(EnumStateMachineExceptionCode.UniversalEquipmentException, message)
        {

        }
        public UniversalEquipmentException() : this("")
        {

        }
    }
}
