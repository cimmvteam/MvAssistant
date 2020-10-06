using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineAlpha
{
    public interface IStateMachineAlarmHandler
    {
        void ProcAlarm(Object sender , Enum enumAlarm, string alarmId);
    }
}
