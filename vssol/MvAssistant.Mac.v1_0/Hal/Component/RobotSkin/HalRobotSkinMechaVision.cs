using MaskAutoCleaner.Hal.Intf.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Imp.Component.RobotSkin
{

    [GuidAttribute("220AC03C-8A5F-4CA8-8787-F70F6BC0D7E1")]
    public class HalRobotSkinMechaVision : HalComponentBase, IHalRobotSkin
    {


        public float GetValue()
        {
            throw new NotImplementedException();
        }

        public string ID { get; set; }

        public string DeviceConnStr { get; set; }

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
