using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.LoadPort
{
    public enum EnumMacMsLoadPortTransition
    {
        ResetStart_ResetIng,
        ResetIng_ResetComplete,
        ResetIng_ResetFail,
        ResetIng_ResetTimeOut,
        
        InitialStart_Initialing,
        Initialing_InitialComplete,
        Initialing_InitialTimeOut,
        Initialing_InitialMustReset,

     //   IdleReadyToDock_DockStart,

        DockStart_DockIng,
        DockIng_DockComplete,
        DockIng_DockTimeOut,
        DockIng_DockMustReset,
        DockIng_DockMustInitial,

     //  ReadyToUndock,
      //  Undock,
        UndockStart_UndockIng,
        UndockIng_UndockComplete,
        UndockIng_UndockMustInitial,
        UndockIng_UndockMustReset,
        UndockIng_UndockTimeOut,
       
    }
}
