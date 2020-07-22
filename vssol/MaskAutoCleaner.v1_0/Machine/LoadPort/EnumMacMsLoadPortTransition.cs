using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.LoadPort
{
    public enum EnumMacMsLoadPortTransition
    {
       Reset,
       Initial,
        Dock,
       ReadyToUndock,
       Undock,
       ReadyToUnload,
       Unload,
    }
}
