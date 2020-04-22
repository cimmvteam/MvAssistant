using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Hal.Component;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.E84
{

    [GuidAttribute("6992B3E5-6303-4C58-944C-50751DC55E50")]
    public class HalE84 : HalComponentBase, IHalE84
    {
        private bool oht_Aligned;
        private string e84_OHTStatus;

        #region for test script
        public bool Oht_Aligned
        {
            get { return oht_Aligned; }
            set { oht_Aligned = value; }
        }

        public string E84_OhtStatus
        {
            get { return e84_OHTStatus; }
            set { e84_OHTStatus = value; }
        }
        #endregion


        public bool HalGetOhtAligned()
        {
            return oht_Aligned;
        }

        public string HalGetOhtStatus()
        {
            return e84_OHTStatus;
        }

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