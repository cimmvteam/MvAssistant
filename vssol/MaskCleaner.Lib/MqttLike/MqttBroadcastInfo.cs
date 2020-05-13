using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.MqttLike
{
    public class MqttBroadcastInfo
    {
        public MqttTopic Topic;
        public MqttTimeMs Time;
        public MqttSignal NewSignal;
    }
}
