using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.MqttLike
{
    public class MqttTopicIdExistException : MacExcpetion
    {

        public MqttTopicIdExistException(string message) : base(message) { }

        public MqttTopicIdExistException(string message, Exception innerException) : base(message, innerException) { }


    }
}
