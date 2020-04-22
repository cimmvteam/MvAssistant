using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaskAutoCleaner.Hal.Intf.Component;
using System.Runtime.InteropServices;
using MaskAutoCleaner.Hal.Intf.Component.Infrared;


namespace MaskAutoCleaner.Hal.ImpFake.Component.Infrared
{

    [GuidAttribute("910C05CC-7848-426D-BEF4-6DE00E0A8A91")]
    public class HalInfraredPhotointerrupter : HalFakeBase, IHalInfraredPhotointerrupter
    {
        public float fake = 0;

        public bool SetIrAddress(string varName)
        {
            throw new NotImplementedException();
        }

        public float GetValue()
        {


            return fake;
        }

        public void HalZeroCalibration()
        {
           
        }

        public int HalConnect()
        {
            return 0;
        }

        public int HalClose()
        {
            return 0;
        }

        public bool HalIsConnected()
        {
            return true;
        }

 
    }
}
