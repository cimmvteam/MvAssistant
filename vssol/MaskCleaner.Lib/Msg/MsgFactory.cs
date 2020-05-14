using MaskAutoCleaner.Alarm;
using MaskAutoCleaner.Msg.PrescribedJobNotify;
using MvLib.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Msg
{
    public class MsgFactory
    {

        public static MsgAlarm CreateAlarm(Object sender, EnumAlarmId alarmId)
        {
            var msg = new MsgAlarm();
            msg.AlarmId = alarmId;
            msg.RecordTime = DateTime.Now;
            msg.Sender = sender;
            return msg;
        }
        public static MsgAlarm CreateAlarm(Object sender, Exception ex)
        {
            var msg = new MsgAlarm();
            var mac = ex as MacAlarmException;
            if (mac == null)
                mac = new MacAlarmException(EnumAlarmId.DotNetException, ex);

            msg.AlarmId = mac.AlarmId;
            msg.Exception = mac;
            msg.RecordTime = DateTime.Now;
            msg.Sender = sender;
            return msg;
        }


        public static MsgAlarm CreateAlarm(Object sender, EnumAlarmId alarmId, EnumAlarmLevel alarmLevel)
        {
            var msg = new MsgAlarm();
            msg.AlarmId = alarmId;
            msg.AlarmLevel = alarmLevel;
            msg.RecordTime = DateTime.Now;
            msg.Sender = sender;
            return msg;
        }

        public static MsgSecsCeid CreateSecsCeid(Object sender, EnumCeid ceid)
        {
            var msg = new MsgSecsCeid();
            msg.Ceid = ceid;
            msg.RecordTime = DateTime.Now;
            msg.Sender = sender;
            return msg;
        }
        public static MsgJobNotify CreateJobNotify(Object sender, EnumJobNotify jn, PrescribedJobNotifyBase jobNotify = null)
        {
            var msg = new MsgJobNotify();
            msg.JobNotify = jn;
            msg.RecordTime = DateTime.Now;
            msg.Sender = sender;
            msg.PrescribedJobNotify = jobNotify;
            return msg;
        }



        public MsgDeviceCmd CreateDeviceCmd() { throw new NotImplementedException(); }

        public MsgSecs CreateSecs() { throw new NotImplementedException(); }

        public MsgRecipe CreateTriggerSignal() { throw new NotImplementedException(); }
    }
}
