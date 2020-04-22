using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Intf.Component
{
    [GuidAttribute("70A69CAE-68EE-4E4A-9A43-A390B05BD0D4")]
    public interface IHalPlc : IHalComponent
    {
        Object GetValue(string varName);
        void SetValue(string varName,Object value);
    }
}
