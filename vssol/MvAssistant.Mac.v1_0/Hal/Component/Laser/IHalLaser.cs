using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Intf.Component
{
    [GuidAttribute("CD7EE9C1-5A1C-41AE-B51A-203839B4486B")]
    public interface IHalLaser : IHalComponent
    {
        bool SetAddress(string varName);
        float Read();
    }
}