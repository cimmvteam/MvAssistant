using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    public enum EnumMacCabinetTransition
    {
        AnyState_NULL,

        StateMachineLoadAllDrawersStateMchineStart_StateMachineLoadAllDrawersStateMchineIng,
        StateMachineLoadAllDrawersStateMchineIng_StateMachineLoadAllDrawersStateMchineCompplete,
        StateMachineLoadAllDrawersStateMchineCompplete_AnyState,


        LoadMoveDrawerTraysToOutStart_LoadMoveDrawerTraysToOutIng,
        LoadMoveDrawerTraysToOutIng_LoadMoveDrawerTraysToOutComplete,
        LoadMoveDrawerTraysToOutComplete_AnyState,

        InitialDrawersStart_InitialDrawersIng,
        InitialDrawersIng_InitialDrawersComplete,
        InitialDrawersComplete_AnyState,

        SynchronousDrawerStatesStart_SynchronousDrawerStatesIng,
        SynchronousDrawerStatesIng_SynchronousDrawerStatesComplete,
        SynchronousDrawerStatesComplete_AnyState

    }
}
