using MaskAutoCleaner.Hal.Intf.Component;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MaskAutoCleaner.Hal.ImpFake.Component.E84
{
    [GuidAttribute("439D2345-C6F5-456E-AD96-675028935C03")]
    public class HalE84Fake : HalFakeBase, IHalE84
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
    }
}