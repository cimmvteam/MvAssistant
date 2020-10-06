using CodeExpress.v1_0.Secs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensingNet.v0_2.QSecs
{
    public class SNetQSecsRcvDataEventArgs : EventArgs
    {
        public SNetQSecsHandler handler;
        public CxHsmsMessage message;

    }
}
