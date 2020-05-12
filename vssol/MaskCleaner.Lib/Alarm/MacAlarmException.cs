using MaskAutoCleaner.Msg;
using MvLib.StateMachine.SmExp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Alarm
{
    //mhtsaizw 改把 BASE接到底層 而不是MacExceptionBase
    public class MacAlarmException : MacExcpetion, IStateErrorInfo
    {
        public EnumAlarmId AlarmId = EnumAlarmId.DotNetException;
        public EnumAlarmLevel AlarmLevel = EnumAlarmLevel.OOS;
        public MacAlarmException() { }
        public MacAlarmException(EnumAlarmId id) { this.AlarmId = id; }
        public MacAlarmException(EnumAlarmId id, EnumAlarmLevel level) { this.AlarmId = id; this.AlarmLevel = level; }
        public MacAlarmException(string message) : base(message) { }
        public MacAlarmException(EnumAlarmId id, Exception innerException) : base(id.ToString(), innerException) { }

        public EnumAlarmAction StateAlarmAction { get; set; }
        Enum IStateErrorInfo.StateAlarmId { get { return this.AlarmId; } set { this.AlarmId = (EnumAlarmId)value; } }
        Enum IStateErrorInfo.StateAlarmLevel { get { return this.AlarmLevel; } set { this.AlarmLevel = (EnumAlarmLevel)value; } }
        public static MacAlarmException GetOrCreate(Exception ex)
        {
            var macex = ex as MacAlarmException;
            if (macex != null) return macex;
            return new MacAlarmException(EnumAlarmId.DotNetException, ex);
        }
        public static MacAlarmException GetOrCreate(EnumAlarmId alarmId)
        {
            return new MacAlarmException(EnumAlarmId.DotNetException);
        }


    }
}
