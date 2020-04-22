using MaskAutoCleaner.Hal.Intf.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MaskAutoCleaner.Hal.Imp.Component.Modbus
{
    [GuidAttribute("A50685A5-7FFB-4FB0-8042-0A3EA70AEDE4")]
    public class HalSensorModbus : HalComponentBase, IHalModbus
    {
        public void HalZeroCalibration()
        {
            throw new NotImplementedException();
        }

        public string ID
        {
            get;
            set;
        }

        public string DeviceConnStr
        {
            get;
            set;
        }

        public int HalConnect()
        {
            throw new NotImplementedException();
        }

        public int HalClose()
        {
            throw new NotImplementedException();
        }

        public bool HalIsConnected()
        {
            throw new NotImplementedException();
        }
    }
}
