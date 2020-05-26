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
    public class MsgSecsCeid : MsgSecs
    {
        public EnumCeid Ceid;
        



    }
}
