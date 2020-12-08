using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Hal.CompDrawer
{
    public class HalDrawerBoxDetectReturn : EventArgs
    {
        public bool? HasBox { get; set; }
    }
}
