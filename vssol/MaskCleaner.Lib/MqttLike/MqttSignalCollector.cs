using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.MqttLike
{
    public class MqttSignalCollector
    {
        public int PurgeSecond = 60;
        public SortedDictionary<MqttTimeMs, List<MqttSignal>> Collector = new SortedDictionary<MqttTimeMs, List<MqttSignal>>();


        public void AddSignal(MqttSignal val, MqttTimeMs? time = null)
        {
            var t = time.HasValue ? time.Value : DateTime.Now;
            if (!this.Collector.ContainsKey(t)) this.Collector[t] = new List<MqttSignal>();

            var vals = this.Collector[t];
            vals.Add(val);


            var oldTime = (MqttTimeMs)DateTime.Now.AddSeconds(-this.PurgeSecond);
            var query = from row in this.Collector
                        where row.Key < oldTime
                        select row;




        }


    }
}
