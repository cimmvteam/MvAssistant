using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SensingNet.v0_2.Framework.Storage
{
    public class SNetFileStorageEventArgs : EventArgs
    {
        public string PrevFilePath;
        public string CurrFilePath;

    }
}
