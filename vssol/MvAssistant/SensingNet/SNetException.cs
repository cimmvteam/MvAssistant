using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensingNet.v0_2
{
    [Serializable]
    public class SNetException : Exception
    {
        public SNetException(string msg) : base(msg) { }




    }
}
