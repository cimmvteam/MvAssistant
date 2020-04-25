using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Hal.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Component.Camera
{
    [GuidAttribute("1BC89ED0-0697-4670-8B67-D6CB8E9F8068")]
    public class HalCameraiDSU3Cam : MacHalComponentBase, IHalCamera
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
