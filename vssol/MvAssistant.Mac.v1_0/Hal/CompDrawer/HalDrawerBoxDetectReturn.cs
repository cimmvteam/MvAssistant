using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompDrawer
{
    public class HalDrawerBoxDetectReturn : EventArgs
    {
        public bool? HasBox { get; set; }
    }
}
