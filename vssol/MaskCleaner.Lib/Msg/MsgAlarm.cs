using MvLib.StateMachine;
using MvLib.StateMachine.SmExp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Msg
{

    public class MsgAlarm : MsgBase
    {
        //Assign in map period
        public EnumAlarmAction Action;

        public EnumAlarmId AlarmId;

        public EnumAlarmLevel AlarmLevel;

        /// <summary>
        /// 故障說明, 簡易排除
        /// </summary>
        public string Description;

        public Exception Exception;

        public string Message;

        public DateTime RecordTime;

        //Risk
        public MsgRisk Risk;


        public string GetExceptionStackTrace() { return this.Exception == null ? null : this.Exception.StackTrace; }


    }
}
