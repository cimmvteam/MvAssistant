using MvLib;
using MvLib.StateMachine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace MaskAutoCleaner.Msg
{
    [Serializable]
    public class MsgBase : IMsg, IStateParam
    {
        public DateTime RecordTime = DateTime.Now;
        public Object Sender;
        public string TransName { get; set; }
    }
}
