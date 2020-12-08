using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.LoadPort
{
    public enum  EnumMacMcLoadPortCmd
    {
        SystemBootup,
        ToGetPOD,
        ToGetPODWithMask,
        Dock,
        UndockWithMaskFromIdleForGetMask,
        ReleasePODWithMask,
        DockWithMask,
        UndockFromIdleForRelesaseMask,
        ReleasePOD,
        UndockFromIdleForGetMask,
        UndockWithMaskFromIdleForRelesaseMask

    }
}
