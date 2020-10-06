using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Force6Axis
{
    [GuidAttribute("E75DDA06-CF8C-4321-B241-3E509EA7F994")]
    public class IHalForce6AxisEventArgs : EventArgs
    {

        public HalForce6AxisVector centerForceVector;
        public HalForce6AxisVector rawForceVector;
        public HalForce6AxisVector correctForceVector { get { return new HalForce6AxisVector(rawForceVector - centerForceVector); } }


   


    }
}
