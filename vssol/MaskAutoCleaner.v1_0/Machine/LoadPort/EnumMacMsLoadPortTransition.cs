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
