using CToolkit.v1_0.Secs;
using MaskAutoCleaner.Msg.PrescribedSecs;
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
    public class MsgSecs : MsgBase
    {
        public bool IsFromExternal;
        public string MessageName;


        public PrescribedSecsBase PrescribedSecs;


        public T As<T>() where T : class { return this as T; }


    }
}
