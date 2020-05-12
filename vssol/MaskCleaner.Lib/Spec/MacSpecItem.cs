using MaskAutoCleaner.Msg;
using MaskAutoCleaner.MqttLike;
using MvLib.TaskDispatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Spec
{

    [TaskPlan(DispatchTime = "20181211", Sponsor = EnumSponsor.MHTSAIZW, Version = "0.1.0", TaskType = EnumTaskType.Enhance,
        Name = "Spec", Description = "Spec type使用數值型別", Status = EnumTaskStatus.OPEN)]
    public class MacSpecItem
    {


        public MqttSignal OocLower;
        public MqttSignal OocUpper;
        public MqttSignal OosLower;
        public MqttSignal OosUpper;




        public bool IsOoc(MqttSignal val)
        {
            if (val.CompareTo(this.OocLower) <= 0 || val.CompareTo(OocUpper) >= 0) return true;
            return false;
        }
        public bool IsOos(MqttSignal val) 
        {
            if (val.CompareTo(this.OosLower) <= 0 || val.CompareTo(OosUpper) >= 0) return true;
            return false;
        }



        public EnumAlarmLevel CheckLevel(MqttSignal val)
        {
            if (this.IsOos(val)) return EnumAlarmLevel.OOS;
            if (this.IsOoc(val)) return EnumAlarmLevel.OOC;
            return EnumAlarmLevel.None;
        }





        public static MacSpecItem Create(MqttSignal cl, MqttSignal cu, MqttSignal sl, MqttSignal su)
        {
            var result = new MacSpecItem();
            result.OocLower = cl;
            result.OocUpper = cu;
            result.OosLower = sl;
            result.OosUpper = su;
            return result;
        }





    }
}
