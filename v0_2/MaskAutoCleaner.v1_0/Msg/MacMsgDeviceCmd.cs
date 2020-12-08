using System;
using System.Collections.Generic;

namespace MaskAutoCleaner.v1_0.Msg
{
    [Serializable]
    public class MacMsgDeviceCmd : MacMsgBase
    {
        public MacMsgDeviceCmd DeviceCmd;
        public List<MacMsgDeviceCmd> SubDeviceCmds = new List<MacMsgDeviceCmd>();



        public virtual string DeviceId { get; set; }
        public virtual string CommandId { get; set; }
        public virtual string CommandName { get; set; }
        public virtual string Remark { get; set; }



        public MacMsgDeviceCmd() { }
        public MacMsgDeviceCmd(MacMsgDeviceCmd cmd) { this.DeviceCmd = cmd; }









    }
}
