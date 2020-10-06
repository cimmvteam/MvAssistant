using MvAssistant.Mac.v1_0.Hal.Component.Identifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Identifier
{
    [GuidAttribute("3175D644-9669-4144-B438-D86D777943EF")]
    public class HalBarcodeReader : MacHalComponentBase, IHalBarcodeReader
    {
        public string ReadBarcode()
        {
            return "";
        }

        public void SetReadMode(int mode)
        {
            return;
        }

        public void HalZeroCalibration()
        {
            return;
        }

        public string ID { get; set; }

        public string DeviceConnStr { get; set; }

        public int HalConnect()
        {
            return 0;
        }

        public int HalClose()
        {
            return 0;
        }

        public bool HalIsConnected()
        {
            return true;
        }
    }
}
