using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Hal.Component;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompCamera
{
    [GuidAttribute("90BA4CDC-7A82-454A-8F3F-6FE6413AEF41")]
    public class HalCameraSenTech : MacHalComponentBase, IHalCamera
    {
        public void SetExposureTime(double mseconds)
        {
            throw new NotImplementedException();
        }

        public void SetFocus(double percentage)
        {
            throw new NotImplementedException();
        }

        public Image Shot()
        {
            throw new NotImplementedException();
        }
    }
}
