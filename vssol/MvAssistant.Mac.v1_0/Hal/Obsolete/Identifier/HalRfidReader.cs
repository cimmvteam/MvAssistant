using MvAssistant.Mac.v1_0.Hal.Component.Identifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Identifier
{

    [GuidAttribute("CB3D995E-81D6-4D0E-9642-D57AF4BF5CF2")]
    public class HalRfidReader : MacHalComponentBase, IHalRfidReader
    {
        public string ReadRfidCode()
        {
            return "";
        }

        public void SetResonanceFrquency(double hz)
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
            throw new NotImplementedException();
        }

        public int HalClose()
        {
            throw new NotImplementedException();
        }

        public bool HalIsConnected()
        {
            throw new NotImplementedException();
        }
    }
}
