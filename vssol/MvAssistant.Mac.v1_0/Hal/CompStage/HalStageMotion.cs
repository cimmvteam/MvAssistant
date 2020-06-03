using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Stage
{
    [GuidAttribute("F1F2FF75-55BC-4BAD-AF8F-A6A4836A6E12")]
    public class HalStageMotion
    {
        public int Speed = 60;
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double R { get; set; }
    }
}
