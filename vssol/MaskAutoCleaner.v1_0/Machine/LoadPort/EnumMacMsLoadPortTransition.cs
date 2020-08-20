using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.LoadPort
{
    public enum EnumMacMsLoadPortTransition
    {
        // Reset
        ResetStart_ResetIng,
        ResetIng_ResetComplete,
        ResetComplete_NULL,

        // Initial
        InitialStart_InitialIng,
        InitialIng_InitialComplete,
        InitialComplete_IdleForPutPOD,
        IdleForPutPOD_NULL,

        // Dock
        DockStart_DockIng,
        DockIng_DockComplete,
        DockComplete_IdleForGetMask,
        IdleForGetMask_NULL,

        // Undock,
        UndockStart_UndockIng,
        UndockIng_UndockComplete,
        UndockComplete_IdleForGetPOD,
        IdleForGetPOD_NULL,
    }
}
