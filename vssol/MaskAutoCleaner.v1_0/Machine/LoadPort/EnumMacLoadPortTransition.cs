using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.LoadPort
{
    public enum EnumMacLoadPortTransition
    {
        //
        AlarmResetStart_AlarmResetIng,
        AlarmResetIng_AlarmResetComplete,
        InitialStart_InitialIng,
        InitialIng_InitialComplete,
        DockStart_DockIng,
        DockWithMaskIng_DockWithMaskComplete,
        DockIng_DockComplete,
        DockComplete_IdleForGetMask,
        UndockStart_UndockIng,
        IdleForGetPOD_NULL,
        AlarmResetComplete_InitialStart,
        InitialComplete_Idle,
        Idle_NULL,
        Idle_IdleForGetPODWithMask,
        IdleForGetPODWithMask_DockWithMaskStart,
        DockWithMaskStart_DockWithMaskIng,
        IdleForReleaseMask_NULL,
        DockWithMaskComplete_IdleForReleaseMask,
        IdleForGetPOD_DockStart,
        IdleForGetPODWithMask_NULL,
        DockComplete_NULL,
       IdleForGetMask_UndockWithMaskStart,
        UndockWithMaskStart_UndockWithMaskIng,
        UndockWithMaskIng_UndockWithMaskComplete,
        UndockWithMaskComplete_IdleForReleasePODWithMask,
        IdleForReleasePODWithMask_NULL,
        IdleForReleaseMask_UndockStart,
        tUndockIng_UndockComplete,
        UndockComplete_IdleForReleasePOD,
        IdleForReleasePOD_UndockStart,
        IdleForReleaseMask_UndockWithMaskStart,
        ToIdleForGetMask_UndockStart,
        IdleForReleasePODWithMask_Idle,
        IdleForReleasePOD_Idle,
        Idle_IdleForGetPOD,
    }
}
