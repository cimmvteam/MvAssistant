using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.MqttLike
{
    public class MqttSubscriber
    {
        public Object Subscriber;
        public Action<MqttBroadcastInfo> Broadcast;


    }
}
