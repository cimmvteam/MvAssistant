using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Intf.Component.Identifier
{
    [GuidAttribute("193384E0-673B-49BD-938B-64415C1E6976")]
    public interface IHalRfidReader : IHalComponent
    {
        string ReadRfidCode();
        void SetResonanceFrquency(double hz);
    }
}
