using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaskAutoCleaner.StateMachine_v1_0
{
    public class StateParameter : IStateParam
    {
        public string AlarmId { get; set; }
        public string TransName { get; set; }
        public StateParameter() { }
        public StateParameter(string TransName)
        {
            this.TransName = TransName;
        }

        public StateParameter(string TransName ,string alarmId)
        {
            this.TransName = TransName;
            this.AlarmId = alarmId;
        }
    }
}
