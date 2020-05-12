using MaskAutoCleaner.Alarm;
using MaskAutoCleaner.Msg;
using MaskAutoCleaner.Spec;
using MaskAutoCleaner.MqttLike;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.StateJob
{
    public class StateJobJudgeSpec : StateJobBasic
    {
        public EnumAlarmId AlarmId;
        public EnumAlarmLevel AlarmLevel;
        public EnumAlarmLevel NeedPopupLevel = EnumAlarmLevel.OOC;
        public EnumAlarmLevel NeedThrowLevel = EnumAlarmLevel.Pass;
        public List<MqttSignal> LastSignals = new List<MqttSignal>(3);
        public int NgLimit = 1;
        public MacSpecItem SpecItem;
        public MacAlarmException ThrowEx;
        public MqttSubscriber Subscriber;




        public void ReceiveSignal(MqttSignal signal)
        {
            this.LastSignals.Add(signal);

            while (this.LastSignals.Count > this.NgLimit)
                this.LastSignals.RemoveAt(0);

            this.CheckLevel();
        }




        public EnumAlarmLevel CheckLevel()
        {

            var oocCnt = 0;
            var oosCnt = 0;
            foreach (var s in this.LastSignals)
            {
                if (this.SpecItem.IsOoc(s)) oocCnt++;
                if (this.SpecItem.IsOos(s)) oosCnt++;
            }

            var level = EnumAlarmLevel.None;
            if (oocCnt == this.NgLimit) level = EnumAlarmLevel.OOC;
            if (oosCnt == this.NgLimit) level = EnumAlarmLevel.OOS;

            this.AlarmLevel = level;
            return level;
        }




    }
}
