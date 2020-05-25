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
    public class MsgDeviceCmd : MsgBase
    {
        public MsgDeviceCmd DeviceCmd;
        public List<MsgDeviceCmd> SubDeviceCmds = new List<MsgDeviceCmd>();



        public virtual string DeviceId { get; set; }
        public virtual string CommandId { get; set; }
        public virtual string CommandName { get; set; }
        public virtual string Remark { get; set; }



        public MsgDeviceCmd() { }
        public MsgDeviceCmd(MsgDeviceCmd cmd) { this.DeviceCmd = cmd; }









    }
}
