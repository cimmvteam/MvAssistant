using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Intf.Component
{
    [GuidAttribute("AAAE12DC-A001-4200-9EC1-DE63247603BB")]
    public interface IHalSECSMODULE : IHalComponent
    {
        bool ConnectTAP(string IPAddress, int port); 
        bool SendToTAP(string StreamFunction, object data);
        void StartListenTAP();
    }
}
