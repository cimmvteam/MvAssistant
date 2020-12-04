using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest.Extends
{
    public static class MacHalOpenStageExtends
    {
        public static void SortUnclampAndLock(this MacHalOpenStage instance)
        {
            instance.SortUnclamp();
            instance.Lock();
        }

        public static void SetBoxTypeAndSortClamp(this MacHalOpenStage instance, BoxType boxType)
        {
            instance.SetBoxType((uint) boxType);
            instance.SortClamp();
        }

    }
}
