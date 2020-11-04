using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.LoadPort
{
    public enum EnumMacMsLoadPortTransition
    {
        // System
        SystemBootup_SystemBootupAlarmResetStart,
        SystemBootupAlarmResetStart_SystemBootupAlarmResetIng,
        SystemBootupAlarmResetIng_SystemBootupAlarmResetComplete,
        SystemBootupAlarmResetComplete_SystemBootupInitialStart,
        SystemBootupInitialStart_SystemBootupInitialIng,
        SystemBootupInitialIng_SystemBootupInitialComplete,
        SystemBootupInitialComplete_IdleForPutPOD,

        // Reset
        AlarmResetStart_AlarmResetIng,
        AlarmResetIng_AlarmResetComplete,
        AlarmResetComplete_NULL,

        // Initial
        InitialStart_InitialIng,
        InitialIng_InitialComplete,
        InitialComplete_IdleForPutPOD,
        IdleForPutPOD_NULL,

        // Dock
        DockStart_DockIng,
        DockWithMaskIng_DockWithMaskComplete,
        DockComplete_IdleForGetMask,
        IdleForGetMask_NULL,

        // Undock,
        UndockStart_UndockIng,
        UndockIng_UndockComplete,
        UndockComplete_IdleForGetPOD,
        IdleForGetPOD_NULL,
        AlarmResetComplete_InitialStart,
        InitialComplete_Idle,
        Idle_NULL,
        Idle_IdleForGetPODWithMask,
        IdleForGetPOD_DockStart,
        IdleForGetPODWithMask_DockWithMaskStart,
        DockWithMaskStart_DockWithMaskIng,
        DockIng_DockWithMaskComplete,
        IdleForReleaseMask_NULL,
        Idle_IdleForGetPOD,
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
        IdleForGetMask_UndockStart,
        IdleForReleasePODWithMask_Idle,
        IdleForReleasePOD_Idle,
    }
}
