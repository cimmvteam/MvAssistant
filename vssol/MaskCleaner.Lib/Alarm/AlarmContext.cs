using MaskAutoCleaner.Context;
using MaskAutoCleaner.Msg;
using MvLib.StateMachine.SmExp;
using MvLib.TaskDispatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
namespace MaskAutoCleaner.Alarm
{

    public class AlarmContext : MacContextBase
    {
        public Dictionary<Msg.EnumAlarmId, AlarmActionInfo> mapTable = new Dictionary<Msg.EnumAlarmId, AlarmActionInfo>();
        private MsgAlarm alarmMsg;

        private int timeOutSpec = 10;

        private string xmlFilePath = @"UserData\AlarmSetting\AlarmSetting.xml";

        public AlarmContext()
        {
        }

        public MsgAlarm AlarmMsg
        {
            get { return alarmMsg; }
            set { alarmMsg = value; }
        }
        public int TimeOutSpec
        {
            get { return timeOutSpec; }
            set { timeOutSpec = value; }
        }

        public string XmlFilePath
        {
            get { return xmlFilePath; }
            set { xmlFilePath = value; }
        }
        public void LoadAlarmMap()
        {
            mapTable.Clear();

            if (!SchemaValidation.ValidationSchemaNow(xmlFilePath, @"UserData\AlarmSetting\AlarmSetting.xsd"))
            {
                throw new XmlException("Xml's architecture error");
            }

            XElement root = XElement.Load(xmlFilePath);
            if (root.Name != "AlarmSetting")
                throw new XmlException("Xml's root name error");


            foreach (string x in Enum.GetNames(typeof(EnumAlarmId)))
            {
                var p1 = from el in root.Elements("Alarm")
                         where (string)el.Attribute("ID") == x
                         select el;

                if (root.Elements("Alarm").Where(z => z.Attribute("ID").Value.Equals(x, StringComparison.InvariantCultureIgnoreCase)).Count() == 0)

                foreach (var y in p1)
                {
                    if (!Enum.IsDefined(typeof(EnumAlarmAction), y.Element("Level").Value))
                        throw new XmlException("Xml's alarm action error");
                    mapTable[(EnumAlarmId)Enum.Parse(typeof(EnumAlarmId), x)] = new AlarmActionInfo { Level = y.Element("Level").Value, AlarmDescription = y.Element("AlarmMsg").Value };
                }
            }
        }

        public void MapAlarm(Msg.MsgAlarm alarm)
        {
            if (alarm == null)
                throw new NullReferenceException("AlarmMsg is null");
            else
            {
                alarmMsg = alarm;
                if (alarm.AlarmId == 0)
                    throw new ArgumentOutOfRangeException("AlarmMsg.AlarmID is empty");
                else if (!Enum.IsDefined(typeof(EnumAlarmId), alarmMsg.AlarmId))
                    throw new ArgumentException("AlarmMsg.AlarmId not be contained in EnumAlarmId");
                if (alarm.Sender == null)
                    throw new NullReferenceException("AlarmMsg.Sender is null");
                if (alarm.RecordTime > DateTime.Now)
                    throw new ArgumentException("Alarm time after now time");
                else
                {
                    if ((DateTime.Now - alarm.RecordTime).Minutes > timeOutSpec)
                        throw new TimeoutException("Alarm time over 10mins");
                }
            }
            try
            {
                alarm.Action = (EnumAlarmAction)Enum.Parse(typeof(EnumAlarmAction), this.mapTable[alarm.AlarmId].Level);
                alarm.Description = this.mapTable[alarm.AlarmId].AlarmDescription;
            }
            catch (Exception) { }
        }


    }



}
