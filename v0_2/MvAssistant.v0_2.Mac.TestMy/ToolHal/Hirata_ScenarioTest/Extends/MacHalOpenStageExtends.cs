using MvAssistant.v0_2.Mac;
using MvAssistant.v0_2.Mac.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.TestMy.ToolHal.Hirata_ScenarioTest.Extends
{
    public static class MacHalOpenStageExtends
    {
        public static void SortUnclampAndLock(this MacHalOpenStage instance)
        {
            instance.SortUnclamp();
            instance.Lock();
        }

        public static void SetBoxTypeAndSortClamp(this MacHalOpenStage instance, EnumMacMaskBoxType boxType)
        {
            instance.SetBoxType((uint) boxType);
            instance.SortClamp();
        }

    }
}
