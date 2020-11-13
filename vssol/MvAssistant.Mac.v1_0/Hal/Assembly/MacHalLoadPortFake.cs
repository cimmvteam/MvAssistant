using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MvAssistant.Mac.v1_0.Hal.CompLoadPort;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [Guid("24DF35C8-AF37-4103-BE97-770294647EEF")]
    public class MacHalLoadPortFake : MacHalAssemblyBase, IMacHalLoadPort
    {
        public IMacHalLoadPortUnit LoadPortUnit
        {
            get
            {
                IMacHalLoadPortUnit rtnV = null;
                for (var i = (int)MacEnumDevice.loadport_1; i <= (int)MacEnumDevice.loadport_2; i++)
                {
                    try
                    {
                        rtnV = (IMacHalLoadPortUnit)this.GetHalDevice((MacEnumDevice)i);
                        if (rtnV != null)
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return rtnV;

            }

        }

        public string Dock()
        { return "OK"; }

        public string Undock()
        { return "OK"; }

        public bool ReadLP_Light_Curtain()
        {
            return false;
        }

        public Tuple<int, int> ReadPressureDiff()
        {
            return new Tuple<int, int>(0, 0);
        }

        public Tuple<int, int> ReadPressureDiffLimitSrtting()
        {
            return new Tuple<int, int>(1, 1);
        }

        public void SetPressureDiffLimit(uint? Gauge1Limit, uint? Gauge2Limit)
        {
            return;
        }
    }
}
