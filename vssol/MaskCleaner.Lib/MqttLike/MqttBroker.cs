using MaskAutoCleaner.Context;
using MaskAutoCleaner.Tasking;
using System;
using System.Collections.Concurrent;

namespace MaskAutoCleaner.MqttLike
{
    public class MqttBroker : MacContextBase, IDisposable
    {
        public MacMachineMediater EqpMediater;
        public ConcurrentDictionary<EnumMqttTopicId, MqttTopic> Topics = new ConcurrentDictionary<EnumMqttTopicId, MqttTopic>();

        MacTask macTask;

        ~MqttBroker() { this.Dispose(false); }


        public MqttTopic this[EnumMqttTopicId key] { get { return this.ToipcGet(key); } }

        public MqttTopic GetOrCreate(EnumMqttTopicId topicid)
        {
            if (this.Topics.ContainsKey(topicid)) return this.Topics[topicid];
            var topic = new MqttTopic();
            topic.TopicId = topicid;
            this.Topics[topicid] = topic;
            return topic;
        }

        public void RunAsyn()
        {
            this.macTask = MacTask.RunLoopUntilCancel(MacEnumTaskPurpose.Mqtt, () =>
            {
                foreach (var hal in this.Topics)
                {
                    try
                    {
                        hal.Value.Update();
                    }
                    catch (Exception ex)
                    {
                        this.EqpMediater.ProcAlarm(this, ex);
                    }
                }
                return true;
            }, 100);


        }




        public MqttTopic ToipcGet(EnumMqttTopicId topicId)
        {
            if (this.Topics.ContainsKey(topicId)) return this.Topics[topicId];
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="topicId"></param>
        /// <param name="isForce"></param>
        /// <returns>回傳受影響的訂閱者數量</returns>
        public int TopicCancel(EnumMqttTopicId topicId, bool isForce = false)
        {
            if (!this.Topics.ContainsKey(topicId)) return 0;
            var cnt = this.Topics[topicId].CountOfSubscriber();
            if (!isForce && cnt > 0) return 0;
            MqttTopic item;
            for (var idx = 0; idx < 3; idx++)
                if (this.Topics.TryRemove(topicId, out item))
                    return cnt;

            return 0;
        }
        public MqttTopic TopicPublish(EnumMqttTopicId topicId, object publisher, Func<MqttSignal> readSensor, bool isThrowIfExist = true)
        {
            if (publisher == null) { throw new ArgumentNullException(); }
            if (readSensor == null) { throw new ArgumentNullException(); }

            var topic = this.GetOrCreate(topicId);
            if (topic.ReadSensor != null)
            {
                if (isThrowIfExist) throw new MqttTopicIdExistException("重複定義Topic");
                return topic;
            }
            topic.Context = this;
            topic.TopicId = topicId;
            topic.ReadSensor = readSensor;
            return topic;
        }
        public MqttTopic TopicSubscribe(EnumMqttTopicId topicId, MqttSubscriber subscriber)
        {
            var topic = this.GetOrCreate(topicId);
            topic.AddSubscriber(subscriber);
            return topic;
        }
        public MqttTopic TopicUnsubscribe(EnumMqttTopicId topicId, MqttSubscriber subscriber)
        {
            var topic = this.GetOrCreate(topicId);
            topic.RemoveSubscriber(subscriber);
            return topic;
        }
        public void TopicUnsubscribe(MqttSubscriber subscriber)
        {
            foreach (var topic in this.Topics)
                topic.Value.RemoveSubscriber(subscriber);
        }







        #region IDisposable

        // Flag: Has Dispose already been called?
        protected new bool disposed = false;
        // Public implementation of Dispose pattern callable by consumers.
        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // Protected implementation of Dispose pattern.
        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //

            this.DisposeSelf();

            disposed = true;
        }


        protected override void DisposeSelf()
        {
            //TODO: Task Cancel

            if (this.macTask != null)
            {
                using (var obj = this.macTask)
                {
                    obj.Cancel();
                    obj.Wait(500);
                }
            }
        }



        #endregion

    }
}
