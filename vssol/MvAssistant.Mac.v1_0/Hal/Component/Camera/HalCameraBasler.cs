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
    [GuidAttribute("A6DDB05A-1D8A-4CC0-8039-0162D6FB4670")]
    public class HalCameraBasler : MacHalComponentBase, IHalCamera
    {




        Image IHalCamera.Shot()
        {
            throw new NotImplementedException();
        }

        Image IHalCamera.Shot(string imgFolderPath)
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


        #region IHal

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

        #endregion
    }
}
