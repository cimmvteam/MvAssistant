using MaskAutoCleaner.v1_0.Msg.SecsReport;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace MaskAutoCleaner.v1_0.Msg
{
    [Serializable]
    public class MacMsgSecs : MacMsgBase
    {
        public bool IsFromExternal;
        public string MessageName;


        public MacSecsReportBase PrescribedSecs;


        public T As<T>() where T : class { return this as T; }


    }
}
