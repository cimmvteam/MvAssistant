using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineAlpha.SmExp
{
    public interface IStateErrorInfo
    {
        Enum StateAlarmId { get; set; }
        EnumAlarmAction StateAlarmAction { get; set; }
        Enum StateAlarmLevel { get; set; }
    }
}
