using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaskAutoCleaner.Hal.Intf.Component;
using System.Runtime.InteropServices;

namespace MaskAutoCleaner.Hal.Imp.Component.Laser
{
    [GuidAttribute("481067B7-7092-40F6-8904-6D309F7DC45B")]
    public class HalLaserOmron : HalComponentBase, IHalLaser
    {

        public bool SetAddress(string varName)
        {
            throw new NotImplementedException();
        }

        public float Read()
        {
            throw new NotImplementedException();
        }

        public string ID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string DeviceConnStr
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
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
