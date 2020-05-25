using System;
using System.Collections.Generic;

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
