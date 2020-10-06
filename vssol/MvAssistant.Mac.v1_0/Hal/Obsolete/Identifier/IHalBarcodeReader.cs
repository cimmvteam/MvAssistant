using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Identifier
{
    [GuidAttribute("9E8476AA-1623-4ED0-A01B-659ECB269713")]
    public interface IHalBarcodeReader : IMacHalComponent
    {
        string ReadBarcode();
        void SetReadMode(int mode);
    }
}
