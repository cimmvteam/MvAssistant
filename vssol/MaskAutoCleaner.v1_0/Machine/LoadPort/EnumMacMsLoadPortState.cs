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
    public enum  EnumMacMsLoadPortState
    {
  
        //Dock
        DockStart,
        DockIng,
        DockComplete,
      
        //Undock,
        UndockStart,
        UndockIng,
        UndockComplete,
      
       // Reset,
        ResetStart,
        ResetIng,
        ResetComplete,
 
        //Initial,
        InitialStart,
        InitialIng,
        InitialComplete,
       
         // Idle
        IdleForGetMask,
        IdleForGetPOD,
        IdleForPutPOD,
    }
}
