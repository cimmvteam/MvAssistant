using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RosTest
{
    public class Publisher
    {
        EventAggregator EventAggregator;
        public Publisher(EventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
        }

        public void PublishMsg(Object Msg)
        {
            if(Msg.GetType().Equals(typeof(string))|| Msg.GetType().Equals(typeof(double))|| Msg.GetType().Equals(typeof(double[]))|| Msg.GetType().Equals(typeof(int)))
                EventAggregator.Publish(Msg);
        }
    }
}
