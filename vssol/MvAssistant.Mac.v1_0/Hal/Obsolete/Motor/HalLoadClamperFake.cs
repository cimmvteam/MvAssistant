using MvAssistant.Mac.v1_0.Hal.Component.Motor;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.ComponentFake.Motor
{
    [GuidAttribute("97E99905-F3D8-4310-83D6-B68931C16797")]
    public class HalLoadClamperFake : HalFakeBase, IHalClamper
    {
        #region for test script
        private bool shrinked;
        public bool Shrinked
        {
            get { return shrinked; }
            set { shrinked = value; }
        }
        #endregion

        public bool HalIsShrinked()
        {
            return shrinked;
        }
        public void HalShrinked()
        {}

        public void HalReleased(){}
    }
}
