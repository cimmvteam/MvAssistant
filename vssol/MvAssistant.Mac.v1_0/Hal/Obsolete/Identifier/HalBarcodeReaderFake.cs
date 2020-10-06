using MvAssistant.Mac.v1_0.Hal.Component.Identifier;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.ComponentFake.Identifier
{
    [GuidAttribute("F6CA4F51-38B6-4658-8F1E-86F3CF559FD1")]
    class HalBarcodeReaderFake : HalFakeBase, IHalBarcodeReader
    {
        public string ReadBarcode()
        {
            return "I_Love_TH";
        }

        public void SetReadMode(int mode)
        {
            return;
        }

        public void HalZeroCalibration()
        {
            return;
        }
    }
}
