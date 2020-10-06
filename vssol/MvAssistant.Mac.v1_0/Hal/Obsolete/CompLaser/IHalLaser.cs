using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component
{
    [GuidAttribute("CD7EE9C1-5A1C-41AE-B51A-203839B4486B")]
    public interface IHalLaser : IMacHalComponent
    {
        bool SetAddress(string varName);
        float Read();
    }
}