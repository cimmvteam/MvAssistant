using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.LoadPort
{
    /// <summary>狀態</summary>
    /// <remarks>
    /// Postfix:
    /// 完成: Complete
    /// 無法完成: Failed
    /// </remarks>
    public enum  EnumMacLoadPortState
    {
       
        DockStart,
        DockIng,
        DockComplete,
        UndockStart,
        UndockIng,
        UndockComplete,
        AlarmResetStart,
        AlarmResetIng,
        AlarmResetComplete,
        InitialStart,
        InitialIng,
        InitialComplete,
        IdleForReleasePOD,
        IdleForReleasePODWithMask,
        IdleForReleaseMask,
        IdleForGetMask,
        IdleForGetPOD,
        Idle,
        IdleForGetPODWithMask,
        DockWithMaskStart,
        DockWithMaskIng,
        DockWithMaskComplete,
        UndockWithMaskStart,
        UndockWithMaskIng,
        UndockWithMaskComplete,
    }
}
