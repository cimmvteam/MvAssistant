using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Hal.Component;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Camera
{
    [GuidAttribute("90BA4CDC-7A82-454A-8F3F-6FE6413AEF41")]
    public class HalCameraSenTech : HalComponentBase, IHalCamera
    {
        int IHal.HalConnect()
        {
            throw new NotImplementedException();
        }

        int IHal.HalClose()
        {
            throw new NotImplementedException();
        }

        bool IHal.HalIsConnected()
        {
            throw new NotImplementedException();
        }

        Image IHalCamera.Shot()
        {
            throw new NotImplementedException();
        }

        void IHalCamera.SetExposureTime(double mseconds)
        {
            throw new NotImplementedException();
        }

        void IHalCamera.SetFocus(double percentage)
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


        public Image Shot(string imgFolderPath)
        {
            throw new NotImplementedException();
        }
    }
}
