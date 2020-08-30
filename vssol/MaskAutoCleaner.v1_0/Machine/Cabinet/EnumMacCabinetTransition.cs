using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    public enum EnumMacCabinetTransition
    {
        Idle_InitialAllDrawerStart,
        InitialAllDrawersStart_InitialAllDrawersIng,
        InitialAllDrawersIng_InitialAllDrawersComplete,
        InitialAllDrawersComplete_WaitingAcceptLoad,
        InitialAllDrawersComplete_AllDrawersInitialError,
    }
}
