using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MaskAutoCleaner.MqttLike
{
    public class MqttTopic
    {
        public MqttBroker Context;
        public bool HasNoUpdateIfNoSubScribers = false;
        public MqttTimeMs LastUpdateTime;
        public Object Publisher;//May no important if it recongnize by topic id
        public TimeSpan ReadInterval = new TimeSpan(100 * TimeSpan.TicksPerMillisecond);
        public Func<MqttSignal> ReadSensor;
        public DateTime ReadTime = DateTime.Now;
        public MqttSignalCollector Signals = new MqttSignalCollector();

        protected List<MqttSubscriber> Subscribers = new List<MqttSubscriber>();
        public EnumMqttTopicId TopicId = EnumMqttTopicId.None;



        public int CountOfSubscriber() { return this.Subscribers.Count; }

        public void AddSubscriber(MqttSubscriber subscriber)
        {
            try
            {
                if (!Monitor.TryEnter(this, 10 * 1000)) { throw new MacExcpetion("無法取得物件鎖"); }
                this.Subscribers.Add(subscriber);
            }
            finally { Monitor.Exit(this); }
        }
        public void RemoveSubscriber(MqttSubscriber subscriber)
        {
            try
            {
                if (!Monitor.TryEnter(this, 10 * 1000)) { throw new MacExcpetion("無法取得物件鎖"); }
                this.Subscribers.Remove(subscriber);
            }
            finally { Monitor.Exit(this); }
        }


        public MqttSignal GetLastSignal()
        {
            if (this.Signals.Collector.Count == 0) return null;
            var query = this.Signals.Collector.Last();
            return query.Value.LastOrDefault();
        }



        public void Update()
        {
            if (this.HasNoUpdateIfNoSubScribers && this.Subscribers.Count == 0) return;
            if (this.ReadSensor == null) return;//尚未設定Publisher

            var now = DateTime.Now;
            this.ReadTime = now;

            //Get sensor value
            var obj = this.ReadSensor();

            var time = (MqttTimeMs)now;
            this.Signals.AddSignal(obj, time);

            this.LastUpdateTime = now;

            var info = new MqttBroadcastInfo();
            info.Topic = this;
            info.Time = now;
            info.NewSignal = obj;


            var subs = new List<MqttSubscriber>();//先放入區域變數, 避免被修改而拋出exception
            try
            {
                if (!Monitor.TryEnter(this, 10 * 1000)) { throw new MacExcpetion("無法取得物件鎖"); }
                subs = this.Subscribers.ToList();
            }
            finally { Monitor.Exit(this); }


            //
            Parallel.ForEach(subs, (item) =>
            {
                item.Broadcast(info);
            });

        }






    }
}
