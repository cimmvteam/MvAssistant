using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Force6Axis
{
    [GuidAttribute("AB56F7F8-546C-4CA3-8889-E203651D50A3")]
    public interface IHalForce6Axis : IMacHalComponent
    {
        HalForce6AxisVector GetVector();
        



        event EventHandler<IHalForce6AxisEventArgs> evtDataReceive;



    }
}
