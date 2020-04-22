using System;
using MaskAutoCleaner.Hal.Intf.Component.Motor;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MaskAutoCleaner.Hal.Imp.Component.Motor
{
    [GuidAttribute("582437CC-52F7-40E0-8E50-D15EF16C8B62")]
    public class HalLoadClamper : HalComponentBase, IHalClamper
    {
        private bool isShrinked;

        public bool HalIsShrinked()
        {
            return isShrinked;
        }
        public void HalShrinked(){}

        public void HalReleased(){}

        public string ID { get; set; }
        public string DeviceConnStr { get; set; }

        /// <summary>
        /// implement device connect / initialize in here
        /// </summary>
        /// <returns>0: success; 1: common fail; 2以上的數字各device可自行定義</returns>
        public int HalConnect()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// implement connection terminate in here
        /// </summary>
        /// <returns>0: success; 1: common fail; 2以上的數字各device可自行定義</returns>
        public int HalClose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// device是否連線正常 / session正常
        /// </summary>
        /// <returns>true: still alive; false: zombie</returns>
        public bool HalIsConnected()
        {
            throw new NotImplementedException();
        }
    }
}
