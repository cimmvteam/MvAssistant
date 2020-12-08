using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RosTest
{
    public class Subscriber
    {
        Subscription<object> Token;
        EventAggregator eventAggregator;

        private string rcvStrMsg;
        private string[] rcvStrMsgArray;
        private double rcvDoubleMsg;
        private double[] rcvDoubleMsgArray;
        bool msgBindingFlag = false;
        
        /** //vs 2013
        public string RcvStrMsg { get => rcvStrMsg; }
        public string[] RcvStrMsgArray { get => rcvStrMsgArray; }
        public double RcvDoubleMsg { get => rcvDoubleMsg; }
        public double[] RcvDoubleMsgArray { get => rcvDoubleMsgArray; }
        */

        public string RcvStrMsg { get { return rcvStrMsg;} }
        public string[] RcvStrMsgArray { get { return rcvStrMsgArray; } }
        public double RcvDoubleMsg { get { return rcvDoubleMsg; } }
        public double[] RcvDoubleMsgArray { get { return  rcvDoubleMsgArray; } }


        public Subscriber(EventAggregator eve)
        {
            eventAggregator = eve;
            eve.Subscribe<object>(this.RcvMsg);
        }

        private void RcvMsg(Object obj)
        {
            if (msgBindingFlag)
            {
                if (obj.GetType().Equals(typeof(double)))
                    rcvDoubleMsg = (double)obj;
                else if (obj.GetType().Equals(typeof(double[])))
                    rcvDoubleMsgArray = (double[])obj;
                else if (obj.GetType().Equals(typeof(string)))
                    rcvStrMsg = (string)obj;
                else if (obj.GetType().Equals(typeof(double[])))
                    rcvStrMsgArray = (string[])obj;
            }
            eventAggregator.UnSbscribe(Token);
        }

        public void BindMsgMemory(double dMsg, double[] dMsgArray, string sMsg, string[] sMsgArray)
        {
            rcvStrMsg = sMsg;
            rcvStrMsgArray = sMsgArray;
            rcvDoubleMsg = dMsg;
            rcvDoubleMsgArray = dMsgArray;
            msgBindingFlag = true;
        }
    }
}
