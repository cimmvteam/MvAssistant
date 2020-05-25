using MvAssistant.Mac.v1_0.Hal.Component.Identifier;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.ComponentFake.Identifier
{
    [GuidAttribute("0EF8119A-5A96-4D94-8F53-359ACACD5572")]
    public class HalRfidReaderFake : HalFakeBase, IHalRfidReader
    {
        public string ReadRfidCode()
        {
            return "I_love_TH";
        }

        public void SetResonanceFrquency(double hz)
        {
            return;
        }

        public void HalZeroCalibration()
        {
            return;
        }
    }
}
