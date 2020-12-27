using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MvAssistant.v0_2.Mac.Hal.CompDrawer;
using MvAssistant.v0_2.Mac.Manifest;

namespace MvAssistant.v0_2.Mac.Hal.Assembly
{
    [Guid("19A85428-6B91-400C-9347-8F72159C576B")]
    public class MacHalCabinetFake : MacHalAssemblyBase, IMacHalCabinet
    {
        public IMacHalDrawer MacHalDrawer
        {
            get
            {
                //return this.
                IMacHalDrawer drawer = null;
                for (var idx = (int)MacEnumDevice.cabinet_drawer_01_01; idx <= (int)MacEnumDevice.cabinet_drawer_07_05; idx++)
                {
                    //減少拋出的Exception量, 事先判斷是否存在
                    if (!this.IsContainDevice((MacEnumDevice)idx)) continue;
                    drawer = (IMacHalDrawer)this.GetHalDevice((MacEnumDevice)idx);
                }
                return drawer;
            }
        }

        public Tuple<int, int> ReadExhaustFlowSetting()
        {
            return new Tuple<int, int>(0, 0);
        }

        public Tuple<bool, bool, bool, bool, bool, bool, bool> ReadLightCurtain()
        {
            return new Tuple<bool, bool, bool, bool, bool, bool, bool>(false, false, false, false, false, false, false);
        }

        public Tuple<int, int> ReadPressureDiff()
        {
            return new Tuple<int, int>(0,0);
        }

        public Tuple<int, int> ReadPressureDiffLimitSetting()
        {
            return new Tuple<int, int>(1,1);
        }

        public void SetExhaustFlow(int? Valve1, int? Valve2)
        {
            return;
        }

        public void SetPressureDiffLimit(uint? Gauge1Limit, uint? Gauge2Limit)
        {
            return;
        }
    }
}
